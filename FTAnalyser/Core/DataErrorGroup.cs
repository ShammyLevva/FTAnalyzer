using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DataErrorGroup
    {
        string errorDescription;
        List<DataError> errors;

        public DataErrorGroup(string description, List<DataError> errors)
        {
            this.errorDescription = description;
            this.errors = errors;
        }

        public override string ToString()
        {
            return errorDescription;
        }

        public List<DataError> Errors { get { return errors; } }
    }
}
