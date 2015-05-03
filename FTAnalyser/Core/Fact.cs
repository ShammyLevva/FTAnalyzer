using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Globalization;
using FTAnalyzer.Utilities;
using System.Text.RegularExpressions;

namespace FTAnalyzer
{
    public class Fact
    {
        public const string ADOPTION = "ADOP", ANNULMENT = "ANUL", BAPTISM = "BAPM",
                BAR_MITZVAH = "BARM", BAS_MITZVAH = "BASM", BIRTH = "BIRT",
                BLESSING = "BLESS", BURIAL = "BURI", CENSUS = "CENS", CENSUS_FTA = "_CENSFTA",
                CHRISTENING = "CHR", ADULT_CHRISTENING = "CHRA", CONFIRMATION = "CONF",
                CREMATION = "CREM", DEATH = "DEAT", PHYSICAL_DESC = "DSCR",
                DIVORCE = "DIV", DIVORCE_FILED = "DIVF", EDUCATION = "EDUC",
                EMIGRATION = "EMIG", ENGAGEMENT = "ENGA", FIRST_COMMUNION = "FCOM",
                GRADUATION = "GRAD", IMMIGRATION = "IMMI", NAT_ID_NO = "IDNO",
                NATIONAL_TRIBAL = "NATI", NUM_CHILDREN = "NCHI", NUM_MARRIAGE = "NMR",
                LEGATEE = "LEGA", MARRIAGE_BANN = "MARB", MARR_CONTRACT = "MARC",
                MARR_LICENSE = "MARL", MARRIAGE = "MARR", MARR_SETTLEMENT = "MARS",
                NATURALIZATION = "NATU", OCCUPATION = "OCCU", PROPERTY = "PROP",
                ORDINATION = "ORDN", PROBATE = "PROB", RESIDENCE = "RESI",
                RETIREMENT = "RETI", WILL = "WILL", SEPARATION = "_SEPR",
                MILITARY = "_MILT", ELECTION = "_ELEC", DEGREE = "_DEG",
                EMPLOYMENT = "_EMPLOY", MEDICAL_CONDITION = "_MDCL", NAME = "NAME",
                CUSTOM_EVENT = "EVEN", CUSTOM_FACT = "FACT", SERVICE_NUMBER = "_MILTID",
                REFERENCE = "REFN", UNKNOWN = "UNKN", ALIAS = "ALIA";

        public const string CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                LOOSEDEATH = "*LOOSED", LOOSEBIRTH = "*LOOSEB", FAMILYSEARCH = "*IGI",
                CONTACT = "*CONT", ARRIVAL = "*ARRI", DEPARTURE = "*DEPT", PARENT = "*PARENT",
                CHILDREN = "*CHILDREN", CHANGE = "*CHNG", LOSTCOUSINS = "*LOST",
                DIED_SINGLE = "*SINGLE", MISSING = "*MISSING", CHILDREN1911 = "CHILDREN1911",
                REPORT = "*REPORT", WORLD_EVENT = "*WORLD_EVENT", BIRTH_CALC = "_BIRTHCALC";

        public static readonly ISet<string> LOOSE_BIRTH_FACTS = new HashSet<string>(new string[] {
            CHRISTENING, BAPTISM, RESIDENCE, WITNESS, EMIGRATION, IMMIGRATION, ARRIVAL, DEPARTURE, 
            EDUCATION, DEGREE, ADOPTION, BAR_MITZVAH, BAS_MITZVAH, ADULT_CHRISTENING, CONFIRMATION, 
            FIRST_COMMUNION, ORDINATION, NATURALIZATION, GRADUATION, RETIREMENT, LOSTCOUSINS, 
            MARR_CONTRACT, MARR_LICENSE, MARR_SETTLEMENT, MARRIAGE, MARRIAGE_BANN, DEATH, 
            CREMATION, BURIAL, CENSUS, BIRTH_CALC
                    });

        public static readonly ISet<string> LOOSE_DEATH_FACTS = new HashSet<string>(new string[] {
            CENSUS, RESIDENCE, WITNESS, EMIGRATION, IMMIGRATION, ARRIVAL, DEPARTURE, EDUCATION,
            DEGREE, ADOPTION, BAR_MITZVAH, BAS_MITZVAH, ADULT_CHRISTENING, CONFIRMATION, FIRST_COMMUNION,
            ORDINATION, NATURALIZATION, GRADUATION, RETIREMENT, LOSTCOUSINS
                    });

        public static readonly ISet<string> RANGED_DATE_FACTS = new HashSet<string>(new string[] {
            EDUCATION, OCCUPATION, RESIDENCE, RETIREMENT, MILITARY, ELECTION, DEGREE, EMPLOYMENT, MEDICAL_CONDITION
                    });

        public static readonly ISet<string> IGNORE_LONG_RANGE = new HashSet<string>(new string[] {
            MARRIAGE, CHILDREN
                    });

