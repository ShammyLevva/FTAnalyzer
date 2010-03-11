using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class NotFilter : RegistrationFilter
    {

        private RegistrationFilter filter1;
        
        public NotFilter(RegistrationFilter f1)
        {
            this.filter1 = f1;
        }

        public bool select(Registration r)
        {
            return ! filter1.select(r);
        }
    }
}