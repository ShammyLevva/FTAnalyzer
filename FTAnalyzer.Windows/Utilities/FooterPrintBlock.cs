using Printing.DataGridViewPrint;

namespace FTAnalyzer.Utilities
{
    public class FooterPrintBlock : PrintBlock, IDisposable
    {
        readonly Font font = new("Tahoma", 9, GraphicsUnit.Point);

        public override SizeF GetSize(System.Drawing.Graphics g, DocumentMetrics metrics) => g.MeasureString("Page X Of Y", font);


        public override void Draw(System.Drawing.Graphics g, Dictionary<CodeEnum, string> codes)
        {
            using StringFormat format = new()
            {
                Trimming = StringTrimming.Word,
                FormatFlags = StringFormatFlags.NoWrap,
                Alignment = StringAlignment.Far
            };

            using SolidBrush blackBrush = new(Color.Black);
            g.DrawString(
                string.Format("Page {0} Of {1}", codes[CodeEnum.SheetNumber], codes[CodeEnum.SheetsCount]),
                font,
                blackBrush,
                Rectangle,
                format);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                    font.Dispose();
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
