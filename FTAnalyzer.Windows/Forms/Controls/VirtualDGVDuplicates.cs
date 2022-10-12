namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVDuplicates : VirtualDataGridView<IDisplayDuplicateIndividual>
    {
        protected override object GetValueFor(IDisplayDuplicateIndividual dup, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayDuplicateIndividual.IndividualID) => dup.IndividualID,
                nameof(IDisplayDuplicateIndividual.Name) => dup.Name,
                nameof(IDisplayDuplicateIndividual.Forenames) => dup.Forenames,
                nameof(IDisplayDuplicateIndividual.Surname) => dup.Surname,
                nameof(IDisplayDuplicateIndividual.BirthDate) => dup.BirthDate,
                nameof(IDisplayDuplicateIndividual.BirthLocation) => dup.BirthLocation,
                nameof(IDisplayDuplicateIndividual.Gender) => dup.Gender,
                nameof(IDisplayDuplicateIndividual.MatchIndividualID) => dup.MatchIndividualID,
                nameof(IDisplayDuplicateIndividual.MatchName) => dup.MatchName,
                nameof(IDisplayDuplicateIndividual.MatchBirthDate) => dup.MatchBirthDate,
                nameof(IDisplayDuplicateIndividual.MatchBirthLocation) => dup.MatchBirthLocation,
                nameof(IDisplayDuplicateIndividual.Score) => dup.Score,
                nameof(IDisplayDuplicateIndividual.IgnoreNonDuplicate) => dup.IgnoreNonDuplicate,
                nameof(IDisplayDuplicateIndividual.MatchGender) => dup.MatchGender,
                _ => null,
            };
        }
    }
}