        public static readonly ISet<string> CREATED_FACTS = new HashSet<string>(new string[] {
            CENSUS_FTA, CHILDREN, PARENT, BIRTH_CALC
                    });

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        static Fact()
        {
            // special tags
            CUSTOM_TAGS.Add("IGI SEARCH", FAMILYSEARCH);
            CUSTOM_TAGS.Add("CHILDLESS", CHILDLESS);
            CUSTOM_TAGS.Add("CONTACT", CONTACT);
            CUSTOM_TAGS.Add("WITNESS", WITNESS);
            CUSTOM_TAGS.Add("WITNESSES", WITNESS);
            CUSTOM_TAGS.Add("UNMARRIED", UNMARRIED);
            CUSTOM_TAGS.Add("FRIENDS", UNMARRIED);
            CUSTOM_TAGS.Add("PARTNERS", UNMARRIED);
            CUSTOM_TAGS.Add("UNKNOWN", UNKNOWN);
            CUSTOM_TAGS.Add("UNKNOWN-BEGIN", UNKNOWN);
            CUSTOM_TAGS.Add("ARRIVAL", ARRIVAL);
            CUSTOM_TAGS.Add("DEPARTURE", DEPARTURE);
            CUSTOM_TAGS.Add("RECORD CHANGE", CHANGE);
            CUSTOM_TAGS.Add("LOST COUSINS", LOSTCOUSINS);
            CUSTOM_TAGS.Add("LOSTCOUSINS", LOSTCOUSINS);
            CUSTOM_TAGS.Add("DIED SINGLE", DIED_SINGLE);
            CUSTOM_TAGS.Add("MISSING", MISSING);
            CUSTOM_TAGS.Add("CHILDREN STATUS", CHILDREN1911);

            // convert custom tags to normal tags
            CUSTOM_TAGS.Add("CENSUS 1841", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1851", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1861", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1871", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1881", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1891", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1901", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1911", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1790", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1800", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1810", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1820", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1830", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1840", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1850", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1860", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1870", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1880", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1890", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1900", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1910", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1920", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1930", CENSUS);
            CUSTOM_TAGS.Add("CENSUS 1940", CENSUS);
            CUSTOM_TAGS.Add("BIRTH REG", BIRTH);
            CUSTOM_TAGS.Add("BIRTH", BIRTH);
            CUSTOM_TAGS.Add("MARRIAGE REG", MARRIAGE);
            CUSTOM_TAGS.Add("MARRIAGE", MARRIAGE);
            CUSTOM_TAGS.Add("DEATH REG", DEATH);
            CUSTOM_TAGS.Add("DEATH", DEATH);
            CUSTOM_TAGS.Add("CHRISTENING", CHRISTENING);
            CUSTOM_TAGS.Add("BURIAL", BURIAL);
            CUSTOM_TAGS.Add("FUNERAL", BURIAL);
            CUSTOM_TAGS.Add("CREMATION", CREMATION);
            CUSTOM_TAGS.Add("PROBATE", PROBATE);
            CUSTOM_TAGS.Add("PROBATE DATE", PROBATE);
            CUSTOM_TAGS.Add("RESIDENCE", RESIDENCE);
            CUSTOM_TAGS.Add("CENSUS", CENSUS);

            // Legacy 8 default fact types
            CUSTOM_TAGS.Add("ALT. BIRTH", BIRTH);
            CUSTOM_TAGS.Add("ALT. CHRISTENING", CHRISTENING);
            CUSTOM_TAGS.Add("ALT. DEATH", DEATH);
            CUSTOM_TAGS.Add("ALT. BURIAL", BURIAL);
            CUSTOM_TAGS.Add("ALT. MARRIAGE", MARRIAGE);
            CUSTOM_TAGS.Add("DIVORCE FILING", DIVORCE_FILED);
            CUSTOM_TAGS.Add("DEGREE", DEGREE);
            CUSTOM_TAGS.Add("ELECTION", ELECTION);
            CUSTOM_TAGS.Add("EMPLOYMENT", EMPLOYMENT);
            CUSTOM_TAGS.Add("MARRIAGE LICENCE", MARR_LICENSE);
            CUSTOM_TAGS.Add("MARRIAGE LICENSE", MARR_LICENSE);
            CUSTOM_TAGS.Add("MARRIAGE CONTRACT", MARR_CONTRACT);
            CUSTOM_TAGS.Add("MEDICAL", MEDICAL_CONDITION);
            CUSTOM_TAGS.Add("MILITARY", MILITARY);
            CUSTOM_TAGS.Add("MILITARY SERVICE", MILITARY);
            CUSTOM_TAGS.Add("PROPERTY", PROPERTY);

            // Create list of Comment facts
            COMMENT_FACTS.Add(NAME);
            COMMENT_FACTS.Add(OCCUPATION);
            COMMENT_FACTS.Add(MILITARY);
            COMMENT_FACTS.Add(SERVICE_NUMBER);
            COMMENT_FACTS.Add(RETIREMENT);
            COMMENT_FACTS.Add(WILL);
            COMMENT_FACTS.Add(ELECTION);
            COMMENT_FACTS.Add(CHILDLESS);
            COMMENT_FACTS.Add(WITNESS);
            COMMENT_FACTS.Add(UNMARRIED);
            COMMENT_FACTS.Add(UNKNOWN);
            COMMENT_FACTS.Add(FAMILYSEARCH);
            COMMENT_FACTS.Add(LOSTCOUSINS);
            COMMENT_FACTS.Add(MISSING);
            COMMENT_FACTS.Add(DEGREE);
            COMMENT_FACTS.Add(EDUCATION);
            COMMENT_FACTS.Add(GRADUATION);
            COMMENT_FACTS.Add(DEPARTURE);
            COMMENT_FACTS.Add(ARRIVAL);
            COMMENT_FACTS.Add(EMPLOYMENT);
            COMMENT_FACTS.Add(MEDICAL_CONDITION);
            COMMENT_FACTS.Add(ORDINATION);
            COMMENT_FACTS.Add(PHYSICAL_DESC);
            COMMENT_FACTS.Add(PROPERTY);
            COMMENT_FACTS.Add(PARENT);
            COMMENT_FACTS.Add(CHILDREN);
            COMMENT_FACTS.Add(ALIAS);
            COMMENT_FACTS.Add(CHILDREN1911);
        }

