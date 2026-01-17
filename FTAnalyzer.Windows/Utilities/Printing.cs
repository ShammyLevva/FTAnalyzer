using System.Drawing.Printing;

namespace FTAnalyzer.Utilities
{
    class Printing(ScrollingRichTextBox rtb) : IDisposable
    {
        readonly ScrollingRichTextBox rtb = rtb;
        readonly StringReader reader = new(rtb.Text);

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                float LeftMargin = e.MarginBounds.Left;
                float TopMargin = e.MarginBounds.Top;
                string? Line = null;
                Font PrintFont = rtb.Font;
                if (PrintFont.SizeInPoints < 11)
                    PrintFont = new(PrintFont.FontFamily, 11f);
                int maxWidth = e.MarginBounds.Right - e.MarginBounds.Left;
                int maxHeight = e.MarginBounds.Bottom - e.MarginBounds.Top;
                float fontHeight = e.Graphics is null ? 0 : PrintFont.GetHeight(e.Graphics);
                using SolidBrush PrintBrush = new(Color.Black);
                float YPosition = TopMargin;
                while (YPosition < maxHeight && ((Line = reader.ReadLine()) is not null))
                {
                    SizeF sf = e.Graphics.MeasureString(Line, PrintFont, maxWidth);
                    e.Graphics.DrawString(Line, PrintFont, PrintBrush, new RectangleF(new PointF(LeftMargin, YPosition), sf), StringFormat.GenericTypographic);
                    YPosition += sf.Height;
                }
                e.HasMorePages = Line is not null;
            }
            catch (Exception) { }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                    reader?.Dispose();
            }
            catch (Exception) { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
