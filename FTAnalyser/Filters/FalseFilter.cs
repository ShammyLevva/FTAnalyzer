using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FalseFilter : RegistrationFilter
    {
        public FalseFilter()
        { }

        public bool select (Registration r) {
            return false;
        }
    }
}