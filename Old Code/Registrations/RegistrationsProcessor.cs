using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class RegistrationsProcessor
    {

        Func<Registration, bool> filter;
        IComparer<Registration> comparator;

        public RegistrationsProcessor(Func<Registration, bool> f, IComparer<Registration> c)
        {
            filter = f;
            comparator = c;
        }

        public RegistrationsProcessor(Func<Registration, bool> f) 
            : this(f, null)
        { }

        public RegistrationsProcessor(IComparer<Registration> c)
            : this(x => true, c)
        { }

        public RegistrationsProcessor()
            : this((IComparer<Registration>)null)
        { }

        private List<Registration> sortRegistrations(List<Registration> regs)
        {
            if (comparator != null)
                regs.Sort(comparator);
            return regs;
        }

        public List<Registration> processRegistrations(IList<Registration> regs)
        {
            return sortRegistrations(regs.Where(filter).ToList());
        }
    }
}