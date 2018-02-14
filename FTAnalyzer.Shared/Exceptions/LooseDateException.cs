using System;

namespace FTAnalyzer
{
    [Serializable]
    public class LooseDataException : Exception
    {
        public LooseDataException(string message)
            : base(message)
        { }
    }
}
