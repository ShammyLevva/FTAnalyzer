using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Globalization;

namespace FTAnalyzer
{
    public class Fact
    {
        public const string ADOPTION = "ADOP", ANNULMENT = "ANUL", BAPTISM = "BAPM",
                BAPTISM_LDS = "BAPL", BAR_MITZVAH = "BARM", BAS_MITZVAH = "BASM", BIRTH = "BIRT",
                BLESSING = "BLESS", BURIAL = "BURI", CENSUS = "CENS",
                CHRISTENING = "CHR", ADULT_CHRISTENING = "CHRA", CONFIRMATION = "CONF",
                CONFIRMATIONLDS = "CONL", CREMATION = "CREM", DEATH = "DEAT", PHYSICAL_DESC = "DSCR",
                DIVORCE = "DIV", DIVORCE_FILED = "DIVF", EDUCATION = "EDUC", EMIGRATION = "EMIG",
                ENDOWMENT = "ENDL", ENGAGEMENT = "ENGA", FIRST_COMMUNION = "FCOM",
                GRADUATION = "GRAD", IMMIGRATION = "IMMI", NAT_ID_NO = "IDNO",
                NATIONAL_TRIBAL = "NATI", NUM_CHILDREN = "NCHI", NUM_MARRIAGE = "NMR",
                LEGATEE = "LEGA", MARRIAGE_BANN = "MARB", MARR_CONTRACT = "MARC",
                MARR_LICENSE = "MARL", MARRIAGE = "MARR", MARR_SETTLEMENT = "MARS",
                NATURALIZATION = "NATU", OCCUPATION = "OCCU", POSSESSIONS = "PROP", ORDINANCE = "ORDI",
                ORDINATION = "ORDN", PROBATE = "PROB", RESIDENCE = "RESI",
                RETIREMENT = "RETI", SEALING_CHILD = "SLGC", SEALING_SPOUSE = "SLGS",
                WILL = "WILL", SEPARATION = "_SEPR", MILITARY = "_MILT",
                ELECTION = "_ELEC", DEGREE = "_DEG", EMPLOYMENT = "_EMPLOY",
                MEDICAL_CONDITION = "_MDCL", CUSTOM_FACT = "EVEN";

        public const string CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSE", FAMILYSEARCH = "*IGI",
                CONTACT = "*CONT", ARRIVAL = "*ARRI", DEPARTURE = "*DEPT",
                CHANGE = "*CHNG", LOSTCOUSINS = "*LOST";

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        static Fact()
        {
            CUSTOM_TAGS.Add("IGI", FAMILYSEARCH);
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
            COMMENT_FACTS.Add(ORDINANCE);
            COMMENT_FACTS.Add(ORDINATION);
            COMMENT_FACTS.Add(PHYSICAL_DESC);
            COMMENT_FACTS.Add(POSSESSIONS);
        }

        public static string GetFactTypeDescription(string factType)
        {
            switch (factType)
            {
                case ADOPTION: return "Adoption";
                case ANNULMENT: return "Annulment";
                case BAPTISM: return "Baptism";
                case BAPTISM_LDS: return "Baptism (LDS)";
                case BAR_MITZVAH: return "Bar mitzvah";
                case BAS_MITZVAH: return "Bas mitzvah";
                case BIRTH: return "Birth";
                case BLESSING: return "Blessing";
                case BURIAL: return "Burial";
                case CENSUS: return "Census";
                case CHRISTENING: return "Christening";
                case ADULT_CHRISTENING: return "Adult christening";
                case CONFIRMATION: return "Confirmation";
                case CONFIRMATIONLDS: return "Confirmation (LDS)";
                case CREMATION: return "Cremation";
                case DEATH: return "Death";
                case PHYSICAL_DESC: return "Physical description";
                case DIVORCE: return "Divorce";
                case DIVORCE_FILED: return "Divorce filed";
                case EDUCATION: return "Education";
                case EMIGRATION: return "Emigration";
                case ENDOWMENT: return "Endowment";
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
                case ORDINANCE: return "Ordinance (LDS)";
                case ORDINATION: return "Ordination";
                case PROBATE: return "Probate";
                case RESIDENCE: return "Residence";
                case RETIREMENT: return "Retirement";
                case SEALING_CHILD: return "Sealing to child (LDS)";
                case SEALING_SPOUSE: return "Sealing to spouse (LDS)";
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
                case UNKNOWN: return "UNKNOWN";
                case LOOSEDEATH: return "Loose death";
                case FAMILYSEARCH: return "IGI search";
                case CONTACT: return "Contact";
                case ARRIVAL: return "Arrival";
                case DEPARTURE: return "Departure";
                case CHANGE: return "Record change";
                case LOSTCOUSINS: return "Lost Cousins";
                default: return factType;
            }
        }

