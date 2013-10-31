using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoAPI.Geometries;

namespace FTAnalyzer
{
    public class Countries
    {
        private static IDictionary<string, FactLocation> locationCache = new Dictionary<string, FactLocation>();

        public static readonly string SCOTLAND = "Scotland", ENGLAND = "England", CANADA = "Canada", UNITED_STATES = "United States",
            WALES = "Wales", IRELAND = "Ireland", UNITED_KINGDOM = "United Kingdom", NEW_ZEALAND = "New Zealand", AUSTRALIA = "Australia",
            UNKNOWN_COUNTRY = "Unknown", ENG_WALES = "England & Wales", INDIA = "India", FRANCE = "France", GERMANY = "Germany",
            ITALY = "Italy", SPAIN = "Spain", BELGIUM = "Belgium", SOUTH_AFRICA = "South Africa", NORTHERN_IRELAND = "Northern Ireland",
            EGYPT = "Egypt", HUNGARY = "Hungary", MALTA = "Malta", DENMARK = "Denmark", SWEDEN = "Sweden", NORWAY = "Norway",
            FINLAND = "Finland", ICELAND = "Iceland", SWITZERLAND = "Switzerland", AUSTRIA = "Austria", NETHERLANDS = "Netherlands",
            CHINA = "China", ZIMBABWE = "Zimbabwe", JAPAN = "Japan", RUSSIA = "Russia", POLAND = "Poland", ST_LUCIA = "Saint Lucia",
            LUXEMBOURG = "Luxembourg", ISLE_OF_MAN = "Isle of Man", GREECE = "Greece", LIBYA = "Libya", NIGERIA = "Nigeria",
            BULGARIA = "Bulgaria", CYPRUS = "Cyprus", ESTONIA = "Estonia", LATVIA = "Latvia", LIECHTENSTIEN = "Liechtenstien",
            LITHUANIA = "Lithuania", ALBANIA = "Albania", ANDORRA = "Andorra", ARMENIA = "Armenia", AZERBAIJAN = "Azerbaijan",
            BELARUS = "Belarus", MACEDONIA = "Macedonia", MOLDOVA = "Moldova", MONACO = "Monaco", MONTENEGRO = "Montenegro",
            PORTUGAL = "Portugal", ROMANIA = "Romania", SAN_MARINO = "San Marino", TURKEY = "Turkey", UKRAINE = "Ukraine",
            BRAZIL = "Brazil", MAURITIUS = "Mauritius", UAE = "United Arab Emirates", AFGHANISTAN = "Afghanistan",
            ARGENTINA = "Argentina", BARBADOS = "Barbados", BANGLADESH = "Bangladesh", BAHAMAS = "Bahamas", SRI_LANKA = "Sri Lanka",
            CUBA = "Cuba", INDONESIA = "Indonesia", ISRAEL = "Israel", IRAQ = "Iraq", IRAN = "Iran", JORDAN = "Jordan",
            JAMAICA = "Jamaica", KENYA = "Kenya", MEXICO = "Mexico";

