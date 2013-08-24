using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FTAnalyzer
{
    public class Individual : IComparable<Individual>, 
        IDisplayIndividual, IDisplayLooseDeath, IDisplayLCReport, IExportIndividual
    {
        
        // define relation type from direct ancestor to related by marriage and 
        // MARRIAGEDB ie: married to a direct or blood relation
        public const int UNKNOWN = 1, DIRECT = 2, BLOOD = 4, MARRIEDTODB = 8, MARRIAGE = 16, UNSET = 32;
        public static readonly string HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child", UNKNOWNSTATUS = "Unknown";

        public string IndividualID { get; private set; }
        //private string gedcomID;
        private string forenames;
        private string surname;
        private string marriedName;
        private string gender;
        private string alias;
        private string status;
        private int relationType;
        private int ahnentafel;
        private string budgieCode;
        private bool infamily;
        private bool hasParents;

        private IList<Fact> facts;
        private IList<FactLocation> locations;
        private IList<Family> familiesAsParent;
        private IList<Family> familiesAsChild;
        
        public Individual (XmlNode node) {
            IndividualID = node.Attributes["ID"].Value;
            Name =   FamilyTree.GetText(node, "NAME");
            Gender = FamilyTree.GetText(node, "SEX");
            alias =  FamilyTree.GetText(node, "ALIA");
            relationType = UNSET;
            status = UNKNOWNSTATUS;
            ahnentafel = 0;
            budgieCode = string.Empty;
            infamily = false;
            hasParents = false;
            facts = new List<Fact>();
            locations = new List<FactLocation>();
            familiesAsChild = new List<Family>();
            familiesAsParent = new List<Family>();

            // Individual attributes
            AddFacts(node, Fact.PHYSICAL_DESC);
            AddFacts(node, Fact.EDUCATION);
            AddFacts(node, Fact.DEGREE);
            AddFacts(node, Fact.NAT_ID_NO);
            AddFacts(node, Fact.NATIONAL_TRIBAL);
            AddFacts(node, Fact.NUM_CHILDREN);
            AddFacts(node, Fact.NUM_MARRIAGE);
            AddFacts(node, Fact.OCCUPATION);
            AddFacts(node, Fact.POSSESSIONS);
            AddFacts(node, Fact.RESIDENCE);
            AddFacts(node, Fact.MEDICAL_CONDITION);

            // Individual events
            AddFacts(node, Fact.BIRTH);
            AddFacts(node, Fact.CHRISTENING);
            AddFacts(node, Fact.DEATH);
            AddFacts(node, Fact.BURIAL);
            AddFacts(node, Fact.CREMATION);
            AddFacts(node, Fact.ADOPTION);
            AddFacts(node, Fact.BAPTISM);
            AddFacts(node, Fact.BAR_MITZVAH);
            AddFacts(node, Fact.BAS_MITZVAH);
            AddFacts(node, Fact.BLESSING);
            AddFacts(node, Fact.ADULT_CHRISTENING);
            AddFacts(node, Fact.CONFIRMATION);
            AddFacts(node, Fact.FIRST_COMMUNION);
            AddFacts(node, Fact.ORDINATION);
            AddFacts(node, Fact.NATURALIZATION);
            AddFacts(node, Fact.EMIGRATION);
            AddFacts(node, Fact.IMMIGRATION);
            AddFacts(node, Fact.CENSUS);
            AddFacts(node, Fact.PROBATE);
            AddFacts(node, Fact.WILL);
            AddFacts(node, Fact.ENDOWMENT);
            AddFacts(node, Fact.LEGATEE);
            AddFacts(node, Fact.GRADUATION);
            AddFacts(node, Fact.RETIREMENT);
            AddFacts(node, Fact.MILITARY);
            AddFacts(node, Fact.ELECTION);
            AddFacts(node, Fact.EMPLOYMENT);

            // LDS facts
            AddFacts(node, Fact.BAPTISM_LDS);
            AddFacts(node, Fact.CONFIRMATIONLDS);
            AddFacts(node, Fact.ORDINANCE);
            AddFacts(node, Fact.SEALING_CHILD);

            // Custom facts
            AddFacts(node, Fact.CUSTOM_FACT);
        }

        internal Individual(Individual i)
        {
            if (i == null)
                FamilyTree.Instance.XmlErrorBox.AppendText("ERROR: Individual copy constructor called with null individual");
            else
            {
                this.IndividualID = i.IndividualID;
                this.forenames = i.forenames;
                this.surname = i.surname;
                this.marriedName = i.marriedName;
                this.gender = i.gender;
                this.alias = i.alias;
                this.status = i.status;
                this.ahnentafel = i.ahnentafel;
                this.budgieCode = i.budgieCode;
                this.relationType = i.relationType;
                this.infamily = i.infamily;
                this.facts = new List<Fact>(i.facts);
                this.locations = new List<FactLocation>(i.locations);
                this.familiesAsChild = new List<Family>(i.familiesAsChild);
                this.familiesAsParent = new List<Family>(i.familiesAsParent);
            }
        }

        #region Properties

        public bool Infamily
        {
            set { infamily = value; }
        }

        public bool HasParents
        {
            get { return hasParents; }
            set { hasParents = value; }
        }

        public bool HasRangedBirthDate
        {
            get { return BirthDate.DateType == FactDate.FactDateType.BET && BirthDate.StartDate.Year != BirthDate.EndDate.Year;  }
        }
        
        public int Ahnentafel
        { 
            get { return ahnentafel; } 
            set { ahnentafel = value; }
        }

        public string BudgieCode
        {
            get { return budgieCode; }
            set { budgieCode = value; }
        }

        public int RelationType 
        { 
            get { return relationType; }
            set { relationType = value; }
        }

        public bool isBloodDirect
        {
            get { return relationType == BLOOD ||
                    relationType == DIRECT || relationType == MARRIEDTODB;
            }
        }

        public string Relation
        {
            get {
                switch (relationType)
                {
                    case DIRECT: return ahnentafel == 1 ? "Root Person" : "Direct Ancestor";
                    case BLOOD: return "Blood Relation";
                    case MARRIAGE: return "By Marriage";
                    case MARRIEDTODB: return "Marr to Direct/Blood";
                    default: return "Unknown";
                }
            }
        }

        public IList<Fact> AllFacts 
        { 
            get { return this.facts; } 
        }

        public IList<FactLocation> AllLocations
        {
            get { return this.locations; }
        }

        public string Alias
        {
            get { return this.alias; }
        }

        public string Gender
        {
            get { return this.gender; }
            private set
            {
                gender = value;
                if (gender.Length == 0)
                    gender = "U";
            }
        }
        
        public string Name
        {
            get { return (forenames + " " + surname).Trim(); }
            private set
            {
                string name = value;
                int startPos = name.IndexOf("/"), endPos = name.LastIndexOf("/");
                if (startPos >= 0 && endPos > startPos)
                {
                    surname = name.Substring(startPos + 1, endPos - startPos - 1);
                    forenames = startPos == 0 ? "UNKNOWN" : name.Substring(0, startPos - 1);
                }
                else
                {
                    surname = "UNKNOWN";
                    forenames = name;
                }
                if (surname == "?")
                    surname = "UNKNOWN";
                marriedName = surname;
            }
        }

        public string Forename
        {
            get
            {
                if (forenames == null)
                    return "";
                else
                {
                    int pos = forenames.IndexOf(' ');
                    return pos > 0 ? forenames.Substring(0, pos) : forenames;
                }
            }
        }

        public string ForenameMetaphone
        {
            get 
            {
                DoubleMetaphone mp = new DoubleMetaphone(Forename);
                return mp.PrimaryKey;
            }
        }

        public string Forenames 
        { 
            get { return forenames; } 
        }
        
        public string Surname
        {
            get { return surname; }
        }

        public string SurnameMetaphone
        {
            get
            {
                DoubleMetaphone mp = new DoubleMetaphone(Surname);
                return mp.PrimaryKey;
            }
        }

        public string MarriedName
        {
            get { return this.marriedName; }
            set { this.marriedName = value; }
        }

        public string CensusName
        {
            get { return this.status == WIFE ? forenames + " " + marriedName + " (" + surname + ")" : Name; }
        }

        public FactDate BirthDate
        {
            get 
            { 
                FactDate f = GetPreferredFactDate(Fact.BIRTH);
                if (Properties.GeneralSettings.Default.UseBaptismDates)
                {
                    if (f != null && f != FactDate.UNKNOWN_DATE) 
                        return f;
                    f = GetPreferredFactDate(Fact.BAPTISM);
                    if (f != null && f != FactDate.UNKNOWN_DATE) 
                        return f;
                    f = GetPreferredFactDate(Fact.CHRISTENING);
                }
                return (f == null) ? FactDate.UNKNOWN_DATE : f;
            }
        }

        public FactLocation BirthLocation
        {
            get
            {
                Fact f = GetPreferredFact(Fact.BIRTH);
                return (f == null) ? null : f.Location;
            }
        }

        public FactDate DeathDate
        {
            get
            {
                FactDate f = GetPreferredFactDate(Fact.DEATH);
                return (f == null) ? FactDate.UNKNOWN_DATE : f;
            }
        }

        public FactLocation DeathLocation
        {
            get
            {
                Fact f = GetPreferredFact(Fact.DEATH);
                return (f == null) ? null : f.Location;
            }
        }

        public FactDate BurialDate
        {
            get
            {
                Fact f = GetPreferredFact(Fact.BURIAL);
                return (f == null) ? null : f.FactDate;
            }
        }

        public string Occupation
        {
            get
            {
                Fact occupation = GetPreferredFact(Fact.OCCUPATION);
                return occupation == null ? "" : occupation.Comment;
            }
        }

        public string Status
        {
            get { return status; }
            set { this.status = value; }
        }

        //public FactLocation BestLocation
        //{
        //    get
        //    {
        //        int bestLevel = -1;
        //        FactLocation result = new FactLocation();
        //        foreach (Fact f in facts)
        //        {
        //            FactLocation l = new FactLocation(f.Place);
        //            if (l.Level > bestLevel)
        //            {
        //                result = l;
        //                bestLevel = l.Level;
        //            }
        //        }
        //        return result;
        //    }
        //}

        //public FactLocation FilterLocation
        //{
        //    get { return BestLocation; }
        //}

        private int MaxAgeAtDeath
        {
            get
            {
                return GetAge(DeathDate).MaxAge;
            }
        }

        public Age LifeSpan
        {
            get
            {
                return getAge(DateTime.Now);
            }
        }

        public string LooseDeath
        {
            get
            {
                Fact loose = GetPreferredFact(Fact.LOOSEDEATH);
                FactDate fd =  loose == null ? FactDate.UNKNOWN_DATE : loose.FactDate;
                return (fd.StartDate > fd.EndDate) ? "Start date after end date: check data errors tab" : fd.ToString();
            }
        }

        public string IndividualRef
        {
            get
            {
                return IndividualID + ": " + Name;
            }
        }

        public IList<Family> FamiliesAsParent
        {
            get { return familiesAsParent; }
            set { familiesAsParent = value.ToList(); }
        }

        public IList<Family> FamiliesAsChild
        {
            get { return familiesAsChild; }
            set { familiesAsChild = value.ToList(); }
        }

        public int CensusFactCount
        {
            get
            {
                int censusFacts = 0;
                foreach (Fact f in facts)
                {
                    if (f.FactType == Fact.CENSUS)
                        censusFacts++;
                }
                return censusFacts;
            }
        }

        public int ResiFactCount
        {
            get
            {
                int resiFacts = 0;
                foreach (Fact f in facts)
                {
                    if (f.FactType == Fact.RESIDENCE)
                        resiFacts++;
                }
                return resiFacts;
            }
        }

        public string MarriageDates
        {
            get
            {
                string output = string.Empty;
                foreach (Family f in familiesAsParent)
                    if(f.MarriageDate.ToString() != string.Empty)
                        output += f.MarriageDate + "; ";
                if (output.Length > 0)
                    return output.Substring(0, output.Length - 2); // remove trailing ;
                else
                    return output;
            }
        }

        public string MarriageLocations
        {
            get
            {
                string output = string.Empty;
                foreach (Family f in familiesAsParent)
                    if(f.MarriageLocation.ToString() != string.Empty)
                        output += f.MarriageLocation + "; ";
                if (output.Length > 0)
                    return output.Substring(0, output.Length - 2); // remove trailing ;
                else
                    return output;
            }
        }

        public int MarriageCount { get { return familiesAsParent.Count; } }

        public int ChildrenCount { get { return familiesAsParent.Sum(x => x.Children.Count); } }

        #endregion

        #region Boolean Tests

        public bool isMale
        {
            get { return this.gender.Equals("M"); }
        }

        public bool isInFamily
        {
            get { return infamily; }
        }

        public bool isCensusDone(FactDate when, bool includeResidence)
        {
            foreach (Fact f in facts)
            {
                if (f.FactType == Fact.CENSUS && f.FactDate.Overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true;
                if (includeResidence && f.FactType == Fact.RESIDENCE && f.FactDate.Overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true; 
            }
            return false;
        }

        public bool isLostCousinEntered(FactDate when)
        {
            foreach (Fact f in facts)
            {
                if (f.FactType == Fact.LOSTCOUSINS && f.FactDate.Overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true;
            }
            return false;
        }

        //public bool isLostCousinsCensus(FactDate when)
        //{
        //    foreach (Fact f in facts)
        //    {
        //        if (f.FactType == Fact.CENSUS || f.FactType == Fact.RESIDENCE)
        //        {
        //            if (f.FactDate.Overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
        //            {
        //                bool supportedLocation = f.Location.SupportedLocation(FactLocation.COUNTRY);
        //                if (f.Location.isUnitedKingdom || !supportedLocation)
        //                {
        //                    if ((f.FactDate.Overlaps(CensusDate.UKCENSUS1841) ||
        //                         f.FactDate.Overlaps(CensusDate.UKCENSUS1881) ||
        //                         f.FactDate.Overlaps(CensusDate.UKCENSUS1911)) &&
        //                        (when == CensusDate.UKCENSUS1841 || when == CensusDate.UKCENSUS1881 || when == CensusDate.UKCENSUS1911))
        //                        return true;
        //                }
        //                else if (f.Location.Country == Countries.SCOTLAND)
        //                {
        //                    if (f.FactDate.Overlaps(CensusDate.UKCENSUS1881) && when == CensusDate.UKCENSUS1881)
        //                        return true;
        //                }
        //                else if (f.Location.Country == Countries.CANADA)
        //                {
        //                    if (f.FactDate.Overlaps(CensusDate.CANADACENSUS1881) && when == CensusDate.CANADACENSUS1881)
        //                        return true;
        //                }
        //                else if (f.Location.Country == Countries.UNITED_STATES)
        //                {
        //                    if ((f.FactDate.Overlaps(CensusDate.USCENSUS1880) ||
        //                         f.FactDate.Overlaps(CensusDate.USCENSUS1940)) && 
        //                        (when == CensusDate.USCENSUS1880 || when == CensusDate.USCENSUS1940))
        //                        return true;
        //                }
        //                else if (f.Location.Country == Countries.IRELAND)
        //                {
        //                    if (f.FactDate.Overlaps(CensusDate.IRELANDCENSUS1911) && when == CensusDate.IRELANDCENSUS1911)
        //                        return true;
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        public bool isDeceased(FactDate when)
        {
            return DeathDate != FactDate.UNKNOWN_DATE && DeathDate.IsBefore(when);
        }
        
        public bool isSingleAtDeath() {
            Fact single = GetPreferredFact(Fact.UNMARRIED);
            return single != null || MaxAgeAtDeath < 16 || LifeSpan.MaxAge < 16;
        }

        public bool isBirthKnown()
        {
            return BirthDate != FactDate.UNKNOWN_DATE && BirthDate.IsExact();
        }

        public bool isDeathKnown()
        {
            return DeathDate != FactDate.UNKNOWN_DATE && DeathDate.IsExact();
        }

        #endregion

        #region Age Functions

        public Age GetAge(FactDate when) {
            return new Age(this, when);
        }
        
        public Age getAge(DateTime when) {
            string now = FactDate.Format(FactDate.FULL, when);
            return GetAge(new FactDate(now));
        }
        
        public int getMaxAge(FactDate when) {
            return GetAge(when).MaxAge;
        }
        
        public int getMinAge(FactDate when) {
            return GetAge(when).MinAge;
        }

        public int getMaxAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return getMaxAge(new FactDate(now));
        }

        public int getMinAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return getMinAge(new FactDate(now));
        }
        #endregion

        #region Fact Functions

        private void AddFacts(XmlNode node, string factType)
        {
            XmlNodeList list = node.SelectNodes(factType);
            foreach(XmlNode n in list) {
                try
                {
                    addFact(new Fact(n, IndividualRef));
                }
                catch (InvalidXMLFactException ex)
                {
                    FamilyTree ft = FamilyTree.Instance;
                    ft.XmlErrorBox.AppendText("Error with Individual : " + IndividualRef + "\n" +
                        "       Invalid fact : " + ex.Message);
                }
            }
        }

        public void addFact(Fact fact) {
            facts.Add(fact);
            FactLocation loc = fact.Location;
            if (loc != null && !locations.Contains(loc))
            {
                locations.Add(loc);
                loc.AddIndividual(this);
            }
        }
                
        public Fact GetPreferredFact(string factType) {
            // Returns the first fact of the given type.
            // This assumes the original GEDCOM file has the preferred fact first in the list
            // as per the GEDCOM 5.5 specification.
            return GetFacts(factType).FirstOrDefault();
        }
        
        public FactDate GetPreferredFactDate (string factType) {
            Fact f = GetPreferredFact(factType);
            return (f == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
        }
        
        public IEnumerable<Fact> GetFacts(string factType) {
            // Returns all facts of the given type.
            return facts.Where(f => f.FactType == factType);
        }

        public Family FirstMarriage
        {
            get
            {
                FactDate firstMarriageDate = new FactDate(FactDate.MAXDATE.ToString());
                foreach (Family marriage in familiesAsParent)
                {
                    if (marriage.MarriageDate != null && marriage.MarriageDate.IsBefore(firstMarriageDate))
                        return marriage;
                }
                return null;
            }
        }

        public string SurnameAtDate(FactDate date)
        {
            string name = surname;
            if (!isMale)
            {
                foreach (Family marriage in familiesAsParent.OrderBy(f => f.MarriageDate))
                {
                    if (marriage.MarriageDate.IsBefore(date) && marriage.Husband != null)
                        name = marriage.Husband.surname;
                }
            }
            return name;
        }

        #endregion

        #region Location functions
        
        public FactLocation BestLocation(FactDate when)
        {
            // this returns a Location a person was at for a given period
            List<Fact> allFacts = new List<Fact>();
            allFacts.AddRange(facts);
            foreach (Family f in familiesAsParent)
            {
                allFacts.AddRange(f.Facts);
            }
            return FactLocation.BestLocation(allFacts, when);
        }

        public bool isAtLocation(FactLocation loc, int level)
        {
            foreach (Fact f in facts)
            {
                if (f.Location.Equals(loc, level))
                    return true;
            }
            return false;
        }
        #endregion

        public void FixIndividualID(int length)
        {
            try
            {
                IndividualID = IndividualID.Substring(0, 1) + IndividualID.Substring(1).PadLeft(length, '0');
            }
            catch (Exception)
            {  // don't error if Individual isn't of type Ixxxx
            }
        }

        public int CompareTo(Individual that)
        {
            // Individuals are naturally ordered by surname, then forenames,
            // then date of birth.
            int res = this.surname.CompareTo(that.surname);
            if (res == 0) {
                res = this.forenames.CompareTo(that.forenames);
                if (res == 0) {
                    Fact b1 = this.GetPreferredFact(Fact.BIRTH);
                    Fact b2 = that.GetPreferredFact(Fact.BIRTH);
                    FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.FactDate;
                    FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.FactDate;
                    res = d1.CompareTo(d2);
                }
            }
            return res;
        }

        private int LCReport(FactDate census)
        {
            
            if (BirthDate.IsAfter(census) || DeathDate.IsBefore(census))
                return 0; // not alive - grey
            if (!isCensusDone(census, true))
            {
                if (CensusDate.IsLostCousinsCensusYear(census) && isLostCousinEntered(census))
                    return 5; // LC entered but no census entered - orange
                else
                    return 1; // no census - red
            }
            if (!CensusDate.IsLostCousinsCensusYear(census))
                return 3; // census entered but not LCyear - green
            if(isLostCousinEntered(census))
                return 4; // census + Lost cousins entered - green
            else
                return 2; // census entered LC not entered - yellow
        }

        public int C1841
        {
            get { return LCReport(CensusDate.UKCENSUS1841); }
        }

        public int C1851
        {
            get { return LCReport(CensusDate.UKCENSUS1851); }
        }

        public int C1861
        {
            get { return LCReport(CensusDate.UKCENSUS1861); }
        }

        public int C1871
        {
            get { return LCReport(CensusDate.UKCENSUS1871); }
        }

        public int C1881
        {
            get { return LCReport(CensusDate.UKCENSUS1881); }
        }

        public int C1891
        {
            get { return LCReport(CensusDate.UKCENSUS1891); }
        }

        public int C1901
        {
            get { return LCReport(CensusDate.UKCENSUS1901); }
        }

        public int C1911
        {
            get { return LCReport(CensusDate.UKCENSUS1911); }
        }
    }
}
