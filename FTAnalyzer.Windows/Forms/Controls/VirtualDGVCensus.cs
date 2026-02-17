namespace FTAnalyzer.Forms.Controls
{
    class VirtualDgvCensus : VirtualDataGridView<IDisplayCensus>
    {
        protected override object GetValueFor(IDisplayCensus census, string propertyName)
        {
            return propertyName switch
            {
                nameof(IDisplayCensus.FamilyID) => census.FamilyID,
                nameof(IDisplayCensus.Position) => census.Position,
                nameof(IDisplayCensus.IndividualID) => census.IndividualID,
                nameof(IDisplayCensus.CensusLocation) => census.CensusLocation,
                nameof(IDisplayCensus.CensusName) => census.CensusName,
                nameof(IDisplayCensus.Age) => census.Age,
                nameof(IDisplayCensus.Occupation) => census.Occupation,
                nameof(IDisplayCensus.BirthDate) => census.BirthDate,
                nameof(IDisplayCensus.BirthLocation) => census.BirthLocation,
                nameof(IDisplayCensus.DeathDate) => census.DeathDate,
                nameof(IDisplayCensus.DeathLocation) => census.DeathLocation,
                nameof(IDisplayCensus.CensusStatus) => census.CensusStatus,
                nameof(IDisplayCensus.Relation) => census.Relation,
                nameof(IDisplayCensus.RelationToRoot) => census.RelationToRoot,
                nameof(IDisplayCensus.CensusRef) => census.CensusRef,
                nameof(IDisplayCensus.Census) => census.Census,
                nameof(IDisplayCensus.Ahnentafel) => census.Ahnentafel,
                _ => census.IndividualID,
            };
        }
    }
}
