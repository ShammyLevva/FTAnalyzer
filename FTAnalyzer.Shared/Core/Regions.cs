using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Regions
    {
        // ISO Region codes at http://en.wikipedia.org/wiki/ISO_3166-2

        public static ISet<Region> SCOTTISH_REGIONS;
        public static ISet<Region> ENGLISH_REGIONS;
        public static ISet<Region> WELSH_REGIONS;
        public static ISet<Region> NIRELAND_REGIONS;
        public static ISet<Region> ISLAND_REGIONS;
        public static ISet<Region> UK_REGIONS;
        public static ISet<Region> IRISH_REGIONS;
        public static ISet<Region> CANADIAN_REGIONS;
        public static ISet<Region> US_STATES;
        public static ISet<Region> AUSTRALIAN_REGIONS;
        public static ISet<Region> NEW_ZEALAND_REGIONS;

        public static IList<Region> PREFERRED_REGIONS;
        public static IDictionary<string, Region> VALID_REGIONS;

        public static readonly Dictionary<Region, List<ModernCounty>> CONVERSIONS = new Dictionary<Region, List<ModernCounty>>();
        public static readonly List<ModernCounty> MODERN_COUNTIES;

        #region Modern County List
        public static ModernCounty OS_ABERDEENSHIRE = new ModernCounty("AB", "Aberdeenshire", Countries.SCOTLAND);
        public static ModernCounty OS_ANGUS = new ModernCounty("AG", "Angus", Countries.SCOTLAND);
        public static ModernCounty OS_ABERDEEN_CITY = new ModernCounty("AN", "Aberdeen City", Countries.SCOTLAND);
        public static ModernCounty OS_ARGYLL_AND_BUTE = new ModernCounty("AR", "Argyll and Bute", Countries.SCOTLAND);
        public static ModernCounty OS_BRADFORD = new ModernCounty("BA", "Bradford", Countries.ENGLAND);
        public static ModernCounty OS_BLACKBURN_WITH_DARWEN = new ModernCounty("BB", "Blackburn with Darwen", Countries.ENGLAND);
        public static ModernCounty OS_BRACKNELL_FOREST = new ModernCounty("BC", "Bracknell Forest", Countries.ENGLAND);
        public static ModernCounty OS_BARKING_AND_DAGENHAM = new ModernCounty("BD", "Barking and Dagenham", Countries.ENGLAND);
        public static ModernCounty OS_BRIDGEND = new ModernCounty("BE", "Bridgend", Countries.WALES);
        public static ModernCounty OS_BEDFORDSHIRE = new ModernCounty("BF", "Bedford", Countries.ENGLAND);
        public static ModernCounty OS_BLAENAU_GWENT = new ModernCounty("BG", "Blaenau Gwent", Countries.WALES);
        public static ModernCounty OS_CITY_OF_BRIGHTON_AND_HOVE = new ModernCounty("BH", "City of Brighton and Hove", Countries.ENGLAND);
        public static ModernCounty OS_BIRMINGHAM = new ModernCounty("BI", "Birmingham", Countries.ENGLAND);
        public static ModernCounty OS_CENTRAL_BEDFORDSHIRE = new ModernCounty("BK", "Central Bedfordshire", Countries.ENGLAND);
        public static ModernCounty OS_BARNSLEY = new ModernCounty("BL", "Barnsley", Countries.ENGLAND);
        public static ModernCounty OS_BUCKINGHAMSHIRE = new ModernCounty("BM", "Buckinghamshire", Countries.ENGLAND);
        public static ModernCounty OS_BARNET = new ModernCounty("BN", "Barnet", Countries.ENGLAND);
        public static ModernCounty OS_BOLTON = new ModernCounty("BO", "Bolton", Countries.ENGLAND);
        public static ModernCounty OS_BLACKPOOL = new ModernCounty("BP", "Blackpool", Countries.ENGLAND);
        public static ModernCounty OS_BROMLEY = new ModernCounty("BR", "Bromley", Countries.ENGLAND);
        public static ModernCounty OS_BATH_AND_NORTH_EAST_SOMERSET = new ModernCounty("BS", "Bath and North East Somerset", Countries.ENGLAND);
        public static ModernCounty OS_BRENT = new ModernCounty("BT", "Brent", Countries.ENGLAND);
        public static ModernCounty OS_BOURNEMOUTH = new ModernCounty("BU", "Bournemouth", Countries.ENGLAND);
        public static ModernCounty OS_BEXLEY = new ModernCounty("BX", "Bexley", Countries.ENGLAND);
        public static ModernCounty OS_BURY = new ModernCounty("BY", "Bury", Countries.ENGLAND);
        public static ModernCounty OS_BRISTOL = new ModernCounty("BZ", "City of Bristol", Countries.ENGLAND);
        public static ModernCounty OS_CALDERDALE = new ModernCounty("CA", "Calderdale", Countries.ENGLAND);
        public static ModernCounty OS_CAMBRIDGESHIRE = new ModernCounty("CB", "Cambridgeshire", Countries.ENGLAND);
        public static ModernCounty OS_CHESHIRE_WEST_AND_CHESTER = new ModernCounty("CC", "Cheshire West and Chester", Countries.ENGLAND);
        public static ModernCounty OS_CARDIFF = new ModernCounty("CD", "Cardiff", Countries.WALES);
        public static ModernCounty OS_CEREDIGION = new ModernCounty("CE", "Ceredigion", Countries.WALES);
        public static ModernCounty OS_CAERPHILLY = new ModernCounty("CF", "Caerphilly", Countries.WALES);
        public static ModernCounty OS_CHESHIRE_EAST = new ModernCounty("CH", "Cheshire East", Countries.ENGLAND);
        public static ModernCounty OS_CLACKMANNANSHIRE = new ModernCounty("CL", "Clackmannanshire", Countries.SCOTLAND);
        public static ModernCounty OS_CAMDEN = new ModernCounty("CM", "Camden", Countries.ENGLAND);
        public static ModernCounty OS_CORNWALL = new ModernCounty("CN", "Cornwall", Countries.ENGLAND);
        public static ModernCounty OS_CARMARTHENSHIRE = new ModernCounty("CT", "Carmarthenshire", Countries.ENGLAND);
        public static ModernCounty OS_CUMBRIA = new ModernCounty("CU", "Cumbria", Countries.ENGLAND);
        public static ModernCounty OS_COVENTRY = new ModernCounty("CV", "Coventry", Countries.ENGLAND);
        public static ModernCounty OS_CONWY = new ModernCounty("CW", "Conwy", Countries.WALES);
        public static ModernCounty OS_CROYDON = new ModernCounty("CY", "Croydon", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_DERBY = new ModernCounty("DB", "City of Derby", Countries.ENGLAND);
        public static ModernCounty OS_DUNDEE_CITY = new ModernCounty("DD", "Dundee City", Countries.SCOTLAND);
        public static ModernCounty OS_DENBIGHSHIRE = new ModernCounty("DE", "Denbighshire", Countries.WALES);
        public static ModernCounty OS_DUMFRIES_AND_GALLOWAY = new ModernCounty("DG", "Dumfries and Galloway", Countries.SCOTLAND);
        public static ModernCounty OS_DARLINGTON = new ModernCounty("DL", "Darlington", Countries.ENGLAND);
        public static ModernCounty OS_DEVON = new ModernCounty("DN", "Devon", Countries.ENGLAND);
        public static ModernCounty OS_DONCASTER = new ModernCounty("DR", "Doncaster", Countries.ENGLAND);
        public static ModernCounty OS_DORSET = new ModernCounty("DT", "Dorset", Countries.ENGLAND);
        public static ModernCounty OS_DURHAM = new ModernCounty("DU", "Durham", Countries.ENGLAND);
        public static ModernCounty OS_DERBYSHIRE = new ModernCounty("DY", "Derbyshire", Countries.ENGLAND);
        public static ModernCounty OS_DUDLEY = new ModernCounty("DZ", "Dudley", Countries.ENGLAND);
        public static ModernCounty OS_EAST_AYRSHIRE = new ModernCounty("EA", "East Ayrshire", Countries.SCOTLAND);
        public static ModernCounty OS_CITY_OF_EDINBURGH = new ModernCounty("EB", "City of Edinburgh", Countries.SCOTLAND);
        public static ModernCounty OS_EAST_DUNBARTONSHIRE = new ModernCounty("ED", "East Dunbartonshire", Countries.SCOTLAND);
        public static ModernCounty OS_EALING = new ModernCounty("EG", "Ealing", Countries.ENGLAND);
        public static ModernCounty OS_EAST_LOTHIAN = new ModernCounty("EL", "East Lothian", Countries.SCOTLAND);
        public static ModernCounty OS_ENFIELD = new ModernCounty("EN", "Enfield", Countries.ENGLAND);
        public static ModernCounty OS_EAST_RENFREWSHIRE = new ModernCounty("ER", "East Renfrewshire", Countries.SCOTLAND);
        public static ModernCounty OS_EAST_SUSSEX = new ModernCounty("ES", "East Sussex", Countries.ENGLAND);
        public static ModernCounty OS_ESSEX = new ModernCounty("EX", "Essex", Countries.ENGLAND);
        public static ModernCounty OS_EAST_RIDING_OF_YORKSHIRE = new ModernCounty("EY", "East Riding of Yorkshire", Countries.ENGLAND);
        public static ModernCounty OS_FALKIRK = new ModernCounty("FA", "Falkirk", Countries.SCOTLAND);
        public static ModernCounty OS_FIFE = new ModernCounty("FF", "Fife", Countries.SCOTLAND);
        public static ModernCounty OS_FLINTSHIRE = new ModernCounty("FL", "Flintshire", Countries.WALES);
        public static ModernCounty OS_GATESHEAD = new ModernCounty("GH", "Gateshead", Countries.ENGLAND);
        public static ModernCounty OS_GLASGOW_CITY = new ModernCounty("GL", "Glasgow City", Countries.SCOTLAND);
        public static ModernCounty OS_GLOUCESTERSHIRE = new ModernCounty("GR", "Gloucestershire", Countries.ENGLAND);
        public static ModernCounty OS_GREENWICH = new ModernCounty("GW", "Greenwich", Countries.ENGLAND);
        public static ModernCounty OS_GWYNEDD = new ModernCounty("GY", "Gwynedd", Countries.WALES);
        public static ModernCounty OS_HALTON = new ModernCounty("HA", "Halton", Countries.ENGLAND);
        public static ModernCounty OS_HERTFORDSHIRE = new ModernCounty("HD", "Hertfordshire", Countries.ENGLAND);
        public static ModernCounty OS_HEREFORDSHIRE = new ModernCounty("HE", "Herefordshire", Countries.ENGLAND);
        public static ModernCounty OS_HAMMERSMITH_AND_FULHAM = new ModernCounty("HF", "Hammersmith and Fulham", Countries.ENGLAND);
        public static ModernCounty OS_HARINGEY = new ModernCounty("HG", "Haringey", Countries.ENGLAND);
        public static ModernCounty OS_HILLINGDON = new ModernCounty("HI", "Hillingdon", Countries.ENGLAND);
        public static ModernCounty OS_HIGHLAND = new ModernCounty("HL", "Highland", Countries.SCOTLAND);
        public static ModernCounty OS_HACKNEY = new ModernCounty("HN", "Hackney", Countries.ENGLAND);
        public static ModernCounty OS_HAMPSHIRE = new ModernCounty("HP", "Hampshire", Countries.ENGLAND);
        public static ModernCounty OS_HARROW = new ModernCounty("HR", "Harrow", Countries.ENGLAND);
        public static ModernCounty OS_HOUNSLOW = new ModernCounty("HS", "Hounslow", Countries.ENGLAND);
        public static ModernCounty OS_HARTLEPOOL = new ModernCounty("HT", "Hartlepool", Countries.ENGLAND);
        public static ModernCounty OS_HAVERING = new ModernCounty("HV", "Havering", Countries.ENGLAND);
        public static ModernCounty OS_ISLE_OF_ANGLESEY = new ModernCounty("IA", "Isle of Anglesey", Countries.WALES);
        public static ModernCounty OS_ISLINGTON = new ModernCounty("IL", "Islington", Countries.ENGLAND);
        public static ModernCounty OS_ISLE_OF_MAN = new ModernCounty("IM", "Isle of Man", Countries.ISLE_OF_MAN);
        public static ModernCounty OS_INVERCLYDE = new ModernCounty("IN", "Inverclyde", Countries.SCOTLAND);
        public static ModernCounty OS_ISLES_OF_SCILLY = new ModernCounty("IS", "Isles of Scilly", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_INVERNESS = new ModernCounty("IV", "City of Inverness", Countries.SCOTLAND); // not on gazetteer but in index??
        public static ModernCounty OS_ISLE_OF_WIGHT = new ModernCounty("IW", "Isle of Wight", Countries.ENGLAND);
        public static ModernCounty OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA = new ModernCounty("KC", "Royal Borough of Kensington and Chelsea", Countries.ENGLAND);
        public static ModernCounty OS_KINGSTON_UPON_THAMES = new ModernCounty("KG", "Kingston upon Thames", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_KINGSTON_UPON_HULL = new ModernCounty("KH", "City of Kingston upon Hull", Countries.ENGLAND);
        public static ModernCounty OS_KIRKLEES = new ModernCounty("KL", "Kirklees", Countries.ENGLAND);
        public static ModernCounty OS_KNOWSLEY = new ModernCounty("KN", "Knowsley", Countries.ENGLAND);
        public static ModernCounty OS_KENT = new ModernCounty("KT", "Kent", Countries.ENGLAND);
        public static ModernCounty OS_LANCASHIRE = new ModernCounty("LA", "Lancashire", Countries.ENGLAND);
        public static ModernCounty OS_LAMBETH = new ModernCounty("LB", "Lambeth", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_LEICESTER = new ModernCounty("LC", "City of Leicester", Countries.ENGLAND);
        public static ModernCounty OS_LEEDS = new ModernCounty("LD", "Leeds", Countries.ENGLAND);
        public static ModernCounty OS_LINCOLNSHIRE = new ModernCounty("LL", "Lincolnshire", Countries.ENGLAND);
        public static ModernCounty OS_LUTON = new ModernCounty("LN", "Luton", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_LONDON = new ModernCounty("LO", "City of London", Countries.ENGLAND);
        public static ModernCounty OS_LIVERPOOL = new ModernCounty("LP", "Liverpool", Countries.ENGLAND);
        public static ModernCounty OS_LEWISHAM = new ModernCounty("LS", "Lewisham", Countries.ENGLAND);
        public static ModernCounty OS_LEICESTERSHIRE = new ModernCounty("LT", "Leicestershire", Countries.ENGLAND);
        public static ModernCounty OS_MANCHESTER = new ModernCounty("MA", "Manchester", Countries.ENGLAND);
        public static ModernCounty OS_MIDDLESBROUGH = new ModernCounty("MB", "Middlesbrough", Countries.ENGLAND);
        public static ModernCounty OS_MEDWAY = new ModernCounty("ME", "Medway", Countries.ENGLAND);
        public static ModernCounty OS_MIDLOTHIAN = new ModernCounty("MI", "Midlothian", Countries.SCOTLAND);
        public static ModernCounty OS_MILTON_KEYNES = new ModernCounty("MK", "Milton Keynes", Countries.ENGLAND);
        public static ModernCounty OS_MONMOUTHSHIRE = new ModernCounty("MM", "Monmouthshire", Countries.WALES);
        public static ModernCounty OS_MORAY = new ModernCounty("MO", "Moray", Countries.SCOTLAND);
        public static ModernCounty OS_MERTON = new ModernCounty("MR", "Merton", Countries.ENGLAND);
        public static ModernCounty OS_MERTHYR_TYDFIL = new ModernCounty("MT", "Merthyr Tydfil", Countries.WALES);
        public static ModernCounty OS_NORTH_AYRSHIRE = new ModernCounty("NA", "North Ayrshire", Countries.SCOTLAND);
        public static ModernCounty OS_NORTH_EAST_LINCOLNSHIRE = new ModernCounty("NC", "North East Lincolnshire", Countries.ENGLAND);
        public static ModernCounty OS_NORTHUMBERLAND = new ModernCounty("ND", "Northumberland", Countries.ENGLAND);
        public static ModernCounty OS_NEWPORT = new ModernCounty("NE", "Newport", Countries.WALES);
        public static ModernCounty OS_CITY_OF_NOTTINGHAM = new ModernCounty("NG", "City of Nottingham", Countries.ENGLAND);
        public static ModernCounty OS_NEWHAM = new ModernCounty("NH", "Newham", Countries.ENGLAND);
        public static ModernCounty OS_NORTH_LINCOLNSHIRE = new ModernCounty("NI", "North Lincolnshire", Countries.ENGLAND);
        public static ModernCounty OS_NORFOLK = new ModernCounty("NK", "Norfolk", Countries.ENGLAND);
        public static ModernCounty OS_NORTH_LANARKSHIRE = new ModernCounty("NL", "North Lanarkshire", Countries.SCOTLAND);
        public static ModernCounty OS_NORTHAMPTONSHIRE = new ModernCounty("NN", "Northamptonshire", Countries.ENGLAND);
        public static ModernCounty OS_NEATH_PORT_TALBOT = new ModernCounty("NP", "Neath Port Talbot", Countries.WALES);
        public static ModernCounty OS_NORTH_TYNESIDE = new ModernCounty("NR", "North Tyneside", Countries.ENGLAND);
        public static ModernCounty OS_NORTH_SOMERSET = new ModernCounty("NS", "North Somerset", Countries.ENGLAND);
        public static ModernCounty OS_NOTTINGHAMSHIRE = new ModernCounty("NT", "Nottinghamshire", Countries.ENGLAND);
        public static ModernCounty OS_NEWCASTLE_UPON_TYNE = new ModernCounty("NW", "Newcastle upon Tyne", Countries.ENGLAND);
        public static ModernCounty OS_NORTH_YORKSHIRE = new ModernCounty("NY", "North Yorkshire", Countries.ENGLAND);
        public static ModernCounty OS_OLDHAM = new ModernCounty("OH", "Oldham", Countries.ENGLAND);
        public static ModernCounty OS_ORKNEY_ISLANDS = new ModernCounty("OK", "Orkney Islands", Countries.SCOTLAND);
        public static ModernCounty OS_OXFORDSHIRE = new ModernCounty("ON", "Oxfordshire", Countries.ENGLAND);
        public static ModernCounty OS_PEMBROKESHIRE = new ModernCounty("PB", "Pembrokeshire", Countries.WALES);
        public static ModernCounty OS_CITY_OF_PETERBOROUGH = new ModernCounty("PE", "City of Peterborough", Countries.ENGLAND);
        public static ModernCounty OS_PERTH_AND_KINROSS = new ModernCounty("PK", "Perth and Kinross", Countries.SCOTLAND);
        public static ModernCounty OS_POOLE = new ModernCounty("PL", "Poole", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_PORTSMOUTH = new ModernCounty("PO", "City of Portsmouth", Countries.ENGLAND);
        public static ModernCounty OS_POWYS = new ModernCounty("PW", "Powys", Countries.WALES);
        public static ModernCounty OS_CITY_OF_PLYMOUTH = new ModernCounty("PY", "City of Plymouth", Countries.ENGLAND);
        public static ModernCounty OS_REDBRIDGE = new ModernCounty("RB", "Redbridge", Countries.ENGLAND);
        public static ModernCounty OS_REDCAR_AND_CLEVELAND = new ModernCounty("RC", "Redcar and Cleveland", Countries.ENGLAND);
        public static ModernCounty OS_ROCHDALE = new ModernCounty("RD", "Rochdale", Countries.ENGLAND);
        public static ModernCounty OS_RENFREWSHIRE = new ModernCounty("RE", "Renfrewshire", Countries.SCOTLAND);
        public static ModernCounty OS_READING = new ModernCounty("RG", "Reading", Countries.ENGLAND);
        public static ModernCounty OS_RHONDDA_CYNON_TAFF = new ModernCounty("RH", "Rhondda Cynon Taff", Countries.WALES);
        public static ModernCounty OS_RUTLAND = new ModernCounty("RL", "Rutland", Countries.ENGLAND);
        public static ModernCounty OS_ROTHERHAM = new ModernCounty("RO", "Rotherham", Countries.ENGLAND);
        public static ModernCounty OS_RICHMOND_UPON_THAMES = new ModernCounty("RT", "Richmond upon Thames", Countries.ENGLAND);
        public static ModernCounty OS_SANDWELL = new ModernCounty("SA", "Sandwell", Countries.ENGLAND);
        public static ModernCounty OS_SCOTTISH_BORDERS = new ModernCounty("SB", "Scottish Borders", Countries.SCOTLAND);
        public static ModernCounty OS_SALFORD = new ModernCounty("SC", "Salford", Countries.ENGLAND);
        public static ModernCounty OS_SWINDON = new ModernCounty("SD", "Swindon", Countries.ENGLAND);
        public static ModernCounty OS_SEFTON = new ModernCounty("SE", "Sefton", Countries.ENGLAND);
        public static ModernCounty OS_STAFFORDSHIRE = new ModernCounty("SF", "Staffordshire", Countries.ENGLAND);
        public static ModernCounty OS_SOUTH_GLOUCESTERSHIRE = new ModernCounty("SG", "South Gloucestershire", Countries.ENGLAND);
        public static ModernCounty OS_SHROPSHIRE = new ModernCounty("SH", "Shropshire", Countries.ENGLAND);
        public static ModernCounty OS_SHETLAND_ISLANDS = new ModernCounty("SI", "Shetland Islands", Countries.SCOTLAND);
        public static ModernCounty OS_CITY_OF_STOKE_ON_TRENT = new ModernCounty("SJ", "City of Stoke-on-Trent", Countries.ENGLAND);
        public static ModernCounty OS_SUFFOLK = new ModernCounty("SK", "Suffolk", Countries.ENGLAND);
        public static ModernCounty OS_SOUTH_LANARKSHIRE = new ModernCounty("SL", "South Lanarkshire", Countries.SCOTLAND);
        public static ModernCounty OS_STOCKTON_ON_TEES = new ModernCounty("SM", "Stockton-on-Tees", Countries.ENGLAND);
        public static ModernCounty OS_ST_HELENS = new ModernCounty("SN", "St Helens", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_SOUTHAMPTON = new ModernCounty("SO", "City of Southampton", Countries.ENGLAND);
        public static ModernCounty OS_SHEFFIELD = new ModernCounty("SP", "Sheffield", Countries.ENGLAND);
        public static ModernCounty OS_SOLIHULL = new ModernCounty("SQ", "Solihull", Countries.ENGLAND);
        public static ModernCounty OS_STIRLING = new ModernCounty("SR", "Stirling", Countries.SCOTLAND);
        public static ModernCounty OS_SWANSEA = new ModernCounty("SS", "Swansea", Countries.WALES);
        public static ModernCounty OS_SOMERSET = new ModernCounty("ST", "Somerset", Countries.ENGLAND);
        public static ModernCounty OS_SURREY = new ModernCounty("SU", "Surrey", Countries.ENGLAND);
        public static ModernCounty OS_SUNDERLAND = new ModernCounty("SV", "Sunderland", Countries.ENGLAND);
        public static ModernCounty OS_SOUTHWARK = new ModernCounty("SW", "Southwark", Countries.ENGLAND);
        public static ModernCounty OS_SOUTH_AYRSHIRE = new ModernCounty("SX", "South Ayrshire", Countries.SCOTLAND);
        public static ModernCounty OS_SOUTH_TYNESIDE = new ModernCounty("SY", "South Tyneside", Countries.ENGLAND);
        public static ModernCounty OS_SUTTON = new ModernCounty("SZ", "Sutton", Countries.ENGLAND);
        public static ModernCounty OS_TORBAY = new ModernCounty("TB", "Torbay", Countries.ENGLAND);
        public static ModernCounty OS_TORFAEN = new ModernCounty("TF", "Torfaen", Countries.WALES);
        public static ModernCounty OS_TOWER_HAMLETS = new ModernCounty("TH", "Tower Hamlets", Countries.ENGLAND);
        public static ModernCounty OS_TRAFFORD = new ModernCounty("TR", "Trafford", Countries.ENGLAND);
        public static ModernCounty OS_TAMESIDE = new ModernCounty("TS", "Tameside", Countries.ENGLAND);
        public static ModernCounty OS_THURROCK = new ModernCounty("TU", "Thurrock", Countries.ENGLAND);
        public static ModernCounty OS_VALE_OF_GLAMORGAN = new ModernCounty("VG", "Vale of Glamorgan", Countries.WALES);
        public static ModernCounty OS_WALSALL = new ModernCounty("WA", "Walsall", Countries.ENGLAND);
        public static ModernCounty OS_WEST_BERKSHIRE = new ModernCounty("WB", "West Berkshire", Countries.ENGLAND);
        public static ModernCounty OS_WINDSOR_AND_MAIDENHEAD = new ModernCounty("WC", "Windsor and Maidenhead", Countries.ENGLAND);
        public static ModernCounty OS_WEST_DUNBARTONSHIRE = new ModernCounty("WD", "West Dunbartonshire", Countries.SCOTLAND);
        public static ModernCounty OS_WAKEFIELD = new ModernCounty("WE", "Wakefield", Countries.ENGLAND);
        public static ModernCounty OS_WALTHAM_FOREST = new ModernCounty("WF", "Waltham Forest", Countries.ENGLAND);
        public static ModernCounty OS_WARRINGTON = new ModernCounty("WG", "Warrington", Countries.ENGLAND);
        public static ModernCounty OS_CITY_OF_WOLVERHAMPTON = new ModernCounty("WH", "City of Wolverhampton", Countries.ENGLAND);
        public static ModernCounty OS_WESTERN_ISLES = new ModernCounty("WI", "Na h-Eileanan an Iar", Countries.SCOTLAND);
        public static ModernCounty OS_WOKINGHAM = new ModernCounty("WJ", "Wokingham", Countries.ENGLAND);
        public static ModernCounty OS_WARWICKSHIRE = new ModernCounty("WK", "Warwickshire", Countries.ENGLAND);
        public static ModernCounty OS_WEST_LOTHIAN = new ModernCounty("WL", "West Lothian", Countries.SCOTLAND);
        public static ModernCounty OS_CITY_OF_WESTMINSTER = new ModernCounty("WM", "City of Westminster", Countries.ENGLAND);
        public static ModernCounty OS_WIGAN = new ModernCounty("WN", "Wigan", Countries.ENGLAND);
        public static ModernCounty OS_WORCESTERSHIRE = new ModernCounty("WO", "Worcestershire", Countries.ENGLAND);
        public static ModernCounty OS_TELFORD_AND_WREKIN = new ModernCounty("WP", "Telford and Wrekin", Countries.ENGLAND);
        public static ModernCounty OS_WIRRAL = new ModernCounty("WR", "Wirral", Countries.ENGLAND);
        public static ModernCounty OS_WEST_SUSSEX = new ModernCounty("WS", "West Sussex", Countries.ENGLAND);
        public static ModernCounty OS_WILTSHIRE = new ModernCounty("WT", "Wiltshire", Countries.ENGLAND);
        public static ModernCounty OS_WANDSWORTH = new ModernCounty("WW", "Wandsworth", Countries.ENGLAND);
        public static ModernCounty OS_WREXHAM = new ModernCounty("WX", "Wrexham", Countries.WALES);
        public static ModernCounty OS_YORK = new ModernCounty("YK", "York", Countries.ENGLAND);
        public static ModernCounty OS_SOUTHEND_ON_SEA = new ModernCounty("YS", "Southend-on-Sea", Countries.ENGLAND);
        public static ModernCounty OS_SLOUGH = new ModernCounty("YT", "Slough", Countries.ENGLAND);
        public static ModernCounty OS_STOCKPORT = new ModernCounty("YY", "Stockport", Countries.ENGLAND);
        #endregion

        #region Regions
        #region Scottish Regions
        public static Region ABERDEEN = new Region("Aberdeenshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ANGUS = new Region("Angus", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region ARGYLL = new Region("Argyll", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region AYR = new Region("Ayrshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BANFF = new Region("Banffshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BERWICK = new Region("Berwickshire", Countries.SCOTLAND, Region.Creation.HISTORIC);
        public static Region BUTE = new Region("Bute", Countries.SCOTLAND, Region.Creation.HISTORIC);
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
       
        public static Region BORDERS = new Region("Borders", Countries.SCOTLAND, Region.Creation.LG_ACT1974);              
        public static Region CENTRAL_SCOT = new Region("Central", Countries.SCOTLAND, Region.Creation.LG_ACT1974);              
        public static Region DUMFRIES_GALLOWAY = new Region("Dumfries and Galloway", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        public static Region GRAMPIAN = new Region("Grampian", Countries.SCOTLAND, Region.Creation.LG_ACT1974);             
        public static Region HIGHLAND = new Region("Highland", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        public static Region LOTHIAN = new Region("Lothian", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        public static Region STRATHCLYDE = new Region("Strathclyde", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        public static Region TAYSIDE = new Region("Tayside", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        public static Region WESTERN_ISLES = new Region("Western Isles", Countries.SCOTLAND, Region.Creation.LG_ACT1974);
        
        public static Region ABERDEEN_CITY = new Region("Aberdeen City", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region ARGYLL_BUTE = new Region("Argyll and Bute", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region DUNDEE_CITY = new Region("Dundee City", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region EAST_AYRSHIRE = new Region("East Ayrshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region EDINBURGH_CITY = new Region("City of Edinburgh", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region EAST_DUNBARTONSHIRE = new Region("East Dunbartonshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region EAST_RENFREW = new Region("East Renfrewshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region FALKIRK = new Region("Falkirk", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region GLASGOW_CITY = new Region("Glasgow City", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region INVERCLYDE = new Region("Inverclyde", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region INVERNESS_CITY = new Region("City of Inverness", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region NORTH_AYRSHIRE = new Region("North Ayrshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region NORTH_LANARK = new Region("North Lanarkshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region PERTH_KINROSS = new Region("Perth and Kinross", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region SOUTH_LANARK = new Region("South Lanarkshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region SOUTH_AYRSHIRE = new Region("South Ayrshire", Countries.SCOTLAND, Region.Creation.MODERN);
        public static Region WEST_DUNBARTON = new Region("West Dunbartonshire", Countries.SCOTLAND, Region.Creation.MODERN);

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
        public static Region DURHAM = new Region("County Durham", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region ESSEX = new Region("Essex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region GLOUCS = new Region("Gloucestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HANTS = new Region("Hampshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HEREFORD = new Region("Herefordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HERTS = new Region("Hertfordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region HUNTS = new Region("Huntingdonshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region KENT = new Region("Kent", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LANCS = new Region("Lancashire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LEICS = new Region("Leicestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region LINCS = new Region("Lincolnshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region MIDDLESEX = new Region("Middlesex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORFOLK = new Region("Norfolk", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORTHAMPTON = new Region("Northamptonshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NORTHUMBERLAND = new Region("Northumberland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region NOTTS = new Region("Nottinghamshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region OXFORD = new Region("Oxfordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region RUTLAND = new Region("Rutland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SHROPS = new Region("Shropshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SOMERSET = new Region("Somerset", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region STAFFS = new Region("Staffordshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SUFFOLK = new Region("Suffolk", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SURREY = new Region("Surrey", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region SUSSEX = new Region("Sussex", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WARWICK = new Region("Warwickshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WESTMORLAND = new Region("Westmorland", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WILTS = new Region("Wiltshire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region WORCESTER = new Region("Worcestershire", Countries.ENGLAND, Region.Creation.HISTORIC);
        public static Region YORKS = new Region("Yorkshire", Countries.ENGLAND, Region.Creation.HISTORIC);

        public static Region LONDON = new Region("London", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region MANCHESTER = new Region("Manchester", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region MERSEYSIDE = new Region("Merseyside", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region EAST_YORKSHIRE = new Region("East Yorkshire", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region NORTH_YORKSHIRE = new Region("North Yorkshire", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region SOUTH_YORKSHIRE = new Region("South Yorkshire", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region TYNE_WEAR = new Region("Tyne and Wear", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region WEST_MIDLANDS = new Region("West Midlands", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region WEST_YORKSHIRE = new Region("West Yorkshire", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region AVON = new Region("Avon", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region CLEVELAND = new Region("Cleveland", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region CUMBRIA = new Region("Cumbria", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region HUMBERSIDE = new Region("Humberside", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region IOW = new Region("Isle of Wight", Countries.ENGLAND, Region.Creation.LG_ACT1974);
        public static Region HEREFORD_WORCESTER = new Region("Hereford and Worcester", Countries.ENGLAND, Region.Creation.LG_ACT1974);

        public static Region BRADFORD = new Region("Bradford", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BLACKBURN = new Region("Blackburn with Darwen", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BRACKNELL = new Region("Bracknell Forest", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BARKING = new Region("Barking and Dagenham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BRIGHTON = new Region("City of Brighton and Hove", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BIRMINGHAM = new Region("Birmingham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BARNSLEY = new Region("Barnsley", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BARNET = new Region("Barnet", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BOLTON = new Region("Bolton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BLACKPOOL = new Region("Blackpool", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BROMLEY = new Region("Bromley", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BATH = new Region("Bath and North East Somerset", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BRENT = new Region("Brent", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BOURNEMOUTH = new Region("Bournemouth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BEXLEY = new Region("Bexley", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BURY = new Region("Bury", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region BRISTOL = new Region("City of Bristol", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region CALDERDALE = new Region("Calderdale", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region CAMDEN = new Region("Camden", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region CENTRAL_BEDFORDSHIRE = new Region("Central Bedfordshire", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region COVENTRY = new Region("Coventry", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region CROYDON = new Region("Croydon", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region DERBY_CITY = new Region("City of Derby", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region DARLINGTON = new Region("Darlington", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region DONCASTER = new Region("Doncaster", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region DUDLEY = new Region("Dudley", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region EALING = new Region("Ealing", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ENFIELD = new Region("Enfield", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region EAST_SUSSEX = new Region("East Sussex", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region GATESHEAD = new Region("Gateshead", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region GREENWICH = new Region("Greenwich", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HALTON = new Region("Halton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HAMMERSMITH = new Region("Hammersmith and Fulham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HARINGEY = new Region("Haringey", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HILLINGDON = new Region("Hillingdon", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HACKNEY = new Region("Hackney", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HARROW = new Region("Harrow", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HOUNSLOW = new Region("Hounslow", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HARTLEPOOL = new Region("Hartlepool", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region HAVERING = new Region("Havering", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ISLINGTON = new Region("Islington", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ISLES_OF_SCILLY = new Region("Isles of Scilly", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region KENSINGTON = new Region("Royal Borough of Kensington and Chelsea", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region KINGSTON_THAMES = new Region("Kingston upon Thames", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region KINGSTON_HULL = new Region("City of Kingston upon Hull", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region KIRKLEES = new Region("Kirklees", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region KNOWSLEY = new Region("Knowsley", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LAMBETH = new Region("Lambeth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LEICESTER_CITY = new Region("City of Leicester", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LEEDS = new Region("Leeds", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LUTON = new Region("Luton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LIVERPOOL = new Region("Liverpool", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region LEWISHAM = new Region("Lewisham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region MIDDLESBROUGH = new Region("Middlesbrough", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region MEDWAY = new Region("Medway", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region MILTON_KEYNES = new Region("Milton Keynes", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region MERTON = new Region("Merton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NE_LINCOLNSHIRE = new Region("North East Lincolnshire", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NEWPORT = new Region("Newport", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NOTTINGHAM_CITY = new Region("City of Nottingham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NEWHAM = new Region("Newham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NORTH_LINCOLNSHIRE = new Region("North Lincolnshire", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NORTH_TYNESIDE = new Region("North Tyneside", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NORTH_SOMERSET = new Region("North Somerset", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region NEWCASTLE = new Region("Newcastle upon Tyne", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region OLDHAM = new Region("Oldham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region PETERBOROUGH = new Region("Peterborough", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region POOLE = new Region("Poole", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region PORTSMOUTH = new Region("Portsmouth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region PLYMOUTH = new Region("Plymouth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region REDBRIDGE = new Region("Redbridge", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region REDCAR = new Region("Redcar and Cleveland", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ROCHDALE = new Region("Rochdale", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region READING = new Region("Reading", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ROTHERHAM = new Region("Rotherham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region RICHMOND_THAMES = new Region("Richmond upon Thames", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SANDWELL = new Region("Sandwell", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SALFORD = new Region("Salford", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SWINDON = new Region("Swindon", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SEFTON = new Region("Sefton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTH_GLOUCESTERSHIRE = new Region("South Gloucestershire", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region STOKE = new Region("Stoke-on-Trent", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region STOCKTON = new Region("Stockton-on-Tees", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ST_HELENS = new Region("St Helens", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTHAMPTON = new Region("City of Southampton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SHEFFIELD = new Region("Sheffield", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOLIHULL = new Region("Solihull", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SUNDERLAND = new Region("Sunderland", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTHWARK = new Region("Southwark", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTH_TYNESIDE = new Region("South Tyneside", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SUTTON = new Region("Sutton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region TORBAY = new Region("Torbay", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region TOWER_HAMLETS = new Region("Tower Hamlets", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region TRAFFORD = new Region("Trafford", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region TAMESIDE = new Region("Tameside", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region THURROCK = new Region("Thurrock", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WALSALL = new Region("Walsall", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WEST_BERKSHIRE = new Region("West Berkshire", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WINDSOR = new Region("Windsor and Maidenhead", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WAKEFIELD = new Region("Wakefield", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WALTHAM_FOREST = new Region("Waltham Forest", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WARRINGTON = new Region("Warrington", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WOLVERHAMPTON = new Region("City of Wolverhampton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WOKINGHAM = new Region("Wokingham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WESTMINSTER = new Region("City of Westminster", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WIGAN = new Region("Wigan", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region TELFORD = new Region("Telford and Wrekin", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WIRRAL = new Region("Wirral", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WEST_SUSSEX = new Region("West Sussex", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WANDSWORTH = new Region("Wandsworth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region WREXHAM = new Region("Wrexham", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region YORK = new Region("York", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTHEND = new Region("Southend-on-Sea", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SLOUGH = new Region("Slough", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region STOCKPORT = new Region("Stockport", Countries.ENGLAND, Region.Creation.MODERN);
        #endregion

        #region Welsh Regions
        public static Region ANGLESEY = new Region("Anglesey", Countries.WALES, Region.Creation.HISTORIC);
        public static Region BRECON = new Region("Brecon", Countries.WALES, Region.Creation.HISTORIC);
        public static Region CAERNARFON = new Region("Caernarfon", Countries.WALES, Region.Creation.HISTORIC);
        public static Region CEREDIGION = new Region("Ceredigion", Countries.WALES, Region.Creation.HISTORIC);
        public static Region CARMARTHEN = new Region("Carmarthen", Countries.WALES, Region.Creation.HISTORIC);
        public static Region DENBIGH = new Region("Denbigh", Countries.WALES, Region.Creation.HISTORIC);
        public static Region FLINT = new Region("Flint", Countries.WALES, Region.Creation.HISTORIC);
        public static Region GLAMORGAN = new Region("Glamorgan", Countries.WALES, Region.Creation.HISTORIC);
        public static Region MERIONETH = new Region("Merioneth", Countries.WALES, Region.Creation.HISTORIC);
        public static Region MONMOUTH = new Region("Monmouth", Countries.WALES, Region.Creation.HISTORIC);
        public static Region MONTGOMERY = new Region("Montgomery", Countries.WALES, Region.Creation.HISTORIC);
        public static Region PEMBROKE = new Region("Pembroke", Countries.WALES, Region.Creation.HISTORIC);
        public static Region RADNOR = new Region("Radnor", Countries.WALES, Region.Creation.HISTORIC);

        public static Region CLWYD = new Region("Clwyd", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region DYFED = new Region("Dyfed", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region GWENT = new Region("Gwent", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region GWYNEDD = new Region("Gwynedd", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region MID_GLAMORGAN = new Region("Mid Glamorgan", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region POWYS = new Region("Powys", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region SOUTH_GLAMORGAN = new Region("South Glamorgan", Countries.WALES, Region.Creation.LG_ACT1974);
        public static Region WEST_GLAMORGAN = new Region("West Glamorgan", Countries.WALES, Region.Creation.LG_ACT1974);

        public static Region BLAENAU_GWENT = new Region("Blaenau Gwent", Countries.WALES, Region.Creation.MODERN);
        public static Region BRIDGEND = new Region("Bridgend", Countries.WALES, Region.Creation.MODERN);
        public static Region CARDIFF = new Region("Cardiff", Countries.WALES, Region.Creation.MODERN);
        public static Region CAERPHILLY = new Region("Caerphilly", Countries.WALES, Region.Creation.MODERN);
        public static Region CONWY = new Region("Conwy", Countries.WALES, Region.Creation.MODERN);
        public static Region MERTHYL = new Region("Merthyr Tydfil", Countries.WALES, Region.Creation.MODERN);
        public static Region NEATH = new Region("Neath Port Talbot", Countries.WALES, Region.Creation.MODERN);
        public static Region RHONDDA = new Region("Rhondda Cynon Taff", Countries.WALES, Region.Creation.MODERN);
        public static Region SWANSEA = new Region("Swansea", Countries.WALES, Region.Creation.MODERN);
        public static Region TORFAEN = new Region("Torfaen", Countries.WALES, Region.Creation.MODERN);
        public static Region VALE_GLAMORGAN = new Region("Vale of Glamorgan", Countries.WALES, Region.Creation.MODERN);
        #endregion

        #region Northern Ireland Regions
        public static Region ANTRIM = new Region("Antrim", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region ARMAGH = new Region("Armagh", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region DOWN = new Region("Down", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region FERMANAGH = new Region("Fermanagh", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region LONDONDERRY = new Region("Londonderry", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region TYRONE = new Region("Tyrone", Countries.NORTHERN_IRELAND, Region.Creation.HISTORIC);
        public static Region ULSTER = new Region("Ulster", Countries.NORTHERN_IRELAND, Region.Creation.MODERN);

        #endregion

        #region UK Islands
        public static Region IOM = new Region("Isle of Man", Countries.ISLE_OF_MAN, Region.Creation.HISTORIC);
        public static Region JERSEY = new Region("Jersey", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region GUERNSEY = new Region("Guernsey", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region ALDERNEY = new Region("Alderney", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        public static Region SARK = new Region("Sark", Countries.CHANNEL_ISLANDS, Region.Creation.HISTORIC);
        #endregion

        #region Irish Regions
        public static Region CARLOW = new Region("Carlow", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region CAVAN = new Region("Cavan", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region CLARE = new Region("Clare", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region CORK = new Region("Cork", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region DONEGAL = new Region("Donegal", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region DUBLIN = new Region("Dublin", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region GALWAY = new Region("Galway", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region KERRY = new Region("Kerry", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region KILDARE = new Region("Kildare", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region KILKENNY = new Region("Kilkenny", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region LAOIS = new Region("Laois", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region LEITRIM = new Region("Leitrim", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region LIMERICK = new Region("Limerick", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region LONGFORD = new Region("Longford", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region LOUTH = new Region("Louth", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region MAYO = new Region("Mayo", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region MEATH = new Region("Meath", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region MONAGHAN = new Region("Monaghan", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region OFFALY = new Region("Offaly", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region ROSCOMMON = new Region("Roscommon", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region SLIGO = new Region("Sligo", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region TIPPERARY = new Region("Tipperary", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region WATERFORD = new Region("Waterford", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region WESTMEATH = new Region("Westmeath", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region WEXFORD = new Region("Wexford", Countries.IRELAND, Region.Creation.HISTORIC);
        public static Region WICKLOW = new Region("Wicklow", Countries.IRELAND, Region.Creation.HISTORIC);

        public static Region DUN_LAOGHAIRE = new Region("Dún Laoghaire–Rathdown", Countries.IRELAND, Region.Creation.MODERN);
        public static Region FINGAL = new Region("Fingal", Countries.IRELAND, Region.Creation.MODERN);
        public static Region NORTH_TIPPERARY = new Region("North Tipperary", Countries.IRELAND, Region.Creation.MODERN);
        public static Region SOUTH_DUBLIN = new Region("South Dublin", Countries.IRELAND, Region.Creation.MODERN);
        public static Region SOUTH_TIPPERARY = new Region("South Tipperary", Countries.IRELAND, Region.Creation.MODERN);
        public static Region LEINSTER = new Region("Leinster", Countries.IRELAND, Region.Creation.MODERN);
        public static Region MUNSTER = new Region("Munster", Countries.IRELAND, Region.Creation.MODERN);
        public static Region CONNACHT = new Region("Connacht", Countries.IRELAND, Region.Creation.MODERN);
          
        #endregion

        #region Canadian Regions
        public static Region ALBERTA = new Region("Alberta", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region BRITISH_COLUMBIA = new Region("British Columbia", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region MANITOBA = new Region("Manitoba", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region NEW_BRUNSWICK = new Region("New Brunswick", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region NEWFOUNDLAND = new Region("Newfoundland", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region NOVA_SCOTIA = new Region("Nova Scotia", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region ONTARIO = new Region("Ontario", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region PRINCE_EDWARD = new Region("Prince Edward Island", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region QUEBEC = new Region("Quebec", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region SASKATCHEWAN = new Region("Saskatchewan", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region NW_TERRITORIES = new Region("North West Territories", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region NUNAVUT = new Region("Nunavut", Countries.CANADA, Region.Creation.HISTORIC);
        public static Region YUKON = new Region("Yukon Territory", Countries.CANADA, Region.Creation.HISTORIC);
        #endregion

        #region US States
        public static Region ALABAMA = new Region("Alabama", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region ALASKA = new Region("Alaska", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region ARIZONA = new Region("Arizona", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region ARKANSAS = new Region("Arkansas", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region CALIFORNIA = new Region("California", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region COLORADO = new Region("Colorado", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region CONNECTICUT = new Region("Connecticut", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region DELAWARE = new Region("Delaware", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region FLORIDA = new Region("Florida", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region GEORGIA = new Region("Georgia", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region HAWAII = new Region("Hawaii", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region IDAHO = new Region("Idaho", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region ILLINOIS = new Region("Illinois", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region INDIANA = new Region("Indiana", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region IOWA = new Region("Iowa", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region KANSAS = new Region("Kansas", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region KENTUCKY = new Region("Kentucky", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region LOUISIANA = new Region("Louisiana", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MAINE = new Region("Maine", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MARYLAND = new Region("Maryland", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MASSACHUSETTS = new Region("Massachusetts", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MICHIGAN = new Region("Michigan", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MINNESOTA = new Region("Minnesota", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MISSISSIPPI = new Region("Mississippi", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MISSOURI = new Region("Missouri", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region MONTANA = new Region("Montana", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEBRASKA = new Region("Nebraska", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEVADA = new Region("Nevada", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEW_HAMPSHIRE = new Region("New Hampshire", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEW_JERSEY = new Region("New Jersey", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEW_MEXICO = new Region("New Mexico", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NEW_YORK = new Region("New York", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NORTH_CAROLINA = new Region("North Carolina", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region NORTH_DAKOTA = new Region("North Dakota", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region OHIO = new Region("Ohio", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region OKLAHOMA = new Region("Oklahoma", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region OREGON = new Region("Oregon", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region PENNSYLVANIA = new Region("Pennsylvania", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region RHODE_ISLAND = new Region("Rhode Island", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region SOUTH_CAROLINA = new Region("South Carolina", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region SOUTH_DAKOTA = new Region("South Dakota", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region TENNESSEE = new Region("Tennessee", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region TEXAS = new Region("Texas", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region UTAH = new Region("Utah", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region VERMONT = new Region("Vermont", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region VIRGINIA = new Region("Virginia", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region WASHINGTON = new Region("Washington", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region WEST_VIRGINIA = new Region("West Virginia", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region WISCONSIN = new Region("Wisconsin", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region WYOMING = new Region("Wyoming", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        public static Region DC = new Region("District of Columbia", Countries.UNITED_STATES, Region.Creation.HISTORIC);
        #endregion

        #region Australian Regions
        public static Region NSW = new Region("New South Wales", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region QUEENSLAND = new Region("Queensland", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region SAUSTRALIA = new Region("South Australia", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region TASMANIA = new Region("Tasmania", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region VICTORIA = new Region("Victoria", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region WAUSTRALIA = new Region("Western Australia", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region ACT = new Region("Australian Capital Territory", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        public static Region NORTHERN_TERRITORY = new Region("Northern Territory", Countries.AUSTRALIA, Region.Creation.HISTORIC);
        #endregion

        #region New Zealand Regions
        public static Region AUCKLAND = new Region("Auckland", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region BAY_OF_PLENTY = new Region("Bay of Plenty", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region CANTERBURY = new Region("Canterbury", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region HAWKES_BAY = new Region("Hawke's Bay", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region MANAWATU_WANGANUI = new Region("Manawatu-Wanganui", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region NORTHLAND = new Region("Northland", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region OTAGO = new Region("Otago", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region SOUTHLAND = new Region("Southland", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region TARANAKI = new Region("Taranaki", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region WAIKATO = new Region("Waikato", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region WELLINGTON = new Region("Wellington", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region WEST_COAST = new Region("West Coast", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region GISBORNE = new Region("Gisborne District", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region MARBOROUGH = new Region("Marlborough District", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region NELSON = new Region("Nelson City", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region TASMAN = new Region("Tasman District", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region CHATAM_ISLANDS = new Region("Chatham Islands Territory", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region NORTH_ISLAND = new Region("North Island", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        public static Region SOUTH_ISLAND = new Region("South Island", Countries.NEW_ZEALAND, Region.Creation.HISTORIC);
        #endregion
        #endregion

        #region Constructor
        static Regions()
        {
            #region Modern County Setup
            MODERN_COUNTIES = new List<ModernCounty>(new ModernCounty[] { 
                OS_ABERDEENSHIRE, OS_ANGUS, OS_ABERDEEN_CITY, OS_ARGYLL_AND_BUTE, OS_BRADFORD, OS_BLACKBURN_WITH_DARWEN,
                OS_BRACKNELL_FOREST, OS_BARKING_AND_DAGENHAM, OS_BRIDGEND, OS_BEDFORDSHIRE, OS_BLAENAU_GWENT, 
                OS_CITY_OF_BRIGHTON_AND_HOVE, OS_BIRMINGHAM, OS_CENTRAL_BEDFORDSHIRE, OS_BARNSLEY, OS_BUCKINGHAMSHIRE,
                OS_BARNET, OS_BOLTON, OS_BLACKPOOL, OS_BROMLEY, OS_BATH_AND_NORTH_EAST_SOMERSET, OS_BRENT, OS_BOURNEMOUTH,
                OS_BEXLEY, OS_BURY, OS_BRISTOL, OS_CALDERDALE, OS_CAMBRIDGESHIRE, OS_CHESHIRE_WEST_AND_CHESTER, OS_CARDIFF,
                OS_CEREDIGION, OS_CAERPHILLY, OS_CHESHIRE_EAST, OS_CLACKMANNANSHIRE, OS_CAMDEN, OS_CORNWALL, 
                OS_CARMARTHENSHIRE, OS_CUMBRIA, OS_COVENTRY, OS_CONWY, OS_CROYDON, OS_CITY_OF_DERBY, OS_DUNDEE_CITY, 
                OS_DENBIGHSHIRE, OS_DUMFRIES_AND_GALLOWAY, OS_DARLINGTON, OS_DEVON, OS_DONCASTER, OS_DORSET, OS_DURHAM, 
                OS_DERBYSHIRE, OS_DUDLEY, OS_EAST_AYRSHIRE, OS_CITY_OF_EDINBURGH, OS_EAST_DUNBARTONSHIRE, OS_EALING, 
                OS_EAST_LOTHIAN, OS_ENFIELD, OS_EAST_RENFREWSHIRE, OS_EAST_SUSSEX, OS_ESSEX, OS_EAST_RIDING_OF_YORKSHIRE, 
                OS_FALKIRK, OS_FIFE, OS_FLINTSHIRE, OS_GATESHEAD, OS_GLASGOW_CITY, OS_GLOUCESTERSHIRE, OS_GREENWICH, 
                OS_GWYNEDD, OS_HALTON, OS_HERTFORDSHIRE, OS_HEREFORDSHIRE, OS_HAMMERSMITH_AND_FULHAM, OS_HARINGEY, 
                OS_HILLINGDON, OS_HIGHLAND, OS_HACKNEY, OS_HAMPSHIRE, OS_HARROW, OS_HOUNSLOW, OS_HARTLEPOOL, OS_HAVERING,
                OS_ISLE_OF_ANGLESEY, OS_ISLINGTON, OS_ISLE_OF_MAN, OS_INVERCLYDE, OS_ISLES_OF_SCILLY, OS_CITY_OF_INVERNESS,
                OS_ISLE_OF_WIGHT, OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA, OS_KINGSTON_UPON_THAMES, 
                OS_CITY_OF_KINGSTON_UPON_HULL, OS_KIRKLEES, OS_KNOWSLEY, OS_KENT, OS_LANCASHIRE, OS_LAMBETH, 
                OS_CITY_OF_LEICESTER, OS_LEEDS, OS_LINCOLNSHIRE, OS_LUTON, OS_CITY_OF_LONDON, OS_LIVERPOOL, 
                OS_LEWISHAM, OS_LEICESTERSHIRE, OS_MANCHESTER, OS_MIDDLESBROUGH, OS_MEDWAY, OS_MIDLOTHIAN, OS_MILTON_KEYNES,
                OS_MONMOUTHSHIRE, OS_MORAY, OS_MERTON, OS_MERTHYR_TYDFIL, OS_NORTH_AYRSHIRE, OS_NORTH_EAST_LINCOLNSHIRE,
                OS_NORTHUMBERLAND, OS_NEWPORT, OS_CITY_OF_NOTTINGHAM, OS_NEWHAM, OS_NORTH_LINCOLNSHIRE, OS_NORFOLK,
                OS_NORTH_LANARKSHIRE, OS_NORTHAMPTONSHIRE, OS_NEATH_PORT_TALBOT, OS_NORTH_TYNESIDE, OS_NORTH_SOMERSET,
                OS_NOTTINGHAMSHIRE, OS_NEWCASTLE_UPON_TYNE, OS_NORTH_YORKSHIRE, OS_OLDHAM, OS_ORKNEY_ISLANDS, OS_OXFORDSHIRE, 
                OS_PEMBROKESHIRE, OS_CITY_OF_PETERBOROUGH, OS_PERTH_AND_KINROSS, OS_POOLE, OS_CITY_OF_PORTSMOUTH, OS_POWYS,
                OS_CITY_OF_PLYMOUTH, OS_REDBRIDGE, OS_REDCAR_AND_CLEVELAND, OS_ROCHDALE, OS_RENFREWSHIRE, OS_READING, 
                OS_RHONDDA_CYNON_TAFF, OS_RUTLAND, OS_ROTHERHAM, OS_RICHMOND_UPON_THAMES, OS_SANDWELL, OS_SCOTTISH_BORDERS, 
                OS_SALFORD, OS_SWINDON, OS_SEFTON, OS_STAFFORDSHIRE, OS_SOUTH_GLOUCESTERSHIRE, OS_SHROPSHIRE, OS_SHETLAND_ISLANDS, 
                OS_CITY_OF_STOKE_ON_TRENT, OS_SUFFOLK, OS_SOUTH_LANARKSHIRE, OS_STOCKTON_ON_TEES, OS_ST_HELENS, 
                OS_CITY_OF_SOUTHAMPTON, OS_SHEFFIELD, OS_SOLIHULL, OS_STIRLING, OS_SWANSEA, OS_SOMERSET, OS_SURREY, 
                OS_SUNDERLAND, OS_SOUTHWARK, OS_SOUTH_AYRSHIRE, OS_SOUTH_TYNESIDE, OS_SUTTON, OS_TORBAY, OS_TORFAEN, 
                OS_TOWER_HAMLETS, OS_TRAFFORD, OS_TAMESIDE, OS_THURROCK, OS_VALE_OF_GLAMORGAN, OS_WALSALL, OS_WEST_BERKSHIRE,
                OS_WINDSOR_AND_MAIDENHEAD, OS_WEST_DUNBARTONSHIRE, OS_WAKEFIELD, OS_WALTHAM_FOREST, OS_WARRINGTON, 
                OS_CITY_OF_WOLVERHAMPTON, OS_WESTERN_ISLES, OS_WOKINGHAM, OS_WARWICKSHIRE, OS_WEST_LOTHIAN, 
                OS_CITY_OF_WESTMINSTER, OS_WIGAN, OS_WORCESTERSHIRE, OS_TELFORD_AND_WREKIN, OS_WIRRAL, OS_WEST_SUSSEX, 
                OS_WILTSHIRE, OS_WANDSWORTH, OS_WREXHAM, OS_YORK, OS_SOUTHEND_ON_SEA, OS_SLOUGH, OS_STOCKPORT
            });
            #endregion

            #region UK Regions
            // List from Scotland's People
            SCOTTISH_REGIONS = new HashSet<Region>(new Region[]{
                    ABERDEEN, ANGUS, ARGYLL, AYR, BANFF, BERWICK, BUTE, CAITHNESS, CLACKMANNAN, DUMFRIES,
                    DUNBARTON, EAST_LOTHIAN, FIFE, INVERNESS, KINCARDINE, KINROSS, KIRKCUDBRIGHT, LANARK, 
                    MIDLOTHIAN, MORAY, NAIRN, ORKNEY, PEEBLES, PERTH, RENFREW, ROSS_CROMARTY, ROXBURGH, 
                    SELKIRK, SHETLAND, STIRLING, SUTHERLAND, WEST_LOTHIAN, WIGTOWN, BORDERS, CENTRAL_SCOT,
                    DUMFRIES_GALLOWAY, GRAMPIAN, HIGHLAND, LOTHIAN, STRATHCLYDE, TAYSIDE, WESTERN_ISLES,
                    ABERDEEN_CITY, ARGYLL_BUTE, DUNDEE_CITY, EAST_AYRSHIRE, EDINBURGH_CITY, 
                    EAST_DUNBARTONSHIRE, EAST_RENFREW, FALKIRK, GLASGOW_CITY, INVERCLYDE, INVERNESS_CITY
                });
            AddScottishRegionAlternates();

            ENGLISH_REGIONS = new HashSet<Region>(new Region[] {
                    BEDS, BERKS, BUCKS, CAMBS, CHESHIRE, CORNWALL, CUMBERLAND, DERBY, DEVON, DORSET,
                    DURHAM, ESSEX, GLOUCS, HANTS, HEREFORD, HERTS, HUNTS, KENT, LANCS, LEICS, LINCS, 
                    MIDDLESEX, NORFOLK, NORTHAMPTON, NORTHUMBERLAND, NOTTS, OXFORD, RUTLAND, SHROPS, 
                    SOMERSET, STAFFS, SUFFOLK, SURREY, SUSSEX, WARWICK, WESTMORLAND, WILTS, WORCESTER,
                    YORKS, LONDON, MANCHESTER, MERSEYSIDE, SOUTH_YORKSHIRE, TYNE_WEAR, WEST_MIDLANDS,
                    WEST_YORKSHIRE, AVON, CLEVELAND, CUMBRIA, HUMBERSIDE, IOW, HEREFORD_WORCESTER,
                    NORTH_YORKSHIRE, EAST_YORKSHIRE, BRADFORD, BLACKBURN, BRACKNELL, BARKING, BRIGHTON,
                    BIRMINGHAM, BARNSLEY, BARNET, BOLTON, BLACKPOOL, BROMLEY, BATH, BRENT, BOURNEMOUTH,
                    BEXLEY, BURY, BRISTOL, CALDERDALE, CAMDEN, COVENTRY, CROYDON, DERBY_CITY,  DARLINGTON,
                    DONCASTER, DUDLEY, EALING, ENFIELD, EAST_SUSSEX, GATESHEAD, GREENWICH, HALTON, 
                    HAMMERSMITH, HARINGEY, HILLINGDON, HACKNEY, HARROW, HOUNSLOW, HARTLEPOOL, HAVERING,
                    ISLINGTON, ISLES_OF_SCILLY, KENSINGTON, KINGSTON_THAMES, KINGSTON_HULL, KIRKLEES, 
                    KNOWSLEY, LAMBETH, LEICESTER_CITY, LEEDS, LUTON, LIVERPOOL, LEWISHAM, MIDDLESBROUGH,
                    MEDWAY, MILTON_KEYNES, MERTON, NE_LINCOLNSHIRE, NEWPORT, NOTTINGHAM_CITY, NEWHAM, 
                    NORTH_LINCOLNSHIRE, NORTH_TYNESIDE, NORTH_SOMERSET, NEWCASTLE, OLDHAM, PETERBOROUGH, 
                    POOLE, PORTSMOUTH, PLYMOUTH, REDBRIDGE, REDCAR, ROCHDALE, READING, ROTHERHAM, 
                    RICHMOND_THAMES, SANDWELL, SALFORD, SWINDON, SEFTON, SOUTH_GLOUCESTERSHIRE, STOKE, 
                    STOCKTON, ST_HELENS, SOUTHAMPTON, SHEFFIELD, SOLIHULL, SUNDERLAND, SOUTHWARK, 
                    SOUTH_TYNESIDE, SUTTON, TORBAY, TOWER_HAMLETS, TRAFFORD, TAMESIDE, THURROCK, WALSALL, 
                    WEST_BERKSHIRE, WINDSOR, WAKEFIELD, WALTHAM_FOREST, WARRINGTON, WOLVERHAMPTON, 
                    WOKINGHAM, WESTMINSTER, WIGAN, TELFORD, WIRRAL, WEST_SUSSEX, WANDSWORTH, WREXHAM, 
                    YORK, SOUTHEND, SLOUGH, STOCKPORT
            });
            AddEnglishRegionAlternates();

            WELSH_REGIONS = new HashSet<Region>(new Region[] {
                    ANGLESEY, BRECON, CAERNARFON, CEREDIGION, CARMARTHEN, DENBIGH, FLINT, GLAMORGAN, MERIONETH, 
                    MONMOUTH, MONTGOMERY, PEMBROKE, RADNOR, CLWYD, DYFED, GWENT, GWYNEDD, MID_GLAMORGAN, 
                    POWYS, SOUTH_GLAMORGAN, WEST_GLAMORGAN, BLAENAU_GWENT, BRIDGEND, CARDIFF, CAERPHILLY,
                    CONWY, BLAENAU_GWENT, BRIDGEND, CARDIFF, CAERPHILLY, CONWY, MERTHYL, NEATH, RHONDDA,
                    SWANSEA, TORFAEN, VALE_GLAMORGAN
            });
            AddWelshRegionAlternates();

            NIRELAND_REGIONS = new HashSet<Region>(new Region[] {
                ANTRIM, ARMAGH, DOWN, FERMANAGH, LONDONDERRY, TYRONE, ULSTER
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
            #endregion 
            
            #region Overseas Regions
            IRISH_REGIONS = new HashSet<Region>(new Region[] { 
                CARLOW, CAVAN, CLARE, CORK, DONEGAL, DUBLIN, GALWAY, KERRY, KILDARE, KILKENNY, LAOIS, LEITRIM,
                LIMERICK, LONGFORD, LOUTH, MAYO, MEATH, MONAGHAN, OFFALY, ROSCOMMON, SLIGO, TIPPERARY, WATERFORD, 
                WESTMEATH, WEXFORD, WICKLOW, DUN_LAOGHAIRE, FINGAL, NORTH_TIPPERARY, SOUTH_DUBLIN, SOUTH_TIPPERARY,
                LEINSTER, MUNSTER, CONNACHT
            });
            AddIrishRegionAlternates();

            CANADIAN_REGIONS = new HashSet<Region>(new Region[] { 
                ALBERTA, BRITISH_COLUMBIA, MANITOBA, NEW_BRUNSWICK, NEWFOUNDLAND, NOVA_SCOTIA, ONTARIO, 
                PRINCE_EDWARD, QUEBEC, SASKATCHEWAN, NW_TERRITORIES, NUNAVUT, YUKON
            });
            AddCanadianRegionAlternates();

            US_STATES = new HashSet<Region>(new Region[] { 
                ALABAMA, ALASKA, ARIZONA, ARKANSAS, CALIFORNIA, COLORADO, CONNECTICUT, DELAWARE, FLORIDA,
                GEORGIA, HAWAII, IDAHO, ILLINOIS, INDIANA, IOWA, KANSAS, KENTUCKY, LOUISIANA, MAINE, 
                MARYLAND, MASSACHUSETTS, MICHIGAN, MINNESOTA, MISSISSIPPI, MISSOURI, MONTANA, NEBRASKA,
                NEVADA, NEW_HAMPSHIRE, NEW_JERSEY, NEW_MEXICO, NEW_YORK, NORTH_CAROLINA, NORTH_DAKOTA,
                OHIO, OKLAHOMA, OREGON, PENNSYLVANIA, RHODE_ISLAND, SOUTH_CAROLINA, SOUTH_DAKOTA,
                TENNESSEE, TEXAS, UTAH, VERMONT, VIRGINIA, WASHINGTON, WEST_VIRGINIA, WISCONSIN, 
                WYOMING, DC
            });
            AddUSStatesAlternates();

            AUSTRALIAN_REGIONS = new HashSet<Region>(new Region[] { 
                NSW, QUEENSLAND, SAUSTRALIA, TASMANIA, VICTORIA, WAUSTRALIA, ACT, NORTHERN_TERRITORY
            });
            AddAustralianRegionAlternates();

            NEW_ZEALAND_REGIONS = new HashSet<Region>(new Region[] { 
                AUCKLAND, BAY_OF_PLENTY, CANTERBURY, HAWKES_BAY, MANAWATU_WANGANUI, NORTHLAND, OTAGO,
                SOUTHLAND, TARANAKI, WAIKATO, WELLINGTON, WEST_COAST, GISBORNE, MARBOROUGH, NELSON,
                TASMAN, CHATAM_ISLANDS, NORTH_ISLAND, SOUTH_ISLAND
            });
            AddNewZealandRegionAlternates();
            #endregion

            #region Valid Regions
            PREFERRED_REGIONS = new List<Region>();
            VALID_REGIONS = new Dictionary<string, Region>();
            AppendValidRegions(UK_REGIONS);
            AppendValidRegions(IRISH_REGIONS);
            AppendValidRegions(CANADIAN_REGIONS);
            AppendValidRegions(US_STATES);
            AppendValidRegions(AUSTRALIAN_REGIONS);
            #endregion

            AddConversions();
            SetRegionsConversions();
        }
        #endregion

        #region Lookup Functions
        public static List<ModernCounty> GetCounties(Region lookup)
        {
            if (CONVERSIONS.ContainsKey(lookup))
                return CONVERSIONS[lookup];
            return new List<ModernCounty>();
        }

        public static ModernCounty OS_GetCounty(string code)
        {
            return MODERN_COUNTIES.FirstOrDefault(c => c.CountyCode.Equals(code));
        }

        private static void AppendValidRegions(ISet<Region> regions)
        {
            foreach (Region r in regions)
            {
                VALID_REGIONS.Add(r.PreferredName, r);
                PREFERRED_REGIONS.Add(r);
                foreach (string alternate in r.AlternativeNames)
                    VALID_REGIONS.Add(alternate, r);
            }
        }

        public static bool IsKnownRegion(string region)
        {
            return VALID_REGIONS.ContainsKey(region);
        }

        public static bool IsPreferredRegion(string region)
        {
            return PREFERRED_REGIONS.Any(x => x.PreferredName.Equals(region));
        }

        public static Region GetRegion(string region)
        {
            region = EnhancedTextInfo.RemoveDiacritics(region); // remove any special characters for Known Region processing
            if (VALID_REGIONS.TryGetValue(region, out Region result))
                return result;
            return null;
        }
        #endregion

        #region Alternates
        private static void AddScottishRegionAlternates()
        {
            // add Anglicised shires
            ANGUS.AddAlternateName("Forfarshire");
            ARGYLL.AddAlternateName("Argyllshire");
            BANFF.AddAlternateName("Banff");
            BERWICK.AddAlternateName("Berwick");
            BUTE.AddAlternateName("Buteshire");
            CLACKMANNAN.AddAlternateName("Clackmannan");
            DUMFRIES.AddAlternateName("Dumfries");
            DUMFRIES.AddAlternateName("Dumfriesshire");
            DUNBARTON.AddAlternateName("Dunbarton");
            DUNBARTON.AddAlternateName("Dumbartonshire");
            DUNBARTON.AddAlternateName("Dumbarton");
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
            MIDLOTHIAN.AddAlternateName("Mid Lothian");
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
            WIGTOWN.AddAlternateName("Wigton");
            WIGTOWN.AddAlternateName("Wigtonshire");
            SHETLAND.AddAlternateName("Zetland");
            ROSS_CROMARTY.AddAlternateName("Ross & Cromarty");
            FIFE.AddAlternateName("Kingdom of Fife");
            CENTRAL_SCOT.AddAlternateName("Central Scotland");
            HIGHLAND.AddAlternateName("Highlands");
            HIGHLAND.AddAlternateName("Highlands & Islands");
            HIGHLAND.AddAlternateName("Highlands and Islands");
            LOTHIAN.AddAlternateName("Lothians");
            WESTERN_ISLES.AddAlternateName("Na h-Eileanan an Iar");
            BORDERS.AddAlternateName("Scottish Borders");
        }

        private static void AddWelshRegionAlternates()
        { // http://www.gazetteer.org.uk/contents.php
            ANGLESEY.AddAlternateName("Sir Fon");
            ANGLESEY.AddAlternateName("Isle of Anglesey");
            BRECON.AddAlternateName("Sir Frycheiniog");
            BRECON.AddAlternateName("Brecknockshire");
            BRECON.AddAlternateName("Breconshire");
            CAERNARFON.AddAlternateName("Sir Gaernarfon");
            CAERNARFON.AddAlternateName("Caernarfonshire");
            CAERNARFON.AddAlternateName("Caernarvonshire");
            CAERNARFON.AddAlternateName("Caernarvon");
            CEREDIGION.AddAlternateName("Cardiganshire");
            CEREDIGION.AddAlternateName("Ceredigionshire");
            CEREDIGION.AddAlternateName("Cardigan");
            CARMARTHEN.AddAlternateName("Sir Gaerfyrddin");
            CARMARTHEN.AddAlternateName("Carmarthenshire");
            DENBIGH.AddAlternateName("Denbighshire");
            DENBIGH.AddAlternateName("Sir Ddinbych");
            FLINT.AddAlternateName("Flintshire");
            FLINT.AddAlternateName("Sir y Fflint");
            GLAMORGAN.AddAlternateName("Morgannwg");
            MERIONETH.AddAlternateName("Merionethshire");
            MERIONETH.AddAlternateName("Meirionnydd");
            MONMOUTH.AddAlternateName("Monmouthshire");
            MONMOUTH.AddAlternateName("Sir Fynwy");
            MONTGOMERY.AddAlternateName("Montogmeryshire");
            MONTGOMERY.AddAlternateName("Sir Drefaldwyn");
            PEMBROKE.AddAlternateName("Pembrokeshire");
            PEMBROKE.AddAlternateName("Sir Benfro");
            RADNOR.AddAlternateName("Radnorshire");
            RADNOR.AddAlternateName("Sir Faesyfed");
        }

        private static void AddNorthernIrelandRegionAlternates()
        {
            ANTRIM.AddAlternateName("County Antrim");
            ARMAGH.AddAlternateName("County Armagh");
            DOWN.AddAlternateName("County Down");
            FERMANAGH.AddAlternateName("County Fermanagh");
            LONDONDERRY.AddAlternateName("County Londonderry");
            TYRONE.AddAlternateName("County Tyrone");
            ANTRIM.AddAlternateName("Co Antrim");
            ARMAGH.AddAlternateName("Co Armagh");
            DOWN.AddAlternateName("Co Down");
            FERMANAGH.AddAlternateName("Co Fermanagh");
            LONDONDERRY.AddAlternateName("Co Londonderry");
            TYRONE.AddAlternateName("Co Tyrone");
            LONDONDERRY.AddAlternateName("Derry");
            LONDONDERRY.AddAlternateName("Co Derry");
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
            DURHAM.AddAlternateName("Durham");
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
            LINCS.AddAlternateName("Lincs");
            LONDON.AddAlternateName("City of London");
            MIDDLESEX.AddAlternateName("Mx");
            MIDDLESEX.AddAlternateName("Middx");
            MIDDLESEX.AddAlternateName("Midx");
            MIDDLESEX.AddAlternateName("Mddx");
            NORFOLK.AddAlternateName("Norf");
            NORTHAMPTON.AddAlternateName("Northants");
            NORTHUMBERLAND.AddAlternateName("Northumb");
            NORTHUMBERLAND.AddAlternateName("Northd");
            NOTTS.AddAlternateName("Notts");
            OXFORD.AddAlternateName("Oxon");
            RUTLAND.AddAlternateName("Rut");
            SHROPS.AddAlternateName("Shrops");
            SHROPS.AddAlternateName("Salop");
            SOMERSET.AddAlternateName("Som");
            SOMERSET.AddAlternateName("Somersetshire");
            STAFFS.AddAlternateName("Staffs");
            STAFFS.AddAlternateName("Staf");
            SUFFOLK.AddAlternateName("Suff");
            SUFFOLK.AddAlternateName("West Suffolk");
            SUFFOLK.AddAlternateName("East Suffolk");
            SURREY.AddAlternateName("Sy");
            SURREY.AddAlternateName("East Surrey");
            SURREY.AddAlternateName("West Surrey");
            SUSSEX.AddAlternateName("Sx");
            SUSSEX.AddAlternateName("Ssx");
            WARWICK.AddAlternateName("Warw");
            WARWICK.AddAlternateName("Warks");
            WARWICK.AddAlternateName("War");
            WARWICK.AddAlternateName("Warwick");
            WESTMORLAND.AddAlternateName("Westm");
            WILTS.AddAlternateName("Wilts");
            WORCESTER.AddAlternateName("Worcs");
            YORKS.AddAlternateName("Yorks");

            TYNE_WEAR.AddAlternateName("Tyne & Wear");
            LONDON.AddAlternateName("Greater London");
            MANCHESTER.AddAlternateName("Greater Manchester");
        }

        private static void AddIrishRegionAlternates()
        {
           CARLOW.AddAlternateName("Co Carlow");        
           CARLOW.AddAlternateName("County Carlow");    
           CAVAN.AddAlternateName("Co Cavan");         
           CAVAN.AddAlternateName("County Cavan");     
           CLARE.AddAlternateName("Co Clare");         
           CLARE.AddAlternateName("County Clare");     
           CORK.AddAlternateName("Co Cork");          
           CORK.AddAlternateName("County Cork");      
           DONEGAL.AddAlternateName("Co Donegal");       
           DONEGAL.AddAlternateName("County Donegal");   
           DUBLIN.AddAlternateName("Co Dublin");        
           DUBLIN.AddAlternateName("County Dublin");    
           GALWAY.AddAlternateName("Co Galway");        
           GALWAY.AddAlternateName("County Galway");    
           KERRY.AddAlternateName("Co Kerry");         
           KERRY.AddAlternateName("County Kerry");     
           KILDARE.AddAlternateName("Co Kildare");       
           KILDARE.AddAlternateName("County Kildare");   
           KILKENNY.AddAlternateName("Co Kilkenny");      
           KILKENNY.AddAlternateName("County Kilkenny");  
           LAOIS.AddAlternateName("Leix");
           LAOIS.AddAlternateName("Co Leix");
           LAOIS.AddAlternateName("County Leix");
           LAOIS.AddAlternateName("Co Laois");
           LAOIS.AddAlternateName("County Laois");
           LAOIS.AddAlternateName("Laoighis");
           LEITRIM.AddAlternateName("Co Leitrim");       
           LEITRIM.AddAlternateName("County Leitrim");   
           LIMERICK.AddAlternateName("Co Limerick");      
           LIMERICK.AddAlternateName("County Limerick");  
           LONGFORD.AddAlternateName("Co Longford");      
           LONGFORD.AddAlternateName("County Longford");  
           LOUTH.AddAlternateName("Co Louth");         
           LOUTH.AddAlternateName("County Louth");     
           MAYO.AddAlternateName("Co Mayo");          
           MAYO.AddAlternateName("County Mayo");      
           MEATH.AddAlternateName("Co Meath");         
           MEATH.AddAlternateName("County Meath");     
           MONAGHAN.AddAlternateName("Co Monaghan");      
           MONAGHAN.AddAlternateName("County Monaghan");  
           OFFALY.AddAlternateName("Co Offaly");        
           OFFALY.AddAlternateName("County Offaly");    
           ROSCOMMON.AddAlternateName("Co Roscommon");     
           ROSCOMMON.AddAlternateName("County Roscommon"); 
           SLIGO.AddAlternateName("Co Sligo");         
           SLIGO.AddAlternateName("County Sligo");     
           TIPPERARY.AddAlternateName("Co Tipperary");        
           TIPPERARY.AddAlternateName("County Tipperary");    
           WATERFORD.AddAlternateName("Co Waterford");     
           WATERFORD.AddAlternateName("County Waterford"); 
           WESTMEATH.AddAlternateName("Co Westmeath");     
           WESTMEATH.AddAlternateName("County Westmeath"); 
           WEXFORD.AddAlternateName("Co Wexford");       
           WEXFORD.AddAlternateName("County Wexford");   
           WICKLOW.AddAlternateName("Co Wicklow");       
           WICKLOW.AddAlternateName("County Wicklow");
           CONNACHT.AddAlternateName("Connaught");
        }

        private static void AddCanadianRegionAlternates()
        {
            BRITISH_COLUMBIA.AddAlternateName("Colombie-Britannique");
            NEW_BRUNSWICK.AddAlternateName("Nouveau-Brunswick");
            NEWFOUNDLAND.AddAlternateName("Newfoundland and Labrador");
            NEWFOUNDLAND.AddAlternateName("Labrador");
            NEWFOUNDLAND.AddAlternateName("Terre-Neuve-et-Labrador");
            NOVA_SCOTIA.AddAlternateName("Nouvelle-Écosse");
            PRINCE_EDWARD.AddAlternateName("Île-du-Prince-Édouard");
            PRINCE_EDWARD.AddAlternateName("Prince Edward");
            QUEBEC.AddAlternateName("Québec");
            NW_TERRITORIES.AddAlternateName("Territoires du Nord-Ouest");
            NW_TERRITORIES.AddAlternateName("Northwest Territory");
            NW_TERRITORIES.AddAlternateName("North West Territory");
            NW_TERRITORIES.AddAlternateName("Northwest Territories");
            YUKON.AddAlternateName("Yukon");
            YUKON.AddAlternateName("Territoire du Yukon");
        }

        private static void AddUSStatesAlternates()
        {
            DC.AddAlternateName("DC");
            DC.AddAlternateName("Dist of Columbia");
        }

        private static void AddAustralianRegionAlternates()
        {
            ACT.AddAlternateName("ACT");
            TASMANIA.AddAlternateName("Van Diemen's Land");
            TASMANIA.AddAlternateName("Van Diemens Land");
            TASMANIA.AddAlternateName("VDL");
        }

        private static void AddNewZealandRegionAlternates()
        {
            AUCKLAND.AddAlternateName("Tāmaki-makau-rau");
            BAY_OF_PLENTY.AddAlternateName("Te Moana a Toi Te Huatahi");
            CANTERBURY.AddAlternateName("Waitaha");
            HAWKES_BAY.AddAlternateName("Te Matau a Māui");
            MANAWATU_WANGANUI.AddAlternateName("Manawatu Whanganui");
            NORTHLAND.AddAlternateName("Te Tai tokerau");
            OTAGO.AddAlternateName("Ō Tākou");
            SOUTHLAND.AddAlternateName("Murihiku");
            WELLINGTON.AddAlternateName("Te Whanga-nui-a-Tara");
            WEST_COAST.AddAlternateName("Te Taihau ā uru");
            GISBORNE.AddAlternateName("Gisborne");
            GISBORNE.AddAlternateName("Tūranga nui a Kiwa");
            MARBOROUGH.AddAlternateName("Marborough");
            NELSON.AddAlternateName("Nelson");
            NELSON.AddAlternateName("Whakatū");
            CHATAM_ISLANDS.AddAlternateName("Wharekauri");
        }
        #endregion

        #region Conversions
        private static void AddConversions()
        {
            #region Scottish Conversions
            AddConversion(ABERDEEN, OS_ABERDEENSHIRE);
            AddConversion(ABERDEEN, OS_ABERDEEN_CITY);
            AddConversion(ABERDEEN, OS_MORAY);
            AddConversion(ABERDEEN_CITY, OS_ABERDEENSHIRE);
            AddConversion(ABERDEEN_CITY, OS_ABERDEEN_CITY);
            AddConversion(ANGUS, OS_ABERDEENSHIRE);
            AddConversion(ANGUS, OS_ANGUS);
            AddConversion(ANGUS, OS_DUNDEE_CITY);
            AddConversion(ANGUS, OS_PERTH_AND_KINROSS);
            AddConversion(ARGYLL, OS_ARGYLL_AND_BUTE);
            AddConversion(ARGYLL, OS_HIGHLAND);
            AddConversion(ARGYLL, OS_PERTH_AND_KINROSS);
            AddConversion(ARGYLL, OS_STIRLING);
            AddConversion(AYR, OS_EAST_AYRSHIRE);
            AddConversion(AYR, OS_NORTH_AYRSHIRE);
            AddConversion(AYR, OS_SOUTH_AYRSHIRE);
            AddConversion(BANFF, OS_ABERDEENSHIRE);
            AddConversion(BANFF, OS_MORAY);
            AddConversion(BERWICK, OS_EAST_LOTHIAN);
            AddConversion(BERWICK, OS_MIDLOTHIAN);
            AddConversion(BERWICK, OS_SCOTTISH_BORDERS);
            AddConversion(BUTE, OS_ARGYLL_AND_BUTE);
            AddConversion(BUTE, OS_NORTH_AYRSHIRE);
            AddConversion(CAITHNESS, OS_HIGHLAND);
            AddConversion(CLACKMANNAN, OS_CLACKMANNANSHIRE);
            AddConversion(CLACKMANNAN, OS_PERTH_AND_KINROSS);
            AddConversion(CLACKMANNAN, OS_STIRLING);
            AddConversion(DUMFRIES, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(DUMFRIES, OS_SCOTTISH_BORDERS);
            AddConversion(DUMFRIES, OS_SOUTH_LANARKSHIRE);
            AddConversion(DUNBARTON, OS_EAST_DUNBARTONSHIRE);
            AddConversion(DUNBARTON, OS_GLASGOW_CITY);
            AddConversion(DUNBARTON, OS_NORTH_LANARKSHIRE);
            AddConversion(DUNBARTON, OS_STIRLING);
            AddConversion(DUNBARTON, OS_WEST_DUNBARTONSHIRE);
            AddConversion(EAST_LOTHIAN, OS_CITY_OF_EDINBURGH);
            AddConversion(EAST_LOTHIAN, OS_EAST_LOTHIAN);
            AddConversion(EAST_LOTHIAN, OS_MIDLOTHIAN);
            AddConversion(EAST_LOTHIAN, OS_SCOTTISH_BORDERS);
            AddConversion(FIFE, OS_FIFE);
            AddConversion(FIFE, OS_PERTH_AND_KINROSS);
            AddConversion(INVERNESS, OS_ABERDEENSHIRE);
            AddConversion(INVERNESS, OS_ARGYLL_AND_BUTE);
            AddConversion(INVERNESS, OS_HIGHLAND);
            AddConversion(INVERNESS, OS_MORAY);
            AddConversion(INVERNESS, OS_WESTERN_ISLES);
            AddConversion(KINCARDINE, OS_ABERDEENSHIRE);
            AddConversion(KINCARDINE, OS_ABERDEEN_CITY);
            AddConversion(KINCARDINE, OS_ANGUS);
            AddConversion(KINROSS, OS_CLACKMANNANSHIRE);
            AddConversion(KINROSS, OS_FIFE);
            AddConversion(KINROSS, OS_PERTH_AND_KINROSS);
            AddConversion(KIRKCUDBRIGHT, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(LANARK, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(LANARK, OS_EAST_RENFREWSHIRE);
            AddConversion(LANARK, OS_GLASGOW_CITY);
            AddConversion(LANARK, OS_NORTH_LANARKSHIRE);
            AddConversion(LANARK, OS_SCOTTISH_BORDERS);
            AddConversion(LANARK, OS_SOUTH_LANARKSHIRE);
            AddConversion(LANARK, OS_WEST_LOTHIAN);
            AddConversion(MIDLOTHIAN, OS_CITY_OF_EDINBURGH);
            AddConversion(MIDLOTHIAN, OS_EAST_LOTHIAN);
            AddConversion(MIDLOTHIAN, OS_MIDLOTHIAN);
            AddConversion(MIDLOTHIAN, OS_SCOTTISH_BORDERS);
            AddConversion(MIDLOTHIAN, OS_WEST_LOTHIAN);
            AddConversion(MORAY, OS_ABERDEENSHIRE);
            AddConversion(MORAY, OS_HIGHLAND);
            AddConversion(MORAY, OS_MORAY);
            AddConversion(NAIRN, OS_HIGHLAND);
            AddConversion(NAIRN, OS_MORAY);
            AddConversion(ORKNEY, OS_ORKNEY_ISLANDS);
            AddConversion(PEEBLES, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(PEEBLES, OS_MIDLOTHIAN);
            AddConversion(PEEBLES, OS_SCOTTISH_BORDERS);
            AddConversion(PEEBLES, OS_SOUTH_LANARKSHIRE);
            AddConversion(PEEBLES, OS_WEST_LOTHIAN);
            AddConversion(PERTH, OS_ANGUS);
            AddConversion(PERTH, OS_CLACKMANNANSHIRE);
            AddConversion(PERTH, OS_DUNDEE_CITY);
            AddConversion(PERTH, OS_FIFE);
            AddConversion(PERTH, OS_PERTH_AND_KINROSS);
            AddConversion(PERTH, OS_STIRLING);
            AddConversion(RENFREW, OS_EAST_RENFREWSHIRE);
            AddConversion(RENFREW, OS_GLASGOW_CITY);
            AddConversion(RENFREW, OS_INVERCLYDE);
            AddConversion(RENFREW, OS_RENFREWSHIRE);
            AddConversion(ROSS_CROMARTY, OS_HIGHLAND);
            AddConversion(ROSS_CROMARTY, OS_WESTERN_ISLES);
            AddConversion(ROXBURGH, OS_SCOTTISH_BORDERS);
            AddConversion(SELKIRK, OS_SCOTTISH_BORDERS);
            AddConversion(SHETLAND, OS_SHETLAND_ISLANDS);
            AddConversion(STIRLING, OS_CLACKMANNANSHIRE);
            AddConversion(STIRLING, OS_EAST_DUNBARTONSHIRE);
            AddConversion(STIRLING, OS_FALKIRK);
            AddConversion(STIRLING, OS_NORTH_LANARKSHIRE);
            AddConversion(STIRLING, OS_PERTH_AND_KINROSS);
            AddConversion(STIRLING, OS_STIRLING);
            AddConversion(SUTHERLAND, OS_HIGHLAND);
            AddConversion(WEST_LOTHIAN, OS_CITY_OF_EDINBURGH);
            AddConversion(WEST_LOTHIAN, OS_FALKIRK);
            AddConversion(WEST_LOTHIAN, OS_NORTH_LANARKSHIRE);
            AddConversion(WEST_LOTHIAN, OS_SCOTTISH_BORDERS);
            AddConversion(WEST_LOTHIAN, OS_SOUTH_LANARKSHIRE);
            AddConversion(WEST_LOTHIAN, OS_WEST_LOTHIAN);
            AddConversion(WIGTOWN, OS_DUMFRIES_AND_GALLOWAY);
            #endregion

            #region 1974 LG act regions
            AddConversion(CENTRAL_SCOT, OS_CLACKMANNANSHIRE);
            AddConversion(CENTRAL_SCOT, OS_EAST_DUNBARTONSHIRE);
            AddConversion(CENTRAL_SCOT, OS_FALKIRK);
            AddConversion(CENTRAL_SCOT, OS_FIFE);
            AddConversion(CENTRAL_SCOT, OS_NORTH_LANARKSHIRE);
            AddConversion(CENTRAL_SCOT, OS_PERTH_AND_KINROSS);
            AddConversion(CENTRAL_SCOT, OS_STIRLING);
            AddConversion(CENTRAL_SCOT, OS_WEST_LOTHIAN);
            AddConversion(GRAMPIAN, OS_ABERDEENSHIRE);
            AddConversion(GRAMPIAN, OS_ABERDEEN_CITY);
            AddConversion(GRAMPIAN, OS_ANGUS);
            AddConversion(GRAMPIAN, OS_HIGHLAND);
            AddConversion(GRAMPIAN, OS_MORAY);
            AddConversion(STRATHCLYDE, OS_ARGYLL_AND_BUTE);
            AddConversion(STRATHCLYDE, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(STRATHCLYDE, OS_EAST_AYRSHIRE);
            AddConversion(STRATHCLYDE, OS_EAST_DUNBARTONSHIRE);
            AddConversion(STRATHCLYDE, OS_EAST_RENFREWSHIRE);
            AddConversion(STRATHCLYDE, OS_GLASGOW_CITY);
            AddConversion(STRATHCLYDE, OS_INVERCLYDE);
            AddConversion(STRATHCLYDE, OS_NORTH_AYRSHIRE);
            AddConversion(STRATHCLYDE, OS_NORTH_LANARKSHIRE);
            AddConversion(STRATHCLYDE, OS_RENFREWSHIRE);
            AddConversion(STRATHCLYDE, OS_SCOTTISH_BORDERS);
            AddConversion(STRATHCLYDE, OS_SOUTH_AYRSHIRE);
            AddConversion(STRATHCLYDE, OS_SOUTH_LANARKSHIRE);
            AddConversion(STRATHCLYDE, OS_STIRLING);
            AddConversion(STRATHCLYDE, OS_WEST_DUNBARTONSHIRE);
            AddConversion(STRATHCLYDE, OS_WEST_LOTHIAN);
            AddConversion(TAYSIDE, OS_ABERDEENSHIRE);
            AddConversion(TAYSIDE, OS_ANGUS);
            AddConversion(TAYSIDE, OS_CLACKMANNANSHIRE);
            AddConversion(TAYSIDE, OS_DUNDEE_CITY);
            AddConversion(TAYSIDE, OS_FIFE);
            AddConversion(TAYSIDE, OS_PERTH_AND_KINROSS);
            AddConversion(TAYSIDE, OS_STIRLING);
            #endregion

            #region English 1974 LG Act Regions
            AddConversion(MERSEYSIDE, OS_BLACKBURN_WITH_DARWEN);
            AddConversion(MERSEYSIDE, OS_BLACKPOOL);
            AddConversion(MERSEYSIDE, OS_BOLTON);
            AddConversion(MERSEYSIDE, OS_BURY);
            AddConversion(MERSEYSIDE, OS_CALDERDALE);
            AddConversion(MERSEYSIDE, OS_CHESHIRE_EAST);
            AddConversion(MERSEYSIDE, OS_CHESHIRE_WEST_AND_CHESTER);
            AddConversion(MERSEYSIDE, OS_CUMBRIA);
            AddConversion(MERSEYSIDE, OS_DERBYSHIRE);
            AddConversion(MERSEYSIDE, OS_FLINTSHIRE);
            AddConversion(MERSEYSIDE, OS_HALTON);
            AddConversion(MERSEYSIDE, OS_KNOWSLEY);
            AddConversion(MERSEYSIDE, OS_LANCASHIRE);
            AddConversion(MERSEYSIDE, OS_LIVERPOOL);
            AddConversion(MERSEYSIDE, OS_MANCHESTER);
            AddConversion(MERSEYSIDE, OS_NORTH_YORKSHIRE);
            AddConversion(MERSEYSIDE, OS_OLDHAM);
            AddConversion(MERSEYSIDE, OS_ROCHDALE);
            AddConversion(MERSEYSIDE, OS_SALFORD);
            AddConversion(MERSEYSIDE, OS_SEFTON);
            AddConversion(MERSEYSIDE, OS_SHROPSHIRE);
            AddConversion(MERSEYSIDE, OS_STAFFORDSHIRE);
            AddConversion(MERSEYSIDE, OS_STOCKPORT);
            AddConversion(MERSEYSIDE, OS_ST_HELENS);
            AddConversion(MERSEYSIDE, OS_TAMESIDE);
            AddConversion(MERSEYSIDE, OS_TRAFFORD);
            AddConversion(MERSEYSIDE, OS_WARRINGTON);
            AddConversion(MERSEYSIDE, OS_WIGAN);
            AddConversion(MERSEYSIDE, OS_WIRRAL);
            AddConversion(MERSEYSIDE, OS_WREXHAM);
            AddConversion(TYNE_WEAR, OS_CUMBRIA);
            AddConversion(TYNE_WEAR, OS_DARLINGTON);
            AddConversion(TYNE_WEAR, OS_DURHAM);
            AddConversion(TYNE_WEAR, OS_GATESHEAD);
            AddConversion(TYNE_WEAR, OS_HARTLEPOOL);
            AddConversion(TYNE_WEAR, OS_MIDDLESBROUGH);
            AddConversion(TYNE_WEAR, OS_NEWCASTLE_UPON_TYNE);
            AddConversion(TYNE_WEAR, OS_NORTHUMBERLAND);
            AddConversion(TYNE_WEAR, OS_NORTH_TYNESIDE);
            AddConversion(TYNE_WEAR, OS_NORTH_YORKSHIRE);
            AddConversion(TYNE_WEAR, OS_SCOTTISH_BORDERS);
            AddConversion(TYNE_WEAR, OS_SOUTH_TYNESIDE);
            AddConversion(TYNE_WEAR, OS_STOCKTON_ON_TEES);
            AddConversion(TYNE_WEAR, OS_SUNDERLAND);
            AddConversion(WEST_MIDLANDS, OS_BIRMINGHAM);
            AddConversion(WEST_MIDLANDS, OS_BUCKINGHAMSHIRE);
            AddConversion(WEST_MIDLANDS, OS_CHESHIRE_EAST);
            AddConversion(WEST_MIDLANDS, OS_CITY_OF_STOKE_ON_TRENT);
            AddConversion(WEST_MIDLANDS, OS_CITY_OF_WOLVERHAMPTON);
            AddConversion(WEST_MIDLANDS, OS_COVENTRY);
            AddConversion(WEST_MIDLANDS, OS_DERBYSHIRE);
            AddConversion(WEST_MIDLANDS, OS_DUDLEY);
            AddConversion(WEST_MIDLANDS, OS_GLOUCESTERSHIRE);
            AddConversion(WEST_MIDLANDS, OS_HEREFORDSHIRE);
            AddConversion(WEST_MIDLANDS, OS_LEICESTERSHIRE);
            AddConversion(WEST_MIDLANDS, OS_NORTHAMPTONSHIRE);
            AddConversion(WEST_MIDLANDS, OS_OXFORDSHIRE);
            AddConversion(WEST_MIDLANDS, OS_SANDWELL);
            AddConversion(WEST_MIDLANDS, OS_SHROPSHIRE);
            AddConversion(WEST_MIDLANDS, OS_SOLIHULL);
            AddConversion(WEST_MIDLANDS, OS_SOUTH_TYNESIDE);
            AddConversion(WEST_MIDLANDS, OS_STAFFORDSHIRE);
            AddConversion(WEST_MIDLANDS, OS_WALSALL);
            AddConversion(WEST_MIDLANDS, OS_WARWICKSHIRE);
            AddConversion(WEST_MIDLANDS, OS_WORCESTERSHIRE);
            AddConversion(AVON, OS_BATH_AND_NORTH_EAST_SOMERSET);
            AddConversion(AVON, OS_BRISTOL);
            AddConversion(AVON, OS_DEVON);
            AddConversion(AVON, OS_DORSET);
            AddConversion(AVON, OS_GLOUCESTERSHIRE);
            AddConversion(AVON, OS_HEREFORDSHIRE);
            AddConversion(AVON, OS_MONMOUTHSHIRE);
            AddConversion(AVON, OS_NORTH_SOMERSET);
            AddConversion(AVON, OS_OXFORDSHIRE);
            AddConversion(AVON, OS_SOMERSET);
            AddConversion(AVON, OS_SOUTH_GLOUCESTERSHIRE);
            AddConversion(AVON, OS_SWINDON);
            AddConversion(AVON, OS_WARWICKSHIRE);
            AddConversion(AVON, OS_WILTSHIRE);
            AddConversion(AVON, OS_WORCESTERSHIRE);
            AddConversion(CLEVELAND, OS_CUMBRIA);
            AddConversion(CLEVELAND, OS_DARLINGTON);
            AddConversion(CLEVELAND, OS_DURHAM);
            AddConversion(CLEVELAND, OS_GATESHEAD);
            AddConversion(CLEVELAND, OS_HARTLEPOOL);
            AddConversion(CLEVELAND, OS_MIDDLESBROUGH);
            AddConversion(CLEVELAND, OS_NEWCASTLE_UPON_TYNE);
            AddConversion(CLEVELAND, OS_NORTHUMBERLAND);
            AddConversion(CLEVELAND, OS_NORTH_TYNESIDE);
            AddConversion(CLEVELAND, OS_NORTH_YORKSHIRE);
            AddConversion(CLEVELAND, OS_REDCAR_AND_CLEVELAND);
            AddConversion(CLEVELAND, OS_SOUTH_TYNESIDE);
            AddConversion(CLEVELAND, OS_STOCKTON_ON_TEES);
            AddConversion(CLEVELAND, OS_SUNDERLAND);
            AddConversion(EAST_YORKSHIRE, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(EAST_YORKSHIRE, OS_CITY_OF_KINGSTON_UPON_HULL);
            AddConversion(HUMBERSIDE, OS_BARNSLEY);
            AddConversion(HUMBERSIDE, OS_BRADFORD);
            AddConversion(HUMBERSIDE, OS_CALDERDALE);
            AddConversion(HUMBERSIDE, OS_DERBYSHIRE);
            AddConversion(HUMBERSIDE, OS_DONCASTER);
            AddConversion(HUMBERSIDE, OS_DURHAM);
            AddConversion(HUMBERSIDE, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(HUMBERSIDE, OS_KIRKLEES);
            AddConversion(HUMBERSIDE, OS_LANCASHIRE);
            AddConversion(HUMBERSIDE, OS_LEEDS);
            AddConversion(HUMBERSIDE, OS_LEICESTERSHIRE);
            AddConversion(HUMBERSIDE, OS_LINCOLNSHIRE);
            AddConversion(HUMBERSIDE, OS_MANCHESTER);
            AddConversion(HUMBERSIDE, OS_NORTHAMPTONSHIRE);
            AddConversion(HUMBERSIDE, OS_NORTH_EAST_LINCOLNSHIRE);
            AddConversion(HUMBERSIDE, OS_NORTH_LINCOLNSHIRE);
            AddConversion(HUMBERSIDE, OS_NORTH_YORKSHIRE);
            AddConversion(HUMBERSIDE, OS_NOTTINGHAMSHIRE);
            AddConversion(HUMBERSIDE, OS_OLDHAM);
            AddConversion(HUMBERSIDE, OS_ROCHDALE);
            AddConversion(HUMBERSIDE, OS_ROTHERHAM);
            AddConversion(HUMBERSIDE, OS_RUTLAND);
            AddConversion(HUMBERSIDE, OS_SHEFFIELD);
            AddConversion(HUMBERSIDE, OS_TAMESIDE);
            AddConversion(HUMBERSIDE, OS_WAKEFIELD);
            AddConversion(HEREFORD_WORCESTER, OS_BIRMINGHAM);
            AddConversion(HEREFORD_WORCESTER, OS_DUDLEY);
            AddConversion(HEREFORD_WORCESTER, OS_GLOUCESTERSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_HEREFORDSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_MONMOUTHSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_POWYS);
            AddConversion(HEREFORD_WORCESTER, OS_SANDWELL);
            AddConversion(HEREFORD_WORCESTER, OS_SHROPSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_SOLIHULL);
            AddConversion(HEREFORD_WORCESTER, OS_STAFFORDSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_WARWICKSHIRE);
            AddConversion(HEREFORD_WORCESTER, OS_WORCESTERSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_BARNSLEY);
            AddConversion(SOUTH_YORKSHIRE, OS_BRADFORD);
            AddConversion(SOUTH_YORKSHIRE, OS_CALDERDALE);
            AddConversion(SOUTH_YORKSHIRE, OS_CHESHIRE_EAST);
            AddConversion(SOUTH_YORKSHIRE, OS_CUMBRIA);
            AddConversion(SOUTH_YORKSHIRE, OS_DERBYSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_DONCASTER);
            AddConversion(SOUTH_YORKSHIRE, OS_DURHAM);
            AddConversion(SOUTH_YORKSHIRE, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_KIRKLEES);
            AddConversion(SOUTH_YORKSHIRE, OS_LANCASHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_LEEDS);
            AddConversion(SOUTH_YORKSHIRE, OS_MANCHESTER);
            AddConversion(SOUTH_YORKSHIRE, OS_NORTH_LINCOLNSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_NORTH_YORKSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_NOTTINGHAMSHIRE);
            AddConversion(SOUTH_YORKSHIRE, OS_OLDHAM);
            AddConversion(SOUTH_YORKSHIRE, OS_ROCHDALE);
            AddConversion(SOUTH_YORKSHIRE, OS_ROTHERHAM);
            AddConversion(SOUTH_YORKSHIRE, OS_SHEFFIELD);
            AddConversion(SOUTH_YORKSHIRE, OS_TAMESIDE);
            AddConversion(SOUTH_YORKSHIRE, OS_WAKEFIELD);
            AddConversion(SOUTH_YORKSHIRE, OS_YORK);
            AddConversion(WEST_YORKSHIRE, OS_BARNSLEY);
            AddConversion(WEST_YORKSHIRE, OS_BRADFORD);
            AddConversion(WEST_YORKSHIRE, OS_CALDERDALE);
            AddConversion(WEST_YORKSHIRE, OS_CHESHIRE_EAST);
            AddConversion(WEST_YORKSHIRE, OS_CUMBRIA);
            AddConversion(WEST_YORKSHIRE, OS_DERBYSHIRE);
            AddConversion(WEST_YORKSHIRE, OS_DONCASTER);
            AddConversion(WEST_YORKSHIRE, OS_DURHAM);
            AddConversion(WEST_YORKSHIRE, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(WEST_YORKSHIRE, OS_KIRKLEES);
            AddConversion(WEST_YORKSHIRE, OS_LANCASHIRE);
            AddConversion(WEST_YORKSHIRE, OS_LEEDS);
            AddConversion(WEST_YORKSHIRE, OS_MANCHESTER);
            AddConversion(WEST_YORKSHIRE, OS_NORTH_LINCOLNSHIRE);
            AddConversion(WEST_YORKSHIRE, OS_NORTH_YORKSHIRE);
            AddConversion(WEST_YORKSHIRE, OS_NOTTINGHAMSHIRE);
            AddConversion(WEST_YORKSHIRE, OS_OLDHAM);
            AddConversion(WEST_YORKSHIRE, OS_ROCHDALE);
            AddConversion(WEST_YORKSHIRE, OS_ROTHERHAM);
            AddConversion(WEST_YORKSHIRE, OS_SHEFFIELD);
            AddConversion(WEST_YORKSHIRE, OS_TAMESIDE);
            AddConversion(WEST_YORKSHIRE, OS_WAKEFIELD);
            AddConversion(WEST_YORKSHIRE, OS_YORK);
            #endregion 

            #region Wales 1974 LG act regions
            AddConversion(CLWYD, OS_CHESHIRE_WEST_AND_CHESTER);
            AddConversion(CLWYD, OS_CONWY);
            AddConversion(CLWYD, OS_DENBIGHSHIRE);
            AddConversion(CLWYD, OS_FLINTSHIRE);
            AddConversion(CLWYD, OS_GWYNEDD);
            AddConversion(CLWYD, OS_POWYS);
            AddConversion(CLWYD, OS_SHROPSHIRE);
            AddConversion(CLWYD, OS_WREXHAM);
            AddConversion(DYFED, OS_CARMARTHENSHIRE);
            AddConversion(DYFED, OS_CEREDIGION);
            AddConversion(DYFED, OS_GWYNEDD);
            AddConversion(DYFED, OS_NEATH_PORT_TALBOT);
            AddConversion(DYFED, OS_PEMBROKESHIRE);
            AddConversion(DYFED, OS_POWYS);
            AddConversion(DYFED, OS_SWANSEA);
            AddConversion(GWENT, OS_BLAENAU_GWENT);
            AddConversion(GWENT, OS_CAERPHILLY);
            AddConversion(GWENT, OS_CARDIFF);
            AddConversion(GWENT, OS_GLOUCESTERSHIRE);
            AddConversion(GWENT, OS_HEREFORDSHIRE);
            AddConversion(GWENT, OS_MERTHYR_TYDFIL);
            AddConversion(GWENT, OS_MONMOUTHSHIRE);
            AddConversion(GWENT, OS_NEWPORT);
            AddConversion(GWENT, OS_POWYS);
            AddConversion(GWENT, OS_RHONDDA_CYNON_TAFF);
            AddConversion(GWENT, OS_TORFAEN);
            AddConversion(GWYNEDD, OS_GWYNEDD);
            AddConversion(GWYNEDD, OS_ISLE_OF_ANGLESEY);
            AddConversion(MID_GLAMORGAN, OS_BRIDGEND);
            AddConversion(MID_GLAMORGAN, OS_MERTHYR_TYDFIL);
            AddConversion(MID_GLAMORGAN, OS_RHONDDA_CYNON_TAFF);
            AddConversion(POWYS, OS_POWYS);
            AddConversion(SOUTH_GLAMORGAN, OS_CARDIFF);
            AddConversion(SOUTH_GLAMORGAN, OS_VALE_OF_GLAMORGAN);
            AddConversion(WEST_GLAMORGAN, OS_SWANSEA);
            AddConversion(WEST_GLAMORGAN, OS_NEATH_PORT_TALBOT);
            #endregion

            #region from parish maps
            AddConversion(ANGLESEY, OS_ISLE_OF_ANGLESEY);
            AddConversion(BEDS, OS_BEDFORDSHIRE);
            AddConversion(BEDS, OS_BUCKINGHAMSHIRE);
            AddConversion(BEDS, OS_CAMBRIDGESHIRE);
            AddConversion(BEDS, OS_CENTRAL_BEDFORDSHIRE);
            AddConversion(BEDS, OS_HERTFORDSHIRE);
            AddConversion(BEDS, OS_LUTON);
            AddConversion(BEDS, OS_MILTON_KEYNES);
            AddConversion(BERKS, OS_BRACKNELL_FOREST);
            AddConversion(BERKS, OS_BUCKINGHAMSHIRE);
            AddConversion(BERKS, OS_GLOUCESTERSHIRE);
            AddConversion(BERKS, OS_HAMPSHIRE);
            AddConversion(BERKS, OS_OXFORDSHIRE);
            AddConversion(BERKS, OS_READING);
            AddConversion(BERKS, OS_SURREY);
            AddConversion(BERKS, OS_SWINDON);
            AddConversion(BERKS, OS_WEST_BERKSHIRE);
            AddConversion(BERKS, OS_WILTSHIRE);
            AddConversion(BERKS, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(BERKS, OS_WOKINGHAM);
            AddConversion(BRECON, OS_BLAENAU_GWENT);
            AddConversion(BRECON, OS_CAERPHILLY);
            AddConversion(BRECON, OS_CARMARTHENSHIRE);
            AddConversion(BRECON, OS_CEREDIGION);
            AddConversion(BRECON, OS_HEREFORDSHIRE);
            AddConversion(BRECON, OS_MERTHYR_TYDFIL);
            AddConversion(BRECON, OS_MONMOUTHSHIRE);
            AddConversion(BRECON, OS_NEATH_PORT_TALBOT);
            AddConversion(BRECON, OS_POWYS);
            AddConversion(BRECON, OS_RHONDDA_CYNON_TAFF);
            AddConversion(BRECON, OS_TORFAEN);
            AddConversion(BUCKS, OS_BEDFORDSHIRE);
            AddConversion(BUCKS, OS_BUCKINGHAMSHIRE);
            AddConversion(BUCKS, OS_CENTRAL_BEDFORDSHIRE);
            AddConversion(BUCKS, OS_HERTFORDSHIRE);
            AddConversion(BUCKS, OS_MILTON_KEYNES);
            AddConversion(BUCKS, OS_NORTHAMPTONSHIRE);
            AddConversion(BUCKS, OS_OXFORDSHIRE);
            AddConversion(BUCKS, OS_SLOUGH);
            AddConversion(BUCKS, OS_SURREY);
            AddConversion(BUCKS, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(BUCKS, OS_WOKINGHAM);
            AddConversion(CAERNARFON, OS_CONWY);
            AddConversion(CAERNARFON, OS_GWYNEDD);
            AddConversion(CAERNARFON, OS_ISLE_OF_ANGLESEY);
            AddConversion(CAMBS, OS_CAMBRIDGESHIRE);
            AddConversion(CAMBS, OS_CENTRAL_BEDFORDSHIRE);
            AddConversion(CAMBS, OS_CITY_OF_PETERBOROUGH);
            AddConversion(CAMBS, OS_ESSEX);
            AddConversion(CAMBS, OS_HERTFORDSHIRE);
            AddConversion(CAMBS, OS_LINCOLNSHIRE);
            AddConversion(CAMBS, OS_NORFOLK);
            AddConversion(CAMBS, OS_SUFFOLK);
            AddConversion(CARMARTHEN, OS_CARMARTHENSHIRE);
            AddConversion(CARMARTHEN, OS_CEREDIGION);
            AddConversion(CARMARTHEN, OS_NEATH_PORT_TALBOT);
            AddConversion(CARMARTHEN, OS_PEMBROKESHIRE);
            AddConversion(CARMARTHEN, OS_POWYS);
            AddConversion(CARMARTHEN, OS_SWANSEA);
            AddConversion(CEREDIGION, OS_CARMARTHENSHIRE);
            AddConversion(CEREDIGION, OS_CEREDIGION);
            AddConversion(CEREDIGION, OS_GWYNEDD);
            AddConversion(CEREDIGION, OS_PEMBROKESHIRE);
            AddConversion(CEREDIGION, OS_POWYS);
            AddConversion(CHESHIRE, OS_CHESHIRE_EAST);
            AddConversion(CHESHIRE, OS_CHESHIRE_WEST_AND_CHESTER);
            AddConversion(CHESHIRE, OS_DERBYSHIRE);
            AddConversion(CHESHIRE, OS_FLINTSHIRE);
            AddConversion(CHESHIRE, OS_HALTON);
            AddConversion(CHESHIRE, OS_MANCHESTER);
            AddConversion(CHESHIRE, OS_OLDHAM);
            AddConversion(CHESHIRE, OS_SHROPSHIRE);
            AddConversion(CHESHIRE, OS_STAFFORDSHIRE);
            AddConversion(CHESHIRE, OS_STOCKPORT);
            AddConversion(CHESHIRE, OS_TAMESIDE);
            AddConversion(CHESHIRE, OS_TRAFFORD);
            AddConversion(CHESHIRE, OS_WARRINGTON);
            AddConversion(CHESHIRE, OS_WIRRAL);
            AddConversion(CHESHIRE, OS_WREXHAM);
            AddConversion(CORNWALL, OS_CORNWALL);
            AddConversion(CORNWALL, OS_DEVON);
            AddConversion(CUMBERLAND, OS_CUMBRIA);
            AddConversion(CUMBERLAND, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(CUMBERLAND, OS_NORTHUMBERLAND);
            AddConversion(CUMBERLAND, OS_SCOTTISH_BORDERS);
            AddConversion(DENBIGH, OS_CHESHIRE_WEST_AND_CHESTER);
            AddConversion(DENBIGH, OS_CONWY);
            AddConversion(DENBIGH, OS_DENBIGHSHIRE);
            AddConversion(DENBIGH, OS_FLINTSHIRE);
            AddConversion(DENBIGH, OS_GWYNEDD);
            AddConversion(DENBIGH, OS_POWYS);
            AddConversion(DENBIGH, OS_SHROPSHIRE);
            AddConversion(DENBIGH, OS_WREXHAM);
            AddConversion(DERBY, OS_CHESHIRE_EAST);
            AddConversion(DERBY, OS_CITY_OF_DERBY);
            AddConversion(DERBY, OS_DERBYSHIRE);
            AddConversion(DERBY, OS_LEICESTERSHIRE);
            AddConversion(DERBY, OS_NOTTINGHAMSHIRE);
            AddConversion(DERBY, OS_SHEFFIELD);
            AddConversion(DERBY, OS_STAFFORDSHIRE);
            AddConversion(DERBY, OS_STOCKPORT);
            AddConversion(DERBY, OS_TAMESIDE);
            AddConversion(DEVON, OS_CITY_OF_PLYMOUTH);
            AddConversion(DEVON, OS_CORNWALL);
            AddConversion(DEVON, OS_DEVON);
            AddConversion(DEVON, OS_HAMPSHIRE);
            AddConversion(DEVON, OS_SOMERSET);
            AddConversion(DEVON, OS_TORBAY);
            AddConversion(DORSET, OS_BOURNEMOUTH);
            AddConversion(DORSET, OS_DEVON);
            AddConversion(DORSET, OS_DORSET);
            AddConversion(DORSET, OS_POOLE);
            AddConversion(DORSET, OS_SOMERSET);
            AddConversion(DORSET, OS_WILTSHIRE);
            AddConversion(DURHAM, OS_CUMBRIA);
            AddConversion(DURHAM, OS_DARLINGTON);
            AddConversion(DURHAM, OS_DURHAM);
            AddConversion(DURHAM, OS_GATESHEAD);
            AddConversion(DURHAM, OS_HARTLEPOOL);
            AddConversion(DURHAM, OS_MIDDLESBROUGH);
            AddConversion(DURHAM, OS_NEWCASTLE_UPON_TYNE);
            AddConversion(DURHAM, OS_NORTHUMBERLAND);
            AddConversion(DURHAM, OS_NORTH_TYNESIDE);
            AddConversion(DURHAM, OS_NORTH_YORKSHIRE);
            AddConversion(DURHAM, OS_SOUTH_TYNESIDE);
            AddConversion(DURHAM, OS_STOCKTON_ON_TEES);
            AddConversion(DURHAM, OS_SUNDERLAND);
            AddConversion(ESSEX, OS_BARKING_AND_DAGENHAM);
            AddConversion(ESSEX, OS_CAMBRIDGESHIRE);
            AddConversion(ESSEX, OS_ENFIELD);
            AddConversion(ESSEX, OS_ESSEX);
            AddConversion(ESSEX, OS_GREENWICH);
            AddConversion(ESSEX, OS_HAVERING);
            AddConversion(ESSEX, OS_HERTFORDSHIRE);
            AddConversion(ESSEX, OS_KENT);
            AddConversion(ESSEX, OS_MEDWAY);
            AddConversion(ESSEX, OS_NEWHAM);
            AddConversion(ESSEX, OS_REDBRIDGE);
            AddConversion(ESSEX, OS_SOUTHEND_ON_SEA);
            AddConversion(ESSEX, OS_SUFFOLK);
            AddConversion(ESSEX, OS_THURROCK);
            AddConversion(ESSEX, OS_WALTHAM_FOREST);
            AddConversion(FLINT, OS_CHESHIRE_WEST_AND_CHESTER);
            AddConversion(FLINT, OS_CONWY);
            AddConversion(FLINT, OS_DENBIGHSHIRE);
            AddConversion(FLINT, OS_FLINTSHIRE);
            AddConversion(FLINT, OS_SHROPSHIRE);
            AddConversion(FLINT, OS_WREXHAM);
            AddConversion(GLAMORGAN, OS_BRIDGEND);
            AddConversion(GLAMORGAN, OS_CAERPHILLY);
            AddConversion(GLAMORGAN, OS_CARDIFF);
            AddConversion(GLAMORGAN, OS_CARMARTHENSHIRE);
            AddConversion(GLAMORGAN, OS_MERTHYR_TYDFIL);
            AddConversion(GLAMORGAN, OS_NEATH_PORT_TALBOT);
            AddConversion(GLAMORGAN, OS_NEWPORT);
            AddConversion(GLAMORGAN, OS_POWYS);
            AddConversion(GLAMORGAN, OS_RHONDDA_CYNON_TAFF);
            AddConversion(GLAMORGAN, OS_SWANSEA);
            AddConversion(GLAMORGAN, OS_VALE_OF_GLAMORGAN);
            AddConversion(GLOUCS, OS_BATH_AND_NORTH_EAST_SOMERSET);
            AddConversion(GLOUCS, OS_BRISTOL);
            AddConversion(GLOUCS, OS_DORSET);
            AddConversion(GLOUCS, OS_GLOUCESTERSHIRE);
            AddConversion(GLOUCS, OS_HEREFORDSHIRE);
            AddConversion(GLOUCS, OS_MONMOUTHSHIRE);
            AddConversion(GLOUCS, OS_NORTH_SOMERSET);
            AddConversion(GLOUCS, OS_OXFORDSHIRE);
            AddConversion(GLOUCS, OS_SOUTH_GLOUCESTERSHIRE);
            AddConversion(GLOUCS, OS_SWINDON);
            AddConversion(GLOUCS, OS_WARWICKSHIRE);
            AddConversion(GLOUCS, OS_WILTSHIRE);
            AddConversion(GLOUCS, OS_WORCESTERSHIRE);
            AddConversion(HANTS, OS_BOURNEMOUTH);
            AddConversion(HANTS, OS_CITY_OF_PORTSMOUTH);
            AddConversion(HANTS, OS_CITY_OF_SOUTHAMPTON);
            AddConversion(HANTS, OS_DORSET);
            AddConversion(HANTS, OS_HALTON);
            AddConversion(HANTS, OS_HAMPSHIRE);
            AddConversion(HANTS, OS_ISLE_OF_WIGHT);
            AddConversion(HANTS, OS_POOLE);
            AddConversion(HANTS, OS_SURREY);
            AddConversion(HANTS, OS_WEST_BERKSHIRE);
            AddConversion(HANTS, OS_WEST_SUSSEX);
            AddConversion(HANTS, OS_WILTSHIRE);
            AddConversion(HANTS, OS_WOKINGHAM);
            AddConversion(HEREFORD, OS_GLOUCESTERSHIRE);
            AddConversion(HEREFORD, OS_HEREFORDSHIRE);
            AddConversion(HEREFORD, OS_MONMOUTHSHIRE);
            AddConversion(HEREFORD, OS_POWYS);
            AddConversion(HEREFORD, OS_SHROPSHIRE);
            AddConversion(HEREFORD, OS_WORCESTERSHIRE);
            AddConversion(HERTS, OS_BARNET);
            AddConversion(HERTS, OS_BUCKINGHAMSHIRE);
            AddConversion(HERTS, OS_CENTRAL_BEDFORDSHIRE);
            AddConversion(HERTS, OS_ENFIELD);
            AddConversion(HERTS, OS_ESSEX);
            AddConversion(HERTS, OS_HARROW);
            AddConversion(HERTS, OS_HERTFORDSHIRE);
            AddConversion(HERTS, OS_HILLINGDON);
            AddConversion(HUNTS, OS_BEDFORDSHIRE);
            AddConversion(HUNTS, OS_CAMBRIDGESHIRE);
            AddConversion(HUNTS, OS_CITY_OF_PETERBOROUGH);
            AddConversion(HUNTS, OS_NORTHAMPTONSHIRE);
            AddConversion(KENT, OS_BEXLEY);
            AddConversion(KENT, OS_BROMLEY);
            AddConversion(KENT, OS_CROYDON);
            AddConversion(KENT, OS_EAST_SUSSEX);
            AddConversion(KENT, OS_GREENWICH);
            AddConversion(KENT, OS_HAVERING);
            AddConversion(KENT, OS_KENT);
            AddConversion(KENT, OS_LEWISHAM);
            AddConversion(KENT, OS_MEDWAY);
            AddConversion(KENT, OS_NEWHAM);
            AddConversion(KENT, OS_SURREY);
            AddConversion(KENT, OS_THURROCK);
            AddConversion(KENT, OS_TOWER_HAMLETS);
            AddConversion(LANCS, OS_BLACKBURN_WITH_DARWEN);
            AddConversion(LANCS, OS_BLACKPOOL);
            AddConversion(LANCS, OS_BOLTON);
            AddConversion(LANCS, OS_BURY);
            AddConversion(LANCS, OS_CALDERDALE);
            AddConversion(LANCS, OS_CHESHIRE_EAST);
            AddConversion(LANCS, OS_CUMBRIA);
            AddConversion(LANCS, OS_HALTON);
            AddConversion(LANCS, OS_KNOWSLEY);
            AddConversion(LANCS, OS_LANCASHIRE);
            AddConversion(LANCS, OS_LIVERPOOL);
            AddConversion(LANCS, OS_MANCHESTER);
            AddConversion(LANCS, OS_NORTH_YORKSHIRE);
            AddConversion(LANCS, OS_OLDHAM);
            AddConversion(LANCS, OS_ROCHDALE);
            AddConversion(LANCS, OS_SALFORD);
            AddConversion(LANCS, OS_SEFTON);
            AddConversion(LANCS, OS_STOCKPORT);
            AddConversion(LANCS, OS_ST_HELENS);
            AddConversion(LANCS, OS_TAMESIDE);
            AddConversion(LANCS, OS_TRAFFORD);
            AddConversion(LANCS, OS_WARRINGTON);
            AddConversion(LANCS, OS_WIGAN);
            AddConversion(LEICS, OS_CITY_OF_LEICESTER);
            AddConversion(LEICS, OS_DERBYSHIRE);
            AddConversion(LEICS, OS_LEICESTERSHIRE);
            AddConversion(LEICS, OS_LINCOLNSHIRE);
            AddConversion(LEICS, OS_NORTHAMPTONSHIRE);
            AddConversion(LEICS, OS_NOTTINGHAMSHIRE);
            AddConversion(LEICS, OS_RUTLAND);
            AddConversion(LEICS, OS_STAFFORDSHIRE);
            AddConversion(LEICS, OS_WARWICKSHIRE);
            AddConversion(LINCS, OS_CAMBRIDGESHIRE);
            AddConversion(LINCS, OS_CITY_OF_PETERBOROUGH);
            AddConversion(LINCS, OS_DONCASTER);
            AddConversion(LINCS, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(LINCS, OS_LEICESTERSHIRE);
            AddConversion(LINCS, OS_LINCOLNSHIRE);
            AddConversion(LINCS, OS_NORFOLK);
            AddConversion(LINCS, OS_NORTHAMPTONSHIRE);
            AddConversion(LINCS, OS_NORTH_EAST_LINCOLNSHIRE);
            AddConversion(LINCS, OS_NORTH_LINCOLNSHIRE);
            AddConversion(LINCS, OS_NOTTINGHAMSHIRE);
            AddConversion(LINCS, OS_RUTLAND);
            AddConversion(LONDON, OS_BARKING_AND_DAGENHAM);
            AddConversion(LONDON, OS_BARNET);
            AddConversion(LONDON, OS_BEXLEY);
            AddConversion(LONDON, OS_BRACKNELL_FOREST);
            AddConversion(LONDON, OS_BRENT);
            AddConversion(LONDON, OS_BROMLEY);
            AddConversion(LONDON, OS_CAMDEN);
            AddConversion(LONDON, OS_CITY_OF_LONDON);
            AddConversion(LONDON, OS_CITY_OF_WESTMINSTER);
            AddConversion(LONDON, OS_CROYDON);
            AddConversion(LONDON, OS_EALING);
            AddConversion(LONDON, OS_EAST_SUSSEX);
            AddConversion(LONDON, OS_ENFIELD);
            AddConversion(LONDON, OS_ESSEX);
            AddConversion(LONDON, OS_GREENWICH);
            AddConversion(LONDON, OS_HACKNEY);
            AddConversion(LONDON, OS_HAMMERSMITH_AND_FULHAM);
            AddConversion(LONDON, OS_HARINGEY);
            AddConversion(LONDON, OS_HARROW);
            AddConversion(LONDON, OS_HAVERING);
            AddConversion(LONDON, OS_HILLINGDON);
            AddConversion(LONDON, OS_HOUNSLOW);
            AddConversion(LONDON, OS_ISLINGTON);
            AddConversion(LONDON, OS_KENT);
            AddConversion(LONDON, OS_KINGSTON_UPON_THAMES);
            AddConversion(LONDON, OS_LAMBETH);
            AddConversion(LONDON, OS_LEWISHAM);
            AddConversion(LONDON, OS_MERTON);
            AddConversion(LONDON, OS_NEWHAM);
            AddConversion(LONDON, OS_REDBRIDGE);
            AddConversion(LONDON, OS_RICHMOND_UPON_THAMES);
            AddConversion(LONDON, OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA);
            AddConversion(LONDON, OS_SOUTHWARK);
            AddConversion(LONDON, OS_SURREY);
            AddConversion(LONDON, OS_SUTTON);
            AddConversion(LONDON, OS_TOWER_HAMLETS);
            AddConversion(LONDON, OS_WALTHAM_FOREST);
            AddConversion(LONDON, OS_WANDSWORTH);
            AddConversion(LONDON, OS_WEST_SUSSEX);
            AddConversion(LONDON, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(MANCHESTER, OS_BURY);
            AddConversion(MANCHESTER, OS_BOLTON);
            AddConversion(MANCHESTER, OS_MANCHESTER);
            AddConversion(MANCHESTER, OS_OLDHAM);
            AddConversion(MANCHESTER, OS_ROCHDALE);
            AddConversion(MANCHESTER, OS_SALFORD);
            AddConversion(MANCHESTER, OS_STOCKPORT);
            AddConversion(MANCHESTER, OS_TAMESIDE);
            AddConversion(MANCHESTER, OS_TRAFFORD);
            AddConversion(MANCHESTER, OS_WIGAN);
            AddConversion(MERIONETH, OS_CONWY);
            AddConversion(MERIONETH, OS_DENBIGHSHIRE);
            AddConversion(MERIONETH, OS_GWYNEDD);
            AddConversion(MERIONETH, OS_POWYS);
            AddConversion(MIDDLESEX, OS_BARNET);
            AddConversion(MIDDLESEX, OS_BRENT);
            AddConversion(MIDDLESEX, OS_BUCKINGHAMSHIRE);
            AddConversion(MIDDLESEX, OS_CAMDEN);
            AddConversion(MIDDLESEX, OS_CITY_OF_LONDON);
            AddConversion(MIDDLESEX, OS_CITY_OF_WESTMINSTER);
            AddConversion(MIDDLESEX, OS_EALING);
            AddConversion(MIDDLESEX, OS_ENFIELD);
            AddConversion(MIDDLESEX, OS_ESSEX);
            AddConversion(MIDDLESEX, OS_HACKNEY);
            AddConversion(MIDDLESEX, OS_HAMMERSMITH_AND_FULHAM);
            AddConversion(MIDDLESEX, OS_HARINGEY);
            AddConversion(MIDDLESEX, OS_HARROW);
            AddConversion(MIDDLESEX, OS_HERTFORDSHIRE);
            AddConversion(MIDDLESEX, OS_HILLINGDON);
            AddConversion(MIDDLESEX, OS_HOUNSLOW);
            AddConversion(MIDDLESEX, OS_ISLINGTON);
            AddConversion(MIDDLESEX, OS_LAMBETH);
            AddConversion(MIDDLESEX, OS_NEWHAM);
            AddConversion(MIDDLESEX, OS_RICHMOND_UPON_THAMES);
            AddConversion(MIDDLESEX, OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA);
            AddConversion(MIDDLESEX, OS_SOUTHWARK);
            AddConversion(MIDDLESEX, OS_SURREY);
            AddConversion(MIDDLESEX, OS_TOWER_HAMLETS);
            AddConversion(MIDDLESEX, OS_WANDSWORTH);
            AddConversion(MIDDLESEX, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(MONMOUTH, OS_BLAENAU_GWENT);
            AddConversion(MONMOUTH, OS_CAERPHILLY);
            AddConversion(MONMOUTH, OS_CARDIFF);
            AddConversion(MONMOUTH, OS_GLOUCESTERSHIRE);
            AddConversion(MONMOUTH, OS_HEREFORDSHIRE);
            AddConversion(MONMOUTH, OS_MERTHYR_TYDFIL);
            AddConversion(MONMOUTH, OS_MONMOUTHSHIRE);
            AddConversion(MONMOUTH, OS_NEWPORT);
            AddConversion(MONMOUTH, OS_POWYS);
            AddConversion(MONMOUTH, OS_RHONDDA_CYNON_TAFF);
            AddConversion(MONMOUTH, OS_TORFAEN);
            AddConversion(MONTGOMERY, OS_CEREDIGION);
            AddConversion(MONTGOMERY, OS_GWYNEDD);
            AddConversion(MONTGOMERY, OS_POWYS);
            AddConversion(MONTGOMERY, OS_SHROPSHIRE);
            AddConversion(NORFOLK, OS_CAMBRIDGESHIRE);
            AddConversion(NORFOLK, OS_LINCOLNSHIRE);
            AddConversion(NORFOLK, OS_NORFOLK);
            AddConversion(NORFOLK, OS_SUFFOLK);
            AddConversion(NORTHAMPTON, OS_BEDFORDSHIRE);
            AddConversion(NORTHAMPTON, OS_BUCKINGHAMSHIRE);
            AddConversion(NORTHAMPTON, OS_CAMBRIDGESHIRE);
            AddConversion(NORTHAMPTON, OS_CITY_OF_PETERBOROUGH);
            AddConversion(NORTHAMPTON, OS_LEICESTERSHIRE);
            AddConversion(NORTHAMPTON, OS_LINCOLNSHIRE);
            AddConversion(NORTHAMPTON, OS_MILTON_KEYNES);
            AddConversion(NORTHAMPTON, OS_NORTHAMPTONSHIRE);
            AddConversion(NORTHAMPTON, OS_OXFORDSHIRE);
            AddConversion(NORTHAMPTON, OS_WARWICKSHIRE);
            AddConversion(NORTHUMBERLAND, OS_CUMBRIA);
            AddConversion(NORTHUMBERLAND, OS_DURHAM);
            AddConversion(NORTHUMBERLAND, OS_GATESHEAD);
            AddConversion(NORTHUMBERLAND, OS_NEWCASTLE_UPON_TYNE);
            AddConversion(NORTHUMBERLAND, OS_NORTHUMBERLAND);
            AddConversion(NORTHUMBERLAND, OS_NORTH_TYNESIDE);
            AddConversion(NORTHUMBERLAND, OS_SCOTTISH_BORDERS);
            AddConversion(NORTH_YORKSHIRE, OS_DARLINGTON);
            AddConversion(NORTH_YORKSHIRE, OS_MIDDLESBROUGH);
            AddConversion(NORTH_YORKSHIRE, OS_REDCAR_AND_CLEVELAND);
            AddConversion(NORTH_YORKSHIRE, OS_STOCKTON_ON_TEES);
            AddConversion(NOTTS, OS_CITY_OF_NOTTINGHAM);
            AddConversion(NOTTS, OS_DERBYSHIRE);
            AddConversion(NOTTS, OS_DONCASTER);
            AddConversion(NOTTS, OS_LEICESTERSHIRE);
            AddConversion(NOTTS, OS_LINCOLNSHIRE);
            AddConversion(NOTTS, OS_NORTH_LINCOLNSHIRE);
            AddConversion(NOTTS, OS_NOTTINGHAMSHIRE);
            AddConversion(OXFORD, OS_BUCKINGHAMSHIRE);
            AddConversion(OXFORD, OS_GLOUCESTERSHIRE);
            AddConversion(OXFORD, OS_NORTHAMPTONSHIRE);
            AddConversion(OXFORD, OS_OXFORDSHIRE);
            AddConversion(OXFORD, OS_READING);
            AddConversion(OXFORD, OS_WARWICKSHIRE);
            AddConversion(OXFORD, OS_WEST_BERKSHIRE);
            AddConversion(OXFORD, OS_WOKINGHAM);
            AddConversion(PEMBROKE, OS_CARMARTHENSHIRE);
            AddConversion(PEMBROKE, OS_CEREDIGION);
            AddConversion(PEMBROKE, OS_PEMBROKESHIRE);
            AddConversion(RADNOR, OS_CEREDIGION);
            AddConversion(RADNOR, OS_HEREFORDSHIRE);
            AddConversion(RADNOR, OS_POWYS);
            AddConversion(RADNOR, OS_SHROPSHIRE);
            AddConversion(RUTLAND, OS_LEICESTERSHIRE);
            AddConversion(RUTLAND, OS_LINCOLNSHIRE);
            AddConversion(RUTLAND, OS_NORTHAMPTONSHIRE);
            AddConversion(RUTLAND, OS_RUTLAND);
            AddConversion(SHROPS, OS_CHESHIRE_EAST);
            AddConversion(SHROPS, OS_HEREFORDSHIRE);
            AddConversion(SHROPS, OS_POWYS);
            AddConversion(SHROPS, OS_SHROPSHIRE);
            AddConversion(SHROPS, OS_STAFFORDSHIRE);
            AddConversion(SHROPS, OS_TELFORD_AND_WREKIN);
            AddConversion(SHROPS, OS_WORCESTERSHIRE);
            AddConversion(SHROPS, OS_WREXHAM);
            AddConversion(SOMERSET, OS_BATH_AND_NORTH_EAST_SOMERSET);
            AddConversion(SOMERSET, OS_BRISTOL);
            AddConversion(SOMERSET, OS_DEVON);
            AddConversion(SOMERSET, OS_DORSET);
            AddConversion(SOMERSET, OS_NORTH_SOMERSET);
            AddConversion(SOMERSET, OS_SOMERSET);
            AddConversion(SOMERSET, OS_SOUTH_GLOUCESTERSHIRE);
            AddConversion(SOMERSET, OS_WILTSHIRE);
            AddConversion(STAFFS, OS_BIRMINGHAM);
            AddConversion(STAFFS, OS_CHESHIRE_EAST);
            AddConversion(STAFFS, OS_CITY_OF_STOKE_ON_TRENT);
            AddConversion(STAFFS, OS_CITY_OF_WOLVERHAMPTON);
            AddConversion(STAFFS, OS_DERBYSHIRE);
            AddConversion(STAFFS, OS_DUDLEY);
            AddConversion(STAFFS, OS_SANDWELL);
            AddConversion(STAFFS, OS_SHROPSHIRE);
            AddConversion(STAFFS, OS_SOUTH_TYNESIDE);
            AddConversion(STAFFS, OS_STAFFORDSHIRE);
            AddConversion(STAFFS, OS_WALSALL);
            AddConversion(STAFFS, OS_WARWICKSHIRE);
            AddConversion(STAFFS, OS_WORCESTERSHIRE);
            AddConversion(SUFFOLK, OS_CAMBRIDGESHIRE);
            AddConversion(SUFFOLK, OS_ESSEX);
            AddConversion(SUFFOLK, OS_NORFOLK);
            AddConversion(SUFFOLK, OS_SUFFOLK);
            AddConversion(SURREY, OS_BRACKNELL_FOREST);
            AddConversion(SURREY, OS_BROMLEY);
            AddConversion(SURREY, OS_CITY_OF_LONDON);
            AddConversion(SURREY, OS_CITY_OF_WESTMINSTER);
            AddConversion(SURREY, OS_CROYDON);
            AddConversion(SURREY, OS_EAST_SUSSEX);
            AddConversion(SURREY, OS_HAMPSHIRE);
            AddConversion(SURREY, OS_HOUNSLOW);
            AddConversion(SURREY, OS_KENT);
            AddConversion(SURREY, OS_KINGSTON_UPON_THAMES);
            AddConversion(SURREY, OS_LAMBETH);
            AddConversion(SURREY, OS_LEWISHAM);
            AddConversion(SURREY, OS_MERTON);
            AddConversion(SURREY, OS_RICHMOND_UPON_THAMES);
            AddConversion(SURREY, OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA);
            AddConversion(SURREY, OS_SOUTHWARK);
            AddConversion(SURREY, OS_SURREY);
            AddConversion(SURREY, OS_SUTTON);
            AddConversion(SURREY, OS_TOWER_HAMLETS);
            AddConversion(SURREY, OS_WANDSWORTH);
            AddConversion(SURREY, OS_WEST_SUSSEX);
            AddConversion(SURREY, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(SUSSEX, OS_CITY_OF_BRIGHTON_AND_HOVE);
            AddConversion(SUSSEX, OS_EAST_SUSSEX);
            AddConversion(SUSSEX, OS_HAMPSHIRE);
            AddConversion(SUSSEX, OS_KENT);
            AddConversion(SUSSEX, OS_SURREY);
            AddConversion(SUSSEX, OS_WEST_SUSSEX);
            AddConversion(WARWICK, OS_BIRMINGHAM);
            AddConversion(WARWICK, OS_BUCKINGHAMSHIRE);
            AddConversion(WARWICK, OS_COVENTRY);
            AddConversion(WARWICK, OS_GLOUCESTERSHIRE);
            AddConversion(WARWICK, OS_LEICESTERSHIRE);
            AddConversion(WARWICK, OS_NORTHAMPTONSHIRE);
            AddConversion(WARWICK, OS_OXFORDSHIRE);
            AddConversion(WARWICK, OS_SOLIHULL);
            AddConversion(WARWICK, OS_STAFFORDSHIRE);
            AddConversion(WARWICK, OS_WARWICKSHIRE);
            AddConversion(WARWICK, OS_WORCESTERSHIRE);
            AddConversion(WESTMORLAND, OS_CUMBRIA);
            AddConversion(WESTMORLAND, OS_DURHAM);
            AddConversion(WESTMORLAND, OS_LANCASHIRE);
            AddConversion(WILTS, OS_BATH_AND_NORTH_EAST_SOMERSET);
            AddConversion(WILTS, OS_DORSET);
            AddConversion(WILTS, OS_GLOUCESTERSHIRE);
            AddConversion(WILTS, OS_HAMPSHIRE);
            AddConversion(WILTS, OS_OXFORDSHIRE);
            AddConversion(WILTS, OS_SOMERSET);
            AddConversion(WILTS, OS_SOUTH_GLOUCESTERSHIRE);
            AddConversion(WILTS, OS_SWINDON);
            AddConversion(WILTS, OS_WEST_BERKSHIRE);
            AddConversion(WILTS, OS_WILTSHIRE);
            AddConversion(WORCESTER, OS_BIRMINGHAM);
            AddConversion(WORCESTER, OS_DUDLEY);
            AddConversion(WORCESTER, OS_GLOUCESTERSHIRE);
            AddConversion(WORCESTER, OS_HEREFORDSHIRE);
            AddConversion(WORCESTER, OS_SANDWELL);
            AddConversion(WORCESTER, OS_SHROPSHIRE);
            AddConversion(WORCESTER, OS_SOLIHULL);
            AddConversion(WORCESTER, OS_STAFFORDSHIRE);
            AddConversion(WORCESTER, OS_WARWICKSHIRE);
            AddConversion(WORCESTER, OS_WORCESTERSHIRE);
            AddConversion(YORKS, OS_BARNSLEY);
            AddConversion(YORKS, OS_BRADFORD);
            AddConversion(YORKS, OS_CALDERDALE);
            AddConversion(YORKS, OS_CITY_OF_KINGSTON_UPON_HULL);
            AddConversion(YORKS, OS_CUMBRIA);
            AddConversion(YORKS, OS_DARLINGTON);
            AddConversion(YORKS, OS_DERBYSHIRE);
            AddConversion(YORKS, OS_DONCASTER);
            AddConversion(YORKS, OS_DURHAM);
            AddConversion(YORKS, OS_EAST_RIDING_OF_YORKSHIRE);
            AddConversion(YORKS, OS_KIRKLEES);
            AddConversion(YORKS, OS_LANCASHIRE);
            AddConversion(YORKS, OS_LEEDS);
            AddConversion(YORKS, OS_MANCHESTER);
            AddConversion(YORKS, OS_MIDDLESBROUGH);
            AddConversion(YORKS, OS_NORTH_LINCOLNSHIRE);
            AddConversion(YORKS, OS_NORTH_YORKSHIRE);
            AddConversion(YORKS, OS_NOTTINGHAMSHIRE);
            AddConversion(YORKS, OS_OLDHAM);
            AddConversion(YORKS, OS_REDCAR_AND_CLEVELAND);
            AddConversion(YORKS, OS_ROCHDALE);
            AddConversion(YORKS, OS_ROTHERHAM);
            AddConversion(YORKS, OS_SHEFFIELD);
            AddConversion(YORKS, OS_STOCKTON_ON_TEES);
            AddConversion(YORKS, OS_TAMESIDE);
            AddConversion(YORKS, OS_WAKEFIELD);
            AddConversion(YORKS, OS_YORK);
            #endregion

            #region extra mappings for Modern Counties - as per Gazetteer
            AddConversion(ARGYLL_BUTE, OS_ARGYLL_AND_BUTE);
            AddConversion(BARKING, OS_BARKING_AND_DAGENHAM);
            AddConversion(BARNET, OS_BARNET);
            AddConversion(BARNSLEY, OS_BARNSLEY);
            AddConversion(BATH, OS_BATH_AND_NORTH_EAST_SOMERSET);
            AddConversion(BEXLEY, OS_BEXLEY);
            AddConversion(BIRMINGHAM, OS_BIRMINGHAM);
            AddConversion(BLACKBURN, OS_BLACKBURN_WITH_DARWEN);
            AddConversion(BLACKPOOL, OS_BLACKPOOL);
            AddConversion(BLAENAU_GWENT, OS_BLAENAU_GWENT);
            AddConversion(BOLTON, OS_BOLTON);
            AddConversion(BORDERS, OS_SCOTTISH_BORDERS);
            AddConversion(BOURNEMOUTH, OS_BOURNEMOUTH);
            AddConversion(BRACKNELL, OS_BRACKNELL_FOREST);
            AddConversion(BRADFORD, OS_BRADFORD);
            AddConversion(BRENT, OS_BRENT);
            AddConversion(BRIDGEND, OS_BRIDGEND);
            AddConversion(BRIGHTON, OS_CITY_OF_BRIGHTON_AND_HOVE);
            AddConversion(BRISTOL, OS_BRISTOL);
            AddConversion(BROMLEY, OS_BROMLEY);
            AddConversion(BURY, OS_BURY);
            AddConversion(CAERPHILLY, OS_CAERPHILLY);
            AddConversion(CALDERDALE, OS_CALDERDALE);
            AddConversion(CAMDEN, OS_CAMDEN);
            AddConversion(CARDIFF, OS_CARDIFF);
            AddConversion(CENTRAL_BEDFORDSHIRE, OS_CENTRAL_BEDFORDSHIRE);
            AddConversion(CONWY, OS_CONWY);
            AddConversion(COVENTRY, OS_COVENTRY);
            AddConversion(CROYDON, OS_CROYDON);
            AddConversion(CUMBRIA, OS_CUMBRIA);
            AddConversion(DARLINGTON, OS_DARLINGTON);
            AddConversion(DERBY_CITY, OS_CITY_OF_DERBY);
            AddConversion(DONCASTER, OS_DONCASTER);
            AddConversion(DUDLEY, OS_DUDLEY);
            AddConversion(DUMFRIES_GALLOWAY, OS_DUMFRIES_AND_GALLOWAY);
            AddConversion(DUNDEE_CITY, OS_DUNDEE_CITY);
            AddConversion(EALING, OS_EALING);
            AddConversion(EAST_AYRSHIRE, OS_EAST_AYRSHIRE);
            AddConversion(EAST_DUNBARTONSHIRE, OS_EAST_DUNBARTONSHIRE);
            AddConversion(EAST_RENFREW, OS_EAST_RENFREWSHIRE);
            AddConversion(EAST_SUSSEX, OS_EAST_SUSSEX);
            AddConversion(EDINBURGH_CITY, OS_CITY_OF_EDINBURGH);
            AddConversion(ENFIELD, OS_ENFIELD);
            AddConversion(FALKIRK, OS_FALKIRK);
            AddConversion(GATESHEAD, OS_GATESHEAD);
            AddConversion(GLASGOW_CITY, OS_GLASGOW_CITY);
            AddConversion(GREENWICH, OS_GREENWICH);
            AddConversion(HACKNEY, OS_HACKNEY);
            AddConversion(HALTON, OS_HALTON);
            AddConversion(HAMMERSMITH, OS_HAMMERSMITH_AND_FULHAM);
            AddConversion(HARINGEY, OS_HARINGEY);
            AddConversion(HARROW, OS_HARROW);
            AddConversion(HARTLEPOOL, OS_HARTLEPOOL);
            AddConversion(HAVERING, OS_HAVERING);
            AddConversion(HIGHLAND, OS_HIGHLAND);
            AddConversion(HILLINGDON, OS_HILLINGDON);
            AddConversion(HOUNSLOW, OS_HOUNSLOW);
            AddConversion(INVERCLYDE, OS_INVERCLYDE);
            AddConversion(INVERNESS_CITY, OS_CITY_OF_INVERNESS);
            AddConversion(IOM, OS_ISLE_OF_MAN);
            AddConversion(IOW, OS_ISLE_OF_WIGHT);
            AddConversion(ISLES_OF_SCILLY, OS_ISLES_OF_SCILLY);
            AddConversion(ISLINGTON, OS_ISLINGTON);
            AddConversion(KENSINGTON, OS_ROYAL_BOROUGH_OF_KENSINGTON_AND_CHELSEA);
            AddConversion(KINGSTON_HULL, OS_CITY_OF_KINGSTON_UPON_HULL);
            AddConversion(KINGSTON_THAMES, OS_KINGSTON_UPON_THAMES);
            AddConversion(KIRKLEES, OS_KIRKLEES);
            AddConversion(KNOWSLEY, OS_KNOWSLEY);
            AddConversion(LAMBETH, OS_LAMBETH);
            AddConversion(LEEDS, OS_LEEDS);
            AddConversion(LEICESTER_CITY, OS_CITY_OF_LEICESTER);
            AddConversion(LEWISHAM, OS_LEWISHAM);
            AddConversion(LIVERPOOL, OS_LIVERPOOL);
            AddConversion(LOTHIAN, OS_EAST_LOTHIAN);
            AddConversion(LOTHIAN, OS_WEST_LOTHIAN);
            AddConversion(LUTON, OS_LUTON);
            AddConversion(MEDWAY, OS_MEDWAY);
            AddConversion(MERTHYL, OS_MERTHYR_TYDFIL);
            AddConversion(MERTON, OS_MERTON);
            AddConversion(MIDDLESBROUGH, OS_MIDDLESBROUGH);
            AddConversion(MILTON_KEYNES, OS_MILTON_KEYNES);
            AddConversion(NEATH, OS_NEATH_PORT_TALBOT);
            AddConversion(NEWCASTLE, OS_NEWCASTLE_UPON_TYNE);
            AddConversion(NEWHAM, OS_NEWHAM);
            AddConversion(NEWPORT, OS_NEWPORT);
            AddConversion(NE_LINCOLNSHIRE, OS_NORTH_EAST_LINCOLNSHIRE);
            AddConversion(NORTH_AYRSHIRE, OS_NORTH_AYRSHIRE);
            AddConversion(NORTH_LANARK, OS_NORTH_LANARKSHIRE);
            AddConversion(NORTH_LINCOLNSHIRE, OS_NORTH_LINCOLNSHIRE);
            AddConversion(NORTH_SOMERSET, OS_NORTH_SOMERSET);
            AddConversion(NORTH_TYNESIDE, OS_NORTH_TYNESIDE);
            AddConversion(NORTH_YORKSHIRE, OS_NORTH_YORKSHIRE);
            AddConversion(NOTTINGHAM_CITY, OS_CITY_OF_NOTTINGHAM);
            AddConversion(OLDHAM, OS_OLDHAM);
            AddConversion(PERTH_KINROSS, OS_PERTH_AND_KINROSS);
            AddConversion(PETERBOROUGH, OS_CITY_OF_PETERBOROUGH);
            AddConversion(PLYMOUTH, OS_CITY_OF_PLYMOUTH);
            AddConversion(POOLE, OS_POOLE);
            AddConversion(PORTSMOUTH, OS_CITY_OF_PORTSMOUTH);
            AddConversion(READING, OS_READING);
            AddConversion(REDBRIDGE, OS_REDBRIDGE);
            AddConversion(REDCAR, OS_REDCAR_AND_CLEVELAND);
            AddConversion(RHONDDA, OS_RHONDDA_CYNON_TAFF);
            AddConversion(RICHMOND_THAMES, OS_RICHMOND_UPON_THAMES);
            AddConversion(ROCHDALE, OS_ROCHDALE);
            AddConversion(ROTHERHAM, OS_ROTHERHAM);
            AddConversion(SALFORD, OS_SALFORD);
            AddConversion(SANDWELL, OS_SANDWELL);
            AddConversion(SEFTON, OS_SEFTON);
            AddConversion(SHEFFIELD, OS_SHEFFIELD);
            AddConversion(SLOUGH, OS_SLOUGH);
            AddConversion(SOLIHULL, OS_SOLIHULL);
            AddConversion(SOUTHAMPTON, OS_CITY_OF_SOUTHAMPTON);
            AddConversion(SOUTHEND, OS_SOUTHEND_ON_SEA);
            AddConversion(SOUTHWARK, OS_SOUTHWARK);
            AddConversion(SOUTH_AYRSHIRE, OS_SOUTH_AYRSHIRE);
            AddConversion(SOUTH_GLOUCESTERSHIRE, OS_SOUTH_GLOUCESTERSHIRE);
            AddConversion(SOUTH_LANARK, OS_SOUTH_LANARKSHIRE);
            AddConversion(SOUTH_TYNESIDE, OS_SOUTH_TYNESIDE);
            AddConversion(STOCKPORT, OS_STOCKPORT);
            AddConversion(STOCKTON, OS_STOCKTON_ON_TEES);
            AddConversion(STOKE, OS_CITY_OF_STOKE_ON_TRENT);
            AddConversion(ST_HELENS, OS_ST_HELENS);
            AddConversion(SUNDERLAND, OS_SUNDERLAND);
            AddConversion(SUTTON, OS_SUTTON);
            AddConversion(SWANSEA, OS_SWANSEA);
            AddConversion(SWINDON, OS_SWINDON);
            AddConversion(TAMESIDE, OS_TAMESIDE);
            AddConversion(TELFORD, OS_TELFORD_AND_WREKIN);
            AddConversion(THURROCK, OS_THURROCK);
            AddConversion(TORBAY, OS_TORBAY);
            AddConversion(TORFAEN, OS_TORFAEN);
            AddConversion(TOWER_HAMLETS, OS_TOWER_HAMLETS);
            AddConversion(TRAFFORD, OS_TRAFFORD);
            AddConversion(VALE_GLAMORGAN, OS_VALE_OF_GLAMORGAN);
            AddConversion(WAKEFIELD, OS_WAKEFIELD);
            AddConversion(WALSALL, OS_WALSALL);
            AddConversion(WALTHAM_FOREST, OS_WALTHAM_FOREST);
            AddConversion(WANDSWORTH, OS_WANDSWORTH);
            AddConversion(WARRINGTON, OS_WARRINGTON);
            AddConversion(WESTERN_ISLES, OS_WESTERN_ISLES);
            AddConversion(WESTMINSTER, OS_CITY_OF_WESTMINSTER);
            AddConversion(WEST_BERKSHIRE, OS_WEST_BERKSHIRE);
            AddConversion(WEST_DUNBARTON, OS_WEST_DUNBARTONSHIRE);
            AddConversion(WEST_SUSSEX, OS_WEST_SUSSEX);
            AddConversion(WIGAN, OS_WIGAN);
            AddConversion(WINDSOR, OS_WINDSOR_AND_MAIDENHEAD);
            AddConversion(WIRRAL, OS_WIRRAL);
            AddConversion(WOKINGHAM, OS_WOKINGHAM);
            AddConversion(WOLVERHAMPTON, OS_CITY_OF_WOLVERHAMPTON);
            AddConversion(WREXHAM, OS_WREXHAM);
            AddConversion(YORK, OS_YORK);
            #endregion
        }

        #region Conversion Functions
        private static void AddConversion(Region region, ModernCounty county)
        {
            List<ModernCounty> counties;
            if (CONVERSIONS.ContainsKey(region))
                counties = CONVERSIONS[region];
            else
            {
                counties = new List<ModernCounty>();
                CONVERSIONS.Add(region, counties);
            }
            if (counties.Contains(county))
                Console.WriteLine("Duplicate county: " + region.PreferredName + " mapped to " + county.CountyName);
            else
                counties.Add(county);
        }

        private static void SetRegionsConversions()
        {
            foreach (Region region in PREFERRED_REGIONS)
                region.CountyCodes = GetCounties(region);
        }
        #endregion
        #endregion
    }
}
