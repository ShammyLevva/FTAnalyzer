using System.Linq;
#if !__MAC__
using System.Windows.Forms;
#endif

namespace FTAnalyzer
{
    public class CensusIndividual : Individual, IDisplayCensus
    {
        public static readonly string HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child", UNKNOWNSTATUS = "Unknown";
        
        private CensusFamily family;
        public int Position { get; private set; }
        public string CensusStatus { get; private set; }

        public CensusIndividual(int position, Individual individual, CensusFamily family, string CensusStatus)
            : base(individual)
        {
            this.Position = position;
            this.family = family;
            this.CensusStatus = CensusStatus;
        }

        public int FamilyMembersCount
        {
            get { return family.Members.Count<CensusIndividual>(); }
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

        public string CensusName
        {
            get
            {
                if (CensusStatus == WIFE)
                    return Forenames + " " + MarriedName + (Surname.Length > 0 ? " (" + Surname + ")" : string.Empty);
                else
                    return Name;
            }
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
                    if (f.IsValidCensus(CensusDate) && f.CensusReference != null)
                        return f.CensusReference.Reference;
                return string.Empty;
            }
        }

#if !__MAC__
        public DataGridViewCellStyle CellStyle { get; set; }
#endif

        public bool IsValidLocation(string location)
        {
            if (!CensusLocation.IsKnownCountry)
                return true;
            else if (Countries.IsUnitedKingdom(location))
                return CensusLocation.IsUnitedKingdom;
            else
                return CensusLocation.Country.Equals(location);
        }

        public override string ToString()
        {
            return IndividualID + ": " + Name + " b." + BirthDate;
        }
    }
}
