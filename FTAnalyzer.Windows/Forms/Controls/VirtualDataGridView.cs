using FTAnalyzer.Utilities;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Zuby.ADGV;

namespace FTAnalyzer.Forms.Controls
{
    // Non-generic marker so callers (e.g. MainForm's theme-toggle handler) can find every grid
    // via a plain control-tree walk without needing VirtualDataGridView<T>'s type argument.
    interface IReapplyTheme
    {
        void ReapplyTheme();
    }

    [ComplexBindingProperties()]
    abstract class VirtualDataGridView<T> : AdvancedDataGridView, IReapplyTheme
    {
        const string SourceIndexColumn = "__SourceIndex__";
        internal SortableBindingList<T> _dataSource;
        internal SortableBindingList<T> _fulllist;
        public string FilterCountText { get; private set; } = string.Empty;

        protected VirtualDataGridView()
        {
            _dataSource = [];
            _fulllist = [];
            VirtualMode = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = true;
            AutoGenerateColumns = false;
          
            // Every column already gets an explicit width from [ColumnDetail], so autosizing
            // isn't needed - and DisplayedCells continuously recalculates widths from cell
            // content, which was fighting the header's own button-space-widening logic (causing
            // header text truncation) and silently disabling manual column resize despite
            // AllowUserToResizeColumns above (per MSDN, AutoSizeColumnsMode != None overrides
            // a column's Resizable behavior).
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Dock = DockStyle.Fill;
            Location = new Point(6, 6);
            Margin = new Padding(6, 6, 6, 6);
            MultiSelect = false;
            ReadOnly = true;
            ResizeRedraw = true;
            RowHeadersVisible = false;
            RowHeadersWidth = 50;
            FilterAndSortEnabled = true;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ScrollBars = ScrollBars.Both;
            CellValueNeeded += OnCellValueNeeded;
            ColumnWidthChanged += OnColumnWidthChanged; // for debugging purposes
            Resize += OnResizeChanged;

            FilterStringChanged += OnFilterStringChanged;

            EnableHeadersVisualStyles = false; // required for ColumnHeadersDefaultCellStyle to take effect
            ApplyColors();

            SetDoubleBuffered();
        }

        void ApplyColors()
        {
            // The default Fixed3D border renders as a fixed light bevel that ignores our colors
            // entirely and has no color property of its own to retint. Only a problem in dark
            // mode (light mode's own bevel already looks fine against a light background) - the
            // grid lines/header already delineate the grid's edges without it.
            BorderStyle = Theme.ActiveColors.IsDark ? BorderStyle.None : BorderStyle.Fixed3D;
            // The grid's own scrollbars are drawn by the OS and have no color property at all -
            // switch their visual-style class instead (see NativeMethods.SetScrollBarTheme).
            NativeMethods.SetScrollBarTheme(this, Theme.ActiveColors.IsDark);
            ColumnHeadersDefaultCellStyle.BackColor = Theme.ActiveColors.Primary;
            ColumnHeadersDefaultCellStyle.ForeColor = Theme.ActiveColors.OnPrimary;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.ActiveColors.Primary;
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.ActiveColors.OnPrimary;
            GridColor = Theme.ActiveColors.Border;
            // A paler green than both the header and the header's filter button, so a selected
            // row (especially the first row, right under the header) reads as its own distinct
            // state rather than merging into the header or bleeding into the button color.
            DefaultCellStyle.ForeColor = Theme.ActiveColors.Text;
            DefaultCellStyle.SelectionBackColor = Theme.ActiveColors.PrimaryPale;
            DefaultCellStyle.SelectionForeColor = Theme.ActiveColors.Text;
            // In dark mode, grids get the darkest background (matching the web app's own dark
            // palette, which reserves its near-black shade for grids/tables and uses the lighter
            // charcoal "card" tone for other elevated panels) - primary/alternating swapped versus
            // light mode, which keeps its original white/parchment rows unchanged.
            Color rowColor = Theme.ActiveColors.IsDark ? Theme.ActiveColors.Background : Theme.ActiveColors.Card;
            Color alternateRowColor = Theme.ActiveColors.IsDark ? Theme.ActiveColors.Card : Theme.ActiveColors.Background;
            BackgroundColor = rowColor;
            RowsDefaultCellStyle.BackColor = rowColor;
            RowsDefaultCellStyle.ForeColor = Theme.ActiveColors.Text;
            AlternatingRowsDefaultCellStyle.BackColor = alternateRowColor;
            AlternatingRowsDefaultCellStyle.ForeColor = Theme.ActiveColors.Text;
        }

