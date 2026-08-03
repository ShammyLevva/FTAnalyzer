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

            // Fixed at 13px regardless of the user's font-scale setting used to read fine at the
            // default (~8pt) level, but FontScaler.Apply can grow Font up to 14pt - the glyph (and
            // the selected dot inside it, at a fixed fraction of the glyph) stayed pinned at its
            // smallest size while the label text next to it kept growing, so the dot became
            // proportionally tiny and hard to spot at higher scale levels. Size it off the current
            // Font instead, floored at the original 13px for the default scale.
            int diameter = Math.Max(13, Font.Height - 2);
            Size glyphSize = new(diameter, diameter);
            Rectangle glyphRect = new(0, (Height - glyphSize.Height) / 2, glyphSize.Width - 1, glyphSize.Height - 1);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color borderColor = Enabled ? ActiveColors.Border : ActiveColors.DisabledText;
            using (Pen borderPen = new(borderColor))
                g.DrawEllipse(borderPen, glyphRect);

            using (SolidBrush fillBrush = new(ActiveColors.Card))
                g.FillEllipse(fillBrush, Rectangle.Inflate(glyphRect, -1, -1));

            if (Checked)
            {
                int dotInset = Math.Max(3, diameter / 4);
                Rectangle dotRect = Rectangle.Inflate(glyphRect, -dotInset, -dotInset);
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
