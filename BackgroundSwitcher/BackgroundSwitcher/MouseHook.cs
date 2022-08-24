using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BackgroundSwitcher {
    public static class MouseHook {
        public static event EventHandler<MouseHookEventArgs> MouseAction;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        public static string MouseMessageToString(MouseMessages m) {
            string s = "";
            if (m == MouseMessages.WM_MOUSEMOVE) return "move";
            if (m.HasFlag(MouseMessages.WM_LBUTTONDOWN)) s += "left down, ";
            if (m.HasFlag(MouseMessages.WM_LBUTTONUP)) s += "left up, ";
            //if (m.HasFlag(MouseMessages.WM_MOUSEMOVE)) s += "move, ";
            if (m.HasFlag(MouseMessages.WM_MOUSEWHEEL)) s += "wheel, ";
            if (m.HasFlag(MouseMessages.WM_RBUTTONDOWN)) s += "right down, ";
            if (m.HasFlag(MouseMessages.WM_RBUTTONUP)) s += "right up";
            return s.Trim().Trim(',');
        }
        public static void Start() {
            _hookID = SetHook(_proc);
        }
        public static void Stop() {
            UnhookWindowsHookEx(_hookID);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode >= 0) {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseAction?.Invoke(null, new MouseHookEventArgs {Type = (MouseMessages)wParam, Data = hookStruct});
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        private static IntPtr SetHook(LowLevelMouseProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule?.ModuleName), 0);
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private const int WH_MOUSE_LL = 14;
        public enum MouseMessages {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }
        public class MouseHookEventArgs : EventArgs {
            public MSLLHOOKSTRUCT Data;
            public MouseMessages Type;
        }
        [StructLayout(LayoutKind.Sequential)] public struct MSLLHOOKSTRUCT {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)] public struct POINT {
            public int x;
            public int y;
        }
    }
}