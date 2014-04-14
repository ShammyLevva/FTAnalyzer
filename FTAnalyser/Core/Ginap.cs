using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Ginap
    {
        public bool IsMale { get; private set; } 
        public string Name { get; private set; }
        
        public Ginap(int oSex, string oGName)
        {
            IsMale = oSex == 2;
            Name = oGName;
        }

        public Ginap(bool male, string name)
        {
            IsMale = male;
            Name = name;
        }
    }
}
