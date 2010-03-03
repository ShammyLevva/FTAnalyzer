using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class CensusFamily : Family {

        private FactDate censusDate;
        private Location bestLocation;

        public bool process(FactDate censusDate) {
            bool result = false;
            this.censusDate = censusDate;
            if(isValidFamily()) {
	            this.bestLocation = updateBestLocation(new Location(), wife);
	            if (isValidIndividual(wife)) {
			        result = true;
			        wife.Status = Individual.WIFE;
	            } else 
			        Wife = null;
		        // overwrite bestLocation by husbands as most commonly the family
		        // end up at husbands location after marriage
			    this.bestLocation = updateBestLocation(bestLocation, husband);
			    if (isValidIndividual(husband)) {
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
			        if (isValidIndividual(child)) {
				        result = true;
				        censusChildren.Add(child);
				    }
			    }
			    children = censusChildren;
            }
		    return result;
        }
        
        private Location updateBestLocation(Location bestLocation, Individual ind) {
            if (ind != null) {
		        Location location = ind.BestLocation;
		        if (location.Level >= Location.PARISH ||
		            location.Level >= bestLocation.Level) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private Location updateBestLocation(Location bestLocation, Fact fact) {
            if (fact != null) {
		        Location location = new Location(fact.Place);
		        if (location.Level >= Location.PARISH ||
			        location.Level >= bestLocation.Level) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private bool isValidIndividual(Individual indiv) {
            if (indiv == null)
                return false;
            FamilyTree ft = FamilyTree.Instance;
            DateTime birth = (indiv.BirthDate == null) ? FactDate.MINDATE : indiv.BirthDate.StartDate;
            DateTime death = (indiv.DeathDate == null) ? FactDate.MAXDATE : indiv.DeathDate.EndDate;
		    if (birth < censusDate.StartDate && 
		        death > censusDate.StartDate && 
		            !indiv.isCensusDone(censusDate)) {
		        if (indiv.Status == Individual.CHILD) { 
		            // individual is a child so remove if married before census date
		    	    return !ft.isMarried(indiv, censusDate);
		        } else {
		            // husband or wife with valid date range
		            return true;
		        }
		    } else {
		        return false;
		    }
        }
        
        private bool isValidFamily() {
    	    if(MarriageDate.StartDate > censusDate.EndDate)
    		    return false;
            // don't process family if either parent is under 16
    	    //if(husband != null) Console.WriteLine("husband : " + husband.getAge(censusDate));
    	    if(husband != null && husband.getMaxAge(censusDate) < 16) 
    	        return false;
    	    //if(wife  != null) Console.WriteLine("wife : " + wife.getAge(censusDate));
    	    if(wife != null && wife.getMaxAge(censusDate) < 16) 
    	        return false;
    	    return true;
        }
        
        public Location BestLocation {
            get { return bestLocation; }
        }
        
        public int Relation {
            get
            {
                int relation = Individual.UNSET;
                foreach (Individual i in Members)
                {
                    if (i.RelationType != Individual.UNKNOWN &&
                        i.RelationType < relation)
                        relation = i.RelationType;
                }
                return relation == Individual.UNSET ? Individual.UNKNOWN : relation;
            }
        }
    }
}