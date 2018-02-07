#if !__MAC__
using System.Drawing;
using FTAnalyzer.Mapping;
#endif

namespace FTAnalyzer
{
    public interface IDisplayGeocodedLocation
    {
        string SortableLocation { get; }
        double Latitude { get; }
        double Longitude { get; }
#if !__MAC__
        Image Icon { get; }
#endif
        string Geocoded { get; }
        string FoundLocation { get; }
        string FoundResultType { get; }
#if !__MAC__
        GeoResponse.CResult.CGeometry.CViewPort ViewPort { get; }
#endif
    }
}
