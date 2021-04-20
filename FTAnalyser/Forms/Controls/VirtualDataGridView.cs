using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    [ComplexBindingProperties()]
    abstract class VirtualDataGridView<T> : DataGridView
    {
        SortableBindingList<T> _dataSource;
        DataGridViewColumn sortedColumn;
        ListSortDirection sortedDirection;

        public VirtualDataGridView()
        {
            _dataSource = new SortableBindingList<T>();
            VirtualMode = true;
            AutoGenerateColumns = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeColumns = true;
            ReadOnly = true;
            RowHeadersWidth = 50;
            MultiSelect = false;
            CellValueNeeded += OnCellValueNeeded;
            ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
            ColumnWidthChanged += OnColumnWidthChanged; // for debugging purposes
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new SortableBindingList<T> DataSource
        {
            get => _dataSource;
            set
            {
                CreateGridColumns();
                _dataSource = value;
                RowCount = value?.Count ?? 1;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public T CurrentRowDataBoundItem => _dataSource[CurrentRow.Index];

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

        public override void Sort(DataGridViewColumn dgvColumn, ListSortDirection direction)
        {
            if (dgvColumn is null || dgvColumn.SortMode == DataGridViewColumnSortMode.NotSortable)
                return;
            // needs to implemt the column sorting depending on colum clicked
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

        void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sortedDirection = Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            Sort(Columns[e.ColumnIndex], sortedDirection);
        }

        void OnCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_dataSource is null || _dataSource.Count == 0)
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
                var val1 = _accessor?.GetValue(ind1) as IComparable;
                var val2 = _accessor?.GetValue(ind2) as IComparable;

                if (val1 is null)
                    return val2 is null ? 0 : _direction * -1;

                return _direction * val1.CompareTo(val2);
            }
        }
    }
}