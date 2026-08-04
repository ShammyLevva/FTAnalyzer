using BruTile;
using BruTile.Web;

namespace FTAnalyzer.Mapping
{
    // Wraps an Esri ImageServer's "exportImage" REST operation as a BruTile IUrlBuilder, so it can
    // back a normal HttpTileSource/TileAsyncLayer even though it isn't a {z}/{x}/{y} tile pyramid.
    // HistoricalYear is mutable (rather than a constructor arg) because BruTile's HttpTileSource is
    // built once up front - the caller sets this property and rebuilds the TileAsyncLayer to pick up
    // a new year, mirroring the web app's source.updateParams()+refresh() at the OpenLayers level.
    public class ArcGisImageServerRequest(string baseUrl) : IUrlBuilder
    {
        // Left null shows the service's own "best available" mosaic for the current view - most
        // individual quads were only surveyed in a handful of discrete years, so defaulting to one
        // specific year usually renders blank.
        public int? HistoricalYear { get; set; }

        public Uri GetUrl(TileInfo info)
        {
            Extent extent = info.Extent;
            string time = HistoricalYear is int year
                ? $"&time={new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds()}," +
                  $"{new DateTimeOffset(year, 12, 31, 23, 59, 59, TimeSpan.Zero).ToUnixTimeMilliseconds()}"
                : string.Empty;

            return new Uri($"{baseUrl}/exportImage?bbox={extent.MinX},{extent.MinY},{extent.MaxX},{extent.MaxY}" +
                $"&bboxSR=3857&imageSR=3857&size=256,256&format=png32&f=image{time}");
        }
    }
}
