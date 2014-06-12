using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FTAnalyzer.Mapping;

namespace FTAnalyzer
{
    public interface IDisplayGeocodedLocation
    {
        string SortableLocation { get; }
        double Latitude { get; }
        double Longitude { get; }
        Image Icon { get; }
        string Geocoded { get; }
        string FoundLocation { get; }
        string FoundResultType { get; }
        GeoResponse.CResult.CGeometry.CViewPort ViewPort { get; }
    }
}
