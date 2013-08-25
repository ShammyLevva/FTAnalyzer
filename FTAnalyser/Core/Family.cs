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
        public static readonly string SINGLE = "Single", MARRIED = "Married", SOLOINDIVIDUAL = "Unrelated";

        public string FamilyID { get; private set; }
        public IList<Fact> Facts { get; private set; }
        public List<Individual> Children { get; internal set; }

        public Individual Husband { get; internal set; }
        public Individual Wife { get; internal set; }

        private Family(string familyID)
        {
            this.FamilyID = familyID;
            this.Facts = new List<Fact>();
            this.Children = new List<Individual>();
        }

        public Family() : this("") { }

        public Family(XmlNode node)
            : this("")
        {
            if (node != null)
            {
                XmlNode eHusband = node.SelectSingleNode("HUSB");
                XmlNode eWife = node.SelectSingleNode("WIFE");
                this.FamilyID = node.Attributes["ID"].Value;
                string husbandID = eHusband == null ? null : eHusband.Attributes["REF"].Value;
                string wifeID = eWife == null ? null : eWife.Attributes["REF"].Value;
                FamilyTree ft = FamilyTree.Instance;
                this.Husband = ft.GetIndividual(husbandID);
                this.Wife = ft.GetIndividual(wifeID);
                if (Husband != null && Wife != null)
                    Wife.MarriedName = Husband.Surname;
                // now iterate through child elements of eChildren
                // finding all individuals
                XmlNodeList list = node.SelectNodes("CHIL");
                foreach (XmlNode n in list)
                {
                    if (n.Attributes["REF"] != null)
                    {
                        Individual child = ft.GetIndividual(n.Attributes["REF"].Value);
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
                AddFacts(node, Fact.CUSTOM_FACT);
            }
        }

        public Family(Individual ind)
            : this(SOLOINDIVIDUAL)
        {
            if (ind.isMale)
                this.Husband = ind;
            else
                this.Wife = ind;

        }

        internal Family(Family f)
        {
            this.FamilyID = f.FamilyID;
            this.Facts = new List<Fact>(f.Facts);
            this.Husband = f.Husband == null ? null : new Individual(f.Husband);
            this.Wife = f.Wife == null ? null : new Individual(f.Wife);
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

        public void FixFamilyID(int length)
        {
            try
            {
                if (FamilyID == null || FamilyID == "" || FamilyID == SOLOINDIVIDUAL)
                    FamilyID = SOLOINDIVIDUAL;
                else
                    FamilyID = FamilyID.Substring(0, 1) + FamilyID.Substring(1).PadLeft(length, '0');
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

        public int FamilySize
        {
            get
            {
                int count = Children.Count;
                if (Husband != null)
                    count++;
                if (Wife != null)
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
                if (Husband == null || Wife == null)
                    return SINGLE;
                else
                    // very crude at the moment needs to check marriage facts 
                    // and return the appropriate marriage text string
                    return MARRIED;
            }
        }

        public string HusbandID
        {
            get { return (Husband == null) ? "" : Husband.IndividualID;
            }
        }

        public string WifeID
        {
            get { return (Wife == null) ? "" : Wife.IndividualID;
            }
        }

        public IEnumerable<Individual> Members
        {
            get
            {
                if (Husband != null) yield return Husband;
                if (Wife != null) yield return Wife;
                foreach (Individual child in Children) yield return child;
            }
        }

        private string FamilyName
        {
            get
            {
                string husbandsName = Husband == null ? "Unknown" : Husband.Name;
                string wifesName = Wife == null ? "Unknown" : Wife.Name;
                return husbandsName + " and " + wifesName;
            }
        }

        public string MarriageFilename
        {
            get
            {
                return FamilyTree.validFilename(FamilyID + " - Marriage of " + FamilyName + ".html");
            }
        }

        public string ChildrenFilename
        {
            get
            {
                return FamilyTree.validFilename(FamilyID + " - Children of " + FamilyName + ".html");
            }
        }

        public string FamilyRef
        {
            get
            {
                return FamilyID + ": " + FamilyName;
            }
        }

        public Individual Spouse(Individual ind)
        {
            if (ind.Equals(Husband))
                return Wife;
            if (ind.Equals(Wife))
                return Husband;
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
            get { return Husband == null ? string.Empty : Husband.Name + " (b." + Husband.BirthDate + ")"; }
        }

        string IDisplayFamily.Wife
        {
            get { return Wife == null ? string.Empty : Wife.Name + " (b." + Wife.BirthDate + ")"; }
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
                // return "central" date of family - use marriage facts, Husband/Wife facts, children birth facts
                List<FactDate> dates = new List<FactDate>();
                foreach (Fact f in Facts)
                    if (f.FactDate.AverageDate != FactDate.UNKNOWN_DATE)
                        dates.Add(f.FactDate.AverageDate);
                if (Husband != null)
                    foreach (Fact f in Husband.Facts)
                        if (f.FactDate.AverageDate != FactDate.UNKNOWN_DATE)
                            dates.Add(f.FactDate.AverageDate);
                if (Wife != null)
                    foreach (Fact f in Wife.Facts)
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
                if (Husband != null)
                    results.Add(Husband.Facts);
                if (Wife != null)
                    results.Add(Wife.Facts);
                foreach (Individual c in Children)
                    results.Add(c.Facts);
                return results.SelectMany(x => x);
            }
        }

        public IEnumerable<DisplayFact> AllDisplayFacts
        {
            get
            {
                List<DisplayFact> results = new List<DisplayFact>();
                // add the family facts then the facts from each individual
                //Facts.Select(f => results.Add(new DisplayFact(null, f)));
                //if (Husband != null)
                //    Husband.Facts.Select(f => results.Add(new DisplayFact(Husband, f)));
                //if (Wife != null)
                //    results.Add(Wife.Facts);
                //foreach (Individual c in Children)
                //    results.Add(c.Facts);
                //return results.SelectMany(x => x);
                string name;
                if(Husband == null)
                    if(Wife == null)
                        name = string.Empty;
                    else
                        name = Wife.Name;
                else
                    if(Wife == null)
                        name = Husband.Name;
                    else
                        name = Husband.Name + " & " + Wife.Name;

                foreach(Fact f in Facts)
                    results.Add(new DisplayFact(null, name, f));
                if (Husband != null)
                    foreach(Fact f in Husband.Facts)
                        results.Add(new DisplayFact(Husband, Husband.Name, f));
                if (Wife != null)
                    foreach (Fact f in Wife.Facts)
                        results.Add(new DisplayFact(Wife, Wife.Name, f));
                foreach (Individual c in Children)
                {
                    foreach (Fact f in c.GetFacts(Fact.BIRTH))
                        results.Add(new DisplayFact(c, c.Name, f));
                    foreach (Fact f in c.GetFacts(Fact.BAPTISM))
                        results.Add(new DisplayFact(c, c.Name, f));
                    foreach (Fact f in c.GetFacts(Fact.CHRISTENING))
                        results.Add(new DisplayFact(c, c.Name, f));
                }
                return results;
            }
        }
    }
}