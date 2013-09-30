using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            JAMAICA = "Jamaica", KENYA = "Kenya";

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
            //MT  Malta
            //MV  Maldives
            //MW  Malawi
            //MX  Mexico
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
            JAMAICA, KENYA
        });

        private static readonly ISet<string> UK_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, NORTHERN_IRELAND
        });

        private static readonly ISet<string> CENSUS_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, UNITED_STATES, CANADA
        });

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
    }
}
