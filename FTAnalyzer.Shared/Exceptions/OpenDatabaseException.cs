using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    [Serializable]
    class OpenDatabaseException : Exception
    {
        public OpenDatabaseException(string message)
            : base(message)
        { }
    }
}
