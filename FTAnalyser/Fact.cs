using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class Fact
    {
        public static readonly String BIRTH = "BIRT", CHRISTENING = "CHRI",
                DEATH = "DEAT", BURIAL = "BURI", CENSUS = "CENS",
                RESIDENCE = "RESI", MARRIAGE = "MARR", RELIGION = "RELI",
                MILITARY = "_MILT", RETIREMENT = "RETI", OCCUPATION = "OCCU",
                SOCIAL_SECURITY_NO = "SSN", WILL = "WILL", ELECTION = "_ELEC",
                EMIGRATION = "EMIG", IMMIGRATION = "IMMI", CUSTOM_FACT = "EVEN",
                CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSE", IGISEARCH = "*IGI";

        private String factID;
        private String factType;
        private FactDate date;
        private String comment;
        private String location;
        private List<FactSource> sources;
        private bool certificatePresent;

        private static readonly Dictionary<String, String> CUSTOM_TAGS = new Dictionary<String, String>();
        private static readonly HashSet<String> COMMENT_FACTS = new HashSet<String>();

/*
        static {
            CUSTOM_TAGS.put("IGI Search", IGISEARCH);
            CUSTOM_TAGS.put("Childless", CHILDLESS);
            CUSTOM_TAGS.put("Witness",   WITNESS);
            CUSTOM_TAGS.put("Unmarried", UNMARRIED);
            CUSTOM_TAGS.put("Friends",   UNMARRIED);
            CUSTOM_TAGS.put("Partners",  UNMARRIED);
            CUSTOM_TAGS.put("Unknown",   UNKNOWN);
            CUSTOM_TAGS.put("Unknown-Begin", UNKNOWN);
            
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
            COMMENT_FACTS.Add(IGISEARCH);
        }
*/
/*        
        public Fact (FactLocal fact) {
            this.factID = fact.getFactID();
            this.factType = fact.getFactType();
            this.date = new FactDate(fact.getFactDate());
            this.comment = fact.getFactComment();
            this.location = fact.getFactLocation();
	        this.sources = new List<FactSource>();
		    Iterator it = fact.getSources().iterator();
		    while (it.hasNext()) {
		        this.sources.Add(new FactSource(it.next()));
		    }
		    this.certificatePresent = fact.getCertificated().boolValue();
        }
*/
        public Fact (int memberID, XmlNode node) {
            if (node != null) {
                factType = node.Name;
                if (factType.Equals("EVEN")) {
                    String tag = node.SelectSingleNode("TYPE").ToString();
                    factType = (String) CUSTOM_TAGS.get(tag);
                    if (factType == null) {
                        factType = Fact.UNKNOWN;
                        Console.WriteLine("Recorded unknown fact type " + tag);
                    }
                }
                date = new FactDate(node.SelectSingleNode("DATE").ToString());
                setCommentAndLocation(factType, node.SelectSingleNode("PLAC").ToString());
                Client client = Client.getInstance();
			    try {
				    // now iterate through source elements of the fact
				    // finding all sources
				    sources = new List<FactSource>();
			        for(Iterator i = node.nodeIterator("SOUR"); i.hasNext();) {
                        XmlNode n = (XmlNode)i.next(); 
			    	    FactSource source = client.getGedcomSource(memberID, n.attributeValue("REF"));
			    	    sources.Add(source);
			        } 
			    } catch (NotFoundException e) {
			        Console.WriteLine("Source not found for fact");
			    }
                if (factType.Equals(DEATH)) {
                    XmlNode cause = node.SelectSingleNode("CAUS");
                    comment = (cause == null) ? "" : cause.ToString();
                }
                this.certificatePresent = setCertificatePresent();
            }
        }

        public Fact (String factType, FactDate date) {
            this.factID = null;
            this.factType = factType;
            this.date = date;
            this.comment = "";
            this.location = "";
        }
        
        private void setCommentAndLocation (String factType, String place) {
            if (place != null) {
                int slash = place.IndexOf("/");
                if (slash >= 0) {
                    comment = place.Substring(0, slash).Trim();
                    // If slash occurs at end of string, location is empty.
                    location = (slash == place.Length - 1) ? "" : place.Substring(slash + 1).Trim();
                } else if (Fact.COMMENT_FACTS.Contains(factType)) {
                    // we have a comment rather than a location
                    comment = place;
                    location = "";
                } else {
                    comment = "";
                    location = place;
                }
            } else {
                comment = "";
                location = "";
            }
        }

        public String getLocation () {
            return location;
        }

        public String getComment () {
            return comment;
        }

        /**
         * @return Returns the date.
         */
        public FactDate getFactDate () {
            return date;
        }

        /**
         * @return Returns the factType.
         */
        public String getFactType () {
            return factType;
        }

        /**
         * @return Returns the dateString.
         */
        public String getDateString () {
            return this.date == null ? "" : this.date.getDateString();
        }

        /**
         * @return Returns the source.
         */
        public List<FactSource> getSources () {
            return sources;
        }

        public String getCountry() {
    	    Location loc = new Location(location);
    	    return loc == null ? "Scotland" : loc.getCountry();
        }
        
        private bool setCertificatePresent() {
	        foreach (FactSource fs in sources) {
	    	    return (factType.Equals(Fact.BIRTH) && fs.isBirthCert()) ||
	    		    (factType.Equals(Fact.DEATH) && fs.isDeathCert()) ||
	    		    (factType.Equals(Fact.MARRIAGE) && fs.isMarriageCert()) ||
	    		    (factType.Equals(Fact.CENSUS) && fs.isCensusCert());
	        }
	        return false;
        }
        
        /**
         * @return Returns the certificatePresent.
         */
        public bool isCertificatePresent() {
            return certificatePresent;
        }
    }
}
