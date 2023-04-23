namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVLooseInfo : VirtualDataGridView<IDisplayLooseInfo>
    {
        protected override object GetValueFor(IDisplayLooseInfo ind, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayLooseInfo.IndividualID) => ind.IndividualID,
                nameof(IDisplayLooseInfo.Forenames) => ind.Forenames,
                nameof(IDisplayLooseInfo.Surname) => ind.Surname,
                nameof(IDisplayLooseInfo.BirthDate) => ind.BirthDate,
                nameof(IDisplayLooseInfo.BirthLocation) => ind.BirthLocation,
                nameof(IDisplayLooseInfo.DeathDate) => ind.DeathDate,
                nameof(IDisplayLooseInfo.DeathLocation) => ind.DeathLocation,
                nameof(IDisplayLooseInfo.LooseBirth) => ind.LooseBirth,
                nameof(IDisplayLooseInfo.LooseDeath) => ind.LooseDeath,
                _ => ind.IndividualID,
            };
        }
    }
}
