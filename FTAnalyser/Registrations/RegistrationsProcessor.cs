using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class RegistrationsProcessor
    {

        RegistrationFilter filter;
        IComparer<Registration> comparator;

        public RegistrationsProcessor(RegistrationFilter f, IComparer<Registration> c)
        {
            filter = f;
            comparator = c;
        }

        public RegistrationsProcessor(RegistrationFilter f) 
            : this(f, null)
        { }

        public RegistrationsProcessor(IComparer<Registration> c)
            : this(new TrueFilter(), c)
        { }

        public RegistrationsProcessor()
            : this(new TrueFilter(), null)
        { }

        private List<Registration> filterRegistrations(List<Registration> regs) {
            List<Registration> result = new List<Registration>();
            foreach (Registration r in regs) {
                if (filter.select(r))
                    result.Add(r);
            }
            return result;
        }

        private List<Registration> sortRegistrations(List<Registration> regs)
        {
            if (comparator != null)
                regs.Sort(comparator);
            return regs;
        }

        public List<Registration> processRegistrations(List<Registration> regs)
        {
            return sortRegistrations(filterRegistrations(regs));
        }
    }
}