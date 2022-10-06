﻿using System;
using System.Drawing;
using System.Linq;
using JSON;

namespace BackgroundSwitcher {
    public class JSONSettings : JSONObject {
        public JSONSettings(string json) : base(json) {
        }
        public string[] BaseFolders {
            get => _baseFolders ?? (_baseFolders = optJSONArray("BaseFolders")?.ToArray() ?? Array.Empty<string>());
            set {
                put("BaseFolders", new JSONArray(value));
                _baseFolders = null;
            }
        }
        public string EditImageCommand => optString("EditImageCommand");
        public string[] Folders {
            get => _folders ?? (_folders = optJSONArray("Folders")?.ToArray() ?? Array.Empty<string>());
            set {
                put("Folders", new JSONArray(value));
                _folders = null;
            }
        }
        public string[] ImageExtensions {
            get {
                if (_imageExtensions == null) {
                    _imageExtensions = optJSONArray("ImageExtensions")?.ToArray() ?? new[] { "JPG", "JPEG", "PNG", "BMP" };
                    _imageExtensions = _imageExtensions.Select(s => s.ToUpper()).ToArray();
                }
                return _imageExtensions;
            }
            set {
                if (value == null || value.Length == 0) {
                    put("ImageExtensions", new JSONArray().put("JPG", "PNG"));
                }
                else put("ImageExtensions", new JSONArray(value));
            }
        }
        public int Margin {
            get => optInt("Margin", 10);
            set => put("Margin", value);
        }
        public double MinRatioDiff => optDouble("MinRatioDiff", 0.01);
        public int MinShowIntervalDays {
            get => optInt("MinShowIntervalDays", 8);
            set => put("MinShowIntervalDays", value);
        }
        public Size MinSourceImageSize {
            get {
                var obj = optJSONObject("MinSourceImageSize") ?? new JSONObject();
                return new Size(obj.optInt("Width", 900), obj.optInt("Height", 900));
            }
            set => put("MinSourceImageSize", new JSONObject().put("Width", value.Width).put("Height", value.Height));
        }
        public string[] NonRecurseFolders {
            get => _nonRecurseFolders ?? (_nonRecurseFolders = optJSONArray("NonRecurseFolders")?.ToArray() ?? Array.Empty<string>());
            set {
                put("NonRecurseFolders", new JSONArray(value));
                _nonRecurseFolders = null;
            }
        }
        public bool ShowFilenames => optBoolean("ShowFilenames");
        public bool ShowFolders => optBoolean("ShowFolders");
        public string[] ShowFoldersAfter {
            get {
                if (_showFoldersAfter == null) {
                    _showFoldersAfter = optJSONArray("ShowFoldersAfter")?.ToArray() ?? Array.Empty<string>();
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