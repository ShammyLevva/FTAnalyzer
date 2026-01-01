namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvIndividuals : VirtualDataGridView<IDisplayIndividual>
    {
        protected override object GetValueFor(IDisplayIndividual ind, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayIndividual.IndividualID) => ind.IndividualID,
                nameof(IDisplayIndividual.Forenames) => ind.Forenames,
                nameof(IDisplayIndividual.Surname) => ind.Surname,
                nameof(IDisplayIndividual.Gender) => ind.Gender,
                nameof(IDisplayIndividual.BirthDate) => ind.BirthDate,
                nameof(IDisplayIndividual.BirthLocation) => ind.BirthLocation,
                nameof(IDisplayIndividual.DeathDate) => ind.DeathDate,
                nameof(IDisplayIndividual.DeathLocation) => ind.DeathLocation,
                nameof(IDisplayIndividual.Occupation) => ind.Occupation,
                nameof(IDisplayIndividual.LifeSpan) => ind.LifeSpan,
                nameof(IDisplayIndividual.Relation) => ind.Relation,
                nameof(IDisplayIndividual.RelationToRoot) => ind.RelationToRoot,
                nameof(IDisplayIndividual.Title) => ind.Title,
                nameof(IDisplayIndividual.Suffix) => ind.Suffix,
                nameof(IDisplayIndividual.Alias) => ind.Alias,
                nameof(IDisplayIndividual.FamilySearchID) => ind.FamilySearchID,
                nameof(IDisplayIndividual.MarriageCount) => ind.MarriageCount,
                nameof(IDisplayIndividual.ChildrenCount) => ind.ChildrenCount,
                nameof(IDisplayIndividual.BudgieCode) => ind.BudgieCode,
                nameof(IDisplayIndividual.Ahnentafel) => ind.Ahnentafel,
                nameof(IDisplayIndividual.HasNotes) => ind.HasNotes,
                nameof(IDisplayIndividual.FactsCount) => ind.FactsCount,
                nameof(IDisplayIndividual.SourcesCount) => ind.SourcesCount,
                _ => ind.IndividualID,
            };
        }
    }
}
