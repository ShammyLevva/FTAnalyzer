using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusFamily : Family {

        private FactDate censusDate;
        private FactLocation bestLocation;

        public CensusFamily(Family f, FactDate censusDate) 
            : base(f)
        {
            this.censusDate = censusDate;
            this.bestLocation = null;
        }

        public bool process(FactDate censusDate, bool censusDone, bool includeResidence, bool lostCousinsCheck) {
            bool result = false;
            this.censusDate = censusDate;
            if(isValidFamily()) {
	            this.bestLocation = updateBestLocation(new FactLocation(), wife);
	            if (isValidIndividual(wife, censusDone, includeResidence, lostCousinsCheck, true)) {
			        result = true;
			        wife.Status = Individual.WIFE;
	            } else 
			        Wife = null;
		        // overwrite bestLocation by husbands as most commonly the family
		        // end up at husbands location after marriage
			    this.bestLocation = updateBestLocation(bestLocation, husband);
                if (isValidIndividual(husband, censusDone, includeResidence, lostCousinsCheck, true))
                {
			        result = true;
			        husband.Status = Individual.HUSBAND;
			    } else 
			        Husband = null;
			    // update bestLocation by marriage date as husband and wife 
			    // locations are often birth locations
			    Fact marriage = getPreferredFact(Fact.MARRIAGE);
			    this.bestLocation = updateBestLocation(bestLocation, marriage);
		        List<Individual> censusChildren = new List<Individual>();
			    // sort children oldest first
			    children.Sort(new CensusAgeComparator());
			    foreach (Individual child in children) {
			        // set location to childs birth location
			        // this will end up setting birth location of last child 
			        // as long as the location is at least Parish level
			        Fact birth = child.getPreferredFact(Fact.BIRTH);
			        this.bestLocation = updateBestLocation(bestLocation, birth);
			        child.Status = Individual.CHILD;
                    if (isValidIndividual(child, censusDone, includeResidence, lostCousinsCheck, false))
                    {
				        result = true;
				        censusChildren.Add(child);
				    }
			    }
			    children = censusChildren;
            }
		    return result;
        }
        
        private FactLocation updateBestLocation(FactLocation bestLocation, Individual ind) {
            if (ind != null) {
		        FactLocation location = ind.BestLocation;
		        if (location.Level >= FactLocation.PARISH ||
		            location.Level >= bestLocation.Level) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private FactLocation updateBestLocation(FactLocation bestLocation, Fact fact) {
            if (fact != null) {
		        FactLocation location = new FactLocation(fact.Place);
		        if (location.Level >= FactLocation.PARISH ||
			        location.Level >= bestLocation.Level) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private bool isValidIndividual(Individual indiv, bool censusDone, bool includeResisdence, bool lostCousinsCheck, bool parentCheck) {
            if (indiv == null)
                return false;
            FamilyTree ft = FamilyTree.Instance;
            DateTime birth = indiv.BirthDate.StartDate;
            DateTime death = indiv.DeathDate.EndDate;
		    if (birth < censusDate.StartDate && death > censusDate.StartDate && indiv.isCensusDone(censusDate, includeResisdence) == censusDone) {
                if(lostCousinsCheck && indiv.isLostCousinEntered(censusDate))
                    return false;
		        if (parentCheck) {
                    // husband or wife with valid date range
                    return true;
		        } else {
                    // individual is a child so remove if married before census date
                    return !ft.isMarried(indiv, censusDate);
                }
		    } else {
		        return false;
		    }
        }
        
        private bool isValidFamily() {
    	    if(MarriageDate.StartDate > censusDate.EndDate)
    		    return false;
            if (FamilyID == "IND")
                return true; // allow solo individual families to be processed
            // don't process family if either parent is under 16
            //if(husband != null) rtb.AppendText("husband : " + husband.getAge(censusDate) + "\n");
    	    if(husband != null && husband.getMaxAge(censusDate) < 16) 
    	        return false;
            //if(wife  != null) rtb.AppendText("wife : " + wife.getAge(censusDate) + "\n");
    	    if(wife != null && wife.getMaxAge(censusDate) < 16) 
    	        return false;
    	    return true;
        }
        
        public FactLocation BestLocation {
            get { return bestLocation; }
        }
        
        public int Relation {
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