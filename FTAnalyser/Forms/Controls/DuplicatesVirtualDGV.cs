namespace FTAnalyzer.Forms.Controls
{
    class DuplicatesVirtualDGV : VirtualDataGridView<IDisplayDuplicateIndividual>
    {
        protected override object GetValueFor(IDisplayDuplicateIndividual ind, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayDuplicateIndividual.IndividualID):
                    return ind.IndividualID;
                case nameof(IDisplayDuplicateIndividual.Name):
                    return ind.Name;
                case nameof(IDisplayDuplicateIndividual.Forenames):
                    return ind.Forenames;
                case nameof(IDisplayDuplicateIndividual.Surname):
                    return ind.Surname;
                case nameof(IDisplayDuplicateIndividual.BirthDate):
                    return ind.BirthDate;
                case nameof(IDisplayDuplicateIndividual.BirthLocation):
                    return ind.BirthLocation;
                case nameof(IDisplayDuplicateIndividual.MatchIndividualID):
                    return ind.MatchIndividualID;
                case nameof(IDisplayDuplicateIndividual.MatchName):
                    return ind.MatchName;
                case nameof(IDisplayDuplicateIndividual.MatchBirthDate):
                    return ind.MatchBirthDate;
                case nameof(IDisplayDuplicateIndividual.MatchBirthLocation):
                    return ind.MatchBirthLocation;
                case nameof(IDisplayDuplicateIndividual.Score):
                    return ind.Score;
                case nameof(IDisplayDuplicateIndividual.IgnoreNonDuplicate):
                    return ind.IgnoreNonDuplicate;
            }
            return null;
        }
    }
}
