using BruTile.Predefined;
using BruTile.Web;
using FTAnalyzer.Shared.Utilities;

namespace FTAnalyzer.Mapping
{
    public class TileSourceFactory
    {
        public enum TileType
        {
            OpenStreetMap,
            OpenHistoricMap,
            BingAerial,
            BingRoads,
            BingHybrid,
            UsgsHistorical
        }

        public virtual HttpTileSource CreateTileSource(TileType type) => CreateTileSource(type, null);

        // usgsRequest is supplied by the caller (rather than constructed internally) when type is
        // UsgsHistorical, so the caller can keep a reference to mutate ArcGisImageServerRequest's
        // HistoricalYear later without needing to unwrap it back out of the returned HttpTileSource.
        public virtual HttpTileSource CreateTileSource(TileType type, ArcGisImageServerRequest? usgsRequest)
        {
            return type switch
            {
                TileType.OpenStreetMap => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
                TileType.OpenHistoricMap => new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                                "http://geo.nls.uk/mapdata3/os/6inchfirst/{z}/{x}/{y}.png",null, "NLS"),
                TileType.BingAerial => KnownTileSources.Create(KnownTileSource.BingAerial),
                TileType.BingRoads => KnownTileSources.Create(KnownTileSource.BingRoads),
                TileType.BingHybrid => KnownTileSources.Create(KnownTileSource.BingHybrid),
                TileType.UsgsHistorical => new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                                usgsRequest ?? CreateUsgsHistoricalRequest(), "USGS"),

                _ => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
            };
        }

        public static ArcGisImageServerRequest CreateUsgsHistoricalRequest() => new(UsgsHistoricalMap.ImageServerUrl);
    }
}
