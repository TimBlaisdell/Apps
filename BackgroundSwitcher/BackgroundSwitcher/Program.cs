using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using JSON;

namespace BackgroundSwitcher {
    class Program {
        private static void Form_OnEditImage(object sender, string imgpath) {
            if (string.IsNullOrEmpty(_settings.EditImageCommand)) {
                MessageBox.Show("No EditImageCommand is configured.");
            }
            Process.Start(new ProcessStartInfo(_settings.EditImageCommand, $"\"{imgpath}\""));
        }
        private static void Cleanup() {
            Log.Write("Cleanup started.");
            string filesfname = Path.Combine(_dataPath, "Files.json");
            if (File.Exists(filesfname)) {
                var newfiles = new JSONArray();
                try {
                    int removed = 0;
                    var files = new JSONArray(File.ReadAllText(filesfname)).ToArray<JSONImageInfo>();
                    var filesByName = new Dictionary<string, List<JSONImageInfo>>();
                    foreach (var file in files) {
                        if (File.Exists(file.Path)) {
                            newfiles.put(file);
                            var fn = Path.GetFileNameWithoutExtension(file.Path);
                            if (!filesByName.ContainsKey(fn)) filesByName.Add(fn, new List<JSONImageInfo>());
                            filesByName[fn].Add(file);
                        }
                        else removed++;
                    }
                    File.WriteAllText(filesfname, newfiles.ToString(true, 0));
                    Log.Write("Cleaned Files.json: removed " + removed + " nonexistent files, found " + newfiles.Count + " files.");
                    foreach (string fn in filesByName.Keys) {
                        var list = filesByName[fn];
                        if (list.Count <= 1) continue;
                        Log.Write(list.Count + " files with the same name found:");
                        foreach (var f in list) Log.Write("   " + f.Path);
                    }
                }
                catch (Exception ex) {
                    Log.Write("Exception caught while cleaning Files.json: " + ex.Message);
                }
            }
            else Log.Write("No Files.json file to clean up.");
        }
        private static RectangleF[] Divide(Rectangle rect, int margin) {
            var rectf = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            return Divide(rectf, margin);
        }
        /// <summary>
        ///     Divides rect into two randomized rectangles with the specified margin between them, such that neither has an aspect
        ///     ratio outside the range of MinRatio - MaxRatio.
        /// </summary>
        private static RectangleF[] Divide(RectangleF rect, int margin) {
            RectangleF rect1 = RectangleF.Empty;
            RectangleF rect2 = RectangleF.Empty;
            int attempts = 0;
            do {
                if (++attempts > 100) break;
                bool vert = _rand.Next(2) == 0;
                if (vert) {
                    var width = rect.Width / 2;
                    width += _rand.Next((int)Math.Round(width)) - width / 2;
                    rect1 = new RectangleF(rect.Left, rect.Top, width - margin / 2.0F, rect.Height);
                    rect2 = new RectangleF(rect.Left + width + margin / 2.0F, rect.Top, (rect.Width - width) - margin / 2.0F, rect.Height);
                }
                else {
                    var height = rect.Height / 2;
                    height += _rand.Next((int)Math.Round(height)) - height / 2;
                    rect1 = new RectangleF(rect.Left, rect.Top, rect.Width, height - margin / 2.0F);
                    rect2 = new RectangleF(rect.Left, rect.Top + height + margin / 2.0F, rect.Width, (rect.Height - height) - margin / 2.0F);
                }
            } while (rect1.Ratio() > MaxRatio || rect2.Ratio() > MaxRatio || rect1.Ratio() < MinRatio || rect2.Ratio() < MinRatio);
            if (attempts > 100) {
                return new[] { rect };
            }
            return new[] { rect1, rect2 };
        }
        /// <summary>
        ///     Go through Folders and NonRecurseFolders and prefix them with the base folder where they are found to exist.
        ///     If no base folders are specified, the folders are assumed to be full paths.
        ///     Also, any folder with a drive specification is assumed to be a full path.
        /// </summary>
        /// <returns>True if successful, false on any error.</returns>
        private static bool FixupFolders() {
            try {
                if (_settings.BaseFolders.Length > 0) {
                    int fixcount = 0;
                    int drivespec = 0;
                    int notfound = 0;
                    for (int i = 0; i < _settings.Folders.Length; ++i) {
                        string folder = _settings.Folders[i];
                        if (folder[1] == ':') {
                            ++drivespec;
                            continue; // this one's a full path, so leave it alone.
                        }
                        int save = fixcount;
                        foreach (string bfolder in _settings.BaseFolders) {
                            string tempfolder = Path.Combine(bfolder, folder);
                            if (Directory.Exists(tempfolder)) {
                                _settings.Folders[i] = tempfolder;
                                ++fixcount;
                                break;
                            }
                        }
                        if (save == fixcount) ++notfound;
                    }
                    for (int i = 0; i < _settings.NonRecurseFolders.Length; ++i) {
                        string folder = _settings.NonRecurseFolders[i];
                        if (folder[1] == ':') {
                            ++drivespec;
                            continue; // this one's a full path, so leave it alone.
                        }
                        int save = fixcount;
                        foreach (string bfolder in _settings.BaseFolders) {
                            string tempfolder = Path.Combine(bfolder, folder);
                            if (Directory.Exists(tempfolder)) {
                                _settings.NonRecurseFolders[i] = tempfolder;
                                ++fixcount;
                                break;
                            }
                        }
                        if (save == fixcount) ++notfound;
                    }
                    Log.Write($"Fixed folders up using base folders list: {fixcount} fixed, {drivespec} had drive spec, {notfound} were not found.");
                }
                else {
                    Log.Write("No base folders, so no folder fixup required.");
                }
                return true;
            }
            catch (Exception ex) {
                Log.Write("FixupFolders failed: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        ///     Returns an array of JSONImageInfo records representing images that meet the following criteria:
        ///     - has not already been shown on this run.
        ///     - larger in size than destrect (won't have to be stretched/pixelated to fit the rect).
        ///     - not in the exceptions list.
        ///     - the difference between its aspect ratio and that of destrect is within the limitation set by maxRatioDiff
        ///     OR
        ///     - destrect's aspect ratio lies between the image's min and max ratios.
        /// </summary>
        private static JSONImageInfo[] GetImagesToUse(IEnumerable<JSONImageInfo> images, RectangleF destRect, double maxRatioDiff, List<JSONImageInfo> exceptions = null) {
            var array = images.Where(i => {
                                         if (i.Shown) return false;
                                         if (i.Size.Width < destRect.Width || i.Size.Height < destRect.Height) return false;
                                         if (exceptions != null && exceptions.Any(e => ReferenceEquals(e, i))) return false;
                                         if (Math.Abs(i.Ratio - destRect.Ratio()) <= maxRatioDiff) return true;
                                         if (i.MinRatio <= destRect.Ratio() && i.MaxRatio >= destRect.Ratio()) return true;
                                         return false;
                                     }).ToArray();
            Array.Sort(array, (i1, i2) => i1.LastShownTicks < i2.LastShownTicks ? -1 : i1.LastShownTicks > i2.LastShownTicks ? 1 : 0);
            return array;
        }
        private static List<RectImages> GetRectList(Rectangle thisScreenRect) {
            // build rectlist, which is a list of randomized rectangles that cover this screen, where none have an aspect ratio < MinRatio or > MaxRatio.
            // Note: keep trying to build the rectlist until you get a set with all the aspect ratios matching an image in imagesToUse. 
            try {
                var rectlist = new List<RectImages>();
                bool done = false;
                int tries = 0;
                while (!done) {
                    rectlist.Clear();
                    // Split the screen once.
                    var rects = Divide(thisScreenRect, _settings.Margin);
                    // Now split each rect into two sub-rects (except that occasionally we leave one alone).
                    foreach (var r in rects) {
                        // in the rare case, we do not split a rect.
                        RectangleF[] rects2 = _rand.Next(7) == 0 ? new[] { r } : Divide(r, _settings.Margin);
                        rectlist.AddRange(rects2.Select(r2 => new RectImages { Rect = r2 }));
                    }
                    // further split a few based on SplitIterations setting.
                    for (int i = 0; i < _settings.SplitIterations; ++i) {
                        int r = _rand.Next(rectlist.Count);
                        var rect = rectlist[r];
                        rectlist.RemoveAt(r);
                        rects = Divide(rect.Rect, _settings.Margin);
                        rectlist.AddRange(rects.Select(r2 => new RectImages { Rect = r2 }));
                    }
                    done = true;
                    var templist = new List<JSONImageInfo>();
                    foreach (var rect in rectlist) {
                        var infos = GetImagesToUse(_imagesFiltered, rect.Rect, _settings.MaxRatioDiff, templist);
                        if (infos.Length == 0) {
                            done = false;
                            break;
                        }
                        rect.Images = infos;
                        if (infos.Length == 1) templist.Add(infos[0]);
                    }
                    ++tries;
                    Thread.Sleep(0);
                    if (tries > 5000) done = true;
                }
                if (tries > 5000) {
                    Log.Write("Was not able to get a good rect list after 5000 tries.");
                    return null;
                }
                Log.Write("Took " + tries + " tries to get a good rect list.");
                return rectlist;
            }
            catch (Exception ex) {
                Log.Write($"GetRectList failed with exception: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        ///     Hides this program's console window.
        /// </summary>
        /// <returns>True if successful, false on any error.</returns>
        private static bool HideMyConsole() {
            // Hide my console window.
            try {
                IntPtr p = Imported.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, Application.ExecutablePath);
                Imported.ShowWindow(p, 2);
            }
            catch {
                Log.Write("Failed to hide console window.");
                return false;
            }
            return true;
        }
        private static void ImageInfoMode() {
            string bgfile = Path.Combine(_dataPath, "CurrentBackground.json");
            if (!File.Exists(bgfile)) return;
            var currbg = new JSONArray(File.ReadAllText(bgfile));
            LoadSettings();
            var form = new MainForm(currbg.ToArray<JSONImageInfo>(), _dataPath, _settings);
            form.EditImage += Form_OnEditImage;
            form.PrepFocusRectsPanel += (sender, e) => {
                                                           if (!FixupFolders()) return;
                                                           if (!LoadNeverShowList()) return;
                                                           if (!LoadImageList()) return;
                                                           e.Images = _images;
                                                       };
            //form.OpenFocusRectEditor += (sender, e) => {
            //                                new Thread(() => {
            //                                               if (!LoadSettings()) return;
            //                                               if (!FixupFolders()) return;
            //                                               if (!LoadNeverShowList()) return;
            //                                               if (!LoadImageList()) return;
            //                                               var a = new Action(() => {
            //                                                                      var f = form.FocusRectEditor = new FocusRectEditor(_images, _dataPath);
            //                                                                      f.Closing += (o, ee) => form.FocusRectEditor = null;
            //                                                                      f.EditImage += Form_OnEditImage;
            //                                                                      f.Show();
            //                                                                  });
            //                                               form.Invoke(a);
            //                                           }).Start();
            //                            };
            form.ShowDialog();
        }
        private static bool LoadImageList() {
            _images = new List<JSONImageInfo>();
            try {
                // load up my last image list
                // omit any that are in Files.json, but are also in the nevershow list.  These will be ones that were shown, but when I saw it I put it in the nevershow list.
                string filesfname = Path.Combine(_dataPath, "Files.json");
                if (File.Exists(filesfname)) {
                    var filesjson = new JSONArray(File.ReadAllText(filesfname)).ToArray<JSONImageInfo>();
                    Log.Write($"{filesjson.Length} records read from Files.json");
                    foreach (var obj in filesjson) {
                        if (_neverShowList.Any(f => obj.Path.ToUpper().Contains(f))) continue;
                        if (_images.Any(i => i.Path == obj.Path)) continue;
                        _images.Add(obj);
                    }
                }
                else Log.Write("0 records read from Files.json");
                Log.Write($"After culling nevershows, {_images.Count} image files.");
                // Load up my list of images.  On the first run, this will take a while.  Subsequent runs should be fast because the files will already be in the list.
                foreach (string folder in _settings.Folders) {
                    if (!Directory.Exists(folder)) {
                        Log.Write("Folder doesn't exist: " + folder);
                        continue;
                    }
                    Console.WriteLine("Processing " + folder);
                    var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Where(f => _settings.ImageExtensions.Contains(Path.GetExtension(f)?.Trim('.').ToUpper())).ToList();
                    ProcessFiles(files);
                }
                foreach (string folder in _settings.NonRecurseFolders) {
                    if (!Directory.Exists(folder)) {
                        Log.Write("Folder doesn't exist: " + folder);
                        continue;
                    }
                    var files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly).Where(f => _settings.ImageExtensions.Contains(Path.GetExtension(f)?.Trim('.').ToUpper())).ToList();
                    Console.WriteLine("Processing " + folder + " (" + files.Count + ")");
                    ProcessFiles(files);
                }
                Log.Write("After searching folders, " + _images.Count + " images found. " + _images.Count(i => i.Validated) + " are validated.");
                var notvalida = _images.Where(i => !i.Validated).ToArray();
                if (notvalida.Length > 0) {
                    // files that are not validated are files that were in the Files.json list, but were found to not exist now.
                    // For each one, see if there's a file in the same folder with the same hashcode.
                    foreach (var info in notvalida) {
                        string dir = Path.GetDirectoryName(info.Path);
                        var samefolder = _images.Where(i => Path.GetDirectoryName(i.Path) == dir && i.Path != info.Path).ToArray();
                        if (samefolder.Length == 0) continue;
                        var samefile = samefolder.FirstOrDefault(i => i.HashCode == info.HashCode);
                        if (samefile != null && samefile.Validated) {
                            // Sanity test: see if there's more than one image in the full list that has this same hash code.
                            int count = _images.Count(i => i.HashCode == info.HashCode);
                            if (count > 2) {
                                Log.Write("Found duplicate hashcodes:");
                                foreach (var zz in _images.Where(i => i.HashCode == info.HashCode)) {
                                    Log.Write(info.Path);
                                }
                            }
                            // I found an identical image that matches the non-validated one.  Remove the non-validated one and replace it with the validated one in FocusRects.
                            _images.Remove(info);
                            string frpath = Path.Combine(dir, "FocusRects.json");
                            if (File.Exists(frpath)) {
                                var json = new JSONObject(File.ReadAllText(frpath));
                                var obj = json.optJSONObject(Path.GetFileName(info.Path));
                                if (obj != null) {
                                    json.remove(Path.GetFileName(info.Path));
                                    json.put(Path.GetFileName(samefile.Path), obj);
                                    File.WriteAllText(frpath, json.ToString(true));
                                    samefile.put("FocusRect", obj);
                                }
                            }
                        }
                    }
                }
                // Remove any images that are in folders I'm no longer required to include.
                // Also remove images where the folder exists, but the image doesn't.
                // The important thing is, I don't want to remove entries where the folder doesn't exist, because that might just be a removable drive or something, and I want to keep those.
                _images.RemoveAll(i => {
                                      if (!_settings.Folders.Any(f => i.Path.StartsWith(f)) && !_settings.NonRecurseFolders.Any(f => i.Path.StartsWith(f))) return true;
                                      return Directory.Exists(Path.GetDirectoryName(i.Path)) && !File.Exists(i.Path);
                                  });
                Log.Write("After culling missing files and folders I don't care about, " + _images.Count + " images found.");
                // Write the _images list back to disk.
                WriteFilesJson();
                // Aftere writing Files.json, cull out records that:
                // 1. Are not validated -- meaning that the file actually exists (prevents using images from removable drives that aren't present right now, but we want to remember if the drive's available next time).
                // 2. Have been shown recently (as defined by MinShowIntervalDays in Settings.json).
                var now = DateTime.Now;
                int notvalid = _images.Count(i => !i.Validated);
                int shownrecently = _images.Count(i => (now - i.LastShown).TotalDays <= _settings.MinShowIntervalDays);
                _imagesFiltered = _images.Where(i => i.Validated && (now - i.LastShown).TotalDays > _settings.MinShowIntervalDays).ToList();
                Log.Write($"{notvalid} failed to validate, {shownrecently} were shown recently.  {_imagesFiltered.Count} usable images (based on settings).");
                // Sort the images by last used timestamp. This means when I go looking for images, I'll find the least recently used one that fits.
                _imagesFiltered.Sort((i1, i2) => DateTime.Compare(i1.LastShown, i2.LastShown));
                return true;
            }
            catch (Exception ex) {
                Log.Write("Failed to load images: " + ex.Message);
                _images = new List<JSONImageInfo>();
                return false;
            }
        }
        private static bool LoadNeverShowList() {
            try {
                // Load never show list.  Force to uppercase.
                string nevershowfname = Path.Combine(_dataPath, "NeverShow.json");
                _neverShowList = File.Exists(nevershowfname) ? new JSONArray(File.ReadAllText(nevershowfname)).ToArray() : Array.Empty<string>();
                if (_neverShowList.Length > 0) _neverShowList = _neverShowList.Select(s => s.ToUpper()).ToArray();
                Log.Write(_neverShowList.Length + " nevershow files read from NeverShow.json.");
                return true;
            }
            catch (Exception ex) {
                Log.Write("Failed to load NeverShow.json: " + ex.Message);
                _neverShowList = null;
                return false;
            }
        }
        /// <summary>
        ///     Loads Settings.json.
        ///     Returns false on any error, or if there are no folders listed, true otherwise.
        /// </summary>
        private static bool LoadSettings() {
            _settings = null;
            try {
                _settings = new JSONSettings(File.ReadAllText(Path.Combine(_dataPath, "Settings.json")));
                // if there are no folders listed, there's nothing to do, so return null.  Program will terminate.
                if (_settings.Folders.Length == 0 && _settings.NonRecurseFolders.Length == 0) {
                    Log.Write("No folders listed in settings.");
                    return false;
                }
                Log.Write("Loaded settings.");
                return true;
            }
            catch (Exception ex) {
                Log.Write("Error loading Settings.json: " + ex.Message);
                return false;
            }
        }
        [STAThread] private static void Main(string[] args) {
             ParseCommandLine(args);
            Log.Init(_dataPath);
            if (_cleanup) {
                Cleanup();
                return;
            }
            if (_imageInfoMode) {
                ImageInfoMode();
                return;
            }
            JSONImageInfo[] currBGarray = null;
            string bgfile = Path.Combine(_dataPath, "CurrentBackground.json");
            if (File.Exists(bgfile)) {
                var currbg = new JSONArray(File.ReadAllText(bgfile));
                currBGarray = currbg.ToArray<JSONImageInfo>();
                if (_minRunMins > 0) {
                    var lastrun = File.GetLastWriteTime(bgfile);
                    if ((DateTime.Now - lastrun).TotalMinutes < _minRunMins) {
                        Log.Write("Too soon since last run. " + DateTime.Now);
                        return;
                    }
                }
            }
            Log.Write("Starting");
            if (!HideMyConsole()) return;
            if (!LoadSettings()) return;
            if (!FixupFolders()) return;
            if (!LoadNeverShowList()) return;
            if (!LoadImageList()) return;
            if (_editFocusRects) {
                var form = new FocusRectEditor(_images, _dataPath);
                form.EditImage += Form_OnEditImage;
                form.ShowDialog();
                return;
            }
            // screens is the array of screens.
            var screens = Screen.AllScreens.Select(s => new ScreenInfo(s)).ToArray();
            var taskbar = new Taskbar();
            Log.Write(screens.Length + " screens.");
            JSONImageInfo.MaxRatioDiff = _settings.MaxRatioDiff;
            MinRatio = _imagesFiltered.Select(i => i.MinRatio).Min();
            MaxRatio = _imagesFiltered.Select(i => i.MaxRatio).Max();
            // Go though the screens and generate wallpaper images
            var imagelist = new List<string>();
            Rectangle allScreensRect = Rectangle.Empty;
            foreach (var screen in screens) {
                Rectangle thisScreenRect = screen.Bounds;
                if (!taskbar.AutoHide && screen.Bounds.IntersectsWith(taskbar.Bounds)) {
                    switch (taskbar.Position) {
                        case Taskbar.TaskbarPosition.Left:
                            thisScreenRect = new Rectangle(screen.Bounds.Left + taskbar.Size.Width, screen.Bounds.Top, screen.Size.Width - taskbar.Size.Width, screen.Size.Height);
                            break;
                        case Taskbar.TaskbarPosition.Top:
                            thisScreenRect = new Rectangle(screen.Bounds.Left, screen.Bounds.Top + taskbar.Size.Height, screen.Size.Width, screen.Size.Height - taskbar.Size.Height);
                            break;
                        case Taskbar.TaskbarPosition.Right:
                            thisScreenRect = new Rectangle(screen.Bounds.Left, screen.Bounds.Top, screen.Size.Width - taskbar.Size.Width, screen.Size.Height);
                            break;
                        case Taskbar.TaskbarPosition.Bottom:
                            thisScreenRect = new Rectangle(screen.Bounds.Left, screen.Bounds.Top, screen.Size.Width, screen.Size.Height - taskbar.Size.Height);
                            break;
                    }
                }
                // build up allScreensRect, which is a complete rectangle that covers all screens.
                allScreensRect = allScreensRect == Rectangle.Empty ? screen.Bounds : Rectangle.Union(allScreensRect, screen.Bounds);
                List<RectImages> rectlist;
                if (!_useLastRectList) {
                    rectlist = GetRectList(thisScreenRect);
                    if (rectlist == null) return;
                }
                else {
                    rectlist = currBGarray.Where(v => v.DestRect.IntersectsWith(thisScreenRect))
                                          .Select(v => new RectImages {
                                                                          Rect = v.DestRect,
                                                                          Images = GetImagesToUse(_imagesFiltered, v.DestRect, _settings.MaxRatioDiff)
                                                                      }).ToList();
                }
                foreach (var rect in rectlist) {
                    Log.Write($"({rect.Rect.Width}, {rect.Rect.Height}): {rect.Images.Length} potential images");
                }
                //// As a test, fill the screen image with white blocks where the images will be.
                //foreach (var rect in rectlist) {
                //    using (var gfx = Graphics.FromImage(screen.Bitmap)) {
                //        gfx.FillRectangle(Brushes.White, rect.OffsetBy(-1 * screen.Bounds.X, -1 * screen.Bounds.Y));
                //    }
                //}
                //screen.Bitmap.Save("screen" + screenNum + ".png");
                //screen.InitBitmap();
                // fill in the rectangles with images that best fit them.
                foreach (var rect in rectlist) {
                    //double ratio = rect.Ratio();
                    //rect.Images = GetImagesToUse(_imagesFiltered, rect.Rect, _settings.MaxRatioDiff);
                    // If no image is found, just leave the rectangle black.  This alerts the user that he needs to:
                    // 1. Decrease MinShowIntervalDays
                    // 2. Increase MaxRatioDiff
                    // 3. Decrease MinSourceImageSize
                    // 4. Add more folders and/or more images.
                    var images = rect.Images.Where(i => !i.Shown).ToArray();
                    if (images.Length == 0) {
                        Log.Write("ERROR: No images to show in rectangle, so it will be empty.");
                        continue;
                    }
                    var image = images[_rand.Next(images.Length)];
                    //JSONImageInfo image = rect.Images.FirstOrDefault(i => !i.Shown);
                    image.Shown = true;
                    if (image.RecentShows.Any(dt => (DateTime.Now - dt).TotalDays < _settings.MinShowIntervalDays)) {
                        Log.Write("Image shown too frequently: " + image.Path);
                    }
                    image.LastShown = DateTime.Now;
                    imagelist.Add(image.Path);
                    image.ShowCount += 1;
                    var goalsize = new SizeF(rect.Rect.Width, image.Size.Height * rect.Rect.Width / image.Size.Width);
                    if (goalsize.Height < rect.Rect.Height) goalsize = new SizeF(image.Size.Width * rect.Rect.Height / image.Size.Height, rect.Rect.Height);
                    var scaleRatio = new SizeF(goalsize.Width / image.Size.Width, goalsize.Height / image.Size.Height);
                    var scaledFocusRect = new RectangleF(image.FocusRect.X * scaleRatio.Width, image.FocusRect.Y * scaleRatio.Height, image.FocusRect.Width * scaleRatio.Width, image.FocusRect.Height * scaleRatio.Height);
                    using (var origbmp = Image.FromFile(image.Path)) {
                        image.Bitmap = new Bitmap(origbmp, new Size((int)Math.Round(goalsize.Width), (int)Math.Round(goalsize.Height)));
                    }
                    if (Math.Abs(goalsize.Width - rect.Rect.Width) < 0.01) {
                        int offset = (int)Math.Round(scaledFocusRect.Y - (rect.Rect.Height - scaledFocusRect.Height) / 2F);
                        if (offset < 0) offset = 0;
                        image.RectShown = new Rectangle(0, offset, (int)Math.Round(rect.Rect.Width), (int)Math.Round(rect.Rect.Height));
                    }
                    else {
                        int offset = (int)Math.Round(scaledFocusRect.X - (rect.Rect.Width - scaledFocusRect.Width) / 2F);
                        if (offset < 0) offset = 0;
                        image.RectShown = new Rectangle(offset, 0, (int)Math.Round(rect.Rect.Width), (int)Math.Round(rect.Rect.Height));
                    }
                    if (image.RectShown.Right > image.Bitmap.Width) {
                        int offset = image.Bitmap.Width - image.RectShown.Right;
                        image.RectShown = new Rectangle(image.RectShown.X + offset, image.RectShown.Y, image.RectShown.Width, image.RectShown.Height);
                    }
                    if (image.RectShown.Bottom > image.Bitmap.Height) {
                        int offset = image.Bitmap.Height - image.RectShown.Bottom;
                        image.RectShown = new Rectangle(image.RectShown.X, image.RectShown.Y + offset, image.RectShown.Width, image.RectShown.Height);
                    }
                    image.DestRect = new Rectangle((int)Math.Round(rect.Rect.Left), (int)Math.Round(rect.Rect.Top), (int)Math.Round(rect.Rect.Width), (int)Math.Round(rect.Rect.Height));
                    image.DestRect.Offset(screen.Bounds.Left * -1, screen.Bounds.Top * -1);
                    using (var gfx = Graphics.FromImage(screen.Bitmap)) {
                        Rectangle destrect = image.DestRect.OffsetBy(-1 * screen.Bounds.X, -1 * screen.Bounds.Y);
                        gfx.DrawImage(image.Bitmap, destrect, image.RectShown, GraphicsUnit.Pixel);
                        if (_settings.ShowFilenames) {
                            var fileinfo = "";
                            if (_settings.ShowFolders) {
                                var pathparts = image.Path.Split('\\');
                                if (pathparts.Length > 2) { // only do this if there's at least one folder in the path.
                                    if (_settings.ShowFoldersAfter == null || _settings.ShowFoldersAfter.Length == 0) fileinfo = pathparts[pathparts.Length - 2] + "\n"; // get the last folder name before the filename.
                                    else {
                                        var showFoldersAfter = _settings.ShowFoldersAfter.Select(s => s.ToUpper()).ToArray();
                                        int indexOfFirstFolderToShow = pathparts.Length - 2;
                                        for (int x = pathparts.Length - 2; x >= 0; --x) {
                                            string pathpart = pathparts[x];
                                            if (showFoldersAfter.Any(f => pathpart.ToUpper().Contains(f))) {
                                                indexOfFirstFolderToShow = x + 1;
                                                break;
                                            }
                                        }
                                        for (int x = indexOfFirstFolderToShow; x < pathparts.Length - 1; ++x) {
                                            fileinfo += pathparts[x] + "\n";
                                        }
                                    }
                                }
                            }
                            fileinfo += Path.GetFileNameWithoutExtension(image.Path);
                            var font = new Font("Ariel", 8, FontStyle.Bold);
                            var size = gfx.MeasureString(fileinfo, font, new SizeF(destrect.Width, destrect.Height));
                            gfx.DrawString(fileinfo, font, Brushes.Black, new RectangleF(destrect.Right - size.Width, destrect.Top, size.Width, size.Height));
                            gfx.DrawString(fileinfo, font, Brushes.White, new RectangleF(destrect.Right - size.Width + 1, destrect.Top + 1, size.Width, size.Height));
                        }
                    }
                    //screen.Bitmap.Save("screen" + screenNum + ".png");
                }
                //++screenNum;
            }
            string pngfile = Path.Combine(_dataPath, "background.png");
            using (var bmp = new Bitmap(allScreensRect.Width, allScreensRect.Height)) {
                using (var gfx = Graphics.FromImage(bmp)) {
                    gfx.FillRectangle(Brushes.Black, allScreensRect.OffsetBy(-1 * allScreensRect.Left, -1 * allScreensRect.Top));
                    foreach (var screen in screens) {
                        gfx.DrawImage(screen.Bitmap, screen.Bounds.OffsetBy(-1 * allScreensRect.Left, -1 * allScreensRect.Top), 0, 0, screen.Size.Width, screen.Size.Height, GraphicsUnit.Pixel);
                    }
                }
                bmp.Save(pngfile);
            }
            SetBackgroud(pngfile);
            WriteFilesJson();
            var imagesused = new JSONArray();
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now + Environment.NewLine);
            foreach (string file in imagelist) {
                sb.Append(file + Environment.NewLine);
                var imgused = _images.First(i => i.Path == file);
                imagesused.put(imgused);
            }
            Log.Write(sb.ToString());
            File.WriteAllText(bgfile, imagesused.ToString(true, 3));
        }
        /// <summary>
        ///     Looks for supported command line args.
        ///     - imageinfo sets _imageInfoMode flag.
        ///     - datapath=[PATH] sets _dataPath.
        /// </summary>
        private static void ParseCommandLine(string[] args) {
            foreach (var arg in args) {
                string str = arg.Remove(0, 1).ToLower();
                if (str.StartsWith("imageinfo")) _imageInfoMode = true;
                else if (str.StartsWith("datapath")) {
                    var strs = arg.Split('=').Select(s => s.Trim()).ToArray();
                    if (strs.Length < 2) continue;
                    var path = strs[1];
                    try {
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        if (!Directory.Exists(path)) throw new Exception();
                    }
                    catch {
                        Log.Write("Unable to access directory: " + path);
                        continue;
                    }
                    _dataPath = path;
                }
                else if (str.StartsWith("uselastrectlist")) _useLastRectList = true;
                else if (str.StartsWith("cleanup")) _cleanup = true;
                else if (str.StartsWith("editfocusrects")) _editFocusRects = true;
                else if (str.StartsWith("minrunmins")) {
                    var strs = arg.Split('=').Select(s => s.Trim()).ToArray();
                    if (strs.Length < 2) continue;
                    _minRunMins = int.Parse(strs[1]);
                }
            }
        }
        /// <summary>
        ///     Takes a list of file paths, and builds up _images, which is a list of ImageInfoPanel records.  The resulting
        ///     _images
        ///     list will only include images for which:
        ///     1. The file exists.
        ///     2. The file is not already in _images (or if it is, it's last write time will be updated).
        ///     3. The file has a valid extention (one of the extensions in the exts array).
        ///     4. The file is not in the nevershow list.
        ///     5. It's height and width are greater than or equal to minsize.
        /// </summary>
        private static void ProcessFiles(List<string> files) {
            int count = 0;
            JSONObject focusRects = new JSONObject();
            string focusRectsPath = null;
            while (files.Count > 0) {
                int f = _rand.Next(files.Count);
                string file = files[f];
                files.RemoveAt(f);
                string fn = Path.GetFileName(file);
                if (fn == null) continue; // should never happen? But anyway, if it does this is a bogus entry we should skip.
                // Skip it if it's not a valid extension.
                string ext = Path.GetExtension(file);
                if (!_settings.ImageExtensions.Contains(ext.Trim('.').ToUpper())) continue;
                ++count;
                Console.WriteLine(count + "/" + files.Count);
                // Skip it if it's in nevershow.
                if (_neverShowList.Any(ns => file.ToUpper().Contains(ns))) continue;
                // get any focusrects for this folder.
                if (Path.GetDirectoryName(file) != focusRectsPath) {
                    focusRectsPath = Path.GetDirectoryName(file) ?? "";
                    string frname = Path.Combine(focusRectsPath, "FocusRects.json");
                    focusRects = File.Exists(frname) ? new JSONObject(File.ReadAllText(frname)) : new JSONObject();
                }
                // Skip it if it's already in the list and has the same timestamp.
                var lwt = File.GetLastWriteTime(file);
                var inforecord = _images.FirstOrDefault(i => i.Path == file);
                if (inforecord != null) {
                    if (inforecord.has("Width") && inforecord.has("Height")) { // because some files were erroneaously written with no size info due to a bug.
                        if (focusRects.has(Path.GetFileName(inforecord.Path))) inforecord.put("FocusRect", focusRects.getJSONObject(Path.GetFileName(inforecord.Path)));
                        else inforecord.remove("FocusRect");
                        if (inforecord.LastWriteTicks == lwt.Ticks) {
                            if (!inforecord.has("Hash")) {
                                inforecord.remove("HashCode"); // where I used to store this.  remove if it's there.
                                inforecord.HashCode = GetHashCode(inforecord.Path);
                            }
                            inforecord.Validated = true;
                            continue; // already got it.
                        }
                    }
                    //else {
                    //    int i = 10;
                    //}
                    _images.RemoveAll(i => i.Path == file); // remove it 'cause I'm about to regenerate it.
                }
                // If I get here, it means this file is one I don't already have in my list or was removed because it's been updated since I last saw it.
                Size sz;
                ulong hashcode;
                try {
                    using (var stream = File.OpenRead(file)) {
                        using (var img = Image.FromStream(stream, false, false)) {
                            sz = img.Size;
                            hashcode = GetHashCode(img);
                        }
                    }
                    //using (var img = Image.FromFile(file)) {
                    //    sz = img.Size;
                    //}
                }
                catch {
                    sz = new Size(0, 0);
                    hashcode = 0;
                }
                if (sz.Width < _settings.MinSourceImageSize.Width || sz.Height < _settings.MinSourceImageSize.Height) continue;
                inforecord = new JSONImageInfo { Path = file, Validated = true, LastWrite = lwt, Size = sz, HashCode = hashcode };
                if (focusRects.has(Path.GetFileName(file))) inforecord.put("FocusRect", focusRects.getJSONObject(Path.GetFileName(file)));
                _images.Add(inforecord);
            }
            Console.WriteLine("\nTotal: " + _images.Count);
        }
        /// <summary>
        ///     set the parameter of system
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam"></param>
        /// <param name="fuWinIni"></param>
        /// <example></example>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern int SystemParametersInfo(UAction uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private static void WriteFilesJson() {
            var jsonarray = new JSONArray();
            foreach (var image in _images) jsonarray.put(image);
            File.WriteAllText(Path.Combine(_dataPath, "Files.json"), jsonarray.ToString(true, 0));
        }
        private static bool _cleanup;
        private static string _dataPath = "C:\\ProgramData\\BackgroundSwitcher";
        private static bool _editFocusRects;
        private static bool _imageInfoMode;
        private static List<JSONImageInfo> _images = new List<JSONImageInfo>();
        private static List<JSONImageInfo> _imagesFiltered = new List<JSONImageInfo>();
        private static int _minRunMins; // use /minRunMins=n to set min run frequency.  Program will do nothing if run too frequently.
        private static string[] _neverShowList = Array.Empty<string>();
        private static readonly Random _rand = new Random();
        private static JSONSettings _settings;
        private static bool _useLastRectList;
        private static double MaxRatio = 4; // max ratio = height 4 times greater than width.
        private static double MinRatio = 1 / 4D; // min ratio = width 4 times greater than height.
        private enum UAction {
            /// <summary>
            ///     set the desktop background image
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,
        }
        // ReSharper disable UnusedMethodReturnValue.Local
        private static ulong GetHashCode(string path) {
            try {
                using (var stream = File.OpenRead(path)) {
                    using (var img = Image.FromStream(stream, false, false)) {
                        return GetHashCode(img);
                    }
                }
                //using (var img = Image.FromFile(file)) {
                //    sz = img.Size;
                //}
            }
            catch {
                return 0;
            }
        }
        private static ulong GetHashCode(Image img) {
            using (var bmp = new Bitmap(10, 10)) {
                using (var gfx = Graphics.FromImage(bmp)) {
                    gfx.DrawImage(img, new Rectangle(0, 0, 10, 10));
                }
                ulong hash = 0;
                var points = new Point[8] {
                                              new Point(1, 1), new Point(8, 1), new Point(1, 8), new Point(8, 8),
                                              new Point(3, 3), new Point(6, 3), new Point(3, 6), new Point(6, 6)
                                          };
                foreach (var p in points) {
                    var c = bmp.GetPixel(p.X, p.Y);
                    ulong i = Convert.ToUInt64(((c.R + c.G + c.B) / 3) & 0x000000FF);
                    hash = (hash << 8) + i;
                }
                return hash;
            }
        }
        /// <summary>
        ///     set the desktop background image
        /// </summary>
        /// <param name="fileName">the path of image</param>
        /// <returns></returns>
        private static int SetBackgroud(string fileName) {
            int result = 0;
            if (File.Exists(fileName)) {
                StringBuilder s = new StringBuilder(fileName);
                result = SystemParametersInfo(UAction.SPI_SETDESKWALLPAPER, 0, s, 0x2);
            }
            return result;
        }
        // ReSharper restore UnusedMethodReturnValue.Local
    }
    public class RectImages {
        public JSONImageInfo[] Images;
        public RectangleF Rect;
    }
}