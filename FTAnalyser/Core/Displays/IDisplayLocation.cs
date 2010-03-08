using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayLocation
    {
        string Country { get; }
        string Region { get; }
        string Parish { get; }
        string Address { get; }
        string Place { get; }
        int Level { get; }

        int CompareTo(IDisplayLocation loc, int level);
    }
}
