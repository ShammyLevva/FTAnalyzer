using System;

namespace FTAnalyzer.Utilities
{
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
    }
}
