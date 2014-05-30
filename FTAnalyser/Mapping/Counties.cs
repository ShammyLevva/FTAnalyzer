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
            public string CommonName { get; private set; }

            public County(string code, string county_name, string common_name)
            {
                CountyCode = code;
                CountyName = county_name;
                CommonName = common_name;
            }

            public override string ToString()
            {
                return CountyCode + ": " + CountyName;
            }
        }

        public static County GetCounty(string code)
        {
            return UK_COUNTIES.First(c => c.CountyCode.Equals(code));
        }

        static Counties()
        {
            UK_COUNTIES.Add(new County("AB","Aberdeenshire","Aberdeenshire"));
            UK_COUNTIES.Add(new County("AG","Angus","Angus"));
            UK_COUNTIES.Add(new County("AN","Aberdeen City","Aberdeen"));
            UK_COUNTIES.Add(new County("AR","Argyll and Bute","Argyll and Bute"));
            UK_COUNTIES.Add(new County("BA","Bradford","Bradford"));
            UK_COUNTIES.Add(new County("BB","Blackburn with Darwen","Blackburn"));
            UK_COUNTIES.Add(new County("BC","Bracknell Forest","Bracknell Forest"));
            UK_COUNTIES.Add(new County("BD","Barking & Dagenham","Barking & Dagenham"));
            UK_COUNTIES.Add(new County("BE","Bridgend","Bridgend"));
            UK_COUNTIES.Add(new County("BF","Bedford","Bedford"));
            UK_COUNTIES.Add(new County("BG","Blaenau Gwent","Blaenau Gwent"));
            UK_COUNTIES.Add(new County("BH","City of Brighton and Hove","City of Brighton and Hove"));
            UK_COUNTIES.Add(new County("BI","Birmingham","Birmingham"));
            UK_COUNTIES.Add(new County("BK","Central Bedfordshire","Central Bedfordshire"));
            UK_COUNTIES.Add(new County("BL","Barnsley","Barnsley"));
            UK_COUNTIES.Add(new County("BM","Buckinghamshire","Buckinghamshire"));
            UK_COUNTIES.Add(new County("BN","Barnet","Barnet"));
            UK_COUNTIES.Add(new County("BO","Bolton","Bolton"));
            UK_COUNTIES.Add(new County("BP","Blackpool","Blackpool"));
            UK_COUNTIES.Add(new County("BR","Bromley","Bromley"));
            UK_COUNTIES.Add(new County("BS","Bath and North East Somerset","Bath and North East Somerset"));
            UK_COUNTIES.Add(new County("BT","Brent","Brent"));
            UK_COUNTIES.Add(new County("BU","Bournemouth","Bournemouth"));
            UK_COUNTIES.Add(new County("BX","Bexley","Bexley"));
            UK_COUNTIES.Add(new County("BY","Bury","Bury"));
            UK_COUNTIES.Add(new County("BZ","City of Bristol","City of Bristol"));
            UK_COUNTIES.Add(new County("CA","Calderdale","Calderdale"));
            UK_COUNTIES.Add(new County("CB","Cambridgeshire","Cambridgeshire"));
            UK_COUNTIES.Add(new County("CC","Cheshire West and Chester","Cheshire West and Chester"));
            UK_COUNTIES.Add(new County("CD","Cardiff","Cardiff"));
            UK_COUNTIES.Add(new County("CE","Ceredigion","Ceredigion"));
            UK_COUNTIES.Add(new County("CF","Caerphilly","Caerphilly"));
            UK_COUNTIES.Add(new County("CH","Cheshire East","Cheshire East"));
            UK_COUNTIES.Add(new County("CL","Clackmannanshire","Clackmannanshire"));
            UK_COUNTIES.Add(new County("CM","Camden","Camden"));
            UK_COUNTIES.Add(new County("CN","Cornwall","Cornwall"));
            UK_COUNTIES.Add(new County("CT","Carmarthenshire","Carmarthenshire"));
            UK_COUNTIES.Add(new County("CU","Cumbria","Cumbria"));
            UK_COUNTIES.Add(new County("CV","Coventry","Coventry"));
            UK_COUNTIES.Add(new County("CW","Conwy","Conwy"));
            UK_COUNTIES.Add(new County("CY","Croydon","Croydon"));
            UK_COUNTIES.Add(new County("DB","City of Derby","City of Derby"));
            UK_COUNTIES.Add(new County("DD","Dundee City","Dundee City"));
            UK_COUNTIES.Add(new County("DE","Denbighshire","Denbighshire"));
            UK_COUNTIES.Add(new County("DG","Dumfries and Galloway","Dumfries and Galloway"));
            UK_COUNTIES.Add(new County("DL","Darlington","Darlington"));
            UK_COUNTIES.Add(new County("DN","Devon","Devon"));
            UK_COUNTIES.Add(new County("DR","Doncaster","Doncaster"));
            UK_COUNTIES.Add(new County("DT","Dorset","Dorset"));
            UK_COUNTIES.Add(new County("DU","Durham","Durham"));
            UK_COUNTIES.Add(new County("DY","Derbyshire","Derbyshire"));
            UK_COUNTIES.Add(new County("DZ","Dudley","Dudley"));
            UK_COUNTIES.Add(new County("EA","East Ayrshire","East Ayrshire"));
            UK_COUNTIES.Add(new County("EB","City of Edinburgh","City of Edinburgh"));
            UK_COUNTIES.Add(new County("ED","East Dunbartonshire","East Dunbartonshire"));
            UK_COUNTIES.Add(new County("EG","Ealing","Ealing"));
            UK_COUNTIES.Add(new County("EL","East Lothian","East Lothian"));
            UK_COUNTIES.Add(new County("EN","Enfield","Enfield"));
            UK_COUNTIES.Add(new County("ER","East Renfrewshire","East Renfrewshire"));
            UK_COUNTIES.Add(new County("ES","East Sussex","East Sussex"));
            UK_COUNTIES.Add(new County("EX","Essex","Essex"));
            UK_COUNTIES.Add(new County("EY","East Riding of Yorkshire","East Riding of Yorkshire"));
            UK_COUNTIES.Add(new County("FA","Falkirk","Falkirk"));
            UK_COUNTIES.Add(new County("FF","Fife","Fife"));
            UK_COUNTIES.Add(new County("FL","Flintshire","Flintshire"));
            UK_COUNTIES.Add(new County("GH","Gateshead","Gateshead"));
            UK_COUNTIES.Add(new County("GL","Glasgow City","Glasgow City"));
            UK_COUNTIES.Add(new County("GR","Gloucestershire","Gloucestershire"));
            UK_COUNTIES.Add(new County("GW","Greenwich","Greenwich"));
            UK_COUNTIES.Add(new County("GY","Gwynedd","Gwynedd"));
            UK_COUNTIES.Add(new County("HA","Halton","Halton"));
            UK_COUNTIES.Add(new County("HD","Hertfordshire","Hertfordshire"));
            UK_COUNTIES.Add(new County("HE","Herefordshire","Herefordshire"));
            UK_COUNTIES.Add(new County("HF","Hammersmith &Fulham","Hammersmith &Fulham"));
            UK_COUNTIES.Add(new County("HG","Haringey","Haringey"));
            UK_COUNTIES.Add(new County("HI","Hillingdon","Hillingdon"));
            UK_COUNTIES.Add(new County("HL","Highland","Highland"));
            UK_COUNTIES.Add(new County("HN","Hackney","Hackney"));
            UK_COUNTIES.Add(new County("HP","Hampshire","Hampshire"));
            UK_COUNTIES.Add(new County("HR","Harrow","Harrow"));
            UK_COUNTIES.Add(new County("HS","Hounslow","Hounslow"));
            UK_COUNTIES.Add(new County("HT","Hartlepool","Hartlepool"));
            UK_COUNTIES.Add(new County("HV","Havering","Havering"));
            UK_COUNTIES.Add(new County("IA","Isle of Anglesey","Isle of Anglesey"));
            UK_COUNTIES.Add(new County("IL","Islington","Islington"));
            UK_COUNTIES.Add(new County("IM","Isle of Man","Isle of Man"));
            UK_COUNTIES.Add(new County("IN","Inverclyde","Inverclyde"));
            UK_COUNTIES.Add(new County("IS","Isles of Scilly","Isles of Scilly"));
            UK_COUNTIES.Add(new County("IW","Isle of Wight","Isle of Wight"));
            UK_COUNTIES.Add(new County("KC","Royal Borough of Kensington & Chelsea","Royal Borough of Kensington & Chelsea"));
            UK_COUNTIES.Add(new County("KG","Kingston upon Thames","Kingston upon Thames"));
            UK_COUNTIES.Add(new County("KH","City of Kingston upon Hull","City of Kingston upon Hull"));
            UK_COUNTIES.Add(new County("KL","Kirklees","Kirklees"));
            UK_COUNTIES.Add(new County("KN","Knowsley","Knowsley"));
            UK_COUNTIES.Add(new County("KT","Kent","Kent"));
            UK_COUNTIES.Add(new County("LA","Lancashire","Lancashire"));
            UK_COUNTIES.Add(new County("LB","Lambeth","Lambeth"));
            UK_COUNTIES.Add(new County("LC","City of Leicester","City of Leicester"));
            UK_COUNTIES.Add(new County("LD","Leeds","Leeds"));
            UK_COUNTIES.Add(new County("LL","Lincolnshire","Lincolnshire"));
            UK_COUNTIES.Add(new County("LN","Luton","Luton"));
            UK_COUNTIES.Add(new County("LO","City of London","City of London"));
            UK_COUNTIES.Add(new County("LP","Liverpool","Liverpool"));
            UK_COUNTIES.Add(new County("LS","Lewisham","Lewisham"));
            UK_COUNTIES.Add(new County("LT","Leicestershire","Leicestershire"));
            UK_COUNTIES.Add(new County("MA","Manchester","Manchester"));
            UK_COUNTIES.Add(new County("MB","Middlesbrough","Middlesbrough"));
            UK_COUNTIES.Add(new County("ME","Medway","Medway"));
            UK_COUNTIES.Add(new County("MI","Midlothian","Midlothian"));
            UK_COUNTIES.Add(new County("MK","Milton Keynes","Milton Keynes"));
            UK_COUNTIES.Add(new County("MM","Monmouthshire","Monmouthshire"));
            UK_COUNTIES.Add(new County("MO","Moray","Moray"));
            UK_COUNTIES.Add(new County("MR","Merton","Merton"));
            UK_COUNTIES.Add(new County("MT","Merthyr Tydfil","Merthyr Tydfil"));
            UK_COUNTIES.Add(new County("NA","North Ayrshire","North Ayrshire"));
            UK_COUNTIES.Add(new County("NC","North East Lincolnshire","North East Lincolnshire"));
            UK_COUNTIES.Add(new County("ND","Northumberland","Northumberland"));
            UK_COUNTIES.Add(new County("NE","Newport","Newport"));
            UK_COUNTIES.Add(new County("NG","City of Nottingham","City of Nottingham"));
            UK_COUNTIES.Add(new County("NH","Newham","Newham"));
            UK_COUNTIES.Add(new County("NI","North Lincolnshire","North Lincolnshire"));
            UK_COUNTIES.Add(new County("NK","Norfolk","Norfolk"));
            UK_COUNTIES.Add(new County("NL","North Lanarkshire","North Lanarkshire"));
            UK_COUNTIES.Add(new County("NN","Northamptonshire","Northamptonshire"));
            UK_COUNTIES.Add(new County("NP","Neath Port Talbot","Neath Port Talbot"));
            UK_COUNTIES.Add(new County("NR","North Tyneside","North Tyneside"));
            UK_COUNTIES.Add(new County("NS","North Somerset","North Somerset"));
            UK_COUNTIES.Add(new County("NT","Nottinghamshire","Nottinghamshire"));
            UK_COUNTIES.Add(new County("NW","Newcastle upon Tyne","Newcastle upon Tyne"));
            UK_COUNTIES.Add(new County("NY","North Yorkshire","North Yorkshire"));
            UK_COUNTIES.Add(new County("OH","Oldham","Oldham"));
            UK_COUNTIES.Add(new County("OK","Orkney Islands","Orkney Islands"));
            UK_COUNTIES.Add(new County("ON","Oxfordshire","Oxfordshire"));
            UK_COUNTIES.Add(new County("PB","Pembrokeshire","Pembrokeshire"));
            UK_COUNTIES.Add(new County("PE","City of Peterborough","City of Peterborough"));
            UK_COUNTIES.Add(new County("PK","Perth and Kinross","Perth and Kinross"));
            UK_COUNTIES.Add(new County("PL","Poole","Poole"));
            UK_COUNTIES.Add(new County("PO","City of Portsmouth","City of Portsmouth"));
            UK_COUNTIES.Add(new County("PW","Powys","Powys"));
            UK_COUNTIES.Add(new County("PY","City of Plymouth","City of Plymouth"));
            UK_COUNTIES.Add(new County("RB","Redbridge","Redbridge"));
            UK_COUNTIES.Add(new County("RC","Redcar & Cleveland","Redcar & Cleveland"));
            UK_COUNTIES.Add(new County("RD","Rochdale","Rochdale"));
            UK_COUNTIES.Add(new County("RE","Renfrewshire","Renfrewshire"));
            UK_COUNTIES.Add(new County("RG","Reading","Reading"));
            UK_COUNTIES.Add(new County("RH","Rhondda,Cynon,Taff","Rhondda,Cynon,Taff"));
            UK_COUNTIES.Add(new County("RL","Rutland","Rutland"));
            UK_COUNTIES.Add(new County("RO","Rotherham","Rotherham"));
            UK_COUNTIES.Add(new County("RT","Richmond upon Thames","Richmond upon Thames"));
            UK_COUNTIES.Add(new County("SA","Sandwell","Sandwell"));
            UK_COUNTIES.Add(new County("SB","Scottish Borders","Scottish Borders"));
            UK_COUNTIES.Add(new County("SC","Salford","Salford"));
            UK_COUNTIES.Add(new County("SD","Swindon","Swindon"));
            UK_COUNTIES.Add(new County("SE","Sefton","Sefton"));
            UK_COUNTIES.Add(new County("SF","Staffordshire","Staffordshire"));
            UK_COUNTIES.Add(new County("SG","South Gloucestershire","South Gloucestershire"));
            UK_COUNTIES.Add(new County("SH","Shropshire","Shropshire"));
            UK_COUNTIES.Add(new County("SI","Shetland Islands","Shetland Islands"));
            UK_COUNTIES.Add(new County("SJ","City of Stoke-on-Trent","City of Stoke-on-Trent"));
            UK_COUNTIES.Add(new County("SK","Suffolk","Suffolk"));
            UK_COUNTIES.Add(new County("SL","South Lanarkshire","South Lanarkshire"));
            UK_COUNTIES.Add(new County("SM","Stockton on Tees","Stockton on Tees"));
            UK_COUNTIES.Add(new County("SN","St Helens","St Helens"));
            UK_COUNTIES.Add(new County("SO","City of Southampton","City of Southampton"));
            UK_COUNTIES.Add(new County("SP","Sheffield","Sheffield"));
            UK_COUNTIES.Add(new County("SQ","Solihull","Solihull"));
            UK_COUNTIES.Add(new County("SR","Stirling","Stirling"));
            UK_COUNTIES.Add(new County("SS","Swansea","Swansea"));
            UK_COUNTIES.Add(new County("ST","Somerset","Somerset"));
            UK_COUNTIES.Add(new County("SU","Surrey","Surrey"));
            UK_COUNTIES.Add(new County("SV","Sunderland","Sunderland"));
            UK_COUNTIES.Add(new County("SW","Southwark","Southwark"));
            UK_COUNTIES.Add(new County("SX","South Ayrshire","South Ayrshire"));
            UK_COUNTIES.Add(new County("SY","South Tyneside","South Tyneside"));
            UK_COUNTIES.Add(new County("SZ","Sutton","Sutton"));
            UK_COUNTIES.Add(new County("TB","Torbay","Torbay"));
            UK_COUNTIES.Add(new County("TF","Torfaen","Torfaen"));
            UK_COUNTIES.Add(new County("TH","Tower Hamlets","Tower Hamlets"));
            UK_COUNTIES.Add(new County("TR","Trafford","Trafford"));
            UK_COUNTIES.Add(new County("TS","Tameside","Tameside"));
            UK_COUNTIES.Add(new County("TU","Thurrock","Thurrock"));
            UK_COUNTIES.Add(new County("VG","The Vale of Glamorgan","The Vale of Glamorgan"));
            UK_COUNTIES.Add(new County("WA","Walsall","Walsall"));
            UK_COUNTIES.Add(new County("WB","West Berkshire","West Berkshire"));
            UK_COUNTIES.Add(new County("WC","Windsor and Maidenhead","Windsor and Maidenhead"));
            UK_COUNTIES.Add(new County("WD","West Dunbartonshire","West Dunbartonshire"));
            UK_COUNTIES.Add(new County("WE","Wakefield","Wakefield"));
            UK_COUNTIES.Add(new County("WF","Waltham Forest","Waltham Forest"));
            UK_COUNTIES.Add(new County("WG","Warrington","Warrington"));
            UK_COUNTIES.Add(new County("WH","City of Wolverhampton","City of Wolverhampton"));
            UK_COUNTIES.Add(new County("WI","Na h-Eileanan an Iar","Na h-Eileanan an Iar"));
            UK_COUNTIES.Add(new County("WJ","Wokingham","Wokingham"));
            UK_COUNTIES.Add(new County("WK","Warwickshire","Warwickshire"));
            UK_COUNTIES.Add(new County("WL","West Lothian","West Lothian"));
            UK_COUNTIES.Add(new County("WM","City of Westminster","City of Westminster"));
            UK_COUNTIES.Add(new County("WN","Wigan","Wigan"));
            UK_COUNTIES.Add(new County("WO","Worcestershire","Worcestershire"));
            UK_COUNTIES.Add(new County("WP","Telford and Wrekin","Telford and Wrekin"));
            UK_COUNTIES.Add(new County("WR","Wirral","Wirral"));
            UK_COUNTIES.Add(new County("WS","West Sussex","West Sussex"));
            UK_COUNTIES.Add(new County("WT","Wiltshire","Wiltshire"));
            UK_COUNTIES.Add(new County("WW","Wandsworth","Wandsworth"));
            UK_COUNTIES.Add(new County("WX","Wrexham","Wrexham"));
            UK_COUNTIES.Add(new County("XX","XXXXXXXX","XXXXXXXX"));
            UK_COUNTIES.Add(new County("YK","York","York"));
            UK_COUNTIES.Add(new County("YS","Southend-on-Sea","Southend-on-Sea"));
            UK_COUNTIES.Add(new County("YT","Slough","Slough"));
            UK_COUNTIES.Add(new County("YY","Stockport","Stockport"));
        }
    }
}