        internal static string GetFactTypeDescription(string factType)
        {
            switch (factType)
            {
                case NAME: return "Alternate Name";
                case ALIAS: return "Also known as";
                case ADOPTION: return "Adoption";
                case ANNULMENT: return "Annulment";
                case BAPTISM: return "Baptism";
                case BAR_MITZVAH: return "Bar mitzvah";
                case BAS_MITZVAH: return "Bas mitzvah";
                case BIRTH: return "Birth";
                case BIRTH_CALC: return "Birth (Calc from Age)";
                case BLESSING: return "Blessing";
                case BURIAL: return "Burial";
                case CENSUS: return "Census";
                case CENSUS_FTA: return "Census (FTAnalyzer)";
                case CHRISTENING: return "Christening";
                case ADULT_CHRISTENING: return "Adult christening";
                case CONFIRMATION: return "Confirmation";
                case CREMATION: return "Cremation";
                case DEATH: return "Death";
                case PHYSICAL_DESC: return "Physical description";
                case DIVORCE: return "Divorce";
                case DIVORCE_FILED: return "Divorce filed";
                case EDUCATION: return "Education";
                case EMIGRATION: return "Emigration";
                case ENGAGEMENT: return "Engagement";
                case FIRST_COMMUNION: return "First communion";
                case GRADUATION: return "Graduation";
                case IMMIGRATION: return "Immigration";
                case NAT_ID_NO: return "National identity no.";
                case NATIONAL_TRIBAL: return "Nationality";
                case NUM_CHILDREN: return "Number of children";
                case NUM_MARRIAGE: return "Number of marriages";
                case LEGATEE: return "Legatee";
                case MARRIAGE_BANN: return "Marriage banns";
                case MARR_CONTRACT: return "Marriage contract";
                case MARR_LICENSE: return "Marriage license";
                case MARRIAGE: return "Marriage";
                case MARR_SETTLEMENT: return "Marriage settlement";
                case NATURALIZATION: return "Naturalization";
                case OCCUPATION: return "Occupation";
                case PROPERTY: return "Property";
                case ORDINATION: return "Ordination";
                case PROBATE: return "Probate";
                case RESIDENCE: return "Residence";
                case RETIREMENT: return "Retirement";
                case WILL: return "Will";
                case SEPARATION: return "Separation";
                case MILITARY: return "Military service";
                case SERVICE_NUMBER: return "Military service number";
                case ELECTION: return "Election";
                case DEGREE: return "Degree";
                case EMPLOYMENT: return "Employment";
                case MEDICAL_CONDITION: return "Medical condition";
                case CHILDLESS: return "Childless";
                case UNMARRIED: return "Unmarried";
                case WITNESS: return "Witness";
                case LOOSEDEATH: return "Loose death";
                case LOOSEBIRTH: return "Loose birth";
                case FAMILYSEARCH: return "Familysearch";
                case CONTACT: return "Contact";
                case ARRIVAL: return "Arrival";
                case DEPARTURE: return "Departure";
                case CHANGE: return "Record change";
                case LOSTCOUSINS: return "Lost Cousins";
                case DIED_SINGLE: return "Died Single";
                case UNKNOWN: return "UNKNOWN";
                case PARENT: return "Parental Info";
                case CHILDREN: return "Child Born";
                case REFERENCE: return "Reference ID";
                case MISSING: return "Missing";
                case CHILDREN1911: return "Children Status";
                case REPORT: return "Fact Report";
                case CUSTOM_EVENT: return "Event";
                case WORLD_EVENT: return "World Event";
                case "": return "UNKNOWN";
                default: return EnhancedTextInfo.ToTitleCase(factType);
            }
        }

