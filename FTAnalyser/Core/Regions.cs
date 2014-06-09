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

        public static IDictionary<string, Region> PREFERRED_REGIONS;
        public static IDictionary<string, Region> VALID_REGIONS;

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

        //North Ayrshire
        //North Lanarkshire
        //Perth and Kinross
        //South Lanarkshire
        //South Ayrshire
        //West Dunbartonshire

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
        public static Region HOUNDSLOW = new Region("Hounslow", Countries.ENGLAND, Region.Creation.MODERN);
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
        public static Region PETERBOROUGH = new Region("City of Peterborough", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region POOLE = new Region("Poole", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region PORTSMOUTH = new Region("City of Portsmouth", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region PLYMOUTH = new Region("City of Plymouth", Countries.ENGLAND, Region.Creation.MODERN);
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
        public static Region STOKE = new Region("City of Stoke-on-Trent", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region STOCKTON = new Region("Stockton-on-Tees", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region ST_HELENS = new Region("St Helens", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTHAMPTON = new Region("City of Southampton", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SHEFFIELD = new Region("Sheffield", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOLIHULL = new Region("Solihull", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SUNDERLAND = new Region("Sunderland", Countries.ENGLAND, Region.Creation.MODERN);
        public static Region SOUTHWARD = new Region("Southwark", Countries.ENGLAND, Region.Creation.MODERN);
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
        public static Region WILTSHIRE = new Region("Wiltshire", Countries.ENGLAND, Region.Creation.MODERN);
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
        public static Region CARDIGAN = new Region("Cardigan", Countries.WALES, Region.Creation.HISTORIC);
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

        static Regions()
        {
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
                    DURHAM, ESSEX, GLOUCS, HANTS, HEREFORD, HERTS, HUNTS, KENT, LANCS, LEICS, LINCOLN, 
                    MIDDLESEX, NORFOLK, NORTHAMPTON, NORTHUMBERLAND, NOTTINGHAM, OXFORD, RUTLAND, SHROPS, 
                    SOMERSET, STAFFORD, SUFFOLK, SURREY, SUSSEX, WARWICK, WESTMORLAND, WILTS, WORCESTER,
                    YORKS, LONDON, MANCHESTER, MERSEYSIDE, SOUTH_YORKSHIRE, TYNE_WEAR, WEST_MIDLANDS,
                    WEST_YORKSHIRE, AVON, CLEVELAND, CUMBRIA, HUMBERSIDE, IOW, HEREFORD_WORCESTER,
                    NORTH_YORKSHIRE, EAST_YORKSHIRE, BRADFORD, BLACKBURN, BRACKNELL, BARKING, BRIGHTON,
                    BIRMINGHAM, BARNSLEY, BARNET, BOLTON, BLACKPOOL, BROMLEY, BATH, BRENT, BOURNEMOUTH,
                    BEXLEY, BURY, BRISTOL, CALDERDALE, CAMDEN, COVENTRY, CROYDON, DERBY_CITY,  DARLINGTON,
                    DONCASTER, DUDLEY, EALING, ENFIELD, EAST_SUSSEX, GATESHEAD, GREENWICH, HALTON, 
                    HAMMERSMITH, HARINGEY, HILLINGDON, HACKNEY, HARROW, HOUNDSLOW, HARTLEPOOL, HAVERING,
                    ISLINGTON, ISLES_OF_SCILLY, KENSINGTON, KINGSTON_THAMES, KINGSTON_HULL, KIRKLEES, 
                    KNOWSLEY, LAMBETH, LEICESTER_CITY, LEEDS, LUTON, LIVERPOOL, LEWISHAM, MIDDLESBROUGH,
                    MEDWAY, MILTON_KEYNES, MERTON, NE_LINCOLNSHIRE, NEWPORT, NOTTINGHAM_CITY, NEWHAM, 
                    NORTH_LINCOLNSHIRE, NORTH_TYNESIDE, NORTH_SOMERSET, NEWCASTLE, OLDHAM, PETERBOROUGH, 
                    POOLE, PORTSMOUTH, PLYMOUTH, REDBRIDGE, REDCAR, ROCHDALE, READING, ROTHERHAM, 
                    RICHMOND_THAMES, SANDWELL, SALFORD, SWINDON, SEFTON, SOUTH_GLOUCESTERSHIRE, STOKE, 
                    STOCKTON, ST_HELENS, SOUTHAMPTON, SHEFFIELD, SOLIHULL, SUNDERLAND, SOUTHWARD, 
                    SOUTH_TYNESIDE, SUTTON, TORBAY, TOWER_HAMLETS, TRAFFORD, TAMESIDE, THURROCK, WALSALL, 
                    WEST_BERKSHIRE, WINDSOR, WAKEFIELD, WALTHAM_FOREST, WARRINGTON, WOLVERHAMPTON, WOKINGHAM,
                    WESTMINSTER, WIGAN, TELFORD, WIRRAL, WEST_SUSSEX, WILTSHIRE, WANDSWORTH, WREXHAM, YORK,
                    SOUTHEND, SLOUGH, STOCKPORT
            });
            AddEnglishRegionAlternates();

            WELSH_REGIONS = new HashSet<Region>(new Region[] {
                    ANGLESEY, BRECON, CAERNARFON, CARDIGAN, CARMARTHEN, DENBIGH, FLINT, GLAMORGAN, MERIONETH, 
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

            #region Valid Regions
            PREFERRED_REGIONS = new Dictionary<string, Region>();
            VALID_REGIONS = new Dictionary<string, Region>();
            AppendValidRegions(UK_REGIONS);
            AppendValidRegions(IRISH_REGIONS);
            AppendValidRegions(CANADIAN_REGIONS);
            AppendValidRegions(US_STATES);
            AppendValidRegions(AUSTRALIAN_REGIONS);
            #endregion
        }

        private static void AppendValidRegions(ISet<Region> regions)
        {
            foreach (Region r in regions)
            {
                VALID_REGIONS.Add(r.PreferredName, r);
                PREFERRED_REGIONS.Add(r.PreferredName, r);
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
            return PREFERRED_REGIONS.ContainsKey(region);
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
            CARDIGAN.AddAlternateName("Cardiganshire");
            CARDIGAN.AddAlternateName("Ceredigionshire");
            CARDIGAN.AddAlternateName("Ceredigion");
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
            LINCOLN.AddAlternateName("Lincs");
            LONDON.AddAlternateName("City of London");
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
    }
}
