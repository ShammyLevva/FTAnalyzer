﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class Fact
    {
        public static readonly string ADOPTION = "ADOP", ANNULMENT = "ANUL", BAPTISM = "BAPM",
                BAPTISM_LDS = "BAPL", BAR_MITZVAH = "BARM", BAS_MITZVAH = "BASM", BIRTH = "BIRT", 
                BLESSING = "BLESS", BURIAL = "BURI", CASTE = "CAST", CENSUS = "CENS", 
                CHRISTENING = "CHR", ADULT_CHRISTENING = "CHRA", CONFIRMATION = "CONF", 
                CONFIRMATIONLDS = "CONL", CREMATION = "CREM", DEATH = "DEAT", PHYSICAL_DESC = "DSCR",
                DIVORCE = "DIV", DIVORCE_FILED = "DIVF", EDUCATION = "EDUC", EMIGRATION = "EMIG", 
                ENDOWMENT = "ENDL", ENGAGEMENT = "ENGA", FIRST_COMMUNION = "FCOM", 
                GRADUATION = "GRAD", IMMIGRATION = "IMMI", NAT_ID_NO = "IDNO", 
                NATIONAL_TRIBAL = "NATI", NUM_CHILDREN = "NCHI", NUM_MARRIAGE = "NMR", 
                LEGATEE = "LEGA", MARRIAGE_BANN = "MARB", MARR_CONTRACT = "MARC",
                MARR_LICENSE = "MARL", MARRIAGE = "MARR", MARR_SETTLEMENT = "MARS",
                NATURALIZATION = "NATU", OCCUPATION = "OCCU", POSSESSIONS = "PROP", ORDINANCE = "ORDI",
                ORDINATION = "ORDN", PROBATE = "PROB", RELIGION = "RELI", RESIDENCE = "RESI",
                RETIREMENT = "RETI", SEALING_CHILD = "SLGC", SEALING_SPOUSE = "SLGS",
                SOCIAL_SECURITY_NO = "SSN", NOBILITY_TITLE = "TITL", WILL = "WILL", 
                MILITARY = "_MILT", ELECTION = "_ELEC", CUSTOM_FACT = "EVEN";

        public static readonly string CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSE", FAMILYSEARCH = "*IGI",
                CONTACT = "*CONT", ARRIVAL = "*ARRI", DEPARTURE = "*DEPT", 
                CHANGE = "*CHNG", LOSTCOUSINS = "*LOST";

        private string factType;
        private FactDate date;
        private string comment;
        private string place;
        private FactLocation location;
        private List<FactSource> sources;
        private bool certificatePresent;

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        static Fact() {
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
            COMMENT_FACTS.Add(RELIGION);
            COMMENT_FACTS.Add(MILITARY);
            COMMENT_FACTS.Add(RETIREMENT);
            COMMENT_FACTS.Add(SOCIAL_SECURITY_NO);
            COMMENT_FACTS.Add(WILL);
            COMMENT_FACTS.Add(ELECTION);
            COMMENT_FACTS.Add(CHILDLESS);
            COMMENT_FACTS.Add(WITNESS);
            COMMENT_FACTS.Add(UNMARRIED);
            COMMENT_FACTS.Add(UNKNOWN);
            COMMENT_FACTS.Add(FAMILYSEARCH);
            COMMENT_FACTS.Add(LOSTCOUSINS);
        }

        #region Constructors

        private Fact()
        {
            this.factType = "";
            this.date = FactDate.UNKNOWN_DATE;
            this.comment = "";
            this.place = "";
            this.location = new FactLocation();
            this.sources = new List<FactSource>();
            this.certificatePresent = false;
        }

        public Fact (XmlNode node, string factRef) 
            :base()
        {
            if (node != null) 
            {
                FamilyTree ft = FamilyTree.Instance;
                try
                {
                    factType = FixFactTypes(node.Name);
                    if (factType.Equals("EVEN"))
                    {
                        string tag = FamilyTree.GetText(node, "TYPE");
                        CUSTOM_TAGS.TryGetValue(tag, out factType);
                        if (factType == null)
                            factType = FixFactTypes(tag);
                        if (factType == null)
                        {
                            factType = Fact.UNKNOWN;
                            FamilyTree.Instance.XmlErrorBox.AppendText("Recorded unknown fact type " + tag + "\n");
                        }
                    }
                    string factDate = FamilyTree.GetText(node, "DATE");
                    date = new FactDate(factDate, factRef);
                    setCommentAndLocation(factType, FamilyTree.GetText(node), FamilyTree.GetText(node, "PLAC"), 
                        FamilyTree.GetText(node, "PLAC/MAP/LATI"), FamilyTree.GetText(node, "PLAC/MAP/LONG"));

                    // now iterate through source elements of the fact finding all sources
                    sources = new List<FactSource>();
                    XmlNodeList list = node.SelectNodes("SOUR");
                    foreach (XmlNode n in list)
                    {
                        if (n.Attributes["Ref"] != null)
                        {   // only process sources with a reference
                            string srcref = n.Attributes["REF"].Value;
                            FactSource source = ft.getGedcomSource(srcref);
                            if (source != null)
                                sources.Add(source);
                            else
                                ft.XmlErrorBox.AppendText("Source " + srcref + " not found." + "\n");
                        }
                    }

                    if (factType == DEATH)
                    {
                        comment = FamilyTree.GetText(node, "CAUS");
                    }
                    this.certificatePresent = setCertificatePresent();
                }
                catch (Exception ex)
                {
                    string message = (node == null) ? "" : node.InnerText + ". ";
                    throw new InvalidXMLFactException(message + "Error " + ex.Message + "\n    With:" + factRef);
                }
            }
        }

        public Fact (string factType, FactDate date) 
            : base()
        {
            this.factType = factType;
            this.date = date;
            this.comment = "";
            this.place = "";
            this.location = FamilyTree.Instance.GetLocation(place,"","");
        }

        #endregion

        #region Properties

        public FactLocation Location {
            get { return location; }
        }

        public string Place
        {
            get { return place; }
        }

        public string Comment
        {
            get { return comment; }
        }

        public FactDate FactDate {
            get { return date; }
        }

        public string FactType {
            get { return factType; }
        }

        public string Datestring {
            get { return this.date == null ? "" : this.date.Datestring; }
        }

        public List<FactSource> Sources {
            get { return sources; }
        }

        public string Country {
            get { return location == null ? "Scotland" : location.Country; }
        }

        public bool CertificatePresent
        {
            get { return certificatePresent; }
        }

        #endregion

        private string FixFactTypes(string tag)
        {
            string initialChars = tag.ToUpper().Substring(0, Math.Min(tag.Length, 4));
            if (initialChars == "BIRT" || initialChars == "MARR" || initialChars == "DEAT")
                return initialChars;
            return tag;
        }

        private void setCommentAndLocation (string factType, string factComment, string factPlace, string latitude, string longitude) {
            if (factComment.Length == 0 && factPlace.Length > 0)
            {
                int slash = factPlace.IndexOf("/");
                if (slash >= 0) {
                    comment = factPlace.Substring(0, slash).Trim();
                    // If slash occurs at end of string, location is empty.
                    place = (slash == factPlace.Length - 1) ? "" : factPlace.Substring(slash + 1).Trim();
                } else if (Fact.COMMENT_FACTS.Contains(factType)) {
                    // we have a comment rather than a location
                    comment = factPlace;
                    place = "";
                } else {
                    comment = "";
                    place = factPlace;
                }
            } else {
                comment = factComment;
                place = factPlace;
            }
            location = FamilyTree.Instance.GetLocation(place, latitude, longitude);
        }

        private bool setCertificatePresent() {
	        foreach (FactSource fs in sources) {
	    	    bool result = (factType.Equals(Fact.BIRTH) && fs.isBirthCert()) ||
	    		    (factType.Equals(Fact.DEATH) && fs.isDeathCert()) ||
	    		    (factType.Equals(Fact.MARRIAGE) && fs.isMarriageCert()) ||
	    		    (factType.Equals(Fact.CENSUS) && fs.isCensusCert());
                if (result) return true;
	        }
	        return false;
        }
    }
}