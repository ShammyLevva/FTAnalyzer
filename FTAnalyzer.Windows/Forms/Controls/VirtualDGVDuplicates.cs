namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVDuplicates : VirtualDataGridView<IDisplayDuplicateIndividual>
    {
        protected override object GetValueFor(IDisplayDuplicateIndividual dup, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayDuplicateIndividual.IndividualID):
                    return dup.IndividualID;
                case nameof(IDisplayDuplicateIndividual.Name):
                    return dup.Name;
                case nameof(IDisplayDuplicateIndividual.Forenames):
                    return dup.Forenames;
                case nameof(IDisplayDuplicateIndividual.Surname):
                    return dup.Surname;
                case nameof(IDisplayDuplicateIndividual.BirthDate):
                    return dup.BirthDate;
                case nameof(IDisplayDuplicateIndividual.BirthLocation):
                    return dup.BirthLocation;
                case nameof(IDisplayDuplicateIndividual.Gender):
                    return dup.Gender;
                case nameof(IDisplayDuplicateIndividual.MatchIndividualID):
                    return dup.MatchIndividualID;
                case nameof(IDisplayDuplicateIndividual.MatchName):
                    return dup.MatchName;
                case nameof(IDisplayDuplicateIndividual.MatchBirthDate):
                    return dup.MatchBirthDate;
                case nameof(IDisplayDuplicateIndividual.MatchBirthLocation):
                    return dup.MatchBirthLocation;
                case nameof(IDisplayDuplicateIndividual.Score):
                    return dup.Score;
                case nameof(IDisplayDuplicateIndividual.IgnoreNonDuplicate):
                    return dup.IgnoreNonDuplicate;
                case nameof(IDisplayDuplicateIndividual.MatchGender):
                    return dup.MatchGender;
            }
            return null;
        }
    }
}
