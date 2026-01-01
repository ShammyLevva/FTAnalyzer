namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvLooseBirths : VirtualDataGridView<IDisplayLooseBirth>
    {
        protected override object GetValueFor(IDisplayLooseBirth ind, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayLooseBirth.IndividualID) => ind.IndividualID,
                nameof(IDisplayLooseBirth.Forenames) => ind.Forenames,
                nameof(IDisplayLooseBirth.Surname) => ind.Surname,
                nameof(IDisplayLooseBirth.BirthDate) => ind.BirthDate,
                nameof(IDisplayLooseBirth.BirthLocation) => ind.BirthLocation,
                nameof(IDisplayLooseBirth.LooseBirth) => ind.LooseBirth,
                _ => ind.IndividualID,
            };
        }
    }
}
