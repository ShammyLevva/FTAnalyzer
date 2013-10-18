using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusFamily : Family
    {

        public FactDate CensusDate { get; private set; }
        public FactLocation BestLocation { get; private set; }

        public CensusFamily(Family f, FactDate censusDate)
            : base(f)
        {
            this.CensusDate = censusDate;
            this.BestLocation = null;
        }

        public new IEnumerable<CensusIndividual> Members
        {
            get { return base.Members.Select((i, pos) => new CensusIndividual(pos, i, this)); }
        }

        public bool Process(FactDate censusDate, bool censusDone)
        {
            bool result = false;
            this.CensusDate = censusDate;
            List<Fact> facts = new List<Fact>();
            if (IsValidFamily())
            {
                if (IsValidIndividual(Wife, censusDone, true))
                {
                    result = true;
                    Wife.Status = Individual.WIFE;
                    facts.AddRange(Wife.PersonalFacts);
                }
                else
                    Wife = null;
                // overwrite bestLocation by husbands as most commonly the family
                // end up at husbands location after marriage
                if (IsValidIndividual(Husband, censusDone, true))
                {
                    result = true;
                    Husband.Status = Individual.HUSBAND;
                    facts.AddRange(Husband.PersonalFacts);
                }
                else
                    Husband = null;
                // update bestLocation by marriage date as Husband and Wife 
                // locations are often birth locations
                Fact marriage = GetPreferredFact(Fact.MARRIAGE);
                if (marriage != null)
                    facts.Add(marriage);
                List<Individual> censusChildren = new List<Individual>();
                // sort children oldest first
                Children.Sort(new CensusAgeComparer());
                foreach (Individual child in Children)
                {
                    // set location to childs birth location
                    // this will end up setting birth location of last child 
                    // as long as the location is at least Parish level
                    child.Status = Individual.CHILD;
                    if (IsValidIndividual(child, censusDone, false))
                    {
                        result = true;
                        censusChildren.Add(child);
                        facts.AddRange(child.PersonalFacts);
                    }
                }
                Children = censusChildren;
                this.BestLocation = FactLocation.BestLocation(facts, censusDate);
            }
            return result;
        }

        private bool IsValidIndividual(Individual indiv, bool censusDone, bool parentCheck)
        {
            if (indiv == null)
                return false;
            FamilyTree ft = FamilyTree.Instance;
            DateTime birth = indiv.BirthDate.StartDate;
            DateTime death = indiv.DeathDate.EndDate;
            if (birth < CensusDate.StartDate && death > CensusDate.StartDate && indiv.IsCensusDone(CensusDate) == censusDone)
            {
                if (parentCheck) // Husband or Wife with valid date range
                    return true;
                else // individual is a child so remove if married before census date
                    return !ft.IsMarried(indiv, CensusDate);
            }
            else
                return false;
        }

        private bool IsValidFamily()
        {
            if (MarriageDate.StartDate > CensusDate.EndDate)
                return false;
            if (FamilyID == Family.SOLOINDIVIDUAL)
                return true; // allow solo individual families to be processed
            // don't process family if either parent is under 16
            //if(Husband != null) rtb.AppendText("Husband : " + Husband.getAge(censusDate) + "\n");
            if (Husband != null && Husband.GetMaxAge(CensusDate) < 16)
                return false;
            //if(Wife  != null) rtb.AppendText("Wife : " + Wife.getAge(censusDate) + "\n");
            if (Wife != null && Wife.GetMaxAge(CensusDate) < 16)
                return false;
            return true;
        }

        public string Surname
        {
            get
            {
                if (Husband != null) return Husband.SurnameAtDate(CensusDate);
                else if (Wife != null) return Wife.SurnameAtDate(CensusDate);
                else
                {
                    Individual child = Children.FirstOrDefault();
                    if (child != null)
                    {
                        return child.SurnameAtDate(CensusDate);
                    }
                }
                return "UNKNOWN";
            }
        }
    }
}