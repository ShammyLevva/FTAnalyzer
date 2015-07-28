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
            mDefault = new OsmTileServerConfig("http://{0}.tile.openstreetmap.org/{1}/{2}/{3}.png", 3, new[] { "a", "b", "c" }, 0, 18);
        }

        public override Uri GetUri(BruTile.TileIndex tileIndex)
        {
            var level = Convert.ToInt32(tileIndex.Level);
            if (level < 7 || level > 14)
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
