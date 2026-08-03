using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace FTAnalyzer.Utilities
{
    [SupportedOSPlatform("windows")]
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

        const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Suspends and resumes painting for a control via WM_SETREDRAW, to avoid visible flicker
        /// when many child controls are updated in a batch (e.g. a font-scale change walking an
        /// entire form's control tree). Callers must always resume drawing in a finally block.
        /// </summary>
        internal static void SuspendDrawing(Control control) => SendMessage(control.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

        internal static void ResumeDrawing(Control control) => SendMessage(control.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);

        const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

        /// <summary>
        /// Darkens a window's title bar via DWM (Windows 10 1809+). Best-effort: silently does
        /// nothing on OS builds that don't support the attribute rather than throwing.
        /// </summary>
        internal static void SetImmersiveDarkMode(IWin32Window window, bool enabled)
        {
            int value = enabled ? 1 : 0;
            _ = DwmSetWindowAttribute(window.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);

        /// <summary>
        /// ProgressBar (and a few other controls) render entirely via the OS visual-style
        /// handler when <see cref="Application.EnableVisualStyles"/> is on, which ignores
        /// BackColor/ForeColor completely - opting a specific control out of theming falls back
        /// to classic GDI drawing, where those color properties actually take effect.
        /// </summary>
        internal static void DisableVisualStyles(Control control) => SetWindowTheme(control.Handle, string.Empty, null);

        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        const uint RDW_INVALIDATE = 0x0001;
        const uint RDW_FRAME = 0x0400;
        const uint RDW_UPDATENOW = 0x0100;

        /// <summary>
        /// Native scrollbars (on grids, tree views, etc.) are drawn by the OS and ignore
        /// BackColor/ForeColor entirely - unlike ProgressBar/TrackBar there's no owning-draw
        /// escape hatch for them. Windows 10 1809+ ships a "DarkMode_Explorer" visual-style class
        /// that renders them with dark-mode colors instead; switching a control's theme class to
        /// it (or back to the default "Explorer" for light mode) is the standard way apps get
        /// dark scrollbars without fully custom-drawing them.
        /// </summary>
        internal static void SetScrollBarTheme(Control control, bool dark)
        {
            SetWindowTheme(control.Handle, dark ? "DarkMode_Explorer" : "Explorer", null);
            // SetWindowTheme only changes which theme class is ASSOCIATED with the window - it
            // doesn't itself repaint anything. Non-client scrollbar chrome already painted under
            // the old theme stays stale until something else forces a redraw (a resize, a scroll,
            // window activation, ...), which a control shown once at startup and never touched
            // again (e.g. a static multi-column CheckedListBox) may never naturally get. Force an
            // immediate NC repaint so the new theme actually shows up right away.
            RedrawWindow(control.Handle, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_FRAME | RDW_UPDATENOW);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetComboBoxInfo(IntPtr hWndCombo, ref COMBOBOXINFO pcbi);

        [StructLayout(LayoutKind.Sequential)]
        struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int buttonState;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }

        /// <summary>
        /// A ComboBox's open dropdown list is a separate native "ComboLBox" window, not a
        /// WinForms child <see cref="Control"/> - same underlying problem as DataGridView's child
        /// ScrollBar controls (see <see cref="SetScrollBarTheme"/>), except here there isn't even
        /// a .NET Control to loop over, so the list window's handle has to be looked up via
        /// GetComboBoxInfo instead. Left untouched, any dropdown with enough items to need a
        /// scrollbar (e.g. the Census Date selector) showed a light-mode scrollbar regardless of
        /// theme, even though the list rows themselves are already owner-drawn correctly.
        /// </summary>
        internal static void SetComboBoxListTheme(ComboBox comboBox, bool dark)
        {
            COMBOBOXINFO info = new() { cbSize = Marshal.SizeOf<COMBOBOXINFO>() };
            if (GetComboBoxInfo(comboBox.Handle, ref info) && info.hwndList != IntPtr.Zero)
                SetWindowTheme(info.hwndList, dark ? "DarkMode_Explorer" : "Explorer", null);
        }

        [DllImport("user32.dll")]
        static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

        /// <summary>
        /// Marks a native control's entire client area as up to date without going through
        /// BeginPaint/EndPaint. Needed when a WM_PAINT handler draws everything itself and
        /// swallows the message instead of forwarding it to the native control (see
        /// HighlightTabControl) - without this, Windows sees the update region as still invalid
        /// and immediately re-posts WM_PAINT in a tight loop.
        /// </summary>
        internal static void ValidateRect(Control control) => ValidateRect(control.Handle, IntPtr.Zero);

        [DllImport("user32.dll", SetLastError = true, BestFitMapping = false, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        static extern IntPtr FindWindow(string lpClassName, string? lpWindowName);

        [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
        static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        public enum TaskbarPosition { Top, Bottom, Left, Right };

        public static int TopTaskbarOffset { get; set; }

        public static TaskbarPosition GetTaskBarPos()
        {
            IntPtr hwnd = FindWindow("Shell_TrayWnd", null);

            APPBARDATA abd = new();
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