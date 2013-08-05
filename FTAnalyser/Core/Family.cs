using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace FTAnalyzer
{
    public class Family : IDisplayFamily
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
                    if (n.Attributes["REF"] != null)
                    {
                        Individual child = ft.getGedcomIndividual(n.Attributes["REF"].Value);
                        if (child != null)
                            children.Add(child);
                        else
                            ft.XmlErrorBox.AppendText("Child not found in family :" + FamilyRef + "\n");
                    }
                    else
                        ft.XmlErrorBox.AppendText("Child without a reference found in family : " + FamilyRef + "\n");
                }
                addFacts(node, Fact.MARRIAGE);
                addFacts(node, Fact.CUSTOM_FACT);
            }
        }

        public Family(Individual ind)
            : this("IND", "")
        {
            if (ind.isMale)
                this.husband = ind;
            else
                this.wife = ind;

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
            this.husband = f.husband == null ? null : new Individual(f.husband);
            this.wife = f.wife == null ? null : new Individual(f.wife);
            this.children = new List<Individual>(f.children);
        }

        private void addFacts(XmlNode node, string factType)
        {
            XmlNodeList list = node.SelectNodes(factType);
            foreach(XmlNode n in list)
            {
                facts.Add(new Fact(n, FamilyRef));
            }
        }

        public void FixFamilyGed(int length)
        {
            try
            {
                if (familyGed == null || familyGed == "")
                    familyGed = "Unlinked";
                else
                    familyGed = familyGed.Substring(0, 1) + familyGed.Substring(1).PadLeft(length, '0');
            }
            catch (Exception)
            { // don't error if family ID is not of format Fxxxx
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

        public int Count
        {
            get
            {
                int count = children.Count;
                if (husband != null)
                    count++;
                if (wife != null)
                    count++;
                return count;
            }
        }

        public List<Fact> AllFacts
        {
            get { return this.facts; }
        }

        public FactDate MarriageDate
        {
            get { return getPreferredFactDate(Fact.MARRIAGE); }
        }

        public string MarriageLocation
        {
            get { 
                Fact marriage  = getPreferredFact(Fact.MARRIAGE);
                if (marriage == null)
                    return string.Empty;
                else
                    return marriage.Location.ToString();
            }
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

        private string familyName
        {
            get
            {
                string husbandsName = husband == null ? "Unknown" : husband.Name;
                string wifesName = wife == null ? "Unknown" : wife.Name;
                return husbandsName + " and " + wifesName;
            }
        }

        public string MarriageFilename
        {
            get {
                
                 return FamilyTree.validFilename(familyGed + " - Marriage of " + familyName + ".html");
            }
        }

        public string ChildrenFilename
        {
            get
            {
                return FamilyTree.validFilename(familyGed + " - Children of " + familyName + ".html");
            }
        }

        public string FamilyRef
        {
            get
            {
                return familyGed + ": " + familyName;
            }
        }

        public Individual Spouse(Individual ind)
        {
            if (ind.Equals(husband))
                return wife;
            if (ind.Equals(wife))
                return husband;
            return null;
        }

        #endregion

        public void setBudgieCode(Individual ind, int lenAhnentafel)
        {
            Individual spouse = ind.isMale ? Wife : Husband;
            if (spouse != null && spouse.BudgieCode == string.Empty)
            {
                spouse.BudgieCode = ind.BudgieCode + "*s";
            }
            List<Individual> sortedChildren = children.OrderBy(c => c.BirthDate).ToList();
            int directChild = 0;
            if (ind.RelationType == Individual.DIRECT)
            {
                //first find which child is a direct
                foreach (Individual child in sortedChildren)
                {
                    directChild++;
                    if (child.RelationType == Individual.DIRECT)
                        break;
                }
            }
            if (directChild > 0)
            {
                int childcount = 0;
                foreach (Individual child in sortedChildren)
                {
                    childcount++;
                    if (child.BudgieCode == string.Empty)
                    {
                        string prefix = (directChild < childcount) ? "+" : "-";
                        string code = (Math.Abs(directChild - childcount)).ToString();
                        string ahnentafel = ((int)Math.Floor(ind.Ahnentafel / 2.0)).ToString();
                        child.BudgieCode = ahnentafel.PadLeft(lenAhnentafel, '0') + prefix + code.PadLeft(2, '0');
                    }
                }
            }
            else
            {   // we have got here because we are not dealing with a direct nor a family that contains a direct child
                int childcount = 0;
                foreach (Individual child in sortedChildren)
                {
                    childcount++;
                    if (child.BudgieCode == string.Empty)
                    {
                        child.BudgieCode = ind.BudgieCode + "." + childcount.ToString().PadLeft(2, '0');
                    }
                }
            }
        }

        public void setSpouseRelation(Individual ind, int relationType)
        {
            Individual spouse = ind.isMale ? Wife : Husband;
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

        #region IDisplayFamily Members


        string IDisplayFamily.Husband
        {
            get { return husband == null ? string.Empty : husband.Name + " (b." + husband.BirthDate + ")"; }
        }

        string IDisplayFamily.Wife
        {
            get { return wife == null ? string.Empty : wife.Name + " (b." + wife.BirthDate + ")"; }
        }

        string IDisplayFamily.Marriage
        {
            get { 
                Fact marriage = getPreferredFact(Fact.MARRIAGE);
                if(marriage == null)
                    return string.Empty;
                if (marriage.Location.isBlank())
                    return "m." + MarriageDate;
                else
                    return "m." + MarriageDate + " at " + marriage.Location;
            }
        }

        string IDisplayFamily.Children
        {
            get { 
                StringBuilder result = new StringBuilder();
                foreach (Individual c in children)
                {
                    if (result.Length > 0)
                        result.Append(", ");
                    result.Append(c.Name + " (b." + c.BirthDate + ")");
                }
                return result.ToString();
            }
        }

        public FactLocation Location
        {
            get {
                
                int bestLevel = -1;
                FactLocation result = new FactLocation();
                foreach (Fact f in AllFamilyFacts)
                {
                    FactLocation l = new FactLocation(f.Place);
                    if (l.Level > bestLevel)
                    {
                        result = l;
                        bestLevel = l.Level;
                    }
                }
                return result;
            }
        }

        #endregion

        public bool isAtLocation(FactLocation loc, int level)
        {
            foreach (Fact f in AllFamilyFacts)
            {
                if (f.Location.Equals(loc, level))
                    return true;
            }
            return false;
        }

        private List<Fact> AllFamilyFacts
        {
            get
            {
                List<Fact> results = new List<Fact>();
                // add the family facts then the facts from each individual
                results.AddRange(facts); 
                if (husband != null)
                    results.AddRange(husband.AllFacts);
                if (wife != null)
                    results.AddRange(wife.AllFacts);
                foreach (Individual c in children)
                    results.AddRange(c.AllFacts);
                return results;
            }
        }
    }
}