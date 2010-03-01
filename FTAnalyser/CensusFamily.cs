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
        
        public CensusFamily (FamilyLocal fam) : base(fam) {
        }
        
        public bool process(FactDate censusDate) {
            bool result = false;
            this.censusDate = censusDate;
            if(checkFamily()) {
	            this.bestLocation = updateBestLocation(new Location(), wife);
	            if (checkIndividual(wife)) {
			        result = true;
			        wife.setStatus(Individual.WIFE);
	            } else 
			        setWife(null);
		        // overwrite bestLocation by husbands as most commonly the family
		        // end up at husbands location after marriage
			    this.bestLocation = updateBestLocation(bestLocation, husband);
			    if (checkIndividual(husband)) {
			        result = true;
			        husband.setStatus(Individual.HUSBAND);
			    } else 
			        setHusband(null);
			    // update bestLocation by marriage date as husband and wife 
			    // locations are often birth locations
			    Fact marriage = getPreferredFact(Fact.MARRIAGE);
			    this.bestLocation = updateBestLocation(bestLocation, marriage);
		        List<Individual> censusChildren = new List<Individual>();
			    // sort children oldest first
			    Collections.sort(children, new CensusAgeComparator());
			    foreach (Individual child in children) {
			        // set location to childs birth location
			        // this will end up setting birth location of last child 
			        // as long as the location is at least Parish level
			        Fact birth = child.getPreferredFact(Fact.BIRTH);
			        this.bestLocation = updateBestLocation(bestLocation, birth);
			        child.setStatus(Individual.CHILD);
			        if (checkIndividual(child)) {
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
		        Location location = ind.getBestLocation();
		        if (location.getLevel() >= Location.PARISH ||
		            location.getLevel() >= bestLocation.getLevel()) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private Location updateBestLocation(Location bestLocation, Fact fact) {
            if (fact != null) {
		        Location location = new Location(fact.getLocation());
		        if (location.getLevel() >= Location.PARISH ||
			        location.getLevel() >= bestLocation.getLevel()) 
		        	    return location;
            }
            return bestLocation;
        }
        
        private bool checkIndividual(Individual indiv) {
            if (indiv == null)
                return false;
            Client client = Client.getInstance();
            Calendar birth = (indiv.getBirthDate() == null) ? 
		            FactDate.MINDATE : indiv.getBirthDate().getStartDate();
		    Calendar death = (indiv.getDeathDate() == null) ?
		            FactDate.MAXDATE : indiv.getDeathDate().getEndDate();
		    if (birth.before(censusDate.getStartDate()) && 
		            death.after(censusDate.getStartDate()) && 
		            !indiv.isCensusDone(censusDate)) {
		        if (indiv.getStatus().Equals(Individual.CHILD)) { 
		            // individual is a child so remove if married before census date
		    	    return !client.isMarried(memberID, indiv, censusDate);
		        } else {
		            // husband or wife with valid date range
		            return true;
		        }
		    } else {
		        return false;
		    }
        }
        
        private bool checkFamily() {
    	    if(getMarriageDate().getStartDate() > censusDate.getEndDate())
    		    return false;
            // don't process family if either parent is under 16
    	    //if(husband != null) System.out.println("husband : " + husband.getAge(censusDate));
    	    if(husband != null && husband.getMaxAge(censusDate) < 16) 
    	        return false;
    	    //if(wife  != null) System.out.println("wife : " + wife.getAge(censusDate));
    	    if(wife != null && wife.getMaxAge(censusDate) < 16) 
    	        return false;
    	    return true;
        }
        
        public Location getBestLocation() {
            return bestLocation;
        }
        
        public int getRelation() {
            int relation = Individual.UNSET;
            foreach (Individual i in getMembers()) {
                if (i.getRelation() != Individual.UNKNOWN &&
                    i.getRelation() < relation) 
                	    relation = i.getRelation();
            }
            return relation == Individual.UNSET ? Individual.UNKNOWN : relation;
        }
    }
}