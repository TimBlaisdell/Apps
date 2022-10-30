using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BackgroundSwitcher.Panels {
    public partial class SettingsPanel : MyUserControl {
        public SettingsPanel() {
            InitializeComponent();
            SetDGVToolTipDelay();
        }
        private int BorderWidth {
            get {
                try {
                    return int.Parse(GetCell("border").Value.ToString());
                }
                catch {
                    return 0;
                }
            }
            //set => GetCell("border").Value = value.ToString();
        }
        public bool ClickOutsideWindow {
            get => GetCell("watch mouse").Value.ToString().ToLower().StartsWith("t");
            set {
                var cell = GetCell("watch mouse");
                if (cell != null) cell.Value = value.ToString();
            }
        }
        private string EditImageCommand => GetCell("image edit").Value.ToString();
        private string ImageExts => GetCell("image ext").Value.ToString();
        private double MaxRatioDiff {
            get {
                try {
                    return double.Parse(GetCell("ratio tol").Value.ToString());
                }
                catch {
                    return 0.0;
                }
            }
        }
        private Size MinImageSize {
            get {
                var cell = GetCell("minimum image size");
                var vals = cell.Value.ToString().Split(',');
                if (vals.Length == 1) vals = new[] { vals[0], vals[0] };
                if (vals.Length != 2) throw new Exception("Minimum image size must be entered as \"width, height\".");
                int x, y;
                try {
                    x = int.Parse(vals[0]);
                    y = int.Parse(vals[1]);
                }
                catch {
                    throw new Exception("Failed to parse minimum size values.  Must be entered as \"width, height\", and both values must be integers.");
                }
                return new Size(x, y);
            }
        }
        private int MinShowInterval {
            get {
                try {
                    return int.Parse(GetCell("Minimum show interval").Value.ToString());
                }
                catch {
                    return 0;
                }
            }
        }
        private bool ShowFilenames {
            get {
                try {
                    return bool.Parse(GetCell("show filenames").Value.ToString());
                }
                catch {
                    return false;
                }
            }
            //set => GetCell("show filenames").Value = value.ToString();
        }
        private bool ShowFolders {
            get {
                try {
                    return bool.Parse(GetCell("show folders with filenames").Value.ToString());
                }
                catch {
                    return false;
                }
            }
            //set => GetCell("show folders with filenames").Value = value.ToString();
        }
        private string[] ShowFoldersAfter => GetCell("show folders cut-off").Value.ToString().Split(',').Select(s => s.Trim()).ToArray();
        private int SplitIterations {
            get {
                try {
                    return int.Parse(GetCell("split").Value.ToString());
                }
                catch {
                    return 0;
                }
            }
        }
        public event EventHandler WatchMouseChanged;
        private void dgvSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (_inSetDataPath || e.ColumnIndex != 1 || e.RowIndex < 0) return;
            ((RowTagData)dgvSettings.Rows[e.RowIndex].Tag).Action();
            SaveSettings();
        }
        public override void SetDataPath(string path) {
            _inSetDataPath = true;
            try {
                base.SetDataPath(path);
                var tagdatas = new[] {
                                         new RowTagData {
                                                            Name = "Image extensions",
                                                            Tooltip = "A comma-separated list of file extensions that will be recognized as images.",
                                                            Action = () => _settings.ImageExtensions = ImageExts.Split(',').Select(s => s.Trim().ToUpper()).ToArray()
                                                        },
                                         new RowTagData {
                                                            Name = "Minimum image size",
                                                            Tooltip = "Images with width or height less that these values will be ignored.\n \n" +
                                                                      "Width is the first value, height the second.",
                                                            Action = () => _settings.MinSourceImageSize = MinImageSize
                                                        },
                                         new RowTagData {
                                                            Name = "Border width",
                                                            Tooltip = "Width in pixels of the border between images.",
                                                            Action = () => _settings.Margin = BorderWidth
                                                        },
                                         new RowTagData {
                                                            Name = "Minimum show interval (days)",
                                                            Tooltip = "After an image is shown, it will not be shown again for at least this many days.\n \n" +
                                                                      "Enter 0 if you don't care.",
                                                            Action = () => _settings.MinShowIntervalDays = MinShowInterval
                                                        },
                                         new RowTagData {
                                                            Name = "Ratio tolerance",
                                                            Tooltip = "When an image does not have a focus rectangle, this is the maximum allowed difference\n" +
                                                                      "between the aspect ratio of the image, and that of the rectangle to be filled.\n \n" +
                                                                      "0 means the aspect ratios must match exactly (not recommended), values\n" +
                                                                      "in the range 0.01 to 0.5 are recommended.\n \n" +
                                                                      "The lower you set this, the harder it will be to find images to fit into a rectangle. The\n" +
                                                                      "higher you set it, the more of the image will be cropped off to fit.",
                                                            Action = () => _settings.MaxRatioDiff = MaxRatioDiff
                                                        },
                                         new RowTagData {
                                                            Name = "Split iterations",
                                                            Tooltip = "This is the number of times the randomized splitting algorithm will be run\n" +
                                                                      "for each screen when creating randomly-sized regions to fill with images.\n \n" +
                                                                      "The higher you set it, the more images will fill the screen (and the\n" +
                                                                      "smaller each will be).",
                                                            Action = () => _settings.SplitIterations = SplitIterations
                                                        },
                                         new RowTagData {
                                                            Name = "Show filenames",
                                                            Tooltip = "Indicates wither filenames should be shown in the upper-left\n" +
                                                                      "corner of images.\n \n" +
                                                                      "Set to true or false.",
                                                            Action = () => _settings.ShowFilenames = ShowFilenames
                                                        },
                                         new RowTagData {
                                                            Name = "Show folders with filenames",
                                                            Tooltip = "If showing filenames, this indicates wither the paths should be shown,\n" +
                                                                      "or just the filenames.\n" +
                                                                      "Set to true to show paths, or false to show only filenames.",
                                                            Action = () => _settings.ShowFolders = ShowFolders
                                                        },
                                         new RowTagData {
                                                            Name = "Show folders cut-off names",
                                                            Tooltip = "If showing filenames and showing folders, this specifies a comma-separated list\n" +
                                                                      "of folder names (not paths, just names).\n \n" +
                                                                      "The paths shown on-screen will be clipped to just show the part after these names.\n \n" +
                                                                      "For example, if a file path is \"C:\\Images\\Camera\\Christmas2000\\Lights.jpg\",\n" +
                                                                      "and \"Camera\" is in the cut-off list, then only \"Christmas2000\\Lights.jpg\"\n" +
                                                                      "will be shown.",
                                                            Action = () => _settings.ShowFoldersAfter = ShowFoldersAfter
                                                        },
                                         new RowTagData {
                                                            Name = "Image edit command",
                                                            Tooltip = "This is the full path to the program that will be invoked to edit an image.\n \n" +
                                                                      "Some examples:\n" +
                                                                      "C:\\Windows\\System32\\mspaint.exe\n" +
                                                                      "C:\\Program Files\\GIMP 2\\bin\\gimp-2.10.exe\n \n" +
                                                                      "The path to the file to be edited will be passed to the program as an\n" +
                                                                      "argument on the command line, so whatever image editor you use must be\n" +
                                                                      "able to accept that.",
                                                            Action = () => _settings.EditImageCommand = EditImageCommand
                                                        },
                                         new RowTagData {
                                                            Name = "Watch mouse activity",
                                                            Tooltip = "Set to \"true\" to watch mouse location and activity outside the window.\n\n" +
                                                                      "The image info tab will be updated with the image under the mouse, and you can\n" +
                                                                      "click to load the image in the editor or right-click to go to the containing folder.",
                                                            Action = () => {
                                                                         _settings.ClickOutsideWindow = ClickOutsideWindow;
                                                                         WatchMouseChanged?.Invoke(this, EventArgs.Empty);
                                                                     }
                                                        }
                                     };
                var vals = new[] {
                                     string.Join(", ", _settings.ImageExtensions),
                                     _settings.MinSourceImageSize.Width + ", " + _settings.MinSourceImageSize.Height,
                                     _settings.Margin.ToString(),
                                     _settings.MinShowIntervalDays.ToString(),
                                     _settings.MaxRatioDiff.ToString(),
                                     _settings.SplitIterations.ToString(),
                                     _settings.ShowFilenames.ToString(),
                                     _settings.ShowFolders.ToString(),
                                     string.Join(", ", _settings.ShowFoldersAfter),
                                     _settings.EditImageCommand,
                                     _settings.ClickOutsideWindow.ToString()
                                 };
                if (tagdatas.Length != vals.Length) throw new Exception("Something's wrong with the code.");
                dgvSettings.Rows.Add(tagdatas.Length);
                for (int i = 0; i < tagdatas.Length; ++i) {
                    dgvSettings.Rows[i].Cells[0].Value = tagdatas[i].Name;
                    dgvSettings.Rows[i].Cells[1].Value = vals[i];
                    dgvSettings.Rows[i].Cells[0].ToolTipText = tagdatas[i].Tooltip;
                    dgvSettings.Rows[i].Cells[1].ToolTipText = tagdatas[i].Tooltip;
                    dgvSettings.Rows[i].Tag = tagdatas[i];
                }
                dgvSettings.Sort(colSettingName, ListSortDirection.Ascending);
            }
            finally {
                _inSetDataPath = false;
            }
        }
        /// <summary>
        ///     Returns the cell corresponding to the setting name.  Partial names are ok.
        /// </summary>
        private DataGridViewCell GetCell(string name) {
            name = name.ToLower();
            foreach (DataGridViewRow row in dgvSettings.Rows) {
                if (row.Cells[0].Value.ToString().ToLower().StartsWith(name)) return row.Cells[1];
            }
            return null;
        }
        /// <summary>
        ///     A hack I found online to increase the time that DataGridView tooltips remain visible.
        /// </summary>
        private void SetDGVToolTipDelay() {
            var fi_toolTipControl = typeof(DataGridView).GetField("toolTipControl", BindingFlags.Instance | BindingFlags.NonPublic);
            var toolTipControl = fi_toolTipControl?.GetValue(dgvSettings);

            var fi_ToolTip = fi_toolTipControl?.FieldType.GetField("toolTip", BindingFlags.Instance | BindingFlags.NonPublic);
            var tt = (ToolTip)fi_ToolTip?.GetValue(toolTipControl);
            if (tt == null) {
                tt = new ToolTip { ShowAlways = true, InitialDelay = 0, UseFading = false, UseAnimation = false };
                fi_ToolTip?.SetValue(toolTipControl, tt);
            }
            tt.AutoPopDelay = 20000;
        }
        private bool _inSetDataPath;
        private class RowTagData {
            public Action Action;
            public string Name;
            public string Tooltip;
        }
    }
}