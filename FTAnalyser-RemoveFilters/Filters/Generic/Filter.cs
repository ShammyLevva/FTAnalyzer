using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public interface Filter<T>
    {
        bool select(T r);
    }
}