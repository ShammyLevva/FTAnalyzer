using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            get { return IsCensusDone(CensusDate) ? BestLocation(CensusDate) : family.BestLocation; }
        }

        public CensusDate CensusDate
        {
            get { return family.CensusDate; }
        }

        public Age Age
        {
            get { return GetAge(CensusDate); }
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
                    if (f.IsValidCensus(CensusDate))
                        return f.CensusReference;
                return string.Empty;
            }
        }

        public DataGridViewCellStyle CellStyle { get; set; }

        public bool IsValidLocation(string location)
        {
            if (!CensusLocation.IsKnownCountry)
                return true;
            else if (Countries.IsUnitedKingdom(location))
                return CensusLocation.IsUnitedKingdom;
            else
                return CensusLocation.Country.Equals(location);
        }
    }
}
