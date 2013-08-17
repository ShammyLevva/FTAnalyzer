using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Filters
{
    public class FilterUtils
    {
        public static Func<T, bool> TrueFilter<T>()
        {
            return (x => true);
        }

        public static Func<T, bool> FalseFilter<T>()
        {
            return (x => false);
        }

        public static Func<T1, T2, bool> FalseFilter<T1, T2>()
        {
            return ((x, y) => false);
        }

        public static Func<T1, T2, bool> TrueFilter<T1, T2>()
        {
            return (x, y) => true;
        }

        public static Func<T, bool> OrFilter<T>(Func<T, bool> p1, Func<T, bool> p2)
        {
            return (x => p1(x) || p2(x));
        }

        public static Func<T, bool> OrFilter<T>(Func<T, bool> p1, Func<T, bool> p2, Func<T, bool> p3)
        {
            return (x => p1(x) || p2(x) || p3(x));
        }

        public static Func<T1, Func<T2, bool>> OrFilter<T1, T2>(Func<T1, Func<T2, bool>> p1, Func<T1, Func<T2, bool>> p2)
        {
            return y => x => p1(y)(x) || p2(y)(x);
        }

        public static Func<T1, Func<T2, bool>> OrFilter<T1, T2>(Func<T1, Func<T2, bool>> p1, Func<T1, Func<T2, bool>> p2, Func<T1, Func<T2, bool>> p3)
        {
            return y => x => p1(y)(x) || p2(y)(x) || p3(y)(x);
        }

        public static Func<T, bool> AndFilter<T>(Func<T, bool> p1, Func<T, bool> p2)
        {
            return (x => p1(x) && p2(x));
        }

        public static Func<T, bool> AndFilter<T>(Func<T, bool> p1, Func<T, bool> p2, Func<T, bool> p3)
        {
            return (x => p1(x) && p2(x) && p3(x));
        }

        public static Func<FactDate, Func<T, bool>> LocationFilter<T>(Func<FactDate, T, FactLocation> f, Func<FactLocation, string> g, string s)
        {
            return d => StringFilter<T>(x => g(f(d, x)), s);
        }

        public static Func<T, bool> StringFilter<T>(Func<T, string> f, string s)
        {
            return (x => StringMatches(f(x), s));
        }

        public static Func<T, bool> IntFilter<T>(Func<T, int> f, int i)
        {
            return x => f(x) == i;
        }

        public static Func<T, bool> DateFilter<T>(Func<T, FactDate> f, FactDate d)
        {
            return x => f(x).overlaps(d);
        }

        public static Func<T, bool> IncompleteDataFilter<T>(int level, Func<T, bool> certificatePresent,
            Func<T, FactDate> filterDate, Func<FactDate, T, FactLocation> filterLocation)
        {
            return t =>
            {
                if (certificatePresent(t))
                    return false;
                FactDate fd = filterDate(t);
                if (fd == null || !fd.isExact())
                    return true;
                FactLocation l = filterLocation(fd, t);
                switch (level)
                {
                    case FactLocation.COUNTRY: return (l.Country.Length == 0);
                    case FactLocation.REGION: return (l.Region.Length == 0);
                    case FactLocation.PARISH: return (l.Parish.Length == 0);
                    case FactLocation.ADDRESS: return (l.Address.Length == 0);
                    case FactLocation.PLACE: return (l.Place.Length == 0);
                    default: return true;
                }
            };
        }

        private static bool StringMatches(string s1, string s2)
        {
            return s1 == null ? false : s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
