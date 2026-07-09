using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms.Controls
{
    // TrackBar's channel is drawn by the OS visual-style handler and shows a hard-coded white
    // background regardless of BackColor - the same class of bug as ProgressBar and the
    // TabControl frame. Opting the control's HWND out of theming (once its handle exists) and
    // painting our own background before the native thumb/channel draws on top is the standard
    // workaround.
    public class ThemedTrackBar : TrackBar
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            NativeMethods.DisableVisualStyles(this);
        }

        const int WM_ERASEBKGND = 0x0014;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                using System.Drawing.Graphics g = System.Drawing.Graphics.FromHdc(m.WParam);
                using SolidBrush brush = new(BackColor);
                g.FillRectangle(brush, ClientRectangle);
                m.Result = 1;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
