using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FTAnalyzer.Graphics
{
    public static class GraphicsUtilities
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = System.Drawing.Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }

        public static Bitmap ResizeImage(Image image, decimal percentage)
        {
            int width = (int)(image.Width * percentage);
            int height = (int)(image.Height * percentage);
            return ResizeImage(image, width, height);
        }

        public static Bitmap ResizeImageToCurrentScale(Image image) => ResizeImage(image, (decimal)GetCurrentScaling());

        public static float GetCurrentScaling()
        {
            float dx;
            using (System.Drawing.Graphics g = Application.OpenForms[0].CreateGraphics())
            {
                dx = g.DpiX;
            }
            return dx / 96;
        }

        public static void DrawGroupBox(GroupBox box, System.Drawing.Graphics g, Color textColor, Color borderColor, float borderWidth)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new(borderBrush, borderWidth);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new(box.ClientRectangle.X,
                                     box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                     box.ClientRectangle.Width - 1,
                                     box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Draw text
                //g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

    }
}
