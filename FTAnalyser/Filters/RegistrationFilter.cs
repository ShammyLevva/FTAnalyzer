using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public interface RegistrationFilter
    {
        bool select(Registration r);
    }
}