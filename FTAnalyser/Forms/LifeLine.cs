using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using SharpMap.Styles;
using SharpMap.Data;
using FTAnalyzer.Mapping;
using System.IO;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using SharpMap.Rendering.Decoration.ScaleBar;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems.Transformations;
using System.Diagnostics;

namespace FTAnalyzer.Forms
{
    public partial class LifeLine : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Color backgroundColour;
        private FeatureDataTable lifelines;
        private VectorLayer linesLayer;
        private LabelLayer labelLayer;
        
        public LifeLine()
        {
            InitializeComponent();
            mnuMapStyle.Setup(linkLabel1, mapBox1);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            foreach (ToolStripItem item in mapZoomToolStrip.Items)
                item.Enabled = true;
            mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            SetupMap();
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = new SortableBindingList<Individual>(ft.AllIndividuals.Where(i => i.AllGeocodedFacts.Count > 0));
            dgIndividuals.Sort(dgIndividuals.Columns["BirthDate"], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns["SortedName"], ListSortDirection.Ascending);
        }

        private void SetupMap()
        {
            lifelines = new FeatureDataTable();
            lifelines.Columns.Add("MapLifeLine", typeof(MapLifeLine));
            lifelines.Columns.Add("Label", typeof(string));

            GeometryFeatureProvider lifelinesGFP = new GeometryFeatureProvider(lifelines);

            linesLayer = new VectorLayer("LifeLines");
            linesLayer.DataSource = lifelinesGFP;
            linesLayer.CoordinateTransformation = MapTransforms.Transform();
            linesLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle line = new VectorStyle();
            line.Line = new Pen(Color.Red);
            line.PointColor = new SolidBrush(Color.Red);
            line.PointSize = 20; // for single fact individuals
            mapBox1.Map.Layers.Add(linesLayer);

            labelLayer = new LabelLayer("Label");
            labelLayer.CoordinateTransformation = MapTransforms.Transform();
            labelLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();
            labelLayer.DataSource = lifelinesGFP;
            labelLayer.Enabled = true;
            //Specifiy field that contains the label string.
            labelLayer.LabelColumn = "Label";
            labelLayer.TextRenderingHint = TextRenderingHint.AntiAlias;
            labelLayer.SmoothingMode = SmoothingMode.AntiAlias;
            LabelStyle style = new LabelStyle();
            style.ForeColor = Color.Black;
            style.Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold);
            style.HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center;
            style.VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom;
            style.CollisionDetection = true;
            style.Offset = new PointF(2, 22);
            style.Halo = new Pen(Color.Yellow, 3);
            labelLayer.Style = style;
            mapBox1.Map.Layers.Add(labelLayer);

            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            AddScaleBar();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void AddScaleBar()
        {
            ScaleBar scalebar = new ScaleBar();
            scalebar.BackgroundColor = Color.White;
            scalebar.RoundedEdges = true;
            mapBox1.Map.Decorations.Add(scalebar);
            mapBox1.Refresh();
        }

        private void RemoveScaleBar()
        {
            mapBox1.Map.Decorations.RemoveAt(0);
            mapBox1.Refresh();
        }

        private void dgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            BuildMap();
        }

        private void BuildMap()
        {
            lifelines.Clear();
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            Envelope bbox = new Envelope();
            foreach (DataGridViewRow row in dgIndividuals.SelectedRows)
            {
                Individual ind = row.DataBoundItem as Individual;
                displayFacts.AddRange(ind.AllGeocodedFacts);
                MapLifeLine line = new MapLifeLine(ind);
                FeatureDataRow fdr = line.AddFeatureDataRow(lifelines);
                foreach(Coordinate c in fdr.Geometry.Coordinates)
                    bbox.ExpandToInclude(c);
            }
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            IMathTransform transform = linesLayer.CoordinateTransformation.MathTransform;
            Envelope expand;
            if (bbox.Centre == null)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
                expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
            mapBox1.Map.ZoomToBox(expand);
            expand.ExpandBy(mapBox1.Map.PixelSize * 20);
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Refresh();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
