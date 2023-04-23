using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Zuby.ADGV;
using System.Diagnostics;

namespace FTAnalyzer.Forms.Controls
{
    [ComplexBindingProperties()]
    abstract class VirtualDataGridView<T> : AdvancedDataGridView
    {
        internal SortableBindingList<T> _dataSource;
        internal SortableBindingList<T> _fulllist;
        public string FilterCountText { get; private set; }

        public VirtualDataGridView()
        {
            _dataSource = new SortableBindingList<T>();
            _fulllist = new SortableBindingList<T>();
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
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ScrollBars = ScrollBars.Both;
            CellValueNeeded += OnCellValueNeeded;
            ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
            ColumnWidthChanged += OnColumnWidthChanged; // for debugging purposes
            Resize += OnResizeChanged;

            FilterStringChanged += OnFilterStringChanged;
            SortStringChanged += OnSortStringChanged;

            SetDoubleBuffered();
        }

        void OnSortStringChanged(object sender, SortEventArgs e)
        {
            int lastPos = e.SortString.LastIndexOf('[');
            if (lastPos >= 0)
            {
                string lastsort = e.SortString.Right(e.SortString.Length - lastPos);
                string column = lastsort.Between('[', ']');
                int pos = lastsort.IndexOf("] ");
                string direction = pos > 0 ? lastsort.Substring(pos + 2, 3) : "ASC";
                ListSortDirection sortDirection = direction == "ASC" ? ListSortDirection.Ascending : ListSortDirection.Descending;
                DataGridViewColumn sortColumn = Columns[column] is null ? Columns[0] : Columns[column];
                Sort(sortColumn, sortDirection);
            }
        }

        public void OnFilterStringChanged(object sender, FilterEventArgs e)
        {
            if (e.Cancel)
                _dataSource = _fulllist;
            else
            {
                SortableBindingList<T> filter = _fulllist;
                foreach (string filteredColumn in VirtualDataGridView<T>.GetFilteredColumns(e.FilterString))
                {
                    List<string> filteredValues = VirtualDataGridView<T>.GetFilteredValues(filteredColumn, e.FilterString);
                    filter = new SortableBindingList<T>(filter.Where(x => filteredValues.Contains(x.GetType().GetProperty(filteredColumn).GetValue(x, null))));
                }
                _dataSource = filter;
                FilterCountText = $"Showing {filter.Count} of {_fulllist.Count}";
                OnVirtualGridFiltered();
            }
            Refresh();
        }

        internal static List<string> GetFilteredColumns(string filterString)
        {
            List<string> result = new();
            List<string> clauses = filterString.Split(new string[] { " AND " }, StringSplitOptions.None).ToList();
            foreach (string clause in clauses)
            {
                int pos = clause.IndexOf("[");
                if (pos > 0)
                {
                    int endpos = clause.IndexOf("]");
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
            List<string> result = new();
            int startclausepos = filterString.IndexOf(filterColumn);
            if (startclausepos > 0)
            {
                int endclausepos = filterString.IndexOf(" AND ", startclausepos);
                string clause = endclausepos > 0 ? filterString.Substring(startclausepos, endclausepos) : filterString[startclausepos..];
                int pos = clause.IndexOf("IN (");
                if (pos >= 0 && pos < clause.Length - 6)
                {
                    int endpos = clause.IndexOf(")", pos);
                    string values = clause.Substring(pos + 4, endpos - pos - 4);
                    foreach (string value in values.Split(','))
                        result.Add(value.Replace("\'", "").Trim());
                }
            }
            return result;
        }

        public event EventHandler<CountEventArgs> VirtualGridFiltered;

        protected void OnVirtualGridFiltered()
        {
            CountEventArgs args = new()
            {
                FilterText = FilterCountText
            };
            VirtualGridFiltered?.Invoke(null, args);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new SortableBindingList<T> DataSource
        {
            get => _dataSource;
            set
            {
                CreateGridColumns();
                _dataSource = value;
                _fulllist = value;
                base.DataSource = value;
                //RowCount = value?.Count ?? 1;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public T CurrentRowDataBoundItem => _dataSource[CurrentRow.Index];

        public T DataBoundItem(int rowIndex) => _dataSource[rowIndex];

        void CreateGridColumns()
        {
            if (DesignMode)
                return;
            Columns.Clear();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                ColumnDetail cd = info.GetCustomAttribute<ColumnDetail>();
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
                DisableFilterCustom(dgvc);
                Columns.Add(dgvc);
            }
        }

        void ForceToParent()
        {
            if (Parent != null)
            {
                //maxHeight = Parent.Height - SystemInformation.HorizontalScrollBarHeight - Location.Y;
                //maxWidth = Parent.Width - SystemInformation.VerticalScrollBarWidth - Location.X;
                int maxHeight = Parent.Height - Location.Y;
                int maxWidth = Parent.Width - Location.X;
                Size = new Size(maxWidth, maxHeight);
                //Debug.WriteLine($"{Name} has parent of {Parent}");
            }
        }

        public override void Sort(DataGridViewColumn dgvColumn, ListSortDirection direction)
        {
            if (dgvColumn is null || dgvColumn.SortMode == DataGridViewColumnSortMode.NotSortable)
                return;
            // needs to implemt the column sorting depending on column clicked
            // add comparers
            foreach (DataGridViewColumn column in Columns)
            {
                if (column == dgvColumn)
                    column.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                else
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            if (_dataSource is null) return;

            var comparer = new PropertyComparer(dgvColumn.DataPropertyName, direction);
            _dataSource.Sort(comparer);

            Refresh();
        }

        void OnResizeChanged(object sender, EventArgs e) => ForceToParent();

        void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //sortedDirection = Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            //Sort(Columns[e.ColumnIndex], sortedDirection);
        }

        void OnCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_dataSource is null || _dataSource.Count == 0 || e.RowIndex > _dataSource.Count -1)
                return;
            var data = _dataSource[e.RowIndex];
            e.Value = GetValueFor(data, Columns[e.ColumnIndex].DataPropertyName);
        }

        void OnColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Debug.WriteLine($"Column {e.Column.Name} changed width to {e.Column.Width}");
        }

        protected abstract object GetValueFor(T data, string propertyName);

        class PropertyComparer : IComparer<T>
        {
            readonly PropertyInfo _accessor;
            readonly int _direction;

            public PropertyComparer(string propertyName, ListSortDirection direction)
            {
                _accessor = typeof(T).GetProperty(propertyName);
                _direction = direction == ListSortDirection.Ascending ? 1 : -1;
            }

            public int Compare(T? ind1, T? ind2)
            {
                IComparable val2 = _accessor?.GetValue(ind2) as IComparable;

                if (_accessor?.GetValue(ind1) is not IComparable val1)
                    return val2 is null ? 0 : _direction * -1;

                return _direction * val1.CompareTo(val2);
            }
        }
    }
}