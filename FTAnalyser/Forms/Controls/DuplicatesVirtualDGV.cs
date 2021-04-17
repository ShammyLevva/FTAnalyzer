using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    class DuplicatesVirtualDGV : DataGridView
    {
        SortableBindingList<IDisplayDuplicateIndividual> _duplicateData;
        DataGridViewColumn sortedColumn;
        ListSortDirection sortedDirection;

        public DuplicatesVirtualDGV()
        {
            VirtualMode = true;
            CellValueNeeded += DuplicatesVirtualDGV_CellValueNeeded;
            ColumnHeaderMouseClick += DuplicatesVirtualDGV_ColumnHeaderMouseClick;
            AllowUserToAddRows = false;
            ReadOnly = true;
        }

        public new SortableBindingList<IDisplayDuplicateIndividual> DataSource
        {
            get => _duplicateData;
            set
            {
                _duplicateData = value;
                RowCount = value?.Count ?? 1;
            }
        }

        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            // needs to implemt the column sorting depending on colum clicked
            // add comparers
            foreach (DataGridViewColumn column in Columns)
            {
                if (column == dataGridViewColumn)
                    column.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                else
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            sortedColumn = dataGridViewColumn;
            sortedDirection = direction;

            if (_duplicateData is null) return;

            var comparer = new PropertyComparer(sortedColumn.DataPropertyName, direction);
            _duplicateData.Sort(comparer);

            Refresh();
        }

        void DuplicatesVirtualDGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sortedDirection = Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            Sort(Columns[e.ColumnIndex], sortedDirection);
        }

        void DuplicatesVirtualDGV_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_duplicateData is null)
                return;
            IDisplayDuplicateIndividual ind = _duplicateData[e.RowIndex];
            // Set the cell value to paint using the Customer object retrieved.
            switch (Columns[e.ColumnIndex].DataPropertyName)
            {
                case nameof(IDisplayDuplicateIndividual.IndividualID):
                    e.Value = ind.IndividualID;
                    break;

                case nameof(IDisplayDuplicateIndividual.Name):
                    e.Value = ind.Name;
                    break;

                case nameof(IDisplayDuplicateIndividual.Forenames):
                    e.Value = ind.Forenames;
                    break;

                case nameof(IDisplayDuplicateIndividual.Surname):
                    e.Value = ind.Surname;
                    break;

                case nameof(IDisplayDuplicateIndividual.BirthDate):
                    e.Value = ind.BirthDate;
                    break;

                case nameof(IDisplayDuplicateIndividual.BirthLocation):
                    e.Value = ind.BirthLocation;
                    break;

                case nameof(IDisplayDuplicateIndividual.MatchIndividualID):
                    e.Value = ind.MatchIndividualID;
                    break;

                case nameof(IDisplayDuplicateIndividual.MatchName):
                    e.Value = ind.MatchName;
                    break;

                case nameof(IDisplayDuplicateIndividual.MatchBirthDate):
                    e.Value = ind.MatchBirthDate;
                    break;

                case nameof(IDisplayDuplicateIndividual.MatchBirthLocation):
                    e.Value = ind.MatchBirthLocation;
                    break;

                case nameof(IDisplayDuplicateIndividual.Score):
                    e.Value = ind.Score;
                    break;

                case nameof(IDisplayDuplicateIndividual.IgnoreNonDuplicate):
                    e.Value = ind.IgnoreNonDuplicate;
                    break;
            }
        }

    }

    class PropertyComparer : IComparer<IDisplayDuplicateIndividual>
    {
        PropertyInfo _accessor;
        int _direction;

        public PropertyComparer(string propertyName, ListSortDirection direction)
        {
            _accessor = typeof(IDisplayDuplicateIndividual).GetProperty(propertyName);
            _direction = direction == ListSortDirection.Ascending ? 1 : -1;
        }

        public int Compare(IDisplayDuplicateIndividual ind1, IDisplayDuplicateIndividual ind2)
        {
            var val1 = _accessor?.GetValue(ind1) as IComparable;
            var val2 = _accessor?.GetValue(ind2) as IComparable;

            if (val1 is null)
                return val2 is null ? 0 : _direction * -1;

            return _direction * val1.CompareTo(val2);
        }

    }
}
