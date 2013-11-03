using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BruTile.Web;

namespace FTAnalyzer.Mapping
{

    public class NLSTileServerConfig : OsmTileServerConfig
    {

        private OsmTileServerConfig mDefault;

        public NLSTileServerConfig()
            : base("http://t{0}.cz.tileserver.com/nls/{1}/{2}/{3}.jpg", 5, new[] { "0", "1", "2", "3", "4" }, 0, 14)
        {
            mDefault = OsmTileServerConfig.Create(KnownTileServers.Mapnik, null);
        }

        public override Uri GetUri(BruTile.TileIndex tileIndex)
        {
            if (tileIndex.Level < 7 || tileIndex.Level > 14)
                return mDefault.GetUri(tileIndex);
            return base.GetUri(tileIndex);
        }
    }

    public class NLSRequest : OsmRequest
    {
        public NLSRequest()
            : base(new NLSTileServerConfig( ))
        { }


    }
}
