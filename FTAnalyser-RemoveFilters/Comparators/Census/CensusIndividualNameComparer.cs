﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusIndividualNameComparer : Comparer<DisplayCensus>
    {
        public override int Compare(DisplayCensus x, DisplayCensus y)
        {
            int r = x.Registration.Surname.CompareTo(y.Registration.Surname);
            if (r == 0)
            {
                r = x.Individual.Forenames.CompareTo(y.Individual.Forenames);
                if (r == 0)
                {
                    r = x.Position - y.Position;
                }
            }
            return r;
        }
    }
}