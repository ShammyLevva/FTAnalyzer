using FTAnalyzer.Forms;
using GeoAPI.Geometries;
using SharpMap;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Layers;
using SharpMap.Rendering;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Mapping
{
    public class MapHelper
    {
        private static MapHelper instance;
        private FamilyTree ft = FamilyTree.Instance;

        private MapHelper()
        {
        }

        public static MapHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapHelper();
                }
                return instance;
            }
        }

        public void SetScaleBar(MapBox mapBox1)
        {
            if (Properties.MappingSettings.Default.HideScaleBar)
            {
                if (mapBox1.Map.Decorations.Count > 0)
                    mapBox1.Map.Decorations.RemoveAt(0);
            }
            else
            {
                ScaleBar scalebar = new ScaleBar
                {
                    BackgroundColor = Color.White,
                    RoundedEdges = true
                };
                mapBox1.Map.Decorations.Add(scalebar);
            }
            mapBox1.Refresh();
        }

        public void MnuHideScaleBar_Click(ToolStripMenuItem mnuHideScaleBar, MapBox mapBox1)
        {
            Properties.MappingSettings.Default.HideScaleBar = mnuHideScaleBar.Checked;
            Properties.MappingSettings.Default.Save();
            SetScaleBar(mapBox1);
        }

        public void CheckIfGeocodingNeeded(Form form, IProgress<string> outputText)
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)));
            if (notsearched > 0 && !ft.Geocoding)
            {
                DialogResult res = MessageBox.Show("You have " + notsearched + " places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    form.Cursor = Cursors.WaitCursor;
                    StartGeocoding(outputText);
                    form.Cursor = Cursors.Default;
                }
            }
        }

        public void OpenGeoLocations(FactLocation location, IProgress<string> outputText)
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
                geoLocations = new GeocodeLocations(outputText);
                geoLocations.Show();
            }
            // we now have opened form
            geoLocations.SelectLocation(location);
        }

        public void StartGeocoding(IProgress<string> outputText)
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                GeocodeLocations geo = new GeocodeLocations(outputText);
                MainForm.DisposeDuplicateForms(geo);
                geo.Show();
                geo.StartGoogleGeoCoding(false);
                geo.BringToFront();
                geo.Focus();
            }
        }

        public void AddParishLayers(Map map)
        {
            VectorRenderer.SizeOfString = SizeOfString;  //fixes label width bug by defining an alternate sizing function

            string filename;

            filename = Path.Combine(Properties.MappingSettings.Default.CustomMapPath, "parish_region.shp");
            AddParishLayer(map, filename, "English", "NAME");

            filename = Path.Combine(Properties.MappingSettings.Default.CustomMapPath, "CivilParish1930.shp");
            AddParishLayer(map, filename, "Scottish", "name");
        }

        public void AddParishLayer(Map map, string filename, string prefix, string labelField)
        {
            if (Properties.MappingSettings.Default.UseParishBoundaries)
            {
                if (File.Exists(filename))
                {
                    VectorLayer parishLayer = new VectorLayer(prefix + "ParishBoundaries")
                    {
                        DataSource = new ShapeFile(filename, true)
                    };
                    parishLayer.Style.Fill = null;
                    parishLayer.Style.Outline = new Pen(Color.Black, 2.0f);
                    parishLayer.Style.EnableOutline = true;
                    parishLayer.MinVisible = 500;
                    parishLayer.MaxVisible = 300000;
                    map.VariableLayers.Add(parishLayer);

                    LabelLayer parishLabelLayer = new LabelLayer(prefix + "ParishNames")
                    {
                        DataSource = new ShapeFile(filename, true),
                        LabelColumn = labelField,
                        TextRenderingHint = TextRenderingHint.AntiAlias,
                        SmoothingMode = SmoothingMode.AntiAlias
                    };

                    LabelStyle style = new LabelStyle
                    {
                        ForeColor = Color.DarkRed,
                        Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold),
                        HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center,
                        VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Middle,
                        CollisionDetection = true,
                        CollisionBuffer = new SizeF(4f, 4f)
                    };

                    parishLabelLayer.Style = style;
                    parishLabelLayer.LabelFilter = LabelCollisionDetection.ThoroughCollisionDetection;
                    parishLabelLayer.Style.CollisionDetection = true; // set twice to fix bug 
                    parishLabelLayer.MinVisible = 500;
                    parishLabelLayer.MaxVisible = 100000;
                    map.Layers.Add(parishLabelLayer);
                }
            }
        }

        // alternate sizing function to fix bug in sharpmap v1.1
        private SizeF SizeOfString(Graphics graphics, string text, Font font)
        {
            var s = TextRenderer.MeasureText(text, font);
            return new SizeF(s.Width + 1f, s.Height);
        }

        public Envelope GetExtents(FeatureDataTable table)
        {
            Envelope bbox = new Envelope();
            Envelope empty = new Envelope();
            foreach (FeatureDataRow row in table)
            {
                foreach (Coordinate c in row.Geometry.Coordinates)
                {
                    if (c != null)
                        bbox.ExpandToInclude(c);
                }
                var x = (Envelope)row["ViewPort"];
                Console.WriteLine(x.ToString());
                if (x.MaxX == 0 && x.MaxY == 0 )
                    Console.WriteLine("we have zeos");
                else
                    bbox.ExpandToInclude(x);
            }
            Envelope expand;
            if (bbox.Centre == null)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
            {
                expand = new Envelope(bbox.TopLeft(), bbox.BottomRight());
                expand.ExpandBy(bbox.Width * 0.1d);
            }
            return expand;
        }

        public List<MapLocation> AllMapLocations
        {
            get
            {
                List<MapLocation> result = new List<MapLocation>();
                foreach (Individual ind in FamilyTree.Instance.AllIndividuals)
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
            foreach (Individual ind in FamilyTree.Instance.AllIndividuals)
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
    }
}
