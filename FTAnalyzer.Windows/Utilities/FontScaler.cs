using FTAnalyzer.Properties;

namespace FTAnalyzer.Utilities
{
    /// <summary>
    /// Applies the user's chosen <see cref="FontScale"/> level to every control in a form's
    /// control tree. This is the single owned replacement for the old
    /// FTAnalyzer.Shared SpecialMethods.SetFonts/GetAllControls pair (moved here because
    /// they are WinForms-only and Shared is meant to hold business logic shared with the
    /// web project, which never used them).
    /// </summary>
    public static class FontScaler
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FontScaler));

        public static void Apply(Control root)
        {
            // Suspend painting for the whole batch instead of letting each of the (potentially
            // hundreds of) child controls below repaint itself individually - visiting a whole
            // form's control tree one Refresh() call at a time produced visible flicker.
            NativeMethods.SuspendDrawing(root);
            try
            {
                FontScaleLevel level = FontScale.ForLevel(FontSettings.Default.FontNumber);
                foreach (Control control in GetAllControls(root))
                    ApplyToControl(control, level);
            }
            catch (Exception e)
            {
                log.Error($"Error applying font scale to '{root.Name}'", e);
            }
            finally
            {
                NativeMethods.ResumeDrawing(root);
                root.Invalidate(true);
            }
        }

        static void ApplyToControl(Control control, FontScaleLevel level)
        {
            control.Font = new(control.Font.FontFamily, level.FontSize, control.Font.Style);
            if (control is DataGridView grid)
            {
                grid.ColumnHeadersDefaultCellStyle.Font = new(grid.ColumnHeadersDefaultCellStyle.Font?.FontFamily ?? grid.Font.FontFamily, level.FontSize, FontStyle.Bold);
                grid.DefaultCellStyle.Font = new(grid.DefaultCellStyle.Font?.FontFamily ?? grid.Font.FontFamily, level.FontSize, grid.DefaultCellStyle.Font?.Style ?? FontStyle.Regular);
                grid.RowTemplate.Height = level.FontHeight;
                // Bold headers render wider than regular ones at the same point size, so a column
                // width that fit a regular-weight header can now clip it. Widen (never shrink) each
                // column just enough to fit its own header text, without touching cell-content-based
                // widths a user may have already resized.
                try
                {
                    foreach (DataGridViewColumn column in grid.Columns.Cast<DataGridViewColumn>())
                        FitColumnToHeader(column);
                }
                catch (Exception e)
                {
                    log.Error($"Error widening columns for '{grid.Name}'", e);
                }
            }
        }

        /// <summary>
        /// Widens (never shrinks) a single column just enough to fit its own header text at the
        /// grid's current header font. Several forms re-fit column widths from cell content whenever
        /// a grid is (re)populated (e.g. <c>GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true)</c>),
        /// which can override the bold-header widening <see cref="Apply"/> already did - call this
        /// again after any such repopulation to restore the header-fit floor.
        /// </summary>
        public static void FitColumnToHeader(DataGridViewColumn column)
        {
            Font? headerFont = column.DataGridView?.ColumnHeadersDefaultCellStyle.Font ?? column.HeaderCell.InheritedStyle.Font;
            if (headerFont is null)
                return;
            int headerWidth = TextRenderer.MeasureText(column.HeaderText, headerFont).Width + 24; // padding for the sort glyph/margins
            if (headerWidth > column.Width)
                column.Width = headerWidth;
        }

        internal static IEnumerable<Control> GetAllControls(Control root)
        {
            Stack<Control> stack = new();
            stack.Push(root);
            while (stack.Count != 0)
            {
                Control next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);
                yield return next;
            }
        }
    }
}
