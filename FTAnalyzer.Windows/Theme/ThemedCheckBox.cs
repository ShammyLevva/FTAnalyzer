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

            // Fixed at 13px regardless of the user's font-scale setting used to read fine at the
            // default (~8pt) level, but FontScaler.Apply can grow Font up to 14pt - the glyph
            // stayed pinned at its smallest size while the label text next to it kept growing, so
            // the checkbox looked disproportionately small at higher scale levels. Size it off the
            // current Font instead, floored at the original 13px for the default scale (same fix as
            // ThemedRadioButton).
            int diameter = Math.Max(13, Font.Height - 2);
            Size glyphSize = new(diameter, diameter);
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
                        // These insets were hand-tuned pixel offsets for the original fixed 12px
                        // box; scale them with glyphRect now that it grows with Font, or the
                        // checkmark stays pinned at its smallest size and reads as a thin sliver
                        // inside a larger square.
                        int narrowInset = Math.Max(2, glyphRect.Width / 6);
                        int wideInset = Math.Max(3, glyphRect.Width / 4);
                        g.DrawLines(checkPen,
                        [
                            new Point(glyphRect.Left + wideInset, glyphRect.Top + glyphRect.Height / 2),
                            new Point(glyphRect.Left + glyphRect.Width / 2 - 1, glyphRect.Bottom - wideInset),
                            new Point(glyphRect.Right - narrowInset, glyphRect.Top + wideInset)
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
