using GeoAPI.Geometries;
using GeoAPI.Geometries.Prepared;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using NetTopologySuite.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OSGazetteerProcessor
{
    public partial class Form1 : Form
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(OSGazetteerProcessor.Form1));

        private IList<OS50kGazetteer> OS50k;
        private List<Feature> englishParishes;
        private List<Feature> scottishParishes;

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
                string filename = Path.Combine(startPath, @"Resources\50kgaz2014.txt");
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
            using (StreamWriter sw = new StreamWriter(@"Resources\50kgaz2014-output.txt")) {
                foreach (OS50kGazetteer os50k in OS50k)
                {
                    sw.WriteLine(os50k.ToString());
                }
            }
        }

        public List<Feature> LoadParishBoundaries(string filename)
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
            foreach (Feature f in features)
            {
                IPreparedGeometry geom = PreparedGeometryFactory.Prepare(f.Geometry);
                int count = 0;
                foreach (OS50kGazetteer os50k in OS50k.Where(x => x.ParishName == null))
                {
                    Coordinate c = new Coordinate(os50k.Longitude, os50k.Latitude);
                    c = MapTransforms.TransformCoordinate(c);
                    IPoint p = GeometryFactory.Default.CreatePoint(c);
                    if (geom.Intersects(p))
                    {
                        os50k.ParishName = (string)f.Attributes[fieldname];
                        count++;
                    }
                }
                featureNum++;
                textBox1.AppendText("Set " + count + " entries for parish: " + f.Attributes[fieldname] + " number " + featureNum + " / " + features.Count + "\n");
                Application.DoEvents();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generateOutputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOS50kGazetteer();
            englishParishes = LoadParishBoundaries(@"C:\Maps\FTAnalyzer\parish_region.shp");
            scottishParishes = LoadParishBoundaries(@"C:\Maps\FTAnalyzer\CivilParish1930.shp");
            AddParishNames(scottishParishes, "name");
            AddParishNames(englishParishes, "NAME");
            SaveOS50kGazetteer();
        }
    }
}
