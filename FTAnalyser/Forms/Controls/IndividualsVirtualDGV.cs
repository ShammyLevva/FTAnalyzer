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
    class IndividualsVirtualDGV : DataGridView
    {
        SortableBindingList<IDisplayIndividual> _individualData;
        DataGridViewColumn sortedColumn;
        ListSortDirection sortedDirection;

        public IndividualsVirtualDGV()
        {
            VirtualMode = true;
            CellValueNeeded += IndividualsVirtualDGV_CellValueNeeded;
            ColumnHeaderMouseClick += IndividualsVirtualDGV_ColumnHeaderMouseClick;
            AllowUserToAddRows = false;
            ReadOnly = true;
            Columns.Clear();
            // needs to add columns as grid isn't defined in Main Form
            foreach (PropertyInfo info in typeof(IDisplayIndividual).GetProperties())
            {
                ColumnDetail cd = info.GetCustomAttribute<ColumnDetail>();
                DataGridViewColumn dgvc;
                if (info.Name == "FamilySearchID")
                    dgvc = new DataGridViewLinkColumn();
                else
                    dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = info.Name;
                dgvc.DataPropertyName = info.Name;
                // get following from the attributes
                dgvc.HeaderText = cd.ColumnName;
                dgvc.Width = (int)cd.ColumnWidth;
                dgvc.MinimumWidth = (int)cd.ColumnWidth;
                Columns.Add(dgvc);
            }
        }

        public new SortableBindingList<IDisplayIndividual> DataSource
        {
            get => _individualData;
            set
            {
                _individualData = value;
                RowCount = value?.Count ?? 1;
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

            if (_individualData is null) return;

            var comparer = new IndividualPropertyComparer(sortedColumn.DataPropertyName, direction);
            _individualData.Sort(comparer);

            Refresh();
        }

        void IndividualsVirtualDGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sortedDirection = Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            Sort(Columns[e.ColumnIndex], sortedDirection);
        }

        void IndividualsVirtualDGV_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_individualData is null)
                return;
            IDisplayIndividual ind = _individualData[e.RowIndex];
            // Set the cell value to paint using the Customer object retrieved.
            switch (Columns[e.ColumnIndex].DataPropertyName)
            {
                case nameof(IDisplayIndividual.IndividualID):
                    e.Value = ind.IndividualID;
                    break;

                case nameof(IDisplayIndividual.Forenames):
                    e.Value = ind.Forenames;
                    break;

                case nameof(IDisplayIndividual.Surname):
                    e.Value = ind.Surname;
                    break;

                case nameof(IDisplayIndividual.Gender):
                    e.Value = ind.Gender;
                    break;

                case nameof(IDisplayIndividual.BirthDate):
                    e.Value = ind.BirthDate;
                    break;

                case nameof(IDisplayIndividual.BirthLocation):
                    e.Value = ind.BirthLocation;
                    break;

                case nameof(IDisplayIndividual.DeathDate):
                    e.Value = ind.DeathDate;
                    break;

                case nameof(IDisplayIndividual.DeathLocation):
                    e.Value = ind.DeathLocation;
                    break;

                case nameof(IDisplayIndividual.Occupation):
                    e.Value = ind.Occupation;
                    break;

                case nameof(IDisplayIndividual.LifeSpan):
                    e.Value = ind.LifeSpan;
                    break;

                case nameof(IDisplayIndividual.Relation):
                    e.Value = ind.Relation;
                    break;

                case nameof(IDisplayIndividual.RelationToRoot):
                    e.Value = ind.RelationToRoot;
                    break;

                case nameof(IDisplayIndividual.Title):
                    e.Value = ind.Title;
                    break;

                case nameof(IDisplayIndividual.Suffix):
                    e.Value = ind.Suffix;
                    break;

                case nameof(IDisplayIndividual.Alias):
                    e.Value = ind.Alias;
                    break;

                case nameof(IDisplayIndividual.FamilySearchID):
                    e.Value = ind.FamilySearchID;
                    break;

                case nameof(IDisplayIndividual.MarriageCount):
                    e.Value = ind.MarriageCount;
                    break;

                case nameof(IDisplayIndividual.ChildrenCount):
                    e.Value = ind.ChildrenCount;
                    break;

                case nameof(IDisplayIndividual.BudgieCode):
                    e.Value = ind.BudgieCode;
                    break;

                case nameof(IDisplayIndividual.Ahnentafel):
                    e.Value = ind.Ahnentafel;
                    break;

                case nameof(IDisplayIndividual.HasNotes):
                    e.Value = ind.HasNotes;
                    break;

                case nameof(IDisplayIndividual.FactsCount):
                    e.Value = ind.FactsCount;
                    break;

                case nameof(IDisplayIndividual.SourcesCount):
                    e.Value = ind.SourcesCount;
                    break;
            }
        }

    }

    class IndividualPropertyComparer : IComparer<IDisplayIndividual>
    {
        PropertyInfo _accessor;
        int _direction;

        public IndividualPropertyComparer(string propertyName, ListSortDirection direction)
        {
            _accessor = typeof(IDisplayIndividual).GetProperty(propertyName);
            _direction = direction == ListSortDirection.Ascending ? 1 : -1;
        }

        public int Compare(IDisplayIndividual ind1, IDisplayIndividual ind2)
        {
            var val1 = _accessor?.GetValue(ind1) as IComparable;
            var val2 = _accessor?.GetValue(ind2) as IComparable;

            if (val1 is null)
                return val2 is null ? 0 : _direction * -1;

            return _direction * val1.CompareTo(val2);
        }

    }
}
