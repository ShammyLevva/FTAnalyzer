﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DefaultCensusComparer : Comparer<CensusIndividual>
    {
        public override int Compare(CensusIndividual c1, CensusIndividual c2)
        {
            int result = c1.FamilyID.CompareTo(c2.FamilyID);
            if (result == 0) result = c1.Position - c2.Position;
            return result;
        }
    }
}
