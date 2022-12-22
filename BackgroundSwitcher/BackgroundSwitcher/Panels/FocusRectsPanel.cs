using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JSON;

namespace BackgroundSwitcher.Panels {
    public partial class FocusRectsPanel : MyUserControl {
        public FocusRectsPanel() {
            InitializeComponent();
            _canvasSizeDiff = new Size(Width - panelCanvas.Width, Height - panelCanvas.Height);
            pbarLoading.Maximum = 100;
            pbarLoading.Value = 0;
            _pBarBuffer = 100;
        }
        public bool Loading { get; private set; }
        public int ProgbarMax {
            get => Loading ? pbarLoading.Maximum - _pBarBuffer : 0;
            set {
                if (value <= _pBarBuffer) {
                    _pBarBuffer -= value;
                    return;
                }
                _pBarBuffer = 0;
                if (Loading) pbarLoading.BeginInvoke(new MethodInvoker(() => { pbarLoading.Maximum = value; }));
            }
        }
        public int ProgbarValue {
            get => Loading ? pbarLoading.Value : 0;
            set {
                if (Loading) pbarLoading.BeginInvoke(new MethodInvoker(() => { pbarLoading.Value = value; }));
            }
        }
        public override Size TargetSize {
            get => base.TargetSize;
            set {
                base.TargetSize = value;
                OnSizeChanged(EventArgs.Empty);
            }
        }
        public event EventHandler<string> EditImage;
        public event EventHandler<PrepFocusRectsPanelEventArgs> PrepFocusRectsPanel;
        private void btnNeverShow_Click(object sender, EventArgs e) {
            try {
                string nevershowfname = Path.Combine(_dataPath, "NeverShow.json");
                var nevershow = File.Exists(nevershowfname) ? new JSONArray(File.ReadAllText(nevershowfname)) : new JSONArray();
                string fn = Path.GetFileNameWithoutExtension(_images[_workingIndex].Path);
                if (!nevershow.ToArray().Contains(fn)) {
                    nevershow.put(Path.GetFileNameWithoutExtension(_images[_workingIndex].Path));
                    File.WriteAllText(nevershowfname, nevershow.ToString(true, 4));
                }
                MessageBox.Show(this, "Image added to never show list.", "Nevershow added");
                _filteredImages?.Remove(_images[_workingIndex]);
                _images.RemoveAt(_workingIndex);
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
        private void menuLoadImage_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                var index = _images.FindIndex(i => i.Path == openFileDialog.FileName);
                if (index < 0) {
                    MessageBox.Show("Selected file was not found in the image list.", "File not found");
                    return;
                }
                SetImage(_images[index].Path);
            }
        }
        private void menuLoadRandom_Click(object sender, EventArgs e) {
            string path;
            do {
                path = _images[_rand.Next(_images.Count)].Path;
            } while (!File.Exists(path));
            SetImage(path);
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
                case Keys.R:
                    menuLoadRandom_Click(null, null);
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
        public void OnResizeEnd(EventArgs e) {
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
        public override void SetDataPath(string path) {
            base.SetDataPath(path);
            lblLoading.Dock = DockStyle.Fill;
            lblLoading.Visible = true;
            lblLoading.BringToFront();
            pbarLoading.Value = 0;
            pbarLoading.Visible = true;
            pbarLoading.BringToFront();
            new Thread(() => {
                           Thread.Sleep(1000);
                           Loading = true;
                           try {
                               var e = new PrepFocusRectsPanelEventArgs();
                               PrepFocusRectsPanel?.Invoke(this, e);
                               _images = e.Images;
                               if (_images != null) {
                                   NewWorkingIndex();
                                   int i = _workingIndex;
                                   _workingIndex = -1;
                                   Invoke(new MethodInvoker(() => SetImage(_images[i].Path)));
                                   UpdateRemaining();
                                   Invoke(new MethodInvoker(() => {
                                                                lblLoading.Visible = false;
                                                                pbarLoading.Visible = false;
                                                            }));
                               }
                           }
                           catch {
                               Loading = false;
                           }
                       }).Start();
        }
        public void SetImage(string path) {
            if (_images == null) return;
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
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            if (ParentForm?.WindowState == FormWindowState.Maximized) OnResizeEnd(e);
        }
        private void LoadImage() {
            var canvasSize = new Size(TargetSize.Width - _canvasSizeDiff.Width, TargetSize.Height - _canvasSizeDiff.Height);
            lblImagePath.Text = _images[_workingIndex].Path;
            using (var bmp = new Bitmap(_images[_workingIndex].Path)) {
                _curImageOrig = new Bitmap(bmp, bmp.Size);
            }
            lblImageSize.Text = $"{_curImageOrig.Width}, {_curImageOrig.Height}";
            lblImageSize.Left = Width - lblImageSize.Width - 20;
            _curImageOrigSize = new SizeF(_curImageOrig.Width, _curImageOrig.Height);
            _ratio = canvasSize.Width / _curImageOrigSize.Width;
            var scaledsize = new SizeF(_curImageOrigSize.Width * _ratio, _curImageOrigSize.Height * _ratio);
            if (scaledsize.Height > canvasSize.Height) {
                _ratio = canvasSize.Height / _curImageOrigSize.Height;
                scaledsize = new SizeF(_curImageOrigSize.Width * _ratio, _curImageOrigSize.Height * _ratio);
            }
            if (_ratio == 0) return;
            _curImageScaled?.Dispose();
            panelCanvas.BackgroundImage = _curImageScaled = new Bitmap((int)Math.Round(scaledsize.Width), (int)Math.Round(scaledsize.Height));
            using (var gfx = Graphics.FromImage(_curImageScaled)) {
                gfx.DrawImage(_curImageOrig, new RectangleF(0, 0, scaledsize.Width, scaledsize.Height));
            }
        }
        private void NewWorkingIndex() {
            int tries = 0;
            int oldindex = _workingIndex;
            if (_filteredImages == null || _filteredImages.Count == 0) {
                do {
                    _workingIndex = _rand.Next(_images.Count);
                    ++tries;
                } while (tries < 100 && (_images[_workingIndex].has("FocusRect") || !File.Exists(_images[_workingIndex].Path)));
                if (tries == 100) {
                    _workingIndex = _images.FindIndex(i => !i.has("FocusRect") && File.Exists(i.Path));
                    if (_workingIndex < 0) {
                        _workingIndex = oldindex;
                        InvokeShowMessage(Color.Green, "There are no more images without a FocusRect specification.");
                        //if (ParentForm != null) {
                        //    var mi = new MethodInvoker(() => MessageBox.Show(ParentForm, "There are no more images without a FocusRect specification."));
                        //    if (ParentForm.InvokeRequired) ParentForm.Invoke(mi);
                        //    else mi();
                        //}
                    }
                }
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
        /// <summary>
        ///     Records the difference in size between this panel and the panelCanvas control within it.  This remains constant as
        ///     the size changes, so it can be used with TargetSize to determine the proper target size of panelCanvas.
        ///     This is required because when this panel is not on-screen, it's size is sometimes zero, so TargetSize must be used
        ///     to really know the size it will be when it's on-screen.
        /// </summary>
        private Size _canvasSizeDiff;
        private Bitmap _curImageOrig;
        private SizeF _curImageOrigSize;
        private Bitmap _curImageScaled;
        private List<JSONImageInfo> _filteredImages;
        private List<JSONImageInfo> _images;
        private int _pBarBuffer;
        private readonly Random _rand = new Random();
        private float _ratio;
        private int _workingIndex;
        public class PrepFocusRectsPanelEventArgs : EventArgs {
            public List<JSONImageInfo> Images;
        }
    }
}