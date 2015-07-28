using System;
using System.Net;
using BruTile;
using BruTile.Web;
using BruTile.Cache;
namespace FTAnalyzer.Mapping
{
    public class BingOSTileSource : TileSource
    {
        public BingOSTileSource(String url, string token, BingOSMapType mapType)
            : this(new BingOSRequest(url, token, mapType))
        {
        }

        public BingOSTileSource(BingOSRequest bingRequest)
            : base(new WebTileProvider(new BingOSRequest("http://ecn.t3.tiles.virtualearth.net/tiles/r{quadkey}.png?g=41&productSet=mmOS", "")),
                new GlobalSphericalMercator("png", true, 1, 19, "Bing"))
        {
        }
    }
}
