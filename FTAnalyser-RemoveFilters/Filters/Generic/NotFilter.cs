using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class NotFilter<T> : Filter<T>
    {

        private Filter<T> filter1;

        public NotFilter(Filter<T> f1)
        {
            this.filter1 = f1;
        }

        public bool select(T t)
        {
            return ! filter1.select(t);
        }
    }
}