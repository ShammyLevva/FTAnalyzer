using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using FTAnalyzer.Filters;
using FTAnalyzer.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using Ionic.Zip;

namespace FTAnalyzer
{
    class FamilyTree : IDisposable
    {
        private static FamilyTree instance;

        private IList<FactSource> sources;
        private IList<Individual> individuals;
        private IList<Family> families;
        private IDictionary<string, List<Individual>> occupations;
        private ISet<string> unknownFactTypes;
        private bool _loading = false;
        private bool _dataloaded = false;
        private RichTextBox xmlErrorbox = new RichTextBox();
        private int maxAhnentafel = 0;
        private IList<DataErrorGroup> dataErrorTypes;
        private SortableBindingList<IDisplayLocation>[] displayLocations;
        private SortableBindingList<IDisplayLooseDeath> looseDeaths;
        private SortableBindingList<IDisplayLooseBirth> looseBirths;
        private TreeNode mainformTreeRootNode;
        private TreeNode placesTreeRootNode;
        private static int DATA_ERROR_GROUPS = 21;

        public bool Geocoding { get; set; }

        #region Static Functions

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

        public static string GetText(XmlNode node)
        {
            if (node == null || node.FirstChild == null || node.FirstChild.Value == null)
            {
                return "";
            }
            if (node.Name.Equals("PAGE") || node.Name.Equals("TITL"))
                return node.InnerText.Trim();
            else
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
            occupations = new Dictionary<string, List<Individual>>();
            dataErrorTypes = new List<DataErrorGroup>();
            displayLocations = new SortableBindingList<IDisplayLocation>[5];
            unknownFactTypes = new HashSet<string>();
            ClearLocations();
            mainformTreeRootNode = null;
            ResetLooseFacts();
            FactLocation.ResetLocations();
        }

        public void ResetLooseFacts()
        {
            looseBirths = null;
            looseDeaths = null;
        }

        public void CheckUnknownFactTypes(string factType)
        {
            if (!unknownFactTypes.Contains(factType))
                unknownFactTypes.Add(factType);
        }

        public bool LoadTree(string filename, ProgressBar pbS, ProgressBar pbI, ProgressBar pbF)
        {
            _loading = true;
            ResetData();
            string rootIndividualID = string.Empty;
            Application.DoEvents();
            XmlDocument doc = GedcomToXml.Load(filename);
            if (doc == null)
                return false;
            xmlErrorbox.AppendText("Loading file " + filename + "\n");
            // First check if file has a valid header record ie: it is actually a GEDCOM file
            XmlNode header = doc.SelectSingleNode("GED/HEAD");
            if (header == null)
            {
                xmlErrorbox.AppendText("\n\nUnable to find GEDCOM 'HEAD' record in first line of file aborting load.\nIs " + filename + " really a GEDCOM file");
                return false;
            }
            XmlNode charset = doc.SelectSingleNode("GED/HEAD/CHAR");
            if (charset != null && charset.InnerText.Equals("UTF-8"))
                doc = GedcomToXml.Load(filename, Encoding.UTF8);
            if (doc == null)
                return false;
            ReportOptions();
            XmlNode root = doc.SelectSingleNode("GED/HEAD/_ROOT");
            if (root != null)
            {
                // file has a root individual
                try
                {
                    rootIndividualID = root.Attributes["REF"].Value;
                }
                catch (Exception)
                { } // don't crash if can't set root individual
            }
            // First iterate through attributes of root finding all sources
            XmlNodeList list = doc.SelectNodes("GED/SOUR");
            pbS.Maximum = list.Count == 0 ? 1 : list.Count;
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
            pbF.Maximum = list.Count == 0 ? 1 : list.Count;
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
            RemoveFamiliesWithNoIndividuals();
            if (rootIndividualID == string.Empty)
                rootIndividualID = individuals[0].IndividualID;
            UpdateRootIndividual(rootIndividualID);
            CountCensusFacts();
            FixIDs();
            SetDataErrorTypes();
            CountUnknownFactTypes();
            FactLocation.LoadGoogleFixesXMLFile(xmlErrorbox);
            LoadGeoLocationsFromDataBase();
            _loading = false;
            _dataloaded = true;
            return true;
        }

        public void UpdateRootIndividual(string rootIndividualID)
        {
            int start = xmlErrorbox.TextLength;
            xmlErrorbox.AppendText("\nCalculating Relationships using " + rootIndividualID + ": " +
                GetIndividual(rootIndividualID).Name + " as root person. Please wait\n\n");
            int end = xmlErrorbox.TextLength;
            xmlErrorbox.SelectionStart = start;
            xmlErrorbox.SelectionLength = end - start;
            xmlErrorbox.SelectionFont = new Font(xmlErrorbox.Font, FontStyle.Bold);
            xmlErrorbox.SelectionLength = 0;
            SetRelations(rootIndividualID);
            SetRelationDescriptions(rootIndividualID);
            xmlErrorbox.AppendText(PrintRelationCount());
        }

        private void ReportOptions()
        {
            if (Properties.GeneralSettings.Default.ReportOptions)
            {
                xmlErrorbox.AppendText("\nThe current general options are set :");
                xmlErrorbox.AppendText("\n    Use Baptism/Christening date if no birth date: " + Properties.GeneralSettings.Default.UseBaptismDates);
                xmlErrorbox.AppendText("\n    Allow Empty values in Locations: " + Properties.GeneralSettings.Default.AllowEmptyLocations);
                xmlErrorbox.AppendText("\n    Treat Residence facts as Census facts: " + Properties.GeneralSettings.Default.UseResidenceAsCensus);
                xmlErrorbox.AppendText("\n    Tolerate slightly inaccurate census dates: " + Properties.GeneralSettings.Default.TolerateInaccurateCensusDate);
                xmlErrorbox.AppendText("\n    Include Locations with partial match status: " + Properties.GeneralSettings.Default.IncludePartials);
                xmlErrorbox.AppendText("\n    Family Census Facts apply to only parents: " + Properties.GeneralSettings.Default.OnlyCensusParents);
                xmlErrorbox.AppendText("\n    Loose Birth minimum parental age: " + Properties.GeneralSettings.Default.MinParentalAge);
                xmlErrorbox.AppendText("\nThe current mapping options are set :");
                xmlErrorbox.AppendText("\n    Custom Maps Location: " + Properties.MappingSettings.Default.CustomMapPath);
                xmlErrorbox.AppendText("\n    Display English & Welsh Parish Boundaries: " + Properties.MappingSettings.Default.UseEnglishParishBoundaries);
                xmlErrorbox.AppendText("\n    Hide Scale Bar: " + Properties.MappingSettings.Default.HideScaleBar);
                xmlErrorbox.AppendText("\n\n");
            }
        }

        private void RemoveFamiliesWithNoIndividuals()
        {
            (families as List<Family>).RemoveAll(x => x.FamilySize == 0);
        }

        private void CountUnknownFactTypes()
        {
            if (unknownFactTypes.Count > 0)
            {
                foreach (string tag in unknownFactTypes)
                {
                    int count = AllExportFacts.Count(f => f.FactType == tag);
                    xmlErrorbox.AppendText("\nFound " + count + " facts of unknown fact type " + tag);
                }
                xmlErrorbox.AppendText("\n");
            }
        }

