using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    [Serializable]
    class BadFamilySearchDataException : Exception
    {
        public BadFamilySearchDataException(string message)
            : base(message)
        { }
    }
}
