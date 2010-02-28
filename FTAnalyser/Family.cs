using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class Family
    {

        public static readonly String SINGLE = "Single", MARRIED = "Married";

        private String familyID = "";
        protected int memberID;
        private String familyGed;
        protected Individual husband;
        private String husbandID;
        private String husbandGed;
        protected Individual wife;
        private String wifeID;
        private String wifeGed;
        private List<Fact> facts;
        protected List<Individual> children;

        protected Family(String familyID, int memberID, String familyGed)
        {
            this.familyID = familyID;
            this.memberID = memberID;
            this.familyGed = familyGed;
            this.facts = new List<Fact>();
            this.children = new List<Individual>();
        }

        public Family(FamilyLocal fam)
        {
            this(fam.getFamilyID(), fam.getMemberID().intValue(), fam.getGedcomID());
            setHusband(fam.getHusband() != null ? new Individual(fam.getHusband()) : null);
            setWife(fam.getWife() != null ? new Individual(fam.getWife()) : null);
            if (husband != null && wife != null)
                wife.setMarriedName(husband.getSurname());
            Iterator it = fam.getChildren().iterator();
            while (it.hasNext())
            {
                this.children.add(new Individual(it.next()));
            }
            it = fam.getFacts().iterator();
            while (it.hasNext())
            {
                this.facts.add(new Fact(it.next()));
            }
        }

        public Family(int memberID, XmlElement element)
        {
            this("", memberID, "");
            if (element != null)
            {
                XmlElement eHusband = element.element("HUSB");
                XmlElement eWife = element.element("WIFE");
                this.familyGed = element.attributeValue("ID");
                this.husbandGed = eHusband == null ? null : eHusband.attributeValue("REF");
                this.wifeGed = eWife == null ? null : eWife.attributeValue("REF");
                Client client = Client.getInstance();
                try
                {
                    setHusband(client.getGedcomIndividual(memberID, this.husbandGed));
                }
                catch (NotFoundException e)
                {
                    setHusband(null);
                }
                try
                {
                    setWife(client.getGedcomIndividual(memberID, this.wifeGed));
                }
                catch (NotFoundException e)
                {
                    setWife(null);
                }
                if (husband != null && wife != null)
                    wife.setMarriedName(husband.getSurname());
                try
                {
                    // now iterate through child elements of eChildren
                    // finding all individuals
                    for (Iterator i = element.elementIterator("CHIL"); i.hasNext(); )
                    {
                        XmlElement el = (XmlElement)i.next();
                        Individual child = client.getGedcomIndividual(memberID, el.attributeValue("REF"));
                        children.add(child);
                    }
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine("Child not found in family :" + this.familyGed);
                }
                addFacts(element, Fact.MARRIAGE);
                addFacts(element, Fact.CUSTOM_FACT);
            }
        }

        private void addFacts(XmlElement element, String factType)
        {
            Iterator it = element.elementIterator(factType);
            while (it.hasNext())
            {
                XmlElement e = (XmlElement)it.next();
                facts.add(new Fact(this.memberID, e));
            }
        }

        /**
         * @return Returns the facts.
         */
        public List<Fact> getAllFacts()
        {
            return this.facts;
        }

        /**
         * @return Returns the first fact of the given type.
         */
        public Fact getPreferredFact(String factType) {
            foreach (Fact f in facts)
            {
	    	    if (f.getFactType().Equals(factType))
	    	        return f;
	        }
	        return null;
	        // return new Fact(factType, FactDate.UNKNOWN_DATE);
	    }

        /**
         * @return Returns the first fact of the given type.
         */
        public FactDate getPreferredFactDate(String factType)
        {
            Fact f = getPreferredFact(factType);
            return (f == null) ? FactDate.UNKNOWN_DATE : f.getFactDate();
        }

        /**
         * @return Returns all facts of the given type.
         */
        public List<Fact> getFacts(String factType) {
	        List<Fact> result = new ArrayList<Fact>();
	        foreach(Fact f in facts) 
            {
	            if (f.getFactType().Equals(factType))
	    	        result.add(f);
	        }
	        return result;
	    }

        public FactDate getMarriageDate()
        {
            return getPreferredFactDate(Fact.MARRIAGE);
        }

        public String getMaritalStatus()
        {
            if (husband == null || wife == null)
            {
                return SINGLE;
            }
            else
            {
                // very crude at the moment needs to check marriage facts 
                // and return the appropriate marriage text string
                return MARRIED;
            }
        }

        /**
         * @return Returns the familyGed.
         */
        public String getFamilyGed()
        {
            return this.familyGed;
        }
        /**
         * @return Returns the familyID.
         */
        public String getFamilyID()
        {
            return this.familyID;
        }
        /**
         * @return Returns the husband.
         */
        public String getHusbandID()
        {
            return this.husbandID;
        }
        /**
         * @return Returns the husbandGed.
         */
        public String getHusbandGed()
        {
            return this.husbandGed;
        }
        /**
         * @return Returns the memberID.
         */
        public int getMemberID()
        {
            return this.memberID;
        }
        /**
         * @return Returns the wife.
         */
        public String getWifeID()
        {
            return this.wifeID;
        }
        /**
         * @return Returns the wifeGed.
         */
        public String getWifeGed()
        {
            return this.wifeGed;
        }
        /**
         * @return Returns the husband.
         */
        public Individual getHusband()
        {
            return this.husband;
        }
        /**
         * @return Returns the wife.
         */
        public Individual getWife()
        {
            return this.wife;
        }
        /**
         * @param wifeID The wifeID to set.
         */
        public void setWifeID(String wifeID)
        {
            this.wifeID = wifeID;
        }
        /**
         * @return Returns the children.
         */
        public List<Individual> getChildren()
        {
            return children;
        }

        protected void setHusband(Individual husband)
        {
            this.husband = husband;
            if (husband == null)
            {
                this.husbandID = "";
                this.husbandGed = "";
            }
            else
            {
                this.husbandID = husband.getIndividualID();
                this.husbandGed = husband.getGedcomID();
            }
        }

        protected void setWife(Individual wife)
        {
            this.wife = wife;
            if (wife == null)
            {
                this.wifeID = "";
                this.wifeGed = "";
            }
            else
            {
                this.wifeID = wife.getIndividualID();
                this.wifeGed = wife.getGedcomID();
            }
        }

        public List<Individual> getMembers()
        {
            List<Individual> members = new ArrayList<Individual>();
            if (husband != null)
                members.add(husband);
            if (wife != null)
                members.add(wife);
            members.addAll(children);
            return members;
        }
    }
}