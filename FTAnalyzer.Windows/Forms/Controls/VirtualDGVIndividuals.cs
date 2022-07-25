namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVIndividuals : VirtualDataGridView<IDisplayIndividual>
    {
        protected override object GetValueFor(IDisplayIndividual ind, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayIndividual.IndividualID):
                    return ind.IndividualID;
                case nameof(IDisplayIndividual.Forenames):
                    return ind.Forenames;
                case nameof(IDisplayIndividual.Surname):
                    return ind.Surname;
                case nameof(IDisplayIndividual.Gender):
                    return ind.Gender;
                case nameof(IDisplayIndividual.BirthDate):
                    return ind.BirthDate;
                case nameof(IDisplayIndividual.BirthLocation):
                    return ind.BirthLocation;
                case nameof(IDisplayIndividual.DeathDate):
                    return ind.DeathDate;
                case nameof(IDisplayIndividual.DeathLocation):
                    return ind.DeathLocation;
                case nameof(IDisplayIndividual.Occupation):
                    return ind.Occupation;
                case nameof(IDisplayIndividual.LifeSpan):
                    return ind.LifeSpan;
                case nameof(IDisplayIndividual.Relation):
                    return ind.Relation;
                case nameof(IDisplayIndividual.RelationToRoot):
                    return ind.RelationToRoot;
                case nameof(IDisplayIndividual.Title):
                    return ind.Title;
                case nameof(IDisplayIndividual.Suffix):
                    return ind.Suffix;
                case nameof(IDisplayIndividual.Alias):
                    return ind.Alias;
                case nameof(IDisplayIndividual.FamilySearchID):
                    return ind.FamilySearchID;
                case nameof(IDisplayIndividual.MarriageCount):
                    return ind.MarriageCount;
                case nameof(IDisplayIndividual.ChildrenCount):
                    return ind.ChildrenCount;
                case nameof(IDisplayIndividual.BudgieCode):
                    return ind.BudgieCode;
                case nameof(IDisplayIndividual.Ahnentafel):
                    return ind.Ahnentafel;
                case nameof(IDisplayIndividual.HasNotes):
                    return ind.HasNotes;
                case nameof(IDisplayIndividual.FactsCount):
                    return ind.FactsCount;
                case nameof(IDisplayIndividual.SourcesCount):
                    return ind.SourcesCount;
            }
            return null;
        }
    }
}
