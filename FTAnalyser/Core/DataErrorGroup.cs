using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DataErrorGroup
    {
        private string errorDescription;

        public DataErrorGroup(string description, IList<DataError> errors)
        {
            this.errorDescription = description;
            this.Errors = errors;
        }

        public override string ToString()
        {
            return errorDescription;
        }

        public IList<DataError> Errors { get; private set; }
    }
}
