using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DataError
    {
        private Individual individual;
        public string Description { get; private set; }

        public DataError(Individual ind, string description)
        {
            this.individual = ind;
            this.Description = description;
        }

        public string Individual { get { return individual.Name; } }

        public FactDate Born { get { return individual.BirthDate; } }
    }
}
