using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public interface RegistrationFilter
    {
        public bool select(Registration r);
    }
}