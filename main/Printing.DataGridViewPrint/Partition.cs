using System.Collections.Generic;
using System.Windows.Forms;

namespace Printing.DataGridViewPrint
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// Represent a portion of a DataGridView which can be printed on one sheet
    /// </summary>
    public class Partition
    {
        
        
        DataGridView GridView { get; set; }

        PartitionBounds _Bounds;
        public PartitionBounds Bounds { get { return _Bounds; } }

        int _SheetNumber;
        public int SheetNumber { get { return _SheetNumber; } }




        public Partition(DataGridView dgv, int sheetNumber, PartitionBounds bounds)
        {
            this.GridView = dgv;
            _SheetNumber = sheetNumber;
            _Bounds = bounds;
        }



        /// <summary>
        /// Get the indexed column from zero to the last one to display on the partition
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataGridViewColumn GetColumn(int i)
        {
            return GridView.Columns[Bounds.StartColumnIndex + i];
        }

        /// <summary>
        /// Get every visible columns of the partition
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataGridViewColumn> GetColumns()
        {
            for (int i = 0; i < Bounds.ColumnCount; i++)
                if (this.Bounds.ColSelector(GetColumn(i))) yield return GetColumn(i);
        }

        /// <summary>
        /// Get the indexed row from zero to the last one to display on the partition
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataGridViewRow GetRow(int i)
        {
            return GridView.Rows[Bounds.StartRowIndex + i];
        }

        /// <summary>
        /// Get every visible rows of the partition
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataGridViewRow> GetRows()
        {
            for (int i = 0; i < Bounds.RowsCount; i++)
                if (this.Bounds.RowSelector(GetRow(i))) yield return GetRow(i);
        }

    }
}
