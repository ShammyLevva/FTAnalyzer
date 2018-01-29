using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    [Serializable]
    class InvalidLocationCSVFileException : Exception
    {
        public InvalidLocationCSVFileException(string message)
            : base(message)
        { }
    }
}
