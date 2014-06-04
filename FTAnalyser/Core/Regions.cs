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
        public static Region ABERDEEN = new Region(Countries.SCOTLAND, "Aberdeenshire");
        public static Region ANGUS = new Region(Countries.SCOTLAND, "Angus");
        public static Region ARGYLL = new Region(Countries.SCOTLAND, "Argyllshire");
        public static Region AYR = new Region(Countries.SCOTLAND, "Ayrshire");
        public static Region BANFF = new Region(Countries.SCOTLAND, "Banffshire");
        public static Region BERWICK = new Region(Countries.SCOTLAND, "Berwickshire");
        public static Region BUTE = new Region(Countries.SCOTLAND, "Buteshire");
        public static Region CAITHNESS = new Region(Countries.SCOTLAND, "Caithness");
        public static Region CLACKMANNAN = new Region(Countries.SCOTLAND, "Clackmannanshire");
        public static Region DUMFRIES = new Region(Countries.SCOTLAND, "Dumfries-shire");
        public static Region DUNBARTON = new Region(Countries.SCOTLAND, "Dunbartonshire");
        public static Region EAST_LOTHIAN = new Region(Countries.SCOTLAND, "East Lothian");
        public static Region FIFE = new Region(Countries.SCOTLAND, "Fife");
        public static Region INVERNESS = new Region(Countries.SCOTLAND, "Inverness-shire");
        public static Region KINCARDINE = new Region(Countries.SCOTLAND, "Kincardineshire");
        public static Region KINROSS = new Region(Countries.SCOTLAND, "Kinross-shire");
        public static Region KIRKCUDBRIGHT = new Region(Countries.SCOTLAND, "Kirkcudbrightshire");
        public static Region LANARK = new Region(Countries.SCOTLAND, "Lanarkshire");
        public static Region MIDLOTHIAN = new Region(Countries.SCOTLAND, "Midlothian");
        public static Region MORAY = new Region(Countries.SCOTLAND, "Moray");
        public static Region NAIRN = new Region(Countries.SCOTLAND, "Nairn");
        public static Region ORKNEY = new Region(Countries.SCOTLAND, "Orkney");
        public static Region PEEBLES = new Region(Countries.SCOTLAND, "Peebles");
        public static Region PERTH = new Region(Countries.SCOTLAND, "Perthshire");
        public static Region RENFREW = new Region(Countries.SCOTLAND, "Renfrewshire");
        public static Region ROSS_CROMARTY = new Region(Countries.SCOTLAND, "Ross and Cromarty");
        public static Region ROXBURGH = new Region(Countries.SCOTLAND, "Roxburgh");
        public static Region SELKIRK = new Region(Countries.SCOTLAND, "Selkirk");
        public static Region SHETLAND = new Region(Countries.SCOTLAND, "Shetland");
        public static Region STIRLING = new Region(Countries.SCOTLAND, "Stirlingshire");
        public static Region SUTHERLAND = new Region(Countries.SCOTLAND, "Sutherland");
        public static Region WEST_LOTHIAN = new Region(Countries.SCOTLAND, "West Lothian");
        public static Region WIGTOWN = new Region(Countries.SCOTLAND, "Wigtownshire");
        #endregion

        #region English Regions
        public static Region BEDS = new Region(Countries.ENGLAND, "Bedfordshire");
        public static Region BERKS = new Region(Countries.ENGLAND, "Berkshire");
        public static Region BUCKS = new Region(Countries.ENGLAND, "Buckinghamshire");
        public static Region CAMBS = new Region(Countries.ENGLAND, "Cambridgeshire");
        public static Region CHESHIRE = new Region(Countries.ENGLAND, "Cheshire");
        public static Region CORNWALL = new Region(Countries.ENGLAND, "Cornwall");
        public static Region CUMBERLAND = new Region(Countries.ENGLAND, "Cumberland");
        public static Region DERBY = new Region(Countries.ENGLAND, "Derbyshire");
        public static Region DEVON = new Region(Countries.ENGLAND, "Devon");
        public static Region DORSET = new Region(Countries.ENGLAND, "Dorset");
        public static Region DURHAM = new Region(Countries.ENGLAND, "Durham");
        public static Region ESSEX = new Region(Countries.ENGLAND, "Essex");
        public static Region GLOUCS = new Region(Countries.ENGLAND, "Gloucestershire");
        public static Region HAMPS = new Region(Countries.ENGLAND, "Hampshire");
        public static Region HEREFORD = new Region(Countries.ENGLAND, "Herefordshire");
        public static Region HERTS = new Region(Countries.ENGLAND, "Hertfordshire");
        public static Region HUNTS = new Region(Countries.ENGLAND, "Huntingdonshire");
        public static Region KENT = new Region(Countries.ENGLAND, "Kent");
        public static Region LANCS = new Region(Countries.ENGLAND, "Lancashire");
        public static Region LEICS = new Region(Countries.ENGLAND, "Leicestershire");
        public static Region LINCOLN = new Region(Countries.ENGLAND, "Lincolnshire");
        public static Region MIDDLESEX = new Region(Countries.ENGLAND, "Middlesex");
        public static Region NORFOLK = new Region(Countries.ENGLAND, "Norfolk");
        public static Region NORTHAMPTON = new Region(Countries.ENGLAND, "Northamptonshire");
        public static Region NORTHUMBERLAND = new Region(Countries.ENGLAND, "Northumberland");
        public static Region NOTTINGHAM = new Region(Countries.ENGLAND, "Nottinghamshire");
        public static Region OXFORD = new Region(Countries.ENGLAND, "Oxfordshire");
        public static Region RUTLAND = new Region(Countries.ENGLAND, "Rutland");
        public static Region SHROPS = new Region(Countries.ENGLAND, "Shropshire");
        public static Region SOMERSET = new Region(Countries.ENGLAND, "Somerset");
        public static Region STAFFORD = new Region(Countries.ENGLAND, "Staffordshire");
        public static Region SUFFOLK = new Region(Countries.ENGLAND, "Suffolk");
        public static Region SURREY = new Region(Countries.ENGLAND, "Surrey");
        public static Region SUSSEX = new Region(Countries.ENGLAND, "Sussex");
        public static Region WARWICK = new Region(Countries.ENGLAND, "Warwickshire");
        public static Region WESTMORLAND = new Region(Countries.ENGLAND, "Westmorland");
        public static Region WILTS = new Region(Countries.ENGLAND, "Wiltshire");
        public static Region WORCESTER = new Region(Countries.ENGLAND, "Worcestershire");
        public static Region YORKS = new Region(Countries.ENGLAND, "Yorkshire");
        #endregion

        // Wales
        //Anglesey / Sir Fon
        //Brecknockshire / Sir Frycheiniog
        //Caernarfonshire / Sir Gaernarfon
        //Cardiganshire / Ceredigion
        //Carmarthenshire / Sir Gaerfyrddin
        //Denbighshire / Sir Ddinbych
        //Dunbartonshire / Dumbartonshire
        //Flintshire / Sir Fflint
        //Glamorgan / Morgannwg
        //Merioneth / Meirionnydd
        //Monmouthshire / Sir Fynwy
        //Montgomeryshire / Sir Drefaldwyn
        //Pembrokeshire / Sir Benfro
        //Radnorshire / Sir Faesyfed

