using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class AllFilter : RegistrationFilter {

        public bool select (Registration r) {
            return true;
        }

    }
}