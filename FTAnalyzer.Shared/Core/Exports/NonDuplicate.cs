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
            IndividualA_BirthDate = dup.BirthDate.ToString();
            IndividualB_ID = dup.MatchIndividualID;
            IndividualB_Name = dup.MatchName;
            IndividualB_BirthDate = dup.MatchBirthDate.ToString();
        }

        public string IndividualA_ID { get; set; }
        public string IndividualA_Name { get; set; }
        public string IndividualA_BirthDate { get; set; }
        public string IndividualB_ID { get; set; }
        public string IndividualB_Name { get; set; }
        public string IndividualB_BirthDate { get; set; }

        public override bool Equals(object obj)
        {
            NonDuplicate that = (NonDuplicate)obj;
            bool result = (this.IndividualA_ID == that.IndividualA_ID &&
                   this.IndividualA_Name == that.IndividualA_Name &&
                   this.IndividualA_BirthDate == that.IndividualA_BirthDate &&
                   this.IndividualB_ID == that.IndividualB_ID &&
                   this.IndividualB_Name == that.IndividualB_Name &&
                   this.IndividualB_BirthDate == that.IndividualB_BirthDate)
                ||
                (this.IndividualA_ID == that.IndividualB_ID &&
                   this.IndividualA_Name == that.IndividualB_Name &&
                   this.IndividualA_BirthDate == that.IndividualB_BirthDate &&
                   this.IndividualB_ID == that.IndividualA_ID &&
                   this.IndividualB_Name == that.IndividualA_Name &&
                   this.IndividualB_BirthDate == that.IndividualA_BirthDate);
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