        private void CountCensusFacts()
        {
            int censusFacts = 0;
            int resiFacts = 0;
            int lostCousinsFacts = 0;
            int censusWarnAllow = 0;
            int resiCensus = 0;
            int resiWarnAllow = 0;
            int lostCousinsWarnAllow = 0;
            int censusWarnIgnore = 0;
            int lostCousinsWarnIgnore = 0;
            int censusErrors = 0;
            int lostCousinsErrors = 0;
            foreach (Individual ind in individuals)
            {
                censusFacts += ind.FactCount(Fact.CENSUS);
                censusWarnAllow += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.WARNINGALLOW);
                censusWarnIgnore += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.WARNINGIGNORE);
                censusErrors += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.ERROR);
                resiFacts += ind.FactCount(Fact.RESIDENCE);
                resiWarnAllow += ind.ErrorFactCount(Fact.RESIDENCE, Fact.FactError.WARNINGALLOW);
                resiCensus += ind.ResidenceCensusFactCount;
                lostCousinsFacts += ind.FactCount(Fact.LOSTCOUSINS);
                lostCousinsWarnAllow += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.WARNINGALLOW);
                lostCousinsWarnIgnore += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.WARNINGIGNORE);
                lostCousinsErrors += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.ERROR);
            }
            int censusTotal = censusFacts + censusWarnAllow + censusWarnIgnore + censusErrors;
            int resiTotal = resiFacts + resiWarnAllow;
            int lostCousinsTotal = lostCousinsFacts + lostCousinsWarnAllow + lostCousinsWarnIgnore + lostCousinsErrors;

            xmlErrorbox.AppendText("\nFound " + censusTotal + " census facts in GEDCOM File (" + censusFacts + " good, ");
            if (censusWarnAllow > 0)
                xmlErrorbox.AppendText(censusWarnAllow + " warnings (data tolerated), ");
            if (censusWarnIgnore > 0)
                xmlErrorbox.AppendText(censusWarnIgnore + " warnings (data ignored in strict mode), ");
            if (censusErrors > 0)
                xmlErrorbox.AppendText(censusErrors + " errors (data discarded), ");
            xmlErrorbox.AppendText((censusFacts + censusWarnAllow) + " usable facts loaded)");

            xmlErrorbox.AppendText("\nFound " + resiTotal + " residence facts in GEDCOM File (" + resiCensus + " treated as Census facts) ");
            if (resiWarnAllow > 0)
            {
                if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate)
                    xmlErrorbox.AppendText(resiWarnAllow + " warnings (data tolerated), ");
                else
                    xmlErrorbox.AppendText(resiWarnAllow + " warnings (data ignored in strict mode), ");
            }

            xmlErrorbox.AppendText("\nFound " + lostCousinsTotal + " Lost Cousins facts in GEDCOM File (" + lostCousinsFacts + " good, ");
            if (lostCousinsWarnAllow > 0)
                xmlErrorbox.AppendText(lostCousinsWarnAllow + " warnings (data tolerated), ");
            if (lostCousinsWarnIgnore > 0)
                xmlErrorbox.AppendText(lostCousinsWarnIgnore + " warnings (data ignored in strict mode), ");
            if (lostCousinsErrors > 0)
                xmlErrorbox.AppendText(lostCousinsErrors + " errors (data discarded), ");
            xmlErrorbox.AppendText((lostCousinsFacts + lostCousinsWarnAllow) + " usable facts loaded)\n");
            if (censusFacts == 0 && resiCensus == 0 && censusWarnAllow == 0)
            {
                xmlErrorbox.AppendText("\nFound no census or suitable residence facts in GEDCOM File.\n");
                xmlErrorbox.AppendText("This is probably because you have recorded census facts as notes\n");
                xmlErrorbox.AppendText("This will mean that the census report will show everyone as not yet found on census\n");
                xmlErrorbox.AppendText("and the Lost Cousins report will report no-one with a census needing to be entered\n");
                xmlErrorbox.AppendText("onto your Lost Cousins My Ancestors page.\n");
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
                if (f.Husband != null)
                    f.Husband.Infamily = true;
                if (f.Wife != null)
                    f.Wife.Infamily = true;
                foreach (Individual c in f.Children)
                {
                    c.Infamily = true;
                    if (f.Husband != null || f.Wife != null)
                        c.HasParents = true;
                }
            }
            int added = 0;
            foreach (Individual ind in individuals)
            {
                if (!ind.IsInFamily)
                {
                    families.Add(new Family(ind, Family.SOLOINDIVIDUAL));
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

        public List<MapLocation> AllMapLocations
        {
            get
            {
                List<MapLocation> result = new List<MapLocation>();
                foreach (Individual ind in individuals)
                {
                    foreach (Fact f in ind.AllFacts)
                        if (f.Location.IsGeoCoded(false))
                            result.Add(new MapLocation(ind, f, f.FactDate));
                }
                return result;
            }
        }

        public List<MapLocation> YearMapLocations(FactDate when, int limit)
        {
            List<MapLocation> result = new List<MapLocation>();
            foreach (Individual ind in individuals)
            {
                if (ind.IsAlive(when) && ind.GetMaxAge(when) < FactDate.MAXYEARS)
                {
                    Fact fact = ind.BestLocationFact(when, limit);
                    FactLocation loc = fact.Location;
                    if (loc.IsGeoCoded(false))
                        result.Add(new MapLocation(ind, fact, when));
                    else
                    {
                        int startlevel = loc.Level - 1;
                        for (int level = startlevel; level > FactLocation.UNKNOWN; level--)
                        {
                            loc = loc.GetLocation(level);
                            if (loc.IsGeoCoded(false))
                            {
                                result.Add(new MapLocation(ind, fact, loc, when));
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<ExportFacts> AllExportFacts
        {
            get
            {
                List<ExportFacts> result = new List<ExportFacts>();
                foreach (Individual ind in individuals)
                {
                    foreach (Fact f in ind.PersonalFacts)
                        result.Add(new ExportFacts(ind, f));
                    foreach (Family fam in ind.FamiliesAsParent)
                        foreach (Fact famfact in fam.Facts)
                            result.Add(new ExportFacts(ind, famfact));
                }
                return result;
            }
        }

        public IEnumerable<Family> AllFamilies
        {
            get { return families; }
        }

        public IEnumerable<Individual> AllIndividuals
        {
            get { return individuals; }
        }

        public int IndividualCount { get { return individuals.Count; } }

        #endregion

        #region Property Functions

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

        public FactSource GetSourceID(string sourceID)
        {
            return sources.FirstOrDefault(s => s.SourceID == sourceID);
        }

        public Individual GetIndividual(string individualID)
        {
            return individuals.FirstOrDefault(i => i.IndividualID == individualID);
        }

        public Family GetFamily(string familyID)
        {
            return families.FirstOrDefault(f => f.FamilyID == familyID);
        }

        public IEnumerable<Individual> GetIndividualsAtLocation(FactLocation loc, int level)
        {
            return individuals.Where(i => i.IsAtLocation(loc, level));
        }

        public IEnumerable<Family> GetFamiliesAtLocation(FactLocation loc, int level)
        {
            return families.Where(f => f.IsAtLocation(loc, level));
        }

        public List<string> GetSurnamesAtLocation(FactLocation loc) { return GetSurnamesAtLocation(loc, FactLocation.SUBREGION); }
        public List<string> GetSurnamesAtLocation(FactLocation loc, int level)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (Individual i in individuals)
            {
                if (!result.Contains(i.Surname) && i.IsAtLocation(loc, level))
                    result.Add(i.Surname);
            }
            List<string> ls = result.ToList();
            ls.Sort();
            return ls;
        }

        private void FixIDs()
        {
            int indLen = individuals.Count.ToString().Length;
            foreach (Individual ind in individuals)
                ind.FixIndividualID(indLen);
            int famLen = families.Count.ToString().Length;
            foreach (Family f in families)
                f.FixFamilyID(famLen);
        }

        #endregion

        #region Loose Births

        public SortableBindingList<IDisplayLooseBirth> LooseBirths
        {
            get
            {
                if (looseBirths != null)
                    return looseBirths;
                SortableBindingList<IDisplayLooseBirth> result = new SortableBindingList<IDisplayLooseBirth>();
                foreach (Individual ind in individuals)
                {
                    CheckLooseBirth(ind, result);
                }
                looseBirths = result;
                return result;
            }
        }

        private void CheckLooseBirth(Individual indiv, SortableBindingList<IDisplayLooseBirth> result)
        {
            FactDate birthDate = indiv.BirthDate;
            FactDate toAdd = null;
            if (birthDate.EndDate.Year - birthDate.StartDate.Year > 1)
            {
                FactDate baseDate = BaseLivingDate(indiv);
                DateTime minStart = baseDate.StartDate;
                DateTime minEnd = baseDate.EndDate;
                foreach (Family fam in indiv.FamiliesAsParent)
                {
                    FactDate marriageDate = fam.GetPreferredFactDate(Fact.MARRIAGE);
                    if (marriageDate.StartDate.Year > Properties.GeneralSettings.Default.MinParentalAge && !marriageDate.IsLongYearSpan)
                    {  // set maximum birthdate as X years before earliest marriage
                        DateTime preMarriage = new DateTime(marriageDate.StartDate.Year - Properties.GeneralSettings.Default.MinParentalAge, 12, 31);
                        if (preMarriage < minEnd && preMarriage >= minStart)
                            minEnd = preMarriage;
                    }
                    if (fam.Children.Count > 0)
                    {   // must be at least X years old at birth of child
                        List<Individual> childrenNoAFT = fam.Children.Where(child => child.BirthDate.EndDate != FactDate.MAXDATE).ToList();
                        if (childrenNoAFT.Count > 0)
                        {
                            int minChildYear = childrenNoAFT.Min(child => child.BirthDate.EndDate).Year;
                            DateTime minChild = new DateTime(minChildYear - Properties.GeneralSettings.Default.MinParentalAge, 12, 31);
                            if (minChild < minEnd && minChild >= minStart)
                                minEnd = minChild;
                        }
                        List<Individual> childrenNoBEF = fam.Children.Where(child => child.BirthDate.StartDate != FactDate.MINDATE).ToList();
                        if (childrenNoBEF.Count > 0)
                        {
                            int maxChildYear = childrenNoBEF.Max(child => child.BirthDate.StartDate).Year;
                            DateTime maxChild;
                            if (indiv.IsMale) // for males check that not over MAXYEARS when oldest child is born
                                maxChild = new DateTime(maxChildYear - FactDate.MAXYEARS, 1, 1);
                            else // for females check that not over 60 when oldest child is born
                                maxChild = new DateTime(maxChildYear - 60, 1, 1);
                            if (maxChild > minStart)
                                minStart = maxChild;
                        }
                    }
                }
                foreach (ParentalRelationship parents in indiv.FamiliesAsChild)
                {  // check min date at least X years after parent
                    Family fam = parents.Family;
                    if (fam.Husband != null && fam.Husband.BirthDate.IsKnown && fam.Husband.BirthDate.StartDate != FactDate.MINDATE)
                        if (fam.Husband.BirthDate.StartDate.AddYears(Properties.GeneralSettings.Default.MinParentalAge) > minStart)
                            minStart = new DateTime(fam.Husband.BirthDate.StartDate.Year + Properties.GeneralSettings.Default.MinParentalAge, 1, 1);
                    if (fam.Wife != null && fam.Wife.BirthDate.IsKnown && fam.Wife.BirthDate.StartDate != FactDate.MINDATE)
                        if (fam.Wife.BirthDate.StartDate.AddYears(Properties.GeneralSettings.Default.MinParentalAge) > minStart)
                            minStart = new DateTime(fam.Wife.BirthDate.StartDate.Year + Properties.GeneralSettings.Default.MinParentalAge, 1, 1);
                }
                if (birthDate.EndDate < minEnd)
                {  // check for BEF XXXX types that are prevalent in my tree
                    if (birthDate.StartDate == FactDate.MINDATE && birthDate.EndDate.AddYears(1) < minEnd)
                        minEnd = birthDate.EndDate.AddYears(1);
                    else
                        minEnd = birthDate.EndDate;
                }
                if (birthDate.StartDate > minStart)
                    minStart = birthDate.StartDate;
                baseDate = new FactDate(minStart, minEnd);
                if (birthDate != baseDate)
                    toAdd = baseDate;
            }
            if (toAdd != null && toAdd != birthDate && toAdd.Distance(birthDate) > 1)
            {
                // we have a date to change and its not the same 
                // range as the existing death date
                Fact looseBirth = new Fact(Fact.LOOSEBIRTH, toAdd);
                indiv.AddFact(looseBirth);
                result.Add(indiv);
            }
        }

        private FactDate BaseLivingDate(Individual indiv)
        {
            DateTime mindate = FactDate.MAXDATE;
            DateTime maxdate = GetMaxLivingDate(indiv, Fact.LOOSE_BIRTH_FACTS);
            DateTime startdate = maxdate.Year < FactDate.MAXYEARS ? FactDate.MINDATE : new DateTime(maxdate.Year - FactDate.MAXYEARS, 1, 1);
            foreach (Fact f in indiv.AllFacts)
            {
                if (Fact.LOOSE_BIRTH_FACTS.Contains(f.FactType))
                {
                    if (f.FactDate.IsKnown)
                    {
                        if (f.FactDate.StartDate != FactDate.MINDATE && f.FactDate.StartDate < mindate)
                            mindate = f.FactDate.StartDate;
                        if (f.FactDate.EndDate != FactDate.MAXDATE && f.FactDate.EndDate < mindate) //copes with BEF dates
                            mindate = f.FactDate.EndDate;
                    }
                }
            }
            if (startdate < mindate)
                return new FactDate(startdate, mindate);
            else if (mindate <= maxdate)
                return new FactDate(mindate, maxdate);
            else
                return FactDate.UNKNOWN_DATE;
        }

        #endregion

        #region Loose Deaths

        public SortableBindingList<IDisplayLooseDeath> LooseDeaths
        {
            get
            {
                if (looseDeaths != null)
                    return looseDeaths;
                SortableBindingList<IDisplayLooseDeath> result = new SortableBindingList<IDisplayLooseDeath>();
                foreach (Individual ind in individuals)
                    CheckLooseDeath(ind, result);
                looseDeaths = result;
                return result;
            }
        }

        private void CheckLooseDeath(Individual indiv, SortableBindingList<IDisplayLooseDeath> result)
        {
            FactDate deathDate = indiv.DeathDate;
            FactDate toAdd = null;
            if (deathDate.EndDate.Year - deathDate.StartDate.Year > 1)
            {
                DateTime maxLiving = GetMaxLivingDate(indiv, Fact.LOOSE_DEATH_FACTS);
                DateTime minDeath = GetMinDeathDate(indiv);
                if (minDeath != FactDate.MAXDATE)
                {   // we don't have a minimum death date so can't proceed - individual may still be alive
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
                        // or they were two BEF dates 
                        // so add to the list of loose deaths
                        toAdd = new FactDate(deathDate.StartDate, minDeath);
                    }
                }
            }
            if (toAdd != null && toAdd != deathDate && toAdd.Distance(deathDate) > 1)
            {
                // we have a date to change and its not the same 
                // range as the existing death date
                Fact looseDeath = new Fact(Fact.LOOSEDEATH, toAdd);
                indiv.AddFact(looseDeath);
                result.Add(indiv);
            }
        }

        private DateTime GetMaxLivingDate(Individual indiv, ISet<string> factTypes)
        {
            DateTime maxdate = FactDate.MINDATE;
            // having got the families the individual is a parent of
            // get the max startdate of the birthdate of the youngest child
            // this then is the minimum point they were alive
            // subtract 9 months for a male
            bool childDate = false;
            foreach (Family fam in indiv.FamiliesAsParent)
            {
                FactDate marriageDate = fam.GetPreferredFactDate(Fact.MARRIAGE);
                if (marriageDate.StartDate > maxdate && !marriageDate.IsLongYearSpan)
                    maxdate = marriageDate.StartDate;
                if (fam.Children.Count > 0)
                {
                    DateTime maxChildBirthDate = fam.Children.Max(child => child.BirthDate.StartDate);
                    if (maxChildBirthDate > maxdate)
                    {
                        maxdate = maxChildBirthDate;
                        childDate = true;
                    }
                }
            }
            if (childDate && indiv.IsMale && maxdate > FactDate.MINDATE.AddMonths(9))
            {
                // set to 9 months before birth if indiv is a father 
                // and we have changed maxdate from the MINDATE default
                // and the date is derived from a child not a marriage
                maxdate = maxdate.AddMonths(-9);
                // now set to Jan 1 of that year 9 months before birth to prevent 
                // very exact 9 months before dates
                maxdate = new DateTime(maxdate.Year, 1, 1);
            }
            foreach (Fact f in indiv.AllFacts)
                if (factTypes.Contains(f.FactType) && f.FactDate.StartDate > maxdate)
                    maxdate = f.FactDate.StartDate;
            // at this point we have the maximum point a person was alive
            // based on their oldest child and last living fact record and marriage date
            return maxdate;
        }

        private DateTime GetMinDeathDate(Individual indiv)
        {
            FactDate deathDate = indiv.DeathDate;
            DateTime now = DateTime.Now;
            FactDate.FactDateType deathDateType = deathDate.DateType;
            FactDate.FactDateType birthDateType = indiv.BirthDate.DateType;
            DateTime minDeath = FactDate.MAXDATE;
            if (indiv.BirthDate.IsKnown && indiv.BirthDate.EndDate.Year < 9999) // filter out births where no year specified
            {
                minDeath = new DateTime(indiv.BirthDate.EndDate.Year + FactDate.MAXYEARS, 12, 31);
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
            return individuals.Where(ind => ind.IsMale && !ind.IsDeathKnown() && filter(ind));
        }

        #endregion

        #region Relationship Functions

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
            foreach (ParentalRelationship parents in indiv.FamiliesAsChild)
            {
                Family family = parents.Family;
                // add parents to queue
                if (family.Husband != null && family.Husband.RelationType == Individual.UNKNOWN)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT && parents.IsNaturalFather)
                    {
                        family.Husband.Ahnentafel = indiv.Ahnentafel * 2;
                        if (family.Husband.Ahnentafel > maxAhnentafel)
                            maxAhnentafel = family.Husband.Ahnentafel;
                        queue.Enqueue(family.Husband); // add to directs queue only if natural father of direct
                    }
                    if(!setAhnenfatel)
                        queue.Enqueue(family.Husband); // add if not checking directs
                }
                if (family.Wife != null && family.Wife.RelationType == Individual.UNKNOWN)
                {
                    if (setAhnenfatel && indiv.RelationType == Individual.DIRECT && parents.IsNaturalMother)
                    {
                        family.Wife.Ahnentafel = indiv.Ahnentafel * 2 + 1;
                        if (family.Wife.Ahnentafel > maxAhnentafel)
                            maxAhnentafel = family.Wife.Ahnentafel;
                        queue.Enqueue(family.Wife);
                    }
                    if (!setAhnenfatel) // add to directs queue only if natural father of direct
                        queue.Enqueue(family.Wife); // add if not checking directs
                }
            }
        }

        private void AddChildrenToQueue(Individual indiv, Queue<Individual> queue, bool isRootPerson)
        {
            IEnumerable<Family> families = indiv.FamiliesAsParent;
            foreach (Family family in families)
            {
                foreach (Individual child in family.Children)
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
                family.SetBudgieCode(indiv, 2);
            }
        }

        public void SetRelations(string startID)
        {
            ClearRelations();
            //SetFamilies();
            Individual rootPerson = GetIndividual(startID);
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
                // set all directs as common ancestor
                i.CommonAncestor = new CommonAncestor(i, 0, false);
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
                    family.SetSpouseRelation(ind, Individual.MARRIEDTODB);
                    // all children of direct ancestors and blood relations
                    // are blood relations
                    family.SetChildRelation(queue, Individual.BLOOD);
                    family.SetChildrenCommonRelation(ind, ind.CommonAncestor);
                    family.SetBudgieCode(ind, lenAhnentafel);
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
                        family.SetSpouseRelation(ind, Individual.MARRIAGE);
                        // children of relatives by marriage that we haven't previously 
                        // identified are also relatives by marriage
                        family.SetChildRelation(queue, Individual.MARRIAGE);
                    }
                }
                Application.DoEvents();
            }
        }

        private void SetRelationDescriptions(string startID)
        {
            Individual rootPerson = GetIndividual(startID);
            IEnumerable<Individual> directs = GetAllRelationsOfType(Individual.DIRECT);
            foreach (Individual i in directs)
                i.RelationToRoot = Relationship.CalculateRelationship(rootPerson, i);
            IEnumerable<Individual> blood = GetAllRelationsOfType(Individual.BLOOD);
            foreach (Individual i in blood)
                i.RelationToRoot = Relationship.CalculateRelationship(rootPerson, i);
            IEnumerable<Individual> married = GetAllRelationsOfType(Individual.MARRIEDTODB);
            foreach(Individual i in married)
            {
                foreach(Family f in i.FamiliesAsParent)
                {
                    if (i.RelationToRoot == null && f.Spouse(i) != null && f.Spouse(i).IsBloodDirect)
                    {
                        if (f.MaritalStatus != Family.MARRIED)
                            i.RelationToRoot = "partner of " + f.Spouse(i).RelationToRoot;
                        else
                            i.RelationToRoot = (i.IsMale ? "husband of " : "wife of ") + f.Spouse(i).RelationToRoot;
                        break;
                    }
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

        #region Displays

        public IEnumerable<CensusFamily> GetAllCensusFamilies(CensusDate censusDate, bool censusDone, bool checkCensus)
        {
            if (censusDate != null)
            {
                HashSet<string> individualIDs = new HashSet<string>();
                foreach (Family f in families)
                {
                    CensusFamily cf = new CensusFamily(f, censusDate);
                    //if(cf.Members.Any(x => x.IndividualID == "I0282"))
                    //    Console.WriteLine("found it");
                    if (cf.Process(censusDate, censusDone, checkCensus))
                    {
                        individualIDs.UnionWith(cf.Members.Select(x => x.IndividualID));
                        yield return cf;
                    }
                }
                // also add all individuals that don't ever appear as a child as they may have census facts for when they are children
                foreach (Individual ind in individuals.Where(x => x.FamiliesAsChild.Count == 0))
                {
                    //if (ind.IndividualID == "I0282")
                    //    Console.WriteLine("found it");
                    CensusFamily cf = new CensusFamily(new Family(ind, Family.PRE_MARRIAGE), censusDate);
                    if (!individualIDs.Contains(ind.IndividualID) && cf.Process(censusDate, censusDone, checkCensus))
                    {
                        individualIDs.Add(ind.IndividualID);
                        yield return cf;
                    }
                }
            }
        }

        public void ClearLocations()
        {
            for (int i = 0; i < 5; i++)
                displayLocations[i] = null;
        }

        private SortableBindingList<IDisplayLocation> GetDisplayLocations(int level)
        {
            List<IDisplayLocation> result = new List<IDisplayLocation>();
            //copy to list so that any GetLocation(level) that creates a new location 
            //won't cause an error due to collection changing
            List<FactLocation> allLocations = FactLocation.AllLocations.ToList();
            foreach (FactLocation loc in allLocations)
            {
                FactLocation c = loc.GetLocation(level);
                if (c.Country != string.Empty && !result.Contains(c))
                    result.Add(c);
            }
            result.Sort(new FactLocationComparer(level));
            displayLocations[level] = new SortableBindingList<IDisplayLocation>(result);
            return displayLocations[level];
        }

        public SortableBindingList<IDisplayLocation> AllDisplayCountries
        {
            get { return displayLocations[FactLocation.COUNTRY] == null ? GetDisplayLocations(FactLocation.COUNTRY) : displayLocations[FactLocation.COUNTRY]; }
        }

        public SortableBindingList<IDisplayLocation> AllDisplayRegions
        {
            get { return displayLocations[FactLocation.REGION] == null ? GetDisplayLocations(FactLocation.REGION) : displayLocations[FactLocation.REGION]; }
        }

        public SortableBindingList<IDisplayLocation> AllDisplaySubRegions
        {
            get { return displayLocations[FactLocation.SUBREGION] == null ? GetDisplayLocations(FactLocation.SUBREGION) : displayLocations[FactLocation.SUBREGION]; }

        }

        public SortableBindingList<IDisplayLocation> AllDisplayAddresses
        {
            get { return displayLocations[FactLocation.ADDRESS] == null ? GetDisplayLocations(FactLocation.ADDRESS) : displayLocations[FactLocation.ADDRESS]; }
        }

        public SortableBindingList<IDisplayLocation> AllDisplayPlaces
        {
            get { return displayLocations[FactLocation.PLACE] == null ? GetDisplayLocations(FactLocation.PLACE) : displayLocations[FactLocation.PLACE]; }
        }

        public List<IDisplayGeocodedLocation> AllGeocodingLocations
        {
            get
            {
                List<IDisplayGeocodedLocation> result = new List<IDisplayGeocodedLocation>();
                foreach (IDisplayGeocodedLocation loc in FactLocation.AllLocations)
                    if (!loc.Equals(FactLocation.UNKNOWN_LOCATION))
                        result.Add(loc);
                return result;
            }
        }

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

        public List<IDisplayColourCensus> ColourCensus(Controls.RelationTypes relType, string surname)
        {
            Predicate<Individual> aliveOnAnyCensus = x => x.AliveOnAnyCensus;
            Predicate<Individual> filter = relType.BuildFilter<Individual>(x => x.RelationType);
            if (surname.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }
            Predicate<Individual> dateFilter = i => ((i.BirthDate.StartsBefore(CensusDate.UKCENSUS1911) || !i.BirthDate.IsKnown) &&
                                                     (i.DeathDate.EndsAfter(CensusDate.UKCENSUS1841) || !i.DeathDate.IsKnown));
            filter = FilterUtils.AndFilter<Individual>(filter, dateFilter, aliveOnAnyCensus);
            return individuals.Where(filter).ToList<IDisplayColourCensus>();
        }

        public List<IDisplayColourBMD> ColourBMD(Controls.RelationTypes relType, string surname)
        {
            Predicate<Individual> filter = relType.BuildFilter<Individual>(x => x.RelationType);
            if (surname.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }
            return individuals.Where(filter).ToList<IDisplayColourBMD>();
        }

        #endregion

        #region Data Errors

        private void SetDataErrorTypes()
        {
            int catchCount = 0;
            dataErrorTypes = new List<DataErrorGroup>();
            List<DataError>[] errors = new List<DataError>[DATA_ERROR_GROUPS];
            for (int i = 0; i < DATA_ERROR_GROUPS; i++)
                errors[i] = new List<DataError>();
            // calculate error lists
            foreach (Individual ind in AllIndividuals)
            {
                try
                {
                    if (ind.DeathDate.IsKnown)
                    {
                        if (ind.BirthDate.IsAfter(ind.DeathDate))
                            errors[(int)dataerror.BIRTH_AFTER_DEATH].Add(new DataError((int)dataerror.BIRTH_AFTER_DEATH, ind, "Died " + ind.DeathDate + " before born"));
                        if (ind.BurialDate != null && ind.BirthDate.IsAfter(ind.BurialDate))
                            errors[(int)dataerror.BIRTH_AFTER_DEATH].Add(new DataError((int)dataerror.BIRTH_AFTER_DEATH, ind, "Buried " + ind.BurialDate + " before born"));
                        if (ind.BurialDate != null && ind.BurialDate.IsBefore(ind.DeathDate) && !ind.BurialDate.Overlaps(ind.DeathDate))
                            errors[(int)dataerror.BURIAL_BEFORE_DEATH].Add(new DataError((int)dataerror.BURIAL_BEFORE_DEATH, ind, "Buried " + ind.BurialDate + " before died " + ind.DeathDate));
                        int minAge = ind.GetMinAge(ind.DeathDate);
                        if (minAge > FactDate.MAXYEARS)
                            errors[(int)dataerror.AGED_MORE_THAN_110].Add(new DataError((int)dataerror.AGED_MORE_THAN_110, ind, "Aged over " + FactDate.MAXYEARS + " before died " + ind.DeathDate));
                    }
                    #region Error facts
                    foreach (Fact f in ind.ErrorFacts)
                    {
                        bool added = false;
                        if (f.FactType == Fact.LOSTCOUSINS)
                        {
                            if (!CensusDate.IsCensusYear(f.FactDate, false))
                            {
                                errors[(int)dataerror.LOST_COUSINS_NON_CENSUS].Add(
                                    new DataError((int)dataerror.LOST_COUSINS_NON_CENSUS, ind, "Lost Cousins event for " + f.FactDate + " which isn't a census year"));
                                added = true;
                            }
                            else if (!CensusDate.IsLostCousinsCensusYear(f.FactDate, false))
                            {
                                errors[(int)dataerror.LOST_COUSINS_NOT_SUPPORTED_YEAR].Add(
                                    new DataError((int)dataerror.LOST_COUSINS_NOT_SUPPORTED_YEAR, ind, "Lost Cousins event for " + f.FactDate + " which isn't a Lost Cousins census year"));
                                added = true;
                            }
                        }
                        else if (f.IsCensusFact)
                        {
                            string comment = f.FactType == Fact.CENSUS ? "Census date " : "Residence date ";
                            if (!f.FactDate.IsKnown)
                            {
                                errors[(int)dataerror.CENSUS_COVERAGE].Add(
                                        new DataError((int)dataerror.CENSUS_COVERAGE, ind, comment + "is blank."));
                                added = true;
                            }
                            else
                            {
                                TimeSpan ts = f.FactDate.EndDate - f.FactDate.StartDate;
                                if (ts.Days > 3650)
                                {
                                    errors[(int)dataerror.CENSUS_COVERAGE].Add(
                                        new DataError((int)dataerror.CENSUS_COVERAGE, ind, comment + f.FactDate + " covers more than one census event."));
                                    added = true;
                                }
                            }
                        }
                        if (f.FactErrorLevel == Fact.FactError.WARNINGALLOW && f.FactType == Fact.RESIDENCE)
                        {
                            errors[(int)dataerror.RESIDENCE_CENSUS_DATE].Add(
                                    new DataError((int)dataerror.RESIDENCE_CENSUS_DATE, f.FactErrorLevel, ind, f.FactErrorMessage));
                            added = true;
                        }
                        if (!added)
                            errors[(int)dataerror.FACT_ERROR].Add(new DataError((int)dataerror.FACT_ERROR, f.FactErrorLevel, ind, f.FactErrorMessage));
                    }
                    #endregion
                    #region All Facts
                    foreach (Fact f in ind.AllFacts)
                    {
                        if (f.FactType != Fact.BIRTH && f.FactDate.IsBefore(ind.BirthDate))
                        {
                            if ((f.FactType == Fact.CHRISTENING || f.FactType == Fact.BAPTISM))
                            {  //due to possible late birth abt qtr reporting use 3 month fudge factor for bapm/chr
                                if (f.FactDate.IsBefore(ind.BirthDate.SubtractMonths(4)))
                                {
                                    errors[(int)dataerror.FACTS_BEFORE_BIRTH].Add(
                                        new DataError((int)dataerror.FACTS_BEFORE_BIRTH, ind, f.FactTypeDescription + " fact recorded: " +
                                                    f.FactDate + " before individual was born"));
                                }
                            }
                            else
                            {
                                errors[(int)dataerror.FACTS_BEFORE_BIRTH].Add(
                                    new DataError((int)dataerror.FACTS_BEFORE_BIRTH, ind, f.FactTypeDescription + " fact recorded: " +
                                                    f.FactDate + " before individual was born"));
                            }
                        }
                        if (Fact.LOOSE_DEATH_FACTS.Contains(f.FactType) && f.FactDate.IsAfter(ind.DeathDate))
                        {
                            errors[(int)dataerror.FACTS_AFTER_DEATH].Add(
                                new DataError((int)dataerror.FACTS_AFTER_DEATH, ind, f.FactTypeDescription + " fact recorded: " +
                                            f.FactDate + " after individual died"));
                        }
                        foreach(string tag in unknownFactTypes)
                        {
                            if (f.FactTypeDescription == tag)
                            {
                                errors[(int)dataerror.UNKNOWN_FACT_TYPE].Add(
                                    new DataError((int)dataerror.UNKNOWN_FACT_TYPE,Fact.FactError.QUESTIONABLE,
                                        ind, "Unknown fact type " + f.FactTypeDescription + " recorded"));
                            }
                        }
                    }
                    #endregion
                    foreach (ParentalRelationship parents in ind.FamiliesAsChild)
                    {
                        Family asChild = parents.Family;
                        Individual father = asChild.Husband;
                        if (father != null && ind.BirthDate.StartDate.Year != 1 && parents.IsNaturalFather)
                        {
                            int minAge = father.GetMinAge(ind.BirthDate);
                            int maxAge = father.GetMaxAge(ind.BirthDate);
                            if (minAge > 90)
                                errors[(int)dataerror.BIRTH_AFTER_FATHER_90].Add(new DataError((int)dataerror.BIRTH_AFTER_FATHER_90, ind, "Father " + father.Name + " born " + father.BirthDate + " is more than 90 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[(int)dataerror.BIRTH_BEFORE_FATHER_13].Add(new DataError((int)dataerror.BIRTH_BEFORE_FATHER_13, ind, "Father " + father.Name + " born " + father.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (father.DeathDate.IsKnown && ind.BirthDate.IsKnown)
                            {
                                FactDate conception = ind.BirthDate.SubtractMonths(9);
                                if (father.DeathDate.IsBefore(conception))
                                    errors[(int)dataerror.BIRTH_AFTER_FATHER_DEATH].Add(new DataError((int)dataerror.BIRTH_AFTER_FATHER_DEATH, ind, "Father " + father.Name + " died " + father.DeathDate + " more than 9 months before individual was born"));
                            }
                        }
                        Individual mother = asChild.Wife;
                        if (mother != null && ind.BirthDate.StartDate.Year != 1 && parents.IsNaturalMother)
                        {
                            int minAge = mother.GetMinAge(ind.BirthDate);
                            int maxAge = mother.GetMaxAge(ind.BirthDate);
                            if (minAge > 60)
                                errors[(int)dataerror.BIRTH_AFTER_MOTHER_60].Add(new DataError((int)dataerror.BIRTH_AFTER_MOTHER_60, ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is more than 60 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[(int)dataerror.BIRTH_BEFORE_MOTHER_13].Add(new DataError((int)dataerror.BIRTH_BEFORE_MOTHER_13, ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (mother.DeathDate.IsKnown && mother.DeathDate.IsBefore(ind.BirthDate))
                                errors[(int)dataerror.BIRTH_AFTER_MOTHER_DEATH].Add(new DataError((int)dataerror.BIRTH_AFTER_MOTHER_DEATH, ind, "Mother " + mother.Name + " died " + mother.DeathDate + " which is before individual was born"));
                        }
                    }
                    foreach (Family asParent in ind.FamiliesAsParent)
                    {
                        Individual spouse = asParent.Spouse(ind);
                        if (asParent.MarriageDate != null && spouse != null)
                        {
                            if (ind.DeathDate != null && asParent.MarriageDate.IsAfter(ind.DeathDate))
                                errors[(int)dataerror.MARRIAGE_AFTER_DEATH].Add(new DataError((int)dataerror.MARRIAGE_AFTER_DEATH, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after individual died " + ind.DeathDate));
                            if (spouse.DeathDate != null && asParent.MarriageDate.IsAfter(spouse.DeathDate))
                                errors[(int)dataerror.MARRIAGE_AFTER_SPOUSE_DEAD].Add(new DataError((int)dataerror.MARRIAGE_AFTER_SPOUSE_DEAD, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after spouse died " + spouse.DeathDate));
                            int maxAge = ind.GetMaxAge(asParent.MarriageDate);
                            if (maxAge < 13)
                                errors[(int)dataerror.MARRIAGE_BEFORE_13].Add(new DataError((int)dataerror.MARRIAGE_BEFORE_13, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before individual was 13 years old"));
                            maxAge = spouse.GetMaxAge(asParent.MarriageDate);
                            if (maxAge < 13)
                                errors[(int)dataerror.MARRIAGE_BEFORE_SPOUSE_13].Add(new DataError((int)dataerror.MARRIAGE_BEFORE_SPOUSE_13, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before spouse born " + spouse.BirthDate + " was 13 years old"));
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
                    if (catchCount == 0) // prevent multiple displays of the same error - usually resource icon load failures
                    {
                        ErrorHandler.Show("FTA_0001", e);
                        catchCount++;
                    }
                }
            }
            for (int i = 0; i < DATA_ERROR_GROUPS; i++)
                dataErrorTypes.Add(new DataErrorGroup(i, errors[i]));
        }

        private enum dataerror : int
        {
            BIRTH_AFTER_DEATH = 0, BIRTH_AFTER_FATHER_90 = 1, BIRTH_AFTER_MOTHER_60 = 2, BIRTH_AFTER_MOTHER_DEATH = 3,
            BIRTH_AFTER_FATHER_DEATH = 4, BIRTH_BEFORE_FATHER_13 = 5, BIRTH_BEFORE_MOTHER_13 = 6, BURIAL_BEFORE_DEATH = 7,
            AGED_MORE_THAN_110 = 8, FACTS_BEFORE_BIRTH = 9, FACTS_AFTER_DEATH = 10, MARRIAGE_AFTER_DEATH = 11,
            MARRIAGE_AFTER_SPOUSE_DEAD = 12, MARRIAGE_BEFORE_13 = 13, MARRIAGE_BEFORE_SPOUSE_13 = 14, LOST_COUSINS_NON_CENSUS = 15,
            LOST_COUSINS_NOT_SUPPORTED_YEAR = 16, RESIDENCE_CENSUS_DATE = 17, CENSUS_COVERAGE = 18, FACT_ERROR = 19, 
            UNKNOWN_FACT_TYPE = 20
        };

        public void SetDataErrorsCheckedDefaults(CheckedListBox list)
        {
            list.Items.Clear();
            foreach (DataErrorGroup dataError in dataErrorTypes)
            {
                int index = list.Items.Add(dataError);
                bool itemChecked = Application.UserAppDataRegistry.GetValue(dataError.ToString(), "True").Equals("True");
                list.SetItemChecked(index, itemChecked);
            }
        }

        public SortableBindingList<DataError> DataErrors(CheckedListBox list)
        {
            List<DataError> errors = new List<DataError>();
            foreach (int indexChecked in list.CheckedIndices)
            {
                DataErrorGroup item = (DataErrorGroup)list.Items[indexChecked];
                errors.AddRange(item.Errors);
            }
            return new SortableBindingList<DataError>(errors);
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
                case 3: uri = BuildFamilySearchQuery(censusCountry, censusYear, person); break;
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
                path.Append("%2B" + FamilySearch.GIVENNAME + "%3A%22" + HttpUtility.UrlEncode(person.Forenames) + "%22%7E%20");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            {
                path.Append("%2B" + FamilySearch.SURNAME + "%3A" + HttpUtility.UrlEncode(surname) + "%7E%20");
            }
            path.Append("%2B" + FamilySearch.RECORD_TYPE + "%3A%283%29");
            if (person.BirthDate.IsKnown)
            {
                int startYear = person.BirthDate.StartDate.Year - 1;
                int endYear = person.BirthDate.EndDate.Year + 1;
                path.Append("%2B" + FamilySearch.BIRTH_YEAR + "%3A" + startYear + "-" + endYear + "%7E%20");
            }
            if (person.BirthLocation != null)
            {
                string location;
                if (person.BirthLocation.Country != country)
                {
                    location = person.BirthLocation.Country;
                }
                else
                {
                    location = person.BirthLocation.GetLocation(FactLocation.REGION).ToString().Replace(",", "");
                }
                path.Append("%2B" + FamilySearch.BIRTH_LOCATION + "%3A" + HttpUtility.UrlEncode(location) + "%7E%20");
            }
            int collection = FamilySearch.CensusCollectionID(country, censusYear);
            if (collection > 0 || country != "Unknown")
                path.Append("&collection_id=" + collection);
            else
            {
                MessageBox.Show("Sorry searching the " + country + " census on FamilySearch for " + censusYear + " is not supported by FTAnalyzer at this time", "FT Analyzer");
                return null;
            }
            return path.Replace("+", "%20").ToString();
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
                if(censusYear == 1901)
                    query.Append("db=websearch-4150&");
                if(censusYear == 1911)
                    query.Append("db=websearch-4050&");
            }
            else if (censusCountry.Equals(Countries.UNITED_STATES))
            {
                query.Append("db=" + censusYear + "usfedcen&");
                query.Append("gss=ms_db&");
            }
            else if (censusCountry.Equals(Countries.CANADA))
            {
                if(censusYear==1921)
                    query.Append("db=cancen1921&");
                else
                    query.Append("db=" + censusYear + "canada&");
            }
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
            if (person.BirthDate.IsKnown)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear - 9;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear + 9;
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
                string location = person.BirthLocation.GetLocation(FactLocation.SUBREGION).ToString();
                query.Append("msbpn__ftp=" + HttpUtility.UrlEncode(location) + "&");
            }
            query.Append("uidh=2t2");
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private string BuildFreeCenQuery(string censusCountry, int censusYear, Individual person)
        {
            if (!censusCountry.Equals(Countries.UNITED_KINGDOM) && !censusCountry.Equals("Unknown"))
            {
                MessageBox.Show("Sorry only UK searches can be done on FreeCEN.", "FT Analyzer");
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
            if (person.BirthDate.IsKnown)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear - 9;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear + 9;
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
                string location = person.BirthLocation.SubRegion;
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
            if (!censusCountry.Equals(Countries.UNITED_KINGDOM) && !censusCountry.Equals("Unknown"))
            {
                MessageBox.Show("Sorry non UK census searching of Find My Past isn't supported in this version of FTAnalyzer", "FT Analyzer");
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
            if (person.BirthDate.IsKnown)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear - 9;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear + 9;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 3) / 2;
                    if (range > 5) range = 10;
                    if (year > censusYear) year = censusYear;
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
                query.Append("birthPlace=" + HttpUtility.UrlEncode(person.BirthLocation.SubRegion) + "&");
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

        #region Birth/Marriage/Death Searching

        public enum SearchType { BIRTH = 0, MARRIAGE = 1, DEATH = 2 };

        public void SearchBMD(SearchType st, Individual individual, int searchProvider)
        {
            string uri = null;

            switch (searchProvider)
            {
                case 0: uri = BuildAncestryQuery(st, individual); break;
                case 1: uri = BuildFindMyPastQuery(st, individual); break;
                case 2: uri = BuildFreeCenQuery(st, individual); break;
                case 3: uri = BuildFamilySearchQuery(st, individual); break;
            }
            if (uri != null)
            {
                Process.Start(uri);
            }
        }

        private string BuildFamilySearchQuery(SearchType st, Individual individual)
        {
            MessageBox.Show(Properties.Messages.NotYet, "FT Analyzer");
            return null;
        }

        private string BuildFreeCenQuery(SearchType st, Individual individual)
        {
            MessageBox.Show(Properties.Messages.NotYet, "FT Analyzer");
            return null;
        }

        private string BuildFindMyPastQuery(SearchType st, Individual individual)
        {
            MessageBox.Show(Properties.Messages.NotYet, "FT Analyzer");
            return null;
        }

        private string BuildAncestryQuery(SearchType st, Individual individual)
        {
            UriBuilder uri = new UriBuilder();
            uri.Host = "search.ancestry.co.uk";
            uri.Path = "cgi-bin/sse.dll";
            //gsln_x=NP&
            StringBuilder query = new StringBuilder();
            query.Append("gl=34&");
            query.Append("gss=ms_f-34&");
            query.Append("rank=1&");
            query.Append("new=1&");
            query.Append("so=3&");
            query.Append("MSAV=1&");
            query.Append("msT=1&");
            if (individual.Forenames != "?" && individual.Forenames.ToUpper() != "UNKNOWN")
            {
                query.Append("gsfn=" + HttpUtility.UrlEncode(individual.Forenames) + "&");
            }
            string surname = string.Empty;
            if (individual.Surname != "?" && individual.Surname.ToUpper() != "UNKNOWN")
            {
                surname = individual.Surname;
            }
            if (individual.MarriedName != "?" && individual.MarriedName.ToUpper() != "UNKNOWN" && individual.MarriedName != individual.Surname)
            {
                surname += " " + individual.MarriedName;
            }
            surname = surname.Trim();
            query.Append("gsln=" + HttpUtility.UrlEncode(surname) + "&");
            if (individual.BirthDate.IsKnown)
            {
                int startYear = individual.BirthDate.StartDate.Year;
                int endYear = individual.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear - 9;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear + 9;
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
            if (individual.BirthLocation != null)
            {
                string location = individual.BirthLocation.GetLocation(FactLocation.SUBREGION).ToString();
                query.Append("msbpn__ftp=" + HttpUtility.UrlEncode(location) + "&");
            }
            query.Append("cpxt=1&uidh=6b2&cp=11");
            uri.Query = query.ToString();
            return uri.ToString();
        }

        #endregion

        #region Location Tree Building
        public TreeNode[] GetAllLocationsTreeNodes(Font defaultFont, bool mainform)
        {
            if (mainformTreeRootNode != null)
                return BuildTreeNodeArray(mainform);

            mainformTreeRootNode = new TreeNode();
            placesTreeRootNode = new TreeNode();
            Font regularFont = new Font(defaultFont, FontStyle.Regular);
            Font boldFont = new Font(defaultFont, FontStyle.Bold);
            foreach (FactLocation location in AllDisplayPlaces)
            {
                string[] parts = location.Parts;
                TreeNode currentM = mainformTreeRootNode;
                TreeNode currentP = placesTreeRootNode;
                foreach (string part in parts)
                {
                    if (part.Length == 0 && !Properties.GeneralSettings.Default.AllowEmptyLocations) break;
                    TreeNode childM = currentM.Nodes.Find(part, false).FirstOrDefault();
                    TreeNode childP = currentP.Nodes.Find(part, false).FirstOrDefault();
                    if (childM == null)
                    {
                        TreeNode child = new TreeNode((part.Length == 0 ? "<blank>" : part));
                        child.Name = part;
                        child.Tag = location;
                        child.ToolTipText = "Geocoding Status : " + location.Geocoded;
                        SetTreeNodeImage(location, child);
                        // Set everything other than known countries to regular
                        if (currentM == mainformTreeRootNode && Countries.IsKnownCountry(part))
                            child.NodeFont = boldFont;
                        else
                            child.NodeFont = regularFont;
                        childM = child;
                        childP = (TreeNode)child.Clone();
                        currentM.Nodes.Add(childM);
                        currentP.Nodes.Add(childP);
                    }
                    currentM = childM;
                    currentP = childP;
                }
            }
            if (Properties.GeneralSettings.Default.AllowEmptyLocations)
            { // trim empty end nodes
                bool recheck = true;
                while (recheck)
                {
                    TreeNode[] emptyNodes = mainformTreeRootNode.Nodes.Find(string.Empty, true);
                    recheck = false;
                    foreach (TreeNode node in emptyNodes)
                    {
                        if (node.FirstNode == null)
                        {
                            node.Remove();
                            recheck = true;
                        }
                    }
                }
            }
            foreach (TreeNode node in mainformTreeRootNode.Nodes)
                node.Text += "         "; // force text to be longer to fix bold bug
            foreach (TreeNode node in placesTreeRootNode.Nodes)
                node.Text += "         "; // force text to be longer to fix bold bug
            return BuildTreeNodeArray(mainform);
        }

        private static void SetTreeNodeImage(FactLocation location, TreeNode child)
        {
            switch (location.GeocodeStatus)
            {
                case FactLocation.Geocode.NOT_SEARCHED:
                    child.ImageIndex = 0;
                    child.ToolTipText += "\nUse 'Run Geocoder' option under Maps menu to search Google for location.";
                    break;
                case FactLocation.Geocode.MATCHED:
                    child.ImageIndex = 1;
                    break;
                case FactLocation.Geocode.PARTIAL_MATCH:
                    child.ImageIndex = 2;
                    break;
                case FactLocation.Geocode.GEDCOM_USER:
                    child.ImageIndex = 3;
                    break;
                case FactLocation.Geocode.NO_MATCH:
                    child.ImageIndex = 4;
                    break;
                case FactLocation.Geocode.INCORRECT:
                    child.ImageIndex = 5;
                    break;
                case FactLocation.Geocode.OUT_OF_BOUNDS:
                    child.ImageIndex = 6;
                    break;
                case FactLocation.Geocode.LEVEL_MISMATCH:
                    child.ImageIndex = 7;
                    break;
            }
        }

        private TreeNode[] BuildTreeNodeArray(bool mainForm)
        {
            TreeNodeCollection nodes;
            if (mainForm)
                nodes = mainformTreeRootNode.Nodes;
            else
                nodes = placesTreeRootNode.Nodes;
            TreeNode[] result = new TreeNode[nodes.Count];
            nodes.CopyTo(result, 0);
            return result;
        }

        public void RefreshTreeNodeIcon(FactLocation location)
        {
            string[] parts = location.Parts;
            TreeNode currentM = mainformTreeRootNode;
            TreeNode currentP = placesTreeRootNode;
            if (currentM != null || currentP != null)
            {
                foreach (string part in parts)
                {
                    if (part.Length == 0 && !Properties.GeneralSettings.Default.AllowEmptyLocations) break;
                    if (mainformTreeRootNode != null)
                    {
                        TreeNode childM = currentM.Nodes.Find(part, false).FirstOrDefault();
                        currentM = childM;
                    }
                    if (placesTreeRootNode != null)
                    {
                        TreeNode childP = currentP.Nodes.Find(part, false).FirstOrDefault();
                        currentP = childP;
                    }
                }
                // we should now have nodes to update   
                if (mainformTreeRootNode != null)
                    SetTreeNodeImage(location, currentM);
                if (placesTreeRootNode != null)
                    SetTreeNodeImage(location, currentP);
            }
        }
        #endregion

        #region Geocoding

        public void LoadGeoLocationsFromDataBase()
        {
            try
            {
                DatabaseHelper dbh = DatabaseHelper.Instance;
                foreach (FactLocation loc in FactLocation.AllLocations)
                    dbh.GetLocationDetails(loc);
                WriteGeocodeStatstoRTB(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading previously geocoded data. " + ex.Message, "FT Analyzer");
            }
        }

        public void WriteGeocodeStatstoRTB(bool geocoding)
        {
            if (geocoding)
                xmlErrorbox.AppendText("\nGeocoding results:");
            // write geocode results - ignore UNKNOWN entry
            int notsearched = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED));
            xmlErrorbox.AppendText("\nFound " + FactLocation.LocationsCount + " locations in file.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER) && x.GoogleLocation.Length > 0) + " are GEDCOM/User Entered and have been geocoded.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER) && x.GoogleLocation.Length == 0) + " are GEDCOM/User Entered but lack a Google Location.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED)) + " have a geocoding match from Google.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH)) + " have partial geocoding match from Google.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.LEVEL_MISMATCH)) + " have partial geocoding match at lower level of detail.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OUT_OF_BOUNDS)) + " found by Google but outside country boundary.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.INCORRECT)) + " marked as incorrect by user.\n");
            xmlErrorbox.AppendText("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NO_MATCH)) + " could not be found on Google.\n");
            xmlErrorbox.AppendText("    " + notsearched + " haven't been searched on Google.");
            if (notsearched > 0)
                xmlErrorbox.AppendText(" Use the 'Run Geocoder' option (under Maps menu) to find them.\n");
            xmlErrorbox.BringToFront(); // force the rich text box to the front
        }

        public void OpenGeoLocations(FactLocation location)
        {
            GeocodeLocations geoLocations = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GeocodeLocations)
                {
                    f.BringToFront();
                    f.Focus();
                    geoLocations = (GeocodeLocations)f;
                    break;
                }
            }
            if (geoLocations == null)
            {
                geoLocations = new GeocodeLocations();
                geoLocations.Show();
            }
            // we now have opened form
            geoLocations.SelectLocation(location);
        }

        #endregion

        #region Relationship Groups
        public List<Individual> GetFamily(Individual startIndividiual)
        {
            List<Individual> results = new List<Individual>();
            foreach (Family f in startIndividiual.FamiliesAsParent)
            {
                foreach (Individual i in f.Members)
                    results.Add(i);
            }
            foreach (ParentalRelationship pr in startIndividiual.FamiliesAsChild)
            {
                foreach (Individual i in pr.Family.Members)
                    results.Add(i);
            }
            return results;
        }

        public List<Individual> GetAncestors(Individual startIndividual)
        {
            List<Individual> results = new List<Individual>();
            Queue<Individual> queue = new Queue<Individual>();
            results.Add(startIndividual);
            queue.Enqueue(startIndividual);
            while (queue.Count > 0)
            {
                Individual ind = queue.Dequeue();
                foreach (ParentalRelationship parents in ind.FamiliesAsChild)
                {
                    if (parents.IsNaturalFather)
                    {
                        queue.Enqueue(parents.Father);
                        results.Add(parents.Father);
                    }
                    if (parents.IsNaturalMother)
                    {
                        queue.Enqueue(parents.Mother);
                        results.Add(parents.Mother);
                    }
                }
            }
            return results;
        }

        public List<Individual> GetDescendants(Individual startIndividual)
        {
            List<Individual> results = new List<Individual>();
            List<Individual> processed = new List<Individual>();
            Queue<Individual> queue = new Queue<Individual>();
            results.Add(startIndividual);
            queue.Enqueue(startIndividual);
            while (queue.Count > 0)
            {
                Individual parent = queue.Dequeue();
                processed.Add(parent);
                foreach (Family f in parent.FamiliesAsParent)
                {
                    Individual spouse = f.Spouse(parent);
                    if (spouse != null && !processed.Contains(spouse))
                    {
                        queue.Enqueue(spouse);
                        results.Add(spouse);
                    }
                    foreach(Individual child in f.Children)
                    {
                        // we have a child and we have a parent check if natural child
                        if (!processed.Contains(child) && child.IsNaturalChildOf(parent))
                        {
                            queue.Enqueue(child);
                            results.Add(child);
                        }
                    }
                }
            }
            return results;
        }

        public List<Individual> GetAllRelations(Individual ind)
        {
            return GetFamily(ind).Union(GetAncestors(ind).Union(GetDescendants(ind))).ToList<Individual>();
        }
        #endregion

        #region Database Functions
        public bool BackupDatabase(SaveFileDialog saveDatabase, string comment)
        {
            string directory = Application.UserAppDataRegistry.GetValue("Geocode Backup Directory", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).ToString();
            saveDatabase.FileName = "FTAnalyzer-Geocodes-" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
            saveDatabase.InitialDirectory = directory;
            DialogResult result = saveDatabase.ShowDialog();
            if (result == DialogResult.OK)
            {
                DatabaseHelper dbh = DatabaseHelper.Instance;
                dbh.StartBackupRestoreDatabase();
                if (File.Exists(saveDatabase.FileName))
                    File.Delete(saveDatabase.FileName);
                ZipFile zip = new ZipFile(saveDatabase.FileName);
                zip.AddFile(dbh.Filename, string.Empty);
                zip.Comment = comment + " on " + DateTime.Now.ToString("dd MMM yyyy HH:mm");
                zip.Save();
                dbh.EndBackupDatabase();
                Application.UserAppDataRegistry.SetValue("Geocode Backup Directory", Path.GetDirectoryName(saveDatabase.FileName));
                MessageBox.Show("Database exported to " + saveDatabase.FileName, "FTAnalyzer Database Export Complete");
                return true;
            }
            return false;
        }
        #endregion

        public void Dispose()
        {
            xmlErrorbox.Dispose();
        }
    }
}
