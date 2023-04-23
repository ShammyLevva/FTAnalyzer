namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVSurnames : VirtualDataGridView<IDisplaySurnames>
    {
        protected override object GetValueFor(IDisplaySurnames occ, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplaySurnames.Surname) => occ.Surname,
                nameof(IDisplaySurnames.Individuals) => occ.Individuals,
                nameof(IDisplaySurnames.Families) => occ.Families,
                nameof(IDisplaySurnames.Marriages) => occ.Marriages,
                _ => occ.Surname,
            };
        }
    }
}
