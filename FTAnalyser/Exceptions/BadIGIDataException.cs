using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    class BadIGIDataException : Exception
    {
        public BadIGIDataException(string message)
            : base(message)
        { }
    }
}
