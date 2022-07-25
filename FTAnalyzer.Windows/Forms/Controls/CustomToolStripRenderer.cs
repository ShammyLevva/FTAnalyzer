using System.Windows.Forms;
using System.Drawing;

namespace FTAnalyzer.Forms.Controls
{
    class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            Graphics g = e.Graphics;
            ToolStrip gs = e.ToolStrip;
            ToolStripButton gsb = (ToolStripButton)e.Item;

            if (gsb.Checked)
            {
                int borderThickness = 2;
                Rectangle imageRectangle = new Rectangle(borderThickness, borderThickness,
                    gsb.Width - 2 * borderThickness, gsb.Height - 2 * borderThickness);

                Brush b = new SolidBrush(Color.LightSteelBlue);
                g.FillRectangle(b, 0, 0, gsb.Width, gsb.Height);

                b = new SolidBrush(Color.SteelBlue);
                g.FillRectangle(b, 0, 0, gsb.Width, imageRectangle.Top);
                g.FillRectangle(b, imageRectangle.Right, 0, gsb.Bounds.Right - imageRectangle.Right, imageRectangle.Bottom);
                g.FillRectangle(b, 0, 0, imageRectangle.Left, gsb.Height);
                g.FillRectangle(b, 0, imageRectangle.Bottom, gsb.Width, gsb.Bounds.Bottom - imageRectangle.Bottom);
            }
        }
    }
}