        //AG  Antigua and Barbuda
        //AO  Angola
        //BA  Bosnia and Herzegovina
        //BF  Burkina Faso
        //BH  Bahrain
        //BI  Burundi
        //BJ  Benin
        //BN  Brunei Darussalam
        //BO  Bolivia (Plurinational State of)
        //BT  Bhutan
        //BW  Botswana
        //BZ  Belize
        //CD  Democratic Republic of the Congo
        //CF  Central African Republic
        //CG  Congo
        //CI  Côte d'Ivoire
        //CL  Chile
        //CM  Cameroon
        //CO  Colombia
        //CR  Costa Rica
        //CV  Cape Verde
        //CZ  Czech Republic
        //DJ  Djibouti
        //DM  Dominica
        //DO  Dominican Republic
        //DZ  Algeria
        //EC  Ecuador
        //ER  Eritrea
        //ET  Ethiopia
        //FJ  Fiji
        //FM  Micronesia (Federated States of)
        //GA  Gabon
        //GD  Grenada
        //GH  Ghana
        //GM  Gambia
        //GN  Guinea
        //GQ  Equatorial Guinea
        //GT  Guatemala
        //GW  Guinea-Bissau
        //GY  Guyana
        //HN  Honduras
        //HR  Croatia
        //HT  Haiti
        //KG  Kyrgyzstan
        //KH  Cambodia
        //KI  Kiribati
        //KM  Comoros
        //KN  Saint Kitts and Nevis
        //KP  Democratic People's Republic of Korea
        //KR  Republic of Korea
        //KW  Kuwait
        //KZ  Kazakhstan
        //LA  Lao People's Democratic Republic
        //LB  Lebanon
        //LR  Liberia
        //LS  Lesotho
        //MA  Morocco
        //MG  Madagascar
        //MH  Marshall Islands
        //MK  The former Yugoslav Republic of Macedonia
        //ML  Mali
        //MM  Myanmar
        //MN  Mongolia
        //MR  Mauritania
        //MV  Maldives
        //MW  Malawi
        //MY  Malaysia
        //MZ  Mozambique
        //NA  Namibia
        //NE  Niger
        //NI  Nicaragua
        //NP  Nepal
        //NR  Nauru
        //OM  Oman
        //PA  Panama
        //PE  Peru
        //PG  Papua New Guinea
        //PH  Philippines
        //PK  Pakistan
        //PW  Palau
        //PY  Paraguay
        //QA  Qatar
        //RS  Serbia
        //RW  Rwanda
        //SA  Saudi Arabia
        //SB  Solomon Islands
        //SC  Seychelles
        //SD  Sudan
        //SG  Singapore
        //SI  Slovenia
        //SK  Slovakia
        //SL  Sierra Leone
        //SN  Senegal
        //SO  Somalia
        //SR  Suriname
        //SS  South Sudan
        //ST  Sao Tome and Principe
        //SV  El Salvador
        //SY  Syrian Arab Republic
        //SZ  Swaziland
        //TD  Chad
        //TG  Togo
        //TH  Thailand
        //TJ  Tajikistan
        //TL  Timor-Leste
        //TM  Turkmenistan
        //TN  Tunisia
        //TO  Tonga
        //TT  Trinidad and Tobago
        //TV  Tuvalu
        //TZ  United Republic of Tanzania
        //UG  Uganda
        //UY  Uruguay
        //UZ  Uzbekistan
        //VC  Saint Vincent and the Grenadines
        //VE  Venezuela (Bolivarian Republic of)
        //VN  Viet Nam
        //VU  Vanuatu
        //WS  Samoa
        //YE  Yemen
        //ZM  Zambia

        private static readonly ISet<string> KNOWN_COUNTRIES = new HashSet<string>(new string[] {
            SCOTLAND, ENGLAND, CANADA, UNITED_STATES, WALES, IRELAND, UNITED_KINGDOM, NEW_ZEALAND, AUSTRALIA, INDIA, FRANCE, GERMANY,
            ITALY, SPAIN, BELGIUM, SOUTH_AFRICA, NORTHERN_IRELAND, EGYPT, HUNGARY, MALTA, DENMARK, SWEDEN, NORWAY, FINLAND, ICELAND,
            SWITZERLAND, AUSTRIA, NETHERLANDS, CHINA, ZIMBABWE, JAPAN, RUSSIA, POLAND, ST_LUCIA, LUXEMBOURG, ISLE_OF_MAN, GREECE,
            LIBYA, NIGERIA, BULGARIA, CYPRUS, ESTONIA, LATVIA, LIECHTENSTIEN, LITHUANIA, ALBANIA, ARMENIA, ANDORRA,
            AZERBAIJAN, BELARUS, MOLDOVA, MONACO, MONTENEGRO, PORTUGAL, ROMANIA, SAN_MARINO, TURKEY, UKRAINE, BRAZIL, MAURITIUS,
            UAE, AFGHANISTAN, ARGENTINA, BARBADOS, BANGLADESH, BAHAMAS, SRI_LANKA, CUBA, INDONESIA, ISRAEL, IRAN, IRAQ, JORDAN,
            JAMAICA, KENYA, MEXICO
        });

