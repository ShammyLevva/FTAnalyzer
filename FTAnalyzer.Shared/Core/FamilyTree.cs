using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using FTAnalyzer.Filters;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GeoAPI.Geometries;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace FTAnalyzer
{
    public class FamilyTree
    {
        #region Variables
        private static FamilyTree instance;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IList<FactSource> sources;
        private IList<Individual> individuals;
        private IList<Family> families;
        private IList<Tuple<string, Fact>> sharedFacts;
        private IDictionary<string, List<Individual>> occupations;
        private IDictionary<StandardisedName, StandardisedName> names;
        private ISet<string> unknownFactTypes;
        private IList<DataErrorGroup> dataErrorTypes;
        private SortableBindingList<IDisplayLocation>[] displayLocations;
        private SortableBindingList<IDisplayLooseDeath> looseDeaths;
        private SortableBindingList<IDisplayLooseBirth> looseBirths;
        private SortableBindingList<DuplicateIndividual> duplicates;
        private static int DATA_ERROR_GROUPS = 23;
        private static XmlNodeList noteNodes = null;
        private bool _loading = false;
        private bool _dataloaded = false;
        private Int64 maxAhnentafel = 0;
        private Dictionary<string, Individual> individualLookup;
        private string rootIndividualID = string.Empty;

        private int SoloFamilies { get; set; }
        private int PreMarriageFamilies { get; set; }
        public bool Geocoding { get; set; }
        public List<NonDuplicate> NonDuplicates { get; private set; }
        #endregion

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

        public static string GetText(XmlNode node, bool lookForText)
        {
            if (node == null)
                return string.Empty;
            if (node.Name.Equals("PAGE") || node.Name.Equals("TITL"))
                return node.InnerText.Trim();
            else
            {
                XmlNode text = node.SelectSingleNode(".//TEXT");
                if (text != null && lookForText && text.ChildNodes.Count > 0)
                    return GetContinuationText(text.ChildNodes);
                else if (node.FirstChild != null && node.FirstChild.Value != null)
                    return node.FirstChild.Value.Trim();
                else
                    return string.Empty;
            }
        }

        public static string GetText(XmlNode node, string tag, bool lookForText)
        {
            return GetText(GetChildNode(node, tag), lookForText);
        }

        public static XmlNode GetChildNode(XmlNode node, String tag)
        {
            return node?.SelectSingleNode(tag);
        }

        public static string GetNotes(XmlNode node)
        {
            if (node == null) return string.Empty;
            XmlNodeList notes = node.SelectNodes("NOTE");
            if (notes.Count == 0) return string.Empty;
            var result = new StringBuilder();
            try
            {
                foreach (XmlNode note in notes)
                {
                    if (note.ChildNodes.Count > 0)
                    {
                        result.AppendLine(GetContinuationText(note.ChildNodes));
                        result.AppendLine();
                    }
                    XmlAttribute ID = note.Attributes["REF"];
                    result.AppendLine(GetNoteRef(ID));
                    result.AppendLine();
                    result.AppendLine();
                }
            }
            catch (Exception)
            { }
            return result.ToString().Trim();
        }

        public static string GetNoteRef(XmlAttribute reference)
        {
            if (noteNodes == null || reference == null)
                return string.Empty;
            var result = new StringBuilder();
            foreach (XmlNode node in noteNodes)
            {
                if (node.Attributes["ID"] != null && node.Attributes["ID"].Value == reference.Value)
                {
                    result.AppendLine(GetContinuationText(node.ChildNodes));
                    return result.ToString();
                }
            }
            return string.Empty;
        }

        private static string GetContinuationText(XmlNodeList nodeList)
        {
            var result = new StringBuilder();
            foreach (XmlNode child in nodeList)
            {
                if (child.Name.Equals("#text") || child.Name.Equals("CONT"))
                    result.AppendLine(); // We have a new continuation so start a new line
                if (!child.Name.Equals("SOUR"))
                    result.Append(child.InnerText);
            }
            result.AppendLine();
            return result.ToString();
        }

        public static string ValidFilename(string filename)
        {
            int pos = filename.IndexOfAny(Path.GetInvalidFileNameChars());
            if (pos == -1)
                return filename;
            var result = new StringBuilder();
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
        public void ResetData()
        {
            _dataloaded = false;
            sources = new List<FactSource>();
            individuals = new List<Individual>();
            families = new List<Family>();
            sharedFacts = new List<Tuple<string, Fact>>();
            occupations = new Dictionary<string, List<Individual>>();
            names = new Dictionary<StandardisedName, StandardisedName>();
            unknownFactTypes = new HashSet<string>();
            dataErrorTypes = new List<DataErrorGroup>();
            displayLocations = new SortableBindingList<IDisplayLocation>[5];
            rootIndividualID = string.Empty;
            SoloFamilies = 0;
            PreMarriageFamilies = 0;
            ResetLooseFacts();
            duplicates = null;
            ClearLocations();
            TreeViewHandler.Instance.ResetData();
            noteNodes = null;
            maxAhnentafel = 0;
            FactLocation.ResetLocations();
            individualLookup = new Dictionary<string, Individual>();
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

        public XmlDocument LoadTreeHeader(string filename, IProgress<string> outputText)
        {
            _loading = true;
            ResetData();
            rootIndividualID = string.Empty;
            outputText.Report("Loading file " + filename + "\n");
            XmlDocument doc = GedcomToXml.Load(filename, Encoding.UTF8, outputText);
            if (doc == null)
            {
                doc = GedcomToXml.Load(filename, outputText);
                if (doc == null)
                    return null;
            }
            // doc.Save(@"c:\temp\FHcensusref.xml");
            // First check if file has a valid header record ie: it is actually a GEDCOM file
            XmlNode header = doc.SelectSingleNode("GED/HEAD");
            if (header == null)
            {
                outputText.Report("\n\nUnable to find GEDCOM 'HEAD' record in first line of file aborting load.\nIs " + filename + " really a GEDCOM file");
                return null;
            }
            XmlNode charset = doc.SelectSingleNode("GED/HEAD/CHAR");
            if (charset != null && charset.InnerText.Equals("ANSEL"))
                doc = GedcomToXml.Load(filename, outputText);
            if (charset != null && charset.InnerText.Equals("UNICODE"))
                doc = GedcomToXml.Load(filename, Encoding.Unicode, outputText);
            if (charset != null && charset.InnerText.Equals("ASCII"))
                doc = GedcomToXml.Load(filename, Encoding.ASCII, outputText);
            if (doc == null)
                return null;
            ReportOptions(outputText);
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
            return doc;
        }

        public void LoadTreeSources(XmlDocument doc, IProgress<int> progress, IProgress<string> outputText)
        {
            // First iterate through attributes of root finding all sources
            XmlNodeList list = doc.SelectNodes("GED/SOUR");
            int sourceMax = list.Count == 0 ? 1 : list.Count;
            int counter = 0;
            foreach (XmlNode n in list)
            {
                var fs = new FactSource(n);
                sources.Add(fs);
                progress.Report((100 * counter++) / sourceMax);
            }
            outputText.Report("Loaded " + counter + " sources.\n");
            progress.Report(100);
            // now get a list of all notes
            noteNodes = doc.SelectNodes("GED/NOTE");
        }

        public void LoadTreeIndividuals(XmlDocument doc, IProgress<int> progress, IProgress<string> outputText)
        {
            // now iterate through child elements of root
            // finding all individuals
            XmlNodeList list = doc.SelectNodes("GED/INDI");
            int individualMax = list.Count;
            int counter = 0;
            foreach (XmlNode n in list)
            {
                var individual = new Individual(n, outputText);
                if (individual.IndividualID == null)
                {
                    outputText.Report("File has invalid GEDCOM data. Individual found with no ID. Search file for 0 @@ INDI\n");
                }
                else
                {
                    individuals.Add(individual);
                    if (individualLookup.ContainsKey(individual.IndividualID))
                        outputText.Report("More than one INDI record found with ID value " + individual.IndividualID + "\n");
                    else
                        individualLookup.Add(individual.IndividualID, individual);
                    AddOccupations(individual);
                    progress.Report((100 * counter++) / individualMax);
                }
            }
            outputText.Report("Loaded " + counter + " individuals.\n");
            progress.Report(100);
        }

        public void LoadTreeFamilies(XmlDocument doc, IProgress<int> progress, IProgress<string> outputText)
        {
            // now iterate through child elements of root
            // finding all families
            XmlNodeList list = doc.SelectNodes("GED/FAM");
            int familyMax = list.Count == 0 ? 1 : list.Count;
            int counter = 0;
            foreach (XmlNode n in list)
            {
                Family family = new Family(n, outputText);
                families.Add(family);
                progress.Report((100 * counter++) / familyMax);
            }
            outputText.Report("Loaded " + counter + " families.\n");
            CheckAllIndividualsAreInAFamily(outputText);
            RemoveFamiliesWithNoIndividuals();
            progress.Report(100);
        }

        public void LoadTreeRelationships(XmlDocument doc, IProgress<int> progress, IProgress<string> outputText)
        {
            if (rootIndividualID == string.Empty)
                rootIndividualID = individuals[0].IndividualID;
            UpdateRootIndividual(rootIndividualID, progress, outputText, true);
            CreateSharedFacts();
            CountCensusFacts(outputText);
            FixIDs();
            SetDataErrorTypes();
            CountUnknownFactTypes(outputText);
            FactLocation.LoadGoogleFixesXMLFile(outputText);
            LoadLegacyLocations(doc.SelectNodes("GED/_PLAC_DEFN/PLAC"), progress);
            LoadGeoLocationsFromDataBase(outputText);
            _loading = false;
            _dataloaded = true;
        }

        private void LoadLegacyLocations(XmlNodeList list, IProgress<int> progress)
        {
            int max = list.Count / 2; // /2 to make locations load account for 50% of bar
            int counter = 0;
            int beforeCount = FactLocation.AllLocations.Count();
            foreach (XmlNode node in list)
            {
                string place = GetText(node, false);
                XmlNode lat_node = node.SelectSingleNode("MAP/LATI");
                XmlNode long_node = node.SelectSingleNode("MAP/LONG");
                if (place.Length > 0 && lat_node != null && long_node != null)
                {
                    string lat = lat_node.InnerText;
                    string lng = long_node.InnerText;
                    FactLocation loc = FactLocation.GetLocation(place, lat, lng, FactLocation.Geocode.GEDCOM_USER, true, true);
                }
                progress.Report(30 + (70 * counter++) / max);
            }
            int afterCount = FactLocation.AllLocations.Count();
            progress.Report(100);
        }

        public bool LoadGeoLocationsFromDataBase(IProgress<string> outputText)
        {
            try
            {
                DatabaseHelper.Instance.LoadGeoLocations();
                WriteGeocodeStatstoRTB(string.Empty, outputText);
            }
            catch (Exception ex)
            {
                outputText.Report("Error loading previously geocoded data. " + ex.Message);
                return false;
            }
            return true;
        }

        public void UpdateRootIndividual(string rootIndividualID, IProgress<int> progress, IProgress<string> outputText, bool locationsToFollow = false)
        {
            //int start = xmlErrorbox.TextLength;
            //xmlErrorbox.AppendText("\nCalculating Relationships using " + rootIndividualID + ": " +
            //    GetIndividual(rootIndividualID).Name + " as root person. Please wait\n\n");
            //int end = xmlErrorbox.TextLength;
            //xmlErrorbox.SelectionStart = start;
            //xmlErrorbox.SelectionLength = end - start;
            //xmlErrorbox.SelectionFont = new Font(xmlErrorbox.Font, FontStyle.Bold);
            //xmlErrorbox.SelectionLength = 0;

            outputText.Report("\nCalculating Relationships using " + rootIndividualID + ": " + GetIndividual(rootIndividualID).Name + " as root person. Please wait\n\n");

            // When the user changes the root individual, no location processing is taking place
            int locationCount = locationsToFollow ? FactLocation.AllLocations.Count() : 0;
            SetRelations(rootIndividualID);
            progress?.Report(10);
            SetRelationDescriptions(rootIndividualID);
            outputText.Report(PrintRelationCount());
            progress?.Report(30);
        }

        public void LoadStandardisedNames(string startPath)
        {
            try
            {
                string filename = Path.Combine(startPath, @"Resources\GINAP.txt");
                if (File.Exists(filename))
                {
                    ReadStandardisedNameFile(filename);
                }
            }
            catch (Exception e)
            {
                log.Warn("Failed to load Standardised names error was : " + e.Message);
            }
        }

        private void ReadStandardisedNameFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                if (line.IndexOf(',') > 0 && (values[0] == "1" || values[0] == "2"))
                {
                    StandardisedName original = new StandardisedName(values[0] == "2", values[2]);
                    StandardisedName standardised = new StandardisedName(values[1] == "2", values[3]);
                    names.Add(original, standardised);
                }
            }
            reader.Close();
        }

        public string GetStandardisedName(bool IsMale, string name)
        {
            StandardisedName gIn = new StandardisedName(IsMale, name);
            names.TryGetValue(gIn, out StandardisedName gOut);
            if (gOut == null)
                return name;
            return gOut.Name;
        }

        private void ReportOptions(IProgress<string> outputText)
        {
            if (Properties.GeneralSettings.Default.ReportOptions)
            {
                outputText.Report("\nThe current file handling options are set :");
                outputText.Report("\n    Use Special Character Filters When Loading : " + Properties.FileHandling.Default.LoadWithFilters);
                outputText.Report("\n    Retry failed lines by looking for bad line breaks : " + Properties.FileHandling.Default.RetryFailedLines);

                outputText.Report("\nThe current general options are set :");
                outputText.Report("\n    Use Baptism/Christening Date If No Birth Date : " + Properties.GeneralSettings.Default.UseBaptismDates);
                outputText.Report("\n    Use Burial/Cremation Date If No Death Date : " + Properties.GeneralSettings.Default.UseBurialDates);
                outputText.Report("\n    Allow Empty Values In Locations : " + Properties.GeneralSettings.Default.AllowEmptyLocations);
                outputText.Report("\n    Treat Residence Facts As Census Facts : " + Properties.GeneralSettings.Default.UseResidenceAsCensus);
                outputText.Report("\n    Tolerate Slightly Inaccurate Census Dates : " + Properties.GeneralSettings.Default.TolerateInaccurateCensusDate);
                outputText.Report("\n    Family Census Facts Apply To Only Parents : " + Properties.GeneralSettings.Default.OnlyCensusParents);
                outputText.Report("\n    Loose Birth Minimum Parental Age : " + Properties.GeneralSettings.Default.MinParentalAge);
                outputText.Report("\n    Show Multiple Fact Forms When Viewing Duplicates : " + Properties.GeneralSettings.Default.MultipleFactForms);
                outputText.Report("\n    Use Compact Census References : " + Properties.GeneralSettings.Default.UseCompactCensusRef);
                outputText.Report("\n    Show Alias In Name Displays : " + Properties.GeneralSettings.Default.ShowAliasInName);
                outputText.Report("\n    Hide People Tagged As Missing From Census : " + Properties.GeneralSettings.Default.HidePeopleWithMissingTag);
                outputText.Report("\n    Files use Country First Locations : " + Properties.GeneralSettings.Default.ReverseLocations);
                outputText.Report("\n    Show World Events on the 'On This Day' tab : " + Properties.GeneralSettings.Default.ShowWorldEvents);
                outputText.Report("\n    Auto Create Census Events from Notes & Sources : " + Properties.GeneralSettings.Default.AutoCreateCensusFacts);
                outputText.Report("\n    Add Auto Created Census Locations to Locations List : " + Properties.GeneralSettings.Default.AddCreatedLocations);
                outputText.Report("\n    Ignore Unknown Fact Type Warnings : " + Properties.GeneralSettings.Default.IgnoreFactTypeWarnings);

                outputText.Report("\nThe current mapping options are set :");
                outputText.Report("\n    Custom Maps Location : " + Properties.MappingSettings.Default.CustomMapPath);
                outputText.Report("\n    Display British Parish Boundaries : " + Properties.MappingSettings.Default.UseParishBoundaries);
                outputText.Report("\n    Hide Scale Bar : " + Properties.MappingSettings.Default.HideScaleBar);
                outputText.Report("\n    Include Locations with Partial Match Status : " + Properties.MappingSettings.Default.IncludePartials);
                outputText.Report("\n\n");
            }
        }

        private void RemoveFamiliesWithNoIndividuals()
        {
            (families as List<Family>).RemoveAll(x => x.FamilySize == 0);
        }

        private void CountUnknownFactTypes(IProgress<string> outputText)
        {
            if (unknownFactTypes.Count > 0 && !Properties.GeneralSettings.Default.IgnoreFactTypeWarnings)
            {
                foreach (string tag in unknownFactTypes)
                {
                    int count = AllExportFacts.Count(f => f.FactType == tag);
                    outputText.Report("\nFound " + count + " facts of unknown fact type " + tag);
                }
                outputText.Report("\n");
            }
        }

        private void CreateSharedFacts()
        {
            foreach (Tuple<string, Fact> t in sharedFacts)
            {
                Individual ind = GetIndividual(t.Item1);
                Fact fact = t.Item2;
                if (ind != null)
                    ind.AddFact(fact);
            }
        }

        private void CountCensusFacts(IProgress<string> outputText)
        {
            int censusFacts = 0;
            int censusFTAFacts = 0;
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
            int censusReferences = 0;
            int blankCensusRefs = 0;
            int partialCensusRefs = 0;
            int unrecognisedCensusRefs = 0;
            foreach (Individual ind in individuals)
            {
                censusFacts += ind.FactCount(Fact.CENSUS);
                censusFTAFacts += ind.FactCount(Fact.CENSUS_FTA);
                censusWarnAllow += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.WARNINGALLOW);
                censusWarnIgnore += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.WARNINGIGNORE);
                censusErrors += ind.ErrorFactCount(Fact.CENSUS, Fact.FactError.ERROR);
                resiFacts += ind.FactCount(Fact.RESIDENCE);
                resiWarnAllow += ind.ErrorFactCount(Fact.RESIDENCE, Fact.FactError.WARNINGALLOW);
                resiCensus += ind.ResidenceCensusFactCount;
                lostCousinsFacts += ind.FactCount(Fact.LOSTCOUSINS) + ind.FactCount(Fact.LC_FTA);
                lostCousinsWarnAllow += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.WARNINGALLOW) + ind.ErrorFactCount(Fact.LC_FTA, Fact.FactError.WARNINGALLOW);
                lostCousinsWarnIgnore += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.WARNINGIGNORE) + ind.ErrorFactCount(Fact.LC_FTA, Fact.FactError.WARNINGIGNORE);
                lostCousinsErrors += ind.ErrorFactCount(Fact.LOSTCOUSINS, Fact.FactError.ERROR) + ind.ErrorFactCount(Fact.LC_FTA, Fact.FactError.ERROR);
                censusReferences += ind.CensusReferenceCount(CensusReference.ReferenceStatus.GOOD);
                blankCensusRefs += ind.CensusReferenceCount(CensusReference.ReferenceStatus.BLANK);
                partialCensusRefs += ind.CensusReferenceCount(CensusReference.ReferenceStatus.INCOMPLETE);
                unrecognisedCensusRefs += ind.CensusReferenceCount(CensusReference.ReferenceStatus.UNRECOGNISED);
            }
            int censusTotal = censusFacts + censusWarnAllow + censusWarnIgnore + censusErrors;
            int resiTotal = resiFacts + resiWarnAllow;
            int lostCousinsTotal = lostCousinsFacts + lostCousinsWarnAllow + lostCousinsWarnIgnore + lostCousinsErrors;

            outputText.Report("\nFound " + censusTotal + " census facts in GEDCOM File (" + censusFacts + " good, ");
            if (censusWarnAllow > 0)
                outputText.Report(censusWarnAllow + " warnings (data tolerated), ");
            if (censusWarnIgnore > 0)
                outputText.Report(censusWarnIgnore + " warnings (data ignored in strict mode), ");
            if (censusErrors > 0)
                outputText.Report(censusErrors + " errors (data discarded), ");
            outputText.Report((censusFacts + censusWarnAllow) + " usable facts loaded)");

            outputText.Report("\nCreated " + censusFTAFacts + " census facts from individuals notes and source references in GEDCOM File");
            outputText.Report("\nFound " + resiTotal + " residence facts in GEDCOM File (" + resiCensus + " treated as census facts) ");
            if (resiWarnAllow > 0)
            {
                if (Properties.GeneralSettings.Default.TolerateInaccurateCensusDate)
                    outputText.Report(resiWarnAllow + " warnings (data tolerated), ");
                else
                    outputText.Report(resiWarnAllow + " warnings (data ignored in strict mode), ");
            }
            outputText.Report("\nFound " + censusReferences + " census references in file and " + blankCensusRefs + " facts missing a census reference");
            if (partialCensusRefs > 0)
                outputText.Report(", with " + partialCensusRefs + " references with partial details");
            if (unrecognisedCensusRefs > 0)
                outputText.Report(" and " + unrecognisedCensusRefs + " references that were unrecognised");
            outputText.Report("\nFound " + lostCousinsTotal + " Lost Cousins facts in GEDCOM File (" + lostCousinsFacts + " good, ");
            if (lostCousinsWarnAllow > 0)
                outputText.Report(lostCousinsWarnAllow + " warnings (data tolerated), ");
            if (lostCousinsWarnIgnore > 0)
                outputText.Report(lostCousinsWarnIgnore + " warnings (data ignored in strict mode), ");
            if (lostCousinsErrors > 0)
                outputText.Report(lostCousinsErrors + " errors (data discarded), ");
            outputText.Report((lostCousinsFacts + lostCousinsWarnAllow) + " usable facts loaded)");
            if (censusFacts == 0 && resiCensus == 0 && censusWarnAllow == 0 && censusFTAFacts == 0)
            {
                outputText.Report("\nFound no census or suitable residence facts in GEDCOM File and no recognisable\n");
                outputText.Report("census references in notes or in source records stored against an individual.\n\n");
                outputText.Report("The most likely reason is that you have recorded census facts as notes and have\n");
                outputText.Report("not recorded any census references. This will mean that the census report will\n");
                outputText.Report("show everyone as not yet found on census and the Lost Cousins report will show\n");
                outputText.Report("no-one with a census needing to be entered onto your Lost Cousins My Ancestors page.");
            }
        }

        private void AddOccupations(Individual individual)
        {
            HashSet<string> jobs = new HashSet<string>();
            foreach (Fact f in individual.GetFacts(Fact.OCCUPATION))
            {
                if (!jobs.Contains(f.Comment))
                {
                    if (!occupations.TryGetValue(f.Comment, out List<Individual> workers))
                    {
                        workers = new List<Individual>();
                        occupations.Add(f.Comment, workers);
                    }
                    workers.Add(individual);
                    jobs.Add(f.Comment);
                }
            }
        }

        private void CheckAllIndividualsAreInAFamily(IProgress<string> outputText)
        {
            foreach (Family f in families)
            {
                if (f.Husband != null)
                {
                    f.Husband.Infamily = true;
                    f.Husband.ReferralFamilyID = f.FamilyID;
                }
                if (f.Wife != null)
                {
                    f.Wife.Infamily = true;
                    f.Wife.ReferralFamilyID = f.FamilyID;
                }
                foreach (Individual c in f.Children)
                {
                    c.Infamily = true;
                    c.ReferralFamilyID = f.FamilyID;
                    if (f.Husband != null || f.Wife != null)
                        c.HasParents = true;
                }
            }
            foreach (Individual ind in individuals)
            {
                if (!ind.IsInFamily)
                    families.Add(new Family(ind, NextSoloFamily));
            }
            if (SoloFamilies > 0)
                outputText.Report("Added " + SoloFamilies + " lone individuals as single families.\n");
        }
        #endregion

        #region Properties

        public bool Loading { get { return _loading; } }

        public bool DataLoaded { get { return _dataloaded; } }

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

        public List<ExportFact> AllExportFacts
        {
            get
            {
                List<ExportFact> result = new List<ExportFact>();
                foreach (Individual ind in individuals)
                {
                    foreach (Fact f in ind.PersonalFacts)
                        result.Add(new ExportFact(ind, f));
                    foreach (Family fam in ind.FamiliesAsParent)
                        foreach (Fact famfact in fam.Facts)
                            result.Add(new ExportFact(ind, famfact));
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

        public IEnumerable<FactSource> AllSources
        {
            get { return sources; }
        }

        public int IndividualCount { get { return individuals.Count; } }

        public List<Individual> DeadOrAlive { get { return individuals.Filter(x => x.DeathDate.IsKnown && x.IsFlaggedAsLiving).ToList<Individual>(); } }

        public string NextSoloFamily { get { return "SF" + ++SoloFamilies; } }

        public string NextPreMarriageFamily { get { return "PM" + ++PreMarriageFamilies; } }
        #endregion

        #region Property Functions

        public IEnumerable<Individual> GetAllRelationsOfType(int relationType)
        {
            return individuals.Filter(ind => ind.RelationType == relationType);
        }

        public IEnumerable<Individual> GetUncertifiedFacts(string factType, int relationType)
        {
            return individuals.Filter(ind =>
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
            //            return individuals.FirstOrDefault(i => i.IndividualID == individualID);
            if (string.IsNullOrEmpty(individualID))
                return null;
            individualLookup.TryGetValue(individualID, out Individual person);
            return person;
        }

        public Family GetFamily(string familyID)
        {
            return families.FirstOrDefault(f => f.FamilyID == familyID);
        }

        public void AddSharedFact(string individual, Fact fact)
        {
            Tuple<string, Fact> sf = new Tuple<string, Fact>(individual, fact);
            sharedFacts.Add(sf);
        }

        public IEnumerable<Individual> GetIndividualsAtLocation(FactLocation loc, int level)
        {
            return individuals.Filter(i => i.IsAtLocation(loc, level));
        }

        public IEnumerable<Family> GetFamiliesAtLocation(FactLocation loc, int level)
        {
            return families.Filter(f => f.IsAtLocation(loc, level));
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
            {
                ind.FixIndividualID(indLen);
                // If the individual id has been changed, the lookup needs to be updated
                if (!individualLookup.ContainsKey(ind.IndividualID))
                    individualLookup.Add(ind.IndividualID, ind);
            }
            int famLen = families.Count.ToString().Length;
            foreach (Family f in families)
                f.FixFamilyID(famLen);
            int sourceLen = sources.Count.ToString().Length;
            foreach (FactSource s in sources)
                s.FixSourceID(sourceLen);
        }

        #endregion

        #region Loose Births

        public SortableBindingList<IDisplayLooseBirth> LooseBirths()
        {
            if (looseBirths != null)
                return looseBirths;
            SortableBindingList<IDisplayLooseBirth> result = new SortableBindingList<IDisplayLooseBirth>();
            try
            {
                foreach (Individual ind in individuals)
                    CheckLooseBirth(ind, result);
            }
            catch (Exception ex)
            {
                throw new LooseDataException("Problem calculating Loose Births. Error was " + ex.Message);
            }
            looseBirths = result;
            return result;
        }

        private void CheckLooseBirth(Individual indiv, SortableBindingList<IDisplayLooseBirth> result = null)
        {
            FactDate birthDate = indiv.BirthDate;
            FactDate toAdd = null;
            if (birthDate.EndDate.Year - birthDate.StartDate.Year > 1)
            {
                FactDate baseDate = BaseLivingDate(indiv);
                DateTime minStart = baseDate.StartDate;
                DateTime minEnd = baseDate.EndDate;
                if (birthDate.EndDate != FactDate.MAXDATE && birthDate.EndDate > minEnd)
                {   // makes sure we use birth date end in event we have not enough facts
                    minEnd = birthDate.EndDate;
                    // don't think we should set the start date as max years before end as end may be wide range into future whereas start was calculated from facts
                    //if (minStart != FactDate.MINDATE && minEnd.Year > minStart.Year + FactDate.MAXYEARS)
                    //    minStart = CreateDate(minEnd.Year - FactDate.MAXYEARS, minStart.Month, minStart.Day); // min end mustn't be more than max years after start
                }
                foreach (Family fam in indiv.FamiliesAsParent)
                {
                    FactDate marriageDate = fam.GetPreferredFactDate(Fact.MARRIAGE);
                    if (marriageDate.StartDate.Year > Properties.GeneralSettings.Default.MinParentalAge && !marriageDate.IsLongYearSpan)
                    {  // set maximum birthdate as X years before earliest marriage
                        DateTime preMarriage = CreateDate(marriageDate.StartDate.Year - Properties.GeneralSettings.Default.MinParentalAge, 12, 31);
                        if (preMarriage < minEnd && preMarriage >= minStart)
                            minEnd = preMarriage;
                    }
                    if (fam.Children.Count > 0)
                    {   // must be at least X years old at birth of child
                        List<Individual> childrenNoAFT =
                            fam.Children.Filter(child => child.BirthDate.EndDate != FactDate.MAXDATE && !child.BirthDate.IsLongYearSpan).ToList();
                        if (childrenNoAFT.Count > 0)
                        {
                            int minChildYear = childrenNoAFT.Min(child => child.BirthDate.EndDate).Year;
                            DateTime minChild = CreateDate(minChildYear - Properties.GeneralSettings.Default.MinParentalAge, 12, 31);
                            if (minChild < minEnd && minChild >= minStart)
                                minEnd = minChild;
                        }
                        List<Individual> childrenNoBEF =
                            fam.Children.Filter(child => child.BirthDate.StartDate != FactDate.MINDATE && !child.BirthDate.IsLongYearSpan).ToList();
                        if (childrenNoBEF.Count > 0)
                        {
                            int maxChildYear = childrenNoBEF.Max(child => child.BirthDate.StartDate).Year;
                            DateTime maxChild;
                            if (indiv.IsMale) // for males check that not over 100 when oldest child is born
                                maxChild = CreateDate(maxChildYear - 100, 1, 1);
                            else // for females check that not over 60 when oldest child is born
                                maxChild = CreateDate(maxChildYear - 60, 1, 1);
                            if (maxChild > minStart)
                                minStart = maxChild;
                        }
                    }
                    Individual spouse = fam.Spouse(indiv);
                    if (spouse != null && spouse.DeathDate.IsKnown)
                    {
                        DateTime maxMarried = CreateDate(spouse.DeathEnd.Year - Properties.GeneralSettings.Default.MinParentalAge, 12, 31);
                        if (maxMarried < minEnd && maxMarried >= minStart)
                            minEnd = maxMarried;
                    }
                }
                foreach (ParentalRelationship parents in indiv.FamiliesAsChild)
                {  // check min date at least X years after parent born and no later than parent dies
                    Family fam = parents.Family;
                    if (fam.Husband != null)
                    {
                        if (fam.Husband.BirthDate.IsKnown && fam.Husband.BirthDate.StartDate != FactDate.MINDATE)
                            if (fam.Husband.BirthDate.StartDate.TryAddYears(Properties.GeneralSettings.Default.MinParentalAge) > minStart)
                                minStart = CreateDate(fam.Husband.BirthDate.StartDate.Year + Properties.GeneralSettings.Default.MinParentalAge, 1, 1);
                        if (fam.Husband.DeathDate.IsKnown && fam.Husband.DeathDate.EndDate != FactDate.MAXDATE)
                            if (fam.Husband.DeathDate.EndDate.Year != FactDate.MAXDATE.Year && fam.Husband.DeathDate.EndDate.AddMonths(9) < minEnd)
                                minEnd = CreateDate(fam.Husband.DeathDate.EndDate.AddMonths(9).Year, 1, 1);
                    }
                    if (fam.Wife != null)
                    {
                        if (fam.Wife.BirthDate.IsKnown && fam.Wife.BirthDate.StartDate != FactDate.MINDATE)
                            if (fam.Wife.BirthDate.StartDate.TryAddYears(Properties.GeneralSettings.Default.MinParentalAge) > minStart)
                                minStart = CreateDate(fam.Wife.BirthDate.StartDate.Year + Properties.GeneralSettings.Default.MinParentalAge, 1, 1);
                        if (fam.Wife.DeathDate.IsKnown && fam.Wife.DeathDate.EndDate != FactDate.MAXDATE)
                            if (fam.Wife.DeathDate.EndDate.Year != FactDate.MAXDATE.Year && fam.Wife.DeathDate.EndDate < minEnd)
                                minEnd = CreateDate(fam.Wife.DeathDate.EndDate.Year, 1, 1);
                    }
                }
                if (birthDate.EndDate <= minEnd && birthDate.EndDate != FactDate.MAXDATE)
                {  // check for BEF XXXX types that are prevalent in my tree
                    if (birthDate.StartDate == FactDate.MINDATE && birthDate.EndDate.TryAddYears(1) <= minEnd)
                        minEnd = birthDate.EndDate.TryAddYears(1);
                    else
                        minEnd = birthDate.EndDate;
                }
                if (birthDate.StartDate > minStart)
                    minStart = birthDate.StartDate;
                // force min & max years with odd dates to be min & max dates
                if (minEnd.Year == FactDate.MAXDATE.Year && minEnd != FactDate.MAXDATE)
                    minEnd = FactDate.MAXDATE;
                if (minStart.Year == 1 && minStart != FactDate.MINDATE)
                    minStart = FactDate.MINDATE;
                if (minEnd.Month == 1 && minEnd.Day == 1 && birthDate.EndDate.Month == 12 && birthDate.EndDate.Day == 31)
                    minEnd = minEnd.TryAddYears(1).AddDays(-1); // year has rounded to 1st Jan when was upper year.
                baseDate = new FactDate(minStart, minEnd);
                if (birthDate != baseDate)
                    toAdd = baseDate;
            }
            if (toAdd != null && toAdd != birthDate && toAdd.Distance(birthDate) > 1)
            {
                // we have a date to change and its not the same 
                // range as the existing death date
                Fact looseBirth = new Fact(indiv.IndividualID, Fact.LOOSEBIRTH, toAdd, FactLocation.UNKNOWN_LOCATION);
                indiv.AddFact(looseBirth);
                if (result == null)
                    result.Add(indiv);
            }
        }

        private DateTime CreateDate(int year, int month, int day)
        {
            if (year > DateTime.MaxValue.Year)
                year = DateTime.MaxValue.Year;
            if (year < 1)
                year = 1;
            if (month > 12)
                month = 12;
            if (month < 1)
                month = 1;
            return new DateTime(year, month, day);
        }

        private FactDate BaseLivingDate(Individual indiv)
        {
            DateTime mindate = FactDate.MAXDATE;
            DateTime maxdate = GetMaxLivingDate(indiv, Fact.LOOSE_BIRTH_FACTS);
            DateTime startdate = maxdate.Year < FactDate.MAXYEARS ? FactDate.MINDATE : CreateDate(maxdate.Year - FactDate.MAXYEARS, 1, 1);
            foreach (Fact f in indiv.AllFacts)
            {
                if (Fact.LOOSE_BIRTH_FACTS.Contains(f.FactType))
                {
                    if (f.FactDate.IsKnown && (!Fact.IGNORE_LONG_RANGE.Contains(f.FactType) || !f.FactDate.IsLongYearSpan))
                    {  // don't consider long year span marriage or children facts
                        if (f.FactDate.StartDate != FactDate.MINDATE && f.FactDate.StartDate < mindate)
                            mindate = f.FactDate.StartDate;
                        if (f.FactDate.EndDate != FactDate.MAXDATE && f.FactDate.EndDate < mindate) //copes with BEF dates
                            mindate = f.FactDate.EndDate;
                    }
                }
            }
            if (startdate.Year != 1 && startdate.Year != FactDate.MAXDATE.Year && startdate < mindate)
                return new FactDate(startdate, mindate);
            else if (mindate.Year != 1 && mindate.Year != FactDate.MAXDATE.Year && mindate <= maxdate)
                return new FactDate(mindate, maxdate);
            else
                return FactDate.UNKNOWN_DATE;
        }

        #endregion

        #region Loose Deaths

        public SortableBindingList<IDisplayLooseDeath> LooseDeaths()
        {
            if (looseDeaths != null)
                return looseDeaths;
            SortableBindingList<IDisplayLooseDeath> result = new SortableBindingList<IDisplayLooseDeath>();
            try
            {
                foreach (Individual ind in individuals)
                    CheckLooseDeath(ind, result);
            }
            catch (Exception ex)
            {
                throw new LooseDataException("Problem calculating Loose Deaths. Error was " + ex.Message);
            }
            looseDeaths = result;
            return result;
        }

        private void CheckLooseDeath(Individual indiv, SortableBindingList<IDisplayLooseDeath> result = null)
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
                              && deathDate.EndDate.TryAddYears(1) == minDeath)
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
                Fact looseDeath = new Fact(indiv.IndividualID, Fact.LOOSEDEATH, toAdd, FactLocation.UNKNOWN_LOCATION);
                indiv.AddFact(looseDeath);
                if (result != null)
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
                List<Individual> childrenNoLongSpan = fam.Children.Filter(child => !child.BirthDate.IsLongYearSpan).ToList<Individual>();
                if (childrenNoLongSpan.Count > 0)
                {
                    DateTime maxChildBirthDate = childrenNoLongSpan.Max(child => child.BirthDate.StartDate);
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
                maxdate = CreateDate(maxdate.Year, 1, 1);
            }
            // Check max date on all facts of facttype but don't consider long year span marriage or children facts
            foreach (Fact f in indiv.AllFacts)
                if (factTypes.Contains(f.FactType) && f.FactDate.StartDate > maxdate && (!Fact.IGNORE_LONG_RANGE.Contains(f.FactType) || !f.FactDate.IsLongYearSpan))
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
                minDeath = CreateDate(indiv.BirthDate.EndDate.Year + FactDate.MAXYEARS, 12, 31);
                if (birthDateType == FactDate.FactDateType.BEF)
                    minDeath = minDeath.TryAddYears(1);
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

        private void CheckLooseMarriage(Individual ind)
        {

        }

        #endregion

        #region TreeTops

        public IEnumerable<IDisplayIndividual> GetTreeTops(Predicate<Individual> filter)
        {
            return individuals.Filter(ind => !ind.HasParents && filter(ind));
        }

        #endregion

        #region WorldWars

        public IEnumerable<IDisplayIndividual> GetWorldWars(Predicate<Individual> filter)
        {
            return individuals.Filter(ind => ind.IsMale && filter(ind));
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
                i.CommonAncestor = null;
                i.RelationToRoot = string.Empty;
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
                    if (!setAhnenfatel)
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

        public Individual RootPerson { get; set; }

        public void SetRelations(string startID)
        {
            ClearRelations();
            RootPerson = GetIndividual(startID);
            Individual ind = RootPerson;
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
            }
            // we have now set all direct ancestors and all blood relations
            // all that remains is to loop through the marriage relations
            IEnumerable<Individual> marriedDBs = GetAllRelationsOfType(Individual.MARRIEDTODB);
            AddToQueue(queue, marriedDBs);
            int ignored = 0;
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
                else
                    ignored++;
            }
        }

        private void SetRelationDescriptions(string startID)
        {
            IEnumerable<Individual> directs = GetAllRelationsOfType(Individual.DIRECT);
            IEnumerable<Individual> blood = GetAllRelationsOfType(Individual.BLOOD);
            IEnumerable<Individual> married = GetAllRelationsOfType(Individual.MARRIEDTODB);
            Individual rootPerson = GetIndividual(startID);
            foreach (Individual i in directs)
            {
                i.RelationToRoot = Relationship.CalculateRelationship(rootPerson, i);
            }
            foreach (Individual i in blood)
            {
                i.RelationToRoot = Relationship.CalculateRelationship(rootPerson, i);
            }
            foreach (Individual i in married)
            {
                foreach (Family f in i.FamiliesAsParent)
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
                foreach (Individual ind in individuals.Filter(x => x.FamiliesAsChild.Count == 0))
                {
                    //if (ind.IndividualID == "I0282")
                    //    Console.WriteLine("found it");
                    CensusFamily cf = new CensusFamily(new Family(ind, FamilyTree.Instance.NextPreMarriageFamily), censusDate);
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
            get { return displayLocations[FactLocation.COUNTRY] ?? GetDisplayLocations(FactLocation.COUNTRY); }
        }

        public SortableBindingList<IDisplayLocation> AllDisplayRegions
        {
            get { return displayLocations[FactLocation.REGION] ?? GetDisplayLocations(FactLocation.REGION); }
        }

        public SortableBindingList<IDisplayLocation> AllDisplaySubRegions
        {
            get { return displayLocations[FactLocation.SUBREGION] ?? GetDisplayLocations(FactLocation.SUBREGION); }

        }

        public SortableBindingList<IDisplayLocation> AllDisplayAddresses
        {
            get { return displayLocations[FactLocation.ADDRESS] ?? GetDisplayLocations(FactLocation.ADDRESS); }
        }

        public SortableBindingList<IDisplayLocation> AllDisplayPlaces
        {
            get { return displayLocations[FactLocation.PLACE] ?? GetDisplayLocations(FactLocation.PLACE); }
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

        public SortableBindingList<IDisplayFact> GetSourceDisplayFacts(FactSource source)
        {
            SortableBindingList<IDisplayFact> result = new SortableBindingList<IDisplayFact>();
            foreach (Fact f in source.Facts)
            {
                if (f.Individual != null)
                {
                    DisplayFact df = new DisplayFact(f.Individual, f);
                    if (!result.Contains(df))
                        result.Add(df);
                }
                else
                {
                    if (f.Family != null && f.Family.Husband != null)
                    {
                        DisplayFact df = new DisplayFact(f.Family.Husband, f);
                        if (!result.Contains(df))
                            result.Add(df);
                    }
                    if (f.Family != null && f.Family.Wife != null)
                    {
                        DisplayFact df = new DisplayFact(f.Family.Wife, f);
                        if (!result.Contains(df))
                            result.Add(df);
                    }
                }
            }
            return result;
        }

        public SortableBindingList<IDisplaySource> AllDisplaySources
        {
            get
            {
                SortableBindingList<IDisplaySource> result = new SortableBindingList<IDisplaySource>();
                foreach (IDisplaySource s in sources)
                    result.Add(s);
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

        public List<IDisplayColourCensus> ColourCensus(string country, Controls.RelationTypes relType, string surname, ComboBoxFamily family)
        {
            Predicate<Individual> filter;
            if (family == null)
            {
                filter = relType.BuildFilter<Individual>(x => x.RelationType);
                if (surname.Length > 0)
                {
                    Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                    filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
                }
                Predicate<Individual> dateFilter;
                if (country.Equals(Countries.UNITED_STATES))
                    dateFilter = i => ((i.BirthDate.StartsBefore(CensusDate.USCENSUS1940) || !i.BirthDate.IsKnown) &&
                                                         (i.DeathDate.EndsAfter(CensusDate.USCENSUS1790) || !i.DeathDate.IsKnown));
                else if (country.Equals(Countries.CANADA))
                    dateFilter = i => ((i.BirthDate.StartsBefore(CensusDate.CANADACENSUS1921) || !i.BirthDate.IsKnown) &&
                                                         (i.DeathDate.EndsAfter(CensusDate.CANADACENSUS1851) || !i.DeathDate.IsKnown));
                else if (country.Equals(Countries.IRELAND))
                    dateFilter = i => ((i.BirthDate.StartsBefore(CensusDate.IRELANDCENSUS1911) || !i.BirthDate.IsKnown) &&
                                                         (i.DeathDate.EndsAfter(CensusDate.IRELANDCENSUS1901) || !i.DeathDate.IsKnown));
                else
                    dateFilter = i => ((i.BirthDate.StartsBefore(CensusDate.UKCENSUS1911) || !i.BirthDate.IsKnown) &&
                                                         (i.DeathDate.EndsAfter(CensusDate.UKCENSUS1841) || !i.DeathDate.IsKnown));
                filter = FilterUtils.AndFilter<Individual>(filter, dateFilter, x => x.AliveOnAnyCensus(country) && !x.OutOfCountryOnAllCensus(country));
            }
            else
                filter = x => family.Members.Contains<Individual>(x);
            return individuals.Filter(filter).ToList<IDisplayColourCensus>();
        }

        public List<IDisplayColourBMD> ColourBMD(Controls.RelationTypes relType, string surname, ComboBoxFamily family)
        {
            Predicate<Individual> filter;
            if (family == null)
            {
                filter = relType.BuildFilter<Individual>(x => x.RelationType);
                if (surname.Length > 0)
                {
                    Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                    filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
                }
            }
            else
                filter = x => family.Members.Contains<Individual>(x);
            return individuals.Filter(filter).ToList<IDisplayColourBMD>();
        }

        public List<IDisplayMissingData> MissingData(Controls.RelationTypes relType, string surname, ComboBoxFamily family)
        {
            Predicate<Individual> filter;
            if (family == null)
            {
                filter = relType.BuildFilter<Individual>(x => x.RelationType);
                if (surname.Length > 0)
                {
                    Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                    filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
                }
            }
            else
                filter = x => family.Members.Contains<Individual>(x);
            return individuals.Filter(filter).ToList<IDisplayMissingData>();
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
            #region Individual Fact Errors
            foreach (Individual ind in AllIndividuals)
            {
                try
                {
                    #region Death facts
                    if (ind.DeathDate.IsKnown)
                    {
                        if (ind.BirthDate.IsAfter(ind.DeathDate))
                            errors[(int)Dataerror.BIRTH_AFTER_DEATH].Add(new DataError((int)Dataerror.BIRTH_AFTER_DEATH, ind, "Died " + ind.DeathDate + " before born"));
                        if (ind.BurialDate != null && ind.BirthDate.IsAfter(ind.BurialDate))
                            errors[(int)Dataerror.BIRTH_AFTER_DEATH].Add(new DataError((int)Dataerror.BIRTH_AFTER_DEATH, ind, "Buried " + ind.BurialDate + " before born"));
                        if (ind.BurialDate != null && ind.BurialDate.IsBefore(ind.DeathDate) && !ind.BurialDate.Overlaps(ind.DeathDate))
                            errors[(int)Dataerror.BURIAL_BEFORE_DEATH].Add(new DataError((int)Dataerror.BURIAL_BEFORE_DEATH, ind, "Buried " + ind.BurialDate + " before died " + ind.DeathDate));
                        int minAge = ind.GetMinAge(ind.DeathDate);
                        if (minAge > FactDate.MAXYEARS)
                            errors[(int)Dataerror.AGED_MORE_THAN_110].Add(new DataError((int)Dataerror.AGED_MORE_THAN_110, ind, "Aged over " + FactDate.MAXYEARS + " before died " + ind.DeathDate));
                        if (ind.IsFlaggedAsLiving)
                            errors[(int)Dataerror.LIVING_WITH_DEATH_DATE].Add(new DataError((int)Dataerror.LIVING_WITH_DEATH_DATE, ind, "Flagged as living but has death date of " + ind.DeathDate));
                    }
                    #endregion
                    #region Error facts
                    foreach (Fact f in ind.ErrorFacts)
                    {
                        bool added = false;
                        if (f.FactErrorNumber != 0)
                        {
                            errors[f.FactErrorNumber].Add(
                                new DataError(f.FactErrorNumber, ind, f.FactErrorMessage));
                            added = true;
                        }
                        else if (f.FactType == Fact.LOSTCOUSINS || f.FactType == Fact.LC_FTA)
                        {
                            if (!CensusDate.IsCensusYear(f.FactDate, f.Country, false))
                            {
                                errors[(int)Dataerror.LOST_COUSINS_NON_CENSUS].Add(
                                    new DataError((int)Dataerror.LOST_COUSINS_NON_CENSUS, ind, "Lost Cousins event for " + f.FactDate + " which isn't a census year"));
                                added = true;
                            }
                            else if (!CensusDate.IsLostCousinsCensusYear(f.FactDate, false))
                            {
                                errors[(int)Dataerror.LOST_COUSINS_NOT_SUPPORTED_YEAR].Add(
                                    new DataError((int)Dataerror.LOST_COUSINS_NOT_SUPPORTED_YEAR, ind, "Lost Cousins event for " + f.FactDate + " which isn't a Lost Cousins census year"));
                                added = true;
                            }
                        }
                        else if (f.IsCensusFact)
                        {
                            string comment = f.FactType == Fact.CENSUS ? "Census date " : "Residence date ";
                            if (!f.FactDate.IsKnown)
                            {
                                errors[(int)Dataerror.CENSUS_COVERAGE].Add(
                                        new DataError((int)Dataerror.CENSUS_COVERAGE, ind, comment + "is blank."));
                                added = true;
                            }
                            else
                            {
                                TimeSpan ts = f.FactDate.EndDate - f.FactDate.StartDate;
                                if (ts.Days > 3650)
                                {
                                    errors[(int)Dataerror.CENSUS_COVERAGE].Add(
                                        new DataError((int)Dataerror.CENSUS_COVERAGE, ind, comment + f.FactDate + " covers more than one census event."));
                                    added = true;
                                }
                            }
                        }
                        if (f.FactErrorLevel == Fact.FactError.WARNINGALLOW && f.FactType == Fact.RESIDENCE)
                        {
                            errors[(int)Dataerror.RESIDENCE_CENSUS_DATE].Add(
                                    new DataError((int)Dataerror.RESIDENCE_CENSUS_DATE, f.FactErrorLevel, ind, f.FactErrorMessage));
                            added = true;
                        }
                        if (!added)
                            errors[(int)Dataerror.FACT_ERROR].Add(new DataError((int)Dataerror.FACT_ERROR, f.FactErrorLevel, ind, f.FactErrorMessage));
                    }
                    #endregion
                    #region All Facts
                    foreach (Fact f in ind.AllFacts)
                    {
                        if (FactBeforeBirth(ind, f))
                            errors[(int)Dataerror.FACTS_BEFORE_BIRTH].Add(
                                new DataError((int)Dataerror.FACTS_BEFORE_BIRTH, ind, f.FactErrorMessage));
                        if (FactAfterDeath(ind, f))
                            errors[(int)Dataerror.FACTS_AFTER_DEATH].Add(
                                new DataError((int)Dataerror.FACTS_AFTER_DEATH, ind, f.FactErrorMessage));
                        if (!Properties.GeneralSettings.Default.IgnoreFactTypeWarnings)
                        {
                            foreach (string tag in unknownFactTypes)
                            {
                                if (f.FactTypeDescription == tag)
                                {
                                    errors[(int)Dataerror.UNKNOWN_FACT_TYPE].Add(
                                        new DataError((int)Dataerror.UNKNOWN_FACT_TYPE, Fact.FactError.QUESTIONABLE,
                                            ind, "Unknown fact type " + f.FactTypeDescription + " recorded"));
                                }
                            }
                        }
                    }
                    #endregion
                    #region Parents Facts
                    foreach (ParentalRelationship parents in ind.FamiliesAsChild)
                    {
                        Family asChild = parents.Family;
                        Individual father = asChild.Husband;
                        if (father != null && ind.BirthDate.StartDate.Year != 1 && parents.IsNaturalFather)
                        {
                            int minAge = father.GetMinAge(ind.BirthDate);
                            int maxAge = father.GetMaxAge(ind.BirthDate);
                            if (minAge > 90)
                                errors[(int)Dataerror.BIRTH_AFTER_FATHER_90].Add(new DataError((int)Dataerror.BIRTH_AFTER_FATHER_90, ind, "Father " + father.Name + " born " + father.BirthDate + " is more than 90 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[(int)Dataerror.BIRTH_BEFORE_FATHER_13].Add(new DataError((int)Dataerror.BIRTH_BEFORE_FATHER_13, ind, "Father " + father.Name + " born " + father.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (father.DeathDate.IsKnown && ind.BirthDate.IsKnown)
                            {
                                FactDate conception = ind.BirthDate.SubtractMonths(9);
                                if (father.DeathDate.IsBefore(conception))
                                    errors[(int)Dataerror.BIRTH_AFTER_FATHER_DEATH].Add(new DataError((int)Dataerror.BIRTH_AFTER_FATHER_DEATH, ind, "Father " + father.Name + " died " + father.DeathDate + " more than 9 months before individual was born"));
                            }
                        }
                        Individual mother = asChild.Wife;
                        if (mother != null && ind.BirthDate.StartDate.Year != 1 && parents.IsNaturalMother)
                        {
                            int minAge = mother.GetMinAge(ind.BirthDate);
                            int maxAge = mother.GetMaxAge(ind.BirthDate);
                            if (minAge > 60)
                                errors[(int)Dataerror.BIRTH_AFTER_MOTHER_60].Add(new DataError((int)Dataerror.BIRTH_AFTER_MOTHER_60, ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is more than 60 yrs old when individual was born"));
                            if (maxAge < 13)
                                errors[(int)Dataerror.BIRTH_BEFORE_MOTHER_13].Add(new DataError((int)Dataerror.BIRTH_BEFORE_MOTHER_13, ind, "Mother " + mother.Name + " born " + mother.BirthDate + " is less than 13 yrs old when individual was born"));
                            if (mother.DeathDate.IsKnown && mother.DeathDate.IsBefore(ind.BirthDate))
                                errors[(int)Dataerror.BIRTH_AFTER_MOTHER_DEATH].Add(new DataError((int)Dataerror.BIRTH_AFTER_MOTHER_DEATH, ind, "Mother " + mother.Name + " died " + mother.DeathDate + " which is before individual was born"));
                        }
                    }
                    foreach (Family asParent in ind.FamiliesAsParent)
                    {
                        Individual spouse = asParent.Spouse(ind);
                        if (asParent.MarriageDate != null && spouse != null)
                        {
                            if (ind.DeathDate != null && asParent.MarriageDate.IsAfter(ind.DeathDate))
                                errors[(int)Dataerror.MARRIAGE_AFTER_DEATH].Add(new DataError((int)Dataerror.MARRIAGE_AFTER_DEATH, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after individual died " + ind.DeathDate));
                            if (spouse.DeathDate != null && asParent.MarriageDate.IsAfter(spouse.DeathDate))
                                errors[(int)Dataerror.MARRIAGE_AFTER_SPOUSE_DEAD].Add(new DataError((int)Dataerror.MARRIAGE_AFTER_SPOUSE_DEAD, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is after spouse died " + spouse.DeathDate));
                            int maxAge = ind.GetMaxAge(asParent.MarriageDate);
                            if (maxAge < 13 && ind.BirthDate.IsAfter(FactDate.MARRIAGE_LESS_THAN_13))
                                errors[(int)Dataerror.MARRIAGE_BEFORE_13].Add(new DataError((int)Dataerror.MARRIAGE_BEFORE_13, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before individual was 13 years old"));
                            maxAge = spouse.GetMaxAge(asParent.MarriageDate);
                            if (maxAge < 13 && spouse.BirthDate.IsAfter(FactDate.MARRIAGE_LESS_THAN_13))
                                errors[(int)Dataerror.MARRIAGE_BEFORE_SPOUSE_13].Add(new DataError((int)Dataerror.MARRIAGE_BEFORE_SPOUSE_13, ind, "Marriage to " + spouse.Name + " in " + asParent.MarriageDate + " is before spouse born " + spouse.BirthDate + " was 13 years old"));
                            //if (ind.FirstMarriage != null && ind.FirstMarriage.MarriageDate != null)
                            //{
                            //    if (asParent.MarriageDate.isAfter(ind.FirstMarriage.MarriageDate))
                            //    {  // we have a later marriage now see if first marriage wife

                            //    }
                            //}
                        }
                    }
                    #endregion
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
            #endregion
            #region Family Fact Errors
            catchCount = 0;
            foreach (Family fam in AllFamilies)
            {
                try
                {
                    foreach (Fact f in fam.Facts)
                    {
                        if (f.FactErrorLevel == Fact.FactError.ERROR)
                        {
                            if (f.FactType == Fact.CHILDREN1911)
                                errors[(int)Dataerror.CHILDRENSTATUS_TOTAL_MISMATCH].Add(
                                    new DataError((int)Dataerror.CHILDRENSTATUS_TOTAL_MISMATCH, fam, f.FactErrorMessage));
                            else
                                errors[(int)Dataerror.FACT_ERROR].Add(
                                    new DataError((int)Dataerror.FACT_ERROR, fam, f.FactErrorMessage));
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
            #endregion

            for (int i = 0; i < DATA_ERROR_GROUPS; i++)
                dataErrorTypes.Add(new DataErrorGroup(i, errors[i]));
        }

        public IList<DataErrorGroup> DataErrorTypes { get { return dataErrorTypes; } }

        public bool FactBeforeBirth(Individual ind, Fact f)
        {
            if (f.FactType != Fact.BIRTH & f.FactType != Fact.BIRTH_CALC && Fact.LOOSE_BIRTH_FACTS.Contains(f.FactType) && f.FactDate.IsBefore(ind.BirthDate))
            {
                if ((f.FactType == Fact.CHRISTENING || f.FactType == Fact.BAPTISM))
                {  //due to possible late birth abt qtr reporting use 3 month fudge factor for bapm/chr
                    if (f.FactDate.IsBefore(ind.BirthDate.SubtractMonths(4)))
                        return true;
                }
                else
                    return true;
            }
            return false;
        }

        public bool FactAfterDeath(Individual ind, Fact f)
        {
            return Fact.LOOSE_DEATH_FACTS.Contains(f.FactType) && f.FactDate.IsAfter(ind.DeathDate);
        }

        public enum Dataerror : int
        {
            BIRTH_AFTER_DEATH = 0, BIRTH_AFTER_FATHER_90 = 1, BIRTH_AFTER_MOTHER_60 = 2, BIRTH_AFTER_MOTHER_DEATH = 3,
            BIRTH_AFTER_FATHER_DEATH = 4, BIRTH_BEFORE_FATHER_13 = 5, BIRTH_BEFORE_MOTHER_13 = 6, BURIAL_BEFORE_DEATH = 7,
            AGED_MORE_THAN_110 = 8, FACTS_BEFORE_BIRTH = 9, FACTS_AFTER_DEATH = 10, MARRIAGE_AFTER_DEATH = 11,
            MARRIAGE_AFTER_SPOUSE_DEAD = 12, MARRIAGE_BEFORE_13 = 13, MARRIAGE_BEFORE_SPOUSE_13 = 14, LOST_COUSINS_NON_CENSUS = 15,
            LOST_COUSINS_NOT_SUPPORTED_YEAR = 16, RESIDENCE_CENSUS_DATE = 17, CENSUS_COVERAGE = 18, FACT_ERROR = 19,
            UNKNOWN_FACT_TYPE = 20, LIVING_WITH_DEATH_DATE = 21, CHILDRENSTATUS_TOTAL_MISMATCH = 22
        };

        #endregion

        #region Census Searching

        public void SearchCensus(string censusCountry, int censusYear, Individual person, int censusProvider)
        {
            string uri = null;

            switch (censusProvider)
            {
                case 0: uri = BuildAncestryQuery(censusCountry, censusYear, person); break;
                case 1:
                    if (censusYear == 1939)
                        uri = BuildFindMyPast1939Query(censusCountry, person);
                    else
                        uri = BuildFindMyPastQuery(censusCountry, censusYear, person);
                    break;
                case 2: uri = BuildFreeCenQuery(censusCountry, censusYear, person); break;
                case 3: uri = BuildFamilySearchQuery(censusCountry, censusYear, person); break;
            }
            if (uri != null)
            {
                HttpUtility.VisitWebsite(uri);
            }
        }

        private string BuildFamilySearchQuery(string country, int censusYear, Individual person)
        {
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            // bad  https://familysearch.org/search/record/results%23count=20&query=%2Bgivenname%3ACharles~%20%2Bsurname%3AGalloway~%20%2Brecord_type%3A(3)&collection_id=2046756
            // good https://familysearch.org/search/record/results#count=20&query=%2Bgivenname%3ACharles%7E%20%2Bsurname%3ABisset%7E%20%2Brecord_country%3AScotland%20%2Brecord_type%3A%283%29&collection_id=2046756
            StringBuilder path = new StringBuilder();
            path.Append("https://www.familysearch.org/search/record/results#count=20&query=");
            if (person.Forename != "?" && person.Forename.ToUpper() != Individual.UNKNOWN_NAME)
                path.Append("%2B" + FamilySearch.GIVENNAME + "%3A%22" + HttpUtility.UrlEncode(person.Forenames) + "%22%7E%20");
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != Individual.UNKNOWN_NAME)
                path.Append("%2B" + FamilySearch.SURNAME + "%3A" + HttpUtility.UrlEncode(surname) + "%7E%20");
            path.Append("%2B" + FamilySearch.RECORD_TYPE + "%3A%283%29");
            if (person.BirthDate.IsKnown)
            {
                int startYear = person.BirthDate.StartDate.Year - 1;
                int endYear = person.BirthDate.EndDate.Year + 1;
                path.Append("%2B" + FamilySearch.BIRTH_YEAR + "%3A" + startYear + "-" + endYear + "%7E%20");
            }
            string location = Countries.UNKNOWN_COUNTRY;
            if (person.BirthLocation != FactLocation.UNKNOWN_LOCATION)
            {
                if (person.BirthLocation.Country != country)
                    location = person.BirthLocation.Country;
                else
                    location = person.BirthLocation.GetLocation(FactLocation.REGION).ToString().Replace(",", "");
                path.Append("%2B" + FamilySearch.BIRTH_LOCATION + "%3A" + HttpUtility.UrlEncode(location) + "%7E%20");
            }
            int collection = FamilySearch.CensusCollectionID(country, censusYear);
            if (collection > 0)
                path.Append("&collection_id=" + collection);
            else
            {
                collection = FamilySearch.CensusCollectionID(location, censusYear);
                if (collection > 0)
                    path.Append("&collection_id=" + collection);
                else
                    if (Countries.IsUnitedKingdom(country))
                {
                    collection = FamilySearch.CensusCollectionID(Countries.ENGLAND, censusYear);
                    path.Append("&collection_id=" + collection);
                }
                else if (Countries.IsKnownCountry(country))
                {
                    throw new CensusSearchException("Sorry searching the " + country + " census on FamilySearch for " + censusYear + " is not supported by FTAnalyzer at this time");
                }
            }
            return path.Replace("+", "%20").ToString();
        }

        private string BuildAncestryQuery(string censusCountry, int censusYear, Individual person)
        {
            UriBuilder uri = new UriBuilder
            {
                Host = "search.ancestry.co.uk",
                Path = "cgi-bin/sse.dll"
            };
            StringBuilder query = new StringBuilder();
            if (censusCountry.Equals(Countries.UNITED_KINGDOM))
            {
                query.Append("gl=" + censusYear + "uki&");
                query.Append("gss=ms_f-68&");
            }
            else if (censusCountry.Equals(Countries.IRELAND))
            {
                if (censusYear == 1901)
                    query.Append("db=websearch-4150&");
                if (censusYear == 1911)
                    query.Append("db=websearch-4050&");
            }
            else if (censusCountry.Equals(Countries.UNITED_STATES))
            {
                query.Append("db=" + censusYear + "usfedcen&");
                query.Append("gss=ms_db&");
            }
            else if (censusCountry.Equals(Countries.CANADA))
            {
                if (censusYear == 1921)
                    query.Append("db=cancen1921&");
                else
                    query.Append("db=" + censusYear + "canada&");
            }
            query.Append("rank=1&");
            query.Append("new=1&");
            query.Append("so=3&");
            query.Append("MSAV=1&");
            query.Append("msT=1&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
                query.Append("gsfn=" + HttpUtility.UrlEncode(person.Forenames) + "&");
            string surname = string.Empty;
            if (person.Surname != "?" && person.Surname.ToUpper() != Individual.UNKNOWN_NAME)
                surname = person.Surname;
            if (person.MarriedName != "?" && person.MarriedName.ToUpper() != Individual.UNKNOWN_NAME && person.MarriedName != person.Surname)
                surname += " " + person.MarriedName;
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
            if (person.BirthLocation != FactLocation.UNKNOWN_LOCATION)
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
                throw new CensusSearchException("Sorry only UK searches can be done on FreeCEN.");
            }
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            UriBuilder uri = new UriBuilder
            {
                Host = "www.freecen.org.uk",
                Path = "/cgi/search.pl"
            };
            StringBuilder query = new StringBuilder();
            query.Append("y=" + censusYear + "&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
            {
                int pos = person.Forenames.IndexOf(" ");
                string forename = person.Forenames;
                if (pos > 0)
                    forename = person.Forenames.Substring(0, pos); //strip out any middle names as FreeCen searches better without then
                query.Append("g=" + HttpUtility.UrlEncode(forename) + "&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != Individual.UNKNOWN_NAME)
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
                    query.Append("r=0&");
                else if (range <= 2)
                    query.Append("r=2&");
                else if (range <= 5)
                    query.Append("r=5&");
                else
                    query.Append("r=10&");
                query.Append("a=" + year + "&");
            }
            if (person.BirthLocation != FactLocation.UNKNOWN_LOCATION)
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
            // new http://search.findmypast.co.uk/results/united-kingdom-records-in-census-land-and-surveys?firstname=peter&firstname_variants=true&lastname=moir&lastname_variants=true&eventyear=1881&eventyear_offset=2&yearofbirth=1825&yearofbirth_offset=2
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            UriBuilder uri = new UriBuilder
            {
                Host = "search.findmypast.co.uk"
            };
            if (censusCountry.Equals(Countries.UNITED_STATES))
                uri.Path = "/results/united-states-records-in-census-land-and-surveys";
            else if (Countries.IsUnitedKingdom(censusCountry))
                uri.Path = "/results/united-kingdom-records-in-census-land-and-surveys";
            else if (censusCountry.Equals(Countries.IRELAND))
                uri.Path = "/results/ireland-records-in-census-land-and-surveys";
            else
                uri.Path = "/results/world-records-in-census-land-and-surveys";
            StringBuilder query = new StringBuilder();
            query.Append("eventyear=" + censusYear + "&eventyear_offset=0&");

            if (person.Forenames != "?" && person.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
            {
                int pos = person.Forenames.IndexOf(" ");
                string forenames = person.Forenames;
                if (pos > 0)
                    forenames = person.Forenames.Substring(0, pos); //strip out any middle names as searches better without then
                query.Append("firstname=" + HttpUtility.UrlEncode(forenames) + "&");
                query.Append("firstname_variants=true&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != Individual.UNKNOWN_NAME)
            {
                query.Append("lastName=" + HttpUtility.UrlEncode(surname) + "&");
                query.Append("lastname_variants=true&");
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
                query.Append("yearofbirth=" + year + "&");
                query.Append("yearofbirth_offset=" + range + "&");
            }
            //if (person.BirthLocation != FactLocation.UNKNOWN_LOCATION)
            //{
            //    query.Append("birthPlace=" + HttpUtility.UrlEncode(person.BirthLocation.SubRegion) + "&");
            //    Tuple<string, string> area = person.BirthLocation.FindMyPastCountyCode;
            //    if (area != null)
            //    {
            //        query.Append("country=" + HttpUtility.UrlEncode(area.Item1) + "&");
            //        query.Append("coIdList=" + HttpUtility.UrlEncode(area.Item2));
            //    }
            //    else
            //    {
            //        query.Append("country=&coIdList=");
            //    }
            //}
            //else
            //{
            //    query.Append("birthPlace=&country=&coIdList=");
            //}
            uri.Query = query.ToString();
            return @"http://www.awin1.com/cread.php?awinmid=2114&awinaffid=88963&clickref=FTACensusSearch&p=" + uri.ToString();
        }

        private string BuildFindMyPast1939Query(string censusCountry, Individual person)
        {
            // new http://search.findmypast.co.uk/results/world-records/1939-register?firstname=frederick&firstname_variants=true&lastname=deakin&lastname_variants=true&yearofbirth=1879
            FactDate censusFactDate = CensusDate.UKCENSUS1939;
            UriBuilder uri = new UriBuilder
            {
                Host = "search.findmypast.co.uk",
                Path = "/results/world-records/1939-register"
            };
            StringBuilder query = new StringBuilder();

            if (person.Forenames != "?" && person.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
            {
                int pos = person.Forenames.IndexOf(" ");
                string forenames = person.Forenames;
                if (pos > 0)
                    forenames = person.Forenames.Substring(0, pos); //strip out any middle names as searches better without then
                query.Append("firstname=" + HttpUtility.UrlEncode(forenames) + "&");
                query.Append("firstname_variants=true&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != Individual.UNKNOWN_NAME)
            {
                query.Append("lastName=" + HttpUtility.UrlEncode(surname) + "&");
                query.Append("lastname_variants=true&");
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
                    if (year > 1939) year = 1939;
                }
                query.Append("yearofbirth=" + year + "&");
                query.Append("yearofbirth_offset=" + range + "&");
            }
            uri.Query = query.ToString();
            return @"http://www.awin1.com/cread.php?awinmid=2114&awinaffid=88963&clickref=FTACensusSearch&p=" + uri.ToString();
        }

        #endregion

        #region Birth/Marriage/Death Searching

        public enum SearchType { BIRTH = 0, MARRIAGE = 1, DEATH = 2 };

        public void SearchBMD(SearchType st, Individual individual, FactDate factdate, int searchProvider)
        {
            string uri = null;
            if (!factdate.IsKnown || factdate.DateType.Equals(FactDate.FactDateType.AFT) || factdate.DateType.Equals(FactDate.FactDateType.BEF))
            {
                if (st.Equals(SearchType.BIRTH))
                {
                    CheckLooseBirth(individual);
                    factdate = individual.LooseBirthDate;
                }
                //if(st.Equals(SearchType.MARRIAGE))
                //{
                //    CheckLooseMarriage(individual);
                //    factdate = individual.LooseMarriageDate;
                //}
                if (st.Equals(SearchType.DEATH))
                {
                    CheckLooseDeath(individual);
                    factdate = individual.LooseDeathDate;
                }
                if (factdate.StartDate > factdate.EndDate)
                    factdate = FactDate.UNKNOWN_DATE; // errors in facts corrupts loose births or deaths
            }
            switch (searchProvider)
            {
                case 0: uri = BuildAncestryQuery(st, individual, factdate); break;
                case 1: uri = BuildFindMyPastQuery(st, individual, factdate); break;
                case 2: uri = BuildFreeBMDQuery(st, individual, factdate); break;
                case 3: uri = BuildFamilySearchQuery(st, individual, factdate); break;
            }
            if (uri != null)
                HttpUtility.VisitWebsite(uri);
        }

        private string BuildFamilySearchQuery(SearchType st, Individual individual, FactDate factdate)
        {
            // https://familysearch.org/search/record/results?count=20&query=%2Bgivenname%3AElizabeth~%20%2Bsurname%3AAckers~%20%2Bbirth_place%3A%22walton%20le%20dale%2C%20lancashire%2C%20england%22~%20%2Bbirth_year%3A1879-1881~%20%2Brecord_country%3AEngland
            UriBuilder uri = new UriBuilder
            {
                Host = "familysearch.org",
                Path = "search/record/results"
            };
            StringBuilder query = new StringBuilder();
            query.Append("count=20&query=");

            if (individual.Forename != "?" && individual.Forename.ToUpper() != Individual.UNKNOWN_NAME)
                query.Append("%2Bgivenname%3A" + HttpUtility.UrlEncode(individual.Forename) + "~%20");
            string surname = GetSurname(st, individual, false);
            query.Append("%2Bsurname%3A" + HttpUtility.UrlEncode(surname) + "~%20");
            if (individual.BirthDate.IsKnown)
            {
                int startYear = individual.BirthDate.StartDate.Year;
                int endYear = individual.BirthDate.EndDate.Year;
                if (startYear == FactDate.MINDATE.Year)
                    startYear = endYear - 9;
                else if (endYear == FactDate.MAXDATE.Year)
                    endYear = startYear + 9;
                query.Append("%2Bbirth_year%3A" + startYear + "-" + endYear + "~%20");
            }
            if (st.Equals(SearchType.BIRTH) && individual.BirthLocation != FactLocation.UNKNOWN_LOCATION)
            {  // add birth place if searching for a birth
                string location = individual.BirthLocation.GetLocation(FactLocation.SUBREGION).ToString();
                query.Append("%2Bbirth_place%3A%22" + HttpUtility.UrlEncode(location) + "%22~%20");
            }
            string record_country = RecordCountry(st, individual, factdate);
            if (Countries.IsKnownCountry(record_country))
                query.Append("%2Brecord_country%3A" + HttpUtility.UrlEncode(record_country));
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private static string RecordCountry(SearchType st, Individual individual, FactDate factdate)
        {
            string record_country = Countries.UNKNOWN_COUNTRY;
            if (Countries.IsKnownCountry(individual.BirthLocation.Country))
                record_country = individual.BirthLocation.Country;
            if (st.Equals(SearchType.MARRIAGE))
                record_country = individual.BestLocation(factdate).Country;
            if (st.Equals(SearchType.DEATH) && Countries.IsKnownCountry(individual.DeathLocation.Country))
                record_country = individual.DeathLocation.Country;
            if (!Countries.IsKnownCountry(record_country))
                record_country = individual.BestLocation(factdate).Country;
            return record_country;
        }

        private string BuildFreeBMDQuery(SearchType st, Individual individual, FactDate factdate)
        {
            throw new CensusSearchException(Properties.Messages.NotYet);
        }

        private string BuildFindMyPastQuery(SearchType st, Individual individual, FactDate factdate)
        {
            UriBuilder uri = new UriBuilder
            {
                Host = "search.findmypast.co.uk"
            };
            string record_country = RecordCountry(st, individual, factdate);
            if (Countries.IsUnitedKingdom(record_country))
                uri.Path = "results/united-kingdom-records-in-birth-marriage-death-and-parish-records";
            else if (record_country.Equals(Countries.UNITED_STATES))
                uri.Path = "results/united-states-records-in-birth-marriage-death-and-parish-records";
            else if (record_country.Equals(Countries.NEW_ZEALAND) || record_country.Equals(Countries.AUSTRALIA))
                uri.Path = "results/australia-and-new-zealand-records-in-birth-marriage-death-and-parish-records";
            else if (record_country.Equals(Countries.IRELAND))
                uri.Path = "results/ireland-records-in-birth-marriage-death-and-parish-records";
            else
                uri.Path = "results/world-records-in-birth-marriage-death-and-parish-records";
            if (st.Equals(SearchType.BIRTH))
                uri.Path = uri.Path + "/births-and-baptisms~church-registers";
            if (st.Equals(SearchType.MARRIAGE))
                uri.Path = uri.Path + "/church-registers~marriages-and-divorces";
            if (st.Equals(SearchType.DEATH))
                uri.Path = uri.Path + "/church-registers~wills-and-probate~deaths-and-burials";
            StringBuilder query = new StringBuilder();
            if (individual.Forenames != "?" && individual.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
                query.Append("firstname=" + HttpUtility.UrlEncode(individual.Forenames) + "&firstname_variants=true&");
            string surname = GetSurname(st, individual, false);
            query.Append("lastname=" + HttpUtility.UrlEncode(surname) + "&lastname_variants=true&");
            AppendYearandRange(individual.BirthDate, query, "yearofbirth=", "yearofbirth_offset=", true);
            if (st.Equals(SearchType.MARRIAGE))
                AppendYearandRange(factdate, query, "yearofmarriage=", "yearofmarriage_offset=", true);
            if (st.Equals(SearchType.DEATH))
                AppendYearandRange(factdate, query, "yearofdeath=", "yearofdeath_offset=", true);
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private static string GetSurname(SearchType st, Individual individual, bool ancestry)
        {
            string surname = string.Empty;
            if (individual.Surname != "?" && individual.Surname.ToUpper() != Individual.UNKNOWN_NAME)
                surname = individual.Surname;
            if (st.Equals(SearchType.DEATH) && individual.MarriedName != "?" && individual.MarriedName.ToUpper() != Individual.UNKNOWN_NAME && individual.MarriedName != individual.Surname)
                surname = ancestry ? surname + " " + individual.MarriedName : individual.MarriedName; // for ancestry combine names for others sites just use marriedName if death search
            surname = surname.Trim();
            return surname;
        }

        private string BuildAncestryQuery(SearchType st, Individual individual, FactDate factdate)
        {
            UriBuilder uri = new UriBuilder
            {
                Host = "search.ancestry.co.uk",
                Path = "cgi-bin/sse.dll"
            };
            //gsln_x=NP&
            StringBuilder query = new StringBuilder();
            if (st.Equals(SearchType.BIRTH))
                query.Append("gl=BMD_BIRTH&");
            if (st.Equals(SearchType.MARRIAGE))
                query.Append("gl=BMD_MARRIAGE&");
            if (st.Equals(SearchType.DEATH))
                query.Append("gl=BMD_DEATH&");
            query.Append("gss=ms_f-34&");
            query.Append("rank=1&");
            query.Append("new=1&");
            query.Append("so=3&");
            query.Append("MSAV=1&");
            query.Append("msT=1&");
            if (individual.Forenames != "?" && individual.Forenames.ToUpper() != Individual.UNKNOWN_NAME)
                query.Append("gsfn=" + HttpUtility.UrlEncode(individual.Forenames) + "&");
            string surname = GetSurname(st, individual, true);
            query.Append("gsln=" + HttpUtility.UrlEncode(surname) + "&");
            AppendYearandRange(individual.BirthDate, query, "msbdy=", "msbdp=", false);
            if (individual.BirthDate.IsKnown)
                query.Append("&msbdy_x=1");
            if (individual.BirthLocation != FactLocation.UNKNOWN_LOCATION)
            {
                string location = individual.BirthLocation.GetLocation(FactLocation.SUBREGION).ToString();
                query.Append("msbpn__ftp=" + HttpUtility.UrlEncode(location) + "&");
            }
            if (st.Equals(SearchType.DEATH) && factdate.IsKnown)
            {
                AppendYearandRange(factdate, query, "msddy=", "msddp=", false);
                query.Append("&msddy_x=1");
            }
            if (st.Equals(SearchType.MARRIAGE) && factdate.IsKnown)
            {
                AppendYearandRange(factdate, query, "msgdy=", "msgdp=", false);
                query.Append("&msgdy_x=1");
            }
            query.Append("cpxt=1&uidh=6b2&cp=11");
            uri.Query = query.ToString();
            return uri.ToString();
        }

        private static void AppendYearandRange(FactDate factdate, StringBuilder query, string yeartext, string rangetext, bool FMP)
        {
            if (factdate.IsKnown)
            {
                int startYear = factdate.StartDate.Year;
                int endYear = factdate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear - (FMP ? 39 : 9);
                    range = FMP ? 40 : 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear + (FMP ? 39 : 9);
                    range = FMP ? 40 : 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 2) / 2; // add two to make year range searches always at least one year either side
                    if (2 < range && range < 5) range = 5;
                    if (range > 5 && !FMP) range = 10;
                    if (FMP)
                    {
                        if (5 < range && range < 10) range = 10;
                        if (10 < range && range < 20) range = 20;
                        if (range > 20) range = 40;
                    }
                }
                query.Append(yeartext + year + "&");
                query.Append(rangetext + range + "&");
            }
        }

        #endregion

        #region Geocoding

        public void WriteGeocodeStatstoRTB(string title, IProgress<string> outputText)
        {
            outputText.Report("\n" + title);
            // write geocode results - ignore UNKNOWN entry
            int notsearched = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED));
            int needsReverse = FactLocation.AllLocations.Count(x => x.NeedsReverseGeocoding);
            //Predicate<FactLocation> predicate = x => x.NeedsReverseGeocoding;
            //List<FactLocation> needRev = FactLocation.AllLocations.Where(predicate).ToList();
            outputText.Report("\nFound " + FactLocation.LocationsCount + " locations in file.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER) && x.FoundLocation.Length > 0) + " are GEDCOM/User Entered and have been geocoded.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER) && x.FoundLocation.Length == 0) + " are GEDCOM/User Entered but lack a Google Location.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED)) + " have a geocoding match from Google.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KMATCH)) + " have a geocoding match from Ordnance Survey.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KFUZZY)) + " have a fuzzy geocoding match from Ordnance Survey.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH)) + " have partial geocoding match from Google.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.LEVEL_MISMATCH)) + " have partial geocoding match at lower level of detail.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KPARTIAL)) + " have partial geocoding match from Ordnance Survey.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OUT_OF_BOUNDS)) + " found by Google but outside country boundary.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.INCORRECT)) + " marked as incorrect by user.\n");
            outputText.Report("    " + FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NO_MATCH)) + " could not be found on Google.\n");
            outputText.Report("    " + notsearched + " haven't been searched.");
            if (notsearched > 0)
                outputText.Report(" Use the 'Run Google/OS Geocoder' option (under Maps menu) to find them.\n");
            if (needsReverse > 0)
            {
                outputText.Report("\nNote " + needsReverse + " of the searched locations are missing a Google location.");
                outputText.Report(" Use the 'Lookup Blank Google Locations' option (under Maps menu) to find them.\n");
            }
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
                    foreach (Individual child in f.Children)
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

        #region Duplicates Processing
        public SortableBindingList<IDisplayDuplicateIndividual> GenerateDuplicatesList(int value, IProgress<int> progress, IProgress<int> maximum, CancellationToken ct)
        {
            log.Debug("FamilyTree.GenerateDuplicatesList");
            if (duplicates != null)
            {
                maximum.Report(MaxDuplicateScore());
                return BuildDuplicateList(value); // we have already processed the duplicates since the file was loaded
            }
            duplicates = new SortableBindingList<DuplicateIndividual>();
            IEnumerable<Individual> males = individuals.Filter<Individual>(x => (x.Gender == "M" || x.Gender == "U"));
            IEnumerable<Individual> females = individuals.Filter<Individual>(x => (x.Gender == "F" || x.Gender == "U"));
            int progressMaximum = (males.Count() * males.Count() + females.Count() * females.Count()) / 2;
            progress.Report(0);
            try
            {
                IdentifyDuplicates(ct, progress, 0, progressMaximum, males);
                IdentifyDuplicates(ct, progress, (males.Count() * males.Count()) / 2, progressMaximum, females);
            }
            catch (OperationCanceledException)
            {
                progress.Report(0);
                maximum.Report(10);
                duplicates = null;
                return null;
            }
            maximum.Report(MaxDuplicateScore());
            DeserializeNonDuplicates();
            return BuildDuplicateList(value);
        }

        private int MaxDuplicateScore()
        {
            int score = 0;
            foreach (DuplicateIndividual dup in duplicates)
            {
                if (dup.Score > score)
                    score = dup.Score;
            }
            return score;
        }

        private void IdentifyDuplicates(CancellationToken ct, IProgress<int> progress, int progressSoFar, int progressMaximum, IEnumerable<Individual> enumerable)
        {
            log.Debug("FamilyTree.IdentifyDuplicates");
            var index = 0;
            foreach (var indA in enumerable)
            {
                index++;
                foreach (var indB in enumerable.Skip(index))
                {
                    if (indA.GenderMatches(indB) && indA.Name != Individual.UNKNOWN_NAME && indB.Name != Individual.UNKNOWN_NAME)
                    {
                        if (indA.SurnameMetaphone.Equals(indB.SurnameMetaphone) &&
                            (indA.ForenameMetaphone.Equals(indB.ForenameMetaphone) || indA.StandardisedName.Equals(indB.StandardisedName)) &&
                            indA.BirthDate.Distance(indB.BirthDate) < 5)
                        {
                            var test = new DuplicateIndividual(indA, indB);
                            if (test.Score > 0)
                                duplicates.Add(test);
                        }
                    }
                    ct.ThrowIfCancellationRequested();
                    progressSoFar++;
                    if (progressSoFar % 1000 == 0)
                        progress.Report((100 * progressSoFar) / progressMaximum);
                }
            }
        }

        public SortableBindingList<IDisplayDuplicateIndividual> BuildDuplicateList(int minScore)
        {
            log.Debug("FamilyTree.BuildDuplicateList");
            if (duplicates == null)
                log.Error("BuildDuplicateList called with null duplicates");

            var select = new SortableBindingList<IDisplayDuplicateIndividual>();
            if (NonDuplicates == null)
                DeserializeNonDuplicates();
            foreach (DuplicateIndividual dup in duplicates)
            {
                if (dup.Score >= minScore)
                {
                    var dispDup = new DisplayDuplicateIndividual(dup);
                    var toCheck = new NonDuplicate(dispDup);
                    dispDup.IgnoreNonDuplicate = NonDuplicates.Contains(toCheck);
                    if (!select.Contains(dispDup) && !(dispDup.IgnoreNonDuplicate && Properties.Settings.Default.HideIgnoredDuplicates))
                        select.Add(dispDup);
                }
            }
            return select;
        }

        public void SerializeNonDuplicates()
        {
            log.Debug("FamilyTree.SerializeNonDuplicates");
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string file = Path.Combine(Properties.GeneralSettings.Default.SavePath, "NonDuplicates.xml");
                using (Stream stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, NonDuplicates);
                }
            }
            catch (Exception e)
            {
                log.Error("Error " + e.Message + " writing NonDuplicates.xml");
            }
        }

        public void DeserializeNonDuplicates()
        {
            log.Debug("FamilyTree.DeserializeNonDuplicates");
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string file = Path.Combine(Properties.GeneralSettings.Default.SavePath, "NonDuplicates.xml");
                if (File.Exists(file))
                {
                    using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        NonDuplicates = (List<NonDuplicate>)formatter.Deserialize(stream);
                    }
                }
                else
                    NonDuplicates = new List<NonDuplicate>();
            }
            catch (Exception e)
            {
                log.Error("Error " + e.Message + " reading NonDuplicates.xml");
                NonDuplicates = new List<NonDuplicate>();
            }
        }
        #endregion

        #region Report Issues
        public HashSet<string> UnrecognisedCensusReferences()
        {
            var result = new HashSet<string>();
            IEnumerable<Fact> unrecognised = AllIndividuals.SelectMany(x => x.PersonalFacts.Filter(f => f.IsCensusFact && f.CensusReference != null && f.CensusReference.Status.Equals(CensusReference.ReferenceStatus.UNRECOGNISED)));
            foreach (Fact f in unrecognised)
                result.Add(CensusReference.ClearCommonPhrases(f.CensusReference.Reference));
            return result;
        }

        public HashSet<string> MissingCensusReferences()
        {
            var result = new HashSet<string>();
            IEnumerable<Fact> missing = AllIndividuals.SelectMany(x => x.PersonalFacts.Filter(f => f.IsCensusFact && f.CensusReference != null && f.CensusReference.Status.Equals(CensusReference.ReferenceStatus.BLANK)));
            foreach (Fact f in missing)
                result.Add(CensusReference.ClearCommonPhrases(f.SourceList)); // for missing census references show sources for census fact
            return result;
        }

        public HashSet<string> UnrecognisedCensusReferencesNotes()
        {
            var result = new HashSet<string>();
            IEnumerable<Individual> unrecognised = AllIndividuals.Filter(i => i.UnrecognisedCensusNotes.Length > 0);
            foreach (Individual i in unrecognised)
                result.Add(i.UnrecognisedCensusNotes + "\n--------------------------------------------------------------------------------\n");
            return result;
        }
        #endregion

        #region Today
        public void AddTodaysFacts(DateTime chosenDate, bool wholeMonth, int stepSize, IProgress<int> progress, IProgress<string> outputText)
        {
            string dateDesc;
            var sb = new StringBuilder();
            if (wholeMonth)
            {
                dateDesc = chosenDate.ToString("MMMM");
                sb.Append(@"{\rtf1\ansi \b GEDCOM and World Events in " + dateDesc + @"\b0.}\n\n");
            }
            else
            {
                dateDesc = chosenDate.ToString("d MMMM");
                sb.Append(@"{\rtf1\ansi \b GEDCOM and World Events on " + dateDesc + @"\b0.}\n\n");
            }
            var todaysFacts = new List<DisplayFact>();
            foreach (Individual i in individuals)
            {
                foreach (Fact f in i.AllFacts)
                    if (!f.Created && !f.IsCensusFact && f.FactType != Fact.OCCUPATION && f.FactDate.IsExact && f.FactDate.StartDate.Month == chosenDate.Month)
                        if (wholeMonth || f.FactDate.StartDate.Day == chosenDate.Day)
                            todaysFacts.Add(new DisplayFact(i, f));
            }
            todaysFacts.Sort();
            if (Properties.GeneralSettings.Default.ShowWorldEvents)
            {
                int earliestYear = todaysFacts.Count > 0 ? todaysFacts[0].FactDate.StartDate.Year : 1752; // if no facts show world events for Gregorian calendar to today
                List<DisplayFact> worldEvents = AddWorldEvents(earliestYear, chosenDate, wholeMonth, stepSize, progress);
                todaysFacts.AddRange(worldEvents);
                todaysFacts.Sort();
            }
            foreach (DisplayFact f in todaysFacts)
                sb.Append(f.ToString() + "\n");
            outputText.Report(sb.ToString());
        }

        public List<DisplayFact> AddWorldEvents(int earliestYear, DateTime chosenDate, bool wholeMonth, int stepSize, IProgress<int> progress)
        {
            // use Wikipedia API at vizgr.org/historical-events/ to find what happened on that date in the past
            var events = new List<DisplayFact>();
            string URL;
            FactDate eventDate;
            int barMinimum = earliestYear;
            int barRange = chosenDate.Year - earliestYear;
            progress.Report(0);
            for (int year = earliestYear; year <= chosenDate.Year; year++)
            {
                int diff = chosenDate.Year - year;
                if (diff % stepSize == 0)
                {
                    if (wholeMonth)
                        URL = @"http://www.vizgr.org/historical-events/search.php?links=true&format=xml&begin_date=" + year.ToString() + chosenDate.ToString("MM", CultureInfo.InvariantCulture) + "00" +
                            "&end_date=" + year.ToString() + chosenDate.ToString("MM", CultureInfo.InvariantCulture) + "31";
                    else
                        URL = @"http://www.vizgr.org/historical-events/search.php?links=true&format=xml&begin_date=" + year.ToString() + chosenDate.ToString("MMdd", CultureInfo.InvariantCulture) +
                            "&end_date=" + year.ToString() + chosenDate.ToString("MMdd", CultureInfo.InvariantCulture);
                    XmlDocument doc = GetWikipediaData(URL);
                    if (wholeMonth)
                        eventDate = new FactDate(CreateDate(year, chosenDate.Month, 1), CreateDate(year, chosenDate.Month + 1, 1).AddDays(-1));
                    else
                        eventDate = new FactDate(CreateDate(year, chosenDate.Month, chosenDate.Day), CreateDate(year, chosenDate.Month, chosenDate.Day));
                    if (doc.InnerText.Length > 0)
                    {
                        FactDate fd;
                        XmlNodeList nodes = doc.SelectNodes("/result/event");
                        foreach (XmlNode worldEvent in nodes)
                        {
                            XmlNode descNode = worldEvent.SelectSingleNode("description");
                            string desc = FixWikiFormatting(descNode.InnerText);
                            XmlNode dateNode = worldEvent.SelectSingleNode("date");
                            fd = GetWikiDate(dateNode, eventDate);
                            var f = new Fact("Wikipedia", Fact.WORLD_EVENT, fd, FactLocation.UNKNOWN_LOCATION, desc, true, true);
                            var df = new DisplayFact(null, string.Empty, string.Empty, f);
                            events.Add(df);
                        }
                    }
                }
                progress.Report((year - barMinimum) / barRange);
            }
            return events;
        }

        private static Regex brackets = new Regex("{{.*}}", RegexOptions.Compiled);
        private static Regex links = new Regex("<a href=.*</a>", RegexOptions.Compiled);
        private static Regex quotes = new Regex("(.*)quot(.*)quot(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private string FixWikiFormatting(string input)
        {
            string result = input.Replace("ampampnbsp", " ").Replace("ampnbsp", " ").Replace("ampampndash", "-").Replace("ampndash", "-");
            //strip out {{cite xxxxx }} citation text with its urls
            result = brackets.Replace(result, string.Empty);
            result = links.Replace(result, string.Empty);
            Match match = quotes.Match(result);
            if (match.Success)
                result = match.Groups[1].ToString().Trim() + " " + '\u0022' + match.Groups[2].ToString().Trim() + '\u0022' + " " + match.Groups[3].ToString().Trim();
            return result;
        }

        private static FactDate GetWikiDate(XmlNode dateNode, FactDate defaultDate)
        {
            FactDate fd;
            try
            {
                string[] dateFields = dateNode.InnerText.Split(new Char[] { '/' });
                int nodeyear = Int32.Parse(dateFields[0]);
                int nodemonth = Int32.Parse(dateFields[1]);
                int nodeday = Int32.Parse(dateFields[2]);
                fd = new FactDate(new DateTime(nodeyear, nodemonth, nodeday).ToString("dd MMM yyyy"));
            }
            catch (Exception)
            {
                log.Error("Error processing wiki date for " + dateNode);
                fd = defaultDate;
            }
            return fd;
        }

        private XmlDocument GetWikipediaData(string URL)
        {
            string result = string.Empty;
            var doc = new XmlDocument();
            try
            {
                //doc.Load(URL); // using doc.load throws XmlException slowing down loading of data
                HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                Encoding encode = Encoding.GetEncoding("utf-8");
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream(), encode);
                    result = reader.ReadToEnd();
                }
                if (!result.Contains("No events found for this query"))
                    doc.LoadXml(result);
            }
            catch (XmlException)
            {
                // we have an empty result so we can just accept that and return an empty document.
            }
            catch (Exception e)
            {
                log.Error("Error trying to load data from " + URL + "\n\n" + e.Message);
            }
            return doc;
        }

        #endregion
    }
}
