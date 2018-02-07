using FTAnalyzer.Mapping;

namespace FTAnalyzer
{
    public interface IDisplayGeocodedLocation
    {
        string SortableLocation { get; }
        double Latitude { get; }
        double Longitude { get; }
#if !__MAC__
        System.Drawing.Image Icon { get; }
#endif
        string Geocoded { get; }
        string FoundLocation { get; }
        string FoundResultType { get; }
        GeoResponse.CResult.CGeometry.CViewPort ViewPort { get; }
    }
}
