using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FactComparer : IEqualityComparer<Fact>
    {
        public bool Equals(Fact x, Fact y)
        {
            return x.FactType == y.FactType && x.FactDate == y.FactDate && x.Location == y.Location;
        }

        public int GetHashCode(Fact obj)
        {
            int code = IntDate(obj.FactDate.StartDate) * 10 + IntDate(obj.FactDate.EndDate);
            return code;
        }

        private int IntDate(DateTime date)
        {
            int dayhash = (date.Year * 100 + date.Month) * 100 + date.Day;
            return dayhash;
        }
    }
}
