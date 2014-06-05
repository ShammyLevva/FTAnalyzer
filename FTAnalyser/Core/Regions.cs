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
        public static ISet<Region> NIRELAND_REGIONS;
        public static ISet<Region> ISLAND_REGIONS;
        public static ISet<Region> UK_REGIONS;
        public static IDictionary<string, Region> VALID_REGIONS;

        #region Scottish Regions
        public static Region ABERDEEN = new Region("Aberdeenshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ANGUS = new Region("Angus", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ARGYLL = new Region("Argyll", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region AYR = new Region("Ayrshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BANFF = new Region("Banffshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BERWICK = new Region("Berwickshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BUTE = new Region("Buteshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region CAITHNESS = new Region("Caithness", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region CLACKMANNAN = new Region("Clackmannanshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region DUMFRIES = new Region("Dumfries-shire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region DUNBARTON = new Region("Dunbartonshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region EAST_LOTHIAN = new Region("East Lothian", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region FIFE = new Region("Fife", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region INVERNESS = new Region("Inverness-shire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region KINCARDINE = new Region("Kincardineshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region KINROSS = new Region("Kinross-shire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region KIRKCUDBRIGHT = new Region("Kirkcudbrightshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region LANARK = new Region("Lanarkshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region MIDLOTHIAN = new Region("Midlothian", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region MORAY = new Region("Moray", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region NAIRN = new Region("Nairn", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ORKNEY = new Region("Orkney", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region PEEBLES = new Region("Peebles", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region PERTH = new Region("Perthshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region RENFREW = new Region("Renfrewshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ROSS_CROMARTY = new Region("Ross and Cromarty", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ROXBURGH = new Region("Roxburgh", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region SELKIRK = new Region("Selkirk", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region SHETLAND = new Region("Shetland", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region STIRLING = new Region("Stirlingshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region SUTHERLAND = new Region("Sutherland", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region WEST_LOTHIAN = new Region("West Lothian", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region WIGTOWN = new Region("Wigtownshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        #endregion

        #region English Regions
        public static Region BEDS = new Region("Bedfordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region BERKS = new Region("Berkshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region BUCKS = new Region("Buckinghamshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region CAMBS = new Region("Cambridgeshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region CHESHIRE = new Region("Cheshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region CORNWALL = new Region("Cornwall", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region CUMBERLAND = new Region("Cumberland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region DERBY = new Region("Derbyshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region DEVON = new Region("Devon", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region DORSET = new Region("Dorset", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region DURHAM = new Region("Durham", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region ESSEX = new Region("Essex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region GLOUCS = new Region("Gloucestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HANTS = new Region("Hampshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HEREFORD = new Region("Herefordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HERTS = new Region("Hertfordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HUNTS = new Region("Huntingdonshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region KENT = new Region("Kent", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LANCS = new Region("Lancashire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LEICS = new Region("Leicestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LINCOLN = new Region("Lincolnshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region MIDDLESEX = new Region("Middlesex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORFOLK = new Region("Norfolk", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORTHAMPTON = new Region("Northamptonshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORTHUMBERLAND = new Region("Northumberland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NOTTINGHAM = new Region("Nottinghamshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region OXFORD = new Region("Oxfordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region RUTLAND = new Region("Rutland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SHROPS = new Region("Shropshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SOMERSET = new Region("Somerset", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region STAFFORD = new Region("Staffordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SUFFOLK = new Region("Suffolk", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SURREY = new Region("Surrey", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SUSSEX = new Region("Sussex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WARWICK = new Region("Warwickshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WESTMORLAND = new Region("Westmorland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WILTS = new Region("Wiltshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WORCESTER = new Region("Worcestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region YORKS = new Region("Yorkshire", Countries.ENGLAND, Region.Creation.HISTORIC);

        public static Region CUMBRIA = new Region("Cumbria", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HUMBERSIDE = new Region("Cumbria", Countries.ENGLAND, Region.Creation.MODERN);
        #endregion

        // Wales
        //Anglesey / Sir Fon
        //Brecknockshire / Sir Frycheiniog
        //Caernarfonshire / Sir Gaernarfon
        //Cardiganshire / Ceredigion
        //Carmarthenshire / Sir Gaerfyrddin
        //Denbighshire / Sir Ddinbych
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

        #region UK Islands
        public static Region IOM = new Region("Isle of Man", Countries.ISLE_OF_MAN, Region.Creation.HISTORIC);
        public static Region JERSEY = new Region("Jersey", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region GUERNSEY = new Region("Guernsey", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region ALDERNEY = new Region("Alderney", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region SARK = new Region("Sark", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        #endregion
        
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
                    DURHAM, ESSEX, GLOUCS, HANTS, HEREFORD, HERTS, HUNTS, KENT, LANCS, LEICS, LINCOLN, 
                    MIDDLESEX, NORFOLK, NORTHAMPTON, NORTHUMBERLAND, NOTTINGHAM, OXFORD, RUTLAND, SHROPS, 
                    SOMERSET, STAFFORD, SUFFOLK, SURREY, SUSSEX, WARWICK, WESTMORLAND, WILTS, WORCESTER,
                    YORKS, CUMBRIA, HUMBERSIDE
            });
            AddEnglishRegionAlternates();

            WELSH_REGIONS = new HashSet<Region>(new Region[] {
            
            });
            AddWelshRegionAlternates();

            NIRELAND_REGIONS = new HashSet<Region>(new Region[] {
            
            });
            AddNorthernIrelandRegionAlternates();

            ISLAND_REGIONS = new HashSet<Region>(new Region[] {
                JERSEY, ALDERNEY, GUERNSEY, SARK, IOM
            });

            UK_REGIONS = new HashSet<Region>();
            UK_REGIONS.UnionWith(SCOTTISH_REGIONS);
            UK_REGIONS.UnionWith(ENGLISH_REGIONS);
            UK_REGIONS.UnionWith(WELSH_REGIONS);
            UK_REGIONS.UnionWith(NIRELAND_REGIONS);
            UK_REGIONS.UnionWith(ISLAND_REGIONS);

            VALID_REGIONS = new Dictionary<string, Region>();
            AppendValidRegions(UK_REGIONS);
        }

        private static void AppendValidRegions(ISet<Region> regions)
        {
            foreach(Region r in regions)
            {
                VALID_REGIONS.Add(r.PreferredName, r);
                foreach (string alternate in r.AlternativeNames)
                    VALID_REGIONS.Add(alternate, r);
            }
        }

        public static bool IsKnownRegion(string region)
        {
            return VALID_REGIONS.ContainsKey(region);
        }

        public static Region GetRegion(string region)
        {
            Region result;
            if (VALID_REGIONS.TryGetValue(region, out result))
                return result;
            return null;
        }

        private static void AddScottishRegionAlternates()
        {
            // add Anglicised shires
            ABERDEEN.AddAlternateName("Aberdeen");
            ANGUS.AddAlternateName("Forfarshire");
            ARGYLL.AddAlternateName("Argyllshire");
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

        private static void AddNorthernIrelandRegionAlternates()
        {

        }

        private static void AddEnglishRegionAlternates()
        {
            BEDS.AddAlternateName("Beds");
            BERKS.AddAlternateName("Berks");
            BUCKS.AddAlternateName("Bucks");
            CAMBS.AddAlternateName("Cambs");
            CHESHIRE.AddAlternateName("Ches");
            CORNWALL.AddAlternateName("Corn");
            CUMBERLAND.AddAlternateName("Cumb");
            DERBY.AddAlternateName("Derbs");
            DERBY.AddAlternateName("Derbys");
            DEVON.AddAlternateName("Devonshire");
            DEVON.AddAlternateName("Dev");
            DORSET.AddAlternateName("Dorsetshire");
            DORSET.AddAlternateName("Dor");
            DURHAM.AddAlternateName("County Durham");
            DURHAM.AddAlternateName("Co Durham");
            DURHAM.AddAlternateName("Co Dur");
            GLOUCS.AddAlternateName("Glos");
            HANTS.AddAlternateName("Hants");
            HANTS.AddAlternateName("Southamptonshire");
            HEREFORD.AddAlternateName("Heref");
            HEREFORD.AddAlternateName("Hereford");
            HEREFORD.AddAlternateName("Here");
            HERTS.AddAlternateName("Herts");
            HUNTS.AddAlternateName("Hunts");
            LANCS.AddAlternateName("Lancs");
            LEICS.AddAlternateName("Leics");
            LINCOLN.AddAlternateName("Lincs");
            MIDDLESEX.AddAlternateName("London");
            MIDDLESEX.AddAlternateName("Mx");
            MIDDLESEX.AddAlternateName("Middx");
            MIDDLESEX.AddAlternateName("Midx");
            MIDDLESEX.AddAlternateName("Mddx");
            NORFOLK.AddAlternateName("Norf");
            NORTHAMPTON.AddAlternateName("Northants");
            NORTHUMBERLAND.AddAlternateName("Northumb");
            NORTHUMBERLAND.AddAlternateName("Northd");
            NOTTINGHAM.AddAlternateName("Notts");
            OXFORD.AddAlternateName("Oxon");
            RUTLAND.AddAlternateName("Rut");
            SHROPS.AddAlternateName("Shrops");
            SHROPS.AddAlternateName("Salop");
            SOMERSET.AddAlternateName("Som");
            SOMERSET.AddAlternateName("Somersetshire");
            STAFFORD.AddAlternateName("Staffs");
            STAFFORD.AddAlternateName("Staf");
            SUFFOLK.AddAlternateName("Suff");
            SUFFOLK.AddAlternateName("West Suffolk");
            SUFFOLK.AddAlternateName("East Suffolk");
            SURREY.AddAlternateName("Sy");
            SURREY.AddAlternateName("East Surrey");
            SURREY.AddAlternateName("West Surrey");
            SUSSEX.AddAlternateName("Sx");
            SUSSEX.AddAlternateName("Ssx");
            SUSSEX.AddAlternateName("East Sussex");
            SUSSEX.AddAlternateName("West Sussex");
            WARWICK.AddAlternateName("Warw");
            WARWICK.AddAlternateName("Warks");
            WARWICK.AddAlternateName("War");
            WESTMORLAND.AddAlternateName("Westm");
            WILTS.AddAlternateName("Wilts");
            WORCESTER.AddAlternateName("Worcs");
            YORKS.AddAlternateName("Yorks");
            YORKS.AddAlternateName("East Yorkshire");
            YORKS.AddAlternateName("North Yorkshire");
            YORKS.AddAlternateName("West Yorkshire");
        }
    }
}
