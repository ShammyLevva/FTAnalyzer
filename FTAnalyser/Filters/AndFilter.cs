using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class AndFilter : RegistrationFilter
    {

        private List<RegistrationFilter> filters;

        private AndFilter()
        {
            this.filters = new List<RegistrationFilter>();
        }

        public AndFilter(RegistrationFilter f1, RegistrationFilter f2)
            : this()
        {
            Add(f1);
            Add(f2);
        }

        public AndFilter(RegistrationFilter f1, RegistrationFilter f2, RegistrationFilter f3)
            : this()
        {
            Add(f1);
            Add(f2);
            Add(f3);
        }

        private void Add(RegistrationFilter rf)
        {
            if (rf is AndFilter)
            {
                AndFilter af = (AndFilter)rf;
                filters.AddRange(af.filters);
            }
            else
            {
                filters.Add(rf);
            }
        }

        public bool select(Registration r)
        {
            foreach (RegistrationFilter f in filters)
            {
                if (!f.select(r))
                {
                    return false;
                }
            }
            return true;
        }
    }
}