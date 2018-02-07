namespace FTAnalyzer
{
    public class DataError
    {
        public DataError(int errorType, Fact.FactError errorLevel, Individual ind, string description)
        {
            ErrorType = DataErrorGroup.ErrorDescription(errorType);
            Icon = FactImage.ErrorIcon(errorLevel).Icon;
            individual = ind;
            family = null;
            Description = description;
        }
        
        public DataError(int errorType, Individual ind, string description)
            : this(errorType, Fact.FactError.ERROR, ind, description) {}

        public DataError(int errorType, Family fam, string description)
            : this(errorType, Fact.FactError.ERROR, (Individual)null, description)
        {
            family = fam;
        }
                    
        private Individual individual;
        private Family family;

#if !__MAC__
        public System.Drawing.Image Icon { get; private set; }
#endif
        public string ErrorType { get; private set; }
        public string Reference { get { return individual == null ? family.FamilyID : individual.IndividualID; } }
        public string Name { get { return individual == null ? family.FamilyName : individual.Name; } }
        public string Description { get; private set; }
        public FactDate Born { get { return individual == null ? FactDate.UNKNOWN_DATE : individual.BirthDate; } }
        public FactDate Died { get { return individual == null ? FactDate.UNKNOWN_DATE : individual.DeathDate; } }

        public bool IsFamily()
        {
            return individual == null;
        }
    }
}
