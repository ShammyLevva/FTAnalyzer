using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class DataError
    {
        public DataError(int errorType, Fact.FactError errorLevel, Individual ind, string description)
        {
            this.ErrorType = DataErrorGroup.ErrorDescription(errorType);
            this.Icon = FactImage.ErrorIcon(errorLevel).Icon;
            this.individual = ind;
            this.family = null;
            this.Description = description;
        }
        
        public DataError(int errorType, Individual ind, string description)
            : this(errorType, Fact.FactError.ERROR, ind, description) {}

        public DataError(int errorType, Family fam, string description)
            : this(errorType, Fact.FactError.ERROR, (Individual)null, description)
        {
            this.family = fam;
        }
                    
        private Individual individual;
        private Family family;
        
        public Image Icon { get; private set; }
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
