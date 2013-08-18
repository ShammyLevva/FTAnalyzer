using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class Parish : FactLocation {
        
        public Parish() {
            this.Address = "";
            this.Place = "";
            this.ParishID = null;
        }

        public Parish(string location) : base(location) {
            this.Address = "";
            this.Place = "";
            this.ParishID = null;
        }

        public override int CompareTo (FactLocation that, int level) {
            int res = this.Country.CompareTo(that.Country);
            if (res == 0 && level > COUNTRY) {
                res = this.Region.CompareTo(that.Region);
                if (res == 0 && level > REGION) {
                    res = this.Parish.CompareTo(that.Parish);
                }
            }
            return res;
        }
    }
}