//Northern Island
        //County Antrim
        //County Armagh
        //County Down
        //County Fermanagh
        //County Londonderry
        //County Tyrone

        
        static Regions()
        {
            // List from Scotland's People
            SCOTTISH_REGIONS = new HashSet<Region>(new Region[]{
                    ABERDEEN, ANGUS, ARGYLL, AYR, BANFF, BERWICK, BUTE, CAITHNESS, CLACKMANNAN, DUMFRIES,
                    DUNBARTON, EAST_LOTHIAN, FIFE, INVERNESS, KINCARDINE, KINROSS, KIRKCUDBRIGHT, LANARK, 
                    MIDLOTHIAN, MORAY, NAIRN, ORKNEY, PEEBLES, PERTH, RENFREW, ROSS_CROMARTY, ROXBURGH, 
                    SELKIRK, SHETLAND, STIRLING, SUTHERLAND, WEST_LOTHIAN, WIGTOWN
                });
            AddScottishRegionAlternates();

            ENGLISH_REGIONS = new HashSet<Region>(new Region[] {
                    BEDS, BERKS, BUCKS, CAMBS, CHESHIRE, CORNWALL, CUMBERLAND, DERBY, DEVON, DORSET,
                    DURHAM, ESSEX, GLOUCS, HAMPS, HEREFORD, HERTS, HUNTS, KENT, LANCS, LEICS, LINCOLN, 
                    MIDDLESEX, NORFOLK, NORTHAMPTON, NORTHUMBERLAND, NOTTINGHAM, OXFORD, RUTLAND, SHROPS, 
                    SOMERSET, STAFFORD, SUFFOLK, SURREY, SUSSEX, WARWICK, WESTMORLAND, WILTS, WORCESTER, YORKS
            });


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
            ABERDEEN.AddAlternateName("Aberdeen");
            ANGUS.AddAlternateName("Forfarshire");
            ARGYLL.AddAlternateName("Argyll");
            BANFF.AddAlternateName("Banff");
            BERWICK.AddAlternateName("Berwick");
            BUTE.AddAlternateName("Butes");
            CLACKMANNAN.AddAlternateName("Clackmannan");
            DUMFRIES.AddAlternateName("Dumfries");
            DUMFRIES.AddAlternateName("Dumfriesshire");
            DUNBARTON.AddAlternateName("Dunbarton");
            EAST_LOTHIAN.AddAlternateName("Haddingtonshire");
            FIFE.AddAlternateName("Fifeshire");
            INVERNESS.AddAlternateName("Inverness");
            INVERNESS.AddAlternateName("Invernessshire");
            KINCARDINE.AddAlternateName("Kincardine");
            KINROSS.AddAlternateName("Kinross");
            KINROSS.AddAlternateName("Kinrossshire");
            KIRKCUDBRIGHT.AddAlternateName("Kirkcudbright");
            LANARK.AddAlternateName("Lanark");
            MIDLOTHIAN.AddAlternateName("Edinburghshire");
            MORAY.AddAlternateName("Elginshire");
            NAIRN.AddAlternateName("Nairnshire");
            PEEBLES.AddAlternateName("Peebles-shire");
            PEEBLES.AddAlternateName("Peeblesshire");
            PERTH.AddAlternateName("Perth");
            RENFREW.AddAlternateName("Renfrew");
            ROSS_CROMARTY.AddAlternateName("Cromartyshire");
            ROSS_CROMARTY.AddAlternateName("Rossshire");
            ROSS_CROMARTY.AddAlternateName("Ross-shire");
            ROXBURGH.AddAlternateName("Roxburghshire");
            SELKIRK.AddAlternateName("Selkirkshire");
            STIRLING.AddAlternateName("Stirling");
            WEST_LOTHIAN.AddAlternateName("Linlithgowshire");
            WIGTOWN.AddAlternateName("Wigtown");
            // alternate names
            SHETLAND.AddAlternateName("Zetland");
            ROSS_CROMARTY.AddAlternateName("Ross & Cromarty");
            FIFE.AddAlternateName("Kingdom of Fife");
        }

        private static void AddWelshRegionAlternates()
        { // http://www.gazetteer.org.uk/contents.php
            //Anglesey             Sir Fon
            //Brecknockshire       Sir Frycheiniog
            //Caernarfonshire      Sir Gaernarfon
            //Cardiganshire        Ceredigion
            //Carmarthenshire      Sir Gaerfyrddin
            //Denbighshire         Sir Ddinbych
            //Flintshire           Sir y Fflint
            //Glamorgan            Morgannwg
            //Merioneth            Meirionnydd
            //Monmouthshire        Sir Fynwy
            //Montgomeryshire      Sir Drefaldwyn
            //Pembrokeshire        Sir Benfro
            //Radnorshire          Sir Faesyfed
        }

        private static void AddEnglishRegionAlternates()
        {

        }
    }
}
