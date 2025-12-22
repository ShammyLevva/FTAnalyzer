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
            BingHybrid,
            NLS_1843_1882_OS_6in,
            NLS_1885_1900_OS_1in,
            NLS_1921_1930_OS_6in
        }

        public virtual HttpTileSource CreateTileSource(TileType type)
        {
            return type switch
            {
                TileType.OpenStreetMap => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
                TileType.OpenHistoricMap => new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                                "http://nls-3.tileserver.com/nls/{z}/{x}/{y}.jpg",
                                                new[] { "a", "b", "c" }, "NLS"),
                TileType.BingAerial => KnownTileSources.Create(KnownTileSource.BingAerial),
                TileType.BingRoads => KnownTileSources.Create(KnownTileSource.BingRoads),
                TileType.BingHybrid => KnownTileSources.Create(KnownTileSource.BingHybrid),
                TileType.NLS_1843_1882_OS_6in => new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                                "http://geo.nls.uk/mapdata3/os/6inchfirst/{z}/{x}/{y}.png",
                                                new[] { "a", "b", "c" }, "NLS"),
                TileType.NLS_1885_1900_OS_1in => new HttpTileSource(new GlobalSphericalMercator(3, 15),
                                                "http://geo.nls.uk/maps/os/1inch_2nd_ed/{z}/{x}/{y}.png",
                                                new[] { "a", "b", "c" }, "NLS"),
                TileType.NLS_1921_1930_OS_6in => new HttpTileSource(new GlobalSphericalMercator(3, 15),
                                                "http://geo.nls.uk/maps/os/popular/{z}/{x}/{y}.png",
                                                new[] { "a", "b", "c" }, "NLS"),
                _ => KnownTileSources.Create(KnownTileSource.OpenStreetMap),
            };
        }
    }
}
