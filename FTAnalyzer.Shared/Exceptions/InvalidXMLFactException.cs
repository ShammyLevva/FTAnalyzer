using System;

namespace FTAnalyzer
{
    [Serializable]
    class InvalidXMLFactException : Exception
    {
        public InvalidXMLFactException(string message)
            : base(message)
        { }
    }
}
