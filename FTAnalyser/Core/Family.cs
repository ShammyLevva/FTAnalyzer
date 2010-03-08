using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
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

        public Family() : this("", "") {}

        public Family(XmlNode node) : this("", "")
        {
            if (node != null)
            {
                XmlNode eHusband = node.SelectSingleNode("HUSB");
                XmlNode eWife = node.SelectSingleNode("WIFE");
                this.familyGed = node.Attributes["ID"].Value;
                this.husbandGed = eHusband == null ? null : eHusband.Attributes["REF"].Value;
                this.wifeGed = eWife == null ? null : eWife.Attributes["REF"].Value;
                FamilyTree ft = FamilyTree.Instance;
                this.Husband = ft.getGedcomIndividual(this.husbandGed);
                this.Wife = ft.getGedcomIndividual(this.wifeGed);
                if (husband != null && wife != null)
                    wife.MarriedName = husband.Surname;
                // now iterate through child elements of eChildren
                // finding all individuals
                XmlNodeList list = node.SelectNodes("CHIL");
                foreach (XmlNode n in list)
                {
                    Individual child = ft.getGedcomIndividual(n.Attributes["REF"].Value);
                    if (child != null)
                        children.Add(child);
                    else
                        Console.WriteLine("Child not found in family :" + this.familyGed);
                }
                addFacts(node, Fact.MARRIAGE);
                addFacts(node, Fact.CUSTOM_FACT);
            }
        }

        internal Family(Family f)
        {
            this.familyID = f.familyID;
            this.familyGed = f.familyGed;
            this.husbandID = f.husbandID;
            this.husbandGed = f.husbandGed;
            this.wifeID = f.wifeID;
            this.wifeGed = f.wifeGed;
            this.facts = new List<Fact>(f.facts);
            this.husband = new Individual(f.husband);
            this.wife = new Individual(f.wife);
            this.children = new List<Individual>(f.children);
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
         * @return Returns the first fact of the given type.
         */
        public Fact getPreferredFact(string factType) {
            foreach (Fact f in facts)
            {
	    	    if (f.FactType == factType)
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
            return (f == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
        }

        /**
         * @return Returns all facts of the given type.
         */
        public List<Fact> getFacts(string factType) {
	        List<Fact> result = new List<Fact>();
	        foreach(Fact f in facts) 
            {
	            if (f.FactType == factType)
	    	        result.Add(f);
	        }
	        return result;
        }

        #region Properties

        public List<Fact> AllFacts
        {
            get { return this.facts; }
        }

        public FactDate MarriageDate
        {
            get { return getPreferredFactDate(Fact.MARRIAGE); }
        }

        public string MaritalStatus
        {
            get
            {
                if (husband == null || wife == null)
                    return SINGLE;
                else
                    // very crude at the moment needs to check marriage facts 
                    // and return the appropriate marriage text string
                    return MARRIED;
            }
        }

        public string FamilyGed
        {
            get { return this.familyGed; }
        }

        public string FamilyID
        {
            get { return this.familyID; }
        }
        
        public string HusbandID
        {
            get { return this.husbandID; }
        }
        
        public string HusbandGed
        {
            get { return this.husbandGed; }
        }

        public Individual Husband
        {
            get { return this.husband; }
            internal set
            {
                this.husband = value;
                if (this.husband == null)
                {
                    this.husbandID = "";
                    this.husbandGed = "";
                }
                else
                {
                    this.husbandID = value.IndividualID;
                    this.husbandGed = value.GedcomID;
                }
            }
        }

        public string WifeID
        {
            get { return this.wifeID; }
            set { this.wifeID = value; }
        }
        
        public string WifeGed
        {
            get { return this.wifeGed; }
        }
        
        public Individual Wife
        {
            get { return this.wife; }
            internal set
            {
                this.wife = value;
                if (this.wife == null)
                {
                    this.wifeID = "";
                    this.wifeGed = "";
                }
                else
                {
                    this.wifeID = value.IndividualID;
                    this.wifeGed = value.GedcomID;
                }
            }
        }

        public List<Individual> Children
        {
            get { return children; }
        }

        public List<Individual> Members
        {
            get
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

        #endregion

        public void setSpouseRelation(Individual ind, int relationType)
        {
            Individual spouse = ind.isMale() ? Wife : Husband;
            if (spouse != null && spouse.RelationType == Individual.UNKNOWN)
            {
                spouse.RelationType = relationType;
            }
        }

        public void setChildRelation(Queue<Individual> queue, int relationType)
        {
            foreach (Individual child in children)
            {
                if (child.RelationType == Individual.UNKNOWN)
                {
                    // add this previously unknown individual to list 
                    // of relatives to update family of
                    child.RelationType = relationType;
                    queue.Enqueue(child);
                }
            }
        }
    }
}