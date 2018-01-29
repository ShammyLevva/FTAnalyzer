using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.IO;
using System.Drawing;

namespace FTAnalyzer.Utilities
{
    class Printing : IDisposable
    {
        private ScrollingRichTextBox rtb;
        private StringReader reader;
        
        public Printing(ScrollingRichTextBox rtb)
        {
            this.rtb = rtb;
            reader = new StringReader(rtb.Text);
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            float LinesPerPage = 0;
            float YPosition = 0;
            int Count = 0;
            float LeftMargin = e.MarginBounds.Left;
            float TopMargin = e.MarginBounds.Top;
            string Line = null;
            Font PrintFont = this.rtb.Font;
            SolidBrush PrintBrush = new SolidBrush(Color.Black);

            LinesPerPage = e.MarginBounds.Height / PrintFont.GetHeight(e.Graphics);

            while (Count < LinesPerPage && ((Line = reader.ReadLine()) != null))
            {
                YPosition = TopMargin + (Count * PrintFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(Line, PrintFont, PrintBrush, LeftMargin, YPosition, new StringFormat());
                Count++;
            }

            if (Line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            PrintBrush.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                reader.Dispose();
                rtb.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
