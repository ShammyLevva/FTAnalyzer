using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class OrFilter<T> : Filter<T> {

        private List<Filter<T>> filters;

        private OrFilter()
        {
            this.filters = new List<Filter<T>>();
        }

        public OrFilter(Filter<T> f1, Filter<T> f2) :
            this()
        {
            Add(f1);
            Add(f2);
        }

        public OrFilter(Filter<T> f1, Filter<T> f2, Filter<T> f3)
            : this()
        {
            Add(f1);
            Add(f2);
            Add(f3);
        }

        private void Add(Filter<T> rf)
        {
            if (rf is OrFilter<T>)
            {
                OrFilter<T> of = (OrFilter<T>)rf;
                filters.AddRange(of.filters);
            }
            else
            {
                filters.Add(rf);
            }
        }

        public bool select(T t)
        {
            foreach (Filter<T> f in filters)
            {
                if (f.select(t))
                {
                    return true;
                }
            }
            return false;
        }
    }
}