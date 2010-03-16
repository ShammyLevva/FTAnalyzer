using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace FTAnalyzer
{
    class FamilyTree
    {
        private static FamilyTree instance;

        private List<FactSource> sources;
        private List<Individual> individuals;
        private List<Family> families;
        private Dictionary<string, FactLocation> locations;
        private bool _loading = false;
        private RichTextBox xmlErrorbox = new RichTextBox();

        private FamilyTree()
        {
            ResetData();
        }

        public static FamilyTree Instance {
            get {
                if (instance == null)
                {
                    instance = new FamilyTree();
                }
                return instance;
            }
        }

        #region Static Functions

        public static string GetText(XmlNode node) {
            if (node == null || node.FirstChild == null || node.FirstChild.Value == null)
            {
                return "";
            }
            return node.FirstChild.Value.Trim();
        }

        public static string GetText(XmlNode node, string tag) {
            return GetText(GetChildNode(node, tag));
        }

        public static XmlNode GetChildNode(XmlNode node, String tag)
        {
            if (node == null)
                return null;
            else
                return node.SelectSingleNode(tag);
        }

        #endregion

        #region Load Gedcom XML
        private void ResetData()
        {
            sources = new List<FactSource>();
            individuals = new List<Individual>();
            families = new List<Family>();
            locations = new Dictionary<string, FactLocation>();
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
            foreach(XmlNode n in list)
            {
                FactSource fs = new FactSource(n);
                sources.Add(fs);
                pbS.Value = counter++;
                Application.DoEvents(); // allows windows to process events and prevents application from appearing to have crashed.
            }
            xmlErrorbox.AppendText("Loaded " + counter + " sources.\n");
            // now iterate through child elements of root
            // finding all individuals
            list = doc.SelectNodes("GED/INDI");
            pbI.Maximum = list.Count;
            counter = 0;
            foreach (XmlNode n in list)
            {
                Individual individual = new Individual(n);
                individuals.Add(individual);
                pbI.Value = counter++;
                Application.DoEvents();
            }
            xmlErrorbox.AppendText("Loaded " + counter + " individuals.\n");
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
            xmlErrorbox.AppendText("Loaded " + counter + " families.\nCalculating Relationships... Please wait\n\n");
            SetRelations(individuals[0].GedcomID);
            PrintRelationCount();
	        SetParishes();
            _loading = false;
        }
        #endregion

        #region Properties

        public bool Loading { get { return _loading; } }

        public RichTextBox XmlErrorBox { 
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
            get {
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
                        FactLocation a = new FactLocation(loc.address + ", " + loc.parish + ", " + loc.region + ", " + loc.country);
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
                    if (f != null && !f.isCertificatePresent())
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

        public List<Family> getFamiliesAsParent(Individual ind)
        {
            List<Family> result = new List<Family>();
            foreach (Family f in families)
            {
                Individual husband = f.Husband;
                Individual wife = f.Wife;
                if (husband != null && husband == ind)
                    result.Add(f);
                else if (wife != null && wife == ind)
                    result.Add(f);
            }
            return result;
        }

        public List<Family> getFamiliesAsChild(Individual ind)
        {
            List<Family> result = new List<Family>();
            foreach (Family f in families)
            {
                foreach (Individual child in f.Children)
                {
                    if (child != null && child == ind)
                    {
                        result.Add(f);
                        break;
                    }
                }
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
            List<Family> families = getFamiliesAsParent(ind);
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

        #endregion

        #region Loose Deaths

        public List<IDisplayLooseDeath> GetLooseDeaths()
        {
            List<IDisplayLooseDeath> result = new List<IDisplayLooseDeath>();
            foreach (Individual ind in individuals)
            {
                checkLooseDeath(ind ,result);
            }
            return result;
        }

        private void checkLooseDeath(Individual indiv, List<IDisplayLooseDeath> result) 
		{
		    FactDate deathDate = indiv.DeathDate;
		    FactDate toAdd = null;
            if (deathDate != null && !deathDate.isExact())
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
            } else if (deathDate == null && indiv.CurrentAge.MinAge > 110) {
		        // also check for empty death dates for people aged over 110
                DateTime maxLiving = getMaxLivingDate(indiv);
                DateTime minDeath = getMinDeathDate(indiv);
                if (minDeath != FactDate.MAXDATE)
                    toAdd = new FactDate(maxLiving, minDeath);
		    }
	        if (toAdd != null && toAdd != deathDate) {
	            // we have a date to change and its not the same 
	            // range as the existing death date
		        Fact looseDeath = new Fact(Fact.LOOSEDEATH, toAdd);
		        indiv.addFact(looseDeath);
		        result.Add(indiv);
	        }
	    }

      	private DateTime getMaxLivingDate(Individual indiv) {
            List<Family> indfam = new List<Family>();
		    if (indiv.isMale()) {
                indfam = FindFamiliesWhereHusband(indiv);
	        } else {
                indfam = FindFamiliesWhereWife(indiv);
	        }
		    // having got the families the individual is a parent of
	        // get the max startdate of the birthdate of the youngest child
	        // this then is the minimum point they were alive
	        // subtract 9 months for a male
	        DateTime maxdate = FactDate.MINDATE;
	        bool childDate = false;
            foreach (Family fam in indfam)
            {
		        FactDate marriageDate = fam.getPreferredFactDate(Fact.MARRIAGE);
		        if (marriageDate.StartDate > maxdate && ! marriageDate.isLongYearSpan()) {
		            maxdate = marriageDate.StartDate;
		        }
		        foreach(Individual child in fam.Children) 
                {
			        FactDate birthday = child.BirthDate;
			        if (birthday.StartDate > maxdate) { 
			            maxdate = birthday.StartDate;
			            childDate = true;
			        }
			    }
		    }
		    if (childDate && indiv.isMale() && maxdate > FactDate.MINDATE) {
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
                DateTime d = f.FactDate.StartDate;
                if (d > maxdate)
                {
                    maxdate = d;
                }
            }
            return maxdate;
        }

        private DateTime getMinDeathDate(Individual indiv)
        {
            FactDate deathDate = indiv.DeathDate == null ? new FactDate("UNKNOWN") : indiv.DeathDate;
            DateTime now = DateTime.Now;
            FactDate.FactDateType deathDateType = deathDate == null ? FactDate.FactDateType.UNK : deathDate.Type;
            FactDate.FactDateType birthDateType = indiv.BirthDate == null ? FactDate.FactDateType.UNK : indiv.BirthDate.Type;
            DateTime minDeath = indiv.BirthDate == null ? FactDate.MAXDATE : indiv.BirthDate.EndDate;
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

        #region Relationship Functions

        private ParentalGroup CreateFamilyGroup(Individual i)
        {
            List<Family> list = getFamiliesAsChild(i);
            if (list.Count > 0)
            {
                Family f = list.First();
                return new ParentalGroup(i, f.Husband, f.Wife, f.getPreferredFact(Fact.MARRIAGE));
            }
            return new ParentalGroup(i, null, null, null);
        }

        private void ClearRelations()
        {
            foreach(Individual i in individuals)
            {
                i.RelationType = Individual.UNKNOWN;
            }
        }

        private void AddToQueue(Queue<Individual> queue, List<Individual> list)
        {
            foreach (Individual i in list)
                queue.Enqueue(i);
        }

        private void addParentsToQueue(Individual indiv, Queue<Individual> queue, bool setAhnenfatel)
        {
            List<Family> families = getFamiliesAsChild(indiv);
            foreach (Family family in families)
            {
                // add parents to queue
                if (family.Husband != null)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT)
                        family.Husband.Ahnentafel = indiv.Ahnentafel * 2;
                    queue.Enqueue(family.Husband);
                }
                if (family.Wife != null)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT)
                        family.Wife.Ahnentafel = indiv.Ahnentafel * 2 + 1;
                    queue.Enqueue(family.Wife);
                }
            }
        }

        private void SetRelations(string startGed)
        {	
            ClearRelations();
            Individual ind = getGedcomIndividual(startGed);
            ind.Ahnentafel = 1;
            Queue<Individual> queue = new Queue<Individual>();
            queue.Enqueue(ind);
            while (queue.Count > 0) {
                // now take an item from the queue
                ind = queue.Dequeue();
                // set them as a direct relation
                ind.RelationType = Individual.DIRECT;
                addParentsToQueue(ind, queue, true);
            }
            // we have now added all direct ancestors
            List<Individual> directs = getAllRelationsOfType(Individual.DIRECT);
            AddToQueue(queue, directs);
		    while(queue.Count > 0) {
			    // get the next person
		        ind = queue.Dequeue();
                List<Family> families = getFamiliesAsParent(ind);
    		    foreach (Family family in families) {
    	            // if the spouse of a direct ancestor is not a direct
    	            // ancestor then they are only related by marriage
    		        family.setSpouseRelation(ind, Individual.MARRIAGEDB);
		            // all children of direct ancestors and blood relations
		            // are blood relations
    		        family.setChildRelation(queue, Individual.BLOOD);
    		    }
                Application.DoEvents();
            }
		    // we have now set all direct ancestors and all blood relations
		    // all that remains is to loop through the marriage relations
            List<Individual> marriedDBs = getAllRelationsOfType(Individual.MARRIAGEDB);
            AddToQueue(queue, marriedDBs);
		    while (queue.Count > 0)
            {
			    // get the next person
                ind = queue.Dequeue();
                // first only process this individual if they are related by marriage or still unknown
		        int relationship = ind.RelationType;
		        if (relationship == Individual.MARRIAGE || 
		            relationship == Individual.MARRIAGEDB ||
		            relationship == Individual.UNKNOWN) {
		            // set this individual to be related by marriage
		            if (relationship == Individual.UNKNOWN)
		                ind.RelationType = Individual.MARRIAGE;
		            addParentsToQueue(ind, queue, false);
                    List<Family> families = getFamiliesAsParent(ind);
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

        private void PrintRelationCount()
        {
            int[] relations = {0,0,0,0,0,0};
            foreach(Individual i in individuals)
                relations[i.RelationType]++;
            xmlErrorbox.AppendText("Direct Ancestors : " + relations[Individual.DIRECT] + "\n");
            xmlErrorbox.AppendText("Blood Relations : " + relations[Individual.BLOOD] + "\n");
            xmlErrorbox.AppendText("Married to Blood or Direct Relation : " + relations[Individual.MARRIAGEDB] + "\n");
            xmlErrorbox.AppendText("Related by Marriage : " + relations[Individual.MARRIAGE] + "\n");
            xmlErrorbox.AppendText("Unknown relation : " + relations[Individual.UNKNOWN] + "\n");
            if(relations[Individual.UNSET] > 0)
                xmlErrorbox.AppendText("Failed to set relationship : " + relations[Individual.UNSET] + "\n");
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
            foreach (Registration r in regs) {
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
                if (i.DeathDate != null)
                {
                    // only include dead individuals
                    ParentalGroup pg = CreateFamilyGroup(i);
                    List<Family> indfam = i.isMale() ? FindFamiliesWhereHusband(i) : FindFamiliesWhereWife(i);
                    if (indfam.Count == 0)
                        result.Add(new DeathRegistration(pg, null, Family.SINGLE));
                    else
                    {
                        foreach(Family f in indfam)
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

        public List<Registration> getAllCensusRegistrations(FactDate censusDate, bool censusDone)
        {
            List<Registration> result = new List<Registration>();
            if (censusDate != null)
            {
                foreach (Family f in families)
                {
                    CensusFamily cf = new CensusFamily(f, censusDate);
                    if (cf.process(censusDate, censusDone))
                        result.Add(new CensusRegistration(null, censusDate, cf));
                }
            }
            return result;
        }

        #endregion

        #region Displays

        public List<IDisplayIndividual> AllDisplayIndividuals
        {
            get
            {
                List<IDisplayIndividual> result = new List<IDisplayIndividual>();
                foreach (IDisplayIndividual i in individuals)
                    result.Add(i);
                return result;
            }
        }

        #endregion
    }
}
