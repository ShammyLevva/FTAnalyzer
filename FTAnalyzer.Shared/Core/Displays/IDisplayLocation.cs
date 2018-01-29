using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FTAnalyzer
{
    public interface IDisplayLocation
    {
        string Country { get; }
        string Region { get; }
        string SubRegion { get; }
        string Address { get; }
        string Place { get; }
        double Latitude { get; }
        double Longitude { get; }
        Image Icon { get; }
        string Geocoded { get; }
        string FoundLocation { get; }

        int CompareTo(IDisplayLocation loc, int level);
        FactLocation GetLocation(int level);

    }
}
