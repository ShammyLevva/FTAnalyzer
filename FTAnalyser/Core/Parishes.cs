using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Parishes
    {
        private static Dictionary<string, string> ScottishParishes;

        static Parishes()
        {
            ScottishParishes = new Dictionary<string, string>();
            ScottishParishes.Add("Inverurie", "204");
        }

        public static string Reference(string parish)
        {
            if (ScottishParishes.ContainsKey(parish))
                return ", Parish Ref: " + ScottishParishes[parish];
            return string.Empty;
        }
    }
}
