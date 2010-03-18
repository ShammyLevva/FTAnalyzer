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
                adaptors.TryGetValue(location.Region, out adaptor);
            }
            if (adaptor == null)
            {
                adaptor = adaptors["Scotland"];
            }
            return adaptor;
        }

        static IGILocation() {
            adaptors = new Dictionary<string, IGILocation>();

            // British Isles
            adaptors.Add("Channel Islands",     new IGILocation(FactLocation.COUNTRY, "2", "ChaI", ""));
            adaptors.Add("England",             new IGILocation(FactLocation.COUNTRY, "2", "Engl", ""));
            adaptors.Add("Ireland",             new IGILocation(FactLocation.COUNTRY, "2", "Irel", ""));
            adaptors.Add("Isle of Man",         new IGILocation(FactLocation.COUNTRY, "2", "IMan", ""));
            adaptors.Add("Scotland",            new IGILocation(FactLocation.COUNTRY, "2", "Scot", ""));
            adaptors.Add("Wales",               new IGILocation(FactLocation.COUNTRY, "2", "Wale", ""));

            // North America
            adaptors.Add("Canada",              new IGILocation(FactLocation.COUNTRY, "11", "CAN", ""));
            adaptors.Add("United States",       new IGILocation(FactLocation.COUNTRY, "11", "US", ""));

            // English counties
            adaptors.Add("Bedfordshire",        new IGILocation(FactLocation.REGION, "2", "Engl", "Bedf"));
            adaptors.Add("Berkshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Berk"));
            adaptors.Add("Buckinghamshire",     new IGILocation(FactLocation.REGION, "2", "Engl", "Buck"));
            adaptors.Add("Cambridgeshire",      new IGILocation(FactLocation.REGION, "2", "Engl", "Camb"));
            adaptors.Add("Cheshire",            new IGILocation(FactLocation.REGION, "2", "Engl", "Ches"));
            adaptors.Add("Cornwall",            new IGILocation(FactLocation.REGION, "2", "Engl", "Corn"));
            adaptors.Add("Cumberland",          new IGILocation(FactLocation.REGION, "2", "Engl", "Cumb"));
            adaptors.Add("Cumbria",             new IGILocation(FactLocation.REGION, "2", "Engl", "Cumr"));
            adaptors.Add("Derbyshire",          new IGILocation(FactLocation.REGION, "2", "Engl", "Derb"));
            adaptors.Add("Devon",               new IGILocation(FactLocation.REGION, "2", "Engl", "Devo"));
            adaptors.Add("Dorset",              new IGILocation(FactLocation.REGION, "2", "Engl", "Dors"));
            adaptors.Add("Durham",              new IGILocation(FactLocation.REGION, "2", "Engl", "Durh"));
            adaptors.Add("Essex",               new IGILocation(FactLocation.REGION, "2", "Engl", "Esse"));
            adaptors.Add("Gloucestershire",     new IGILocation(FactLocation.REGION, "2", "Engl", "Glou"));
            adaptors.Add("Hampshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Hamp"));
            adaptors.Add("Herefordshire",       new IGILocation(FactLocation.REGION, "2", "Engl", "Here"));
            adaptors.Add("Hertfordshire",       new IGILocation(FactLocation.REGION, "2", "Engl", "Hert"));
            adaptors.Add("Huntingdonshire",     new IGILocation(FactLocation.REGION, "2", "Engl", "Hunt"));
            adaptors.Add("Kent",                new IGILocation(FactLocation.REGION, "2", "Engl", "Kent"));
            adaptors.Add("Lancashire",          new IGILocation(FactLocation.REGION, "2", "Engl", "Lanc"));
            adaptors.Add("Leicestershire",      new IGILocation(FactLocation.REGION, "2", "Engl", "Leic"));
            adaptors.Add("Lincolnshire",        new IGILocation(FactLocation.REGION, "2", "Engl", "Linc"));
            adaptors.Add("Middlesex",           new IGILocation(FactLocation.REGION, "2", "Engl", "Lond"));
            adaptors.Add("Monmouthshire",       new IGILocation(FactLocation.REGION, "2", "Engl", "Monm"));
            adaptors.Add("Norfolk",             new IGILocation(FactLocation.REGION, "2", "Engl", "Norf"));
            adaptors.Add("Northamptonshire",    new IGILocation(FactLocation.REGION, "2", "Engl", "Nham"));
            adaptors.Add("Northumberland",      new IGILocation(FactLocation.REGION, "2", "Engl", "Nthu"));
            adaptors.Add("Nottinghamshire",     new IGILocation(FactLocation.REGION, "2", "Engl", "NOtt"));
            adaptors.Add("Oxfordshire",         new IGILocation(FactLocation.REGION, "2", "Engl", "Oxfo"));
            adaptors.Add("Rutland",             new IGILocation(FactLocation.REGION, "2", "Engl", "Rutl"));
            adaptors.Add("Shropshire",          new IGILocation(FactLocation.REGION, "2", "Engl", "Shro"));
            adaptors.Add("Somerset",            new IGILocation(FactLocation.REGION, "2", "Engl", "Som"));
            adaptors.Add("Staffordshire",       new IGILocation(FactLocation.REGION, "2", "Engl", "Staf"));
            adaptors.Add("Suffolk",             new IGILocation(FactLocation.REGION, "2", "Engl", "Suff"));
            adaptors.Add("Sussex",              new IGILocation(FactLocation.REGION, "2", "Engl", "Suss"));
            adaptors.Add("Surrey",              new IGILocation(FactLocation.REGION, "2", "Engl", "Surr"));
            adaptors.Add("Warwickshire",        new IGILocation(FactLocation.REGION, "2", "Engl", "Warw"));
            adaptors.Add("Westmorland",         new IGILocation(FactLocation.REGION, "2", "Engl", "Wmor"));
            adaptors.Add("Wiltshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "Wilt"));
            adaptors.Add("Worcestershire",      new IGILocation(FactLocation.REGION, "2", "Engl", "Worc"));
            adaptors.Add("Yorkshire",           new IGILocation(FactLocation.REGION, "2", "Engl", "York"));

            // Scottish counties
            adaptors.Add("Aberdeenshire",       new IGILocation(FactLocation.REGION, "2", "Scot", "Aber"));
            adaptors.Add("Angus",               new IGILocation(FactLocation.REGION, "2", "Scot", "Angu"));
            adaptors.Add("Argyll",              new IGILocation(FactLocation.REGION, "2", "Scot", "Argy"));
            adaptors.Add("Ayrshire",            new IGILocation(FactLocation.REGION, "2", "Scot", "Ayr"));
            adaptors.Add("Banffshire",          new IGILocation(FactLocation.REGION, "2", "Scot", "Banf"));
            adaptors.Add("Berwickshire",        new IGILocation(FactLocation.REGION, "2", "Scot", "Berw"));
            adaptors.Add("Bute",                new IGILocation(FactLocation.REGION, "2", "Scot", "Bute"));
            adaptors.Add("Caithness",           new IGILocation(FactLocation.REGION, "2", "Scot", "Cait"));
            adaptors.Add("Clackmannanshire",    new IGILocation(FactLocation.REGION, "2", "Scot", "Clac"));
            adaptors.Add("Dumfries",            new IGILocation(FactLocation.REGION, "2", "Scot", "Dumf"));
            adaptors.Add("Dunbartonshire",      new IGILocation(FactLocation.REGION, "2", "Scot", "Dunb"));
            adaptors.Add("East Lothian",        new IGILocation(FactLocation.REGION, "2", "Scot", "ELot"));
            adaptors.Add("Fife",                new IGILocation(FactLocation.REGION, "2", "Scot", "Fife"));
            adaptors.Add("Inverness",           new IGILocation(FactLocation.REGION, "2", "Scot", "Inve"));
            adaptors.Add("Kincardineshire",     new IGILocation(FactLocation.REGION, "2", "Scot", "Kinc"));
            adaptors.Add("Kinross",             new IGILocation(FactLocation.REGION, "2", "Scot", "Kinr"));
            adaptors.Add("Kirkcudbrightshire",  new IGILocation(FactLocation.REGION, "2", "Scot", "Kirk"));
            adaptors.Add("Lanarkshire",         new IGILocation(FactLocation.REGION, "2", "Scot", "Lana"));
            adaptors.Add("Midlothian",          new IGILocation(FactLocation.REGION, "2", "Scot", "Mlot"));
            adaptors.Add("Moray",               new IGILocation(FactLocation.REGION, "2", "Scot", "Mora"));
            adaptors.Add("Nairn",               new IGILocation(FactLocation.REGION, "2", "Scot", "Nair"));
            adaptors.Add("Orkney",              new IGILocation(FactLocation.REGION, "2", "Scot", "Orkn"));
            adaptors.Add("Peebles",             new IGILocation(FactLocation.REGION, "2", "Scot", "Peeb"));
            adaptors.Add("Perthshire",          new IGILocation(FactLocation.REGION, "2", "Scot", "Pert"));
            adaptors.Add("Renfrewshire",        new IGILocation(FactLocation.REGION, "2", "Scot", "Renf"));
            adaptors.Add("Ross and Cromarty",   new IGILocation(FactLocation.REGION, "2", "Scot", "RoCr"));
            adaptors.Add("Roxburgh",            new IGILocation(FactLocation.REGION, "2", "Scot", "Roxb"));
            adaptors.Add("Selkirk",             new IGILocation(FactLocation.REGION, "2", "Scot", "Selk"));
            adaptors.Add("Shetland",            new IGILocation(FactLocation.REGION, "2", "Scot", "Shet"));
            adaptors.Add("Stirlingshire",       new IGILocation(FactLocation.REGION, "2", "Scot", "Stir"));
            adaptors.Add("Sutherland",          new IGILocation(FactLocation.REGION, "2", "Scot", "Suth"));
            adaptors.Add("West Lothian",        new IGILocation(FactLocation.REGION, "2", "Scot", "WLot"));
            adaptors.Add("Wigtonshire",         new IGILocation(FactLocation.REGION, "2", "Scot", "Wigt"));
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
