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
            CHINA = "China", CEYLON = "Ceylon", ZIMBABWE = "Zimbabwe", JAPAN = "Japan", RUSSIA = "Russia", POLAND = "Poland", 
            LUXEMBOURG = "Luxembourg", ISLE_OF_MAN="Isle of Man", GREECE = "Greece", LIBYA = "Libya", NIGERIA = "Nigeria", 
            BULGARIA = "Bulgaria", CYPRUS = "Cyprus", ESTONIA = "Estonia", GEORGIA = "Georgia", LATVIA = "Latvia", 
            LIECHTENSTIEN = "Liechtenstien", LITHUANIA = "Lithuania", ALBANIA = "Albania", ANDORRA = "Andorra", ARMENIA = "Armenia",
            AZERBAIJAN = "Azerbaijan", BELARUS = "Belarus", MACEDONIA = "Macedonia", MOLDOVA = "Moldova", MONACO = "Monaco", 
            MONTENEGRO = "Montenegro", PORTUGAL = "Portugal", ROMANIA ="Romania", SAN_MARINO = "San Marino", TURKEY = "Turkey",
            UKRAINE = "Ukraine", BRAZIL = "Brazil";

            //AE  United Arab Emirates
            //AF  Afghanistan
            //AG  Antigua and Barbuda
            //AL  Albania
            //AO  Angola
            //AR  Argentina
            //BA  Bosnia and Herzegovina
            //BB  Barbados
            //BD  Bangladesh
            //BF  Burkina Faso
            //BH  Bahrain
            //BI  Burundi
            //BJ  Benin
            //BN  Brunei Darussalam
            //BO  Bolivia (Plurinational State of)
            //BS  Bahamas
            //BT  Bhutan
            //BW  Botswana
            //BY  Belarus
            //BZ  Belize
            //CD  Democratic Republic of the Congo
            //CF  Central African Republic
            //CG  Congo
            //CI  Côte d'Ivoire
            //CL  Chile
            //CM  Cameroon
            //CO  Colombia
            //CR  Costa Rica
            //CU  Cuba
            //CV  Cape Verde
            //CZ  Czech Republic
            //DJ  Djibouti
            //DM  Dominica
            //DO  Dominican Republic
            //DZ  Algeria
            //EC  Ecuador
            //EE  Estonia
            //ER  Eritrea
            //ET  Ethiopia
            //FJ  Fiji
            //FM  Micronesia (Federated States of)
            //GA  Gabon
            //GD  Grenada
            //GE  Georgia
            //GH  Ghana
            //GM  Gambia
            //GN  Guinea
            //GQ  Equatorial Guinea
            //GR  Greece
            //GT  Guatemala
            //GW  Guinea-Bissau
            //GY  Guyana
            //HN  Honduras
            //HR  Croatia
            //HT  Haiti
            //HU  Hungary
            //ID  Indonesia
            //IE  Ireland
            //IL  Israel
            //IN  India
            //IQ  Iraq
            //IR  Iran (Islamic Republic of)
            //IS  Iceland
            //IT  Italy
            //JM  Jamaica
            //JO  Jordan
            //JP  Japan
            //KE  Kenya
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
            //LC  Saint Lucia
            //LI  Liechtenstein
            //LK  Sri Lanka
            //LR  Liberia
            //LS  Lesotho
            //LT  Lithuania
            //LU  Luxembourg
            //LV  Latvia
            //LY  Libyan Arab Jamahiriya
            //MA  Morocco
            //MC  Monaco
            //MD  Republic of Moldova
            //ME  Montenegro
            //MG  Madagascar
            //MH  Marshall Islands
            //MK  The former Yugoslav Republic of Macedonia
            //ML  Mali
            //MM  Myanmar
            //MN  Mongolia
            //MR  Mauritania
            //MT  Malta
            //MU  Mauritius
            //MV  Maldives
            //MW  Malawi
            //MX  Mexico
            //MY  Malaysia
            //MZ  Mozambique
            //NA  Namibia
            //NE  Niger
            //NG  Nigeria
            //NI  Nicaragua
            //NP  Nepal
            //NR  Nauru
            //OM  Oman
            //PA  Panama
            //PE  Peru
            //PG  Papua New Guinea
            //PH  Philippines
            //PK  Pakistan
            //PL  Poland
            //PT  Portugal
            //PW  Palau
            //PY  Paraguay
            //QA  Qatar
            //RO  Romania
            //RS  Serbia
            //RU  Russian Federation
            //RW  Rwanda
            //SA  Saudi Arabia
            //SB  Solomon Islands
            //SC  Seychelles
            //SD  Sudan
            //SE  Sweden
            //SG  Singapore
            //SI  Slovenia
            //SK  Slovakia
            //SL  Sierra Leone
            //SM  San Marino
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
            //TR  Turkey
            //TT  Trinidad and Tobago
            //TV  Tuvalu
            //TZ  United Republic of Tanzania
            //UA  Ukraine
            //UG  Uganda
            //US  United States of America
            //UY  Uruguay
            //UZ  Uzbekistan
            //VC  Saint Vincent and the Grenadines
            //VE  Venezuela (Bolivarian Republic of)
            //VN  Viet Nam
            //VU  Vanuatu
            //WS  Samoa
            //YE  Yemen
            //ZA  South Africa
            //ZM  Zambia
            //ZW  Zimbabwe

        private static readonly ISet<string> KNOWN_COUNTRIES = new HashSet<string>(new string[] {
            SCOTLAND, ENGLAND, CANADA, UNITED_STATES, WALES, IRELAND, UNITED_KINGDOM, NEW_ZEALAND, AUSTRALIA, INDIA, FRANCE, GERMANY,
            ITALY, SPAIN, BELGIUM, SOUTH_AFRICA, NORTHERN_IRELAND, EGYPT, HUNGARY, MALTA, DENMARK, SWEDEN, NORWAY, FINLAND, ICELAND,
            SWITZERLAND, AUSTRIA, NETHERLANDS, CHINA, CEYLON, ZIMBABWE, JAPAN, RUSSIA, POLAND, LUXEMBOURG, ISLE_OF_MAN, GREECE,
            LIBYA, NIGERIA, BULGARIA, CYPRUS, ESTONIA, GEORGIA, LATVIA, LIECHTENSTIEN, LITHUANIA, ALBANIA, ARMENIA, ANDORRA,
            AZERBAIJAN, BELARUS,MOLDOVA, MONACO, MONTENEGRO, PORTUGAL, ROMANIA, SAN_MARINO, TURKEY, UKRAINE
        });

        private static readonly ISet<string> UK_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, NORTHERN_IRELAND
        });

        private static readonly ISet<string> CENSUS_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, UNITED_STATES, CANADA
        });

        public static FactLocation FactLocation(string country)
        {
            FactLocation location;
            if (!locationCache.TryGetValue(country, out location))
            {
                location = new FactLocation(country);
                locationCache.Add(country, location);
            }
            return location;
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
    }
}
