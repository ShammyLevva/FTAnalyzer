using System;

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
