using System;
using System.Collections.Generic;
using Printing.DataGridViewPrint;
using System.Drawing;

namespace FTAnalyzer.Utilities
{
    public class FooterPrintBlock : PrintBlock, IDisposable
    {
        readonly Font font = new("Tahoma", 9, GraphicsUnit.Point);

        public override SizeF GetSize(Graphics g, DocumentMetrics metrics) => g.MeasureString("Page X Of Y", font);


        public override void Draw(System.Drawing.Graphics g, Dictionary<CodeEnum, string> codes)
        {
            StringFormat format = new()
            {
                Trimming = StringTrimming.Word,
                FormatFlags = StringFormatFlags.NoWrap,
                Alignment = StringAlignment.Far
            };

            // as you can see below, we are using the codes param to know on which page we are for instance.
            g.DrawString(
                string.Format("Page {0} Of {1}", codes[CodeEnum.SheetNumber], codes[CodeEnum.SheetsCount]),
                font,
                new SolidBrush(Color.Black),
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