        private static readonly ISet<string> UK_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, NORTHERN_IRELAND, ISLE_OF_MAN
        });

        private static readonly ISet<string> CENSUS_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, UNITED_STATES, CANADA, ISLE_OF_MAN
        });

        private static Dictionary<string, Envelope> BOUNDING_BOXES;
        private static Envelope WHOLE_WORLD = new Envelope(-180, 180, -90, 90);

        static Countries()
        {   // generate position at http://imeasuremap.com/?e=57.4552937099324,-4.98779296874996:0::rectangle:0
            BOUNDING_BOXES = new Dictionary<string, Envelope>();
            BOUNDING_BOXES.Add(SCOTLAND, new Envelope(-7.974074, -0.463426, 54.571547, 60.970872));
            BOUNDING_BOXES.Add(ENGLAND, new Envelope(-6.523879, 1.879409, 49.814376, 55.865022));
            BOUNDING_BOXES.Add(WALES, new Envelope(-5.561202, -2.596147, 51.296580, 53.450153));
            BOUNDING_BOXES.Add(IRELAND, new Envelope(-10.746749, -5.298783, 51.296580, 55.467681));
            BOUNDING_BOXES.Add(NORTHERN_IRELAND, new Envelope(-8.329757, -5.298783, 53.872250, 55.467681));
            BOUNDING_BOXES.Add(CANADA, new Envelope(-141, -52, 41.129387, 83.232810));
            BOUNDING_BOXES.Add(UNITED_STATES, new Envelope(-169.136641, -66.086137, 17.665423, 71.626319));
            BOUNDING_BOXES.Add(AUSTRALIA, new Envelope(112.728595, 154.343553, -44.134565, -9.219173));
            BOUNDING_BOXES.Add(NEW_ZEALAND, new Envelope(166.199058, 178.689262, -47.405457, -34.187216));
            BOUNDING_BOXES.Add(FRANCE, new Envelope(-5.231600, 8.357236, 42.237011, 51.173873));
            BOUNDING_BOXES.Add(BELGIUM, new Envelope(2.436859, 6.533508, 49.389841, 51.530658));
            BOUNDING_BOXES.Add(NETHERLANDS, new Envelope(3.205904, 7.324528, 50.886015, 53.756544));
            BOUNDING_BOXES.Add(GERMANY, new Envelope(5.732761, 15.212713, 47.126544, 55.048204));
            BOUNDING_BOXES.Add(SPAIN, new Envelope(-9.428367, 4.709787, 35.867189, 43.875768));
            BOUNDING_BOXES.Add(PORTUGAL, new Envelope(-17.360492, -6.100757, 32.487006, 42.254591));
            BOUNDING_BOXES.Add(ITALY, new Envelope(6.523787, 18.662428, 36.523271, 47.168847));
            BOUNDING_BOXES.Add(MEXICO, new Envelope(-117.314102, -86.630537, 14.216935, 32.927605));
        }

        public static bool IsUnitedKingdom(string country)
        {
            return UK_COUNTRIES.Contains(country);
        }

        public static bool IsKnownCountry(string country)
        {
            return KNOWN_COUNTRIES.Contains(country);
        }

        public static bool IsCensusCountry(string country)
        {
            return CENSUS_COUNTRIES.Contains(country);
        }

        public static bool IsEnglandWales(string country)
        {
            return country.Equals(ENG_WALES) || country.Equals(ENGLAND) || country.Equals(WALES) || country.Equals(ISLE_OF_MAN);
        }

        public static Envelope BoundingBox(string country)
        {
            if (BOUNDING_BOXES.ContainsKey(country))
                return BOUNDING_BOXES[country];
            else
                return WHOLE_WORLD;
        }
    }
}
