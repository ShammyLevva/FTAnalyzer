using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class InvalidXMLFactException : Exception
    {
        public InvalidXMLFactException(string message)
            : base(message)
        { }
    }
}
