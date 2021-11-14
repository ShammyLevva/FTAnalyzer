namespace FTAnalyzer.Forms.Controls
{
    class VirtualDGVCustomFacts : VirtualDataGridView<IDisplayCustomFact>
    {
        protected override object GetValueFor(IDisplayCustomFact fact, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IDisplayCustomFact.CustomFactName):
                    return fact.CustomFactName;
                case nameof(IDisplayCustomFact.IndividualCount):
                    return fact.IndividualCount;
                case nameof(IDisplayCustomFact.FamilyCount):
                    return fact.FamilyCount;
                case nameof(IDisplayCustomFact.Ignore):
                    return fact.Ignore;
            }
            return null;
        }
    }
}
