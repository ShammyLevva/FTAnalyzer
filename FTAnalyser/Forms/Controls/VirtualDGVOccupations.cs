namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVOccupations : VirtualDataGridView<IDisplayOccupation>
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
