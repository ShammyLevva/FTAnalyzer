
using System;
using System.Windows.Forms;
namespace Printing.DataGridViewPrint
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// Bounds of a partition, it covers mostly included rows and columns
    /// but also cordinates information about it (size,..)
    /// </summary>
    public class PartitionBounds
    {
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }

        public int StartColumnIndex { get; set; }
        public int EndColumnIndex { get; set; }

        public float StartX { get; set; }
        public float StartY { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public Func<DataGridViewRow, bool> RowSelector { get; set; }
        public Func<DataGridViewColumn, bool> ColSelector{ get; set; }

        public int ColumnCount
        {
            get { return 1 + EndColumnIndex - StartColumnIndex; }
        }

        public int RowsCount
        {
            get { return 1 + EndRowIndex - StartRowIndex; }
        }
    }
}
