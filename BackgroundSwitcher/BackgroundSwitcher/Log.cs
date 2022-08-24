using System;
using System.IO;

namespace BackgroundSwitcher {
    public static class Log {
        private static string _fname = "C:\\ProgramData\\BackgroundSwitcher\\log.txt";
        public static void Init(string path) {
            Directory.CreateDirectory(path);
            _fname = Path.Combine(path, "log.txt");
            if (File.Exists(_fname)) {
                var lwt = File.GetLastWriteTime(_fname);
                if ((DateTime.Now - lwt).TotalDays > 10) File.Delete(_fname);
            }
        }
        public static void Write(string str) {
            Console.WriteLine(str);
            File.AppendAllText(_fname, str + Environment.NewLine);
        }
    }
}