using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BackgroundSwitcher.Panels {
    public partial class FoldersPanel : MyUserControl {
        public FoldersPanel() {
            InitializeComponent();
        }
        private void btnAddBaseFolder_Click(object sender, EventArgs e) {
            string s = txtAddValue.Text.Trim();
            if (s == "" || listBaseFolders.Items.Contains(s)) return;
            listBaseFolders.Items.Add(s);
            var list = _settings.BaseFolders.ToList();
            list.Add(s);
            _settings.BaseFolders = list.ToArray();
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void btnAddFolder_Click(object sender, EventArgs e) {
            string s = txtAddValue.Text.Trim();
            if (s == "" || listFolders.Items.Contains(s)) return;
            listFolders.Items.Add(s);
            var list = _settings.Folders.ToList();
            list.Add(s);
            _settings.Folders = list.ToArray();
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void btnAddNonRecurse_Click(object sender, EventArgs e) {
            string s = txtAddValue.Text.Trim();
            if (s == "" || listNonRecurse.Items.Contains(s)) return;
            listNonRecurse.Items.Add(s);
            var list = _settings.NonRecurseFolders.ToList();
            list.Add(s);
            _settings.NonRecurseFolders = list.ToArray();
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void listBaseFolders_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                listBaseFolders.Items.Remove(listBaseFolders.SelectedItem);
                _settings.BaseFolders = listBaseFolders.Items.Cast<string>().ToArray();
                File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
            }
        }
        private void listFolders_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                listFolders.Items.Remove(listFolders.SelectedItem);
                _settings.Folders = listFolders.Items.Cast<string>().ToArray();
                File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
            }
        }
        private void listNonRecurse_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                listNonRecurse.Items.Remove(listNonRecurse.SelectedItem);
                _settings.NonRecurseFolders = listNonRecurse.Items.Cast<string>().ToArray();
                File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
            }
        }
        public override void SetDataPath(string path) {
            base.SetDataPath(path);
            listFolders.Items.AddRange(_settings.Folders.ToArray<object>());
            listNonRecurse.Items.AddRange(_settings.NonRecurseFolders.ToArray<object>());
            listBaseFolders.Items.AddRange(_settings.BaseFolders.ToArray<object>());
        }
    }
}