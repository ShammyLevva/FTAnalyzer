using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class BirthFact : Fact
    {
        public BirthFact(FactDate date)
            : base(Fact.BIRTH, date)
        {
            Created = true;
        }

    }
}
