using System;

namespace FTAnalyzer
{
    [Serializable]
    public class CensusSearchException : Exception
    {
        public CensusSearchException(string message)
            : base(message)
        { }
    }
}
