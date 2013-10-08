using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FTAnalyzer
{
    public interface IDisplayGeocodedLocation
    {
        string SortableLocation { get; }
        double Latitude { get; }
        double Longitude { get; }
        Image Icon { get; }
        string Geocoded { get; }
        string GoogleLocation { get; }
        string GoogleResultType { get; }
    }
}
