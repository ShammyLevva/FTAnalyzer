using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class Individual : IComparable<Individual>, 
        IDisplayIndividual, IDisplayLooseDeath, IDisplayLCReport, IExportIndividual
    {
        
        // define relation type from direct ancestor to related by marriage and 
        // MARRIAGEDB ie: married to a direct or blood relation
        public const int UNKNOWN = 1, DIRECT = 2, BLOOD = 4, MARRIEDTODB = 8, MARRIAGE = 16, UNSET = 32;
        public static readonly string HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child", UNKNOWNSTATUS = "Unknown";
        
        private string individualID;
        private string gedcomID;
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

        private List<Fact> facts;
        private List<FactLocation> locations;
        private List<Family> familiesAsParent;
        private List<Family> familiesAsChild;
        
        public Individual (XmlNode node) {
            gedcomID = node.Attributes["ID"].Value;
            individualID = gedcomID;
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

            addFacts(node, Fact.BIRTH);
            addFacts(node, Fact.CHRISTENING);
            addFacts(node, Fact.DEATH);
            addFacts(node, Fact.BURIAL);
            addFacts(node, Fact.CENSUS);
            addFacts(node, Fact.RESIDENCE);
            addFacts(node, Fact.OCCUPATION);
            addFacts(node, Fact.CUSTOM_FACT);
        }

        internal Individual(Individual i)
        {
            if (i == null)
                FamilyTree.Instance.XmlErrorBox.AppendText("ERROR: Individual copy constructor called with null individual");
            else
            {
                this.individualID = i.individualID;
                this.gedcomID = i.gedcomID;
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
                this.familiesAsChild = i.familiesAsChild;
                this.familiesAsParent = i.familiesAsParent;
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
            get { return BirthDate.Type == FactDate.FactDateType.BET && BirthDate.StartDate.Year != BirthDate.EndDate.Year;  }
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

        public string IndividualID 
        { 
            get { return individualID; } 
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

        public List<Fact> AllFacts 
        { 
            get { return this.facts; } 
        }

        public List<FactLocation> AllLocations
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
        
        public string GedcomID
        {
            get { return gedcomID; }
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
        
        public string DateOfBirth
        {
            get
            {
                Fact f = getPreferredFact(Fact.BIRTH);
                return (f == null) ? "" : f.Datestring;
            }
        }

        public FactDate BirthDate
        {
            get 
            { 
                FactDate f = getPreferredFactDate(Fact.BIRTH);
                return (f == null) ? FactDate.UNKNOWN_DATE : f;
            }
        }

        public FactLocation BirthLocation
        {
            get
            {
                Fact f = getPreferredFact(Fact.BIRTH);
                return (f == null) ? null : f.Location;
            }
        }

        public FactDate DeathDate
        {
            get
            {
                FactDate f = getPreferredFactDate(Fact.DEATH);
                return (f == null) ? FactDate.UNKNOWN_DATE : f;
            }
        }

        public string DateOfDeath
        {
            get
            {
                Fact f = getPreferredFact(Fact.DEATH);
                return (f == null) ? "" :
                    ((f.Datestring == null) ? "" : f.Datestring);
            }
        }

        public FactLocation DeathLocation
        {
            get
            {
                Fact f = getPreferredFact(Fact.DEATH);
                return (f == null) ? null : f.Location;
            }
        }

        public FactDate BurialDate
        {
            get
            {
                Fact f = getPreferredFact(Fact.BURIAL);
                return (f == null) ? null : f.FactDate;
            }
        }

        public string Occupation
        {
            get
            {
                Fact occupation = getPreferredFact(Fact.OCCUPATION);
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
                return getAge(DeathDate).MaxAge;
            }
        }

        public Age CurrentAge
        {
            get
            {
                return getAge(DateTime.Now);
            }
        }

        public FactDate LooseDeath
        {
            get
            {
                Fact loose = getPreferredFact(Fact.LOOSEDEATH);
                return loose == null ? new FactDate(FactDate.MINDATE, FactDate.MAXDATE) : loose.FactDate;
            }
        }

        public string IndividualRef
        {
            get
            {
                return gedcomID + ": " + Name;
            }
        }

        public List<Family> FamiliesAsParent
        {
            get { return familiesAsParent; }
            set { familiesAsParent = value; }
        }

        public List<Family> FamiliesAsChild
        {
            get { return familiesAsChild; }
            set { familiesAsChild = value; }
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
                if (f.FactType == Fact.CENSUS && f.FactDate.overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true;
                if (includeResidence && f.FactType == Fact.RESIDENCE && f.FactDate.overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true; 
            }
            return false;
        }

        public bool isLostCousinEntered(FactDate when)
        {
            foreach (Fact f in facts)
            {
                if (f.FactType == Fact.LOSTCOUSINS && f.FactDate.overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    return true;
            }
            return false;
        }

        public bool isLostCousinsCensus(FactDate when)
        {
            foreach (Fact f in facts)
            {
                if (f.FactType == Fact.CENSUS || f.FactType == Fact.RESIDENCE)
                {
                    if (f.FactDate.overlaps(when) && !f.FactDate.Equals(FactDate.UNKNOWN_DATE))
                    {
                        bool supportedLocation = f.Location.SupportedLocation(FactLocation.COUNTRY);
                        if (f.Location.country == FactLocation.ENGLAND || f.Location.country == FactLocation.WALES
                            || f.Location.country == FactLocation.UNITED_KINGDOM || !supportedLocation)
                        {
                            if ((f.FactDate.overlaps(CensusDate.UKCENSUS1841) ||
                                 f.FactDate.overlaps(CensusDate.UKCENSUS1881) ||
                                 f.FactDate.overlaps(CensusDate.UKCENSUS1911)) &&
                                (when == CensusDate.UKCENSUS1841 || when == CensusDate.UKCENSUS1881 || when == CensusDate.UKCENSUS1911))
                                return true;
                        }
                        else if (f.Location.country == FactLocation.SCOTLAND)
                        {
                            if (f.FactDate.overlaps(CensusDate.UKCENSUS1881) && when == CensusDate.UKCENSUS1881)
                                return true;
                        }
                        else if (f.Location.country == FactLocation.CANADA)
                        {
                            if (f.FactDate.overlaps(CensusDate.CANADACENSUS1881) && when == CensusDate.CANADACENSUS1881)
                                return true;
                        }
                        else if (f.Location.country == FactLocation.UNITED_STATES)
                        {
                            if ((f.FactDate.overlaps(CensusDate.USCENSUS1880) ||
                                 f.FactDate.overlaps(CensusDate.USCENSUS1940)) && 
                                (when == CensusDate.USCENSUS1880 || when == CensusDate.USCENSUS1940))
                                return true;
                        }
                        else if (f.Location.country == FactLocation.IRELAND)
                        {
                            if (f.FactDate.overlaps(CensusDate.IRELANDCENSUS1911) && when == CensusDate.IRELANDCENSUS1911)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool isDeceased(FactDate when)
        {
            return DeathDate != FactDate.UNKNOWN_DATE && DeathDate.isBefore(when);
        }
        
        public bool isSingleAtDeath() {
            Fact single = getPreferredFact(Fact.UNMARRIED);
            return single != null || MaxAgeAtDeath < 16 || CurrentAge.MaxAge < 16;
        }

        public bool isBirthKnown()
        {
            return BirthDate != FactDate.UNKNOWN_DATE && BirthDate.isExact();
        }

        public bool isDeathKnown()
        {
            return DeathDate != FactDate.UNKNOWN_DATE && DeathDate.isExact();
        }

        #endregion

        #region Age Functions

        public Age getAge(FactDate when) {
            return new Age(this, when);
        }
        
        public Age getAge(DateTime when) {
            string now = FactDate.Format(FactDate.FULL, when);
            return getAge(new FactDate(now));
        }
        
        public int getMaxAge(FactDate when) {
            return getAge(when).MaxAge;
        }
        
        public int getMinAge(FactDate when) {
            return getAge(when).MinAge;
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

        private void addFacts(XmlNode node, string factType)
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
                
        public Fact getPreferredFact(string factType) {
            // Returns the first fact of the given type.
            // This assumes the original GEDCOM file has the preferred fact first in the list
            // as per the GEDCOM 5.5 specification.
            foreach(Fact f in facts)
            {
                if (f.FactType == factType)
                    return f;
            }
            return null;
        }
        
        public FactDate getPreferredFactDate (string factType) {
            Fact f = getPreferredFact(factType);
            return (f == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
        }
        
        public List<Fact> getFacts(string factType) {
            // Returns all facts of the given type.
            List<Fact> result = new List<Fact>();
            foreach(Fact f in facts)
            {
                if (f.FactType == factType)
                    result.Add(f);
            }
            return result;
        }

        public Family FirstMarriage
        {
            get
            {
                FactDate firstMarriageDate = new FactDate(FactDate.MAXDATE.ToString());
                Family firstMarriage = null;
                foreach (Family marriage in familiesAsParent)
                {
                    if (marriage.MarriageDate != null && marriage.MarriageDate.isBefore(firstMarriageDate))
                        firstMarriage = marriage;
                }
                return firstMarriage;
            }
        }

        public string SurnameAtDate(FactDate date)
        {
            string name = surname;
            if (!isMale)
            {
                List<Family> families = familiesAsParent;
                families.Sort(new FamilyDateComparer());
                foreach (Family marriage in families)
                {
                    if (marriage.MarriageDate.isBefore(date) && marriage.husband != null)
                        name = marriage.husband.surname;
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
                allFacts.AddRange(f.AllFacts);
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
                individualID = individualID.Substring(0, 1) + individualID.Substring(1).PadLeft(length, '0');
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
                    Fact b1 = this.getPreferredFact(Fact.BIRTH);
                    Fact b2 = that.getPreferredFact(Fact.BIRTH);
                    FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.FactDate;
                    FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.FactDate;
                    res = d1.CompareTo(d2);
                }
            }
            return res;
        }

        private int LCReport(FactDate census)
        {
            
            if (BirthDate.isAfter(census) || DeathDate.isBefore(census))
                return 0; // not alive - grey
            if (!isCensusDone(census, true))
                return 1; // no census - red
            if (!isLostCousinsCensus(census))
                return 3; // not LCyear - green
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