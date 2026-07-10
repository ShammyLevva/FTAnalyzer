using System.Drawing.Drawing2D;

namespace FTAnalyzer.Theme
{
    // CheckBox hardcodes disabled-state text to SystemColors.GrayText, ignoring ForeColor
    // entirely - fine against a light background but close to unreadable against
    // ActiveColors.Background in dark mode, and no supported property overrides it. Same class
    // of "native chrome ignores our colors" problem as ThemedProgressBar/HighlightTabControl, so
    // the fix here is the same: stop asking the native control to paint itself and draw the
    // whole thing (glyph + text) from ActiveColors ourselves.
    public class ThemedCheckBox : CheckBox
    {
        public ThemedCheckBox()
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
                g.DrawRectangle(borderPen, glyphRect);

            Rectangle fillRect = glyphRect;
            fillRect.Inflate(-1, -1);
            switch (CheckState)
            {
                case CheckState.Checked:
                    using (SolidBrush fillBrush = new(Enabled ? ActiveColors.Primary : ActiveColors.PrimaryPale))
                        g.FillRectangle(fillBrush, fillRect);
                    using (Pen checkPen = new(Enabled ? ActiveColors.OnPrimary : ActiveColors.Background, 2f))
                    {
                        checkPen.StartCap = LineCap.Round;
                        checkPen.EndCap = LineCap.Round;
                        g.DrawLines(checkPen,
                        [
                            new Point(glyphRect.Left + 3, glyphRect.Top + glyphRect.Height / 2),
                            new Point(glyphRect.Left + glyphRect.Width / 2 - 1, glyphRect.Bottom - 3),
                            new Point(glyphRect.Right - 2, glyphRect.Top + 3)
                        ]);
                    }
                    break;
                case CheckState.Indeterminate:
                    using (SolidBrush fillBrush = new(Enabled ? ActiveColors.PrimaryPale : ActiveColors.Border))
                        g.FillRectangle(fillBrush, fillRect);
                    break;
                default:
                    using (SolidBrush fillBrush = new(ActiveColors.Card))
                        g.FillRectangle(fillBrush, fillRect);
                    break;
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
