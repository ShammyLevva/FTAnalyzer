using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FalseFilter<T> : Filter<T>
    {
        public FalseFilter()
        { }

        public bool select (T t) {
            return false;
        }
    }
}