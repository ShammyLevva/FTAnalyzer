using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using FTAnalyzer.Utilities;
using System.Web;
using System.Diagnostics;

namespace FTAnalyzer
{
    class FamilyTree
    {
        private static FamilyTree instance;

        private List<FactSource> sources;
        private List<Individual> individuals;
        private List<Family> families;
        private Dictionary<string, FactLocation> locations;
        private Dictionary<string, List<Individual>> occupations;
        private bool _loading = false;
        private bool _dataloaded = false;
        private RichTextBox xmlErrorbox = new RichTextBox();
        private int maxAhnentafel = 0;
        private List<DataErrorGroup> dataErrorTypes;

        private FamilyTree()
        {
            ResetData();
        }

        public static FamilyTree Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FamilyTree();
                }
                return instance;
            }
        }

        #region Static Functions

        public static string GetText(XmlNode node)
        {
            if (node == null || node.FirstChild == null || node.FirstChild.Value == null)
            {
                return "";
            }
            return node.FirstChild.Value.Trim();
        }

        public static string GetText(XmlNode node, string tag)
        {
            return GetText(GetChildNode(node, tag));
        }

        public static XmlNode GetChildNode(XmlNode node, String tag)
        {
            if (node == null)
                return null;
            else
                return node.SelectSingleNode(tag);
        }

        public static string validFilename(string filename)
        {
            int pos = filename.IndexOfAny(Path.GetInvalidFileNameChars());
            if (pos == -1)
                return filename;
            StringBuilder result = new StringBuilder();
            string remainder = filename;
            while (pos != -1)
            {
                result.Append(remainder.Substring(0, pos));
                if (pos == remainder.Length)
                {
                    remainder = string.Empty;
                    pos = -1;
                }
                else
                {
                    remainder = remainder.Substring(pos + 1);
                    pos = remainder.IndexOfAny(Path.GetInvalidFileNameChars());
                }
            }
            result.Append(remainder);
            return result.ToString();
        }

        #endregion

        #region Load Gedcom XML
        private void ResetData()
        {
            _dataloaded = false;
            sources = new List<FactSource>();
            individuals = new List<Individual>();
            families = new List<Family>();
            locations = new Dictionary<string, FactLocation>();
            occupations = new Dictionary<string, List<Individual>>();
            dataErrorTypes = new List<DataErrorGroup>();
        }

        public void LoadTree(string filename, ProgressBar pbS, ProgressBar pbI, ProgressBar pbF)
        {
            _loading = true;
            ResetData();
            Application.DoEvents();
            XmlDocument doc = GedcomToXml.Load(filename);
            xmlErrorbox.AppendText("Loading file " + filename + "\n");
            // First iterate through attributes of root finding all sources
            XmlNodeList list = doc.SelectNodes("GED/SOUR");
            pbS.Maximum = list.Count;
            int counter = 0;
            foreach (XmlNode n in list)
            {
                FactSource fs = new FactSource(n);
                sources.Add(fs);
                pbS.Value = counter++;
                Application.DoEvents(); // allows windows to process events and prevents application from appearing to have crashed.
            }
            xmlErrorbox.AppendText("Loaded " + counter + " sources.\n");
            pbS.Value = pbS.Maximum;
            // now iterate through child elements of root
            // finding all individuals
            list = doc.SelectNodes("GED/INDI");
            pbI.Maximum = list.Count;
            counter = 0;
            foreach (XmlNode n in list)
            {
                Individual individual = new Individual(n);
                individuals.Add(individual);
                AddOccupations(individual);
                pbI.Value = counter++;
                Application.DoEvents();
            }
            xmlErrorbox.AppendText("Loaded " + counter + " individuals.\n");
            pbI.Value = pbI.Maximum;
            // now iterate through child elements of root
            // finding all families
            list = doc.SelectNodes("GED/FAM");
            pbF.Maximum = list.Count;
            counter = 0;
            foreach (XmlNode n in list)
            {
                Family family = new Family(n);
                families.Add(family);
                pbF.Value = counter++;
                Application.DoEvents();
            }
            xmlErrorbox.AppendText("Loaded " + counter + " families.\n");
            pbF.Value = pbF.Maximum;
            CheckAllIndividualsAreInAFamily();
            xmlErrorbox.AppendText("Calculating Relationships using " + individuals[0].GedcomID + ": " +
                individuals[0].Name + " as starter person. Please wait.\n\n");
            SetRelations(individuals[0].GedcomID);
            xmlErrorbox.AppendText(PrintRelationCount());
            CountCensusFacts();
            SetParishes();
            FixIDs();
            SetDataErrorTypes();
            _loading = false;
            _dataloaded = true;
        }

        private void CountCensusFacts()
        {
            int censusFacts = 0;
            int resiFacts = 0;
            foreach (Individual ind in individuals)
            {
                censusFacts += ind.CensusFactCount;
                resiFacts += ind.ResiFactCount;
            }
            xmlErrorbox.AppendText("\nFound " + censusFacts + " census facts in GEDCOM File.");
            xmlErrorbox.AppendText("\nFound " + resiFacts + " residence facts in GEDCOM File.\n");
            if (censusFacts == 0 && resiFacts == 0)
            {
                xmlErrorbox.AppendText("\nFound no census or residence facts in GEDCOM File.\n");
                xmlErrorbox.AppendText("This is probably because you have recorded census facts as notes\n");
                xmlErrorbox.AppendText("This will mean that the census report will show everyone as not yet found on census\n");
                xmlErrorbox.AppendText("and the Lost Cousins report will report no-one with a census needing entered to Lost Cousins\n");
            }
        }

        private void AddOccupations(Individual individual)
        {
            HashSet<string> jobs = new HashSet<string>();
            foreach (Fact f in individual.GetFacts(Fact.OCCUPATION))
            {
                if (!jobs.Contains(f.Comment))
                {
                    List<Individual> workers;
                    if (!occupations.TryGetValue(f.Comment, out workers))
                    {
                        workers = new List<Individual>();
                        occupations.Add(f.Comment, workers);
                    }
                    workers.Add(individual);
                    jobs.Add(f.Comment);
                }
            }
        }

        private void CheckAllIndividualsAreInAFamily()
        {
            foreach (Family f in families)
            {
                if (f.husband != null)
                    f.husband.Infamily = true;
                if (f.wife != null)
                    f.wife.Infamily = true;
                foreach (Individual c in f.children)
                {
                    c.Infamily = true;
                    if (f.husband != null || f.wife != null)
                        c.HasParents = true;
                }
            }
            int added = 0;
            foreach (Individual ind in individuals)
            {
                if (!ind.isInFamily)
                {
                    families.Add(new Family(ind));
                    added++;
                }
            }
            if (added > 0)
                xmlErrorbox.AppendText("Added " + added + " lone individuals as single families.\n");
        }
        #endregion

        #region Properties

        public bool Loading { get { return _loading; } }

        public bool DataLoaded { get { return _dataloaded; } }

        public RichTextBox XmlErrorBox
        {
            get { return xmlErrorbox; }
            set { xmlErrorbox = value; }
        }

        public List<ExportFacts> AllFacts
        {
            get
            {
                List<ExportFacts> result = new List<ExportFacts>();
                foreach (Individual ind in individuals)
                {
                    foreach (Fact f in ind.AllFacts)
                        result.Add(new ExportFacts(ind, f));
                    foreach (Family fam in ind.FamiliesAsParent)
                        foreach (Fact famfact in fam.AllFacts)
                            result.Add(new ExportFacts(ind, famfact));
                }
                return result;
            }
        }

        public List<Family> AllFamilies
        {
            get { return families; }
        }

        public List<Individual> AllIndividuals
        {
            get { return individuals; }
        }

        public List<FactLocation> AllLocations
        {
            get { return locations.Values.ToList(); }
        }

        public List<IDisplayLocation> AllCountries
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    if (loc.Country != string.Empty)
                    {
                        FactLocation c = new FactLocation(loc.Country);
                        if (!result.Contains(c))
                            result.Add(c);
                    }
                }
                return result;
            }
        }

        public List<IDisplayLocation> AllRegions
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    if (loc.Region != string.Empty)
                    {
                        FactLocation r = new FactLocation(loc.Region + ", " + loc.Country);
                        if (!result.Contains(r))
                            result.Add(r);
                    }
                }
                return result;
            }
        }

        public List<IDisplayLocation> AllParishes
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    if (loc.Parish != string.Empty)
                    {
                        FactLocation p = new FactLocation(loc.Parish + ", " + loc.Region + ", " + loc.Country);
                        if (!result.Contains(p))
                            result.Add(p);
                    }
                }
                return result;
            }
        }

        public List<IDisplayLocation> AllAddresses
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    if (loc.Address != string.Empty)
                    {
                        FactLocation a = new FactLocation(loc.Address + ", " + loc.Parish + ", " + loc.Region + ", " + loc.Country);
                        if (!result.Contains(a))
                            result.Add(a);
                    }
                }
                return result;
            }
        }

        public List<IDisplayLocation> AllPlaces
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    if (loc.Place != string.Empty && !result.Contains(loc))
                        result.Add(loc);
                }
                return result;
            }
        }

        public int IndividualCount { get { return individuals.Count; } }

        #endregion

        #region Property Functions

        public FactLocation GetLocation(string place, string latitude, string longitude)
        {
            FactLocation loc;
            locations.TryGetValue(place, out loc);
            if (loc == null)
            {
                loc = new FactLocation(place, latitude, longitude);
                locations.Add(place, loc);
            }
            return loc; // should return object that is in list of locations 
        }

        public IEnumerable<Individual> GetAllRelationsOfType(int relationType)
        {
            return individuals.Where(ind => ind.RelationType == relationType);
        }

        public IEnumerable<Individual> GetUncertifiedFacts(string factType, int relationType)
        {
            return individuals.Where(ind =>
            {
                if (ind.RelationType == relationType)
                {
                    Fact f = ind.GetPreferredFact(factType);
                    return (f != null && !f.CertificatePresent);
                }
                return false;
            });
        }

        public List<Family> FindFamiliesWhereHusband(Individual ind)
        {
            List<Family> result = new List<Family>();
            foreach (Family f in families)
            {
                if (f.Husband != null && f.Husband == ind)
                    result.Add(f);
            }
            return result;
        }

        public List<Family> FindFamiliesWhereWife(Individual ind)
        {
            List<Family> result = new List<Family>();
            foreach (Family f in families)
            {
                if (f.Wife != null && f.Wife == ind)
                    result.Add(f);
            }
            return result;
        }

        public FactSource getGedcomSource(string gedcomID)
        {
            foreach (FactSource s in sources)
            {
                if (s.GedcomID == gedcomID)
                    return s;
            }
            return null;
        }

        public bool isMarried(Individual ind, FactDate fd)
        {
            if (ind.isSingleAtDeath())
                return false;
            return ind.FamiliesAsParent.Any(f =>
            {
                FactDate marriage = f.getPreferredFactDate(Fact.MARRIAGE);
                return (marriage != null && marriage.IsBefore(fd));
            });
        }

        public Individual GetIndividual(string individualID)
        {
            return individuals.FirstOrDefault(i => i.IndividualID == individualID);
        }

        public Individual GetGedcomIndividual(string gedcomID)
        {
            return individuals.FirstOrDefault(i => i.GedcomID == gedcomID);
        }

        public Family GetGedcomFamily(string gedcomID)
        {
            return families.FirstOrDefault(f => f.FamilyGed == gedcomID);
        }

        public IEnumerable<Individual> GetIndividualsAtLocation(FactLocation loc, int level)
        {
            return individuals.Where(i => i.isAtLocation(loc, level));
        }

        public IEnumerable<Family> GetFamiliesAtLocation(FactLocation loc, int level)
        {
            return families.Where(f => f.isAtLocation(loc, level));
        }

        public List<string> getSurnamesAtLocation(FactLocation loc) { return getSurnamesAtLocation(loc, FactLocation.PARISH); }
        public List<string> getSurnamesAtLocation(FactLocation loc, int level)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (Individual i in individuals)
            {
                if (!result.Contains(i.Surname) && i.isAtLocation(loc, level))
                    result.Add(i.Surname);
            }
            List<string> ls = result.ToList();
            ls.Sort();
            return ls;
        }

        private void FixIDs()
        {
            int lenID = individuals.Count.ToString().Length;
            foreach (Individual ind in individuals)
            {
                ind.FixIndividualID(lenID);
            }
            lenID = families.Count.ToString().Length;
            foreach (Family f in families)
            {
                f.FixFamilyGed(lenID);
            }
        }

        #endregion

        #region Loose Deaths

        public SortableBindingList<IDisplayLooseDeath> GetLooseDeaths()
        {
            SortableBindingList<IDisplayLooseDeath> result = new SortableBindingList<IDisplayLooseDeath>();
            foreach (Individual ind in individuals)
            {
                checkLooseDeath(ind, result);
            }
            return result;
        }

        private void checkLooseDeath(Individual indiv, SortableBindingList<IDisplayLooseDeath> result)
        {
            FactDate deathDate = indiv.DeathDate;
            FactDate toAdd = null;
            if (deathDate != FactDate.UNKNOWN_DATE && deathDate.DateType != FactDate.FactDateType.ABT && !deathDate.IsExact())
            {
                DateTime maxLiving = GetMaxLivingDate(indiv);
                DateTime minDeath = GetMinDeathDate(indiv);
                if (maxLiving > deathDate.StartDate)
                {
                    // the starting death date is before the last alive date
                    // so add to the list of loose deaths
                    if (minDeath < deathDate.EndDate)
                        toAdd = new FactDate(maxLiving, minDeath);
                    else if (deathDate.DateType == FactDate.FactDateType.BEF && minDeath != FactDate.MAXDATE
                          && deathDate.EndDate != FactDate.MAXDATE
                          && deathDate.EndDate.AddYears(1) == minDeath)
                        toAdd = new FactDate(maxLiving, minDeath);
                    else
                        toAdd = new FactDate(maxLiving, deathDate.EndDate);
                }
                else if (minDeath < deathDate.EndDate)
                {
                    // earliest death date before current latest death
                    // or they were two BEF dates (flagged by hour == 1)
                    // so add to the list of loose deaths
                    toAdd = new FactDate(deathDate.StartDate, minDeath);
                }
            }
            else if (deathDate == FactDate.UNKNOWN_DATE && indiv.CurrentAge.MinAge > 110)
            {
                // also check for empty death dates for people aged over 110
                DateTime maxLiving = GetMaxLivingDate(indiv);
                DateTime minDeath = GetMinDeathDate(indiv);
                if (minDeath != FactDate.MAXDATE)
                    toAdd = new FactDate(maxLiving, minDeath);
            }
            if (toAdd != null && toAdd != deathDate)
            {
                // we have a date to change and its not the same 
                // range as the existing death date
                Fact looseDeath = new Fact(Fact.LOOSEDEATH, toAdd);
                indiv.addFact(looseDeath);
                result.Add(indiv);
            }
        }

        private DateTime GetMaxLivingDate(Individual indiv)
        {
            DateTime maxdate = FactDate.MINDATE;
            List<Family> indfam = new List<Family>();
            if (indiv.isMale)
            {
                indfam = FindFamiliesWhereHusband(indiv);
            }
            else
            {
                indfam = FindFamiliesWhereWife(indiv);
            }
            // having got the families the individual is a parent of
            // get the max startdate of the birthdate of the youngest child
            // this then is the minimum point they were alive
            // subtract 9 months for a male
            bool childDate = false;
            foreach (Family fam in indfam)
            {
                FactDate marriageDate = fam.getPreferredFactDate(Fact.MARRIAGE);
                if (marriageDate.StartDate > maxdate && !marriageDate.IsLongYearSpan())
                {
                    maxdate = marriageDate.StartDate;
                }
                foreach (Individual child in fam.Children)
                {
                    FactDate birthday = child.BirthDate;
                    if (birthday.StartDate > maxdate)
                    {
                        maxdate = birthday.StartDate;
                        childDate = true;
                    }
                }
            }
            if (childDate && indiv.isMale && maxdate > FactDate.MINDATE)
            {
                // set to 9 months before birth if indiv is a father 
                // and we have changed maxdate from the MINDATE default
                // and the date is derived from a child not a marriage
                maxdate = maxdate.AddMonths(-9);
                // now set to Jan 1 of that year 9 months before birth to prevent 
                // very exact 9 months before dates
                maxdate = new DateTime(maxdate.Year, 1, 1);
            }
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.CENSUS));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.RESIDENCE));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.WITNESS));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.EMIGRATION));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.IMMIGRATION));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.ARRIVAL));
            maxdate = GetMaxDate(maxdate, GetMaxFactDate(indiv, Fact.DEPARTURE));
            // at this point we have the maximum point a person was alive
            // based on their oldest child and last census record and marriage date
            return maxdate;
        }

        private DateTime GetMaxDate(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        private DateTime GetMaxFactDate(Individual indiv, string factType)
        {
            DateTime maxdate = FactDate.MINDATE;
            IEnumerable<Fact> facts = indiv.GetFacts(factType);
            foreach (Fact f in facts)
            {
                DateTime d = factType == Fact.BIRTH ? new DateTime(f.FactDate.StartDate.Year, 1, 1) : f.FactDate.StartDate;
                if (d > maxdate)
                {
                    maxdate = d;
                }
            }
            return maxdate;
        }

        private DateTime GetMinDeathDate(Individual indiv)
        {
            FactDate deathDate = indiv.DeathDate;
            DateTime now = DateTime.Now;
            FactDate.FactDateType deathDateType = deathDate.DateType;
            FactDate.FactDateType birthDateType = indiv.BirthDate.DateType;
            DateTime minDeath = indiv.BirthDate.EndDate;
            if (minDeath.Year == 1) // filter out births where no year specified
                minDeath = FactDate.MAXDATE;
            if (minDeath != FactDate.MAXDATE)
            {
                minDeath = new DateTime(minDeath.Year + 110, 12, 31);
                if (birthDateType == FactDate.FactDateType.BEF)
                    minDeath = minDeath.AddYears(1);
                if (minDeath > now) // 110 years after birth is after todays date so we set to ignore
                    minDeath = FactDate.MAXDATE;
            }
            FactDate burialDate = indiv.GetPreferredFactDate(Fact.BURIAL);
            if (burialDate.EndDate < minDeath)
                minDeath = burialDate.EndDate;
            if (minDeath <= deathDate.EndDate)
                return minDeath;
            if (deathDateType == FactDate.FactDateType.BEF && minDeath != FactDate.MAXDATE)
                return minDeath;
            else
                return deathDate.EndDate;
        }

        #endregion

        #region TreeTops

        public IEnumerable<IDisplayIndividual> GetTreeTops(Predicate<Individual> filter)
        {
            return individuals.Where(ind => !ind.HasParents && filter(ind));
        }

        #endregion

        #region WarDead

        public IEnumerable<IDisplayIndividual> GetWarDead(Predicate<Individual> filter)
        {
            return individuals.Where(ind => ind.isMale && !ind.isDeathKnown() && filter(ind));
        }

        #endregion

        #region Relationship Functions

        private ParentalGroup CreateFamilyGroup(Individual i)
        {
            Family f = i.FamiliesAsChild.FirstOrDefault();
            return (f == null) ?
                    new ParentalGroup(i, null, null, null) :
                    new ParentalGroup(i, f.Husband, f.Wife, f.getPreferredFact(Fact.MARRIAGE));
        }

        private void ClearRelations()
        {
            foreach (Individual i in individuals)
            {
                i.RelationType = Individual.UNKNOWN;
                i.BudgieCode = string.Empty;
                i.Ahnentafel = 0;
            }
        }

        private void AddToQueue(Queue<Individual> queue, IEnumerable<Individual> list)
        {
            foreach (Individual i in list)
            {
                queue.Enqueue(i);
            }
        }

        private void AddParentsToQueue(Individual indiv, Queue<Individual> queue, bool setAhnenfatel)
        {
            IEnumerable<Family> families = indiv.FamiliesAsChild;
            foreach (Family family in families)
            {
                // add parents to queue
                if (family.Husband != null && family.Husband.RelationType == Individual.UNKNOWN)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT)
                    {
                        family.Husband.Ahnentafel = indiv.Ahnentafel * 2;
                        if (family.Husband.Ahnentafel > maxAhnentafel)
                            maxAhnentafel = family.Husband.Ahnentafel;
                    }
                    queue.Enqueue(family.Husband);
                }
                if (family.Wife != null && family.Wife.RelationType == Individual.UNKNOWN)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT)
                    {
                        family.Wife.Ahnentafel = indiv.Ahnentafel * 2 + 1;
                        if (family.Wife.Ahnentafel > maxAhnentafel)
                            maxAhnentafel = family.Wife.Ahnentafel;
                    }
                    queue.Enqueue(family.Wife);
                }
            }
        }

        private void AddChildrenToQueue(Individual indiv, Queue<Individual> queue, bool isRootPerson)
        {
            IEnumerable<Family> families = indiv.FamiliesAsParent;
            foreach (Family family in families)
            {
                foreach (Individual child in family.children)
                {
                    // add child to queue
                    if (child.RelationType == Individual.BLOOD || child.RelationType == Individual.UNKNOWN)
                    {
                        child.RelationType = Individual.BLOOD;
                        if (isRootPerson)
                            child.Ahnentafel = indiv.Ahnentafel - 2;
                        else
                            child.Ahnentafel = indiv.Ahnentafel - 1;
                        child.BudgieCode = "-" + Math.Abs(child.Ahnentafel).ToString().PadLeft(2, '0') + "c";
                        queue.Enqueue(child);
                    }
                }
                family.setBudgieCode(indiv, 2);
            }
        }

        public void SetRelations(string startGed)
        {
            ClearRelations();
            SetFamilies();
            Individual rootPerson = GetGedcomIndividual(startGed);
            Individual ind = rootPerson;
            ind.RelationType = Individual.DIRECT;
            ind.Ahnentafel = 1;
            maxAhnentafel = 1;
            Queue<Individual> queue = new Queue<Individual>();
            queue.Enqueue(ind);
            while (queue.Count > 0)
            {
                // now take an item from the queue
                ind = queue.Dequeue();
                // set them as a direct relation
                ind.RelationType = Individual.DIRECT;
                AddParentsToQueue(ind, queue, true);
                Application.DoEvents();
            }
            int lenAhnentafel = maxAhnentafel.ToString().Length;
            // we have now added all direct ancestors
            IEnumerable<Individual> directs = GetAllRelationsOfType(Individual.DIRECT);
            foreach (Individual i in directs)
            {
                // add all direct ancestors budgie codes
                i.BudgieCode = (i.Ahnentafel).ToString().PadLeft(lenAhnentafel, '0') + "d";
            }
            AddToQueue(queue, directs);
            while (queue.Count > 0)
            {
                // get the next person
                ind = queue.Dequeue();
                IEnumerable<Family> families = ind.FamiliesAsParent;
                foreach (Family family in families)
                {
                    // if the spouse of a direct ancestor is not a direct
                    // ancestor then they are only related by marriage
                    family.setSpouseRelation(ind, Individual.MARRIEDTODB);
                    // all children of direct ancestors and blood relations
                    // are blood relations
                    family.setChildRelation(queue, Individual.BLOOD);
                    family.setBudgieCode(ind, lenAhnentafel);
                }
                Application.DoEvents();
            }
            // we have now set all direct ancestors and all blood relations
            // now we need to set all descendants of root person's budgie code
            //queue.Enqueue(rootPerson);
            //bool isRootPerson = true;
            //while (queue.Count > 0)
            //{
            //    // now take an item from the queue
            //    ind = queue.Dequeue();
            //    // set them as a direct relation
            //    if (ind != rootPerson)
            //        ind.RelationType = Individual.BLOOD;
            //    addChildrenToQueue(ind, queue, isRootPerson);
            //    isRootPerson = false;
            //    Application.DoEvents();
            //}
            // all that remains is to loop through the marriage relations
            IEnumerable<Individual> marriedDBs = GetAllRelationsOfType(Individual.MARRIEDTODB);
            AddToQueue(queue, marriedDBs);
            while (queue.Count > 0)
            {
                // get the next person
                ind = queue.Dequeue();
                // first only process this individual if they are related by marriage or still unknown
                int relationship = ind.RelationType;
                if (relationship == Individual.MARRIAGE ||
                    relationship == Individual.MARRIEDTODB ||
                    relationship == Individual.UNKNOWN)
                {
                    // set this individual to be related by marriage
                    if (relationship == Individual.UNKNOWN)
                        ind.RelationType = Individual.MARRIAGE;
                    AddParentsToQueue(ind, queue, false);
                    IEnumerable<Family> families = ind.FamiliesAsParent;
                    foreach (Family family in families)
                    {
                        family.setSpouseRelation(ind, Individual.MARRIAGE);
                        // children of relatives by marriage that we haven't previously 
                        // identified are also relatives by marriage
                        family.setChildRelation(queue, Individual.MARRIAGE);
                    }
                }
                Application.DoEvents();
            }
        }

        private void SetFamilies()
        {
            foreach (Family f in families)
            {
                if (f.Husband != null)
                    f.Husband.FamiliesAsParent.Add(f);
                if (f.Wife != null)
                    f.Wife.FamiliesAsParent.Add(f);
                foreach (Individual child in f.Children)
                {
                    if (child != null)
                        child.FamiliesAsChild.Add(f);
                }
            }
        }

        public string PrintRelationCount()
        {
            StringBuilder sb = new StringBuilder();
            int[] relations = new int[Individual.UNSET + 1];
            foreach (Individual i in individuals)
                relations[i.RelationType]++;
            sb.Append("Direct Ancestors : " + relations[Individual.DIRECT] + "\n");
            sb.Append("Blood Relations : " + relations[Individual.BLOOD] + "\n");
            sb.Append("Married to Blood or Direct Relation : " + relations[Individual.MARRIEDTODB] + "\n");
            sb.Append("Related by Marriage : " + relations[Individual.MARRIAGE] + "\n");
            sb.Append("Unknown relation : " + relations[Individual.UNKNOWN] + "\n");
            if (relations[Individual.UNSET] > 0)
                sb.Append("Failed to set relationship : " + relations[Individual.UNSET] + "\n");
            return sb.ToString();
        }

        #endregion

        #region Parish Functions

        private void SetParishes()
        {
            foreach (FactLocation loc in locations.Values)
            {
                // do something with parishes

            }
        }

        #endregion

        #region Registrations

        //public void processRegistration(String filename, RegistrationsProcessor rp,
        //    List<Registration> sourceRegs, BaseOutputFormatter formatter)
        //{
        //    TextWriter output = new StreamWriter(filename + ".csv");
        //    formatter.printHeader(output);
        //    List<Registration> regs = rp.processRegistrations(sourceRegs);
        //    foreach (Registration r in regs)
        //    {
        //        formatter.printItem(r, output);
        //    }
        //    FamilyTree.Instance.xmlErrorbox.AppendText("written " + regs.Count + " records to " + filename + "\n");
        //    output.Close();
        //}

        //public List<Registration> getAllBirthRegistrations()
        //{
        //    List<Registration> result = new List<Registration>();
        //    foreach (Individual i in individuals)
        //    {
        //        ParentalGroup pg = CreateFamilyGroup(i);
        //        result.Add(new BirthRegistration(pg));
        //    }
        //    return result;
        //}

        //public List<Registration> getAllMarriageRegistrations()
        //{
        //    List<Registration> result = new List<Registration>();
        //    foreach (Individual i in individuals)
        //    {
        //        if (!i.isSingleAtDeath())
        //        {
        //            ParentalGroup pg = CreateFamilyGroup(i);
        //            List<Family> indfam = i.isMale ? FindFamiliesWhereHusband(i) : FindFamiliesWhereWife(i);
        //            if (indfam.Count == 0)
        //                result.Add(new MarriageRegistration(pg, null, null));
        //            else if (i.isMale)
        //            {
        //                foreach (Family f in indfam)
        //                {
        //                    ParentalGroup pg2 = CreateFamilyGroup(f.Wife);
        //                    result.Add(new MarriageRegistration(pg, pg2, f));
        //                }
        //            }
        //        }

        //    }
        //    return result;
        //}

        //public List<Registration> getAllDeathRegistrations()
        //{
        //    List<Registration> result = new List<Registration>();
        //    foreach (Individual i in individuals)
        //    {
        //        if (i.DeathDate != FactDate.UNKNOWN_DATE)
        //        {
        //            // only include dead individuals
        //            ParentalGroup pg = CreateFamilyGroup(i);
        //            List<Family> indfam = i.isMale ? FindFamiliesWhereHusband(i) : FindFamiliesWhereWife(i);
        //            if (indfam.Count == 0)
        //                result.Add(new DeathRegistration(pg, null, Family.SINGLE));
        //            else
        //            {
        //                foreach (Family f in indfam)
        //                {
        //                    if (i.isMale)
        //                        result.Add(new DeathRegistration(pg, f.Wife, f.MaritalStatus));
        //                    else
        //                        result.Add(new DeathRegistration(pg, f.Husband, f.MaritalStatus));
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        //public List<Registration> getAllCensusRegistrations(FactDate censusDate, bool censusDone, bool includeResidence, bool lostCousinsCheck)
        //{
        //    List<Registration> result = new List<Registration>();
        //    if (censusDate != null)
        //    {
        //        foreach (Family f in families)
        //        {
        //            CensusFamily cf = new CensusFamily(f, censusDate);
        //            if (cf.process(censusDate, censusDone, includeResidence, lostCousinsCheck))
        //                result.Add(new CensusRegistration(null, censusDate, cf));
        //        }
        //    }
        //    return result;
        //}

        public IEnumerable<CensusFamily> GetAllCensusFamilies(FactDate censusDate, bool censusDone, bool includeResidence, bool lostCousinsCheck)
        {
            if (censusDate != null)
            {
                foreach (Family f in families)
                {
                    CensusFamily cf = new CensusFamily(f, censusDate);
                    if (cf.Process(censusDate, censusDone, includeResidence, lostCousinsCheck))
                        yield return cf;
                }
            }
        }
        #endregion

        #region Displays

        public SortableBindingList<IDisplayIndividual> AllDisplayIndividuals
        {
            get
            {
                SortableBindingList<IDisplayIndividual> result = new SortableBindingList<IDisplayIndividual>();
                foreach (IDisplayIndividual i in individuals)
                    result.Add(i);
                return result;
            }
        }

        public SortableBindingList<IDisplayFamily> AllDisplayFamilies
        {
            get
            {
                SortableBindingList<IDisplayFamily> result = new SortableBindingList<IDisplayFamily>();
                foreach (IDisplayFamily f in families)
                    result.Add(f);
                return result;
            }
        }

        public SortableBindingList<IDisplayOccupation> AllDisplayOccupations
        {
            get
            {
                SortableBindingList<IDisplayOccupation> result = new SortableBindingList<IDisplayOccupation>();
                foreach (string occ in occupations.Keys)
                    result.Add(new DisplayOccupation(occ, occupations[occ].Count));
                return result;
            }
        }

        public SortableBindingList<Individual> AllWorkers(string job)
        {
            return new SortableBindingList<Individual>(occupations[job]);
        }

        public SortableBindingList<IDisplayLCReport> LCReport(bool blnDirectBlood)
        {
            SortableBindingList<IDisplayLCReport> result = new SortableBindingList<IDisplayLCReport>();
            foreach (Individual i in individuals)
            {
                if (!blnDirectBlood || (blnDirectBlood && i.isBloodDirect))
                {
                    // valid to add check LC status && age within range
                    if (!i.BirthDate.IsAfter(CensusDate.UKCENSUS1911) && !i.DeathDate.IsBefore(CensusDate.UKCENSUS1841))
                    {
                        //born & died within census periods so we can add them to result
                        result.Add(i);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Data Errors

        private void SetDataErrorTypes()
        {
            dataErrorTypes = new List<DataErrorGroup>();
            List<DataError>[] errors = new List<DataError>[15];
            for (int i = 0; i < 15; i++)
                errors[i] = new List<DataError>();
            // calculate error lists
            foreach (Individual ind in AllIndividuals)
            {
                try
                {
                    if (ind.DeathDate != FactDate.UNKNOWN_DATE)
                    {
                        if (ind.BirthDate.IsAfter(ind.DeathDate))
                            errors[0].Add(new DataError(ind, "Died " + ind.DeathDate + " before born"));
                        if (ind.BurialDate != null && ind.BirthDate.IsAfter(ind.BurialDate))
                            errors[0].Add(new DataError(ind, "Buried " + ind.BurialDate + " before born"));
                        if (ind.BurialDate != null && ind.BurialDate.IsBefore(ind.DeathDate) && !ind.BurialDate.Overlaps(ind.DeathDate))
                            errors[7].Add(new DataError(ind, "Buried " + ind.BurialDate + " before died " + ind.DeathDate));
                        int minAge = ind.getMinAge(ind.DeathDate);
                        if (minAge > 110)
                            errors[8].Add(new DataError(ind, "Aged over 110 before died " + ind.DeathDate));
                    }
                    foreach (Fact f in ind.AllFacts)
                    {
                        if (f.FactType != Fact.BIRTH && f.FactDate.IsBefore(ind.BirthDate))
                            errors[9].Add(new DataError(ind, f.FactType + " fact recorded: " + f.FactDate + " before individual was born"));
                    }
                    foreach (Family asChild in ind.FamiliesAsChild)
                    {
                        Individual father = asChild.Husband;
                        if (father != null && ind.BirthDate.StartDate.Year != 1)
                        {
                            int minAge = father.getMinAge(ind.BirthDate);
                            int maxAge = father.getMaxAge(ind.BirthDate);
                            if (minAge > 90)
                                errors[1].Add(new DataError(ind, "Father " + father.Name + " born " + father.BirthDate + " is more than 90 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[5].Add(new DataError(ind, "Father " + father.Name + " born " + father.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (father.DeathDate != FactDate.UNKNOWN_DATE && ind.BirthDate != FactDate.UNKNOWN_DATE)
                            {
                                FactDate conception = ind.BirthDate.SubtractMonths(9);
                                if (father.DeathDate.IsBefore(conception))
                                    errors[4].Add(new DataError(ind, "Father " + father.Name + " died " + father.DeathDate + " more than 9 months before individual was born"));
                            }
                        }
                        Individual mother = asChild.Wife;
                        if (mother != null && ind.BirthDate.StartDate.Year != 1)
                        {
                            int minAge = mother.getMinAge(ind.BirthDate);
                            int maxAge = mother.getMaxAge(ind.BirthDate);
                            if (minAge > 60)
                                errors[2].Add(new DataError(ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is more than 60 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[6].Add(new DataError(ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (mother.DeathDate != FactDate.UNKNOWN_DATE && mother.DeathDate.IsBefore(ind.BirthDate))
                                errors[3].Add(new DataError(ind, "Mother " + mother.Name + " died " + mother.DeathDate + " which is before individual was born"));
                        }
                    }
                    foreach (Family asParent in ind.FamiliesAsParent)
                    {
                        Individual spouse = asParent.Spouse(ind);
                        if (asParent.MarriageDate != null && spouse != null)
                        {
                            if (ind.DeathDate != null && asParent.MarriageDate.IsAfter(ind.DeathDate))
                                errors[10].Add(new DataError(ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after individual died " + ind.DeathDate));
                            if (spouse.DeathDate != null && asParent.MarriageDate.IsAfter(spouse.DeathDate))
                                errors[11].Add(new DataError(ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after spouse died " + spouse.DeathDate));
                            int maxAge = ind.getMaxAge(asParent.MarriageDate);
                            if (maxAge < 13)
                                errors[12].Add(new DataError(ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before individual was 13 years old"));
                            maxAge = spouse.getMaxAge(asParent.MarriageDate);
                            if (maxAge < 13)
                                errors[13].Add(new DataError(ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before spouse born " + spouse.BirthDate + " was 13 years old"));
                            //if (ind.FirstMarriage != null && ind.FirstMarriage.MarriageDate != null)
                            //{
                            //    if (asParent.MarriageDate.isAfter(ind.FirstMarriage.MarriageDate))
                            //    {  // we have a later marriage now see if first marriage wife

                            //    }
                            //}
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unexpected Error Checking for inconsistencies in your data\nPlease report this on the issues page at http://ftanalyzer.codeplex.com \nError was " + e.Message);
                }
            }
            dataErrorTypes.Add(new DataErrorGroup("Birth after death/burial", errors[0]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after father aged 90+", errors[1]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after mother aged 60+", errors[2]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after mothers death", errors[3]));
            dataErrorTypes.Add(new DataErrorGroup("Birth more than 9m after fathers death", errors[4]));
            dataErrorTypes.Add(new DataErrorGroup("Birth before father aged 13", errors[5]));
            dataErrorTypes.Add(new DataErrorGroup("Birth before mother aged 13", errors[6]));
            dataErrorTypes.Add(new DataErrorGroup("Burial before death", errors[7]));
            dataErrorTypes.Add(new DataErrorGroup("Aged more than 120 at death", errors[8]));
            dataErrorTypes.Add(new DataErrorGroup("Facts dated before birth", errors[9]));
            dataErrorTypes.Add(new DataErrorGroup("Marriage after death", errors[10]));
            dataErrorTypes.Add(new DataErrorGroup("Marriage after spouse's death", errors[11]));
            dataErrorTypes.Add(new DataErrorGroup("Marriage before aged 13", errors[12]));
            dataErrorTypes.Add(new DataErrorGroup("Marriage before spouse aged 13", errors[13]));
            //            dataErrorTypes.Add(new DataErrorGroup("Later marriage before previous spouse died", errors[14]));
        }

        public void SetDataErrorsCheckedDefaults(CheckedListBox list)
        {
            list.Items.Clear();
            foreach (DataErrorGroup dataError in dataErrorTypes)
            {
                int index = list.Items.Add(dataError);
                list.SetItemChecked(index, true);
            }
        }

        public List<DataError> DataErrors(CheckedListBox list)
        {
            List<DataError> errors = new List<DataError>();
            foreach (int indexChecked in list.CheckedIndices)
            {
                DataErrorGroup item = (DataErrorGroup)list.Items[indexChecked];
                errors.AddRange(item.Errors);
            }
            return errors;
        }
        #endregion

        #region Census Searching

        public void SearchCensus(string censusCountry, int censusYear, Individual person, int censusProvider)
        {
            string uri = null;

            switch (censusProvider)
            {
                case 0: uri = BuildAncestryQuery(censusCountry, censusYear, person); break;
                case 1: uri = BuildFindMyPastQuery(censusCountry, censusYear, person); break;
                case 2: uri = BuildFreeCenQuery(censusCountry, censusYear, person); break;
                case 3:
                    string country = person.BestLocation(new FactDate(censusYear.ToString())).Country;
                    uri = BuildFamilySearchQuery(country, censusYear, person); break;
            }
            if (uri != null)
            {
                Process.Start(uri);
            }
        }

        private string BuildFamilySearchQuery(string country, int censusYear, Individual person)
        {
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            // bad  https://familysearch.org/search/record/results%23count=20&query=%2Bgivenname%3ACharles~%20%2Bsurname%3AGalloway~%20%2Brecord_type%3A(3)&collection_id=2046756
            // good https://familysearch.org/search/record/results#count=20&query=%2Bgivenname%3ACharles%7E%20%2Bsurname%3ABisset%7E%20%2Brecord_country%3AScotland%20%2Brecord_type%3A%283%29&collection_id=2046756
            StringBuilder path = new StringBuilder();
            path.Append("https://www.familysearch.org/search/record/results#count=20&query=");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                path.Append("%2B" + FamilySearch.GIVENNAME + "%3A" + HttpUtility.UrlEncode(person.Forenames) + "%7E%20");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            {
                path.Append("%2B" + FamilySearch.SURNAME + "%3A" + HttpUtility.UrlEncode(surname) + "%7E%20");
            }
            path.Append("%2B" + FamilySearch.RECORD_TYPE + "%3A%283%29");
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year - 1;
                int endYear = person.BirthDate.EndDate.Year + 1;
                path.Append("%2B" + FamilySearch.BIRTH_YEAR + "%3A" + startYear + "-" + endYear + "%7E%20");
            }
            if (person.BirthLocation != null)
            {
                string location = person.BirthLocation.GetLocation(FactLocation.REGION).ToString().Replace(",", "");
                path.Append("%2B" + FamilySearch.BIRTH_LOCATION + "%3A" + HttpUtility.UrlEncode(location) + "%7E%20");
            }
            int collection = FamilySearch.CensusCollectionID(country, censusYear);
            if (collection > 0)
                path.Append("&collection_id=" + collection);
            return path.ToString();
        }

        private string BuildAncestryQuery(string censusCountry, int censusYear, Individual person)
        {
            UriBuilder uri = new UriBuilder();
            uri.Host = "search.ancestry.co.uk";
            uri.Path = "cgi-bin/sse.dll";
            StringBuilder query = new StringBuilder();
            if (censusCountry.Equals(Countries.UNITED_KINGDOM))
            {
                query.Append("gl=" + censusYear + "uki&");
                query.Append("gss=ms_f-68&");
            }
            else if (censusCountry.Equals(Countries.IRELAND))
            {
                MessageBox.Show("Sorry searching the Ireland census on Ancestry for " + censusYear + " is not supported by FTAnalyzer at this time");
                return null;
            }
            else if (censusCountry.Equals(Countries.UNITED_STATES))
            {
                query.Append("db=" + censusYear + "usfedcen&");
                query.Append("gss=ms_db&");
            }
            else if (censusCountry.Equals(Countries.CANADA))
                query.Append("db=" + censusYear + "canada&");
            query.Append("rank=1&");
            query.Append("new=1&");
            query.Append("so=3&");
            query.Append("MSAV=1&");
            query.Append("msT=1&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                query.Append("gsfn=" + HttpUtility.UrlEncode(person.Forenames) + "&");
            }
            string surname = string.Empty;
            if (person.Surname != "?" && person.Surname.ToUpper() != "UNKNOWN")
            {
                surname = person.Surname;
            }
            if (person.MarriedName != "?" && person.MarriedName.ToUpper() != "UNKNOWN" && person.MarriedName != person.Surname)
            {
                surname += " " + person.MarriedName;
            }
            surname = surname.Trim();
            query.Append("gsln=" + HttpUtility.UrlEncode(surname) + "&");
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear + 1;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear - 1;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 1) / 2;
                    if (2 < range && range < 5) range = 5;
                    if (range > 5) range = 10;
                }
                query.Append("msbdy=" + year + "&");
                query.Append("msbdp=" + range + "&");
            }
            if (person.BirthLocation != null)
            {
                string location = person.BirthLocation.GetLocation(FactLocation.PARISH).ToString();
                query.Append("msbpn__ftp=" + HttpUtility.UrlEncode(location) + "&");
            }
            query.Append("uidh=2t2");
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private string BuildFreeCenQuery(string censusCountry, int censusYear, Individual person)
        {
            if (!censusCountry.Equals(Countries.UNITED_KINGDOM))
            {
                MessageBox.Show("Sorry only UK searches can be done on FreeCEN.");
                return null;
            }
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            UriBuilder uri = new UriBuilder();
            uri.Host = "www.freecen.org.uk";
            uri.Path = "/cgi/search.pl";
            StringBuilder query = new StringBuilder();
            query.Append("y=" + censusYear + "&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                int pos = person.Forenames.IndexOf(" ");
                string forename = person.Forenames;
                if (pos > 0)
                    forename = person.Forenames.Substring(0, pos); //strip out any middle names as FreeCen searches better without then
                query.Append("g=" + HttpUtility.UrlEncode(forename) + "&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            {
                query.Append("s=" + HttpUtility.UrlEncode(surname) + "&");
                query.Append("p=on&");
            }
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear + 1;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear - 1;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 5) / 2;
                }
                if (range == 0)
                {
                    query.Append("r=0&");
                }
                else if (range <= 2)
                {
                    query.Append("r=2&");
                }
                else if (range <= 5)
                {
                    query.Append("r=5&");
                }
                else
                {
                    query.Append("r=10&");
                }
                query.Append("a=" + year + "&");
            }
            if (person.BirthLocation != null)
            {
                string location = person.BirthLocation.Parish;
                query.Append("t=" + HttpUtility.UrlEncode(location) + "&");
                query.Append("b=" + person.BirthLocation.FreeCenCountyCode + "&");
            }
            query.Append("c=all&"); // initially set to search all counties need a routine to return FreeCen county codes 
            query.Append("z=Find&"); // executes search
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private string BuildFindMyPastQuery(string censusCountry, int censusYear, Individual person)
        {
            // bad  http://www.findmypast.co.uk/CensusPersonSearchResultServlet?basicSearch=false&censusYear=1881&occupation=&otherForenames=&otherLastName=&pageDirection=&recordPosition=0&residence=&route=&searchHouseholds=6,15&searchInstitutions=9&searchVessels=11,12&sortOrder=nameAsc&startNewSearch=startNewSearch&forenames=Michael&fns=fns&lastName=Tebbutt&sns=sns&yearOfBirth=1867&yearOfBirthVariation=1&birthPlace=Streatham&country=England&coIdList=Surrey++++++++++++++++++++++++++++++++++%3a3%2c4+++++++++++++++++++++++++++
            // good http://www.findmypast.co.uk/CensusPersonSearchResultServlet?basicSearch=false&censusYear=1881&occupation=&otherForenames=&otherLastName=&pageDirection=&recordPosition=0&residence=&route=&searchHouseholds=6,15&searchInstitutions=9&searchVessels=11,12&sortOrder=nameAsc&startNewSearch=startNewSearch&forenames=C&fns=fns&lastName=Whitethread&sns=sns&yearOfBirth=1867&yearOfBirthVariation=1&birthPlace=Streatham&country=England&coIdList=Surrey++++++++++++++++++++++++++++++++++%3a3%2c4+++++++++++++++++++++++++++
            if (!censusCountry.Equals(Countries.UNITED_KINGDOM))
            {
                MessageBox.Show("Sorry non UK census searching of Find My Past isn't supported in this version of FTAnalyzer");
                return null;
            }
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            UriBuilder uri = new UriBuilder();
            uri.Host = "www.findmypast.co.uk";
            uri.Path = "/CensusPersonSearchResultServlet";
            StringBuilder query = new StringBuilder();
            query.Append("basicSearch=false&");
            query.Append("censusYear=" + censusYear + "&");
            query.Append("occupation=&");
            query.Append("otherForenames=&");
            query.Append("otherLastName=&");
            query.Append("pageDirection=&");
            query.Append("recordPosition=0&");
            query.Append("residence=&");
            query.Append("route=&");
            query.Append("searchHouseholds=6,15&");
            query.Append("searchInstitutions=9&");
            query.Append("searchVessels=11,12&");
            query.Append("sortOrder=nameAsc&");
            query.Append("startNewSearch=startNewSearch&");

            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                int pos = person.Forenames.IndexOf(" ");
                string forenames = person.Forenames;
                if (pos > 0)
                    forenames = person.Forenames.Substring(0, pos); //strip out any middle names as FreeCen searches better without then
                query.Append("forenames=" + HttpUtility.UrlEncode(forenames) + "&");
                query.Append("fns=fns&");
            }
            else
            {
                query.Append("forenames=&fns=fns&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            {
                query.Append("lastName=" + HttpUtility.UrlEncode(surname) + "&");
                query.Append("sns=sns&");
            }
            else
            {
                query.Append("lastName=&sns=sns&");
            }
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear + 1;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear - 1;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 3) / 2;
                    if (range > 5) range = 10;
                }
                query.Append("yearOfBirth=" + year + "&");
                query.Append("yearOfBirthVariation=" + range + "&");
            }
            else
            {
                query.Append("yearOfBirth=&yearOfBirthVariation=&");
            }
            if (person.BirthLocation != null)
            {
                query.Append("birthPlace=" + HttpUtility.UrlEncode(person.BirthLocation.Parish) + "&");
                Tuple<string, string> area = person.BirthLocation.FindMyPastCountyCode;
                if (area != null)
                {
                    query.Append("country=" + HttpUtility.UrlEncode(area.Item1) + "&");
                    query.Append("coIdList=" + HttpUtility.UrlEncode(area.Item2));
                }
                else
                {
                    query.Append("country=&coIdList=");
                }
            }
            else
            {
                query.Append("birthPlace=&country=&coIdList=");
            }
            uri.Query = query.ToString();
            return uri.ToString();
        }
        #endregion
    }
}