        public enum FactError { GOOD = 0, WARNINGALLOW = 1, WARNINGIGNORE = 2, ERROR = 3, QUESTIONABLE = 4, IGNORE = 5 };

        #region Constructors

        private Fact(string reference, bool preferred)
        {
            this.FactType = string.Empty;
            this.FactDate = FactDate.UNKNOWN_DATE;
            this.Comment = string.Empty;
            this.Place = string.Empty;
            this.Location = FactLocation.UNKNOWN_LOCATION;
            this.Sources = new List<FactSource>();
            this.CensusReference = CensusReference.UNKNOWN;
            this.CertificatePresent = false;
            this.FactErrorLevel = FactError.GOOD;
            this.FactErrorMessage = string.Empty;
            this.FactErrorNumber = 0;
            this.GedcomAge = null;
            this.Created = false;
            this.Tag = string.Empty;
            this.Preferred = preferred;
        }

        public Fact(XmlNode node, Family family, bool preferred)
            : this(family.FamilyRef, preferred)
        {
            Individual = null;
            Family = family;
            CreateFact(node, family.FamilyRef, preferred);
        }

        public Fact(XmlNode node, Individual ind, bool preferred)
            : this(ind.IndividualID, preferred)
        {
            Individual = ind;
            Family = null;
            CreateFact(node, ind.IndividualRef, preferred);
        }

        private void CreateFact(XmlNode node, string reference, bool preferred)
        {
            if (node != null)
            {
                FamilyTree ft = FamilyTree.Instance;
                try
                {
                    FactType = FixFactTypes(node.Name);
                    string factDate = FamilyTree.GetText(node, "DATE", false);
                    this.FactDate = new FactDate(factDate, reference);
                    this.Preferred = preferred;
                    if (FactType.Equals(CUSTOM_EVENT) || FactType.Equals(CUSTOM_FACT))
                    {
                        string tag = FamilyTree.GetText(node, "TYPE", false).ToUpper();
                        string factType;
                        if (CUSTOM_TAGS.TryGetValue(tag, out factType))
                        {
                            FactType = factType;
                            CheckCensusDate(tag);
                        }
                        else
                        {
                            FactType = Fact.UNKNOWN;
                            FamilyTree.Instance.CheckUnknownFactTypes(tag);
                            Tag = tag;
                        }
                    }
                    SetCommentAndLocation(FactType, FamilyTree.GetText(node, false), FamilyTree.GetText(node, "PLAC", false),
                        FamilyTree.GetText(node, "PLAC/MAP/LATI", false), FamilyTree.GetText(node, "PLAC/MAP/LONG", false));
                    SetAddress(FactType, node);

                    // only check UK census dates for errors as those are used for colour census
                    if (FactType.Equals(CENSUS) && Location.IsUnitedKingdom)
                        CheckCensusDate("Census");

                    // need to check residence after setting location
                    if (FactType.Equals(RESIDENCE) && Properties.GeneralSettings.Default.UseResidenceAsCensus)
                        CheckResidenceCensusDate();

                    // check Children Status is valid
                    if (FactType.Equals(CHILDREN1911))
                        CheckValidChildrenStatus(node);

                    // now iterate through source elements of the fact finding all sources
                    XmlNodeList list = node.SelectNodes("SOUR");
                    foreach (XmlNode n in list)
                    {
                        if (n.Attributes["REF"] != null)
                        {   // only process sources with a reference
                            string srcref = n.Attributes["REF"].Value;
                            FactSource source = ft.GetSourceID(srcref);
                            if (source != null)
                            {
                                Sources.Add(source);
                                source.AddFact(this);
                            }
                            else
                                ft.XmlErrorBox.AppendText("Source " + srcref + " not found." + "\n");
                        }
                        if (IsCensusFact)
                            this.CensusReference = new CensusReference(this, n);
                    }
                    // if we have checked the sources and no census ref see if its been added as a comment to this fact
                    if (IsCensusFact)
                    {
                        CheckForSharedFacts(node);
                        if (this.CensusReference == CensusReference.UNKNOWN)
                            this.CensusReference = new CensusReference(this, node);
                    }
                    if (FactType == DEATH)
                    {
                        Comment = FamilyTree.GetText(node, "CAUS", true);
                        if (node.FirstChild != null && node.FirstChild.Value == "Y" && !FactDate.IsKnown)
                            FactDate = new FactDate(FactDate.MINDATE, DateTime.Now); // if death flag set as Y then death before today.
                    }
                    string age = FamilyTree.GetText(node, "AGE", false);
                    if (age.Length > 0)
                        this.GedcomAge = new Age(age, FactDate);
                    this.CertificatePresent = SetCertificatePresent();
                }
                catch (Exception ex)
                {
                    string message = (node == null) ? string.Empty : node.InnerText + ". ";
                    throw new InvalidXMLFactException(message + "\n            Error " + ex.Message + "\n");
                }
            }
        }

