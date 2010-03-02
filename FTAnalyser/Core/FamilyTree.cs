using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    class FamilyTree
    {
        private static FamilyTree instance;

        private List<FactSource> sources;
        private List<Individual> individuals;
        private List<Family> families;

        private FamilyTree()
        {
            sources = new List<FactSource>();
            individuals = new List<Individual>();
            families = new List<Family>();
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

        public static string GetText(XmlNode node) {
            return (node != null)? node.InnerText.Trim() : "";
        }

        public static string GetText(XmlNode node, string tag) {
            return GetText(GetChildNode(node, tag));
        }

        public static XmlNode GetChildNode(XmlNode node, String tag)
        {
            if (node == null)
            {
                return null;
            }
            else
            {
                return node.SelectSingleNode(tag);
            }
        }

        public void LoadTree(XmlDocument doc)
        {
            // First iterate through attributes of root finding all sources
            XmlNodeList list = doc.SelectNodes("GED/SOUR");
            foreach(XmlNode n in list)
            {
                FactSource fs = new FactSource(n);
                addSource(fs);
            }
            // now iterate through child elements of root
            // finding all individuals
            list = doc.SelectNodes("GED/INDI");
            foreach(XmlNode n in list)
            {
                Individual individual = new Individual(n);
                addIndividual(individual);
            }
            // now iterate through child elements of root
            // finding all families
            list = doc.SelectNodes("GED/FAM");
            foreach(XmlNode n in list)
            {
                Family family = new Family(n);
                addFamily(family);
            } 
            setRelations("I1");
		    printRelationCount();
	        setParishes();
        }

        private void setRelations(string individual)
        {
        }

        private void printRelationCount()
        {
        }

        private void setParishes()
        {
        }

        public void addSource(FactSource source)
        {
            sources.Add(source);
        }

        public void addIndividual(Individual individual)
        {
            individuals.Add(individual);
        }

        public void addFamily(Family family)
        {
            families.Add(family);
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
            foreach (Family f in families) {
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
                if (husband != null && husband.Equals(ind))
                {
                    result.Add(f);
                }
                else if (wife != null && wife.Equals(ind))
                {
                    result.Add(f);
                }
            }
            return result;
        }
    }

}
