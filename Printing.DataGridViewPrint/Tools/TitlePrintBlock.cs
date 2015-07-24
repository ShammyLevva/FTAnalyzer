using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Printing.DataGridViewPrint.Tools
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// Reusable Sample of a title to print on first printed sheet
    /// </summary>
    public class TitlePrintBlock : PrintBlock
    {

        public String Title { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }
        public StringFormat Format { get; set; }
        
        public TitlePrintBlock()
        {
            Format = new StringFormat();
            Format.LineAlignment = StringAlignment.Center;
            Format.Alignment = StringAlignment.Center;
            Format.Trimming = StringTrimming.Word;
            Format.FormatFlags = StringFormatFlags.LineLimit;
            this.Font = new Font("Tahoma", 14, FontStyle.Bold | FontStyle.Underline);
            this.Title = "this is a sample title";
            this.ForeColor = Color.Black;
        }


        public TitlePrintBlock(String title)
            : this()
        {
            this.Title = title;
        }

        public TitlePrintBlock(String title, Color foreColor)
            : this(title)
        {
            this.ForeColor = foreColor;
        }

        public TitlePrintBlock(String title, Color foreColor, Font font)
            : this(title, foreColor)
        {
            this.Font = font;
        }

        public TitlePrintBlock(String title, Color foreColor, Font font, StringFormat format)
            : this(title, foreColor, font)
        {
            this.Format = format;
        }


        public override SizeF GetSize(Graphics g, DocumentMetrics metrics)
        {
            return g.MeasureString(Title, Font, metrics.PrintAbleWidth, Format);
        }

        public override void Draw(Graphics g, Dictionary<CodeEnum, string> codes)
        {
            g.DrawString(Title, Font, new SolidBrush(ForeColor), Rectangle, Format);
        }

    }
}
