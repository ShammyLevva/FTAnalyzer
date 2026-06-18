namespace FTAnalyzer.Forms.Controls
{
    class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            System.Drawing.Graphics g = e.Graphics;
            ToolStrip? gs = e.ToolStrip;
            ToolStripButton gsb = (ToolStripButton)e.Item;

            if (gsb.Checked)
            {
                int borderThickness = 2;
                Rectangle imageRectangle = new(borderThickness, borderThickness,
                    gsb.Width - 2 * borderThickness, gsb.Height - 2 * borderThickness);

                using SolidBrush lightBrush = new(Color.LightSteelBlue);
                g.FillRectangle(lightBrush, 0, 0, gsb.Width, gsb.Height);

                using SolidBrush steelBrush = new(Color.SteelBlue);
                g.FillRectangle(steelBrush, 0, 0, gsb.Width, imageRectangle.Top);
                g.FillRectangle(steelBrush, imageRectangle.Right, 0, gsb.Bounds.Right - imageRectangle.Right, imageRectangle.Bottom);
                g.FillRectangle(steelBrush, 0, 0, imageRectangle.Left, gsb.Height);
                g.FillRectangle(steelBrush, 0, imageRectangle.Bottom, gsb.Width, gsb.Bounds.Bottom - imageRectangle.Bottom);
            }
        }
    }
}
