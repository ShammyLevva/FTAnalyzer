using FTAnalyzer.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.Theme
{
    // TabControl always paints some native chrome around each tab - a solid border box under
    // TabAppearance.Normal, separator lines under FlatButtons - regardless of DrawMode/Appearance,
    // and that chrome ignores our colors entirely (confirmed: even with the owner-draw border
    // pen set to a near-black color, the border stayed bright white). Owner-draw only lets us
    // paint each tab's interior; the surrounding decoration is applied by the native control
    // afterwards and can't be recolored or suppressed via any property.
    //
    // So this class stops asking the native control to paint at all: WM_PAINT is intercepted and
    // swallowed completely, and the whole tab strip (plus the page-frame area, which had its own
    // native border for the same reason) is drawn from scratch here. TabControl still owns page
    // layout/switching via SelectedIndex/TabPages - only its rendering is bypassed.
    public class HighlightTabControl : TabControl
    {
        const int WM_ERASEBKGND = 0x0014;
        const int WM_PAINT = 0x000F;

        // With SizeMode.Normal (variable-width tabs sized to each tab's own text), the native
        // control only remeasures tab widths against its current Font when its handle is
        // recreated - a runtime Font change alone doesn't retrigger that, leaving tabs sized for
        // whatever font was in effect when the handle was first created. RecreateHandle is
        // protected on Control, so expose it for MainForm to call after a font-scale change.
        public void RemeasureTabsForCurrentFont() => RecreateHandle();

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                // Painting happens entirely in WM_PAINT below; treat the background as already
                // handled so Windows doesn't separately flash the native background color first.
                m.Result = 1;
                return;
            }
            if (m.Msg == WM_PAINT)
            {
                PaintControl();
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        void PaintControl()
        {
            using System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(Handle);
            using SolidBrush backBrush = new(ActiveColors.Background);
            g.FillRectangle(backBrush, ClientRectangle);

            for (int index = 0; index < TabCount; index++)
                DrawTab(g, index);

            // No BeginPaint/EndPaint cycle happened (we painted via a plain window DC), so the
            // update region is still marked invalid unless we clear it ourselves - otherwise
            // Windows immediately re-posts WM_PAINT in a tight loop.
            NativeMethods.ValidateRect(this);
        }

        void DrawTab(System.Drawing.Graphics g, int index)
        {
            TabPage page = TabPages[index];
            bool isSelected = index == SelectedIndex;
            Rectangle bounds = GetTabRect(index);

            Color backColour = isSelected ? ActiveColors.Primary : ActiveColors.Background;
            Color textColour = isSelected ? ActiveColors.OnPrimary : ActiveColors.Text;
            // AccentWarm is a mid-tone in both palettes, so it under-contrasts against light
            // mode's pale background (the intended subtle look) but over-contrasts against dark
            // mode's near-black background (reads as a bright/white outline). Border is designed
            // to be subtle against either theme's own background, so use that instead.
            Color borderColour = isSelected ? ActiveColors.Primary : ActiveColors.Border;

            using SolidBrush backBrush = new(backColour);
            using SolidBrush textBrush = new(textColour);
            // Unselected tabs share their fill color with the page background (only the border
            // separates one tab from the next), so a 1px line was too subtle to read as a
            // boundary - widen it there. Selected tabs already stand out via their fill color, so
            // leave that border at its original weight.
            using Pen borderPen = new(borderColour, isSelected ? 1f : 2f);

            g.FillRectangle(backBrush, bounds);

            Rectangle borderRect = bounds;
            borderRect.Inflate(-1, -1);
            g.DrawRectangle(borderPen, borderRect);

            using StringFormat sf = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(page.Text, Font, textBrush, bounds, sf);
        }
    }
}
