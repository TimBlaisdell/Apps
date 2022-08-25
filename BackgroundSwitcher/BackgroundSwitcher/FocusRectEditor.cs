using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JSON;

namespace BackgroundSwitcher {
    public partial class FocusRectEditor : Form {
        public FocusRectEditor() {
            InitializeComponent();
        }
        public FocusRectEditor(List<JSONImageInfo> images, string datapath) : this() {
            _datapath = datapath;
            _images = images;
            NewWorkingIndex();
            LoadImage();
            lblRemaining.Text = "Remaining: " + (_filteredImages ?? _images).Count(i => !i.has("FocusRect"));
        }
        public event EventHandler<string> EditImage;
        private void btnNeverShow_Click(object sender, EventArgs e) {
            try {
                string nevershowfname = Path.Combine(_datapath, "NeverShow.json");
                var nevershow = File.Exists(nevershowfname) ? new JSONArray(File.ReadAllText(nevershowfname)) : new JSONArray();
                string fn = Path.GetFileNameWithoutExtension(_images[_workingIndex].Path);
                if (!nevershow.ToArray().Contains(fn)) {
                    nevershow.put(Path.GetFileNameWithoutExtension(_images[_workingIndex].Path));
                    File.WriteAllText(nevershowfname, nevershow.ToString(true, 4));
                }
                MessageBox.Show(this, "Image added to never show list.", "Nevershow added");
                _filteredImages.RemoveAt(_workingIndex);
                NewWorkingIndex();
                LoadImage();
            }
            catch (Exception ex) {
                MessageBox.Show(this, "Error: " + ex.Message, "Error");
            }
        }
        private void btnSet_Click(object sender, EventArgs e) {
            var rect = panelCanvas.SelectedRect;
            SetFocusRect(rect);
            btnSkip_Click(null, null);
        }
        private void btnSkip_Click(object sender, EventArgs e) {
            int oldindex = _workingIndex;
            NewWorkingIndex();
            LoadImage();
            if (_workingIndex == oldindex) {
                MessageBox.Show("No next image found.");
            }
        }
        private void btnUseWholeImage_Click(object sender, EventArgs e) {
            SetFocusRect(new RectangleF(0, 0, 0, 0));
            btnSkip_Click(null, null);
        }
        //private void lblRemaining_TextChanged(object sender, EventArgs e) {
        //    lblRemaining.Left = numX.Left - lblRemaining.Width - 10;
        //}
        private void menuAutoRect_Click(object sender, EventArgs e) {
            int margin = Math.Min(_curImageScaled.Width, _curImageScaled.Height) > 300 ? 100 : 10;
            panelCanvas.SelectedRect = new Rectangle(margin, margin, _curImageScaled.Width - margin * 2, _curImageScaled.Height - margin * 2);
        }
        private void menuEditThisImage_Click(object sender, EventArgs e) {
            try {
                EditImage?.Invoke(this, _images[_workingIndex].Path);
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void menuGoToFolder_Click(object sender, EventArgs e) {
            try {
                Process.Start("explorer.exe", "/select," + _images[_workingIndex].Path);
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void menuReloadImage_Click(object sender, EventArgs e) {
            LoadImage();
        }
        private void menuSelectFolder_Click(object sender, EventArgs e) {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                menuStayInFolder.Checked = true;
                string path = folderBrowserDialog.SelectedPath;
                _filteredImages = _images.Where(i => Path.GetDirectoryName(i.Path) == path).ToList();
                if (_filteredImages.Count == 0) {
                    MessageBox.Show("There are no images in the selected folder.");
                    _filteredImages = null;
                    menuStayInFolder.Checked = false;
                }
                else {
                    NewWorkingIndex();
                    LoadImage();
                }
            }
        }
        private void menuStayInFolder_Click(object sender, EventArgs e) {
            menuStayInFolder.Checked = !menuStayInFolder.Checked;
            if (menuStayInFolder.Checked) {
                string path = Path.GetDirectoryName(_images[_workingIndex].Path);
                _filteredImages = _images.Where(i => Path.GetDirectoryName(i.Path) == path).ToList();
            }
            else _filteredImages = null;
        }
        private void num_ValueChanged(object sender, EventArgs e) {
            var oldrect = new RectangleF(panelCanvas.SelectedRect.X * (1 / _ratio),
                                         panelCanvas.SelectedRect.Y * (1 / _ratio),
                                         panelCanvas.SelectedRect.Width * (1 / _ratio),
                                         panelCanvas.SelectedRect.Height * (1 / _ratio));
            if (sender == numX) {
                var newx = (float)numX.Value;
                numWidth.Value = (int)numWidth.Value - (int)Math.Round(newx - oldrect.X);
            }
            else if (sender == numY) {
                var newy = (float)numY.Value;
                numHeight.Value = (int)numHeight.Value - (int)Math.Round(newy - oldrect.Y);
            }
            else {
                panelCanvas.SelectedRect = new Rectangle((int)Math.Round((int)numX.Value * _ratio),
                                                         (int)Math.Round((int)numY.Value * _ratio),
                                                         (int)Math.Round((int)numWidth.Value * _ratio),
                                                         (int)Math.Round((int)numHeight.Value * _ratio));
            }
        }
        private void panelCanvas_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.S:
                    btnSet_Click(btnSet, EventArgs.Empty);
                    break;
                case Keys.X:
                    btnSkip_Click(btnSkip, EventArgs.Empty);
                    break;
                case Keys.F: {
                    if (e.Control) {
                        if (MessageBox.Show("Set all remaining images to use full image?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                            new Thread(() => {
                                           var images = _filteredImages ?? _images;
                                           for (int i = 0; i < images.Count; ++i) {
                                               _workingIndex = images == _images ? i : _images.FindIndex(img => img == images[i]);
                                               if (_images[_workingIndex].has("FocusRect")) continue;
                                               SetFocusRect(new RectangleF(0, 0, 0, 0));
                                               Thread.Sleep(10);
                                           }
                                       }).Start();
                        }
                    }
                    btnUseWholeImage_Click(btnUseWholeImage, EventArgs.Empty);
                    break;
                }
                case Keys.A:
                    menuAutoRect_Click(menuAutoRect, EventArgs.Empty);
                    break;
                case Keys.N:
                    btnNeverShow_Click(btnNeverShow, EventArgs.Empty);
                    break;
                case Keys.E:
                    EditImage?.Invoke(this, _images[_workingIndex].Path);
                    break;
            }
        }
        private void panelCanvas_MouseLeave(object sender, EventArgs e) {
            lblCoords.Visible = false;
        }
        private void panelCanvas_MouseMove(object sender, MouseEventArgs e) {
            lblCoords.Visible = true;
            lblCoords.Text = $"{(int)Math.Round(e.X * (1 / _ratio))}, {(int)Math.Round(e.Y * (1 / _ratio))}";
        }
        private void panelCanvas_SelectedRectChanged(object sender, EventArgs e) {
            numX.Visible = numY.Visible = numWidth.Visible = numHeight.Visible = panelCanvas.SelectedRect.Width > 0 && panelCanvas.SelectedRect.Height > 0;
            if (!numX.Visible) return;
            var rect = new RectangleF(panelCanvas.SelectedRect.X * (1 / _ratio), panelCanvas.SelectedRect.Y * (1 / _ratio), panelCanvas.SelectedRect.Width * (1 / _ratio), panelCanvas.SelectedRect.Height * (1 / _ratio));
            numX.Value = (int)Math.Round(Math.Max(0, rect.X));
            numY.Value = (int)Math.Round(Math.Max(0, rect.Y));
            numWidth.Value = (int)Math.Round(rect.Width);
            numHeight.Value = (int)Math.Round(rect.Height);
        }
        public void SetImage(string path) {
            int index = _images.FindIndex(i => i.Path == path);
            if (index >= 0 && index != _workingIndex) {
                _workingIndex = index;
                LoadImage();
                string fname = Path.Combine(Path.GetDirectoryName(path), "FocusRects.json");
                if (File.Exists(fname)) {
                    var obj = new JSONObject(File.ReadAllText(fname));
                    obj = obj.optJSONObject(Path.GetFileName(path));
                    if (obj != null && obj.optInt("Width") > 0 && obj.optInt("Height") > 0) {
                        panelCanvas.SelectedRect = new Rectangle((int)Math.Round(obj.optInt("X") * _ratio),
                                                                 (int)Math.Round(obj.optInt("Y") * _ratio),
                                                                 (int)Math.Round(obj.optInt("Width") * _ratio),
                                                                 (int)Math.Round(obj.optInt("Height") * _ratio));
                    }
                }
            }
        }
        protected override void OnResizeEnd(EventArgs e) {
            base.OnResizeEnd(e);
            if (panelCanvas.BackgroundImage == null) return;
            var oldsize = panelCanvas.BackgroundImage.Size;
            var oldrect = panelCanvas.SelectedRect;
            LoadImage();
            var newsize = panelCanvas.BackgroundImage.Size;
            if (!oldrect.IsEmpty) {
                panelCanvas.SelectedRect = new Rectangle((int)Math.Round(oldrect.X * (newsize.Width / (float)oldsize.Width)),
                                                         (int)Math.Round(oldrect.Y * (newsize.Height / (float)oldsize.Height)),
                                                         (int)Math.Round(oldrect.Width * (newsize.Width / (float)oldsize.Width)),
                                                         (int)Math.Round(oldrect.Height * (newsize.Height / (float)oldsize.Height)));
            }
        }
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            if (WindowState == FormWindowState.Maximized) OnResizeEnd(e);
        }
        private void LoadImage() {
            lblImagePath.Text = _images[_workingIndex].Path;
            using (var bmp = new Bitmap(_images[_workingIndex].Path)) {
                _curImageOrig = new Bitmap(bmp, bmp.Size);
            }
            lblImageSize.Text = $"{_curImageOrig.Width}, {_curImageOrig.Height}";
            lblImageSize.Left = Width - lblImageSize.Width - 20;
            _curImageOrigSize = new SizeF(_curImageOrig.Width, _curImageOrig.Height);
            _ratio = panelCanvas.Width / _curImageOrigSize.Width;
            var scaledsize = new SizeF(_curImageOrigSize.Width * _ratio, _curImageOrigSize.Height * _ratio);
            if (scaledsize.Height > panelCanvas.Height) {
                _ratio = panelCanvas.Height / _curImageOrigSize.Height;
                scaledsize = new SizeF(_curImageOrigSize.Width * _ratio, _curImageOrigSize.Height * _ratio);
            }
            _curImageScaled?.Dispose();
            panelCanvas.BackgroundImage = _curImageScaled = new Bitmap((int)Math.Round(scaledsize.Width), (int)Math.Round(scaledsize.Height));
            using (var gfx = Graphics.FromImage(_curImageScaled)) {
                gfx.DrawImage(_curImageOrig, new RectangleF(0, 0, scaledsize.Width, scaledsize.Height));
            }
        }
        private void NewWorkingIndex() {
            int tries = 0;
            if (_filteredImages == null || _filteredImages.Count == 0) {
                do {
                    _workingIndex = _rand.Next(_images.Count);
                    ++tries;
                } while (tries < 100 && (_images[_workingIndex].has("FocusRect") || !File.Exists(_images[_workingIndex].Path)));
            }
            else {
                int index;
                do {
                    index = _rand.Next(_filteredImages.Count);
                    ++tries;
                } while (tries < 100 && (_filteredImages[index].has("FocusRect") || !File.Exists(_filteredImages[index].Path)));
                if (tries == 100) {
                    index = _filteredImages.FindIndex(i => !i.has("FocusRect") && File.Exists(i.Path));
                    if (index < 0) {
                        MessageBox.Show("There are no more images in the current folder without FocusRect specifications.");
                        _filteredImages = null;
                        menuStayInFolder.Checked = false;
                        NewWorkingIndex();
                        return;
                    }
                }
                _workingIndex = _images.FindIndex(i => i == _filteredImages[index]);
            }
            UpdateRemaining();
        }
        private void SetFocusRect(RectangleF rect) {
            rect = new RectangleF(rect.X * (1 / _ratio), rect.Y * (1 / _ratio), rect.Width * (1 / _ratio), rect.Height * (1 / _ratio));
            var info = _images[_workingIndex];
            info.put("FocusRect",
                     new JSONObject().put("X", (int)Math.Round(rect.X))
                                     .put("Y", (int)Math.Round(rect.Y))
                                     .put("Width", (int)Math.Round(rect.Width))
                                     .put("Height", (int)Math.Round(rect.Height)));
            var fname = Path.Combine(Path.GetDirectoryName(info.Path), "FocusRects.json");
            JSONObject obj = File.Exists(fname) ? new JSONObject(File.ReadAllText(fname)) : new JSONObject();
            obj.put(Path.GetFileName(info.Path), info.getJSONObject("FocusRect"));
            File.WriteAllText(fname, obj.ToString(true));
            UpdateRemaining();
        }
        private void UpdateRemaining() {
            var mi = new MethodInvoker(() => lblRemaining.Text = "Remaining: " + (_filteredImages ?? _images).Count(i => !i.has("FocusRect")));
            if (lblRemaining.InvokeRequired) lblRemaining.Invoke(mi);
            else mi.Invoke();
        }
        private Bitmap _curImageOrig;
        private SizeF _curImageOrigSize;
        private Bitmap _curImageScaled;
        private readonly string _datapath;
        private List<JSONImageInfo> _filteredImages;
        private readonly List<JSONImageInfo> _images;
        private readonly Random _rand = new Random();
        private float _ratio;
        private int _workingIndex;
    }
}