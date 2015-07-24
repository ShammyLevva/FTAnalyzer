using System.Drawing.Printing;

namespace Printing.DataGridViewPrint
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// Usefull to keep the printable coordinates of a document
    /// </summary>
    public class DocumentMetrics
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int LeftMargin { get; set; }

        public int RightMargin { get; set; }

        public int TopMargin { get; set; }

        public int BottomMargin { get; set; }

        /// <summary>
        /// substract the margins from the document width
        /// </summary>
        public int PrintAbleWidth
        {
            get
            {
                return Width - LeftMargin - RightMargin;
            }
        }

        /// <summary>
        /// substract the margins from the document height
        /// </summary>
        public int PrintAbleHeight
        {
            get
            {
                return Height - TopMargin - BottomMargin;
            }
        }


        /// <summary>
        /// DocumentMetrics factory, take information in given PrintDocument object
        /// </summary>
        /// <param name="printDocument"></param>
        /// <returns>printable coordinates</returns>
        public static DocumentMetrics FromPrintDocument(PrintDocument printDocument)
        {
            PageSettings pageSettings = printDocument.DefaultPageSettings;
            
            return new DocumentMetrics()
            {
                Width = 
                    (pageSettings.Landscape)
                        ?pageSettings.PaperSize.Height:pageSettings.PaperSize.Width,
                Height = 
                    (pageSettings.Landscape)
                        ?pageSettings.PaperSize.Width:pageSettings.PaperSize.Height,
                LeftMargin = pageSettings.Margins.Left,
                TopMargin = pageSettings.Margins.Top,
                RightMargin = pageSettings.Margins.Right,
                BottomMargin = pageSettings.Margins.Bottom
            };
        }


    }
}
