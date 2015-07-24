using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Printing.DataGridViewPrint.Tools
{
    public class PrintingDataGridViewProvider
    {
        IEnumerator<Partition> drawCursor;

        public GridDrawer _Drawer;
        public GridDrawer Drawer { get { return _Drawer; } }


        PrintPageEventHandler _PrintPageEventHandler;

        PrintDocument _PrintDocument;
        PrintDocument PrintDocument
        {
            get { return _PrintDocument; }
            set
            {
                if (value == null)
                    throw new NullReferenceException();
                if (_PrintDocument != null)
                    _PrintDocument.PrintPage -= _PrintPageEventHandler;
                _PrintDocument = value;
                _PrintDocument.PrintPage += _PrintPageEventHandler;
            }
        }

        public PrintingDataGridViewProvider(GridDrawer drawer, PrintDocument printDocument)
        {
            _PrintPageEventHandler = new PrintPageEventHandler(PrintDocument_PrintPage);

            _Drawer = drawer;
            
            this.PrintDocument = printDocument;
        }

        public static PrintingDataGridViewProvider Create(PrintDocument printDocument,
            DataGridView dgv, bool printLevelByLevel, bool mustCenterPartition, bool mustFitColumnsToPage,
            PrintBlock titlePrintBlock, PrintBlock sheetHeader, PrintBlock sheetFooter)
        {
            return new PrintingDataGridViewProvider(
                new GridDrawer(dgv, mustCenterPartition, mustFitColumnsToPage)
                {
                    SheetHeader = sheetHeader,
                    SheetFooter = sheetFooter,
                    TitlePrintBlock = titlePrintBlock,
                    MustPrintLevelByLevel = printLevelByLevel
                }, 
                printDocument);
        }


        void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (drawCursor == null)
            {
                Drawer.Initialize(e.Graphics, DocumentMetrics.FromPrintDocument(PrintDocument));

                drawCursor = Drawer.Partitions().GetEnumerator();

                if (!drawCursor.MoveNext())
                    throw new Exception("Nothing to print");
            }
            Drawer.DrawSheet(e.Graphics, drawCursor.Current);
            e.HasMorePages = drawCursor.MoveNext();

            if (!e.HasMorePages) drawCursor = null;
        }
    }
}
