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

        public CensusIndividual(int position, Individual individual, CensusFamily family)
            : base(individual)
        {
            this.Position = position;
            this.family = family;
        }

        public string FamilyID
        {
            get { return family.FamilyID; }
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
            get { return GetAge(family.CensusDate); }
        }

        public bool IsAlive()
        {
            return family.CensusDate.IsAfter(BirthDate) && family.CensusDate.IsBefore(DeathDate);
        }

        public string CensusSurname
        {
            get { return family.Surname; }
        }

        public string CensusReference
        {
            get 
            {
                foreach (Fact f in AllFacts)
                {
                    if (f.FactDate.IsKnown)
                    {
                        if (f.IsCensusFact && f.FactDate.Overlaps(family.CensusDate))
                            return f.CensusDetails;
                    }
                }
                return string.Empty;
            }
        }

        public bool IsValidLocation(string location)
        {
            if (!CensusLocation.isKnownCountry)
                return true;
            else if (Countries.IsUnitedKingdom(location))
                return CensusLocation.isUnitedKingdom;
            else
                return CensusLocation.Country.Equals(location);
        }
    }
}
