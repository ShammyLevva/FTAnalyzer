using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class OrFilter : RegistrationFilter {

        private RegistrationFilter filter1;
        private RegistrationFilter filter2;
        
        public OrFilter (RegistrationFilter f1, RegistrationFilter f2) {
            this.filter1 = f1;
            this.filter2 = f2;
        }
        
        public bool select (Registration r) {
            return filter1.select(r) || filter2.select(r);
        }
    }
}