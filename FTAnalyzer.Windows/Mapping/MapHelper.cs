using FTAnalyzer.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.Properties;
using NetTopologySuite.Geometries;
using SharpMap;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Layers;
using SharpMap.Rendering;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Styles;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FTAnalyzer.Mapping
{
    public class MapHelper
    {
        static MapHelper instance;
        readonly FamilyTree ft = FamilyTree.Instance;

        MapHelper()
        {
        }

        public static MapHelper Instance
        {
            get
            {
                instance ??= new MapHelper();
                return instance;
            }
        }

        public static void SetScaleBar(MapBox mapBox1)
        {
            if (MappingSettings.Default.HideScaleBar)
            {
                if (mapBox1.Map.Decorations.Count > 0)
                    mapBox1.Map.Decorations.RemoveAt(0);
            }
            else
            {
                ScaleBar scalebar = new()
                {
                    BackgroundColor = Color.White,
                    RoundedEdges = true
                };
                mapBox1.Map.Decorations.Add(scalebar);
            }
            mapBox1.Refresh();
        }

        public static void MnuHideScaleBar_Click(ToolStripMenuItem mnuHideScaleBar, MapBox mapBox1)
        {
            MappingSettings.Default.HideScaleBar = mnuHideScaleBar.Checked;
            MappingSettings.Default.Save();
            SetScaleBar(mapBox1);
        }

        public void CheckIfGeocodingNeeded(Form form, IProgress<string> outputText)
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)));
            if (notsearched > 0 && !ft.Geocoding)
            {
                DialogResult res = MessageBox.Show($"You have {notsearched} places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    form.Cursor = Cursors.WaitCursor;
                    StartGeocoding(outputText);
                    //UIHelpers.ShowMessage("Sorry Google have changed the method of accessing API data - this feature is unavailable until access method been reprogrammed.");
                    form.Cursor = Cursors.Default;
                }
            }
        }

        public static void OpenGeoLocations(FactLocation location, IProgress<string> outputText)
        {
            try
            {
                GeocodeLocations? geoLocations = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f is GeocodeLocations locations)
                    {
                        f.BringToFront();
                        f.Focus();
                        geoLocations = locations;
                        break;
                    }
                }
                if (geoLocations is null)
                {
                    geoLocations = new(outputText);
                    geoLocations.Show();
                }
                // we now have opened form
                geoLocations.SelectLocation(location);
            }
            catch (Exception) { }
        }

        public void StartGeocoding(IProgress<string> outputText)
        {
            try
            {
                if (!ft.Geocoding) // don't geocode if another geocode session in progress
                {
                    GeocodeLocations geo = new(outputText);
                    MainForm.DisposeDuplicateForms(geo);
                    geo.Show();
                    geo.StartGoogleGeoCoding(false);
                    geo.BringToFront();
                    geo.Focus();
                }
            }
            catch (Exception e)
            {
                UIHelpers.ShowMessage($"A problem occurred starting geocoding the error was:\n{e.Message}");
            }
        }

        public void AddParishLayers(Map map)
        {
            VectorRenderer.SizeOfString = SizeOfString;  //fixes label width bug by defining an alternate sizing function

            string filename;

            filename = Path.Combine(MappingSettings.Default.CustomMapPath, "parish_region.shp");
            AddParishLayer(map, filename, "English", "NAME");

            filename = Path.Combine(MappingSettings.Default.CustomMapPath, "CivilParish1930.shp");
            AddParishLayer(map, filename, "Scottish", "name");
        }

        public static void AddParishLayer(Map map, string filename, string prefix, string labelField)
        {
            try
            {
                if (MappingSettings.Default.UseParishBoundaries)
                {
                    if (File.Exists(filename))
                    {
                        VectorLayer parishLayer = new(prefix + "ParishBoundaries")
                        {
                            DataSource = new ShapeFile(filename, true)
                        };
                        parishLayer.Style.Fill = null;
                        parishLayer.Style.Outline = new Pen(Color.Black, 2.0f);
                        parishLayer.Style.EnableOutline = true;
                        parishLayer.MinVisible = 500;
                        parishLayer.MaxVisible = 300000;
                        map.VariableLayers.Add(parishLayer);

                        LabelLayer parishLabelLayer = new(prefix + "ParishNames")
                        {
                            DataSource = new ShapeFile(filename, true),
                            LabelColumn = labelField,
                            TextRenderingHint = TextRenderingHint.AntiAlias,
                            SmoothingMode = SmoothingMode.AntiAlias
                        };

                        LabelStyle style = new()
                        {
                            ForeColor = Color.DarkRed,
                            Font = new(FontFamily.GenericSerif, 14, FontStyle.Bold),
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
            catch (Exception) { }
        }

        // alternate sizing function to fix bug in sharpmap v1.1
        SizeF SizeOfString(Graphics graphics, string text, Font font)
        {
            var s = TextRenderer.MeasureText(text, font);
            return new SizeF(s.Width + 1f, s.Height);
        }

        public static Envelope GetExtents(FeatureDataTable table)
        {
            Envelope bbox = new();
            Envelope empty = new();
            foreach (FeatureDataRow row in table)
            {
                foreach (Coordinate c in row.Geometry.Coordinates)
                {
                    if (c is not null)
                        bbox.ExpandToInclude(c);
                }
                var x = (Envelope)row["ViewPort"];
                Debug.WriteLine(x.ToString());
                if (x.MaxX == 0 && x.MaxY == 0 )
                    Debug.WriteLine("we have zeos");
                else
                    bbox.ExpandToInclude(x);
            }
            Envelope expand;
            if (bbox.Centre is null)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
            {
                expand = new Envelope(bbox.TopLeft(), bbox.BottomRight());
                expand.ExpandBy(bbox.Width * 0.1d);
            }
            return expand;
        }

        public static List<MapLocation> AllMapLocations
        {
            get
            {
                List<MapLocation> result = new();
                foreach (Individual ind in FamilyTree.Instance.AllIndividuals)
                {
                    foreach (Fact f in ind.AllFacts)
                        if (f.Location.IsGeoCoded(false))
                            result.Add(new MapLocation(ind, f, f.FactDate));
                }
                return result;
            }
        }

        public static List<MapLocation> YearMapLocations(FactDate when, int limit)
        {
            List<MapLocation> result = new();
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
