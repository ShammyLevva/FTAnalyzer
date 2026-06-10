using BruTile.Predefined;
using BruTile.Web;

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
            BingHybrid
        }

        public virtual HttpTileSource CreateTileSource(TileType type)
        {
            return type switch
            {
                TileType.OpenStreetMap => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
                TileType.OpenHistoricMap => new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                                "http://geo.nls.uk/mapdata3/os/6inchfirst/{z}/{x}/{y}.png",null, "NLS"),
                TileType.BingAerial => KnownTileSources.Create(KnownTileSource.BingAerial),
                TileType.BingRoads => KnownTileSources.Create(KnownTileSource.BingRoads),
                TileType.BingHybrid => KnownTileSources.Create(KnownTileSource.BingHybrid),

                _ => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
            };
        }
    }
}
