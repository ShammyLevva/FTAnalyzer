using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Printing.DataGridViewPrint
{

    public enum CodeEnum
    {
        SheetNumber,
        SheetsCount,
        Date,
        Time
    }

    /// <author>Blaise Braye</author>
    /// <summary>
    /// GridDraw is a DataGridView Printing tool which try to help developper to print quickly and efficiently datas from grids
    /// </summary>
    public class GridDrawer
    {
        #region Fields

        static readonly Exception NotSynException = new Exception("Drawer is not synchronised with datagridview");

        private DataGridView _GridView;

        DocumentMetrics metrics;

        float scale;

        float columnHeaderHeight;
        float[] rowsHeights;
        float[] columnWidths;

        int[] firstRows;
        int[] firstColumns;

        Func<DataGridViewRow, bool> rowSelector;
        Func<DataGridViewColumn, bool> colSelector;

        Dictionary<int, IEnumerable<Partition>> levels;

        Dictionary<CodeEnum, string> codes;

        #endregion


        #region Properties

        /// <summary>
        /// This is the grid that the Drawer will print
        /// </summary>
        public DataGridView GridView
        {
            get { return _GridView; }
            set
            {
                if (value == null)
                    throw new Exception("GridView must not be null");
                IsInitialized = false;
                this._GridView = value;
            }
        }


        /// <summary>
        /// used by Drawer to know if it has been initialized
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// Define if you want to center grid on sheet?
        /// </summary>
        public bool MustCenterPartition { get; set; }

        /// <summary>
        /// Define if you want to ensure that grid won't be larger than printed sheet
        /// </summary>
        public bool MustFitColumnsToPage { get; set; }

        /// <summary>
        /// Define if you want to print level by level or not (partions columns by partitions columns)
        /// Default value: true
        /// </summary>
        public bool MustPrintLevelByLevel { get; set; }

        /// <summary>
        /// Define if you want to print selected columns only
        /// </summary>
        public bool MustPrintSelectedColumns { get; set; }

        /// <summary>
        /// Define if you want to print selected rows only
        /// </summary>
        public bool MustPrintSelectedRows { get; set; }


        /// <summary>
        /// Define a title block which appears on first sheet only, above the grid
        /// </summary>
        public PrintBlock TitlePrintBlock { get; set; }

        /// <summary>
        /// Define a footer which appears on each sheet
        /// </summary>
        public PrintBlock SheetFooter { get; set; }

        /// <summary>
        /// Define a header which appears on each sheet
        /// </summary>
        public PrintBlock SheetHeader { get; set; }


        /// <summary>
        /// /// Once Drawer has been initialized, return the number of levels to print
        /// </summary>
        public int LevelsCount
        {
            get { return firstRows.Length; }
        }

        /// <summary>
        /// /// Once Drawer has been initialized, return the number of partitions of one level
        /// </summary>
        public int PartitionsCount
        {
            get { return firstRows.Length; }
        }

        /// <summary>
        /// Once Drawer has been initialized, return the number of page to print
        /// </summary>
        public int SheetsCount
        {
            get { return LevelsCount * firstColumns.Length; }
        }
                


        #endregion

        #region Constructors

        /// <param name="dgv">GridView to manage</param>
        public GridDrawer(DataGridView dgv)
        {
            this.GridView = dgv;
            MustPrintLevelByLevel = true;
        }

        /// <param name="dgv">GridView to manage</param>
        /// <param name="mustCenterPartition">Do you want to center grid on sheet?</param>
        public GridDrawer(DataGridView dgv, bool mustCenterPartition)
            : this(dgv)
        {
            this.MustCenterPartition = mustCenterPartition;
        }


        /// <param name="dgv">GridView to manage</param>
        /// <param name="mustCenterPartition">Do you want to center grid on sheet?</param>
        /// <param name="mustFitColumnsToPage">Do you want to ensure that grid won't be larger than printed sheet</param>
        public GridDrawer(DataGridView dgv, bool mustCenterPartition, bool mustFitColumnsToPage)
            : this(dgv, mustCenterPartition)
        {
            this.MustFitColumnsToPage = mustFitColumnsToPage;
        }


        #endregion

        /// <summary>
        /// Make first calculations to know exactly what we can print on each sheet
        /// </summary>
        /// <param name="g"></param>
        /// <param name="metrics"></param>
        public void Initialize(Graphics g, DocumentMetrics metrics)
        {
            this.metrics = metrics;

            #region Take care of set blocks

            if (SheetHeader != null)
            {
                float headerHeight = SheetHeader.GetSize(g, metrics).Height;

                SheetHeader.Rectangle =
                    new RectangleF(metrics.LeftMargin, metrics.TopMargin, metrics.PrintAbleWidth, headerHeight);

                metrics.TopMargin += (int)Math.Ceiling(headerHeight);
            }

            if (TitlePrintBlock != null)
            {
                float titleHeight = TitlePrintBlock.GetSize(g, metrics).Height;

                TitlePrintBlock.Rectangle =
                    new RectangleF(metrics.LeftMargin, metrics.TopMargin, metrics.PrintAbleWidth, titleHeight);
            }

            if (SheetFooter != null)
            {
                float footerHeight = SheetFooter.GetSize(g, metrics).Height;

                metrics.BottomMargin += (int)Math.Ceiling(footerHeight);
                float footerY = metrics.TopMargin + metrics.PrintAbleHeight;

                SheetFooter.Rectangle =
                    new RectangleF(metrics.LeftMargin, footerY, metrics.PrintAbleWidth, footerHeight);
            }


            #endregion


            columnWidths = new float[GridView.Columns.Count];


            colSelector =
                new Func<DataGridViewColumn, bool>(column =>
                    (MustPrintSelectedColumns && column.DataGridView.SelectedColumns.Count > 0)
                    ? column.Selected
                    : column.Visible);


            float gridWidth = 0; 
            // calculate the grid width to know the difference with a sheet width,
            // it's usefull to determinate a scale
            foreach (var column in GridView.Columns.OfType<DataGridViewColumn>()
                .Where(colSelector))
            {
                columnWidths[column.Index] = column.Width(g);
                gridWidth += columnWidths[column.Index];
            }

            // we fix the scale to 1 if user doesn't need to zoom out the grid or if
            // the gridwidth is lower thant sheet width
            scale = (!MustFitColumnsToPage || gridWidth < metrics.PrintAbleWidth)
                ? 1 : metrics.PrintAbleWidth / gridWidth;


            columnHeaderHeight = GridView.HeaderHeight(g,scale);


            // let's go to set the partition splits
            List<int> firstColumnsOnPartition = new List<int>();

            float tmpWidth = 0;
            foreach (var column in GridView.Columns.OfType<DataGridViewColumn>()
                .Where(colSelector))
            {
                columnWidths[column.Index] *= scale;

                tmpWidth += columnWidths[column.Index];

                if (tmpWidth > metrics.PrintAbleWidth || column.Index == 0)
                {
                    firstColumnsOnPartition.Insert(0, column.Index);
                    tmpWidth = columnWidths[column.Index];
                }
            }
            firstColumnsOnPartition.Reverse();


            // let's go to set the level splits

            rowSelector =
                new Func<DataGridViewRow, bool>(row => 
                    (MustPrintSelectedRows && row.DataGridView.SelectedRows.Count>0) 
                    ? row.Selected 
                    : row.Visible);
            
            rowsHeights = new float[GridView.Rows.Count];
            List<int> firstRowsOnPartition = new List<int>();

            
            
            // we have to set the first visible row outside of the loop
            // cause we want to take care about the possible set Title block
            int firstVisibleRowIndex = GridView.Rows.OfType<DataGridViewRow>()
                    .Where(rowSelector).First().Index;
            firstRowsOnPartition.Insert(0, firstVisibleRowIndex);
            rowsHeights[firstVisibleRowIndex] = GridView.Rows[firstVisibleRowIndex].RowHeight(g);
            
            float tmpHeight = rowsHeights[firstVisibleRowIndex];

            if (TitlePrintBlock != null)
                tmpHeight += TitlePrintBlock.Rectangle.Height;

            // skip the first visible row cause it is already in firstrows (see above)
            foreach (var row in GridView.Rows.OfType<DataGridViewRow>()
                                    .Where(rowSelector).Skip(1)) 
            {
                rowsHeights[row.Index] = row.RowHeight(g,scale);
                tmpHeight += rowsHeights[row.Index];

                // warn to take care about the column headers
                if (tmpHeight >= metrics.PrintAbleHeight - columnHeaderHeight)
                {
                    firstRowsOnPartition.Insert(0, row.Index);
                    tmpHeight = rowsHeights[row.Index];
                }
            }
            firstRowsOnPartition.Reverse();

            // now that it is calculated, we can fix definitely firstRows and firstColumns in arrays
            firstRows = firstRowsOnPartition.ToArray();
            firstColumns = firstColumnsOnPartition.ToArray();

            
            levels = new Dictionary<int, IEnumerable<Partition>>(firstRows.Length);

            codes = new Dictionary<CodeEnum, string>();
            codes[CodeEnum.SheetsCount] = SheetsCount.ToString();
            codes[CodeEnum.Date] = DateTime.Now.ToShortDateString();
            codes[CodeEnum.Time] = DateTime.Now.ToShortTimeString();

            IsInitialized = true;
            
        }


        /// <summary>
        /// Get each partitions to print, take care of the MustPrintLevelByLevel property value
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Partition> Partitions()
        {
            for (int levelIndex = 1; levelIndex <= firstRows.Length; levelIndex++)
                foreach (var partition in (MustPrintLevelByLevel)?PartitionsByLevel(levelIndex):PartitionsByColumn(levelIndex))
                    yield return partition;
        }


        


        /// <summary>
        /// Get partitions of one printable level
        /// </summary>
        /// <param name="indexLevel">level from which user needs partitions</param>
        /// <returns></returns>
        public IEnumerable<Partition> PartitionsByLevel(int indexLevel)
        {
            if (!IsInitialized) throw NotSynException;

            int firstRow = firstRows[indexLevel - 1];
            int lastRow = (indexLevel >= firstRows.Length) ?
                                rowsHeights.Length - 1 // last dgv row index
                                : firstRows[indexLevel] - 1;

            float height = rowsHeights.Skip(firstRow).Take(lastRow + 1 - firstRow).Sum();


            // initialization of each partition of the current level [indexLevel]
            for (int partitionLevel = 0; partitionLevel < firstColumns.Length; partitionLevel++)
            {
                int firstColumn = firstColumns[partitionLevel];
                int lastColumn = (partitionLevel + 1 == firstColumns.Length) ?
                                    columnWidths.Length - 1 // last dgv column index
                                    : firstColumns[partitionLevel + 1] - 1; // next first column - 1

                float width = columnWidths.Skip(firstColumn).Take(lastColumn + 1 - firstColumn).Sum();

                int pageNumber = ((indexLevel - 1) * PartitionsCount + partitionLevel) + 1;

                yield return CreatePartition(
                    indexLevel, partitionLevel, pageNumber,
                    firstRow, lastRow, firstColumn, lastColumn, 
                    metrics.LeftMargin, metrics.TopMargin, width, height);

            }
        }

        /// <summary>
        /// Get [partitionLevel]th partitions of each levels
        /// </summary>
        /// <param name="partitionLevel">[partitionLevel]th partitions of each levels</param>
        /// <returns></returns>
        public IEnumerable<Partition> PartitionsByColumn(int partitionLevel)
        {
            if (!IsInitialized) throw NotSynException;

            int firstColumn = firstColumns[partitionLevel-1];
            int lastColumn = (partitionLevel == firstColumns.Length) ?
                                columnWidths.Length - 1 // last dgv column index
                                : firstColumns[partitionLevel] - 1; // next first column - 1

            float width = columnWidths.Skip(firstColumn).Take(lastColumn + 1 - firstColumn).Sum();

            for (int indexLevel = 0; indexLevel < firstRows.Length; indexLevel++)
            {
                int firstRow = firstRows[indexLevel];
                int lastRow = (indexLevel + 1 == firstRows.Length) ?
                                    rowsHeights.Length - 1 // last dgv row index
                                    : firstRows[indexLevel + 1] - 1;

                float height = rowsHeights.Skip(firstRow).Take(lastRow + 1 - firstRow).Sum();

                int pageNumber = ((partitionLevel - 1) * LevelsCount + indexLevel) + 1;

                yield return CreatePartition(
                    indexLevel, partitionLevel, pageNumber,
                    firstRow, lastRow, firstColumn, lastColumn,
                    metrics.LeftMargin, metrics.TopMargin, width, height);

            }

        }


        Partition CreatePartition(
            int indexLevel, int partitionLevel, int pageNumber, 
            int firstRow, int lastRow, int firstColumn,  int lastColumn,
            float startx, float starty, float width, float height)
        {

            if (MustCenterPartition)
                startx += (metrics.PrintAbleWidth - width) / 2.0F;

            PartitionBounds bounds = new PartitionBounds()
            {
                ColSelector = colSelector,
                RowSelector = rowSelector,
                StartRowIndex = firstRow,
                EndRowIndex = lastRow,
                StartColumnIndex = firstColumn,
                EndColumnIndex = lastColumn,
                StartX = startx,
                StartY = starty,
                Width = width,
                Height = height
            };

            if (indexLevel == 1 && TitlePrintBlock != null)
                bounds.StartY += TitlePrintBlock.Rectangle.Height;



            return new Partition(
                    GridView,
                    pageNumber,
                    bounds);
        }


        /// <summary>
        /// Print a sheet containing the given partition
        /// </summary>
        /// <param name="g"></param>
        /// <param name="partition"></param>
        public void DrawSheet(Graphics g, Partition partition)
        {
            codes[CodeEnum.SheetNumber] = partition.SheetNumber.ToString();

            if (SheetHeader != null) // print header if it has been set
                SheetHeader.Draw(g, codes);

            // print report title on first page if a title block has been set
            if (partition.SheetNumber == 1 && TitlePrintBlock != null)
                TitlePrintBlock.Draw(g, codes);

            float currentY = DrawColumnHeaders(g, partition);
            DrawRows(g, partition, currentY);

            if (SheetFooter != null) // print footer if it has been set
                SheetFooter.Draw(g, codes);

        }
  

        /// <summary>
        /// Draw Grid column headers from partition
        /// </summary>
        /// <param name="g"></param>
        /// <param name="partition">Partition to print</param>
        /// <returns></returns>
        private float DrawColumnHeaders(Graphics g, Partition partition)
        {
            PartitionBounds bounds = partition.Bounds;

            float currentY = bounds.StartY;
            float currentX = bounds.StartX;



            // Setting the LinePen that will be used to draw lines and rectangles (derived from the GridColor property of the DataGridView control)
            Pen linePen = new Pen(GridView.GridColor, 1);

            // Calculating and drawing the HeaderBounds        
            RectangleF HeaderBounds = new RectangleF(currentX, currentY, bounds.Width, columnHeaderHeight);

            g.FillRectangle(
                new SolidBrush(GridView.ColumnsHeaderBackColor()),
                HeaderBounds);


            // Setting the format that will be used to print each cell of the header row
            StringFormat CellFormat = new StringFormat();
            CellFormat.Trimming = StringTrimming.Word;
            CellFormat.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;



            // Printing each visible cell of the header row
            foreach (var column in partition.GetColumns())
            {
                DataGridViewHeaderCell cell = column.HeaderCell;

                CellFormat.Alignment = cell.HorizontalAlignement();


                RectangleF CellBounds = new RectangleF(currentX, currentY, columnWidths[column.Index], columnHeaderHeight);

                // Printing the cell text
                g.DrawString(column.HeaderText,
                    cell.Font(scale),
                    new SolidBrush(cell.ForeColor()),
                    CellBounds, 
                    CellFormat);


                // Drawing the cell bounds
                if (GridView.RowHeadersBorderStyle != DataGridViewHeaderBorderStyle.None) // Draw the cell border only if the HeaderBorderStyle is not None
                    g.DrawRectangle(linePen, currentX, currentY, columnWidths[column.Index], columnHeaderHeight);


                currentX += columnWidths[column.Index];

            }

            return currentY + columnHeaderHeight;
        }


        /// <summary>
        /// Draw Grid rows from given partition
        /// </summary>
        /// <param name="g"></param>
        /// <param name="partition">Partition to print</param>
        /// <param name="currentY">Starting Y coordinate</param>
        private void DrawRows(Graphics g, Partition partition, float currentY)
        {
            PartitionBounds bounds = partition.Bounds;

            // Setting the LinePen that will be used to draw lines and rectangles
            Pen linePen = new Pen(GridView.GridColor, 1);

            // Setting the format that will be used to print each cell
            StringFormat CellFormat = new StringFormat();
            CellFormat.Trimming = StringTrimming.Word;
            CellFormat.FormatFlags = StringFormatFlags.LineLimit;


            foreach (var row in partition.GetRows())
            {
                float rowHeight = rowsHeights[row.Index];


                // Setting the RowFore style
                SolidBrush RowForeBrush = new SolidBrush(row.ForeColor());


                // Calculating the starting x coordinate that the printing process will start from
                float currentX = bounds.StartX;



                // Calculating the entire CurrentRow bounds                
                RectangleF RowBounds = new RectangleF(currentX, currentY, bounds.Width, rowHeight);

                g.FillRectangle(new SolidBrush(row.BackColor()), RowBounds);

                foreach (var column in partition.GetColumns().OrderBy(x => x.DisplayIndex))
                {
                    var cell = row.Cells[column.Index];

                    CellFormat.Alignment = cell.HorizontalAlignement();

                    RectangleF CellBounds = new RectangleF(currentX, currentY, columnWidths[column.Index], rowHeight);

                    if (cell is DataGridViewImageCell)
                    {
                        Bitmap bm = cell.Value as Bitmap;
                        PointF pos = new PointF(currentX + (columnWidths[column.Index] - bm.Width) / 2,
                            currentY + (rowHeight - bm.Height) / 2);
                        g.DrawImage(bm, pos);
                    }
                    else
                    {
                        // Printing the cell text
                        g.DrawString(
                            cell.EditedFormattedValue.ToString(),
                            cell.Font(scale),
                            new SolidBrush(row.ForeColor()),
                            CellBounds,
                            CellFormat);
                    }
                    // Drawing the cell bounds
                    if (GridView.CellBorderStyle != DataGridViewCellBorderStyle.None) // Draw the cell border only if the CellBorderStyle is not None
                        g.DrawRectangle(linePen, currentX, currentY, columnWidths[column.Index], rowHeight);

                    currentX += columnWidths[column.Index];
                }

                currentY += rowHeight;
            }
        }

    }
}
