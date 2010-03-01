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

        public static readonly string SINGLE = "Single", MARRIED = "Married";

        private string familyID = "";
        private string familyGed;
        private string husbandID;
        private string husbandGed;
        private string wifeID;
        private string wifeGed;
        private List<Fact> facts;
        internal Individual husband;
        internal Individual wife;
        internal List<Individual> children;

        private Family(string familyID, string familyGed)
        {
            this.familyID = familyID;
            this.familyGed = familyGed;
            this.facts = new List<Fact>();
            this.children = new List<Individual>();
        }

        public Family() : this("", "")
        {
        }
/*
        public Family(FamilyLocal fam)
        {
            SetupFamily(fam.getFamilyID(), fam.getGedcomID());
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
        public Family(XmlNode node) : this("", "")
        {
            if (node != null)
            {
                XmlNode eHusband = node.SelectSingleNode("HUSB");
                XmlNode eWife = node.SelectSingleNode("WIFE");
                this.familyGed = node.Attributes.GetNamedItem("ID").ToString();
                this.husbandGed = eHusband == null ? null : eHusband.Attributes.GetNamedItem("REF").ToString();
                this.wifeGed = eWife == null ? null : eWife.Attributes.GetNamedItem("REF").ToString();
                FamilyTree ft = FamilyTree.Instance;
                setHusband(ft.getGedcomIndividual(this.husbandGed));
                setWife(ft.getGedcomIndividual(this.wifeGed));
                if (husband != null && wife != null)
                    wife.setMarriedName(husband.getSurname());
                // now iterate through child elements of eChildren
                // finding all individuals
                XmlNodeList list = node.SelectNodes("CHIL");
                foreach (XmlNode n in list)
                {
                    Individual child = ft.getGedcomIndividual(n.Attributes.GetNamedItem("REF").ToString());
                    if (child != null)
                    {
                        children.Add(child);
                    }
                    else
                    {

                        Console.WriteLine("Child not found in family :" + this.familyGed);
                    }
                }
                addFacts(node, Fact.MARRIAGE);
                addFacts(node, Fact.CUSTOM_FACT);
            }
        }

        private void addFacts(XmlNode node, string factType)
        {
            XmlNodeList list = node.SelectNodes(factType);
            foreach(XmlNode n in list)
            {
                facts.Add(new Fact(n));
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
        public Fact getPreferredFact(string factType) {
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
        public FactDate getPreferredFactDate(string factType)
        {
            Fact f = getPreferredFact(factType);
            return (f == null) ? FactDate.UNKNOWN_DATE : f.getFactDate();
        }

        /**
         * @return Returns all facts of the given type.
         */
        public List<Fact> getFacts(string factType) {
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

        public string getMaritalStatus()
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
        public string getFamilyGed()
        {
            return this.familyGed;
        }
        /**
         * @return Returns the familyID.
         */
        public string getFamilyID()
        {
            return this.familyID;
        }
        /**
         * @return Returns the husband.
         */
        public string getHusbandID()
        {
            return this.husbandID;
        }
        /**
         * @return Returns the husbandGed.
         */
        public string getHusbandGed()
        {
            return this.husbandGed;
        }
        /**
         * @return Returns the wife.
         */
        public string getWifeID()
        {
            return this.wifeID;
        }
        /**
         * @return Returns the wifeGed.
         */
        public string getWifeGed()
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
        public void setWifeID(string wifeID)
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