        // Called after a light/dark toggle - constructor-time colors don't update themselves.
        public void ReapplyTheme()
        {
            ApplyColors();
            Invalidate();
        }

        public void OnFilterStringChanged(object? sender, FilterEventArgs e)
        {
            if (e.Cancel)
                _dataSource = _fulllist;
            else
            {
                SortableBindingList<T> filter = _fulllist;
                string filterString = e.FilterString ?? string.Empty;
                foreach (string filteredColumn in VirtualDataGridView<T>.GetFilteredColumns(filterString))
                {
                    List<string> filteredValues = VirtualDataGridView<T>.GetFilteredValues(filteredColumn, filterString);
                    filter = [.. filter.Where(x => x is not null && filteredValues.Contains(x.GetType().GetProperty(filteredColumn)?.GetValue(x, null)))];
                }
                _dataSource = filter;
                DataView dataView = BuildDataTable(_dataSource).DefaultView;
                base.DataSource = dataView;
                FilterCountText = $"Showing {filter.Count} of {_fulllist.Count}";
                OnVirtualGridFiltered();
            }
            Refresh();
        }

        internal static List<string> GetFilteredColumns(string filterString)
        {
            string[] separator = [" AND "];
            List<string> result = [];
            List<string> clauses = [.. filterString.Split(separator, StringSplitOptions.None)];
            foreach (string clause in clauses)
            {
                int pos = clause.IndexOf('[', StringComparison.Ordinal);
                if (pos > 0)
                {
                    int endpos = clause.IndexOf(']', StringComparison.Ordinal);
                    if (endpos > 0 && pos < endpos)
                        result.Add(clause.Substring(pos + 1, endpos - pos - 1));
                }
            }
            return result;
        }

        // deal with filter string of type 
        // (Convert([Gender],System.String) IN ('U')) AND (Convert([Surname],System.String) IN ('Mitchell')) AND (Convert([Forenames],System.String) IN ('UNKNOWN'))
        // deal with updating count in statusbar
        internal static List<string> GetFilteredValues(string filterColumn, string filterString)
        {
            List<string> result = [];
            int startclausepos = filterString.IndexOf(filterColumn);
            if (startclausepos > 0)
            {
                int endclausepos = filterString.IndexOf(" AND ", startclausepos);
                string clause = endclausepos > 0 ? filterString.Substring(startclausepos, endclausepos) : filterString[startclausepos..];
                int pos = clause.IndexOf("IN (");
                if (pos >= 0 && pos < clause.Length - 6)
                {
                    int endpos = clause.IndexOf(')', pos);
                    string values = clause.Substring(pos + 4, endpos - pos - 4);
                    foreach (string value in values.Split(','))
                        result.Add(value.Replace("\'", "", StringComparison.Ordinal).Trim());
                }
            }
            return result;
        }

        public event EventHandler<CountEventArgs>? VirtualGridFiltered;

        protected void OnVirtualGridFiltered()
        {
            CountEventArgs args = new()
            {
                FilterText = FilterCountText
            };
            VirtualGridFiltered?.Invoke(this, args);
        }

        [DefaultValue(null), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new SortableBindingList<T>? DataSource
        {
            get => _dataSource;
            set
            {
                CreateGridColumns();
                _dataSource = value ?? [];
                _fulllist = value ?? [];
                if (value is not null)
                {
                    DataView dataView = BuildDataTable(value).DefaultView;
                    base.DataSource = dataView;
                }
                else
                    base.DataSource = null;
            }
        }

        static DataTable BuildDataTable(SortableBindingList<T> lst)
        {
            DataTable tbl = CreateTable();
            tbl.Columns.Add(SourceIndexColumn, typeof(int));
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            for (int i = 0; i < lst.Count; i++)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(lst[i]);
                row[SourceIndexColumn] = i;
                tbl.Rows.Add(row);
            }
            return tbl;
        }

