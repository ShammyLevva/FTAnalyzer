namespace FTAnalyzer.Forms.Controls
{
    // A standard WinForms ProgressBar is drawn entirely by the OS visual-style handler, which
    // ignores BackColor/ForeColor even after SetWindowTheme opts the control's HWND out of
    // theming (PBM_SETBKCOLOR/PBM_SETBARCOLOR are only honoured by the classic, pre-XP renderer
    // that modern Windows no longer falls back to) - so it can never be recoloured for dark mode.
    // Owner-drawing it ourselves sidesteps the problem entirely.
    public class ThemedProgressBar : Control
    {
        int minimum;
        int maximum = 100;
        int value;

        public ThemedProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public int Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                if (this.value < minimum)
                    this.value = minimum;
                Invalidate();
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                if (this.value > maximum)
                    this.value = maximum;
                Invalidate();
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public int Value
        {
            get => value;
            set
            {
                this.value = Math.Clamp(value, minimum, maximum);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using SolidBrush backBrush = new(BackColor);
            e.Graphics.FillRectangle(backBrush, ClientRectangle);

            int range = Math.Max(1, maximum - minimum);
            double fraction = (double)(value - minimum) / range;
            int fillWidth = (int)(ClientRectangle.Width * fraction);
            if (fillWidth > 0)
            {
                using SolidBrush foreBrush = new(ForeColor);
                e.Graphics.FillRectangle(foreBrush, new Rectangle(0, 0, fillWidth, ClientRectangle.Height));
            }

            using Pen borderPen = new(Theme.ActiveColors.Border);
            e.Graphics.DrawRectangle(borderPen, 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
        }
    }
}
