using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    abstract class VirtualDataGridView<T> : DataGridView
    {
        SortableBindingList<T> _dataSource;
        DataGridViewColumn sortedColumn;
        ListSortDirection sortedDirection;

        public VirtualDataGridView()
        {
            VirtualMode = true;
            CellValueNeeded += OnCellValueNeeded;
            ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
            AllowUserToAddRows = false;
            ReadOnly = true;
            CreateGridColumns();
        }

        public new SortableBindingList<T> DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                RowCount = value?.Count ?? 1;
            }
        }

        void CreateGridColumns()
        {
            Columns.Clear();
            // needs to add columns as grid isn't defined in Main Form
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