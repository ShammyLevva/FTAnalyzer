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