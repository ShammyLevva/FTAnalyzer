using System;

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
