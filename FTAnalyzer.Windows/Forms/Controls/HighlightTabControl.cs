using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public class HighlightTabControl : TabControl
    {
        public HighlightTabControl()
        {
            DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        // With SizeMode.Normal (variable-width tabs sized to each tab's own text), the native
        // control only remeasures tab widths against its current Font when its handle is
        // recreated - a runtime Font change alone doesn't retrigger that, leaving tabs sized for
        // whatever font was in effect when the handle was first created. RecreateHandle is
        // protected on Control, so expose it for MainForm to call after a font-scale change.
        public void RemeasureTabsForCurrentFont() => RecreateHandle();

        const int WM_ERASEBKGND = 0x0014;

        // DrawItem only paints each tab's own rectangle, so the native tab-strip background
        // beyond the last tab (and any margin around them) is left unpainted and shows the
        // system default color. Fill the whole control background ourselves first.
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                using System.Drawing.Graphics g = System.Drawing.Graphics.FromHdc(m.WParam);
                using SolidBrush brush = new(Theme.ActiveColors.Background);
                g.FillRectangle(brush, ClientRectangle);
                m.Result = 1;
                return;
            }
            base.WndProc(ref m);
        }

        // Ensures the designer calls our draw logic as well.
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            DrawHighlightTab(e);
        }

        void DrawHighlightTab(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= TabPages.Count)
                return;

            TabPage page = TabPages[e.Index];
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backColour = isSelected ? Theme.ActiveColors.Primary : Theme.ActiveColors.Background;
            Color textColour = isSelected ? Theme.ActiveColors.OnPrimary : Theme.ActiveColors.Text;
            Color borderColour = isSelected ? Theme.ActiveColors.Primary : Theme.ActiveColors.AccentWarm;

            using var backBrush = new SolidBrush(backColour);
            using var textBrush = new SolidBrush(textColour);
            using var borderPen = new Pen(borderColour);

            e.Graphics.FillRectangle(backBrush, e.Bounds);

            var borderRect = e.Bounds;
            borderRect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(borderPen, borderRect);

            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(page.Text, e.Font ?? DefaultFont, textBrush, e.Bounds, sf);
        }
    }
}