        private void CheckForSharedFacts(XmlNode node)
        {
            XmlNodeList list = node.SelectNodes("_SHAR");
            foreach (XmlNode n in list)
            {
                string indref = n.Attributes["REF"].Value;
                string role = FamilyTree.GetText(n, "ROLE", false);
                if (role.Equals("Household Member"))
                    FamilyTree.Instance.AddSharedFact(indref, this);
            }
        }

        public static readonly string CHILDREN_STATUS_PATTERN1 = @"(\d{1,2}) Total ?,? ?(\d{1,2}) (Alive|Living) ?,? ?(\d{1,2}) Dead";
        public static readonly string CHILDREN_STATUS_PATTERN2 = @"Total:? (\d{1,2}) ?,? ?(Alive|Living):? (\d{1,2}) ?,? ?Dead:? (\d{1,2})";

        private void CheckValidChildrenStatus(XmlNode node)
        {
            if (Comment.Length == 0)
                Comment = FamilyTree.GetNotes(node);
            if (Comment.IndexOf("ignore", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                this.FactErrorLevel = FactError.IGNORE;
                return;
            }
            bool success = false;
            int total, alive, dead;
            total = alive = dead = 0;
            Match matcher = Regex.Match(Comment, CHILDREN_STATUS_PATTERN1);
            if (matcher.Success)
            {
                success = true;
                int.TryParse(matcher.Groups[1].ToString(), out total);
                int.TryParse(matcher.Groups[2].ToString(), out alive);
                int.TryParse(matcher.Groups[4].ToString(), out dead);
            }
            else
            {
                matcher = Regex.Match(Comment, CHILDREN_STATUS_PATTERN2);
                if (matcher.Success)
                {
                    success = true;
                    int.TryParse(matcher.Groups[1].ToString(), out total);
                    int.TryParse(matcher.Groups[3].ToString(), out alive);
                    int.TryParse(matcher.Groups[4].ToString(), out dead);
                }
            }
            if (success)
            {
                if (total == alive + dead)
                    return;
                this.FactErrorMessage = "Children status total doesn't equal numbers alive plus numbers dead.";
            }
            else
                this.FactErrorMessage = "Children status doesn't match valid pattern Total x, Alive y, Dead z";
            this.FactErrorNumber = (int)FamilyTree.Dataerror.CHILDRENSTATUS_TOTAL_MISMATCH;
            this.FactErrorLevel = FactError.ERROR;
        }

        private void SetAddress(string factType, XmlNode node)
        {
            XmlNode addr = node.SelectSingleNode("ADDR");
            if (addr == null)
                return;
            string result = string.Empty; // need to do something with an ADDR tag
            XmlNode ctry = node.SelectSingleNode("ADDR/CTRY");
            if (ctry != null)
                result = ctry.InnerText;
            XmlNode stae = node.SelectSingleNode("ADDR/STAE");
            if (stae != null)
                result = (result.Length > 0) ? stae.InnerText + ", " + result : stae.InnerText;
            XmlNode city = node.SelectSingleNode("ADDR/CITY");
            if (city != null)
                result = (result.Length > 0) ? city.InnerText + ", " + result : city.InnerText;
            XmlNode adr3 = node.SelectSingleNode("ADDR/ADR3");
            if (adr3 != null)
                result = (result.Length > 0) ? adr3.InnerText + ", " + result : adr3.InnerText;
            XmlNode adr2 = node.SelectSingleNode("ADDR/ADR2");
            if (adr2 != null)
                result = (result.Length > 0) ? adr2.InnerText + ", " + result : adr2.InnerText;
            XmlNode adr1 = node.SelectSingleNode("ADDR/ADR1");
            if (adr1 != null)
                result = (result.Length > 0) ? adr1.InnerText + ", " + result : adr1.InnerText;
            string address = string.Empty;
            if (addr.FirstChild != null && addr.FirstChild.Value != null)
                address = addr.FirstChild.Value;
            foreach (XmlNode cont in node.SelectNodes("ADDR/CONT"))
            {
                if (cont.FirstChild != null && cont.FirstChild.Value != null)
                    address += " " + cont.FirstChild.Value;
            }
            if (address.Length > 0)
                result = (result.Length > 0) ? address + ", " + result : address;
            //   ADDR <ADDRESS_LINE> {1:1} p.41
            //+1 CONT <ADDRESS_LINE> {0:3} p.41
            //+1 ADR1 <ADDRESS_LINE1> {0:1} p.41
            //+1 ADR2 <ADDRESS_LINE2> {0:1} p.41
            //+1 ADR3 <ADDRESS_LINE3> {0:1} p.41
            //+1 CITY <ADDRESS_CITY> {0:1} p.41
            //+1 STAE <ADDRESS_STATE> {0:1} p.42
            //+1 POST <ADDRESS_POSTAL_CODE> {0:1} p.41
            //+1 CTRY <ADDRESS_COUNTRY> 

            // if we have a location and its not a comment fact then add them together
            if (!Location.Equals(FactLocation.UNKNOWN_LOCATION))
                result = result + ", " + Location.GEDCOMLocation;
            if (!Fact.COMMENT_FACTS.Contains(factType))
                Location = FactLocation.GetLocation(result);
        }

        public Fact(string factRef, string factType, FactDate date, FactLocation loc, string comment = "", bool preferred = true, bool createdByFTA = false)
            : this(factRef, preferred)
        {
            this.FactType = factType;
            this.FactDate = date;
            this.Comment = comment;
            this.Created = createdByFTA;
            this.Place = string.Empty;
            this.Location = loc;
        }

        #endregion

        #region Properties

        public Age GedcomAge { get; private set; }
        public bool Created { get; protected set; }
        public bool Preferred { get; private set; }
        private string Tag { get; set; }
        public CensusReference CensusReference { get; private set; }
        public FactLocation Location { get; private set; }
        public string Place { get; private set; }
        public string Comment { get; private set; }
        public FactDate FactDate { get; private set; }
        public string FactType { get; private set; }
        public int FactErrorNumber { get; private set; }
        public FactError FactErrorLevel { get; private set; }
        public string FactErrorMessage { get; private set; }
        public Individual Individual { get; private set; }
        public Family Family { get; private set; }
        public string FactTypeDescription { get { return (FactType == Fact.UNKNOWN && Tag.Length > 0) ? Tag : GetFactTypeDescription(FactType); } }

        public bool IsCensusFact
        {
            get
            {
                if (FactType == CENSUS || FactType == CENSUS_FTA) return true;
                if (FactType == RESIDENCE && Properties.GeneralSettings.Default.UseResidenceAsCensus)
                    return CensusDate.IsCensusYear(FactDate, Properties.GeneralSettings.Default.TolerateInaccurateCensusDate);
                return false;
            }
        }

        public bool IsLCCensusFact
        {
            get
            {
                if (!IsCensusFact)
                    return false;
                if (!CensusDate.IsLostCousinsCensusYear(FactDate, false))
                    return false;
                if (FactDate.YearMatches(CensusDate.EWCENSUS1841) && Countries.IsEnglandWales(Country))
                    return true;
                if (FactDate.YearMatches(CensusDate.EWCENSUS1881) && Countries.IsEnglandWales(Country))
                    return true;
                if (FactDate.YearMatches(CensusDate.SCOTCENSUS1881) && Country.Equals(Countries.SCOTLAND))
                    return true;
                if (FactDate.YearMatches(CensusDate.CANADACENSUS1881) && Country.Equals(Countries.CANADA))
                    return true;
                if (FactDate.YearMatches(CensusDate.EWCENSUS1911) && Countries.IsEnglandWales(Country))
                    return true;
                if (FactDate.YearMatches(CensusDate.IRELANDCENSUS1911) && Country.Equals(Countries.IRELAND))
                    return true;
                if (FactDate.YearMatches(CensusDate.USCENSUS1880) && Country.Equals(Countries.UNITED_STATES))
                    return true;
                if (FactDate.YearMatches(CensusDate.USCENSUS1940) && Country.Equals(Countries.UNITED_STATES))
                    return true;
                return false;
            }
        }

        public string DateString
        {
            get { return this.FactDate == null ? string.Empty : this.FactDate.DateString; }
        }

        public string SourceList
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (FactSource s in Sources.OrderBy(s => s.ToString()))
                {
                    if (sb.Length > 0) sb.Append("\n");
                    sb.Append(s.ToString());
                }
                return sb.ToString();
            }
        }

