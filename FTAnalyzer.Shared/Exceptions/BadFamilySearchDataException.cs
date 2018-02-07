using System;

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
