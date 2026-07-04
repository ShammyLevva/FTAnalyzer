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

            Color backColour = isSelected ? SystemColors.Highlight : SystemColors.Control;
            Color textColour = isSelected ? SystemColors.HighlightText : SystemColors.ControlText;
            Color borderColour = isSelected ? SystemColors.Highlight : SystemColors.ControlDark;

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