using System;

namespace FTAnalyzer
{
    [Serializable]
    class FactDateException : Exception
    {
        public FactDateException(string message)
            : base(message)
        { }
    }
}
