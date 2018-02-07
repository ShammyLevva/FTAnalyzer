using System;

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
