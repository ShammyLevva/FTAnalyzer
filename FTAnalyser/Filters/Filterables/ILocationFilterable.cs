using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface ILocationFilterable
    {
        FactLocation BestLocation(FactDate when);
    }
}
