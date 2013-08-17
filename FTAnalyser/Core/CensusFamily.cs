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
            get
            {
                return base.Members.Select((i, pos) => new CensusIndividual(pos, i, this));
            }
        }

        public bool process(FactDate censusDate, bool censusDone, bool includeResidence, bool lostCousinsCheck)
        {
            bool result = false;
            this.CensusDate = censusDate;
            List<Fact> facts = new List<Fact>();
            if (isValidFamily())
            {
                if (isValidIndividual(wife, censusDone, includeResidence, lostCousinsCheck, true))
                {
                    result = true;
                    wife.Status = Individual.WIFE;
                    facts.AddRange(wife.AllFacts);
                }
                else
                    Wife = null;
                // overwrite bestLocation by husbands as most commonly the family
                // end up at husbands location after marriage
                if (isValidIndividual(husband, censusDone, includeResidence, lostCousinsCheck, true))
                {
                    result = true;
                    husband.Status = Individual.HUSBAND;
                    facts.AddRange(husband.AllFacts);
                }
                else
                    Husband = null;
                // update bestLocation by marriage date as husband and wife 
                // locations are often birth locations
                Fact marriage = getPreferredFact(Fact.MARRIAGE);
                if (marriage != null)
                    facts.Add(marriage);
                List<Individual> censusChildren = new List<Individual>();
                // sort children oldest first
                children.Sort(new CensusAgeComparator());
                foreach (Individual child in children)
                {
                    // set location to childs birth location
                    // this will end up setting birth location of last child 
                    // as long as the location is at least Parish level
                    child.Status = Individual.CHILD;
                    if (isValidIndividual(child, censusDone, includeResidence, lostCousinsCheck, false))
                    {
                        result = true;
                        censusChildren.Add(child);
                        facts.AddRange(child.AllFacts);
                    }
                }
                children = censusChildren;
                this.BestLocation = FactLocation.BestLocation(facts, censusDate);
            }
            return result;
        }

        private bool isValidIndividual(Individual indiv, bool censusDone, bool includeResisdence, bool lostCousinsCheck, bool parentCheck)
        {
            if (indiv == null)
                return false;
            FamilyTree ft = FamilyTree.Instance;
            DateTime birth = indiv.BirthDate.StartDate;
            DateTime death = indiv.DeathDate.EndDate;
            if (birth < CensusDate.StartDate && death > CensusDate.StartDate && indiv.isCensusDone(CensusDate, includeResisdence) == censusDone)
            {
                if (lostCousinsCheck && indiv.isLostCousinEntered(CensusDate))
                    return false;
                if (parentCheck)
                {
                    // husband or wife with valid date range
                    return true;
                }
                else
                {
                    // individual is a child so remove if married before census date
                    return !ft.isMarried(indiv, CensusDate);
                }
            }
            else
            {
                return false;
            }
        }

        private bool isValidFamily()
        {
            if (MarriageDate.StartDate > CensusDate.EndDate)
                return false;
            if (FamilyID == "IND")
                return true; // allow solo individual families to be processed
            // don't process family if either parent is under 16
            //if(husband != null) rtb.AppendText("husband : " + husband.getAge(censusDate) + "\n");
            if (husband != null && husband.getMaxAge(CensusDate) < 16)
                return false;
            //if(wife  != null) rtb.AppendText("wife : " + wife.getAge(censusDate) + "\n");
            if (wife != null && wife.getMaxAge(CensusDate) < 16)
                return false;
            return true;
        }

        public int Relation
        {
            get
            {
                int relation = Individual.UNSET;
                foreach (Individual i in Members)
                {
                    if (i.RelationType != Individual.UNKNOWN && i.RelationType < relation)
                        relation = i.RelationType;
                }
                return relation == Individual.UNSET ? Individual.UNKNOWN : relation;
            }
        }
    }
}