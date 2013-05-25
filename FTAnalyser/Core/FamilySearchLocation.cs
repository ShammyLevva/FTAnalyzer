using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FamilySearchLocation : ICloneable
    {
         
        private FactLocation location;
        private int level;
        private string juris1, juris2, region;

        private static Dictionary<string, FamilySearchLocation> adaptors;

        public static FamilySearchLocation Adapt(FactLocation location, int level)
        {
            FamilySearchLocation adaptor = null;
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

        static FamilySearchLocation() {
            adaptors = new Dictionary<string, FamilySearchLocation>();

            // British Isles
            adaptors.Add("Channel Islands",     new FamilySearchLocation(FactLocation.COUNTRY, "2", "ChaI", ""));
            adaptors.Add("England",             new FamilySearchLocation(FactLocation.COUNTRY, "2", "Engl", ""));
            adaptors.Add("Ireland",             new FamilySearchLocation(FactLocation.COUNTRY, "2", "Irel",  ""));
            adaptors.Add("Isle of Man",         new FamilySearchLocation(FactLocation.COUNTRY, "2", "IMan", ""));
            adaptors.Add("Scotland",            new FamilySearchLocation(FactLocation.COUNTRY, "2", "Scot", ""));
            adaptors.Add("Wales",               new FamilySearchLocation(FactLocation.COUNTRY, "2", "Wale", ""));

            // North America
            adaptors.Add("Canada",              new FamilySearchLocation(FactLocation.COUNTRY, "11", "CAN", ""));
            adaptors.Add("United States",       new FamilySearchLocation(FactLocation.COUNTRY, "11", "US", ""));

            // The Name used by the country adaptor is the FamilySearchName for that area
            // English counties
            adaptors.Add("Bedford",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Bedf"));
            adaptors.Add("Berkshire",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Berk"));
            adaptors.Add("Buckingham",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Buck"));
            adaptors.Add("Cambridge",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Camb"));
            adaptors.Add("Cheshire",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Ches"));
            adaptors.Add("Cornwall",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Corn"));
            adaptors.Add("Cumberland",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Cumb"));
            adaptors.Add("Cumbria",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Cumr"));
            adaptors.Add("Derby",               new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Derb"));
            adaptors.Add("Devon",               new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Devo"));
            adaptors.Add("Dorset",              new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Dors"));
            adaptors.Add("Durham",              new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Durh"));
            adaptors.Add("Essex",               new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Esse"));
            adaptors.Add("Gloucester",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Glou"));
            adaptors.Add("Hampshire",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Hamp"));
            adaptors.Add("Hereford",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Here"));
            adaptors.Add("Hertford",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Hert"));
            adaptors.Add("Huntingdon",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Hunt"));
            adaptors.Add("Kent",                new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Kent"));
            adaptors.Add("Lancashire",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Lanc"));
            adaptors.Add("Leicester",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Leic"));
            adaptors.Add("Lincoln",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Linc"));
            adaptors.Add("Middlesex",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Lond"));
            adaptors.Add("Monmouth",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Monm"));
            adaptors.Add("Norfolk",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Norf"));
            adaptors.Add("Northampton",         new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Nham"));
            adaptors.Add("Northumberland",      new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Nthu"));
            adaptors.Add("Nottingham",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "NOtt"));
            adaptors.Add("Oxford",              new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Oxfo"));
            adaptors.Add("Rutland",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Rutl"));
            adaptors.Add("Shropshire",          new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Shro"));
            adaptors.Add("Somerset",            new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Som"));
            adaptors.Add("Staffordshire",       new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Staf"));
            adaptors.Add("Suffolk",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Suff"));
            adaptors.Add("Sussex",              new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Suss"));
            adaptors.Add("Surrey",              new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Surr"));
            adaptors.Add("Warwick",             new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Warw"));
            adaptors.Add("Westmorland",         new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Wmor"));
            adaptors.Add("Wiltshire",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Wilt"));
            adaptors.Add("Worcester",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "Worc"));
            adaptors.Add("Yorkshire",           new FamilySearchLocation(FactLocation.REGION, "2", "Engl", "York"));

            // Scottish counties
            adaptors.Add("Aberdeen",            new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Aber"));
            adaptors.Add("Angus",               new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Angu"));
            adaptors.Add("Argyll",              new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Argy"));
            adaptors.Add("Ayr",                 new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Ayr"));
            adaptors.Add("Banff",               new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Banf"));
            adaptors.Add("Berwick",             new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Berw"));
            adaptors.Add("Bute",                new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Bute"));
            adaptors.Add("Caithness",           new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Cait"));
            adaptors.Add("Clackmannan",         new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Clac"));
            adaptors.Add("Dumfries",            new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Dumf"));
            adaptors.Add("Dunbarton",           new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Dunb"));
            adaptors.Add("East Lothian",        new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "ELot"));
            adaptors.Add("Fife",                new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Fife"));
            adaptors.Add("Inverness",           new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Inve"));
            adaptors.Add("Kincardine",          new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Kinc"));
            adaptors.Add("Kinross",             new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Kinr"));
            adaptors.Add("Kirkcudbright",       new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Kirk"));
            adaptors.Add("Lanark",              new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Lana"));
            adaptors.Add("Midlothian",          new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Mlot"));
            adaptors.Add("Moray",               new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Mora"));
            adaptors.Add("Nairn",               new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Nair"));
            adaptors.Add("Orkney",              new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Orkn"));
            adaptors.Add("Peebles",             new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Peeb"));
            adaptors.Add("Perth",               new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Pert"));
            adaptors.Add("Renfrew",             new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Renf"));
            adaptors.Add("Ross and Cromarty",   new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "RoCr"));
            adaptors.Add("Roxburgh",            new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Roxb"));
            adaptors.Add("Selkirk",             new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Selk"));
            adaptors.Add("Shetland",            new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Shet"));
            adaptors.Add("Stirling",            new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Stir"));
            adaptors.Add("Sutherland",          new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Suth"));
            adaptors.Add("West Lothian",        new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "WLot"));
            adaptors.Add("Wigton",              new FamilySearchLocation(FactLocation.REGION, "2", "Scot", "Wigt"));

            // Welsh Counties
            adaptors.Add("Anglesey",            new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Angl"));
            adaptors.Add("Brecon",              new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Brec"));
            adaptors.Add("Caernarvon",          new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Caer"));
            adaptors.Add("Cardigan",            new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Card"));
            adaptors.Add("Carmarthen",          new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Carm"));
            adaptors.Add("Denbigh",             new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Denb"));
            adaptors.Add("Flint",               new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Flin"));
            adaptors.Add("Glamorgan",           new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Glam"));
            adaptors.Add("Gwynedd",             new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Gwyn"));
            adaptors.Add("Merioneth",           new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Meri"));
            adaptors.Add("Mid-Glamorgan",       new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "MGla"));
            adaptors.Add("Montgomery",          new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Mntg"));
            adaptors.Add("Pembroke",            new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Pemb"));
            adaptors.Add("Radnor",              new FamilySearchLocation(FactLocation.REGION, "2", "Wale", "Radn"));

            //Irish Counties
            adaptors.Add("Antrim",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Antr"));
            adaptors.Add("Armagh",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Arma"));
            adaptors.Add("Cavan",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Carl"));
            adaptors.Add("Carlow",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Cava")); 
            adaptors.Add("Clare",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Clar"));
	        adaptors.Add("Connaught",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "ConP"));
            adaptors.Add("Cork",        new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Cork")); 
            adaptors.Add("Donegal",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Done")); 
            adaptors.Add("Down",        new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Down")); 
            adaptors.Add("Dublin",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Dubl"));
	        adaptors.Add("Fermanagh",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Ferm")); 
            adaptors.Add("Galway",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Galw")); 
            adaptors.Add("Kerry",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Kerr")); 
            adaptors.Add("Kildare",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Kild")); 
            adaptors.Add("Kilkenny",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Kilk"));
	        adaptors.Add("Laoighis",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Laoi")); 
            adaptors.Add("Leinster",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "LeiP")); 
            adaptors.Add("Leitrim",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Leit")); 
            adaptors.Add("Limerick",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Lime")); 
            adaptors.Add("Londonderry", new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Ldry"));
	        adaptors.Add("Longford",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Long")); 
            adaptors.Add("Louth",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Lout")); 
            adaptors.Add("Mayo",        new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Mayo")); 
            adaptors.Add("Meath",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Meat")); 
            adaptors.Add("Monaghan",    new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Mghn"));
	        adaptors.Add("Munster",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "MunP")); 
            adaptors.Add("Offaly",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Offa")); 
            adaptors.Add("Roscommon",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Rosc")); 
            adaptors.Add("Sligo",       new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Slig")); 
            adaptors.Add("Tipperary",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Tipp"));
	        adaptors.Add("Tyrone",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Tyro")); 
            adaptors.Add("Ulster",      new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "UlsP")); 
            adaptors.Add("Waterford",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Wat")); 
            adaptors.Add("Westmeath",   new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Wmea")); 
            adaptors.Add("Wexford",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Wexf"));
	        adaptors.Add("Wicklow",     new FamilySearchLocation(FactLocation.REGION, "2", "Irel", "Wick"));

            // Canadian Provinces
            adaptors.Add("Alberta",                 new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Alta"));
            adaptors.Add("British Columbia",        new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "BrCo"));
            adaptors.Add("Manitoba",                new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Mani"));
            adaptors.Add("New Brunswick",           new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "NBru"));
            adaptors.Add("Newfoundland",            new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Newf"));
            adaptors.Add("Northwest Territories",   new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "NWT"));
            adaptors.Add("Nova Scotia",             new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "NSco"));
            adaptors.Add("Ontario",                 new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Ont"));
            adaptors.Add("Prince Edward Island",    new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "PEI"));
            adaptors.Add("Quebec",                  new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Queb"));
            adaptors.Add("Saskatchewan",            new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "Sask"));
            adaptors.Add("Yukon",                   new FamilySearchLocation(FactLocation.REGION, "11", "CAN", "YukT"));

            // US States
            adaptors.Add("Alabama",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Alab"));
            adaptors.Add("Alaska",                  new FamilySearchLocation(FactLocation.REGION, "11", "US", "Alas"));
            adaptors.Add("Arizona",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Ariz"));
            adaptors.Add("Arkansas",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Ark"));
            adaptors.Add("California",              new FamilySearchLocation(FactLocation.REGION, "11", "US", "Cal"));
            adaptors.Add("Colorado",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Colr"));
            adaptors.Add("Connecticut",             new FamilySearchLocation(FactLocation.REGION, "11", "US", "Conn"));
            adaptors.Add("Delaware",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Dela"));
            adaptors.Add("District of Columbia",    new FamilySearchLocation(FactLocation.REGION, "11", "US", "DiCo"));
            adaptors.Add("Florida",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Flor"));
            adaptors.Add("Georgia",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Geor"));
            adaptors.Add("Hawaii",                  new FamilySearchLocation(FactLocation.REGION, "11", "US", "Hawa"));
            adaptors.Add("Idaho",                   new FamilySearchLocation(FactLocation.REGION, "11", "US", "Idah"));
            adaptors.Add("Illinois",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Ill"));
            adaptors.Add("Indiana",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Indn"));
            adaptors.Add("Iowa",                    new FamilySearchLocation(FactLocation.REGION, "11", "US", "Iowa"));
            adaptors.Add("Kansas",                  new FamilySearchLocation(FactLocation.REGION, "11", "US", "Kan"));
            adaptors.Add("Kentucky",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Ktky"));
            adaptors.Add("Louisiana",               new FamilySearchLocation(FactLocation.REGION, "11", "US", "Lou"));
            adaptors.Add("Maine",                   new FamilySearchLocation(FactLocation.REGION, "11", "US", "Main"));
            adaptors.Add("Maryland",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Mary"));
            adaptors.Add("Massachusetts",           new FamilySearchLocation(FactLocation.REGION, "11", "US", "Mass"));
            adaptors.Add("Michigan",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Mchi"));
            adaptors.Add("Minnesota",               new FamilySearchLocation(FactLocation.REGION, "11", "US", "Minn"));
            adaptors.Add("Mississippi",             new FamilySearchLocation(FactLocation.REGION, "11", "US", "Misp"));
            adaptors.Add("Missouri",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Miso"));
            adaptors.Add("Montana",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Mont"));
            adaptors.Add("Nebraska",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Nebr"));
            adaptors.Add("Nevada",                  new FamilySearchLocation(FactLocation.REGION, "11", "US", "Nev"));
            adaptors.Add("New Hampshire",           new FamilySearchLocation(FactLocation.REGION, "11", "US", "NHam"));
            adaptors.Add("New Jersey",              new FamilySearchLocation(FactLocation.REGION, "11", "US", "NJer"));
            adaptors.Add("New Mexico",              new FamilySearchLocation(FactLocation.REGION, "11", "US", "NMex"));
            adaptors.Add("New York",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "NYor"));
            adaptors.Add("North Carolina",          new FamilySearchLocation(FactLocation.REGION, "11", "US", "NCar"));
            adaptors.Add("North Dakota",            new FamilySearchLocation(FactLocation.REGION, "11", "US", "NDak"));
            adaptors.Add("Ohio",                    new FamilySearchLocation(FactLocation.REGION, "11", "US", "Ohio"));
            adaptors.Add("Oklahoma",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Okla"));
            adaptors.Add("Oregon",                  new FamilySearchLocation(FactLocation.REGION, "11", "US", "Oreg"));
            adaptors.Add("Pennsylvania",            new FamilySearchLocation(FactLocation.REGION, "11", "US", "Penn"));
            adaptors.Add("Rhode Island",            new FamilySearchLocation(FactLocation.REGION, "11", "US", "RhoI"));
            adaptors.Add("South Carolina",          new FamilySearchLocation(FactLocation.REGION, "11", "US", "SCar"));
            adaptors.Add("South Dakota",            new FamilySearchLocation(FactLocation.REGION, "11", "US", "SDak"));
            adaptors.Add("Tennessee",               new FamilySearchLocation(FactLocation.REGION, "11", "US", "Tenn"));
            adaptors.Add("Texas",                   new FamilySearchLocation(FactLocation.REGION, "11", "US", "Tex"));
            adaptors.Add("Utah",                    new FamilySearchLocation(FactLocation.REGION, "11", "US", "Utah"));
            adaptors.Add("Vermont",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Verm"));
            adaptors.Add("Virginia",                new FamilySearchLocation(FactLocation.REGION, "11", "US", "Virg"));
            adaptors.Add("Washington",              new FamilySearchLocation(FactLocation.REGION, "11", "US", "Wash"));
            adaptors.Add("West Virginia",           new FamilySearchLocation(FactLocation.REGION, "11", "US", "WVir"));
            adaptors.Add("Wisconsin",               new FamilySearchLocation(FactLocation.REGION, "11", "US", "Wisc"));
            adaptors.Add("Wyoming",                 new FamilySearchLocation(FactLocation.REGION, "11", "US", "Wyom"));
        }

        private FamilySearchLocation(int level, string region, string juris1, string juris2)
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
            return new FamilySearchLocation(this.level, this.region, this.juris1, this.juris2);
        }

    }
}
