using System.Drawing.Drawing2D;

namespace FTAnalyzer.Theme
{
    // See ThemedCheckBox - same "WinForms hardcodes disabled text to an unreadable-on-dark
    // SystemColors.GrayText" problem, same fix (full owner-paint from ActiveColors).
    public class ThemedRadioButton : RadioButton
    {
        public ThemedRadioButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            using (SolidBrush backBrush = new(BackColor))
                g.FillRectangle(backBrush, ClientRectangle);

            Size glyphSize = new(13, 13);
            Rectangle glyphRect = new(0, (Height - glyphSize.Height) / 2, glyphSize.Width - 1, glyphSize.Height - 1);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color borderColor = Enabled ? ActiveColors.Border : ActiveColors.DisabledText;
            using (Pen borderPen = new(borderColor))
                g.DrawEllipse(borderPen, glyphRect);

            using (SolidBrush fillBrush = new(ActiveColors.Card))
                g.FillEllipse(fillBrush, Rectangle.Inflate(glyphRect, -1, -1));

            if (Checked)
            {
                Rectangle dotRect = Rectangle.Inflate(glyphRect, -3, -3);
                using SolidBrush dotBrush = new(Enabled ? ActiveColors.Primary : ActiveColors.PrimaryPale);
                g.FillEllipse(dotBrush, dotRect);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                Rectangle textRect = new(glyphSize.Width + 4, 0, Math.Max(0, Width - glyphSize.Width - 4), Height);
                Color textColor = Enabled ? ForeColor : ActiveColors.DisabledText;
                TextRenderer.DrawText(g, Text, Font, textRect, textColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.WordEllipsis);
            }

            if (Focused && ShowFocusCues)
            {
                Rectangle focusRect = ClientRectangle;
                focusRect.Inflate(-1, -1);
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }
        }
    }
}
