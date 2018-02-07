using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace FTAnalyzer.Utilities
{
    class ScrollingRichTextBox : System.Windows.Forms.RichTextBox
    {
        private class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(
              IntPtr hWnd,
              uint Msg,
              IntPtr wParam,
              IntPtr lParam);
        }

        private const int WM_VSCROLL = 277;
        private const int SB_LINEUP = 0;
        private const int SB_LINEDOWN = 1;
        private const int SB_TOP = 6;
        private const int SB_BOTTOM = 7;

        public void ScrollToBottom()
        {
            NativeMethods.SendMessage(Handle, WM_VSCROLL, new IntPtr(SB_BOTTOM), new IntPtr(0));
        }

        public void ScrollToTop()
        {
            NativeMethods.SendMessage(Handle, WM_VSCROLL, new IntPtr(SB_TOP), new IntPtr(0));
        }

        public void ScrollLineDown()
        {
            NativeMethods.SendMessage(Handle, WM_VSCROLL, new IntPtr(SB_LINEDOWN), new IntPtr(0));
        }

        public void ScrollLineUp()
        {
            NativeMethods.SendMessage(Handle, WM_VSCROLL, new IntPtr(SB_LINEUP), new IntPtr(0));
        }
    }
}