        public IList<FactSource> Sources { get; private set; }

        public string Country
        {
            get { return Location == null ? "UNKNOWN" : Location.Country; }
        }

        public bool CertificatePresent { get; private set; }

        #endregion

        public string ReverseLocation(string location)
        {
            return string.Join(",", location.Split(',').Reverse());
        }

        public void SetError(int number, FactError level, string message)
        {
            FactErrorNumber = number;
            FactErrorLevel = level;
            FactErrorMessage = message;
        }

        private string FixFactTypes(string tag)
        {
            string initialChars = tag.ToUpper().Substring(0, Math.Min(tag.Length, 4));
            if (initialChars == "BIRT" || initialChars == "MARR" || initialChars == "DEAT")
                return initialChars;
            return tag;
        }

        public void UpdateFactDate(FactDate date)
        {
            if (!FactDate.IsKnown && date != null && date.IsKnown)
                FactDate = date;
        }

        public bool HasValidCensusReference
        {
            get { return CensusReference != null && CensusReference.Status != CensusReference.ReferenceStatus.BLANK; }
        }

        public void SetCensusReferenceDetails(CensusReference cr, CensusLocation cl, string comment)
        {
            if (!HasValidCensusReference)
                this.CensusReference = cr;
            if (Location.IsBlank)
                Location = cl.Equals(CensusLocation.UNKNOWN) ? 
                    FactLocation.GetLocation(cr.Country, Properties.GeneralSettings.Default.AddCreatedLocations) :
                    FactLocation.GetLocation(cl.Location, Properties.GeneralSettings.Default.AddCreatedLocations);
            if (Comment.Length == 0 && comment.Length > 0)
                Comment = comment;
        }