        static DataTable CreateTable()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                Type type = (IsNullableType(prop.PropertyType) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType) ?? typeof(string);
                tbl.Columns.Add(prop.Name, type);
            }
            return tbl;
        }
        static bool IsNullableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public T? CurrentRowDataBoundItem => CurrentRow is not null ? DataBoundItem(CurrentRow.Index) : default;

        public T DataBoundItem(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < RowCount &&
                Rows[rowIndex].DataBoundItem is DataRowView drv &&
                drv.Row[SourceIndexColumn] is int sourceIndex)
                return _dataSource[sourceIndex];
            return _dataSource[rowIndex];
        }

        void CreateGridColumns()
        {
            if (DesignMode)
                return;
            Columns.Clear();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                ColumnDetail? cd = info.GetCustomAttribute<ColumnDetail>();
                DataGridViewColumn dgvc;
                switch (cd?.TypeofColumn)
                {
                    case ColumnDetail.ColumnType.LinkCell:
                        dgvc = new DataGridViewLinkColumn();
                        break;
                    case ColumnDetail.ColumnType.CheckBox:
                        dgvc = new DataGridViewCheckBoxColumn();
                        break;
                    case ColumnDetail.ColumnType.Icon:
                        dgvc = new DataGridViewImageColumn();
                        DisableFilterChecklist(dgvc);
                        break;
                    default:
                        dgvc = new DataGridViewTextBoxColumn();
                        break;
                }
                dgvc.Name = info.Name;
                dgvc.DataPropertyName = info.Name;
                // get following from the attributes
                dgvc.HeaderText = cd?.ColumnName ?? info.Name;
                dgvc.Width = (int)(cd?.ColumnWidth ?? 100);
                dgvc.MinimumWidth = (int)(cd?.ColumnWidth ?? 100);
                dgvc.HeaderCell.Style.Alignment = cd?.Alignment ?? DataGridViewContentAlignment.MiddleLeft;
                dgvc.SortMode = DataGridViewColumnSortMode.Programmatic;
                DisableFilterCustom(dgvc);
                Columns.Add(dgvc);
            }
        }

        void ForceToParent()
        {
            if (Parent is not null)
            {
                //maxHeight = Parent.Height - SystemInformation.HorizontalScrollBarHeight - Location.Y;
                //maxWidth = Parent.Width - SystemInformation.VerticalScrollBarWidth - Location.X;
                int maxHeight = Parent.Height - Location.Y;
                int maxWidth = Parent.Width - Location.X;
                Size = new Size(maxWidth, maxHeight);
                //Debug.WriteLine($"{Name} has parent of {Parent}");
            }
        }


        void OnResizeChanged(object? sender, EventArgs e) => ForceToParent();

        void OnCellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
        {
            if (_dataSource is null || _dataSource.Count == 0 || e.RowIndex > _dataSource.Count - 1)
                return;
            T data = DataBoundItem(e.RowIndex);
            e.Value = GetValueFor(data, Columns[e.ColumnIndex].DataPropertyName);
        }

        static void OnColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
        {
            Debug.WriteLine($"Column {e.Column.Name} changed width to {e.Column.Width}");
        }

        protected abstract object GetValueFor(T data, string propertyName);

        class PropertyComparer(string propertyName, ListSortDirection direction) : IComparer<T>
        {
            readonly PropertyInfo? _accessor = typeof(T).GetProperty(propertyName);
            readonly int _direction = direction == ListSortDirection.Ascending ? 1 : -1;

            public int Compare(T? x, T? y)
            {
                IComparable? val2 = _accessor?.GetValue(y) as IComparable;

                if (_accessor?.GetValue(x) is not IComparable val1)
                    return val2 is null ? 0 : _direction * -1;

                return _direction * val1.CompareTo(val2);
            }
        }
    }
}