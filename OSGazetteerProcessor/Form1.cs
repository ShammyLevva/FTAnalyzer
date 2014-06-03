using GeoAPI.Geometries.Prepared;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using NetTopologySuite.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSGazetteerProcessor
{
    public partial class Form1 : Form
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(OSGazetteerProcessor.Form1));

        private IList<OS50kGazetteer> OS50k;
        private List<Feature> englishParishes;
        private List<Feature> scottishParishes;
        private List<Feature> oldEnglishCounties;

        private IDictionary<string, ISet<Tuple<string,string>>> oldCountiesToCounties;

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadOS50kGazetteer()
        {
            OS50k = new List<OS50kGazetteer>();
            try
            {
                string startPath;
                if (Application.StartupPath.Contains("Common7\\IDE")) // running unit tests
                    startPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..");
                else
                    startPath = Application.StartupPath;
                string filename = @"C:\Maps\FTAnalyzer\50kgaz2014-input.txt";
                if (File.Exists(filename))
                    ReadOS50kGazetteer(filename);
            }
            catch (Exception e)
            {
                log.Warn("Failed to load OS50k Gazetteer error was : " + e.Message);
            }
        }

        public void ReadOS50kGazetteer(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.IndexOf(':') > 0)
                    OS50k.Add(new OS50kGazetteer(line));
            }
            reader.Close();
        }

        public void SaveOS50kGazetteer()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Maps\FTAnalyzer\OS50kGazetteer.txt"))
            {
                foreach (OS50kGazetteer os50k in OS50k)
                    sw.WriteLine(os50k.ToString());
            }
        }

        public List<Feature> LoadFeatureList(string filename)
        {
            List<Feature> features = new List<Feature>();
            if (File.Exists(filename))
            {
                using (ShapefileDataReader sdr = new ShapefileDataReader(filename, GeometryFactory.Default))
                {
                    DbaseFileHeader header = sdr.DbaseHeader;
                    while (sdr.Read())
                    {
                        Feature feature = new Feature();
                        AttributesTable attributesTable = new AttributesTable();
                        string[] keys = new string[header.NumFields];
                        Geometry geometry = (Geometry)sdr.Geometry;
                        for (int i = 0; i < header.NumFields; i++)
                        {
                            DbaseFieldDescriptor fldDescriptor = header.Fields[i];
                            keys[i] = fldDescriptor.Name;
                            attributesTable.AddAttribute(fldDescriptor.Name, sdr.GetValue(i));
                        }
                        feature.Geometry = geometry;
                        feature.Attributes = attributesTable;
                        features.Add(feature);
                    }
                }
            }
            return features;
        }

        private void AddParishNames(List<Feature> features, string fieldname)
        {
            int featureNum = 0;
            int originalToSearch = 0;
            int lastSaved = int.MaxValue;
            int featuresCount = features.Count();
            foreach (Feature f in features)
            {
                featureNum++;
                if (f.Attributes[fieldname] == null || f.Attributes[fieldname].ToString().Length == 0)
                    Console.WriteLine("Parish is null?? type_code:" + f.Attributes["TYPE_CODE"]); // f.Attributes["TYPE_CODE"] = "FA"
                IPreparedGeometry geom = PreparedGeometryFactory.Prepare(f.Geometry);
                int count = 0;
                IEnumerable<OS50kGazetteer> toSearch = OS50k.Where(x => x.ParishName == null || x.ParishName.Length == 0);
                if (originalToSearch == 0)
                    originalToSearch = toSearch.Count();
                foreach(OS50kGazetteer os50k in toSearch)
                {
                    if (geom.Intersects(os50k.BufferedPoint))
                    {
                        os50k.ParishName = (string)f.Attributes[fieldname];
                        count++;
                    }
                }
                int left = toSearch.Count() - count;
                textBox1.AppendText("Set " + count + " entries for parish: " + f.Attributes[fieldname] + " number " + featureNum + " / " + featuresCount + " leaving " + left + " of " + originalToSearch + " to search\n");
                if (lastSaved - 2000 > left)
                {
                    lastSaved = left;
                    SaveOS50kGazetteer();
                }
                Application.DoEvents();
            }
        }

        private void ProcessOldCounties()
        {
            oldCountiesToCounties = new Dictionary<string, ISet<Tuple<string,string>>>();

            foreach (Feature f in oldEnglishCounties)
            {
                IPreparedGeometry geometry = PreparedGeometryFactory.Prepare(f.Geometry);
                foreach (OS50kGazetteer os50k in OS50k)
                {
                    if (geometry.Intersects(os50k.Point))
                    {
                        ISet<Tuple<string,string>> oldCounties = null;
                        string name = (string)f.Attributes["NAME"];
                        if (!oldCountiesToCounties.TryGetValue(name, out oldCounties))
                        {
                            oldCounties = new SortedSet<Tuple<string,string>>();
                            oldCountiesToCounties.Add(name, oldCounties);
                        }
                        oldCounties.Add(Tuple.Create(os50k.CountyCode, os50k.CountyName));
                    }
                }
            }
        }

        private void SaveCounties()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Maps\FTAnalyzer\counties.txt"))
            {
                foreach (string c in oldCountiesToCounties.Keys)
                {
                    ISet<Tuple<string, string>> value = oldCountiesToCounties[c];
                    foreach (Tuple<string, string> t in value)
                    {
                        writer.Write("UK_COUNTIES.Add(new County(\"");
                        writer.Write(c);
                        writer.Write("\", \"");
                        writer.Write(t.Item1);
                        writer.Write("\", \"");
                        writer.Write(t.Item2);
                        writer.WriteLine("\"));");
                    }
                }
            }
        }
            
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generateOutputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOS50kGazetteer();

            //scottishParishes = LoadFeatureList(@"C:\Maps\FTAnalyzer\CivilParish1930.shp");
            //AddParishNames(scottishParishes, "name");
            //englishParishes = LoadFeatureList(@"C:\Maps\FTAnalyzer\parish_region.shp");
            // strip parishes of blank filler area (FA) codes
            //englishParishes = englishParishes.Where(x => x.Attributes["TYPE_CODE"].ToString() != "FA").ToList<Feature>();
            //AddParishNames(englishParishes, "NAME");
            //SaveOS50kGazetteer();

            oldEnglishCounties = LoadFeatureList(@"C:\Maps\FTAnalyzer\Historic_Counties_of_England&Wales_Web.shp");
            ProcessOldCounties();
            SaveCounties();
            MessageBox.Show("Finished");
        }
    }
}
