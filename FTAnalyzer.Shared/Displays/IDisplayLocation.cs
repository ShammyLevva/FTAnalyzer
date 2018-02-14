#if !__MACOS__
using System.Drawing;
#endif

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
#if !__MACOS__
        Image Icon { get; }
#endif
        string Geocoded { get; }
        string FoundLocation { get; }

        int CompareTo(IDisplayLocation loc, int level);
        FactLocation GetLocation(int level);

    }
}
