namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLooseDeaths : VirtualDataGridView<IDisplayLooseDeath>
    {
        protected override object GetValueFor(IDisplayLooseDeath ind, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayLooseDeath.IndividualID) => ind.IndividualID,
                nameof(IDisplayLooseDeath.Forenames) => ind.Forenames,
                nameof(IDisplayLooseDeath.Surname) => ind.Surname,
                nameof(IDisplayLooseDeath.BirthDate) => ind.BirthDate,
                nameof(IDisplayLooseDeath.BirthLocation) => ind.BirthLocation,
                nameof(IDisplayLooseDeath.DeathDate) => ind.DeathDate,
                nameof(IDisplayLooseDeath.DeathLocation) => ind.DeathLocation,
                nameof(IDisplayLooseDeath.LooseDeath) => ind.LooseDeath,
                _ => ind.IndividualID,
            };
        }
    }
}
