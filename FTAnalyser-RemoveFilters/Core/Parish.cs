using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class Parish : FactLocation {
        
        public Parish() {
            this.address = "";
            this.place = "";
            this.parishID = null;
        }

        public Parish(string location) : base(location) {
            this.address = "";
            this.place = "";
            this.parishID = null;
        }

        public override int CompareTo (FactLocation that, int level) {
            int res = this.country.CompareTo(that.country);
            if (res == 0 && level > COUNTRY) {
                res = this.region.CompareTo(that.region);
                if (res == 0 && level > REGION) {
                    res = this.parish.CompareTo(that.parish);
                }
            }
            return res;
        }
    }
}