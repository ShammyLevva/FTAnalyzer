using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using FTAnalyzer.Utilities;

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

        public void LoadTree(XmlDocument doc, ProgressBar pbS, ProgressBar pbI, ProgressBar pbF)
        {
            _loading = true;
            ResetData();
            Application.DoEvents();
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
            SetParishes();
            FixIDs();
            SetDataErrorTypes();
            _loading = false;
            _dataloaded = true;
        }

        private void AddOccupations(Individual individual)
        {
            HashSet<string> jobs = new HashSet<string>();
            foreach (Fact f in individual.getFacts(Fact.OCCUPATION))
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
                if (!ind.isInFamily())
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

        public List<Fact> AllFacts
        {
            get
            {
                List<Fact> result = new List<Fact>();
                foreach (Individual ind in individuals)
                    result.AddRange(ind.AllFacts);
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

        public List<IDisplayLocation> AllDisplayLocations
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in locations.Values.ToList())
                {
                    result.Add(loc);
                }
                return result;
            }
        }

        public List<IDisplayLocation> AllCountries
        {
            get
            {
                List<IDisplayLocation> result = new List<IDisplayLocation>();
                foreach (FactLocation loc in AllDisplayLocations)
                {
                    if (loc.country != string.Empty)
                    {
                        FactLocation c = new FactLocation(loc.country);
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
                foreach (FactLocation loc in AllDisplayLocations)
                {
                    if (loc.region != string.Empty)
                    {
                        FactLocation r = new FactLocation(loc.region + ", " + loc.country);
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
                foreach (FactLocation loc in AllDisplayLocations)
                {
                    if (loc.parish != string.Empty)
                    {
                        FactLocation p = new FactLocation(loc.parish + ", " + loc.region + ", " + loc.country);
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
                foreach (FactLocation loc in AllDisplayLocations)
                {
                    if (loc.address != string.Empty)
                    {
                        FactLocation a = new FactLocation(loc.Address + ", " + loc.parish + ", " + loc.region + ", " + loc.country);
                        if (!result.Contains(a))
                            result.Add(a);
                    }
                }
                return result;
            }
        }

        public int IndividualCount { get { return individuals.Count; } }

        #endregion

        #region Property Functions

        public FactLocation GetLocation(string place)
        {
            FactLocation loc;
            locations.TryGetValue(place, out loc);
            if (loc == null)
            {
                loc = new FactLocation(place);
                locations.Add(place, loc);
            }
            return loc; // should return object that is in list of locations 
        }

        public List<Individual> getAllRelationsOfType(int relationType)
        {
            List<Individual> result = new List<Individual>();
            foreach (Individual ind in individuals)
                if (ind.RelationType == relationType)
                    result.Add(ind);
            return result;
        }

        public List<Individual> getUncertifiedFacts(string factType, int relationType)
        {
            List<Individual> result = new List<Individual>();
            foreach (Individual ind in individuals)
                if (ind.RelationType == relationType)
                {
                    Fact f = ind.getPreferredFact(factType);
                    if (f != null && !f.CertificatePresent)
                        result.Add(ind);
                }
            return result;
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
            List<Family> families = ind.FamiliesAsParent;
            foreach (Family f in families)
            {
                FactDate marriage = f.getPreferredFactDate(Fact.MARRIAGE);
                if (marriage != null && marriage.isBefore(fd))
                    return true;
            }
            return false;
        }

        public Individual getIndividual(string individualID)
        {
            foreach (Individual i in individuals)
            {
                if (i.IndividualID == individualID)
                    return i;
            }
            return null;
        }

        public Individual getGedcomIndividual(string gedcomID)
        {
            foreach (Individual i in individuals)
            {
                if (i.GedcomID == gedcomID)
                    return i;
            }
            return null;
        }

        public Family getGedcomFamily(string gedcomID)
        {
            foreach (Family f in families)
            {
                if (f.FamilyGed == gedcomID)
                    return f;
            }
            return null;
        }

        public List<Individual> getIndividualsAtLocation(FactLocation loc, int level)
        {
            List<Individual> result = new List<Individual>();
            foreach (Individual i in individuals)
            {
                if (!result.Contains(i) && i.isAtLocation(loc, level))
                    result.Add(i);
            }
            return result;
        }

        public List<Family> getFamiliesAtLocation(FactLocation loc, int level)
        {
            List<Family> result = new List<Family>();
            foreach (Family f in families)
            {
                if (!result.Contains(f) && f.isAtLocation(loc, level))
                    result.Add(f);
            }
            return result;
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
            if (deathDate != FactDate.UNKNOWN_DATE && deathDate.Type != FactDate.FactDateType.ABT && !deathDate.isExact())
            {
                DateTime maxLiving = getMaxLivingDate(indiv);
                DateTime minDeath = getMinDeathDate(indiv);
                if (maxLiving > deathDate.StartDate)
                {
                    // the starting death date is before the last alive date
                    // so add to the list of loose deaths
                    if (minDeath < deathDate.EndDate)
                        toAdd = new FactDate(maxLiving, minDeath);
                    else if (deathDate.Type == FactDate.FactDateType.BEF && minDeath != FactDate.MAXDATE
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
                DateTime maxLiving = getMaxLivingDate(indiv);
                DateTime minDeath = getMinDeathDate(indiv);
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

        private DateTime getMaxLivingDate(Individual indiv)
        {
            DateTime maxdate = FactDate.MINDATE;
            List<Family> indfam = new List<Family>();
            if (indiv.isMale())
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
                if (marriageDate.StartDate > maxdate && !marriageDate.isLongYearSpan())
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
            if (childDate && indiv.isMale() && maxdate > FactDate.MINDATE)
            {
                // set to 9 months before birth if indiv is a father 
                // and we have changed maxdate from the MINDATE default
                // and the date is derived from a child not a marriage
                maxdate = maxdate.AddMonths(-9);
                // now set to Jan 1 of that year 9 months before birth to prevent 
                // very exact 9 months before dates
                maxdate = new DateTime(maxdate.Year, 1, 1);
            }
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.CENSUS));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.RESIDENCE));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.WITNESS));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.EMIGRATION));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.IMMIGRATION));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.ARRIVAL));
            maxdate = getMaxDate(maxdate, getMaxFactDate(indiv, Fact.DEPARTURE));
            // at this point we have the maximum point a person was alive
            // based on their oldest child and last census record and marriage date
            return maxdate;
        }

        private DateTime getMaxDate(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        private DateTime getMaxFactDate(Individual indiv, string factType)
        {
            DateTime maxdate = FactDate.MINDATE;
            List<Fact> facts = indiv.getFacts(factType);
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

        private DateTime getMinDeathDate(Individual indiv)
        {
            FactDate deathDate = indiv.DeathDate;
            DateTime now = DateTime.Now;
            FactDate.FactDateType deathDateType = deathDate.Type;
            FactDate.FactDateType birthDateType = indiv.BirthDate.Type;
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
            FactDate burialDate = indiv.getPreferredFactDate(Fact.BURIAL);
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

        public List<IDisplayTreeTops> GetTreeTops(Filter<Individual> filter)
        {
            List<IDisplayTreeTops> result = new List<IDisplayTreeTops>();
            foreach (Individual ind in individuals)
            {
                if (!ind.HasParents)
                {
                    if (filter.select(ind))
                        result.Add(ind);
                }
            }
            return result;
        }

        #endregion

        #region WarDead

        public List<IDisplayTreeTops> GetWarDead(Filter<Individual> filter)
        {
            List<IDisplayTreeTops> result = new List<IDisplayTreeTops>();
            foreach (Individual ind in individuals)
            {
                if (ind.isMale() && !ind.isDeathKnown() && filter.select(ind))
                    result.Add(ind);
            }
            return result;
        }

        #endregion

        #region Relationship Functions

        private ParentalGroup CreateFamilyGroup(Individual i)
        {
            List<Family> list = i.FamiliesAsChild;
            if (list.Count > 0)
            {
                Family f = list.First();
                return new ParentalGroup(i, f.Husband, f.Wife, f.getPreferredFact(Fact.MARRIAGE));
            }
            return new ParentalGroup(i, null, null, null);
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

        private void AddToQueue(Queue<Individual> queue, List<Individual> list)
        {
            foreach (Individual i in list)
            {
                queue.Enqueue(i);
            }
        }

        private void addParentsToQueue(Individual indiv, Queue<Individual> queue, bool setAhnenfatel)
        {
            List<Family> families = indiv.FamiliesAsChild;
            foreach (Family family in families)
            {
                // add parents to queue
                if (family.Husband != null && family.Husband.RelationType == Individual.UNKNOWN)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT)
                    {
                        family.Husband.Ahnentafel = indiv.Ahnentafel * 2;
                        if(family.Husband.Ahnentafel > maxAhnentafel)
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

        public void SetRelations(string startGed)
        {
            ClearRelations();
            SetFamilies();
            Individual ind = getGedcomIndividual(startGed);
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
                addParentsToQueue(ind, queue, true);
                Application.DoEvents();
            }
            int lenAhnentafel = maxAhnentafel.ToString().Length;
            // we have now added all direct ancestors
            List<Individual> directs = getAllRelationsOfType(Individual.DIRECT);
            foreach(Individual i in directs)
            {
                // add all direct ancestors budgie codes
                i.BudgieCode = (i.Ahnentafel).ToString().PadLeft(lenAhnentafel,'0') + "d";
            }
            AddToQueue(queue, directs);
            while (queue.Count > 0)
            {
                // get the next person
                ind = queue.Dequeue();
                List<Family> families = ind.FamiliesAsParent;
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
            // all that remains is to loop through the marriage relations
            List<Individual> marriedDBs = getAllRelationsOfType(Individual.MARRIEDTODB);
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
                    addParentsToQueue(ind, queue, false);
                    List<Family> families = ind.FamiliesAsParent;
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
            ind = getGedcomIndividual(startGed);
            ind.RelationType = Individual.ROOT;
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

        public void processRegistration(String filename, RegistrationsProcessor rp,
            List<Registration> sourceRegs, BaseOutputFormatter formatter)
        {
            TextWriter output = new StreamWriter(filename + ".csv");
            formatter.printHeader(output);
            List<Registration> regs = rp.processRegistrations(sourceRegs);
            foreach (Registration r in regs)
            {
                formatter.printItem(r, output);
            }
            FamilyTree.Instance.xmlErrorbox.AppendText("written " + regs.Count + " records to " + filename + "\n");
            output.Close();
        }

        public List<Registration> getAllBirthRegistrations()
        {
            List<Registration> result = new List<Registration>();
            foreach (Individual i in individuals)
            {
                ParentalGroup pg = CreateFamilyGroup(i);
                result.Add(new BirthRegistration(pg));
            }
            return result;
        }

        public List<Registration> getAllMarriageRegistrations()
        {
            List<Registration> result = new List<Registration>();
            foreach (Individual i in individuals)
            {
                if (!i.isSingleAtDeath())
                {
                    ParentalGroup pg = CreateFamilyGroup(i);
                    List<Family> indfam = i.isMale() ? FindFamiliesWhereHusband(i) : FindFamiliesWhereWife(i);
                    if (indfam.Count == 0)
                        result.Add(new MarriageRegistration(pg, null, null));
                    else if (i.isMale())
                    {
                        foreach (Family f in indfam)
                        {
                            ParentalGroup pg2 = CreateFamilyGroup(f.Wife);
                            result.Add(new MarriageRegistration(pg, pg2, f));
                        }
                    }
                }

            }
            return result;
        }

        public List<Registration> getAllDeathRegistrations()
        {
            List<Registration> result = new List<Registration>();
            foreach (Individual i in individuals)
            {
                if (i.DeathDate != FactDate.UNKNOWN_DATE)
                {
                    // only include dead individuals
                    ParentalGroup pg = CreateFamilyGroup(i);
                    List<Family> indfam = i.isMale() ? FindFamiliesWhereHusband(i) : FindFamiliesWhereWife(i);
                    if (indfam.Count == 0)
                        result.Add(new DeathRegistration(pg, null, Family.SINGLE));
                    else
                    {
                        foreach (Family f in indfam)
                        {
                            if (i.isMale())
                                result.Add(new DeathRegistration(pg, f.Wife, f.MaritalStatus));
                            else
                                result.Add(new DeathRegistration(pg, f.Husband, f.MaritalStatus));
                        }
                    }
                }
            }
            return result;
        }

        public List<Registration> getAllCensusRegistrations(FactDate censusDate, bool censusDone, bool includeResidence, bool lostCousinsCheck)
        {
            List<Registration> result = new List<Registration>();
            if (censusDate != null)
            {
                foreach (Family f in families)
                {
                    CensusFamily cf = new CensusFamily(f, censusDate);
                    if (cf.process(censusDate, censusDone, includeResidence, lostCousinsCheck))
                        result.Add(new CensusRegistration(null, censusDate, cf));
                }
            }
            return result;
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
                    result.Add(new DisplayOccupation(occ,occupations[occ].Count));
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
                    if (!i.BirthDate.isAfter(CensusDate.UKCENSUS1911) && !i.DeathDate.isBefore(CensusDate.UKCENSUS1841))
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
            foreach(Individual ind in AllIndividuals)
            {
                try
                {
                    if (ind.DeathDate != FactDate.UNKNOWN_DATE)
                    {
                        if (ind.BirthDate.isAfter(ind.DeathDate))
                            errors[0].Add(new DataError(ind, "Died " + ind.DeathDate + " before born"));
                        if (ind.BurialDate != null && ind.BurialDate.isBefore(ind.DeathDate) && !ind.BurialDate.overlaps(ind.DeathDate))
                            errors[7].Add(new DataError(ind, "Buried " + ind.BurialDate + " before died " + ind.DeathDate));
                        int minAge = ind.getMinAge(ind.DeathDate);
                        if(minAge > 110)
                            errors[8].Add(new DataError(ind, "Aged over 110 before died " + ind.DeathDate));
                    }
                    foreach (Fact f in ind.AllFacts)
                    {
                        if(f.FactType != Fact.BIRTH && f.FactDate.isBefore(ind.BirthDate))
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
                                FactDate conception = ind.BirthDate.subtractMonths(9);
                                if (father.DeathDate.isBefore(conception))
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
                            if (mother.DeathDate != FactDate.UNKNOWN_DATE && mother.DeathDate.isBefore(ind.BirthDate))
                                errors[3].Add(new DataError(ind, "Mother " + mother.Name + " died " + mother.DeathDate + " which is before individual was born"));
                        }
                    }
                    foreach (Family asParent in ind.FamiliesAsParent)
                    {
                        Individual spouse = asParent.Spouse(ind);
                        if (asParent.MarriageDate != null && spouse !=null)
                        {
                            if (ind.DeathDate != null && asParent.MarriageDate.isAfter(ind.DeathDate))
                                errors[10].Add(new DataError(ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after individual died " + ind.DeathDate));
                            if (spouse.DeathDate != null && asParent.MarriageDate.isAfter(spouse.DeathDate))
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
            dataErrorTypes.Add(new DataErrorGroup("Birth after death", errors[0]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after father aged 90+", errors[1]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after mother aged 60+", errors[2]));
            dataErrorTypes.Add(new DataErrorGroup("Birth after mothers death", errors[3]));
            dataErrorTypes.Add(new DataErrorGroup("Birth more than 9m after fathers death", errors[4]));
            dataErrorTypes.Add(new DataErrorGroup("Birth before father aged 13", errors[5]));
            dataErrorTypes.Add(new DataErrorGroup("Birth before mother aged 13", errors[6]));
            dataErrorTypes.Add(new DataErrorGroup("Burial before death", errors[7]));
            dataErrorTypes.Add(new DataErrorGroup("Aged more than 120 at death" , errors[8]));
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
            foreach(DataErrorGroup dataError in dataErrorTypes)
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
                DataErrorGroup item = (DataErrorGroup) list.Items[indexChecked];
                errors.AddRange(item.Errors);
            }
            return errors;
        }
        #endregion

    }
}