        private void CheckResidenceCensusDate()
        {
            if (FactDate.IsKnown && CensusDate.IsCensusYear(FactDate, true) && !CensusDate.IsCensusYear(FactDate, false))
            {
                // residence isn't a normal census year but it is a census year if tolerate is on
                if (CensusDate.IsCensusCountry(FactDate, Location) || !Location.IsKnownCountry)
                {
                    //                    this.FactErrorNumber = (int) FamilyTree.Dataerror.RESIDENCE_CENSUS_DATE;
                    this.FactErrorLevel = Fact.FactError.WARNINGALLOW;
                    this.FactErrorMessage = "Warning : Residence date " + FactDate + " is in a census year but doesn't overlap census date.";
                    if (!Properties.GeneralSettings.Default.TolerateInaccurateCensusDate)
                        this.FactErrorMessage += " This would be accepted as a census fact with Tolerate slightly inaccurate census dates option.";
                }
            }
        }

        private void CheckCensusDate(string tag)
        {
            FactDate yearAdjusted = FactDate;
            // check if census fails to overlaps a census date
            if ((tag == "Census 1841" && !FactDate.Overlaps(CensusDate.UKCENSUS1841)) ||
                (tag == "Census 1851" && !FactDate.Overlaps(CensusDate.UKCENSUS1851)) ||
                (tag == "Census 1861" && !FactDate.Overlaps(CensusDate.UKCENSUS1861)) ||
                (tag == "Census 1871" && !FactDate.Overlaps(CensusDate.UKCENSUS1871)) ||
                (tag == "Census 1881" && !FactDate.Overlaps(CensusDate.UKCENSUS1881)) ||
                (tag == "Census 1891" && !FactDate.Overlaps(CensusDate.UKCENSUS1891)) ||
                (tag == "Census 1901" && !FactDate.Overlaps(CensusDate.UKCENSUS1901)) ||
                (tag == "Census 1911" && !FactDate.Overlaps(CensusDate.UKCENSUS1911)) ||
                (tag == "Census" && !CensusDate.IsUKCensusYear(FactDate, false)) ||
                ((tag == "Lost Cousins" || tag == "LostCousins") && !CensusDate.IsLostCousinsCensusYear(FactDate, false))
                && FactDate.DateString.Length >= 4)
            {
                // if not a census overlay then set date to year and try that instead
                string year = FactDate.DateString.Substring(FactDate.DateString.Length - 4);
                int result = 0;
                if (Int32.TryParse(year, out result))
                {
                    yearAdjusted = new FactDate(year);
                    if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate)
                    {
                        //                        this.FactErrorNumber = (int)FamilyTree.Dataerror.RESIDENCE_CENSUS_DATE;
                        this.FactErrorMessage = "Warning: Inaccurate Census date '" + FactDate + "' treated as '" + yearAdjusted + "'";
                        this.FactErrorLevel = Fact.FactError.WARNINGALLOW;
                    }
                    else
                    {
                        //                        this.FactErrorNumber = (int)FamilyTree.Dataerror.RESIDENCE_CENSUS_DATE;
                        this.FactErrorLevel = Fact.FactError.WARNINGIGNORE;
                        this.FactErrorMessage = "Inaccurate Census date '" + FactDate + "' fact ignored in strict mode. Check for incorrect date entered or try Tolerate slightly inaccurate census date option.";
                    }
                }
            }
            if ((tag == "Census 1841" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1841)) ||
                (tag == "Census 1851" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1851)) ||
                (tag == "Census 1861" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1861)) ||
                (tag == "Census 1871" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1871)) ||
                (tag == "Census 1881" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1881)) ||
                (tag == "Census 1891" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1891)) ||
                (tag == "Census 1901" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1901)) ||
                (tag == "Census 1911" && !yearAdjusted.Overlaps(CensusDate.UKCENSUS1911)))
            {
                this.FactErrorMessage = "UK Census fact error date '" + FactDate + "' doesn't match '" + tag + "' tag. Check for incorrect date entered.";
                this.FactErrorLevel = FactError.ERROR;
                //                this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                return;
            }
            if (tag == "Census" || tag == "LostCousins" || tag == "Lost Cousins")
            {
                TimeSpan ts = FactDate.EndDate - FactDate.StartDate;
                if (ts.Days > 3650)
                {
                    //                    this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                    this.FactErrorLevel = FactError.ERROR;
                    this.FactErrorMessage = "Date covers more than one census.";
                    return;
                }
            }
            if (tag == "Census")
            {
                if (!CensusDate.IsCensusYear(yearAdjusted, false))
                {
                    //                    this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                    this.FactErrorMessage = "Census fact error date '" + FactDate + "' isn't a supported census date. Check for incorrect date entered or try Tolerate slightly inaccurate census date option.";
                    this.FactErrorLevel = FactError.ERROR;
                    return;
                }
                if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate && yearAdjusted.IsKnown && !CensusDate.IsCensusYear(yearAdjusted, true))
                {
                    //                    this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                    this.FactErrorMessage = "Warning : Census fact error date '" + FactDate + "' overlaps census date but is vague. Check for incorrect date entered.";
                    this.FactErrorLevel = FactError.WARNINGALLOW;
                }
                if (!FactDate.Equals(yearAdjusted))
                    FactDate = yearAdjusted;
            }
            if (tag == "Lost Cousins" || tag == "LostCousins")
            {
                if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate && yearAdjusted.IsKnown && !CensusDate.IsLostCousinsCensusYear(yearAdjusted, true))
                {
                    //                    this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                    this.FactErrorMessage = "Lost Cousins fact error date '" + FactDate + "' overlaps Lost Cousins census year but is vague. Check for incorrect date entered.";
                    this.FactErrorLevel = Fact.FactError.WARNINGALLOW;
                }
                if (!CensusDate.IsLostCousinsCensusYear(yearAdjusted, false))
                {
                    //                    this.FactErrorNumber = (int)FamilyTree.Dataerror.CENSUS_COVERAGE;
                    this.FactErrorMessage = "Lost Cousins fact error date '" + FactDate + "' isn't a supported Lost Cousins census year. Check for incorrect date entered or try Tolerate slightly inaccurate census date option.";
                    this.FactErrorLevel = Fact.FactError.ERROR;
                }
                if (!FactDate.Equals(yearAdjusted))
                    FactDate = yearAdjusted;
            }
        }

        private void SetCommentAndLocation(string factType, string factComment, string factPlace, string latitude, string longitude)
        {
            if (factComment.Length == 0 && factPlace.Length > 0)
            {
                if (factPlace.EndsWith("/"))
                {
                    Comment = factPlace.Substring(0, factPlace.Length - 1);
                    Place = string.Empty;
                }
                else
                {
                    int slash = factPlace.IndexOf("/");
                    if (slash >= 0)
                    {
                        Comment = factPlace.Substring(0, slash).Trim();
                        // If slash occurs at end of string, location is empty.
                        Place = (slash == factPlace.Length - 1) ? string.Empty : factPlace.Substring(slash + 1).Trim();
                    }
                    else if (Fact.COMMENT_FACTS.Contains(factType))
                    {
                        // we have a comment rather than a location
                        Comment = factPlace;
                        Place = string.Empty;
                    }
                    else
                    {
                        Comment = string.Empty;
                        Place = factPlace;
                    }
                }
            }
            else
            {
                Comment = factComment;
                Place = factPlace;
                if (factType == NAME)
                    Comment = Comment.Replace("/", "");
            }
            Comment = EnhancedTextInfo.ToTitleCase(Comment).Trim();
            if (Properties.GeneralSettings.Default.ReverseLocations)
                Location = FactLocation.GetLocation(ReverseLocation(Place), latitude, longitude, FactLocation.Geocode.NOT_SEARCHED);
            else
                Location = FactLocation.GetLocation(Place, latitude, longitude, FactLocation.Geocode.NOT_SEARCHED);
        }

        private bool SetCertificatePresent()
        {
            return Sources.Any(fs =>
            {
                return (FactType.Equals(Fact.BIRTH) && fs.isBirthCert()) ||
                    (FactType.Equals(Fact.DEATH) && fs.isDeathCert()) ||
                    (FactType.Equals(Fact.MARRIAGE) && fs.isMarriageCert()) ||
                    (FactType.Equals(Fact.CENSUS) && fs.isCensusCert());
            });
        }

        public bool IsValidCensus(FactDate when)
        {
            return FactDate.IsKnown && IsCensusFact && FactDate.Overlaps(when) && FactDate.IsNotBEForeOrAFTer && FactErrorLevel == Fact.FactError.GOOD;
        }

        public bool IsValidLostCousins(FactDate when)
        {
            return FactDate.IsKnown && FactType == Fact.LOSTCOUSINS && FactDate.Overlaps(when) && FactDate.IsNotBEForeOrAFTer && FactErrorLevel == Fact.FactError.GOOD;
        }

        public bool IsOverseasUKCensus(string country)
        {
            return country.Equals(Countries.OVERSEAS_UK) || (!Countries.IsUnitedKingdom(country) && CensusReference != null && CensusReference.IsUKCensus);
        }

        public override string ToString()
        {
            return FactTypeDescription + ": " + FactDate + (Location.ToString().Length > 0 ? " at " + Location : string.Empty) + (Comment.ToString().Length > 0 ? "  (" + Comment + ")" : string.Empty);
        }
    }
}
