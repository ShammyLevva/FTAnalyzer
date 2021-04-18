namespace FTAnalyzer.Forms.Controls
{
    class OccupationsVirtualDGV : VirtualDataGridView<IDisplayOccupation>
    {
        protected override object GetValueFor(IDisplayOccupation occ, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayOccupation.Occupation):
                    return occ.Occupation;
                case nameof(IDisplayOccupation.Count):
                    return occ.Count;
            }
            return null;
        }
    }
}
