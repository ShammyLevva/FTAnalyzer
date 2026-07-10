using FTAnalyzer.Graphics;

namespace FTAnalyzer.Theme
{
    // GroupBox's native border/caption are drawn by the OS and ignore BackColor/ForeColor
    // entirely - the same class of bug as CheckBox/RadioButton/ProgressBar. Worse: the disabled
    // caption text hardcodes SystemColors.GrayText, unreadable against a dark background, and a
    // Paint-event workaround (the approach used before this control existed - see
    // GraphicsUtilities.DrawGroupBox) can only add a colored BORDER on top, since the event fires
    // after the native (broken) caption text has already been drawn underneath it. Fully
    // owner-painting - reusing DrawGroupBox for the border, adding the caption text ourselves -
    // fixes both at once.
    public class ThemedGroupBox : GroupBox
    {
        public ThemedGroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            using (SolidBrush backBrush = new(BackColor))
                g.FillRectangle(backBrush, ClientRectangle);

            Color borderColor = Enabled ? ActiveColors.Border : ActiveColors.DisabledText;
            GraphicsUtilities.DrawGroupBox(this, g, borderColor, 2);

            if (!string.IsNullOrEmpty(Text))
            {
                Color textColor = Enabled ? ForeColor : ActiveColors.DisabledText;
                using SolidBrush textBrush = new(textColor);
                // Must match DrawGroupBox's own textGap floor, or the border's top-left corner
                // and the caption text drift out of alignment on boxes with Padding.Left < 4.
                g.DrawString(Text, Font, textBrush, Math.Max(Padding.Left, 4), 0);
            }
        }
    }
}
