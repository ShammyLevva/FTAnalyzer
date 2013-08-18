using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CensusIndividual : Individual, IDisplayCensus
    {
        private CensusFamily family;
        public int Position { get; private set; }

        public CensusIndividual(int position, Individual individual, CensusFamily family) : base(individual)
        {
            this.Position = position;
            this.family = family;
        }

        public string FamilyGed
        {
            get { return family.FamilyGed; }
        }

        public FactLocation CensusLocation
        {
            get { return family.BestLocation; }
        }

        public FactDate CensusDate
        {
            get { return family.CensusDate; }
        }

        public Age Age
        {
            get { return getAge(family.CensusDate); }
        }

        public bool isAlive
        {
            get { return family.CensusDate.isAfter(BirthDate) && family.CensusDate.isBefore(DeathDate); }
        }

        public string CensusSurname
        {
            get { return family.Surname; }
        }

        public bool IsValidLocation(string location)
        {
            if (!CensusLocation.isKnownCountry)
                return true;
            else if (Countries.isUnitedKingdom(location))
                return CensusLocation.isUnitedKingdom;
            else
                return CensusLocation.Country.Equals(location);
        }
    }
}
