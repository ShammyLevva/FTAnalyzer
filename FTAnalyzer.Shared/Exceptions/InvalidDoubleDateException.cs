using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    [Serializable]
    public class InvalidDoubleDateException : Exception
    {
        public InvalidDoubleDateException(string message)
            : base(message)
        { }
    }
}
