namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvCustomFacts : VirtualDataGridView<IDisplayCustomFact>
    {
        public VirtualDgvCustomFacts()
        {
            ReadOnly = false;
            CellValuePushed += OnCellValuePushed;
            CurrentCellDirtyStateChanged += OnCurrentCellDirtyStateChanged;
        }

        protected override object GetValueFor(IDisplayCustomFact fact, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayCustomFact.CustomFactName):
                    return fact.CustomFactName;
                case nameof(IDisplayCustomFact.IndividualCount):
                    return fact.IndividualCount;
                case nameof(IDisplayCustomFact.FamilyCount):
                    return fact.FamilyCount;
                case nameof(IDisplayCustomFact.Ignore):
                    return fact.Ignore;
                default:
                    break;
            }
            return fact.CustomFactName;
        }

        void OnCellValuePushed(object? sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _dataSource.Count) return;
            IDisplayCustomFact fact = DataBoundItem(e.RowIndex);
            string propertyName = Columns[e.ColumnIndex].DataPropertyName;
            if (propertyName == nameof(IDisplayCustomFact.Ignore) && e.Value is bool ignore)
                fact.Ignore = ignore;
        }

        static void OnCurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (sender is DataGridView dgv && dgv.CurrentCell is DataGridViewCheckBoxCell)
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
