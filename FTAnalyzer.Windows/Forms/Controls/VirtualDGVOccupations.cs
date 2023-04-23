namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVOccupations : VirtualDataGridView<IDisplayOccupation>
    {
        protected override object GetValueFor(IDisplayOccupation occ, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayOccupation.Occupation) => occ.Occupation,
                nameof(IDisplayOccupation.Count) => occ.Count,
                _ => occ.Occupation,
            };
        }
    }
}
