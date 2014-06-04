using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Regions
    {
        public static ISet<Region> SCOTTISH_REGIONS;
        public static ISet<Region> ENGLISH_REGIONS;
        public static ISet<Region> WELSH_REGIONS;
        public static ISet<Region> UK_REGIONS;
        public static ISet<string> VALID_REGIONS;

        #region Scottish Regions
        public static Region ABERDEEN = new Region(Countries.SCOTLAND, "Aberdeen");
        public static Region ANGUS = new Region(Countries.SCOTLAND, "Angus");
        public static Region ARGYLL = new Region(Countries.SCOTLAND, "Argyll");
        public static Region AYR = new Region(Countries.SCOTLAND, "Ayr");
        public static Region BANFF = new Region(Countries.SCOTLAND, "Banff");
        public static Region BERWICK = new Region(Countries.SCOTLAND, "Berwick");
        public static Region BUTE = new Region(Countries.SCOTLAND, "Bute");
        public static Region CAITHNESS = new Region(Countries.SCOTLAND, "Caithness");
        public static Region CLACKMANNAN = new Region(Countries.SCOTLAND, "Clackmannan");
        public static Region DUMFRIES = new Region(Countries.SCOTLAND, "Dumfries");
        public static Region DUNBARTON = new Region(Countries.SCOTLAND, "Dunbarton");
        public static Region EAST_LOTHIAN = new Region(Countries.SCOTLAND, "East Lothian");
        public static Region FIFE = new Region(Countries.SCOTLAND, "Fife");
        public static Region INVERNESS = new Region(Countries.SCOTLAND, "Inverness");
        public static Region KINCARDINE = new Region(Countries.SCOTLAND, "Kincardine");
        public static Region KINROSS = new Region(Countries.SCOTLAND, "Kinross");
        public static Region KIRKCUDBRIGHT = new Region(Countries.SCOTLAND, "Kirkcudbright");
        public static Region LANARK = new Region(Countries.SCOTLAND, "Lanark");
        public static Region MIDLOTHIAN = new Region(Countries.SCOTLAND, "Midlothian");
        public static Region MORAY = new Region(Countries.SCOTLAND, "Moray");
        public static Region NAIRN = new Region(Countries.SCOTLAND, "Nairn");
        public static Region ORKNEY = new Region(Countries.SCOTLAND, "Orkney");
        public static Region PEEBLES = new Region(Countries.SCOTLAND, "Peebles");
        public static Region PERTH = new Region(Countries.SCOTLAND, "Perth");
        public static Region RENFREW = new Region(Countries.SCOTLAND, "Renfrew");
        public static Region ROSS_CROMARTY = new Region(Countries.SCOTLAND, "Ross and Cromarty");
        public static Region ROXBURGH = new Region(Countries.SCOTLAND, "Roxburgh");
        public static Region SELKIRK = new Region(Countries.SCOTLAND, "Selkirk");
        public static Region SHETLAND = new Region(Countries.SCOTLAND, "Shetland");
        public static Region STIRLING = new Region(Countries.SCOTLAND, "Stirling");
        public static Region SUTHERLAND = new Region(Countries.SCOTLAND, "Sutherland");
        public static Region WEST_LOTHIAN = new Region(Countries.SCOTLAND, "West Lothian");
        public static Region WIGTOWN = new Region(Countries.SCOTLAND, "Wigtown");
        #endregion

        static Regions()
        {
            SCOTTISH_REGIONS = new HashSet<Region>();
            // List from Scotland's People
            SCOTTISH_REGIONS.UnionWith(new Region[]{
                    ABERDEEN, ANGUS, ARGYLL, AYR, BANFF, BERWICK, BUTE, CAITHNESS, CLACKMANNAN, DUMFRIES,
                    DUNBARTON, EAST_LOTHIAN, FIFE, INVERNESS, KINCARDINE, KINROSS, KIRKCUDBRIGHT, LANARK, 
                    MIDLOTHIAN, MORAY, NAIRN, ORKNEY, PEEBLES, PERTH, RENFREW, ROSS_CROMARTY, ROXBURGH, 
                    SELKIRK, SHETLAND, STIRLING, SUTHERLAND, WEST_LOTHIAN, WIGTOWN
                });
            AddScottishRegionAlternates();

            ENGLISH_REGIONS = new HashSet<Region>();


            WELSH_REGIONS = new HashSet<Region>();

            UK_REGIONS = new HashSet<Region>();
            UK_REGIONS.UnionWith(SCOTTISH_REGIONS);
            UK_REGIONS.UnionWith(ENGLISH_REGIONS);
            UK_REGIONS.UnionWith(WELSH_REGIONS);
            AppendValidRegions(UK_REGIONS);
        }

        private static void AppendValidRegions(ISet<Region> regions)
        {
            foreach(Region r in regions)
            {
                VALID_REGIONS.Add(r.PreferredName);
                foreach (string alternate in r.AlternativeNames)
                    VALID_REGIONS.Add(alternate);
            }
        }

        private static void AddScottishRegionAlternates()
        {
            // add Anglicised shires
            ABERDEEN.AddAlternateName("Aberdeenshire");
            ANGUS.AddAlternateName("Forfarshire");
            ARGYLL.AddAlternateName("Argyllshire");
            AYR.AddAlternateName("Ayrshire");
            BANFF.AddAlternateName("Banffshire");
            BERWICK.AddAlternateName("Berwickshire");
            BUTE.AddAlternateName("Buteshire");
            CLACKMANNAN.AddAlternateName("Clackmannanshire");
            DUMFRIES.AddAlternateName("Dumfries-shire");
            DUMFRIES.AddAlternateName("Dumfriesshire");
            DUNBARTON.AddAlternateName("Dunbartonshire");
            EAST_LOTHIAN.AddAlternateName("Haddingtonshire");
            FIFE.AddAlternateName("Fifeshire");
            INVERNESS.AddAlternateName("Inverness-shire");
            INVERNESS.AddAlternateName("Invernessshire");
            KINCARDINE.AddAlternateName("Kincardineshire");
            KINROSS.AddAlternateName("Kinross-shire");
            KINROSS.AddAlternateName("Kinrossshire");
            KIRKCUDBRIGHT.AddAlternateName("Kirkcudbrightshire");
            LANARK.AddAlternateName("Lanarkshire");
            MORAY.AddAlternateName("Elginshire");
            NAIRN.AddAlternateName("Nairnshire");
            PEEBLES.AddAlternateName("Peebles-shire");
            PEEBLES.AddAlternateName("Peeblesshire");
            PERTH.AddAlternateName("Perthshire");
            RENFREW.AddAlternateName("Renfrewshire");
            ROXBURGH.AddAlternateName("Roxburghshire");
            SELKIRK.AddAlternateName("Selkirkshire");
            STIRLING.AddAlternateName("Stirlingshire");
            WEST_LOTHIAN.AddAlternateName("Linlithgowshire");
            WIGTOWN.AddAlternateName("Wigtownshire");
            // alternate names
            SHETLAND.AddAlternateName("Zetland");
            ROSS_CROMARTY.AddAlternateName("Ross & Cromarty");
            FIFE.AddAlternateName("Kingdom of Fife");
        }

        
    }
}
