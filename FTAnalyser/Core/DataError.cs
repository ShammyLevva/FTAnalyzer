using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DataError
    {
        private Individual individual;
        public string ErrorType { get; private set; }
        public string Description { get; private set; }

        public DataError(int errorType, Individual ind, string description)
        {
            this.ErrorType = DataErrorGroup.ErrorDescription(errorType);
            this.individual = ind;
            this.Description = description;
        }

        public string IndividualID { get { return individual.IndividualID; } }

        public string Individual { get { return individual.Name; } }

        public FactDate Born { get { return individual.BirthDate; } }

        public FactDate Died { get { return individual.DeathDate; } }
    }
}