        #region Constructors

        private Fact()
        {
            this.FactType = "";
            this.FactDate = FactDate.UNKNOWN_DATE;
            this.Comment = "";
            this.Place = "";
            this.Location = new FactLocation();
            this.Sources = new List<FactSource>();
            this.CertificatePresent = false;
        }

        public Fact(XmlNode node, string factRef)
            : base()
        {
            if (node != null)
            {
                FamilyTree ft = FamilyTree.Instance;
                try
                {
                    FactType = FixFactTypes(node.Name);
                    if (FactType.Equals("EVEN"))
                    {
                        string tag = FamilyTree.GetText(node, "TYPE");
                        string factType;
                        if (CUSTOM_TAGS.TryGetValue(tag, out factType))
                            FactType = factType;
                        else
                        {
                            FactType = FixFactTypes(tag);
                            if (FactType == null)
                            {
                                FactType = Fact.UNKNOWN;
                                FamilyTree.Instance.XmlErrorBox.AppendText("Recorded unknown fact type " + tag + "\n");
                            }
                        }
                    }
                    string factDate = FamilyTree.GetText(node, "DATE");
                    this.FactDate = new FactDate(factDate, factRef);
                    SetCommentAndLocation(FactType, FamilyTree.GetText(node), FamilyTree.GetText(node, "PLAC"),
                        FamilyTree.GetText(node, "PLAC/MAP/LATI"), FamilyTree.GetText(node, "PLAC/MAP/LONG"));

                    // now iterate through source elements of the fact finding all sources
                    Sources = new List<FactSource>();
                    XmlNodeList list = node.SelectNodes("SOUR");
                    foreach (XmlNode n in list)
                    {
                        if (n.Attributes["Ref"] != null)
                        {   // only process sources with a reference
                            string srcref = n.Attributes["REF"].Value;
                            FactSource source = ft.GetGedcomSource(srcref);
                            if (source != null)
                                Sources.Add(source);
                            else
                                ft.XmlErrorBox.AppendText("Source " + srcref + " not found." + "\n");
                        }
                    }

                    if (FactType == DEATH)
                    {
                        Comment = FamilyTree.GetText(node, "CAUS");
                    }
                    this.CertificatePresent = SetCertificatePresent();
                }
                catch (Exception ex)
                {
                    string message = (node == null) ? "" : node.InnerText + ". ";
                    throw new InvalidXMLFactException(message + "Error " + ex.Message + "\n    With:" + factRef);
                }
            }
        }

        public Fact(string factType, FactDate date)
            : base()
        {
            this.FactType = factType;
            this.FactDate = date;
            this.Comment = "";
            this.Place = "";
            this.Location = FamilyTree.Instance.GetLocation(Place, "", "");
        }

        #endregion

        #region Properties

        public FactLocation Location { get; private set; }

        public string Place { get; private set; }

        public string Comment { get; private set; }

        public FactDate FactDate { get; private set; }

        public string FactType { get; private set; }

        public string DateString
        {
            get { return this.FactDate == null ? "" : this.FactDate.DateString; }
        }

        public IList<FactSource> Sources { get; private set; }

        public string Country
        {
            get { return Location == null ? "Scotland" : Location.Country; }
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
            TextInfo txtInfo = new CultureInfo("en-GB", false).TextInfo;
            Comment = txtInfo.ToTitleCase(Comment);
            Location = FamilyTree.Instance.GetLocation(Place, latitude, longitude);
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
    }
}
