using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            switch(type)
            {
                case TileType.OpenStreetMap:
                    return KnownTileSources.Create(KnownTileSource.OpenStreetMap);
                case TileType.OpenHistoricMap:
                    return new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                "http://nls-3.tileserver.com/nls/{z}/{x}/{y}.jpg",
                                new[] { "a", "b", "c" }, "NLS");
                case TileType.BingAerial:
                    return KnownTileSources.Create(KnownTileSource.BingAerial);
                case TileType.BingRoads:
                    return KnownTileSources.Create(KnownTileSource.BingRoads);
                case TileType.BingHybrid:
                    return KnownTileSources.Create(KnownTileSource.BingHybrid);
                case TileType.NLS_1843_1882_OS_6in:
                    return new HttpTileSource(new GlobalSphericalMercator(1, 16),
                                "http://geo.nls.uk/mapdata3/os/6inchfirst/{z}/{x}/{y}.png",
                                new[] { "a", "b", "c" }, "NLS");
                case TileType.NLS_1885_1900_OS_1in:
                    return new HttpTileSource(new GlobalSphericalMercator(3, 15),
                                "http://geo.nls.uk/maps/os/1inch_2nd_ed/{z}/{x}/{y}.png",
                                new[] { "a", "b", "c" }, "NLS");
                case TileType.NLS_1921_1930_OS_6in:
                    return new HttpTileSource(new GlobalSphericalMercator(3, 15),
                                "http://geo.nls.uk/maps/os/popular/{z}/{x}/{y}.png",
                                new[] { "a", "b", "c" }, "NLS");
                default:
                    return KnownTileSources.Create(KnownTileSource.OpenStreetMap);
            }
        }
    }
}
