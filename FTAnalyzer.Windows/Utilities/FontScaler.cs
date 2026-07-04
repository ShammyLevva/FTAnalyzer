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
            }
        }

        static IEnumerable<Control> GetAllControls(Control root)
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
