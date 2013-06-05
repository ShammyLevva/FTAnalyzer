using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DataError
    {
        private Individual ind;
        private string description;

        public DataError(Individual ind, string description)
        {
            this.ind = ind;
            this.description = description;
        }

        public string Individual
        {
            get { return ind.Name; }
        }

        public FactDate Born
        {
            get { return ind.BirthDate; }
        }

        public string Description
        {
            get { return description; }
        }
    }
}
