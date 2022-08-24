using System;
using System.Runtime.InteropServices;

namespace BackgroundSwitcher {
    public static class Imported {
        [DllImport("user32.dll")] public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string strname);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
    }
}