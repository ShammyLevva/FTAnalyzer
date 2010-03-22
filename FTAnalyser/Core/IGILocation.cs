using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class IGILocation : ICloneable
    {
         
        private FactLocation location;
        private int level;
        private string juris1, juris2, region;

        private static Dictionary<string, IGILocation> adaptors;

        public static IGILocation Adapt(FactLocation location, int level)
        {
            IGILocation adaptor = null;
            if (level == FactLocation.COUNTRY)
            {
                adaptors.TryGetValue(location.Country, out adaptor);
            }
            else if (level == FactLocation.REGION)
            {
                adaptors.TryGetValue(location.RegionID, out adaptor);
                if(adaptor == null) // we have no region so just use country
                    adaptors.TryGetValue(location.Country, out adaptor);
            }
            if (adaptor == null)
            {
                // TODO: Throw error or something it shouldn't be null
                Console.WriteLine("Failed to process " + location.ToString() + " at level " + level);
            }
            return adaptor;
        }

        static IGILocation() {
            adaptors = new Dictionary<string, IGILocation>();

            // British Isles
            adaptors.Add("Channel Islands",     new IGILocation(FactLocation.COUNTRY, "2", "ChaI", ""));
            adaptors.Add("England",             new IGILocation(FactLocation.COUNTRY, "2", "Engl", ""));
            adaptors.Add("Ireland",             new IGILocation(FactLocation.COUNTRY, "2", "Irel",  ""));
            adaptors.Add("Isle of Man",         new IGILocation(FactLocation.COUNTRY, "2", "IMan", ""));
            adaptors.Add("Scotland",            new IGILocation(FactLocation.COUNTRY, "2", "Scot", ""));
            adaptors.Add("Wales",               new IGILocation(FactLocation.COUNTRY, "2", "Wale", ""));

            // North America
            adaptors.Add("Canada",              new IGILocation(FactLocation.COUNTRY, "11", "CAN", ""));
            adaptors.Add("United States",       new IGILocation(FactLocation.COUNTRY, "11", "US", ""));

            // The Name used by the country adaptor is the IGIName for that area
            // English counties
            adaptors.Add("Bedford",             new IGILocation(FactLocation.REGION, "2", "Engl", "Bedf"));
            adaptors.Add("Berkshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Berk"));
            adaptors.Add("Buckingham",          new IGILocation(FactLocation.REGION, "2", "Engl", "Buck"));
            adaptors.Add("Cambridge",           new IGILocation(FactLocation.REGION, "2", "Engl", "Camb"));
            adaptors.Add("Cheshire",            new IGILocation(FactLocation.REGION, "2", "Engl", "Ches"));
            adaptors.Add("Cornwall",            new IGILocation(FactLocation.REGION, "2", "Engl", "Corn"));
            adaptors.Add("Cumberland",          new IGILocation(FactLocation.REGION, "2", "Engl", "Cumb"));
            adaptors.Add("Cumbria",             new IGILocation(FactLocation.REGION, "2", "Engl", "Cumr"));
            adaptors.Add("Derby",               new IGILocation(FactLocation.REGION, "2", "Engl", "Derb"));
            adaptors.Add("Devon",               new IGILocation(FactLocation.REGION, "2", "Engl", "Devo"));
            adaptors.Add("Dorset",              new IGILocation(FactLocation.REGION, "2", "Engl", "Dors"));
            adaptors.Add("Durham",              new IGILocation(FactLocation.REGION, "2", "Engl", "Durh"));
            adaptors.Add("Essex",               new IGILocation(FactLocation.REGION, "2", "Engl", "Esse"));
            adaptors.Add("Gloucester",          new IGILocation(FactLocation.REGION, "2", "Engl", "Glou"));
            adaptors.Add("Hampshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Hamp"));
            adaptors.Add("Hereford",            new IGILocation(FactLocation.REGION, "2", "Engl", "Here"));
            adaptors.Add("Hertford",            new IGILocation(FactLocation.REGION, "2", "Engl", "Hert"));
            adaptors.Add("Huntingdon",          new IGILocation(FactLocation.REGION, "2", "Engl", "Hunt"));
            adaptors.Add("Kent",                new IGILocation(FactLocation.REGION, "2", "Engl", "Kent"));
            adaptors.Add("Lancashire",          new IGILocation(FactLocation.REGION, "2", "Engl", "Lanc"));
            adaptors.Add("Leicester",           new IGILocation(FactLocation.REGION, "2", "Engl", "Leic"));
            adaptors.Add("Lincoln",             new IGILocation(FactLocation.REGION, "2", "Engl", "Linc"));
            adaptors.Add("Middlesex",           new IGILocation(FactLocation.REGION, "2", "Engl", "Lond"));
            adaptors.Add("Monmouth",            new IGILocation(FactLocation.REGION, "2", "Engl", "Monm"));
            adaptors.Add("Norfolk",             new IGILocation(FactLocation.REGION, "2", "Engl", "Norf"));
            adaptors.Add("Northampton",         new IGILocation(FactLocation.REGION, "2", "Engl", "Nham"));
            adaptors.Add("Northumberland",      new IGILocation(FactLocation.REGION, "2", "Engl", "Nthu"));
            adaptors.Add("Nottingham",          new IGILocation(FactLocation.REGION, "2", "Engl", "NOtt"));
            adaptors.Add("Oxford",              new IGILocation(FactLocation.REGION, "2", "Engl", "Oxfo"));
            adaptors.Add("Rutland",             new IGILocation(FactLocation.REGION, "2", "Engl", "Rutl"));
            adaptors.Add("Shropshire",          new IGILocation(FactLocation.REGION, "2", "Engl", "Shro"));
            adaptors.Add("Somerset",            new IGILocation(FactLocation.REGION, "2", "Engl", "Som"));
            adaptors.Add("Staffordshire",       new IGILocation(FactLocation.REGION, "2", "Engl", "Staf"));
            adaptors.Add("Suffolk",             new IGILocation(FactLocation.REGION, "2", "Engl", "Suff"));
            adaptors.Add("Sussex",              new IGILocation(FactLocation.REGION, "2", "Engl", "Suss"));
            adaptors.Add("Surrey",              new IGILocation(FactLocation.REGION, "2", "Engl", "Surr"));
            adaptors.Add("Warwick",             new IGILocation(FactLocation.REGION, "2", "Engl", "Warw"));
            adaptors.Add("Westmorland",         new IGILocation(FactLocation.REGION, "2", "Engl", "Wmor"));
            adaptors.Add("Wiltshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Wilt"));
            adaptors.Add("Worcester",           new IGILocation(FactLocation.REGION, "2", "Engl", "Worc"));
            adaptors.Add("Yorkshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "York"));

            // Scottish counties
            adaptors.Add("Aberdeen",            new IGILocation(FactLocation.REGION, "2", "Scot", "Aber"));
            adaptors.Add("Angus",               new IGILocation(FactLocation.REGION, "2", "Scot", "Angu"));
            adaptors.Add("Argyll",              new IGILocation(FactLocation.REGION, "2", "Scot", "Argy"));
            adaptors.Add("Ayr",                 new IGILocation(FactLocation.REGION, "2", "Scot", "Ayr"));
            adaptors.Add("Banff",               new IGILocation(FactLocation.REGION, "2", "Scot", "Banf"));
            adaptors.Add("Berwick",             new IGILocation(FactLocation.REGION, "2", "Scot", "Berw"));
            adaptors.Add("Bute",                new IGILocation(FactLocation.REGION, "2", "Scot", "Bute"));
            adaptors.Add("Caithness",           new IGILocation(FactLocation.REGION, "2", "Scot", "Cait"));
            adaptors.Add("Clackmannan",         new IGILocation(FactLocation.REGION, "2", "Scot", "Clac"));
            adaptors.Add("Dumfries",            new IGILocation(FactLocation.REGION, "2", "Scot", "Dumf"));
            adaptors.Add("Dunbarton",           new IGILocation(FactLocation.REGION, "2", "Scot", "Dunb"));
            adaptors.Add("East Lothian",        new IGILocation(FactLocation.REGION, "2", "Scot", "ELot"));
            adaptors.Add("Fife",                new IGILocation(FactLocation.REGION, "2", "Scot", "Fife"));
            adaptors.Add("Inverness",           new IGILocation(FactLocation.REGION, "2", "Scot", "Inve"));
            adaptors.Add("Kincardine",          new IGILocation(FactLocation.REGION, "2", "Scot", "Kinc"));
            adaptors.Add("Kinross",             new IGILocation(FactLocation.REGION, "2", "Scot", "Kinr"));
            adaptors.Add("Kirkcudbright",       new IGILocation(FactLocation.REGION, "2", "Scot", "Kirk"));
            adaptors.Add("Lanark",              new IGILocation(FactLocation.REGION, "2", "Scot", "Lana"));
            adaptors.Add("Midlothian",          new IGILocation(FactLocation.REGION, "2", "Scot", "Mlot"));
            adaptors.Add("Moray",               new IGILocation(FactLocation.REGION, "2", "Scot", "Mora"));
            adaptors.Add("Nairn",               new IGILocation(FactLocation.REGION, "2", "Scot", "Nair"));
            adaptors.Add("Orkney",              new IGILocation(FactLocation.REGION, "2", "Scot", "Orkn"));
            adaptors.Add("Peebles",             new IGILocation(FactLocation.REGION, "2", "Scot", "Peeb"));
            adaptors.Add("Perth",               new IGILocation(FactLocation.REGION, "2", "Scot", "Pert"));
            adaptors.Add("Renfrew",             new IGILocation(FactLocation.REGION, "2", "Scot", "Renf"));
            adaptors.Add("Ross and Cromarty",   new IGILocation(FactLocation.REGION, "2", "Scot", "RoCr"));
            adaptors.Add("Roxburgh",            new IGILocation(FactLocation.REGION, "2", "Scot", "Roxb"));
            adaptors.Add("Selkirk",             new IGILocation(FactLocation.REGION, "2", "Scot", "Selk"));
            adaptors.Add("Shetland",            new IGILocation(FactLocation.REGION, "2", "Scot", "Shet"));
            adaptors.Add("Stirling",            new IGILocation(FactLocation.REGION, "2", "Scot", "Stir"));
            adaptors.Add("Sutherland",          new IGILocation(FactLocation.REGION, "2", "Scot", "Suth"));
            adaptors.Add("West Lothian",        new IGILocation(FactLocation.REGION, "2", "Scot", "WLot"));
            adaptors.Add("Wigton",              new IGILocation(FactLocation.REGION, "2", "Scot", "Wigt"));

            // Welsh Counties
            adaptors.Add("Anglesey",            new IGILocation(FactLocation.REGION, "2", "Wale", "Angl"));
            adaptors.Add("Brecon",              new IGILocation(FactLocation.REGION, "2", "Wale", "Brec"));
            adaptors.Add("Caernarvon",          new IGILocation(FactLocation.REGION, "2", "Wale", "Caer"));
            adaptors.Add("Cardigan",            new IGILocation(FactLocation.REGION, "2", "Wale", "Card"));
            adaptors.Add("Carmarthen",          new IGILocation(FactLocation.REGION, "2", "Wale", "Carm"));
            adaptors.Add("Denbigh",             new IGILocation(FactLocation.REGION, "2", "Wale", "Denb"));
            adaptors.Add("Flint",               new IGILocation(FactLocation.REGION, "2", "Wale", "Flin"));
            adaptors.Add("Glamorgan",           new IGILocation(FactLocation.REGION, "2", "Wale", "Glam"));
            adaptors.Add("Gwynedd",             new IGILocation(FactLocation.REGION, "2", "Wale", "Gwyn"));
            adaptors.Add("Merioneth",           new IGILocation(FactLocation.REGION, "2", "Wale", "Meri"));
            adaptors.Add("Mid-Glamorgan",       new IGILocation(FactLocation.REGION, "2", "Wale", "MGla"));
            adaptors.Add("Montgomery",          new IGILocation(FactLocation.REGION, "2", "Wale", "Mntg"));
            adaptors.Add("Pembroke",            new IGILocation(FactLocation.REGION, "2", "Wale", "Pemb"));
            adaptors.Add("Radnor",              new IGILocation(FactLocation.REGION, "2", "Wale", "Radn"));

            //Irish Counties
            adaptors.Add("Antrim",      new IGILocation(FactLocation.REGION, "2", "Irel", "Antr"));
            adaptors.Add("Armagh",      new IGILocation(FactLocation.REGION, "2", "Irel", "Arma"));
            adaptors.Add("Cavan",       new IGILocation(FactLocation.REGION, "2", "Irel", "Carl"));
            adaptors.Add("Carlow",      new IGILocation(FactLocation.REGION, "2", "Irel", "Cava")); 
            adaptors.Add("Clare",       new IGILocation(FactLocation.REGION, "2", "Irel", "Clar"));
	        adaptors.Add("Connaught",   new IGILocation(FactLocation.REGION, "2", "Irel", "ConP"));
            adaptors.Add("Cork",        new IGILocation(FactLocation.REGION, "2", "Irel", "Cork")); 
            adaptors.Add("Donegal",     new IGILocation(FactLocation.REGION, "2", "Irel", "Done")); 
            adaptors.Add("Down",        new IGILocation(FactLocation.REGION, "2", "Irel", "Down")); 
            adaptors.Add("Dublin",      new IGILocation(FactLocation.REGION, "2", "Irel", "Dubl"));
	        adaptors.Add("Fermanagh",   new IGILocation(FactLocation.REGION, "2", "Irel", "Ferm")); 
            adaptors.Add("Galway",      new IGILocation(FactLocation.REGION, "2", "Irel", "Galw")); 
            adaptors.Add("Kerry",       new IGILocation(FactLocation.REGION, "2", "Irel", "Kerr")); 
            adaptors.Add("Kildare",     new IGILocation(FactLocation.REGION, "2", "Irel", "Kild")); 
            adaptors.Add("Kilkenny",    new IGILocation(FactLocation.REGION, "2", "Irel", "Kilk"));
	        adaptors.Add("Laoighis",    new IGILocation(FactLocation.REGION, "2", "Irel", "Laoi")); 
            adaptors.Add("Leinster",    new IGILocation(FactLocation.REGION, "2", "Irel", "LeiP")); 
            adaptors.Add("Leitrim",     new IGILocation(FactLocation.REGION, "2", "Irel", "Leit")); 
            adaptors.Add("Limerick",    new IGILocation(FactLocation.REGION, "2", "Irel", "Lime")); 
            adaptors.Add("Londonderry", new IGILocation(FactLocation.REGION, "2", "Irel", "Ldry"));
	        adaptors.Add("Longford",    new IGILocation(FactLocation.REGION, "2", "Irel", "Long")); 
            adaptors.Add("Louth",       new IGILocation(FactLocation.REGION, "2", "Irel", "Lout")); 
            adaptors.Add("Mayo",        new IGILocation(FactLocation.REGION, "2", "Irel", "Mayo")); 
            adaptors.Add("Meath",       new IGILocation(FactLocation.REGION, "2", "Irel", "Meat")); 
            adaptors.Add("Monaghan",    new IGILocation(FactLocation.REGION, "2", "Irel", "Mghn"));
	        adaptors.Add("Munster",     new IGILocation(FactLocation.REGION, "2", "Irel", "MunP")); 
            adaptors.Add("Offaly",      new IGILocation(FactLocation.REGION, "2", "Irel", "Offa")); 
            adaptors.Add("Roscommon",   new IGILocation(FactLocation.REGION, "2", "Irel", "Rosc")); 
            adaptors.Add("Sligo",       new IGILocation(FactLocation.REGION, "2", "Irel", "Slig")); 
            adaptors.Add("Tipperary",   new IGILocation(FactLocation.REGION, "2", "Irel", "Tipp"));
	        adaptors.Add("Tyrone",      new IGILocation(FactLocation.REGION, "2", "Irel", "Tyro")); 
            adaptors.Add("Ulster",      new IGILocation(FactLocation.REGION, "2", "Irel", "UlsP")); 
            adaptors.Add("Waterford",   new IGILocation(FactLocation.REGION, "2", "Irel", "Wat")); 
            adaptors.Add("Westmeath",   new IGILocation(FactLocation.REGION, "2", "Irel", "Wmea")); 
            adaptors.Add("Wexford",     new IGILocation(FactLocation.REGION, "2", "Irel", "Wexf"));
	        adaptors.Add("Wicklow",     new IGILocation(FactLocation.REGION, "2", "Irel", "Wick"));

            // Canadian Provinces
            adaptors.Add("Alberta",                 new IGILocation(FactLocation.REGION, "11", "CAN", "Alta"));
            adaptors.Add("British Columbia",        new IGILocation(FactLocation.REGION, "11", "CAN", "BrCo"));
            adaptors.Add("Manitoba",                new IGILocation(FactLocation.REGION, "11", "CAN", "Mani"));
            adaptors.Add("New Brunswick",           new IGILocation(FactLocation.REGION, "11", "CAN", "NBru"));
            adaptors.Add("Newfoundland",            new IGILocation(FactLocation.REGION, "11", "CAN", "Newf"));
            adaptors.Add("Northwest Territories",   new IGILocation(FactLocation.REGION, "11", "CAN", "NWT"));
            adaptors.Add("Nova Scotia",             new IGILocation(FactLocation.REGION, "11", "CAN", "NSco"));
            adaptors.Add("Ontario",                 new IGILocation(FactLocation.REGION, "11", "CAN", "Ont"));
            adaptors.Add("Prince Edward Island",    new IGILocation(FactLocation.REGION, "11", "CAN", "PEI"));
            adaptors.Add("Quebec",                  new IGILocation(FactLocation.REGION, "11", "CAN", "Queb"));
            adaptors.Add("Saskatchewan",            new IGILocation(FactLocation.REGION, "11", "CAN", "Sask"));
            adaptors.Add("Yukon",                   new IGILocation(FactLocation.REGION, "11", "CAN", "YukT"));

            // US States
            adaptors.Add("Alabama",                 new IGILocation(FactLocation.REGION, "11", "US", "Alab"));
            adaptors.Add("Alaska",                  new IGILocation(FactLocation.REGION, "11", "US", "Alas"));
            adaptors.Add("Arizona",                 new IGILocation(FactLocation.REGION, "11", "US", "Ariz"));
            adaptors.Add("Arkansas",                new IGILocation(FactLocation.REGION, "11", "US", "Ark"));
            adaptors.Add("California",              new IGILocation(FactLocation.REGION, "11", "US", "Cal"));
            adaptors.Add("Colorado",                new IGILocation(FactLocation.REGION, "11", "US", "Colr"));
            adaptors.Add("Connecticut",             new IGILocation(FactLocation.REGION, "11", "US", "Conn"));
            adaptors.Add("Delaware",                new IGILocation(FactLocation.REGION, "11", "US", "Dela"));
            adaptors.Add("District of Columbia",    new IGILocation(FactLocation.REGION, "11", "US", "DiCo"));
            adaptors.Add("Florida",                 new IGILocation(FactLocation.REGION, "11", "US", "Flor"));
            adaptors.Add("Georgia",                 new IGILocation(FactLocation.REGION, "11", "US", "Geor"));
            adaptors.Add("Hawaii",                  new IGILocation(FactLocation.REGION, "11", "US", "Hawa"));
            adaptors.Add("Idaho",                   new IGILocation(FactLocation.REGION, "11", "US", "Idah"));
            adaptors.Add("Illinois",                new IGILocation(FactLocation.REGION, "11", "US", "Ill"));
            adaptors.Add("Indiana",                 new IGILocation(FactLocation.REGION, "11", "US", "Indn"));
            adaptors.Add("Iowa",                    new IGILocation(FactLocation.REGION, "11", "US", "Iowa"));
            adaptors.Add("Kansas",                  new IGILocation(FactLocation.REGION, "11", "US", "Kan"));
            adaptors.Add("Kentucky",                new IGILocation(FactLocation.REGION, "11", "US", "Ktky"));
            adaptors.Add("Louisiana",               new IGILocation(FactLocation.REGION, "11", "US", "Lou"));
            adaptors.Add("Maine",                   new IGILocation(FactLocation.REGION, "11", "US", "Main"));
            adaptors.Add("Maryland",                new IGILocation(FactLocation.REGION, "11", "US", "Mary"));
            adaptors.Add("Massachusetts",           new IGILocation(FactLocation.REGION, "11", "US", "Mass"));
            adaptors.Add("Michigan",                new IGILocation(FactLocation.REGION, "11", "US", "Mchi"));
            adaptors.Add("Minnesota",               new IGILocation(FactLocation.REGION, "11", "US", "Minn"));
            adaptors.Add("Mississippi",             new IGILocation(FactLocation.REGION, "11", "US", "Misp"));
            adaptors.Add("Missouri",                new IGILocation(FactLocation.REGION, "11", "US", "Miso"));
            adaptors.Add("Montana",                 new IGILocation(FactLocation.REGION, "11", "US", "Mont"));
            adaptors.Add("Nebraska",                new IGILocation(FactLocation.REGION, "11", "US", "Nebr"));
            adaptors.Add("Nevada",                  new IGILocation(FactLocation.REGION, "11", "US", "Nev"));
            adaptors.Add("New Hampshire",           new IGILocation(FactLocation.REGION, "11", "US", "NHam"));
            adaptors.Add("New Jersey",              new IGILocation(FactLocation.REGION, "11", "US", "NJer"));
            adaptors.Add("New Mexico",              new IGILocation(FactLocation.REGION, "11", "US", "NMex"));
            adaptors.Add("New York",                new IGILocation(FactLocation.REGION, "11", "US", "NYor"));
            adaptors.Add("North Carolina",          new IGILocation(FactLocation.REGION, "11", "US", "NCar"));
            adaptors.Add("North Dakota",            new IGILocation(FactLocation.REGION, "11", "US", "NDak"));
            adaptors.Add("Ohio",                    new IGILocation(FactLocation.REGION, "11", "US", "Ohio"));
            adaptors.Add("Oklahoma",                new IGILocation(FactLocation.REGION, "11", "US", "Okla"));
            adaptors.Add("Oregon",                  new IGILocation(FactLocation.REGION, "11", "US", "Oreg"));
            adaptors.Add("Pennsylvania",            new IGILocation(FactLocation.REGION, "11", "US", "Penn"));
            adaptors.Add("Rhode Island",            new IGILocation(FactLocation.REGION, "11", "US", "RhoI"));
            adaptors.Add("South Carolina",          new IGILocation(FactLocation.REGION, "11", "US", "SCar"));
            adaptors.Add("South Dakota",            new IGILocation(FactLocation.REGION, "11", "US", "SDak"));
            adaptors.Add("Tennessee",               new IGILocation(FactLocation.REGION, "11", "US", "Tenn"));
            adaptors.Add("Texas",                   new IGILocation(FactLocation.REGION, "11", "US", "Tex"));
            adaptors.Add("Utah",                    new IGILocation(FactLocation.REGION, "11", "US", "Utah"));
            adaptors.Add("Vermont",                 new IGILocation(FactLocation.REGION, "11", "US", "Verm"));
            adaptors.Add("Virginia",                new IGILocation(FactLocation.REGION, "11", "US", "Virg"));
            adaptors.Add("Washington",              new IGILocation(FactLocation.REGION, "11", "US", "Wash"));
            adaptors.Add("West Virginia",           new IGILocation(FactLocation.REGION, "11", "US", "WVir"));
            adaptors.Add("Wisconsin",               new IGILocation(FactLocation.REGION, "11", "US", "Wisc"));
            adaptors.Add("Wyoming",                 new IGILocation(FactLocation.REGION, "11", "US", "Wyom"));
        }

        private IGILocation(int level, string region, string juris1, string juris2)
        {
            this.level = level;
            this.region = region;
            this.juris1 = juris1;
            this.juris2 = juris2;
            this.location = null;
        }

        #region Properties

        public int Level
        {
            get { return level; }
        }

        public string Region
        {
            get { return region; }
        }

        public string Juris1
        {
            get { return juris1; }
        }

        public string Juris2 {
            get { return juris2; }
        }

        private FactLocation Location
        {
            get { return location; }

            set { this.location = value; }
        }

        #endregion

        public object Clone()
        {
            return new IGILocation(this.level, this.region, this.juris1, this.juris2);
        }

    }
}
