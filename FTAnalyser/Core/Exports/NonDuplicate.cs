using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    [Serializable]
    public class NonDuplicate
    {
        public NonDuplicate()
        { }

        public NonDuplicate(DisplayDuplicateIndividual dup)
        {
            IndividualA_ID = dup.IndividualID;
            IndividualA_Name = dup.Name;
            IndividualA_BirthDate = dup.BirthDate;
            IndividualB_ID = dup.MatchIndividualID;
            IndividualB_Name = dup.MatchName;
            IndividualB_BirthDate = dup.MatchBirthDate;
        }

        public string IndividualA_ID { get; set; }
        public string IndividualA_Name { get; set; }
        public FactDate IndividualA_BirthDate { get; set; }
        public string IndividualB_ID { get; set; }
        public string IndividualB_Name { get; set; }
        public FactDate IndividualB_BirthDate { get; set; }
    }
}
