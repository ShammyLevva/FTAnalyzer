using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class TrueFilter : RegistrationFilter {

        public TrueFilter()
        { }
        
        public bool select (Registration r) {
            return true;
        }
    }
}