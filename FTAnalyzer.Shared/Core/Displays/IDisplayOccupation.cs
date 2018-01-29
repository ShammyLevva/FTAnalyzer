using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayOccupation : IComparable<IDisplayOccupation>
    {
        string Occupation { get; }
        int Count { get; }
    }
}
