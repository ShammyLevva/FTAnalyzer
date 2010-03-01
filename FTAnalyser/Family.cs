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
        private String familyGed;
        private String husbandID;
        private String husbandGed;
        private String wifeID;
        private String wifeGed;
        private List<Fact> facts;
        internal int memberID;
        internal Individual husband;
        internal Individual wife;
        internal List<Individual> children;

        private void SetupFamily(String familyID, int memberID, String familyGed)
        {
            this.familyID = familyID;
            this.memberID = memberID;
            this.familyGed = familyGed;
            this.facts = new List<Fact>();
            this.children = new List<Individual>();
        }
/*
        public Family(FamilyLocal fam)
        {
            SetupFamily(fam.getFamilyID(), fam.getMemberID().intValue(), fam.getGedcomID());
            setHusband(fam.getHusband() != null ? new Individual(fam.getHusband()) : null);
            setWife(fam.getWife() != null ? new Individual(fam.getWife()) : null);
            if (husband != null && wife != null)
                wife.setMarriedName(husband.getSurname());
            Iterator it = fam.getChildren().iterator();
            while (it.hasNext())
            {
                this.children.Add(new Individual(it.next()));
            }
            it = fam.getFacts().iterator();
            while (it.hasNext())
            {
                this.facts.Add(new Fact(it.next()));
            }
        }
*/
    public Family(int memberID, XmlNode node)
        {
            SetupFamily("", memberID, "");
            if (node != null)
            {
                XmlNode eHusband = node.SelectSingleNode("HUSB");
                XmlNode eWife = node.SelectSingleNode("WIFE");
                this.familyGed = node.Attributes.GetNamedItem("ID").ToString();
                this.husbandGed = eHusband == null ? null : eHusband.Attributes.GetNamedItem("REF").ToString();
                this.wifeGed = eWife == null ? null : eWife.Attributes.GetNamedItem("REF").ToString();
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
                    for (Iterator i = node.nodeIterator("CHIL"); i.hasNext(); )
                    {
                        XmlNode n = (XmlNode)i.next();
                        Individual child = client.getGedcomIndividual(memberID, n.attributeValue("REF"));
                        children.Add(child);
                    }
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine("Child not found in family :" + this.familyGed);
                }
                addFacts(node, Fact.MARRIAGE);
                addFacts(node, Fact.CUSTOM_FACT);
            }
        }

        private void addFacts(XmlNode node, String factType)
        {
            Iterator it = node.elementIterator(factType);
            while (it.hasNext())
            {
                XmlNode n = (XmlNode)it.next();
                facts.Add(new Fact(this.memberID, n));
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
	        List<Fact> result = new List<Fact>();
	        foreach(Fact f in facts) 
            {
	            if (f.getFactType().Equals(factType))
	    	        result.Add(f);
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
            List<Individual> members = new List<Individual>();
            if (husband != null)
                members.Add(husband);
            if (wife != null)
                members.Add(wife);
            members.AddRange(children);
            return members;
        }
    }
}