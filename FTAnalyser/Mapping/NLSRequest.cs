using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BruTile.Web;

namespace FTAnalyzer.Mapping
{
    public class NLSRequest : OsmRequest
    {
        public NLSRequest()
            : base(new OsmTileServerConfig("http://t{0}.cz.tileserver.com/nls/{1}/{2}/{3}.jpg", 5, new[] { "0", "1", "2", "3", "4" }, 1, 14))
        { }
    }
}
