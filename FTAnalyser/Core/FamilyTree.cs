using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FTAnalyser
{
    class FamilyTree
    {
        private static FamilyTree instance;

        private List<FactSource> sources;
        private List<Individual> individuals;
        private List<Family> families;
        private List<Location> locations;

        private FamilyTree()
        {
            sources = new List<FactSource>();
            individuals = new List<Individual>();
            families = new List<Family>();
            locations = new List<Location>();
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
            return (node != null)? node.InnerText.Trim() : "";
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

        public int LoadTree(XmlDocument doc) { return LoadTree(doc, new ProgressBar(), new ProgressBar(), new ProgressBar()); }
        public int LoadTree(XmlDocument doc, ProgressBar pbS, ProgressBar pbI, ProgressBar pbF)
        {
            // First iterate through attributes of root finding all sources
            XmlNodeList list = doc.SelectNodes("GED/SOUR");
            pbS.Maximum = list.Count;
            int counter = 0;
            foreach(XmlNode n in list)
            {
                FactSource fs = new FactSource(n);
                sources.Add(fs);
                pbS.Value = counter++;
            }
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
            }
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
            } 
            // SetRelations(individuals[0].GedcomID); // needs testing
	        setParishes();
            return RelationCount;
        }

        #region Properties and Property Functions

        private int RelationCount
        {
            get { return 0; }
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

        public List<Individual> getAllRelationsOfType(int relationType)
        {
            List<Individual> result = new List<Individual>();
            foreach (Individual ind in individuals)
                if (ind.RelationType == relationType)
                    result.Add(ind);
            return result;
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

        public Location GetLocation(string place)
        {
            Location loc = new Location(place);
            if (!locations.Contains(loc))
                locations.Add(loc);
            return loc; // should return object that is in list of locations 
        }

        #endregion

        #region Processes

        private void ClearRelations()
        {
            foreach(Individual i in individuals)
            {
                i.RelationType = Individual.UNKNOWN;
            }
        }

        private void addParentsToQueue(Individual indiv, LinkedList<Individual> queue)
        {
            List<Family> families = getFamiliesAsChild(indiv);
            foreach (Family family in families)
            {
                // add parents to queue
                if (family.Husband != null)
                    queue.AddLast(family.Husband);
                if (family.Wife != null)
                    queue.AddLast(family.Wife);
            }
        }

        private void SetRelations(string startGed)
        {	
            ClearRelations();
            Individual ind = getGedcomIndividual(startGed);
            LinkedList<Individual> queue = new LinkedList<Individual>();
            queue.AddFirst(ind);
            while (queue.Count > 0) {
                // now take an item from the queue
                ind = queue.First();
                queue.RemoveFirst();
                // set them as a direct relation
                ind.RelationType = Individual.DIRECT;
                addParentsToQueue(ind, queue);
            }
            // we have now added all direct ancestors
            List<Individual> directs = getAllRelationsOfType(Individual.DIRECT);
		    queue.Union(directs);
		    while(queue.Count > 0) {
			    // get the next person
		        ind = queue.First();
                queue.RemoveFirst();
			    List<Family> families = getFamiliesAsParent(ind);
    		    foreach (Family family in families) {
    	            // if the spouse of a direct ancestor is not a direct
    	            // ancestor then they are only related by marriage
    		        family.setSpouseRelation(ind, Individual.MARRIAGEDB);
		            // all children of direct ancestors and blood relations
		            // are blood relations
    		        family.setChildRelation(queue, Individual.BLOOD);
    		    }
		    }
		    // we have now set all direct ancestors and all blood relations
		    // all that remains is to loop through the marriage relations
            List<Individual> marrieds = getAllRelationsOfType(Individual.MARRIAGE);
            List<Individual> marriedDBs = getAllRelationsOfType(Individual.MARRIAGEDB);
		    queue.Union(marriedDBs);
		    queue.Union(marrieds);
            while (queue.Count > 0)
            {
			    // get the next person
                ind = queue.First();
                queue.RemoveFirst();
                // first only process this individual if they are related by marriage or still unknown
		        int relationship = ind.RelationType;
		        if (relationship == Individual.MARRIAGE || 
		            relationship == Individual.MARRIAGEDB ||
		            relationship == Individual.UNKNOWN) {
		            // set this individual to be related by marriage
		            if (relationship == Individual.UNKNOWN)
		                ind.RelationType = Individual.MARRIAGE;
		            addParentsToQueue(ind, queue);
                    List<Family> families = getFamiliesAsParent(ind);
                    foreach (Family family in families)
                    {
                        family.setSpouseRelation(ind, Individual.MARRIAGE);
	    	            // children of relatives by marriage that we haven't previously 
	    	            // identified are also relatives by marriage
	    		        family.setChildRelation(queue, Individual.MARRIAGE);
	    		    }
		        }
		    }
        }

        private void setParishes()
        {
            foreach (Location loc in locations)
            {

            }
        }

        #endregion
    }
}
