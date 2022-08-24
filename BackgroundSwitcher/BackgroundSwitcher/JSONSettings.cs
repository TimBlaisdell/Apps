using System.Drawing;
using System.Linq;
using JSON;

namespace BackgroundSwitcher {
    public class JSONSettings : JSONObject {
        public JSONSettings(string json) : base(json) {
        }
        public string[] BaseFolders => _baseFolders ?? (_baseFolders = optJSONArray("BaseFolders")?.ToArray() ?? new string[0]);
        public string EditImageCommand => optString("EditImageCommand");
        public string[] Folders => _folders ?? (_folders = optJSONArray("Folders")?.ToArray() ?? new string[0]);
        public string[] ImageExtensions {
            get {
                if (_imageExtensions == null) {
                    _imageExtensions = optJSONArray("ImageExtensions")?.ToArray() ?? new[] { "JPG", "JPEG", "PNG", "BMP" };
                    _imageExtensions = _imageExtensions.Select(s => s.ToUpper()).ToArray();
                }
                return _imageExtensions;
            }
        }
        public int Margin => optInt("Margin", 10);
        public double MinRatioDiff => optDouble("MinRatioDiff", 0.01);
        public int MinShowIntervalDays => optInt("MinShowIntervalDays", 8);
        public Size MinSourceImageSize {
            get {
                var obj = optJSONObject("MinSourceImageSize") ?? new JSONObject();
                return new Size(obj.optInt("Width", 900), obj.optInt("Height", 900));
            }
        }
        public string[] NonRecurseFolders => _nonRecurseFolders ?? (_nonRecurseFolders = optJSONArray("NonRecurseFolders")?.ToArray() ?? new string[0]);
        public bool ShowFilenames => optBoolean("ShowFilenames");
        public bool ShowFolders => optBoolean("ShowFolders");
        public string[] ShowFoldersAfter {
            get {
                if (_showFoldersAfter == null) {
                    _showFoldersAfter = optJSONArray("ShowFoldersAfter")?.ToArray() ?? new string[0];
                    _showFoldersAfter = _showFoldersAfter.Select(s => s.ToUpper()).ToArray();
                }
                return _showFoldersAfter;
            }
        }
        public int SplitIterations => optInt("SplitIterations", 10);
        private string[] _baseFolders;
        private string[] _folders;
        private string[] _imageExtensions;
        private string[] _nonRecurseFolders;
        private string[] _showFoldersAfter;
    }
}