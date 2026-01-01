namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvOccupations : VirtualDataGridView<IDisplayOccupation>
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
