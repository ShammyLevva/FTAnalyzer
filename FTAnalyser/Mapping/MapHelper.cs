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
                ScaleBar scalebar = new ScaleBar();
                scalebar.BackgroundColor = Color.White;
                scalebar.RoundedEdges = true;
                mapBox1.Map.Decorations.Add(scalebar);
            }
            mapBox1.Refresh();
        }

        public void mnuHideScaleBar_Click(ToolStripMenuItem mnuHideScaleBar, MapBox mapBox1)
        {
            Properties.MappingSettings.Default.HideScaleBar = mnuHideScaleBar.Checked;
            Properties.MappingSettings.Default.Save();
            SetScaleBar(mapBox1);
        }

        public void CheckIfGeocodingNeeded(Form form)
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)));
            if (notsearched > 0 && !ft.Geocoding)
            {
                DialogResult res = MessageBox.Show("You have " + notsearched + " places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    form.Cursor = Cursors.WaitCursor;
                    StartGeocoding();
                    form.Cursor = Cursors.Default;
                }
            }
        }

        public void StartGeocoding()
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                GeocodeLocations geo = new GeocodeLocations();
                MainForm.DisposeDuplicateForms(geo);
                geo.Show();
                geo.StartGeoCoding(false);
                geo.BringToFront();
                geo.Focus();
            }
        }

        public void AddParishLayers(Map map)
        {
            AddEnglishParishLayer(map);
        }

        public void AddEnglishParishLayer(Map map)
        {
            if (Properties.MappingSettings.Default.UseParishBoundaries)
            {
                string filename = Path.Combine(Properties.MappingSettings.Default.CustomMapPath, "parish_region.shp");
                if (File.Exists(filename))
                {
                    VectorLayer parishLayer = new VectorLayer("ParishBoundaries");
                    parishLayer.DataSource = new ShapeFile(filename, true);
                    parishLayer.Style.Fill = null;
                    parishLayer.Style.Outline = new Pen(Color.Black, 2.0f);
                    parishLayer.Style.EnableOutline = true;
                    parishLayer.MinVisible = 500;
                    parishLayer.MaxVisible = 300000;
                    map.VariableLayers.Add(parishLayer);

                    LabelLayer parishLabelLayer = new LabelLayer("ParishNames");
                    parishLabelLayer.DataSource = new ShapeFile(filename, true);
                    parishLabelLayer.LabelColumn = "NAME";
                    parishLabelLayer.TextRenderingHint = TextRenderingHint.AntiAlias;
                    parishLabelLayer.SmoothingMode = SmoothingMode.AntiAlias;

                    LabelStyle style = new LabelStyle();
                    style.ForeColor = Color.DarkRed;
                    style.Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold);
                    style.HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center;
                    style.VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Middle;
                    style.CollisionDetection = true;
                    style.CollisionBuffer = new SizeF(4f, 4f);

                    parishLabelLayer.Style = style;
                    parishLabelLayer.LabelFilter = LabelCollisionDetection.ThoroughCollisionDetection;
                    parishLabelLayer.Style.CollisionDetection = true; // set twice to fix bug 
                    parishLabelLayer.MinVisible = 500;
                    parishLabelLayer.MaxVisible = 100000;
                    map.Layers.Add(parishLabelLayer);
                }
            }
        }

        public Envelope GetExtents(FeatureDataTable table)
        {
            Envelope bbox = new Envelope();
            foreach (FeatureDataRow row in table)
            {
                foreach (Coordinate c in row.Geometry.Coordinates)
                {
                    if (c != null)
                        bbox.ExpandToInclude(c);
                }
                bbox.ExpandToInclude((Envelope)row["ViewPort"]);
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
    }
}
