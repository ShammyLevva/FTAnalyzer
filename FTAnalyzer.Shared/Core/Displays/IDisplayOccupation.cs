using System;

namespace FTAnalyzer
{
    public interface IDisplayOccupation : IComparable<IDisplayOccupation>
    {
        string Occupation { get; }
        int Count { get; }
    }
}
