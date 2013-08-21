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

        public string FamilyID { get; private set; }
        public string FamilyGed { get; private set; }
        public string HusbandID { get; private set; }
        public string HusbandGed { get; private set; }
        public string WifeID { get; private set; }
        public string WifeGed { get; private set; }
        public IList<Fact> Facts { get; private set; }
        internal Individual husband;
        internal Individual wife;
        public List<Individual> Children { get; internal set; }

        private Family(string familyID, string familyGed)
        {
            this.FamilyID = familyID;
            this.FamilyGed = familyGed;
            this.Facts = new List<Fact>();
            this.Children = new List<Individual>();
        }

        public Family() : this("", "") { }

        public Family(XmlNode node)
            : this("", "")
        {
            if (node != null)
            {
                XmlNode eHusband = node.SelectSingleNode("HUSB");
                XmlNode eWife = node.SelectSingleNode("WIFE");
                this.FamilyGed = node.Attributes["ID"].Value;
                this.HusbandGed = eHusband == null ? null : eHusband.Attributes["REF"].Value;
                this.WifeGed = eWife == null ? null : eWife.Attributes["REF"].Value;
                FamilyTree ft = FamilyTree.Instance;
                this.Husband = ft.GetGedcomIndividual(this.HusbandGed);
                this.Wife = ft.GetGedcomIndividual(this.WifeGed);
                if (husband != null && wife != null)
                    wife.MarriedName = husband.Surname;
                // now iterate through child elements of eChildren
                // finding all individuals
                XmlNodeList list = node.SelectNodes("CHIL");
                foreach (XmlNode n in list)
                {
                    if (n.Attributes["REF"] != null)
                    {
                        Individual child = ft.GetGedcomIndividual(n.Attributes["REF"].Value);
                        if (child != null)
                            Children.Add(child);
                        else
                            ft.XmlErrorBox.AppendText("Child not found in family :" + FamilyRef + "\n");
                    }
                    else
                        ft.XmlErrorBox.AppendText("Child without a reference found in family : " + FamilyRef + "\n");
                }

                AddFacts(node, Fact.ANNULMENT);
                AddFacts(node, Fact.DIVORCE);
                AddFacts(node, Fact.DIVORCE_FILED);
                AddFacts(node, Fact.ENGAGEMENT);
                AddFacts(node, Fact.MARRIAGE);
                AddFacts(node, Fact.MARRIAGE_BANN);
                AddFacts(node, Fact.MARR_CONTRACT);
                AddFacts(node, Fact.MARR_LICENSE);
                AddFacts(node, Fact.MARR_SETTLEMENT);
                AddFacts(node, Fact.SEPARATION);
                AddFacts(node, Fact.SEALING_SPOUSE);
                AddFacts(node, Fact.CUSTOM_FACT);
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
            this.FamilyID = f.FamilyID;
            this.FamilyGed = f.FamilyGed;
            this.HusbandID = f.HusbandID;
            this.HusbandGed = f.HusbandGed;
            this.WifeID = f.WifeID;
            this.WifeGed = f.WifeGed;
            this.Facts = new List<Fact>(f.Facts);
            this.husband = f.husband == null ? null : new Individual(f.husband);
            this.wife = f.wife == null ? null : new Individual(f.wife);
            this.Children = new List<Individual>(f.Children);
        }

        private void AddFacts(XmlNode node, string factType)
        {
            XmlNodeList list = node.SelectNodes(factType);
            foreach (XmlNode n in list)
            {
                Facts.Add(new Fact(n, FamilyRef));
            }
        }

        public void FixFamilyGed(int length)
        {
            try
            {
                if (FamilyGed == null || FamilyGed == "")
                    FamilyGed = "Unlinked";
                else
                    FamilyGed = FamilyGed.Substring(0, 1) + FamilyGed.Substring(1).PadLeft(length, '0');
            }
            catch (Exception)
            { // don't error if family ID is not of format Fxxxx
            }
        }

        /**
         * @return Returns the first fact of the given type.
         */
        public Fact GetPreferredFact(string factType)
        {
            return Facts.Where(f => (f.FactType == factType)).FirstOrDefault();
        }

        /**
         * @return Returns the first fact of the given type.
         */
        public FactDate GetPreferredFactDate(string factType)
        {
            Fact f = GetPreferredFact(factType);
            return (f == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
        }

        /**
         * @return Returns all facts of the given type.
         */
        public IEnumerable<Fact> GetFacts(string factType)
        {
            return Facts.Where(f => f.FactType == factType);
        }

        #region Properties

        public int Count
        {
            get
            {
                int count = Children.Count;
                if (husband != null)
                    count++;
                if (wife != null)
                    count++;
                return count;
            }
        }

        public FactDate MarriageDate
        {
            get { return GetPreferredFactDate(Fact.MARRIAGE); }
        }

        public string MarriageLocation
        {
            get
            {
                Fact marriage = GetPreferredFact(Fact.MARRIAGE);
                return (marriage == null) ? string.Empty : marriage.Location.ToString();
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

        public Individual Husband
        {
            get { return this.husband; }
            internal set
            {
                this.husband = value;
                if (this.husband == null)
                {
                    this.HusbandID = "";
                    this.HusbandGed = "";
                }
                else
                {
                    this.HusbandID = value.IndividualID;
                    this.HusbandGed = value.GedcomID;
                }
            }
        }

        public Individual Wife
        {
            get { return this.wife; }
            internal set
            {
                this.wife = value;
                if (this.wife == null)
                {
                    this.WifeID = "";
                    this.WifeGed = "";
                }
                else
                {
                    this.WifeID = value.IndividualID;
                    this.WifeGed = value.GedcomID;
                }
            }
        }

        public IEnumerable<Individual> Members
        {
            get
            {
                if (husband != null) yield return husband;
                if (wife != null) yield return wife;
                foreach (Individual child in Children) yield return child;
            }
        }

        private string FamilyName
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
            get
            {
                return FamilyTree.validFilename(FamilyGed + " - Marriage of " + FamilyName + ".html");
            }
        }

        public string ChildrenFilename
        {
            get
            {
                return FamilyTree.validFilename(FamilyGed + " - Children of " + FamilyName + ".html");
            }
        }

        public string FamilyRef
        {
            get
            {
                return FamilyGed + ": " + FamilyName;
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

        public void SetBudgieCode(Individual ind, int lenAhnentafel)
        {
            Individual spouse = ind.isMale ? Wife : Husband;
            if (spouse != null && spouse.BudgieCode == string.Empty)
            {
                spouse.BudgieCode = ind.BudgieCode + "*s";
            }
            int directChild = 0;
            if (ind.RelationType == Individual.DIRECT)
            {
                //first find which child is a direct
                foreach (Individual child in Children.OrderBy(c => c.BirthDate))
                {
                    directChild++;
                    if (child.RelationType == Individual.DIRECT)
                        break;
                }
            }
            if (directChild > 0)
            {
                int childcount = 0;
                foreach (Individual child in Children.OrderBy(c => c.BirthDate))
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
                foreach (Individual child in Children.OrderBy(c => c.BirthDate))
                {
                    childcount++;
                    if (child.BudgieCode == string.Empty)
                    {
                        child.BudgieCode = ind.BudgieCode + "." + childcount.ToString().PadLeft(2, '0');
                    }
                }
            }
        }

        public void SetSpouseRelation(Individual ind, int relationType)
        {
            Individual spouse = ind.isMale ? Wife : Husband;
            if (spouse != null && spouse.RelationType == Individual.UNKNOWN)
            {
                spouse.RelationType = relationType;
            }
        }

        public void SetChildRelation(Queue<Individual> queue, int relationType)
        {
            foreach (Individual child in Children)
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
            get
            {
                Fact marriage = GetPreferredFact(Fact.MARRIAGE);
                if (marriage == null)
                    return string.Empty;
                if (marriage.Location.IsBlank())
                    return MarriageDate.ToString();
                else
                    return MarriageDate.ToString() + " at " + marriage.Location;
            }
        }

        string IDisplayFamily.Children
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (Individual c in Children)
                {
                    if (result.Length > 0)
                        result.Append(", ");
                    result.Append(c.Name + " (b." + c.BirthDate + ")");
                }
                return result.ToString();
            }
        }

        public FactDate FamilyDate
        {
            get
            {
                // return "central" date of family - use marriage facts, husband/wife facts, children birth facts
                List<FactDate> dates = new List<FactDate>();
                foreach (Fact f in Facts)
                    if (f.FactDate.AverageDate != FactDate.UNKNOWN_DATE)
                        dates.Add(f.FactDate.AverageDate);
                if (husband != null)
                    foreach (Fact f in husband.AllFacts)
                        if (f.FactDate.AverageDate != FactDate.UNKNOWN_DATE)
                            dates.Add(f.FactDate.AverageDate);
                if (wife != null)
                    foreach (Fact f in wife.AllFacts)
                        if (f.FactDate.AverageDate != FactDate.UNKNOWN_DATE)
                            dates.Add(f.FactDate.AverageDate);
                foreach (Individual c in Children)
                    if (c.BirthDate.AverageDate != FactDate.UNKNOWN_DATE)
                        dates.Add(c.BirthDate.AverageDate);
                if (dates.Count == 0)
                    return FactDate.UNKNOWN_DATE;
                long averageTicks = 0L;
                foreach (FactDate fd in dates)
                    averageTicks += fd.StartDate.Ticks / dates.Count;
                try
                {
                    DateTime averageDate = new DateTime(averageTicks);
                    return new FactDate(averageDate, averageDate);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                return FactDate.UNKNOWN_DATE;
            }
        }

        public FactLocation Location
        {
            get
            {
                return FactLocation.BestLocation(AllFamilyFacts, FamilyDate);
            }
        }

        #endregion

        public bool IsAtLocation(FactLocation loc, int level)
        {
            foreach (Fact f in AllFamilyFacts)
            {
                if (f.Location.Equals(loc, level))
                    return true;
            }
            return false;
        }

        private IEnumerable<Fact> AllFamilyFacts
        {
            get
            {
                List<IList<Fact>> results = new List<IList<Fact>>();
                // add the family facts then the facts from each individual
                results.Add(Facts);
                if (husband != null)
                    results.Add(husband.AllFacts);
                if (wife != null)
                    results.Add(wife.AllFacts);
                foreach (Individual c in Children)
                    results.Add(c.AllFacts);
                return results.SelectMany(x => x);
            }
        }
    }
}