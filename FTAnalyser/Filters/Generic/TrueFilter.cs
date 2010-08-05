using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class TrueFilter<T> : Filter<T>
    {
        public TrueFilter()
        { }
        
        public bool select (T t) {
            return true;
        }
    }
}