using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Zuby.ADGV;

namespace FTAnalyzer.Forms.Controls
{
    [ComplexBindingProperties()]
    abstract class VirtualDataGridView<T> : AdvancedDataGridView
    {
        internal SortableBindingList<T> _dataSource;
        internal SortableBindingList<T> _fulllist;
        DataGridViewColumn sortedColumn;
        ListSortDirection sortedDirection;

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
                List<string> filteredValues = GetFilteredValues(e.FilterString);
                string filteredColumns = GetFilteredColumns(e.FilterString);
                _dataSource = new SortableBindingList<T>(_fulllist.Where(s => filteredValues.Contains(s.GetType().GetProperty(filteredColumns).GetValue(s, null))));
            }
            Refresh();
        }

        internal List<string> GetFilteredValues(string filterString)
        {
            List<string> result = new List<string>();
            int pos = filterString.IndexOf("IN (");
            if (pos >= 0 && pos < filterString.Length-6)
            {
                int endpos = filterString.IndexOf(")", pos);
                string values = filterString.Substring(pos + 4, endpos - pos - 4);
                foreach (string value in values.Split(','))
                    result.Add(value.Replace("\'","").Trim());
            }
            return result;
        }

        internal string GetFilteredColumns(string filterString)
        {
            string result = string.Empty;
            // for now only return first filtered column later return list of strings
            int pos = filterString.IndexOf("[");
            if(pos > 0)
            {
                int endpos = filterString.IndexOf("]");
                if (endpos > 0 && pos < endpos)
                    result = filterString.Substring(pos+1, endpos - pos -1);
            }
            return result;
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
                RowCount = value?.Count ?? 1;
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
                //Console.WriteLine($"{Name} has parent of {Parent}");
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
            sortedColumn = dgvColumn;
            sortedDirection = direction;

            if (_dataSource is null) return;

            var comparer = new PropertyComparer(sortedColumn.DataPropertyName, direction);
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
            Console.WriteLine($"Column {e.Column.Name} changed width to {e.Column.Width}");
        }

        protected abstract object GetValueFor(T data, string propertyName);

        class PropertyComparer : IComparer<T>
        {
            PropertyInfo _accessor;
            int _direction;

            public PropertyComparer(string propertyName, ListSortDirection direction)
            {
                _accessor = typeof(T).GetProperty(propertyName);
                _direction = direction == ListSortDirection.Ascending ? 1 : -1;
            }

            public int Compare(T ind1, T ind2)
            {
                IComparable val1 = _accessor?.GetValue(ind1) as IComparable;
                IComparable val2 = _accessor?.GetValue(ind2) as IComparable;

                if (val1 is null)
                    return val2 is null ? 0 : _direction * -1;

                return _direction * val1.CompareTo(val2);
            }
        }
    }
}