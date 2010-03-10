using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class AndFilter : RegistrationFilter
    {

        private RegistrationFilter filter1;
        private RegistrationFilter filter2;
        private RegistrationFilter filter3;

        public AndFilter(RegistrationFilter f1, RegistrationFilter f2)
            : this(f1, f2, new TrueFilter())
        { }

        public AndFilter(RegistrationFilter f1, RegistrationFilter f2, RegistrationFilter f3)
        {
            this.filter1 = f1;
            this.filter2 = f2;
            this.filter3 = f3;
        }

        public bool select(Registration r)
        {
            return filter1.select(r) && filter2.select(r) && filter3.select(r);
        }
    }
}