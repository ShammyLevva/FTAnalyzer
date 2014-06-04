using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Regions
    {
        public static List<Region> SCOTTISH_REGIONS;
        public static List<Region> ENGLISH_REGIONS;
        public static List<Region> WELSH_REGIONS;
        public static List<Region> UK_REGIONS;

        static Regions()
        {
            SCOTTISH_REGIONS = new List<Region>();
            // List from Scotland's People
            Region ABERDEEN = new Region(Countries.SCOTLAND, "Aberdeen");
		    Region ABERDEEN_CITY = new Region(Countries.SCOTLAND, "Aberdeen City");
		    Region ANGUS = new Region(Countries.SCOTLAND, "Angus");
		    Region ARGYLL = new Region(Countries.SCOTLAND, "Argyll");
		    Region AYR = new Region(Countries.SCOTLAND, "Ayr");
		    Region BANFF = new Region(Countries.SCOTLAND, "Banff");
		    Region BERWICK = new Region(Countries.SCOTLAND, "Berwick");
		    Region BUTE = new Region(Countries.SCOTLAND, "Bute");
		    Region CAITHNESS = new Region(Countries.SCOTLAND, "Caithness");
		    Region CLACKMANNAN = new Region(Countries.SCOTLAND, "Clackmannan");
		    Region DUMFRIES = new Region(Countries.SCOTLAND, "Dumfries");
		    Region DUNBARTON = new Region(Countries.SCOTLAND, "Dunbarton");
		    Region DUNDEE_CITY = new Region(Countries.SCOTLAND, "Dundee City");
		    Region EAST_LOTHIAN = new Region(Countries.SCOTLAND, "East Lothian");
		    Region EDINBURGH_CITY = new Region(Countries.SCOTLAND, "Edinburugh City");
		    Region FIFE = new Region(Countries.SCOTLAND, "Fife");
		    Region GLASGOW_CITY = new Region(Countries.SCOTLAND, "Glasgow City");
		    Region INVERNESS = new Region(Countries.SCOTLAND, "Inverness");
		    Region KINCARDINE = new Region(Countries.SCOTLAND, "Kincardine");
		    Region KINROSS = new Region(Countries.SCOTLAND, "Kinross");
		    Region KIRKCUDBRIGHT = new Region(Countries.SCOTLAND, "Kirkcudbright");
		    Region LANARK = new Region(Countries.SCOTLAND, "Lanark");
		    Region MIDLOTHIAN = new Region(Countries.SCOTLAND, "Midlothian");
		    Region MORAY = new Region(Countries.SCOTLAND, "Moray");
		    Region NAIRN = new Region(Countries.SCOTLAND, "Nairn");
		    Region ORKNEY = new Region(Countries.SCOTLAND, "Orkney");
		    Region PEEBLES = new Region(Countries.SCOTLAND, "Peebles");
		    Region PERTH = new Region(Countries.SCOTLAND, "Perth");
		    Region RENFREW = new Region(Countries.SCOTLAND, "Renfrew");
		    Region ROSS_CROMARTY = new Region(Countries.SCOTLAND, "Ross and Cromarty");
		    Region ROXBURGH = new Region(Countries.SCOTLAND, "Roxburgh");
		    Region SELKIRK = new Region(Countries.SCOTLAND, "Selkirk");
		    Region SHETLAND = new Region(Countries.SCOTLAND, "Shetland");
		    Region STIRLING = new Region(Countries.SCOTLAND, "Stirling");
		    Region SUTHERLAND = new Region(Countries.SCOTLAND, "Sutherland");
		    Region WEST_LOTHIAN = new Region(Countries.SCOTLAND, "West Lothian");
		    Region WIGTOWN = new Region(Countries.SCOTLAND, "Wigtown");
            SCOTTISH_REGIONS.AddRange(new Region[]{
                    ABERDEEN, ABERDEEN_CITY, ANGUS, ARGYLL, AYR, BANFF, BERWICK, BUTE, CAITHNESS, CLACKMANNAN, DUMFRIES,
                    DUNBARTON, DUNDEE_CITY, EAST_LOTHIAN, EDINBURGH_CITY, FIFE, GLASGOW_CITY, INVERNESS, KINCARDINE, 
                    KINROSS, KIRKCUDBRIGHT, LANARK, MIDLOTHIAN, MORAY, NAIRN, ORKNEY, PEEBLES, PERTH, RENFREW, 
                    ROSS_CROMARTY, ROXBURGH, SELKIRK, SHETLAND, STIRLING, SUTHERLAND, WEST_LOTHIAN, WIGTOWN
                });
            AddScottishRegionAlternates();

            ENGLISH_REGIONS = new List<Region>();


            WELSH_REGIONS = new List<Region>();

            UK_REGIONS = new List<Region>();
            UK_REGIONS.AddRange(SCOTTISH_REGIONS);
            UK_REGIONS.AddRange(ENGLISH_REGIONS);
            UK_REGIONS.AddRange(WELSH_REGIONS);
        }

        private static void AddScottishRegionAlternates()
        {
            throw new NotImplementedException();
        }
    }
}
