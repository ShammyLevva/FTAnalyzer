using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace FTAnalyzer
{
    public class Individual : IComparable<Individual>,
        IDisplayIndividual, IDisplayLooseDeath, IDisplayLooseBirth, IExportIndividual,
        IDisplayColourCensus, IDisplayColourBMD, IDisplayMissingData
    {
        // define relation type from direct ancestor to related by marriage and 
        // MARRIAGEDB ie: married to a direct or blood relation
        public const int UNKNOWN = 1, DIRECT = 2, BLOOD = 4, MARRIEDTODB = 8, MARRIAGE = 16, UNSET = 32;
        public const string UNKNOWN_NAME = "UNKNOWN";

        public string IndividualID { get; private set; }
        private string forenames;
        private string surname;
        private string marriedName;
        private string gender;
        private string alias;
        private int relationType;
        private DoubleMetaphone surnameMetaphone;
        private DoubleMetaphone forenameMetaphone;
        public string Notes { get; private set; }
        public string StandardisedName { get; private set; }
        public bool HasParents { get; set; }
        public bool Infamily { get; set; }
        public bool IsFlaggedAsLiving { get; private set; }
        public Int64 Ahnentafel { get; set; }
        public string BudgieCode { get; set; }
        public string RelationToRoot { get; set; }
        public CommonAncestor CommonAncestor { get; set; }
        public string UnrecognisedCensusNotes { get; private set; }

        private IList<Fact> facts;
        private IList<Fact> errorFacts;
        private IList<FactLocation> locations;
        private IList<Family> familiesAsParent;
        private IList<ParentalRelationship> familiesAsChild;
        private Dictionary<string, Fact> preferredFacts;

        private Individual()
        {
            IndividualID = string.Empty;
            forenames = string.Empty;
            surname = string.Empty;
            forenameMetaphone = new DoubleMetaphone();
            surnameMetaphone = new DoubleMetaphone();
            marriedName = string.Empty;
            StandardisedName = string.Empty;
            UnrecognisedCensusNotes = string.Empty;
            IsFlaggedAsLiving = false;
            Gender = "U";
            alias = string.Empty;
            Ahnentafel = 0;
            BudgieCode = string.Empty;
            relationType = UNSET;
            RelationToRoot = string.Empty;
            CommonAncestor = null;
            Infamily = false;
            Notes = string.Empty;
            HasParents = false;
            ReferralFamilyID = string.Empty;
            facts = new List<Fact>();
            errorFacts = new List<Fact>();
            locations = new List<FactLocation>();
            familiesAsChild = new List<ParentalRelationship>();
            familiesAsParent = new List<Family>();
            preferredFacts = new Dictionary<string, Fact>();
        }

        public Individual(XmlNode node)
            : this()
        {
            IndividualID = node.Attributes["ID"].Value;
            Name = FamilyTree.GetText(node, "NAME", false);
            Gender = FamilyTree.GetText(node, "SEX", false);
            alias = FamilyTree.GetText(node, "ALIA", false);
            IsFlaggedAsLiving = node.SelectSingleNode("_FLGS/__LIVING") != null;
            forenameMetaphone = new DoubleMetaphone(Forename);
            surnameMetaphone = new DoubleMetaphone(Surname);
            Notes = FamilyTree.GetNotes(node);
            StandardisedName = FamilyTree.Instance.GetStandardisedName(IsMale, Forename);

            // Individual attributes
            AddFacts(node, Fact.NAME);
            AddFacts(node, Fact.ALIAS);
            AddFacts(node, Fact.PHYSICAL_DESC);
            AddFacts(node, Fact.EDUCATION);
            AddFacts(node, Fact.DEGREE);
            AddFacts(node, Fact.NAT_ID_NO);
            AddFacts(node, Fact.NATIONAL_TRIBAL);
            AddFacts(node, Fact.NUM_CHILDREN);
            AddFacts(node, Fact.NUM_MARRIAGE);
            AddFacts(node, Fact.OCCUPATION);
            AddFacts(node, Fact.PROPERTY);
            AddFacts(node, Fact.MEDICAL_CONDITION);
            AddFacts(node, Fact.REFERENCE);

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
            AddFacts(node, Fact.RESIDENCE);
            AddFacts(node, Fact.PROBATE);
            AddFacts(node, Fact.WILL);
            AddFacts(node, Fact.LEGATEE);
            AddFacts(node, Fact.GRADUATION);
            AddFacts(node, Fact.RETIREMENT);
            AddFacts(node, Fact.MILITARY);
            AddFacts(node, Fact.SERVICE_NUMBER);
            AddFacts(node, Fact.ELECTION);
            AddFacts(node, Fact.EMPLOYMENT);

            // Custom facts
            AddFacts(node, Fact.CUSTOM_EVENT);
            AddFacts(node, Fact.CUSTOM_FACT);
            AddFacts(node, Fact.UNKNOWN);

            if (Properties.GeneralSettings.Default.AutoCreateCensusFacts)
            {
                AddCensusSourceFacts();
                AddCensusNoteFacts();
            }
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
                this.forenameMetaphone = i.forenameMetaphone;
                this.surnameMetaphone = i.surnameMetaphone;
                this.marriedName = i.marriedName;
                this.StandardisedName = i.StandardisedName;
                this.IsFlaggedAsLiving = i.IsFlaggedAsLiving;
                this.gender = i.gender;
                this.alias = i.alias;
                this.Ahnentafel = i.Ahnentafel;
                this.BudgieCode = i.BudgieCode;
                this.relationType = i.relationType;
                this.RelationToRoot = i.RelationToRoot;
                this.Infamily = i.Infamily;
                this.Notes = i.Notes;
                this.HasParents = i.HasParents;
                this.ReferralFamilyID = i.ReferralFamilyID;
                this.CommonAncestor = i.CommonAncestor;
                this.facts = new List<Fact>(i.facts);
                this.errorFacts = new List<Fact>(i.errorFacts);
                this.locations = new List<FactLocation>(i.locations);
                this.familiesAsChild = new List<ParentalRelationship>(i.familiesAsChild);
                this.familiesAsParent = new List<Family>(i.familiesAsParent);
                this.preferredFacts = new Dictionary<string, Fact>(i.preferredFacts);
            }
        }

        #region Properties

        public bool HasRangedBirthDate
        {
            get { return BirthDate.DateType == FactDate.FactDateType.BET && BirthDate.StartDate.Year != BirthDate.EndDate.Year; }
        }

        public bool HasLostCousinsFact
        {
            get
            {
                foreach (Fact f in AllFacts)
                    if (f.FactType == Fact.LOSTCOUSINS || f.FactType == Fact.LC_FTA)
                        return true;
                return false;
            }
        }

        public int RelationType
        {
            get { return relationType; }
            set
            {
                if (relationType == UNKNOWN || relationType > value)
                    relationType = value;
            }
        }

        public bool IsBloodDirect
        {
            get { return relationType == BLOOD || relationType == DIRECT || relationType == MARRIEDTODB; }
        }

        public bool HasNotes
        {
            get { return Notes.Length > 0; }
        }

        public string Relation
        {
            get
            {
                switch (relationType)
                {
                    case DIRECT: return Ahnentafel == 1 ? "Root Person" : "Direct Ancestor";
                    case BLOOD: return "Blood Relation";
                    case MARRIAGE: return "By Marriage";
                    case MARRIEDTODB: return "Marr to Direct/Blood";
                    default: return "Unknown";
                }
            }
        }

        public IList<Fact> PersonalFacts
        {
            get { return this.facts; }
        }

        public IList<Fact> ErrorFacts
        {
            get { return this.errorFacts; }
        }

        public IList<Fact> AllFacts
        {
            get
            {
                List<Fact> allfacts = new List<Fact>();
                allfacts.AddRange(this.facts);
                foreach (Family f in familiesAsParent)
                    allfacts.AddRange(f.Facts);
                return allfacts;
            }
        }

        public IList<IDisplayFact> AllGeocodedFacts
        {
            get
            {
                List<IDisplayFact> allGeocodedFacts = new List<IDisplayFact>();
                foreach (Fact f in AllFacts)
                    if (f.Location.IsGeoCoded(false) && f.Location.GeocodeStatus != FactLocation.Geocode.UNKNOWN)
                        allGeocodedFacts.Add(new DisplayFact(this, f));
                allGeocodedFacts.Sort();
                return allGeocodedFacts;
            }
        }

        public int GeoLocationCount
        {
            get { return AllGeocodedFacts.Count; }
        }

        public IList<FactLocation> Locations
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

        public bool GenderMatches(Individual that)
        {
            return this.Gender == that.Gender || this.Gender == "U" || that.Gender == "U";
        }

        public string SortedName
        {
            get { return (surname + ", " + forenames).Trim(); }
        }

        public string Name
        {
            get
            {
                if (Properties.GeneralSettings.Default.ShowAliasInName && Alias.Length > 0)
                    return (forenames + (" '" + Alias + "' ") + surname).Trim();
                else
                    return (forenames + " " + surname).Trim();
            }
            private set
            {
                string name = value;
                int startPos = name.IndexOf("/"), endPos = name.LastIndexOf("/");
                if (startPos >= 0 && endPos > startPos)
                {
                    surname = name.Substring(startPos + 1, endPos - startPos - 1);
                    forenames = startPos == 0 ? Individual.UNKNOWN_NAME : name.Substring(0, startPos - 1);
                }
                else
                {
                    surname = Individual.UNKNOWN_NAME;
                    forenames = name;
                }
                if (surname == "?" || surname.ToLower() == "mnu" || surname.Length == 0)
                    surname = Individual.UNKNOWN_NAME;
                if(Properties.GeneralSettings.Default.TreatFemaleSurnamesAsUnknown && !IsMale && surname.StartsWith("(") && surname.EndsWith(")"))
                    surname = Individual.UNKNOWN_NAME;
                marriedName = surname;
            }
        }

        public string Forename
        {
            get
            {
                if (forenames == null)
                    return string.Empty;
                else
                {
                    int pos = forenames.IndexOf(' ');
                    return pos > 0 ? forenames.Substring(0, pos) : forenames;
                }
            }
        }

        public string ForenameMetaphone
        {
            get { return forenameMetaphone.PrimaryKey; }
        }

        public string Forenames
        {
            get
            {
                if (Properties.GeneralSettings.Default.ShowAliasInName && Alias.Length > 0)
                    return forenames + " '" + Alias + "' ";
                else
                    return forenames;
            }
        }

        public string Surname
        {
            get { return surname; }
        }

        public string SurnameMetaphone
        {
            get { return surnameMetaphone.PrimaryKey; }
        }

        public string MarriedName
        {
            get { return this.marriedName; }
            set { this.marriedName = value; }
        }

        public Fact BirthFact
        {
            get
            {
                Fact f = GetPreferredFact(Fact.BIRTH);
                if (f != null)
                    return f;
                f = GetPreferredFact(Fact.BIRTH_CALC);
                if (Properties.GeneralSettings.Default.UseBaptismDates)
                {
                    if (f != null)
                        return f;
                    f = GetPreferredFact(Fact.BAPTISM);
                    if (f != null)
                        return f;
                    f = GetPreferredFact(Fact.CHRISTENING);
                }
                return f;
            }
        }

        public FactDate BirthDate
        {
            get { return BirthFact == null ? FactDate.UNKNOWN_DATE : BirthFact.FactDate; }
        }

        public DateTime BirthStart
        {
            get { return BirthDate.StartDate != FactDate.MINDATE ? BirthDate.StartDate : BirthDate.EndDate; }
        }

        public FactLocation BirthLocation
        {
            get { return (BirthFact == null) ? FactLocation.UNKNOWN_LOCATION : BirthFact.Location; }
        }

        public Fact DeathFact
        {
            get
            {
                Fact f = GetPreferredFact(Fact.DEATH);
                if (Properties.GeneralSettings.Default.UseBurialDates)
                {
                    if (f != null)
                        return f;
                    f = GetPreferredFact(Fact.BURIAL);
                    if (f != null)
                        return f;
                    f = GetPreferredFact(Fact.CREMATION);
                }
                return f;
            }
        }

        public FactDate DeathDate
        {
            get { return DeathFact == null ? FactDate.UNKNOWN_DATE : DeathFact.FactDate; }
        }

        public DateTime DeathEnd
        {
            get { return DeathDate.EndDate != FactDate.MAXDATE ? DeathDate.EndDate : DeathDate.StartDate; }
        }

        public FactLocation DeathLocation
        {
            get { return DeathFact == null ? FactLocation.UNKNOWN_LOCATION : DeathFact.Location; }
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
                return occupation == null ? string.Empty : occupation.Comment;
            }
        }

        private int MaxAgeAtDeath
        {
            get { return GetAge(DeathDate).MaxAge; }
        }

        public Age LifeSpan
        {
            get { return GetAge(DateTime.Now); }
        }

        public FactDate LooseBirthDate
        {
            get
            {
                Fact loose = GetPreferredFact(Fact.LOOSEBIRTH);
                return loose == null ? FactDate.UNKNOWN_DATE : loose.FactDate;
            }
        }

        public string LooseBirth
        {
            get
            {
                FactDate fd = LooseBirthDate;
                return (fd.StartDate > fd.EndDate) ? "Alive facts after death, check data errors tab and children's births" : fd.ToString();
            }
        }

        public FactDate LooseDeathDate
        {
            get
            {
                Fact loose = GetPreferredFact(Fact.LOOSEDEATH);
                return loose == null ? FactDate.UNKNOWN_DATE : loose.FactDate;
            }
        }

        public string LooseDeath
        {
            get
            {
                FactDate fd = LooseDeathDate;
                return (fd.StartDate > fd.EndDate) ? "Alive facts after death, check data errors tab and children's births" : fd.ToString();
            }
        }

        public string IndividualRef
        {
            get { return IndividualID + ": " + Name; }
        }

        public string ServiceNumber
        {
            get
            {
                Fact service = GetPreferredFact(Fact.SERVICE_NUMBER);
                return service == null ? "" : service.Comment;
            }
        }

        public IList<Family> FamiliesAsParent
        {
            get { return familiesAsParent; }
        }

        public IList<ParentalRelationship> FamiliesAsChild
        {
            get { return familiesAsChild; }
        }

        public bool IsNaturalChildOf(Individual parent)
        {
            foreach (ParentalRelationship pr in FamiliesAsChild)
            {
                if (pr.Family != null)
                {
                    if (pr.IsNaturalFather && parent.IsMale && parent.Equals(pr.Father))
                        return true;
                    if (pr.IsNaturalMother && !parent.IsMale && parent.Equals(pr.Mother))
                        return true;
                }
            }
            return false;
        }

        public int FactCount(string factType)
        {
            return facts.Count(f => f.FactType == factType && f.FactErrorLevel == Fact.FactError.GOOD);
        }

        public int ResidenceCensusFactCount
        {
            get { return facts.Count(f => f.FactType == Fact.RESIDENCE && f.IsCensusFact); }
        }

        public int ErrorFactCount(string factType, Fact.FactError errorLevel)
        {
            return errorFacts.Count(f => f.FactType == factType && f.FactErrorLevel == errorLevel);
        }

        public string MarriageDates
        {
            get
            {
                string output = string.Empty;
                foreach (Family f in familiesAsParent)
                    if (f.MarriageDate.ToString() != string.Empty)
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
                    if (f.MarriageLocation.ToString() != string.Empty)
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

        public bool IsMale
        {
            get { return this.gender.Equals("M"); }
        }

        public bool IsInFamily
        {
            get { return Infamily; }
        }

        public bool IsMarried(FactDate fd)
        {
            if (IsSingleAtDeath())
                return false;
            return FamiliesAsParent.Any(f =>
            {
                FactDate marriage = f.GetPreferredFactDate(Fact.MARRIAGE);
                return (marriage != null && marriage.IsBefore(fd));
            });
        }

        public bool HasMilitaryFacts
        {
            get { return facts.Any(f => f.FactType == Fact.MILITARY || f.FactType == Fact.SERVICE_NUMBER); }
        }

        public bool HasCensusLocation(CensusDate when)
        {
            if (when == null) return false;
            foreach (Fact f in facts)
            {
                if (f.IsValidCensus(when) && f.Location.ToString().Length > 0)
                    return true;
            }
            return false;
        }

        public bool CensusFactExists(FactDate when, bool includeCreated)
        {
            if (when == null) return false;
            foreach (Fact f in facts)
            {
                if (f.IsValidCensus(when))
                {
                    if (!f.Created)
                        return true;
                    else
                        return includeCreated;
                }
            }
            return false;
        }

        public bool IsCensusDone(CensusDate when) { return IsCensusDone(when, true, true); }
        public bool IsCensusDone(CensusDate when, bool includeUnknownCountries) { return IsCensusDone(when, includeUnknownCountries, true); }
        public bool IsCensusDone(CensusDate when, bool includeUnknownCountries, bool checkCountry)
        {
            if (when == null) return false;
            foreach (Fact f in facts)
            {
                if (f.IsValidCensus(when))
                {
                    if (!checkCountry)
                        return true;
                    if (f.Location.CensusCountryMatches(when.Country, includeUnknownCountries))
                        return true;
                    if (Countries.IsUnitedKingdom(when.Country) && f.IsOverseasUKCensus(f.Country))
                        return true;
                }
            }
            return false;
        }

        public bool IsTaggedMissingCensus(CensusDate when)
        {
            if (when == null) return false;
            return facts.Any(x => x.FactType == Fact.MISSING && x.FactDate.Overlaps(when));
        }

        public string ReferralFamilyID { get; set; }

        public Fact GetCensusFact(Fact lcFact, bool includeCreated = true)
        {
            if (includeCreated)
                return facts.FirstOrDefault(x => x.IsCensusFact && x.FactDate.Overlaps(lcFact.FactDate));
            else
                return facts.FirstOrDefault(x => x.IsCensusFact && !x.Created && x.FactDate.Overlaps(lcFact.FactDate));
        }

        public bool IsLostCousinsEntered(CensusDate when) { return IsLostCousinsEntered(when, true); }
        public bool IsLostCousinsEntered(CensusDate when, bool includeUnknownCountries)
        {
            foreach (Fact f in facts)
            {
                if (f.IsValidLostCousins(when))
                {
                    if (f.Location.CensusCountryMatches(when.Country, includeUnknownCountries) || this.BestLocation(when).CensusCountryMatches(when.Country, includeUnknownCountries))
                        return true;
                    Fact censusFact = GetCensusFact(f);
                    if (censusFact != null)
                    {
                        if (when.Country.Equals(Countries.SCOTLAND) && Countries.IsEnglandWales(censusFact.Country))
                            return false;
                        if (Countries.IsUnitedKingdom(when.Country) && censusFact.IsOverseasUKCensus(censusFact.Country))
                            return true;
                    }
                }
            }
            return false;
        }

        private FactComparer factComparer = new FactComparer();

        public int DuplicateLCFacts
        {
            get
            {
                IEnumerable<Fact> lcFacts = AllFacts.Where(f => f.FactType == Fact.LOSTCOUSINS || f.FactType == Fact.LC_FTA);
                int distinctFacts = lcFacts.Distinct(factComparer).Count();
                return LostCousinsFacts - distinctFacts;
            }
        }

        public int DuplicateLCCensusFacts
        {
            get
            {
                IEnumerable<Fact> censusFacts = AllFacts.Where(f => f.IsLCCensusFact);
                int distinctFacts = censusFacts.Distinct(factComparer).Count();
                return censusFacts.Count() - distinctFacts;
            }
        }

        public bool MissingLostCousins(CensusDate censusDate, bool includeUnknownCountries)
        {
            bool isCensusDone = IsCensusDone(censusDate, includeUnknownCountries);
            bool isLostCousinsEntered = IsLostCousinsEntered(censusDate, includeUnknownCountries);
            return isCensusDone && !isLostCousinsEntered;
        }

        public int LostCousinsFacts
        {
            get { return facts.Count(f => f.FactType == Fact.LOSTCOUSINS || f.FactType == Fact.LC_FTA); }
        }

        public bool IsAlive(FactDate when)
        {
            return IsBorn(when) && !IsDeceased(when);
        }

        public bool IsBorn(FactDate when)
        {
            return !BirthDate.IsKnown || BirthDate.StartsBefore(when); // assume born if birthdate is unknown
        }

        public bool IsDeceased(FactDate when)
        {
            return DeathDate.IsKnown && DeathDate.IsBefore(when);
        }

        public bool IsSingleAtDeath()
        {
            Fact single = GetPreferredFact(Fact.UNMARRIED);
            return single != null || MaxAgeAtDeath < 16 || LifeSpan.MaxAge < 16;
        }

        public bool IsBirthKnown()
        {
            return BirthDate.IsKnown && BirthDate.IsExact;
        }

        public bool IsDeathKnown()
        {
            return DeathDate.IsKnown && DeathDate.IsExact;
        }

        #endregion

        #region Age Functions

        public Age GetAge(FactDate when)
        {
            return new Age(this, when);
        }

        public Age GetAge(FactDate when, string factType)
        {
            return (factType == Fact.BIRTH || factType == Fact.PARENT) ? Age.BIRTH : new Age(this, when);
        }

        public Age GetAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return GetAge(new FactDate(now));
        }

        public int GetMaxAge(FactDate when)
        {
            return GetAge(when).MaxAge;
        }

        public int GetMinAge(FactDate when)
        {
            return GetAge(when).MinAge;
        }

        public int GetMaxAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return GetMaxAge(new FactDate(now));
        }

        public int GetMinAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return GetMinAge(new FactDate(now));
        }
        #endregion

        #region Fact Functions

        private void AddFacts(XmlNode node, string factType)
        {
            XmlNodeList list = node.SelectNodes(factType);
            bool preferredFact = true;
            foreach (XmlNode n in list)
            {
                try
                {
                    if (factType != Fact.NAME || !preferredFact)
                    {  // don't add first name in file as a fact 
                        Fact f = new Fact(n, this, preferredFact);
                        AddFact(f);
                        if (f.GedcomAge != null && f.GedcomAge.CalculatedBirthDate != FactDate.UNKNOWN_DATE)
                        {
                            string reason = "Calculated from " + f.ToString() + " with Age: " + f.GedcomAge.GEDCOM_Age;
                            Fact calculatedBirth = new Fact(IndividualID, Fact.BIRTH_CALC, f.GedcomAge.CalculatedBirthDate, FactLocation.UNKNOWN_LOCATION, reason, false, true);
                            AddFact(calculatedBirth);
                        }
                    }
                }
                catch (InvalidXMLFactException ex)
                {
                    FamilyTree ft = FamilyTree.Instance;
                    ft.XmlErrorBox.AppendText("Error with Individual : " + IndividualRef + "\n" +
                        "       Invalid fact : " + ex.Message);
                }
                preferredFact = false;
            }
        }

        public void AddFact(Fact fact)
        {
            FamilyTree ft = FamilyTree.Instance;
            if (ft.FactBeforeBirth(this, fact))
                fact.SetError((int)FamilyTree.Dataerror.FACTS_BEFORE_BIRTH, Fact.FactError.ERROR,
                    fact.FactTypeDescription + " fact recorded: " + fact.FactDate + " before individual was born");
            if (ft.FactAfterDeath(this, fact))
                fact.SetError((int)FamilyTree.Dataerror.FACTS_AFTER_DEATH, Fact.FactError.ERROR,
                    fact.FactTypeDescription + " fact recorded: " + fact.FactDate + " after individual died");

            switch (fact.FactErrorLevel)
            {
                case Fact.FactError.GOOD:
                    AddGoodFact(fact);
                    break;
                case Fact.FactError.WARNINGALLOW:
                    AddGoodFact(fact);
                    errorFacts.Add(fact);
                    break;
                case Fact.FactError.WARNINGIGNORE:
                case Fact.FactError.ERROR:
                    errorFacts.Add(fact);
                    break;
            }
        }

        private void AddGoodFact(Fact fact)
        {
            facts.Add(fact);
            if (fact.Preferred && !preferredFacts.ContainsKey(fact.FactType))
                preferredFacts.Add(fact.FactType, fact);
            AddLocation(fact);
        }

        /// <summary>
        /// Checks the individual's node data to see if any census references exist in the source records
        /// </summary>
        private void AddCensusSourceFacts()
        {
            List<Fact> toAdd = new List<Fact>(); // we can't vary the facts collection whilst looping
            foreach (Fact f in facts)
            {
                if (!f.IsCensusFact && !CensusFactExists(f.FactDate, true))
                {
                    foreach (FactSource s in f.Sources)
                    {
                        CensusReference cr = new CensusReference(IndividualID, s.SourceTitle + " " + s.SourceText, true);
                        if (OKtoAddReference(cr, true))
                        {
                            cr.Fact.Sources.Add(s);
                            toAdd.Add(cr.Fact);
                            if (cr.IsLCCensusFact)
                                CreateLCFact(toAdd, cr);
                        }
                        else
                            UpdateCensusFactReference(cr);
                    }
                }
            }
            foreach (Fact f in toAdd)
                AddFact(f);
        }

        private void CreateLCFact(List<Fact> toAdd, CensusReference cr)
        {
            if (!IsLostCousinsEntered((CensusDate)cr.Fact.FactDate))
            {
                Fact lcFact = new Fact("LostCousins", Fact.LC_FTA, cr.Fact.FactDate, cr.Fact.Location, "Lost Cousins fact created by FTAnalyzer by recognising census ref " + cr.Reference, false, true);
                if (toAdd == null)
                    AddFact(lcFact);
                else
                    toAdd.Add(lcFact);
            }
        }

        /// <summary>
        /// Checks the notes against an individual to see if any census data exists
        /// </summary>
        private void AddCensusNoteFacts()
        {
            if (HasNotes)
            {
                bool checkNotes = true;
                string notes = CensusReference.ClearCommonPhrases(Notes);
                while (checkNotes)
                {
                    checkNotes = false;
                    CensusReference cr = new CensusReference(IndividualID, notes, false);
                    if (OKtoAddReference(cr, false))
                    {   // add census fact even if other created census facts exist for that year
                        AddFact(cr.Fact);
                        if (cr.IsLCCensusFact)
                            CreateLCFact(null, cr);
                    }
                    else
                        UpdateCensusFactReference(cr);
                    if (cr.MatchString.Length > 0)
                    {
                        int pos = notes.IndexOf(cr.MatchString, StringComparison.InvariantCultureIgnoreCase);
                        if (pos != -1)
                        {
                            notes = notes.Remove(pos, cr.MatchString.Length);
                            checkNotes = notes.Length > 0 && cr.MatchString.Length > 0;
                        }
                    }
                }
                if (notes.Length > 10) // no point recording really short notes 
                    UnrecognisedCensusNotes = IndividualID + ": " + Name + ". Notes : " + notes;
            }
        }

        private void UpdateCensusFactReference(CensusReference cr)
        {
            Fact censusFact = GetCensusFact(cr.Fact, false);
            if (censusFact != null && censusFact.CensusReference.Status.Equals(CensusReference.ReferenceStatus.BLANK) && (cr.IsKnownStatus))
                censusFact.SetCensusReferenceDetails(cr, CensusLocation.UNKNOWN, string.Empty);
        }

        private bool OKtoAddReference(CensusReference cr, bool includeCreated)
        {
            return cr.IsKnownStatus && !CensusFactExists(cr.Fact.FactDate, includeCreated);
        }

        private void AddLocation(Fact fact)
        {
            FactLocation loc = fact.Location;
            if (loc != null && !locations.Contains(loc))
            {
                locations.Add(loc);
                loc.AddIndividual(this);
            }
        }

        public Fact GetPreferredFact(string factType)
        {
            if (preferredFacts.ContainsKey(factType))
                return preferredFacts[factType];
            return facts.FirstOrDefault(f => f.FactType == factType);
        }

        public FactDate GetPreferredFactDate(string factType)
        {
            Fact f = GetPreferredFact(factType);
            return (f == null || f.FactDate == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
        }

        public IEnumerable<Fact> GetFacts(string factType)
        {
            // Returns all facts of the given type.
            return facts.Where(f => f.FactType == factType);
        }

        public string SurnameAtDate(FactDate date)
        {
            string name = surname;
            if (!IsMale)
            {
                foreach (Family marriage in familiesAsParent.OrderBy(f => f.MarriageDate))
                {
                    if ((marriage.MarriageDate.Equals(date) || marriage.MarriageDate.IsBefore(date)) && marriage.Husband != null)

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
            return FactLocation.BestLocation(AllFacts, when);
        }

        public Fact BestLocationFact(FactDate when, int limit)
        {
            // this returns a Fact a person was at for a given period
            return FactLocation.BestLocationFact(AllFacts, when, limit);
        }

        public bool IsAtLocation(FactLocation loc, int level)
        {
            foreach (Fact f in AllFacts)
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
            if (that == null)
                return -1;
            int res = this.surname.CompareTo(that.surname);
            if (res == 0)
            {
                res = this.forenames.CompareTo(that.forenames);
                if (res == 0)
                {
                    FactDate d1 = this.BirthDate;
                    FactDate d2 = that.BirthDate;
                    res = d1.CompareTo(d2);
                }
            }
            return res;
        }

        private int ColourCensusReport(CensusDate census)
        {
            if (BirthDate.IsAfter(census) || DeathDate.IsBefore(census) || GetAge(census).MinAge >= FactDate.MAXYEARS)
                return 0; // not alive - grey
            if (!IsCensusDone(census))
            {
                if (IsTaggedMissingCensus(census))
                    return 8;
                if (IsCensusDone(census, true, false) || (Countries.IsUnitedKingdom(census.Country) && IsCensusDone(census.EquivalentUSCensus, true, false)))
                    return 6; // checks if on census outside UK in census year or on prior year (to check US census)
                FactLocation location = BestLocation(census);
                if (CensusDate.IsLostCousinsCensusYear(census, true) && IsLostCousinsEntered(census) && !OutOfCountryCheck(census, location))
                    return 5; // LC entered but no census entered - orange
                if (location.IsKnownCountry)
                {
                    if (OutOfCountryCheck(census, location))
                        return 7; // Likely out of country on census date
                    else
                        return 1; // no census - red
                }
                else
                    return 1; // no census - red
            }
            if (!CensusDate.IsLostCousinsCensusYear(census, true))
                return 3; // census entered but not LCyear - green
            if (IsLostCousinsEntered(census))
                return 4; // census + Lost cousins entered - green
            else
            {
                // we have a census in a LC year but no LC event check if country is a LC country.
                int year = census.StartDate.Year;
                if (year == 1841 && IsCensusDone(CensusDate.EWCENSUS1841, false))
                    return 2; // census entered LC not entered - yellow
                if (year == 1880 && IsCensusDone(CensusDate.USCENSUS1880, false))
                    return 2; // census entered LC not entered - yellow
                if (year == 1881 &&
                    (IsCensusDone(CensusDate.EWCENSUS1881, false) || IsCensusDone(CensusDate.CANADACENSUS1881, false) ||
                     IsCensusDone(CensusDate.SCOTCENSUS1881, false)))
                    return 2; // census entered LC not entered - yellow
                if (year == 1911 && (IsCensusDone(CensusDate.EWCENSUS1911, false) || IsCensusDone(CensusDate.IRELANDCENSUS1911, false)))
                    return 2; // census entered LC not entered - yellow
                if (year == 1940 && IsCensusDone(CensusDate.USCENSUS1940, false))
                    return 2; // census entered LC not entered - yellow
                return 3;  // census entered and LCyear but not LC country - green
            }
        }

        #region Colour Census Values
        public int C1841
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1841); }
        }

        public int C1851
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1851); }
        }

        public int C1861
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1861); }
        }

        public int C1871
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1871); }
        }

        public int C1881
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1881); }
        }

        public int C1891
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1891); }
        }

        public int C1901
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1901); }
        }

        public int C1911
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1911); }
        }

        public int C1939
        {
            get { return ColourCensusReport(CensusDate.UKCENSUS1939); }
        }

        public int Ire1901
        {
            get { return ColourCensusReport(CensusDate.IRELANDCENSUS1901); }
        }

        public int Ire1911
        {
            get { return ColourCensusReport(CensusDate.IRELANDCENSUS1911); }
        }

        public int US1790
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1790); }
        }

        public int US1800
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1800); }
        }

        public int US1810
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1810); }
        }

        public int US1820
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1820); }
        }

        public int US1830
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1830); }
        }

        public int US1840
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1840); }
        }

        public int US1850
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1850); }
        }

        public int US1860
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1860); }
        }

        public int US1870
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1870); }
        }

        public int US1880
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1880); }
        }

        public int US1890
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1890); }
        }

        public int US1900
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1900); }
        }

        public int US1910
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1910); }
        }

        public int US1920
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1920); }
        }

        public int US1930
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1930); }
        }

        public int US1940
        {
            get { return ColourCensusReport(CensusDate.USCENSUS1940); }
        }

        public int Can1851
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1851); }
        }

        public int Can1861
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1861); }
        }

        public int Can1871
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1871); }
        }

        public int Can1881
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1881); }
        }

        public int Can1891
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1891); }
        }

        public int Can1901
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1901); }
        }

        public int Can1906
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1906); }
        }

        public int Can1911
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1911); }
        }

        public int Can1916
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1916); }
        }

        public int Can1921
        {
            get { return ColourCensusReport(CensusDate.CANADACENSUS1921); }
        }

        public int V1865
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1865); }
        }

        public int V1875
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1875); }
        }

        public int V1885
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1885); }
        }

        public int V1895
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1895); }
        }

        public int V1905
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1905); }
        }

        public int V1915
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1915); }
        }

        public int V1920
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1920); }
        }

        public int V1925
        {
            get { return ColourCensusReport(CensusDate.SCOTVALUATION1925); }
        }
        #endregion

        #region Colour BMD Values

        public ColourValues.BMDColour Birth
        {
            get { return BirthDate.DateStatus(false); }
        }

        public ColourValues.BMDColour BaptChri
        {
            get
            {
                FactDate baptism = GetPreferredFactDate(Fact.BAPTISM);
                FactDate christening = GetPreferredFactDate(Fact.CHRISTENING);
                ColourValues.BMDColour baptismStatus = baptism.DateStatus(true);
                ColourValues.BMDColour christeningStatus = christening.DateStatus(true);
                if (baptismStatus.Equals(ColourValues.BMDColour.EMPTY))
                    return christeningStatus;
                if (christeningStatus.Equals(ColourValues.BMDColour.EMPTY))
                    return baptismStatus;
                return (int)baptismStatus < (int)christeningStatus ? baptismStatus : christeningStatus;
            }
        }

        private ColourValues.BMDColour CheckMarriageStatus(Family fam)
        {
            // individual is a member of a family as parent so check family status
            if ((this.IndividualID == fam.HusbandID && fam.Wife == null) ||
                (this.IndividualID == fam.WifeID && fam.Husband == null))
                return ColourValues.BMDColour.NO_PARTNER; // no partner but has children
            else if (fam.GetPreferredFact(Fact.MARRIAGE) == null)
                return ColourValues.BMDColour.NO_MARRIAGE; // has a partner but no marriage fact
            else
                return fam.MarriageDate.DateStatus(false); // has a partner and a marriage so return date status
        }

        public ColourValues.BMDColour Marriage1
        {
            get
            {
                Family fam = Marriages(0);
                if (fam == null)
                {
                    if (MaxAgeAtDeath > 13 && GetPreferredFact(Fact.DIED_SINGLE) == null)
                        return ColourValues.BMDColour.NO_SPOUSE; // of marrying age but hasn't a partner nor died single
                    else
                        return ColourValues.BMDColour.EMPTY;
                }
                else
                {
                    return CheckMarriageStatus(fam);
                }
            }
        }

        public ColourValues.BMDColour Marriage2
        {
            get
            {
                Family fam = Marriages(1);
                if (fam == null)
                    return ColourValues.BMDColour.EMPTY;
                else
                    return CheckMarriageStatus(fam);
            }
        }

        public ColourValues.BMDColour Marriage3
        {
            get
            {
                Family fam = Marriages(2);
                if (fam == null)
                    return 0;
                else
                    return CheckMarriageStatus(fam);
            }
        }

        public string FirstMarriage
        {
            get { return MarriageString(0); }
        }

        public string SecondMarriage
        {
            get { return MarriageString(1); }
        }

        public string ThirdMarriage
        {
            get { return MarriageString(2); }
        }

        public FactDate FirstMarriageDate
        {
            get
            {
                Family fam = Marriages(0);
                if (fam == null)
                    return FactDate.UNKNOWN_DATE;
                else
                    return Marriages(0).MarriageDate;
            }
        }

        public FactDate SecondMarriageDate
        {
            get
            {
                Family fam = Marriages(1);
                if (fam == null)
                    return FactDate.UNKNOWN_DATE;
                else
                    return Marriages(1).MarriageDate;
            }
        }

        public FactDate ThirdMarriageDate
        {
            get
            {
                Family fam = Marriages(2);
                if (fam == null)
                    return FactDate.UNKNOWN_DATE;
                else
                    return Marriages(2).MarriageDate;
            }
        }

        public ColourValues.BMDColour Death
        {
            get
            {
                if (IsFlaggedAsLiving)
                    return ColourValues.BMDColour.ISLIVING;
                else if (!DeathDate.IsKnown && GetMaxAge(DateTime.Now) < FactDate.MAXYEARS)
                    if (GetMaxAge(DateTime.Now) < 90)
                        return ColourValues.BMDColour.EMPTY;
                    else
                        return ColourValues.BMDColour.OVER90;
                else
                    return DeathDate.DateStatus(false);
            }
        }

        public ColourValues.BMDColour CremBuri
        {
            get
            {
                FactDate cremation = GetPreferredFactDate(Fact.CREMATION);
                FactDate burial = GetPreferredFactDate(Fact.BURIAL);
                ColourValues.BMDColour cremationStatus = cremation.DateStatus(true);
                ColourValues.BMDColour burialStatus = burial.DateStatus(true);
                if (cremationStatus.Equals(ColourValues.BMDColour.EMPTY))
                    return burialStatus;
                if (burialStatus.Equals(ColourValues.BMDColour.EMPTY))
                    return cremationStatus;
                return (int)cremationStatus < (int)burialStatus ? cremationStatus : burialStatus;
            }
        }

        #endregion

        public float Score
        {
            get { return 0.0f; }
            // TODO Add scoring mechanism
        }

        public bool AliveOnAnyCensus(string country)
        {
            int ukCensus = C1841 + C1851 + C1861 + C1871 + C1881 + C1891 + C1901 + C1911 + C1939;
            if (country.Equals(Countries.UNITED_STATES))
                return (US1790 + US1800 + US1810 + US1810 + US1820 + US1830 + US1840 + US1850 + US1860 + US1870 + US1880 + US1890 + US1900 + US1910 + US1920 + US1930 + US1940) > 0;
            else if (country.Equals(Countries.CANADA))
                return (Can1851 + Can1861 + Can1871 + Can1881 + Can1891 + Can1901 + Can1906 + Can1911 + Can1916 + Can1921) > 0;
            else if (country.Equals(Countries.IRELAND))
                return (Ire1901 + Ire1911) > 0;
            else if (country.Equals(Countries.SCOTLAND))
                return (ukCensus + V1865 + V1875 + V1885 + V1895 + V1905 + V1915 + V1920 + V1925) > 0;
            else
                return ukCensus > 0;
        }

        public bool OutOfCountryOnAllCensus(string country)
        {
            if (country.Equals(Countries.UNITED_STATES))
                return CheckOutOfCountry("US1");
            else if (country.Equals(Countries.CANADA))
                return CheckOutOfCountry("Can1");
            else if (country.Equals(Countries.IRELAND))
                return CheckOutOfCountry("Ire1");
            else
                return CheckOutOfCountry("C1");
        }

        public bool OutOfCountryCheck(CensusDate census, FactLocation location)
        {
            return (Countries.IsUnitedKingdom(census.Country) && !location.IsUnitedKingdom) ||
                  (!Countries.IsUnitedKingdom(census.Country) && census.Country != location.Country);
        }

        public bool OutOfCountry(CensusDate census)
        {
            return CheckOutOfCountry(census.PropertyName);
        }

        private bool CheckOutOfCountry(string prefix)
        {
            foreach (PropertyInfo property in typeof(Individual).GetProperties())
            {
                if (property.Name.StartsWith(prefix))
                {
                    int value = (int)property.GetValue(this, null);
                    if (value != 0 && value != 6 && value != 7)
                        return false;
                }
            }
            return true;
        }

        public bool HasLostCousinsFactWithNoCensusFact
        {
            get
            {
                foreach (CensusDate censusDate in CensusDate.LOSTCOUSINS_CENSUS)
                {
                    if (IsLostCousinsEntered(censusDate, false) && !IsCensusDone(censusDate))
                        return true;
                }
                return false;
            }
        }
        
        public int LostCousinsCensusFactCount
        {
            get { return facts.Count(f => f.IsLCCensusFact); }
        }

        public int CensusFactCount
        {
            get { return facts.Count(f => f.IsCensusFact); }
        }

        public int CensusDateFactCount(CensusDate censusDate)
        {
            return facts.Count(f => f.IsValidCensus(censusDate));
        }

        public bool IsLivingError
        {
            get { return IsFlaggedAsLiving && DeathDate.IsKnown; }
        }

        public int CensusReferenceCount(CensusReference.ReferenceStatus referenceStatus)
        {
            return AllFacts.Count(f => f.IsCensusFact && f.CensusReference != null && f.CensusReference.Status.Equals(referenceStatus));
        }

        private Family Marriages(int number)
        {
            if (number < FamiliesAsParent.Count)
            {
                Family f = FamiliesAsParent.OrderBy(d => d.MarriageDate).ElementAt(number);
                return f;
            }
            return null;
        }

        private string MarriageString(int number)
        {
            Family marriage = Marriages(number);
            if (marriage == null)
                return string.Empty;
            else
            {
                if (this.IndividualID == marriage.HusbandID && marriage.Wife != null)
                    return "To " + marriage.Wife.Name + " : " + marriage.ToString();
                else if (this.IndividualID == marriage.WifeID && marriage.Husband != null)
                    return "To " + marriage.Husband.Name + " : " + marriage.ToString();
                else
                    return "Married : " + marriage.ToString();
            }
        }

        public int NumMissingLostCousins(string country)
        {
            if (!AliveOnAnyCensus(country)) return 0;
            int numMissing = CensusDate.LOSTCOUSINS_CENSUS.Count(x => this.IsCensusDone(x) && !this.IsLostCousinsEntered(x));
            return numMissing;
        }

        public override bool Equals(object that)
        {
            if (that is Individual)
                return this.IndividualID.Equals(((Individual)that).IndividualID);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return IndividualID + ": " + Name + " b." + BirthDate;
        }
    }
}
