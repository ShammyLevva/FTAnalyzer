using System;
using System.Net;
using BruTile;
using BruTile.Web;
using BruTile.Cache;
using BruTile.Predefined;

namespace FTAnalyzer.Mapping
{
    public class BingOSTileSource : TileSource
    {
        public BingOSTileSource(String url, string token, BingMapType mapType)
            : this(new BingRequest(url, token, mapType))
        {
        }

        public BingOSTileSource(
                        BingRequest bingRequest,
                        IPersistentCache<byte[]> persistentCache = null)
            : base(new WebTileProvider(new BingRequest("http://ecn.t3.tiles.virtualearth.net/tiles/r{quadkey}.png?g=41&productSet=mmOS", "")),
                new GlobalSphericalMercator("png", true, 1, 19, "Bing"))
        {
        }
    }
}
