using System;
using System.Runtime.InteropServices;

namespace FTAnalyzer.Utilities
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        [DllImport("SHCore.dll", SetLastError = true)]
        internal static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();

        internal enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        internal enum DPI_AWARENESS_CONTEXT
        {
            DPI_AWARENESS_CONTEXT_UNAWARE = 16,
            DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = 17,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = 18,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = 34
        }

        [DllImport("gdi32.dll")]
        internal static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("user32.dll", SetLastError = true, BestFitMapping =false, CharSet = CharSet.Unicode,ThrowOnUnmappableChar = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
        static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        public enum TaskbarPosition { Top, Bottom, Left, Right };

        public static int TopTaskbarOffset { get; set; }

        public static TaskbarPosition GetTaskBarPos()
        {
            IntPtr hwnd = FindWindow("Shell_TrayWnd", null);
            if (hwnd != null)
            {
                APPBARDATA abd = new APPBARDATA();
                abd.cbSize = Marshal.SizeOf(abd);
                abd.hWnd = hwnd;
                _ = SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);
                int uEdge = GetEdge(abd.rc);
                TopTaskbarOffset = 0;
                switch (uEdge)
                {
                    case (int)ABEdge.ABE_LEFT:
                        return TaskbarPosition.Left;
                    case (int)ABEdge.ABE_RIGHT:
                        return TaskbarPosition.Right;
                    case (int)ABEdge.ABE_TOP:
                        TopTaskbarOffset = abd.rc.bottom;
                        return TaskbarPosition.Top;
                    case (int)ABEdge.ABE_BOTTOM:
                        return TaskbarPosition.Bottom;
                    default:
                        return TaskbarPosition.Bottom;
                }
            }
            return TaskbarPosition.Bottom;
        }

        static int GetEdge(RECT rc)
        {
            int uEdge;
            if (rc.top == rc.left && rc.bottom > rc.right)
                uEdge = (int)ABEdge.ABE_LEFT;
            else if (rc.top == rc.left && rc.bottom < rc.right)
                uEdge = (int)ABEdge.ABE_TOP;
            else if (rc.top > rc.left)
                uEdge = (int)ABEdge.ABE_BOTTOM;
            else
                uEdge = (int)ABEdge.ABE_RIGHT;
            return uEdge;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public bool lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        enum ABEdge
        {
            ABE_LEFT = 0,
            ABE_TOP = 1,
            ABE_RIGHT = 2,
            ABE_BOTTOM = 3
        }

        enum ABMsg
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }
    }
}