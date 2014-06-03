using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Mapping
{
    public class Counties
    {
        public static readonly List<County> UK_COUNTIES = new List<County>();

        public class County
        {
            public string CountyCode { get; private set; }
            public string CountyName { get; private set; }
            public string LookupName { get; private set; }

            public County(string lookupName, string code, string countyName)
            {
                LookupName = lookupName;
                CountyCode = code;
                CountyName = countyName;
            }

            public override string ToString()
            {
                return CountyCode + ": " + CountyName;
            }
        }

        public static List<County> GetCounties(string lookup)
        {
            return UK_COUNTIES.Where(c => c.LookupName.Equals(lookup)).ToList<County>();
        }

        public static County GetCounty(string code)
        {
            return UK_COUNTIES.First(c => c.CountyCode.Equals(code));
        }

        public static readonly County UNKNOWN = new County("Unknown", "XX", "County Unknown");

        static Counties()
        {
            UK_COUNTIES.Add(new County("Aberdeen City", "AN", "Aberdeen City"));
            UK_COUNTIES.Add(new County("Aberdeen", "AN", "Aberdeen City"));
            UK_COUNTIES.Add(new County("Aberdeenshire", "AN", "Aberdeen City"));
            UK_COUNTIES.Add(new County("Aberdeenshire", "AB", "Aberdeenshire"));
            UK_COUNTIES.Add(new County("Kincardineshire", "AB", "Aberdeenshire"));
            UK_COUNTIES.Add(new County("Banffshire", "AB", "Aberdeenshire"));
            UK_COUNTIES.Add(new County("Angus", "AG", "Angus"));
            UK_COUNTIES.Add(new County("Argyll", "AR", "Argyll and Bute"));
            UK_COUNTIES.Add(new County("Ayrshire", "EA", "East Ayrshire"));
            UK_COUNTIES.Add(new County("Ayrshire", "NA", "North Ayrshire"));
            UK_COUNTIES.Add(new County("Ayrshire", "SX", "South Ayrshire"));
            UK_COUNTIES.Add(new County("Barking & Dagenham", "BD", "Barking & Dagenham"));
            UK_COUNTIES.Add(new County("Barking", "BD", "Barking & Dagenham"));
            UK_COUNTIES.Add(new County("Barnet", "BN", "Barnet"));
            UK_COUNTIES.Add(new County("Barnsley", "BL", "Barnsley"));
            UK_COUNTIES.Add(new County("Bath and North East Somerset", "BS", "Bath and North East Somerset"));
            UK_COUNTIES.Add(new County("Bath", "BS", "Bath and North East Somerset"));
            UK_COUNTIES.Add(new County("Bedford", "BF", "Bedford"));
            UK_COUNTIES.Add(new County("Bedfordshire", "BK", "Central Bedfordshire"));
            UK_COUNTIES.Add(new County("Berkshire", "WB", "West Berkshire"));
            UK_COUNTIES.Add(new County("Bexley", "BX", "Bexley"));
            UK_COUNTIES.Add(new County("Birmingham", "BI", "Birmingham"));
            UK_COUNTIES.Add(new County("Blackburn", "BB", "Blackburn with Darwen"));
            UK_COUNTIES.Add(new County("Blackpool", "BP", "Blackpool"));
            UK_COUNTIES.Add(new County("Blaenau Gwent", "BG", "Blaenau Gwent"));
            UK_COUNTIES.Add(new County("Bolton", "BO", "Bolton"));
            UK_COUNTIES.Add(new County("Bournemouth", "BU", "Bournemouth"));
            UK_COUNTIES.Add(new County("Bracknell Forest", "BC", "Bracknell Forest"));
            UK_COUNTIES.Add(new County("Bradford", "BA", "Bradford"));
            UK_COUNTIES.Add(new County("Brent", "BT", "Brent"));
            UK_COUNTIES.Add(new County("Bridgend", "BE", "Bridgend"));
            UK_COUNTIES.Add(new County("Brighton", "BH", "City of Brighton and Hove"));
            UK_COUNTIES.Add(new County("Bristol", "BZ", "City of Bristol"));
            UK_COUNTIES.Add(new County("Bromley", "BR", "Bromley"));
            UK_COUNTIES.Add(new County("Buckinghamshire", "BM", "Buckinghamshire"));
            UK_COUNTIES.Add(new County("Bury", "BY", "Bury"));
            UK_COUNTIES.Add(new County("Bute", "AR", "Argyll and Bute"));
            UK_COUNTIES.Add(new County("Caerphilly", "CF", "Caerphilly"));
            UK_COUNTIES.Add(new County("Calderdale", "CA", "Calderdale"));
            UK_COUNTIES.Add(new County("Cambridgeshire", "CB", "Cambridgeshire"));
            UK_COUNTIES.Add(new County("Camden", "CM", "Camden"));
            UK_COUNTIES.Add(new County("Cardiff", "CD", "Cardiff"));
            UK_COUNTIES.Add(new County("Carmarthenshire", "CT", "Carmarthenshire"));
            UK_COUNTIES.Add(new County("Central Bedfordshire", "BK", "Central Bedfordshire"));
            UK_COUNTIES.Add(new County("Ceredigion", "CE", "Ceredigion"));
            UK_COUNTIES.Add(new County("Chelsea", "KC", "Royal Borough of Kensington & Chelsea"));
            UK_COUNTIES.Add(new County("Cheshire East", "CH", "Cheshire East"));
            UK_COUNTIES.Add(new County("Cheshire West and Chester", "CC", "Cheshire West and Chester"));
            UK_COUNTIES.Add(new County("Cheshire", "CC", "Cheshire West and Chester"));
            UK_COUNTIES.Add(new County("Cheshire", "CH", "Cheshire East"));
            UK_COUNTIES.Add(new County("Chester", "CC", "Cheshire West and Chester"));
            UK_COUNTIES.Add(new County("City of Brighton and Hove", "BH", "City of Brighton and Hove"));
            UK_COUNTIES.Add(new County("City of Bristol", "BZ", "City of Bristol"));
            UK_COUNTIES.Add(new County("City of Derby", "DB", "City of Derby"));
            UK_COUNTIES.Add(new County("City of Edinburgh", "EB", "City of Edinburgh"));
            UK_COUNTIES.Add(new County("City of Kingston upon Hull", "KH", "City of Kingston upon Hull"));
            UK_COUNTIES.Add(new County("City of Leicester", "LC", "City of Leicester"));
            UK_COUNTIES.Add(new County("City of London", "LO", "City of London"));
            UK_COUNTIES.Add(new County("City of Nottingham", "NG", "City of Nottingham"));
            UK_COUNTIES.Add(new County("City of Peterborough", "PE", "City of Peterborough"));
            UK_COUNTIES.Add(new County("City of Plymouth", "PY", "City of Plymouth"));
            UK_COUNTIES.Add(new County("City of Portsmouth", "PO", "City of Portsmouth"));
            UK_COUNTIES.Add(new County("City of Southampton", "SO", "City of Southampton"));
            UK_COUNTIES.Add(new County("City of Stoke-on-Trent", "SJ", "City of Stoke-on-Trent"));
            UK_COUNTIES.Add(new County("City of Westminster", "WM", "City of Westminster"));
            UK_COUNTIES.Add(new County("City of Wolverhampton", "WH", "City of Wolverhampton"));
            UK_COUNTIES.Add(new County("Clackmannanshire", "CL", "Clackmannanshire"));
            UK_COUNTIES.Add(new County("Conwy", "CW", "Conwy"));
            UK_COUNTIES.Add(new County("Cornwall", "CN", "Cornwall"));
            UK_COUNTIES.Add(new County("Coventry", "CV", "Coventry"));
            UK_COUNTIES.Add(new County("Croydon", "CY", "Croydon"));
            UK_COUNTIES.Add(new County("Cumbria", "CU", "Cumbria"));
            UK_COUNTIES.Add(new County("Dagenham", "BD", "Barking & Dagenham"));
            UK_COUNTIES.Add(new County("Darlington", "DL", "Darlington"));
            UK_COUNTIES.Add(new County("Denbighshire", "DE", "Denbighshire"));
            UK_COUNTIES.Add(new County("Derby", "DB", "City of Derby"));
            UK_COUNTIES.Add(new County("Derbyshire", "DY", "Derbyshire"));
            UK_COUNTIES.Add(new County("Devon", "DN", "Devon"));
            UK_COUNTIES.Add(new County("Doncaster", "DR", "Doncaster"));
            UK_COUNTIES.Add(new County("Dorset", "DT", "Dorset"));
            UK_COUNTIES.Add(new County("Dudley", "DZ", "Dudley"));
            UK_COUNTIES.Add(new County("Dumfries and Galloway", "DG", "Dumfries and Galloway"));
            UK_COUNTIES.Add(new County("Dumfries", "DG", "Dumfries and Galloway"));
            UK_COUNTIES.Add(new County("Dunbartonshire", "ED", "East Dunbartonshire"));
            UK_COUNTIES.Add(new County("Dunbartonshire", "WD", "West Dunbartonshire"));
            UK_COUNTIES.Add(new County("Dundee City", "DD", "Dundee City"));
            UK_COUNTIES.Add(new County("Dundee", "DD", "Dundee City"));
            UK_COUNTIES.Add(new County("Durham", "DU", "Durham"));
            UK_COUNTIES.Add(new County("Ealing", "EG", "Ealing"));
            UK_COUNTIES.Add(new County("East Ayrshire", "EA", "East Ayrshire"));
            UK_COUNTIES.Add(new County("East Dunbartonshire", "ED", "East Dunbartonshire"));
            UK_COUNTIES.Add(new County("East Lothian", "EL", "East Lothian"));
            UK_COUNTIES.Add(new County("East Renfrewshire", "ER", "East Renfrewshire"));
            UK_COUNTIES.Add(new County("East Riding of Yorkshire", "EY", "East Riding of Yorkshire"));
            UK_COUNTIES.Add(new County("East Sussex", "ES", "East Sussex"));
            UK_COUNTIES.Add(new County("Edinburgh", "EB", "City of Edinburgh"));
            UK_COUNTIES.Add(new County("Enfield", "EN", "Enfield"));
            UK_COUNTIES.Add(new County("Essex", "EX", "Essex"));
            UK_COUNTIES.Add(new County("Falkirk", "FA", "Falkirk"));
            UK_COUNTIES.Add(new County("Fife", "FF", "Fife"));
            UK_COUNTIES.Add(new County("Flintshire", "FL", "Flintshire"));
            UK_COUNTIES.Add(new County("Fulham", "HF", "Hammersmith and Fulham"));
            UK_COUNTIES.Add(new County("Galloway", "DG", "Dumfries and Galloway"));
            UK_COUNTIES.Add(new County("Gateshead", "GH", "Gateshead"));
            UK_COUNTIES.Add(new County("Glasgow City", "GL", "Glasgow City"));
            UK_COUNTIES.Add(new County("Glasgow", "GL", "Glasgow City"));
            UK_COUNTIES.Add(new County("Gloucestershire", "GR", "Gloucestershire"));
            UK_COUNTIES.Add(new County("Gloucestershire", "SG", "South Gloucestershire"));
            UK_COUNTIES.Add(new County("Greenwich", "GW", "Greenwich"));
            UK_COUNTIES.Add(new County("Gwynedd", "GY", "Gwynedd"));
            UK_COUNTIES.Add(new County("Hackney", "HN", "Hackney"));
            UK_COUNTIES.Add(new County("Halton", "HA", "Halton"));
            UK_COUNTIES.Add(new County("Hammersmith and Fulham", "HF", "Hammersmith and Fulham"));
            UK_COUNTIES.Add(new County("Hammersmith", "HF", "Hammersmith and Fulham"));
            UK_COUNTIES.Add(new County("Hampshire", "HP", "Hampshire"));
            UK_COUNTIES.Add(new County("Haringey", "HG", "Haringey"));
            UK_COUNTIES.Add(new County("Harrow", "HR", "Harrow"));
            UK_COUNTIES.Add(new County("Hartlepool", "HT", "Hartlepool"));
            UK_COUNTIES.Add(new County("Havering", "HV", "Havering"));
            UK_COUNTIES.Add(new County("Herefordshire", "HE", "Herefordshire"));
            UK_COUNTIES.Add(new County("Hertfordshire", "HD", "Hertfordshire"));
            UK_COUNTIES.Add(new County("Highland", "HL", "Highland"));
            UK_COUNTIES.Add(new County("Hillingdon", "HI", "Hillingdon"));
            UK_COUNTIES.Add(new County("Hounslow", "HS", "Hounslow"));
            UK_COUNTIES.Add(new County("Hove", "BH", "City of Brighton and Hove"));
            UK_COUNTIES.Add(new County("Inverclyde", "IN", "Inverclyde"));
            UK_COUNTIES.Add(new County("Isle of Anglesey", "IA", "Isle of Anglesey"));
            UK_COUNTIES.Add(new County("Isle of Man", "IM", "Isle of Man"));
            UK_COUNTIES.Add(new County("Isle of Wight", "IW", "Isle of Wight"));
            UK_COUNTIES.Add(new County("Isles of Scilly", "IS", "Isles of Scilly"));
            UK_COUNTIES.Add(new County("Islington", "IL", "Islington"));
            UK_COUNTIES.Add(new County("Kensington & Chelsea", "KC", "Royal Borough of Kensington & Chelsea"));
            UK_COUNTIES.Add(new County("Kensington", "KC", "Royal Borough of Kensington & Chelsea"));
            UK_COUNTIES.Add(new County("Kent", "KT", "Kent"));
            UK_COUNTIES.Add(new County("Kingston upon Hull", "KH", "City of Kingston upon Hull"));
            UK_COUNTIES.Add(new County("Kingston upon Thames", "KG", "Kingston upon Thames"));
            UK_COUNTIES.Add(new County("Kirklees", "KL", "Kirklees"));
            UK_COUNTIES.Add(new County("Knowsley", "KN", "Knowsley"));
            UK_COUNTIES.Add(new County("Lambeth", "LB", "Lambeth"));
            UK_COUNTIES.Add(new County("Lanarkshire", "NL", "North Lanarkshire"));
            UK_COUNTIES.Add(new County("Lanarkshire", "SL", "South Lanarkshire"));
            UK_COUNTIES.Add(new County("Lancashire", "LA", "Lancashire"));
            UK_COUNTIES.Add(new County("Leeds", "LD", "Leeds"));
            UK_COUNTIES.Add(new County("Leicester", "LC", "City of Leicester"));
            UK_COUNTIES.Add(new County("Leicestershire", "LT", "Leicestershire"));
            UK_COUNTIES.Add(new County("Lewisham", "LS", "Lewisham"));
            UK_COUNTIES.Add(new County("Lincolnshire", "LL", "Lincolnshire"));
            UK_COUNTIES.Add(new County("Lincolnshire", "NC", "North East Lincolnshire"));
            UK_COUNTIES.Add(new County("Lincolnshire", "NI", "North Lincolnshire"));
            UK_COUNTIES.Add(new County("Liverpool", "LP", "Liverpool"));
            UK_COUNTIES.Add(new County("London", "LO", "City of London"));
            UK_COUNTIES.Add(new County("Lothian", "EL", "East Lothian"));
            UK_COUNTIES.Add(new County("Lothian", "WL", "West Lothian"));
            UK_COUNTIES.Add(new County("Luton", "LN", "Luton"));
            UK_COUNTIES.Add(new County("Maidenhead", "WC", "Windsor and Maidenhead"));
            UK_COUNTIES.Add(new County("Manchester", "MA", "Manchester"));
            UK_COUNTIES.Add(new County("Medway", "ME", "Medway"));
            UK_COUNTIES.Add(new County("Merthyr Tydfil", "MT", "Merthyr Tydfil"));
            UK_COUNTIES.Add(new County("Merton", "MR", "Merton"));
            UK_COUNTIES.Add(new County("Middlesbrough", "MB", "Middlesbrough"));
            UK_COUNTIES.Add(new County("Midlothian", "MI", "Midlothian"));
            UK_COUNTIES.Add(new County("Milton Keynes", "MK", "Milton Keynes"));
            UK_COUNTIES.Add(new County("Monmouthshire", "MM", "Monmouthshire"));
            UK_COUNTIES.Add(new County("Moray", "MO", "Moray"));
            UK_COUNTIES.Add(new County("Na h-Eileanan an Iar", "WI", "Na h-Eileanan an Iar"));
            UK_COUNTIES.Add(new County("Neath Port Talbot", "NP", "Neath Port Talbot"));
            UK_COUNTIES.Add(new County("Newcastle upon Tyne", "NW", "Newcastle upon Tyne"));
            UK_COUNTIES.Add(new County("Newham", "NH", "Newham"));
            UK_COUNTIES.Add(new County("Newport", "NE", "Newport"));
            UK_COUNTIES.Add(new County("Norfolk", "NK", "Norfolk"));
            UK_COUNTIES.Add(new County("North Ayrshire", "NA", "North Ayrshire"));
            UK_COUNTIES.Add(new County("North East Lincolnshire", "NC", "North East Lincolnshire"));
            UK_COUNTIES.Add(new County("North Lanarkshire", "NL", "North Lanarkshire"));
            UK_COUNTIES.Add(new County("North Lincolnshire", "NI", "North Lincolnshire"));
            UK_COUNTIES.Add(new County("North Somerset", "NS", "North Somerset"));
            UK_COUNTIES.Add(new County("North Tyneside", "NR", "North Tyneside"));
            UK_COUNTIES.Add(new County("North Yorkshire", "NY", "North Yorkshire"));
            UK_COUNTIES.Add(new County("Northamptonshire", "NN", "Northamptonshire"));
            UK_COUNTIES.Add(new County("Northumberland", "ND", "Northumberland"));
            UK_COUNTIES.Add(new County("Nottingham", "NG", "City of Nottingham"));
            UK_COUNTIES.Add(new County("Nottinghamshire", "NT", "Nottinghamshire"));
            UK_COUNTIES.Add(new County("Oldham", "OH", "Oldham"));
            UK_COUNTIES.Add(new County("Orkney Islands", "OK", "Orkney Islands"));
            UK_COUNTIES.Add(new County("Oxfordshire", "ON", "Oxfordshire"));
            UK_COUNTIES.Add(new County("Pembrokeshire", "PB", "Pembrokeshire"));
            UK_COUNTIES.Add(new County("Perth and Kinross", "PK", "Perth and Kinross"));
            UK_COUNTIES.Add(new County("Peterborough", "PE", "City of Peterborough"));
            UK_COUNTIES.Add(new County("Plymouth", "PY", "City of Plymouth"));
            UK_COUNTIES.Add(new County("Poole", "PL", "Poole"));
            UK_COUNTIES.Add(new County("Portsmouth", "PO", "City of Portsmouth"));
            UK_COUNTIES.Add(new County("Powys", "PW", "Powys"));
            UK_COUNTIES.Add(new County("Reading", "RG", "Reading"));
            UK_COUNTIES.Add(new County("Redbridge", "RB", "Redbridge"));
            UK_COUNTIES.Add(new County("Redcar & Cleveland", "RC", "Redcar & Cleveland"));
            UK_COUNTIES.Add(new County("Renfrewshire", "ER", "East Renfrewshire"));
            UK_COUNTIES.Add(new County("Renfrewshire", "RE", "Renfrewshire"));
            UK_COUNTIES.Add(new County("Rhondda", "RH", "Rhondda,Cynon,Taff"));
            UK_COUNTIES.Add(new County("Richmond upon Thames", "RT", "Richmond upon Thames"));
            UK_COUNTIES.Add(new County("Rochdale", "RD", "Rochdale"));
            UK_COUNTIES.Add(new County("Rotherham", "RO", "Rotherham"));
            UK_COUNTIES.Add(new County("Royal Borough of Kensington & Chelsea", "KC", "Royal Borough of Kensington & Chelsea"));
            UK_COUNTIES.Add(new County("Rutland", "RL", "Rutland"));
            UK_COUNTIES.Add(new County("Salford", "SC", "Salford"));
            UK_COUNTIES.Add(new County("Sandwell", "SA", "Sandwell"));
            UK_COUNTIES.Add(new County("Scottish Borders", "SB", "Scottish Borders"));
            UK_COUNTIES.Add(new County("Sefton", "SE", "Sefton"));
            UK_COUNTIES.Add(new County("Sheffield", "SP", "Sheffield"));
            UK_COUNTIES.Add(new County("Shetland Islands", "SI", "Shetland Islands"));
            UK_COUNTIES.Add(new County("Shropshire", "SH", "Shropshire"));
            UK_COUNTIES.Add(new County("Slough", "YT", "Slough"));
            UK_COUNTIES.Add(new County("Solihull", "SQ", "Solihull"));
            UK_COUNTIES.Add(new County("Somerset", "BS", "Bath and North East Somerset"));
            UK_COUNTIES.Add(new County("Somerset", "NS", "North Somerset"));
            UK_COUNTIES.Add(new County("Somerset", "ST", "Somerset"));
            UK_COUNTIES.Add(new County("South Ayrshire", "SX", "South Ayrshire"));
            UK_COUNTIES.Add(new County("South Gloucestershire", "SG", "South Gloucestershire"));
            UK_COUNTIES.Add(new County("South Lanarkshire", "SL", "South Lanarkshire"));
            UK_COUNTIES.Add(new County("South Tyneside", "SY", "South Tyneside"));
            UK_COUNTIES.Add(new County("Southampton", "SO", "City of Southampton"));
            UK_COUNTIES.Add(new County("Southend-on-Sea", "YS", "Southend-on-Sea"));
            UK_COUNTIES.Add(new County("Southwark", "SW", "Southwark"));
            UK_COUNTIES.Add(new County("St Helens", "SN", "St Helens"));
            UK_COUNTIES.Add(new County("Staffordshire", "SF", "Staffordshire"));
            UK_COUNTIES.Add(new County("Stirling", "SR", "Stirling"));
            UK_COUNTIES.Add(new County("Stockport", "YY", "Stockport"));
            UK_COUNTIES.Add(new County("Stockton on Tees", "SM", "Stockton on Tees"));
            UK_COUNTIES.Add(new County("Stoke-on-Trent", "SJ", "City of Stoke-on-Trent"));
            UK_COUNTIES.Add(new County("Suffolk", "SK", "Suffolk"));
            UK_COUNTIES.Add(new County("Sunderland", "SV", "Sunderland"));
            UK_COUNTIES.Add(new County("Surrey", "SU", "Surrey"));
            UK_COUNTIES.Add(new County("Sussex", "ES", "East Sussex"));
            UK_COUNTIES.Add(new County("Sussex", "WS", "West Sussex"));
            UK_COUNTIES.Add(new County("Sutton", "SZ", "Sutton"));
            UK_COUNTIES.Add(new County("Swansea", "SS", "Swansea"));
            UK_COUNTIES.Add(new County("Swindon", "SD", "Swindon"));
            UK_COUNTIES.Add(new County("Tameside", "TS", "Tameside"));
            UK_COUNTIES.Add(new County("Telford and Wrekin", "WP", "Telford and Wrekin"));
            UK_COUNTIES.Add(new County("Telford", "WP", "Telford and Wrekin"));
            UK_COUNTIES.Add(new County("The Vale of Glamorgan", "VG", "The Vale of Glamorgan"));
            UK_COUNTIES.Add(new County("Thurrock", "TU", "Thurrock"));
            UK_COUNTIES.Add(new County("Torbay", "TB", "Torbay"));
            UK_COUNTIES.Add(new County("Torfaen", "TF", "Torfaen"));
            UK_COUNTIES.Add(new County("Tower Hamlets", "TH", "Tower Hamlets"));
            UK_COUNTIES.Add(new County("Trafford", "TR", "Trafford"));
            UK_COUNTIES.Add(new County("Tyneside", "NR", "North Tyneside"));
            UK_COUNTIES.Add(new County("Tyneside", "SY", "South Tyneside"));
            UK_COUNTIES.Add(new County("Vale of Glamorgan", "VG", "The Vale of Glamorgan"));
            UK_COUNTIES.Add(new County("Wakefield", "WE", "Wakefield"));
            UK_COUNTIES.Add(new County("Walsall", "WA", "Walsall"));
            UK_COUNTIES.Add(new County("Waltham Forest", "WF", "Waltham Forest"));
            UK_COUNTIES.Add(new County("Wandsworth", "WW", "Wandsworth"));
            UK_COUNTIES.Add(new County("Warrington", "WG", "Warrington"));
            UK_COUNTIES.Add(new County("Warwickshire", "WK", "Warwickshire"));
            UK_COUNTIES.Add(new County("West Berkshire", "WB", "West Berkshire"));
            UK_COUNTIES.Add(new County("West Dunbartonshire", "WD", "West Dunbartonshire"));
            UK_COUNTIES.Add(new County("West Lothian", "WL", "West Lothian"));
            UK_COUNTIES.Add(new County("West Sussex", "WS", "West Sussex"));
            UK_COUNTIES.Add(new County("Western Isles", "WI", "Na h-Eileanan an Iar"));
            UK_COUNTIES.Add(new County("Westminster", "WM", "City of Westminster"));
            UK_COUNTIES.Add(new County("Wigan", "WN", "Wigan"));
            UK_COUNTIES.Add(new County("Wiltshire", "WT", "Wiltshire"));
            UK_COUNTIES.Add(new County("Windsor and Maidenhead", "WC", "Windsor and Maidenhead"));
            UK_COUNTIES.Add(new County("Windsor", "WC", "Windsor and Maidenhead"));
            UK_COUNTIES.Add(new County("Wirral", "WR", "Wirral"));
            UK_COUNTIES.Add(new County("Wokingham", "WJ", "Wokingham"));
            UK_COUNTIES.Add(new County("Wolverhampton", "WH", "City of Wolverhampton"));
            UK_COUNTIES.Add(new County("Worcestershire", "WO", "Worcestershire"));
            UK_COUNTIES.Add(new County("Wrekin", "WP", "Telford and Wrekin"));
            UK_COUNTIES.Add(new County("Wrexham", "WX", "Wrexham"));
            UK_COUNTIES.Add(new County("York", "YK", "York"));
        }
    }
}
