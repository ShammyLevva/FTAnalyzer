using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BruTile.Web;

namespace FTAnalyzer.Mapping
{
    public class BingOSTileSource : BingTileSource
    {
        public BingOSTileSource() :
            base(BingRequest.UrlBing, null, BingMapType.Roads) 
        {}
    }
}
