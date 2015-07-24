using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Printing.DataGridViewPrint
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// usefull extentions to DataGridView components
    /// </summary>
    public static class DataGridViewExtentions
    {

        /// <returns>affected Font to the cell</returns>
        public static Font Font(this DataGridViewCell cell)
        {
            return (cell.HasStyle && cell.Style.Font != null) ?
                cell.Style.Font : cell.InheritedStyle.Font;
        }

        /// <summary>
        /// Multiply the cell font with scale
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="scale">percentage of font size</param>
        /// <returns>Scaled Font</returns>
        public static Font Font(this DataGridViewCell cell, float scale)
        {
            Font font = Font(cell);

            if (scale != 1)
                font = new Font(font.FontFamily,font.Size * scale);

            return font;
        }



        /// <summary>
        /// Get the forecolor of the cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>ForeColor of the cell</returns>
        public static Color ForeColor(this DataGridViewCell cell)
        {
            if (cell.HasStyle && cell.Style.ForeColor != Color.Empty) return cell.Style.ForeColor;
            else return cell.InheritedStyle.ForeColor;
        }

        /// <summary>
        /// Get the BackColor of the cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>BackColor of the cell</returns>
        public static Color BackColor(this DataGridViewCell cell)
        {
            if (cell.HasStyle && cell.Style.BackColor != Color.Empty) return cell.Style.BackColor;
            else return cell.InheritedStyle.BackColor;
        }

        /// <summary>
        /// Get the BackColor of the column headers of the gridview
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns>BackColor of the cell</returns>
        public static Color ColumnsHeaderBackColor(this DataGridView gridView){
            return (!gridView.ColumnHeadersDefaultCellStyle.BackColor.IsEmpty) 
                ? gridView.ColumnHeadersDefaultCellStyle.BackColor : gridView.DefaultCellStyle.BackColor;
        }

        /// <summary>
        /// Get the forecolor of the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>ForeColor of the row</returns>
        public static Color ForeColor(this DataGridViewRow row)
        {
            if (row.HasDefaultCellStyle && row.DefaultCellStyle.ForeColor != Color.Empty)
                return row.DefaultCellStyle.ForeColor;
            else return row.InheritedStyle.ForeColor;
        }

        /// <summary>
        /// Get the BackColor of the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>BackColor of the row</returns>
        public static Color BackColor(this DataGridViewRow row)
        {
            if (row.Index % 2 == 0 || row.DataGridView.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty)
            {
                if (row.HasDefaultCellStyle && row.DefaultCellStyle.BackColor != Color.Empty)
                    return row.DefaultCellStyle.BackColor;
                else return row.InheritedStyle.BackColor;
            }
            else
            {
                return row.DataGridView.AlternatingRowsDefaultCellStyle.BackColor;
            }

        }

        /// <summary>
        /// Get text horizontal alignement in given cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>text horizontal alignement in given cell</returns>
        public static StringAlignment HorizontalAlignement(this DataGridViewCell cell)
        {

            DataGridViewContentAlignment alignement = DataGridViewContentAlignment.TopLeft;

            if (cell.HasStyle && cell.Style.Alignment != DataGridViewContentAlignment.NotSet)
                alignement = cell.Style.Alignment;
            else alignement = cell.InheritedStyle.Alignment;

            // Check the CurrentCell alignment and apply it to the CellFormat
            if (alignement.ToString().Contains("Right"))
                return StringAlignment.Far;
            else if (alignement.ToString().Contains("Center"))
                return StringAlignment.Center;
            else
                return StringAlignment.Near;
        }


        /// <summary>
        /// Calculate the height of a DataGridViewRow in a drawing surface,
        /// it doesn't make calculation with hidden cells
        /// </summary>
        /// <param name="row"></param>
        /// <param name="g"></param>
        /// <returns>row height</returns>
        public static float RowHeight(this DataGridViewRow row, Graphics g)
        {
            return (from cell in row.Cells.OfType<DataGridViewCell>()
                    where cell.Visible
                    select g.MeasureString(
                         cell.EditedFormattedValue.ToString(),
                         cell.Font(),
                         cell.Size.Width).Height).Max();
        }

        /// <summary>
        /// Multiply row height with scale value
        /// </summary>
        /// <param name="row"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <returns>Scaled height</returns>
        public static float RowHeight(this DataGridViewRow row, Graphics g, float scale)
        {
            return RowHeight(row, g) * scale;
        }

        /// <summary>
        /// Calculate the height of a cell in a drawing surface
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="g"></param>
        /// <returns>cell row height</returns>
        public static float RowHeight(this DataGridViewCell cell, Graphics g)
        {
            return cell.OwningRow.RowHeight(g);
        }

        /// <summary>
        /// Multiply cell height with scale value
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <returns>Scaled cell height</returns>
        public static float RowHeight(this DataGridViewCell cell, Graphics g, float scale)
        {
            return RowHeight(cell, g) * scale;
        }

        /// <summary>
        /// Calculate the header height of a DataGridView in a drawing surface,
        /// it takes care only about visible columns
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="g"></param>
        /// <returns>DataGridView Columns Header Height</returns>
        public static float HeaderHeight(this DataGridView dgv, Graphics g)
        {
            return (from column in dgv.Columns.OfType<DataGridViewColumn>()
                    where column.Visible
                    select g.MeasureString(
                         column.HeaderCell.EditedFormattedValue.ToString(),
                         column.HeaderCell.Font(),
                         column.HeaderCell.Size.Width).Height).Max();
        }

        /// <summary>
        /// Multiply DataGridView Columns Header Height with scale value 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <returns>Scaled DataGridView Columns Header Height</returns>
        public static float HeaderHeight(this DataGridView dgv, Graphics g, float scale)
        {
            return HeaderHeight(dgv, g) * scale;
        }

        /// <summary>
        /// Calculate the width of a column in a drawing surface,
        /// it doesn't make calculation with hidden cells
        /// </summary>
        /// <param name="column"></param>
        /// <param name="g"></param>
        /// <returns>Column width</returns>
        public static float Width(this DataGridViewColumn column, Graphics g)
        {
            var maxCellsWidth = 
                (from row in column.DataGridView.Rows.OfType<DataGridViewRow>()
                 where row.Visible
                 select g.MeasureString(
                        row.Cells[column.Index].EditedFormattedValue.ToString(),
                        row.Cells[column.Index].Font(),
                        row.Cells[column.Index].Size.Width).Width
                ).Max();

            return Math.Max(
                        maxCellsWidth,
                        g.MeasureString(
                            column.HeaderText,
                            column.HeaderCell.Font(),
                            column.HeaderCell.Size.Width).Width);
        }

        /// <summary>
        /// Multiply column width with scale value
        /// </summary>
        /// <param name="column"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <returns>Scaled column width</returns>
        public static float Width(this DataGridViewColumn column, Graphics g, float scale)
        {
            return Width(column, g) * scale;
        }

        /// <summary>
        /// Calculate the width of a DataGridViewCell in a drawing surface
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="g"></param>
        /// <returns>Cell width</returns>
        public static float ColumnWidth(this DataGridViewCell cell, Graphics g)
        {
            return cell.OwningColumn.Width(g);
        }
        /// <summary>
        /// Multiply cell width with scale value
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        /// <returns>Scaled cell width</returns>
        public static float ColumnWidth(this DataGridViewCell cell, Graphics g, float scale)
        {
            return ColumnWidth(cell, g) * scale;
        }

    }
}
