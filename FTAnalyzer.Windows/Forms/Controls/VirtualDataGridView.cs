using FTAnalyzer.Utilities;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Zuby.ADGV;

namespace FTAnalyzer.Forms.Controls
{
    [ComplexBindingProperties()]
    abstract class VirtualDataGridView<T> : AdvancedDataGridView
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
          
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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

            SetDoubleBuffered();
        }

        public void OnFilterStringChanged(object? sender, FilterEventArgs e)
        {
            if (e.Cancel)
                _dataSource = _fulllist;
            else
            {
                SortableBindingList<T> filter = _fulllist;
                foreach (string filteredColumn in VirtualDataGridView<T>.GetFilteredColumns(e.FilterString))
                {
                    List<string> filteredValues = VirtualDataGridView<T>.GetFilteredValues(filteredColumn, e.FilterString);
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