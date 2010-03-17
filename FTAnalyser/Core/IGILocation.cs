using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class IGILocation : ICloneable
    {
         
        private FactLocation location;

        private string juris1, juris2, region;

        private static Dictionary<string, IGILocation> adaptors;


        public static IGILocation Adapt(FactLocation location)
        {
            IGILocation adaptor;
            adaptors.TryGetValue(location.Country, out adaptor);
            if (adaptor == null)
            {
                adaptor = adaptors["Scotland"];
            }
            return adaptor;
        }

        static IGILocation() {
            adaptors = new Dictionary<string, IGILocation>();

            // British Isles
            adaptors.Add("Channel Islands",     new IGILocation("2", "ChaI", ""));
            adaptors.Add("England",             new IGILocation("2", "Engl", ""));
            adaptors.Add("Ireland",             new IGILocation("2", "Irel", ""));
            adaptors.Add("Isle of Man",         new IGILocation("2", "IMan", ""));
            adaptors.Add("Scotland",            new IGILocation("2", "Scot", ""));
            adaptors.Add("Wales",               new IGILocation("2", "Wale", ""));

            // North America
            adaptors.Add("Canada",              new IGILocation("11", "CAN", ""));
            adaptors.Add("United States",       new IGILocation("11", "US", ""));
        }

        private IGILocation(string region, string juris1, string juris2)
        {
            this.region = region;
            this.juris1 = juris1;
            this.juris2 = juris2;
            this.location = null;
        }

        #region Properties

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
            return new IGILocation(this.region, this.juris1, this.juris2);
        }

    }
}
