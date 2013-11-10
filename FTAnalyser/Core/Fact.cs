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
                BLESSING = "BLESS", BURIAL = "BURI", CENSUS = "CENS",
                CHRISTENING = "CHR", ADULT_CHRISTENING = "CHRA", CONFIRMATION = "CONF",
                CREMATION = "CREM", DEATH = "DEAT", PHYSICAL_DESC = "DSCR",
                DIVORCE = "DIV", DIVORCE_FILED = "DIVF", EDUCATION = "EDUC",
                EMIGRATION = "EMIG", ENGAGEMENT = "ENGA", FIRST_COMMUNION = "FCOM",
                GRADUATION = "GRAD", IMMIGRATION = "IMMI", NAT_ID_NO = "IDNO",
                NATIONAL_TRIBAL = "NATI", NUM_CHILDREN = "NCHI", NUM_MARRIAGE = "NMR",
                LEGATEE = "LEGA", MARRIAGE_BANN = "MARB", MARR_CONTRACT = "MARC",
                MARR_LICENSE = "MARL", MARRIAGE = "MARR", MARR_SETTLEMENT = "MARS",
                NATURALIZATION = "NATU", OCCUPATION = "OCCU", POSSESSIONS = "PROP",
                ORDINATION = "ORDN", PROBATE = "PROB", RESIDENCE = "RESI",
                RETIREMENT = "RETI", WILL = "WILL", SEPARATION = "_SEPR",
                MILITARY = "_MILT", ELECTION = "_ELEC", DEGREE = "_DEG",
                EMPLOYMENT = "_EMPLOY", MEDICAL_CONDITION = "_MDCL",
                CUSTOM_FACT = "EVEN", CUSTOM_FACT2 = "FACT";

        public const string CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSED", LOOSEBIRTH = "*LOOSEB", FAMILYSEARCH = "*IGI",
                CONTACT = "*CONT", ARRIVAL = "*ARRI", DEPARTURE = "*DEPT",
                CHANGE = "*CHNG", LOSTCOUSINS = "*LOST", DIED_SINGLE = "*SINGLE";

        public static readonly ISet<string> LOOSE_BIRTH_FACTS = new HashSet<string>(new string[] {
            CHRISTENING, BAPTISM, RESIDENCE, WITNESS, EMIGRATION, IMMIGRATION, ARRIVAL, DEPARTURE, 
            EDUCATION, DEGREE, ADOPTION, BAR_MITZVAH, BAS_MITZVAH, ADULT_CHRISTENING, CONFIRMATION, 
            FIRST_COMMUNION, ORDINATION, NATURALIZATION, GRADUATION, RETIREMENT, LOSTCOUSINS, 
            MARR_CONTRACT, MARR_LICENSE, MARR_SETTLEMENT, MARRIAGE, MARRIAGE_BANN, DEATH, 
            CREMATION, BURIAL
                    });

        public static readonly ISet<string> LOOSE_DEATH_FACTS = new HashSet<string>(new string[] {
            CENSUS, RESIDENCE, WITNESS, EMIGRATION, IMMIGRATION, ARRIVAL, DEPARTURE, EDUCATION,
            DEGREE, ADOPTION, BAR_MITZVAH, BAS_MITZVAH, ADULT_CHRISTENING, CONFIRMATION, FIRST_COMMUNION,
            ORDINATION, NATURALIZATION, GRADUATION, RETIREMENT, LOSTCOUSINS
                    });

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        //Database online. Class: HO107; Piece: 1782; Folio: 719; Page: 25; GSU
        //Database online. Class: HO107; Piece 709; Book: 6; Civil Parish: StLeonard Shoreditch; County: Middlesex; Enumeration District: 19;Folio: 53; Page: 15; Line: 16; GSU roll: 438819.
        //Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU
        //RG14PN22623 RG78PN1327 RD455 SD10 ED13 SN183
        //Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69
        //Class: RG14; Piece: 21983
        //Class: RG14; Piece: 12577; Schedule Number: 103
        private static readonly string EW_CENSUS_PATTERN = "Class: RG(\\d{2,3}); Piece: (\\d{1,5}); Folio: (\\d{1,4}); Page: (\\d{1,3}); GSU";
        private static readonly string EW_CENSUS_1841_PATTERN = "Class: HO107; Piece: (\\d{1,5}); Folio: (\\d{1,4}); Page: (\\d{1,3}); GSU";
        private static readonly string EW_CENSUS_1841_PATTERN2 = "Class: HO107; Piece:? (\\d{1,5}); Book: (\\d{1,3});.*?Folio: (\\d{1,4}); Page: (\\d{1,3});";
        private static readonly string EW_CENSUS_1911_PATTERN = "^RG14PN(\\d{1,6}) .*SN(\\d{1,3})$";
        private static readonly string EW_CENSUS_1911_PATTERN2 = "Class: RG14; Piece: (\\d{1,6})$";
        private static readonly string EW_CENSUS_1911_PATTERN3 = "Class: RG14; Piece: (\\d{1,6}); Schedule Number: (\\d{1,3})$";
        private static readonly string EW_CENSUS_1911_PATTERN4 = "Class: RG14; Piece: (\\d{1,6}); Page: (\\d{1,3})$";
        private static readonly string SCOT_CENSUS_PATTERN = "Parish: ([A-Za-z]+); ED: (\\d{1,3}); Page: (\\d{1,4}); Line: (\\d{1,2}); Roll: CSSCT";
        private static readonly string SCOT_CENSUS_PATTERN2 = "(\\d{3}/\\d{2}) (\\d{3}/\\d{2}) (\\d{3,4})";

        static Fact()
        {
            CUSTOM_TAGS.Add("IGI Search", FAMILYSEARCH);
            CUSTOM_TAGS.Add("Childless", CHILDLESS);
            CUSTOM_TAGS.Add("Contact", CONTACT);
            CUSTOM_TAGS.Add("Witness", WITNESS);
            CUSTOM_TAGS.Add("Unmarried", UNMARRIED);
            CUSTOM_TAGS.Add("Friends", UNMARRIED);
            CUSTOM_TAGS.Add("Partners", UNMARRIED);
            CUSTOM_TAGS.Add("Unknown", UNKNOWN);
            CUSTOM_TAGS.Add("Unknown-Begin", UNKNOWN);
            CUSTOM_TAGS.Add("Arrival", ARRIVAL);
            CUSTOM_TAGS.Add("Departure", DEPARTURE);
            CUSTOM_TAGS.Add("Record Change", CHANGE);
            CUSTOM_TAGS.Add("Lost Cousins", LOSTCOUSINS);
            CUSTOM_TAGS.Add("LostCousins", LOSTCOUSINS);
            CUSTOM_TAGS.Add("Died Single", DIED_SINGLE);
            CUSTOM_TAGS.Add("Census 1841", CENSUS);
            CUSTOM_TAGS.Add("Census 1851", CENSUS);
            CUSTOM_TAGS.Add("Census 1861", CENSUS);
            CUSTOM_TAGS.Add("Census 1871", CENSUS);
            CUSTOM_TAGS.Add("Census 1881", CENSUS);
            CUSTOM_TAGS.Add("Census 1891", CENSUS);
            CUSTOM_TAGS.Add("Census 1901", CENSUS);
            CUSTOM_TAGS.Add("Census 1911", CENSUS);
            CUSTOM_TAGS.Add("Census 1790", CENSUS);
            CUSTOM_TAGS.Add("Census 1800", CENSUS);
            CUSTOM_TAGS.Add("Census 1810", CENSUS);
            CUSTOM_TAGS.Add("Census 1820", CENSUS);
            CUSTOM_TAGS.Add("Census 1830", CENSUS);
            CUSTOM_TAGS.Add("Census 1840", CENSUS);
            CUSTOM_TAGS.Add("Census 1850", CENSUS);
            CUSTOM_TAGS.Add("Census 1860", CENSUS);
            CUSTOM_TAGS.Add("Census 1870", CENSUS);
            CUSTOM_TAGS.Add("Census 1880", CENSUS);
            CUSTOM_TAGS.Add("Census 1890", CENSUS);
            CUSTOM_TAGS.Add("Census 1900", CENSUS);
            CUSTOM_TAGS.Add("Census 1910", CENSUS);
            CUSTOM_TAGS.Add("Census 1920", CENSUS);
            CUSTOM_TAGS.Add("Census 1930", CENSUS);
            CUSTOM_TAGS.Add("Census 1940", CENSUS);

            COMMENT_FACTS.Add(OCCUPATION);
            COMMENT_FACTS.Add(MILITARY);
            COMMENT_FACTS.Add(RETIREMENT);
            COMMENT_FACTS.Add(WILL);
            COMMENT_FACTS.Add(ELECTION);
            COMMENT_FACTS.Add(CHILDLESS);
            COMMENT_FACTS.Add(WITNESS);
            COMMENT_FACTS.Add(UNMARRIED);
            COMMENT_FACTS.Add(UNKNOWN);
            COMMENT_FACTS.Add(FAMILYSEARCH);
            COMMENT_FACTS.Add(LOSTCOUSINS);
            COMMENT_FACTS.Add(DEGREE);
            COMMENT_FACTS.Add(EDUCATION);
            COMMENT_FACTS.Add(GRADUATION);
            COMMENT_FACTS.Add(DEPARTURE);
            COMMENT_FACTS.Add(ARRIVAL);
            COMMENT_FACTS.Add(EMPLOYMENT);
            COMMENT_FACTS.Add(MEDICAL_CONDITION);
            COMMENT_FACTS.Add(ORDINATION);
            COMMENT_FACTS.Add(PHYSICAL_DESC);
            COMMENT_FACTS.Add(POSSESSIONS);
        }

        private string GetFactTypeDescription(string factType)
        {
            switch (factType)
            {
                case ADOPTION: return "Adoption";
                case ANNULMENT: return "Annulment";
                case BAPTISM: return "Baptism";
                case BAR_MITZVAH: return "Bar mitzvah";
                case BAS_MITZVAH: return "Bas mitzvah";
                case BIRTH: return "Birth";
                case BLESSING: return "Blessing";
                case BURIAL: return "Burial";
                case CENSUS: return "Census";
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
                case POSSESSIONS: return "Property";
                case ORDINATION: return "Ordination";
                case PROBATE: return "Probate";
                case RESIDENCE: return "Residence";
                case RETIREMENT: return "Retirement";
                case WILL: return "Will";
                case SEPARATION: return "Separation";
                case MILITARY: return "Military service";
                case ELECTION: return "Election";
                case DEGREE: return "Degree";
                case EMPLOYMENT: return "Employment";
                case MEDICAL_CONDITION: return "Medical condition";
                case CHILDLESS: return "Childless";
                case UNMARRIED: return "Dies single";
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
                default: return factType;
            }
        }

        public enum FactError { GOOD = 0, WARNINGALLOW = 1, WARNINGIGNORE = 2, ERROR = 3, QUESTIONABLE = 4 };

        public string Piece { get; private set; }
        public string Folio { get; private set; }
        public string Page { get; private set; }
        public string Book { get; private set; }
        public string Schedule { get; private set; }
        public string Parish { get; private set; }
        public string ED { get; private set; }
        public Age GedcomAge { get; private set; }
        public bool Created { get; protected set; }
        public bool Preferred { get; private set; }
        private string Tag { get; set; }
        
        #region Constructors

        private Fact()
        {
            this.FactType = string.Empty;
            this.FactDate = FactDate.UNKNOWN_DATE;
            this.Comment = string.Empty;
            this.Place = string.Empty;
            this.Location = FactLocation.UNKNOWN_LOCATION;
            this.Sources = new List<FactSource>();
            this.CertificatePresent = false;
            this.FactErrorLevel = FactError.GOOD;
            this.FactErrorMessage = string.Empty;
            this.Piece = string.Empty;
            this.Folio = string.Empty;
            this.Book = string.Empty;
            this.Page = string.Empty;
            this.Schedule = string.Empty;
            this.Parish = string.Empty;
            this.ED = string.Empty;
            this.GedcomAge = null;
            this.Created = false;
            this.Tag = string.Empty;
            this.Preferred = false;
        }

        public Fact(XmlNode node, string factRef)
            : this()
        {
            if (node != null)
            {
                FamilyTree ft = FamilyTree.Instance;
                try
                {
                    FactType = FixFactTypes(node.Name);
                    string factDate = FamilyTree.GetText(node, "DATE");
                    this.FactDate = new FactDate(factDate, factRef);
                    if (FactType.Equals(CUSTOM_FACT) || FactType.Equals(CUSTOM_FACT2))
                    {
                        string tag = FamilyTree.GetText(node, "TYPE");
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
                    SetCommentAndLocation(FactType, FamilyTree.GetText(node), FamilyTree.GetText(node, "PLAC"),
                        FamilyTree.GetText(node, "PLAC/MAP/LATI"), FamilyTree.GetText(node, "PLAC/MAP/LONG"));

                    if (FactType.Equals(CENSUS) && Location.IsUnitedKingdom)
                    {  // only check UK census dates for errors as those are used for colour census
                        CheckCensusDate("Census");
                    }

                    // need to check residence after setting location
                    if (FactType.Equals(RESIDENCE) && Properties.GeneralSettings.Default.UseResidenceAsCensus)
                    {
                        CheckResidenceCensusDate();
                    }
                    // now iterate through source elements of the fact finding all sources
                    XmlNodeList list = node.SelectNodes("SOUR");
                    foreach (XmlNode n in list)
                    {
                        if (n.Attributes["REF"] != null)
                        {   // only process sources with a reference
                            string srcref = n.Attributes["REF"].Value;
                            FactSource source = ft.GetSourceID(srcref);
                            if (source != null)
                                Sources.Add(source);
                            else
                                ft.XmlErrorBox.AppendText("Source " + srcref + " not found." + "\n");
                        }
                        GetCensusReference(n);
                    }
                    if (FactType == DEATH)
                    {
                        Comment = FamilyTree.GetText(node, "CAUS");
                        if (node.FirstChild.Value == "Y" && !FactDate.IsKnown)
                        {
                            FactDate = new FactDate(FactDate.MINDATE, DateTime.Now); // if death flag set as Y then death before today.
                        }
                    }
                    string age = FamilyTree.GetText(node, "AGE");
                    if (age.Length > 0)
                        this.GedcomAge = new Age(age, FactDate);
                    this.CertificatePresent = SetCertificatePresent();
                }
                catch (Exception ex)
                {
                    string message = (node == null) ? "" : node.InnerText + ". ";
                    throw new InvalidXMLFactException(message + "\n            Error " + ex.Message + "\n");
                }
            }
        }

        private void GetCensusReference(XmlNode n)
        {
            string text = FamilyTree.GetText(n, "PAGE");
            if (text.Length > 0)
            {
                Match matcher = Regex.Match(text, SCOT_CENSUS_PATTERN);
                if (matcher.Success)
                {
                    this.Parish = matcher.Groups[1].ToString();
                    this.ED = matcher.Groups[2].ToString();
                    this.Page = matcher.Groups[3].ToString();
                }
                matcher = Regex.Match(text, SCOT_CENSUS_PATTERN2);
                if (matcher.Success)
                {
                    this.Parish = matcher.Groups[1].ToString().Replace("/00", "").Replace("/", "-");
                    this.ED = matcher.Groups[2].ToString().Replace("/00", "").TrimStart('0');
                    this.Page = matcher.Groups[3].ToString().TrimStart('0');
                }
                matcher = Regex.Match(text, EW_CENSUS_PATTERN);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Folio = matcher.Groups[2].ToString();
                    this.Page = matcher.Groups[3].ToString();
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN2);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Book = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN2);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = "Missing";
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN3);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN4);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Page = matcher.Groups[2].ToString();
                }
            }
        }

        public Fact(string factType, FactDate date)
            : this()
        {
            this.FactType = factType;
            this.FactDate = date;
            this.Comment = string.Empty;
            this.Place = string.Empty;
            this.Location = FactLocation.GetLocation(Place);
        }

        #endregion

        #region Properties

        public FactLocation Location { get; private set; }

        public string Place { get; private set; }

        public string Comment { get; private set; }

        public FactDate FactDate { get; private set; }

        public string FactType { get; private set; }

        public FactError FactErrorLevel { get; private set; }

        public string FactErrorMessage { get; private set; }

        public string FactTypeDescription { get { return FactType == Fact.UNKNOWN ? Tag : GetFactTypeDescription(FactType); } }

        public bool IsCensusFact
        {
            get
            {
                if (FactType == CENSUS) return true;
                if (FactType == RESIDENCE && Properties.GeneralSettings.Default.UseResidenceAsCensus)
                {
                    return CensusDate.IsCensusYear(FactDate, Properties.GeneralSettings.Default.TolerateInaccurateCensusDate);
                }
                return false;
            }
        }

        public string DateString
        {
            get { return this.FactDate == null ? "" : this.FactDate.DateString; }
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

        private string FixFactTypes(string tag)
        {
            string initialChars = tag.ToUpper().Substring(0, Math.Min(tag.Length, 4));
            if (initialChars == "BIRT" || initialChars == "MARR" || initialChars == "DEAT")
                return initialChars;
            return tag;
        }

        private void CheckResidenceCensusDate()
        {
            if (FactDate.IsKnown && CensusDate.IsCensusYear(FactDate, true) && !CensusDate.IsCensusYear(FactDate, false))
            {
                // residence isn't a normal census year but it is a census year if tolerate is on
                if (CensusDate.IsCensusCountry(FactDate, Location) || !Location.IsKnownCountry)
                {
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
                        this.FactErrorMessage = "Warning: Inaccurate Census date '" + FactDate + "' treated as '" + yearAdjusted + "'";
                        this.FactErrorLevel = Fact.FactError.WARNINGALLOW;
                    }
                    else
                    {
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
                return;
            }
            if (tag == "Census" || tag == "LostCousins" || tag == "Lost Cousins")
            {
                if (!CensusDate.IsUKCensusYear(yearAdjusted, false))
                {
                    this.FactErrorMessage = "Census fact error date '" + FactDate + "' isn't a supported UK census date. Check for incorrect date entered or try Tolerate slightly inaccurate census date option.";
                    this.FactErrorLevel = FactError.ERROR;
                    return;
                }
                TimeSpan ts = FactDate.EndDate - FactDate.StartDate;
                if (ts.Days > 3650)
                {
                    this.FactErrorLevel = FactError.ERROR;
                    this.FactErrorMessage = "Date covers more than one census.";
                    return;
                }
            }
            if (tag == "Census")
            {
                if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate && yearAdjusted.IsKnown && !CensusDate.IsUKCensusYear(yearAdjusted, true))
                {
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
                    this.FactErrorMessage = "Lost Cousins fact error date '" + FactDate + "' overlaps Lost Cousins census year but is vague. Check for incorrect date entered.";
                    this.FactErrorLevel = Fact.FactError.WARNINGALLOW;
                }
                if (!CensusDate.IsLostCousinsCensusYear(yearAdjusted, false))
                {
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
                    Place = "";
                }
                else
                {
                    int slash = factPlace.IndexOf("/");
                    if (slash >= 0)
                    {
                        Comment = factPlace.Substring(0, slash).Trim();
                        // If slash occurs at end of string, location is empty.
                        Place = (slash == factPlace.Length - 1) ? "" : factPlace.Substring(slash + 1).Trim();
                    }
                    else if (Fact.COMMENT_FACTS.Contains(factType))
                    {
                        // we have a comment rather than a location
                        Comment = factPlace;
                        Place = "";
                    }
                    else
                    {
                        Comment = "";
                        Place = factPlace;
                    }
                }
            }
            else
            {
                Comment = factComment;
                Place = factPlace;
            }
            Comment = EnhancedTextInfo.ToTitleCase(Comment);
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

        public string CensusReference
        {
            get
            {
                string result = string.Empty;
                if (Location.Country.Equals(Countries.CANADA) && FactDate.Overlaps(CensusDate.CANADACENSUS1881))
                    return "Canada Census references not yet available";
                else if (Location.Country.Equals(Countries.UNITED_STATES) && FactDate.Overlaps(CensusDate.USCENSUS1880))
                    return "US Census references not yet available";
                else if (Location.Country.Equals(Countries.IRELAND) && FactDate.Overlaps(CensusDate.IRELANDCENSUS1911))
                    return "Irish Census references not yet available";
                else if (Location.Country.Equals(Countries.UNITED_STATES) && FactDate.Overlaps(CensusDate.USCENSUS1940))
                    return "US Census references not yet available";
                else if (Piece.Length > 0)
                {
                    if (Countries.IsEnglandWales(Location.Country) && FactDate.Overlaps(CensusDate.UKCENSUS1881))
                        return "Piece: " + Piece + ", Folio: " + Folio + ", Page: " + Page;
                    if (Countries.IsEnglandWales(Location.Country) && FactDate.Overlaps(CensusDate.UKCENSUS1841))
                    {
                        if (Book.Length > 0)
                            return "Piece: " + Piece + ", Book: " + Book + ", Folio: " + Folio + ", Page: " + Page;
                        else
                            return "Piece: " + Piece + ", Book: see census image (stamped on the census page after the piece number), Folio: " + Folio + ", Page: " + Page;
                    }
                    if (Countries.IsEnglandWales(Location.Country) && FactDate.Overlaps(CensusDate.UKCENSUS1911))
                    {
                        if (Schedule.Length > 0)
                            return "Piece: " + Piece + ", Schedule: " + Schedule;
                        else
                            return "Piece: " + Piece + ", Page: " + Page;
                    }
                }
                else if (Parish.Length > 0)
                {
                    if (Location.Country.Equals(Countries.SCOTLAND) && FactDate.Overlaps(CensusDate.UKCENSUS1881))
                        return "Parish: " + Parish + Parishes.Reference(Parish) + " ED: " + ED + ", Page: " + Page;
                }

                return result;
            }
        }

        public override string ToString()
        {
            return FactType + ": " + FactDate + " at " + Location;
        }
    }
}
