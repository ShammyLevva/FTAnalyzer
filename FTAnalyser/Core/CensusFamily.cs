using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusFamily : Family
    {

        public CensusDate CensusDate { get; private set; }
        public FactLocation BestLocation { get; private set; }
        public new CensusIndividual Husband { get; private set; }
        public new CensusIndividual Wife { get; private set; }
        public new List<CensusIndividual> Children { get; private set; }

        public CensusFamily(Family f, CensusDate censusDate)
            : base(f)
        {
            this.CensusDate = censusDate;
            this.BestLocation = null;
            this.Wife = Members.FirstOrDefault(x => x.Ind_ID == f.WifeID);
            this.Husband = Members.FirstOrDefault(x => x.Ind_ID == f.HusbandID);
            this.Children = new List<CensusIndividual>();
            foreach (Individual child in f.Children)
            {
                this.Children.Add(Members.FirstOrDefault(x => x.Ind_ID == child.Ind_ID));
            }
        }

        public new IEnumerable<CensusIndividual> Members
        {
            get { return base.Members.Select((i, pos) => new CensusIndividual(pos, i, this)); }
        }

        public bool Process(CensusDate censusDate, bool censusDone, bool checkCensus)
        {
            bool result = false;
            this.CensusDate = censusDate;
            List<Fact> facts = new List<Fact>();
            if (IsValidFamily())
            {
                if (IsValidIndividual(this.Wife, censusDone, true, checkCensus))
                {
                    result = true;
                    Wife.Status = Individual.WIFE;
                    facts.AddRange(Wife.PersonalFacts);
                }
                else
                    Wife = null;
                // overwrite bestLocation by husbands as most commonly the family
                // end up at husbands location after marriage
                if (IsValidIndividual(Husband, censusDone, true, checkCensus))
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
                List<CensusIndividual> censusChildren = new List<CensusIndividual>();
                // sort children oldest first
                Children.Sort(new CensusAgeComparer());
                foreach (CensusIndividual child in Children)
                {
                    // set location to childs birth location
                    // this will end up setting birth location of last child 
                    // as long as the location is at least Parish level
                    child.Status = Individual.CHILD;
                    if (IsValidIndividual(child, censusDone, false, checkCensus))
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

        private bool IsValidIndividual(CensusIndividual indiv, bool censusDone, bool parentCheck, bool checkCensus)
        {
            if (indiv == null)
                return false;
            DateTime birth = indiv.BirthDate.StartDate;
            DateTime death = indiv.DeathDate.EndDate;
            FactLocation bestLocation = indiv.BestLocation(CensusDate);
            if (birth <= CensusDate.StartDate && death >= CensusDate.StartDate && 
                (!bestLocation.IsKnownCountry || CensusDate.IsCensusCountry(CensusDate, bestLocation)))
            {
                if ((checkCensus && indiv.IsCensusDone(CensusDate) == censusDone) || !checkCensus)
                {
                    if (parentCheck) // Husband or Wife with valid date range
                        return true;
                    else // individual is a child so remove if married before census date
                        return !indiv.IsMarried(CensusDate);
                }
                else
                    return false;
            }
            else
                return false;
        }

        private bool IsValidFamily()
        {
            Individual eldestChild = Children.OrderBy(x => x.BirthDate).FirstOrDefault();
            if (MarriageDate.IsAfter(CensusDate) && (eldestChild == null || eldestChild.BirthDate.IsAfter(CensusDate)))
                return false;
            if (FamilyID == Family.SOLOINDIVIDUAL || FamilyID == Family.PRE_MARRIAGE)
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