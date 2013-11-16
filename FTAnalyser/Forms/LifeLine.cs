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
        private bool isloading;

        public LifeLine()
        {
            InitializeComponent();
            isloading = true;
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
            dgFacts.AutoGenerateColumns = false;
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = new SortableBindingList<Individual>(ft.AllIndividuals);
            dgIndividuals.Sort(dgIndividuals.Columns["BirthDate"], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns["SortedName"], ListSortDirection.Ascending);
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            isloading = false;
        }

        private void DatabaseHelper_GeoLocationUpdated(object location, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DatabaseHelper_GeoLocationUpdated(location, e)));
                return;
            }
            BuildMap();
        }

        private void SetupMap()
        {
            lifelines = new FeatureDataTable();
            lifelines.Columns.Add("MapLifeLine", typeof(MapLifeLine));
            lifelines.Columns.Add("StartPoint", typeof(bool));
            lifelines.Columns.Add("EndPoint", typeof(bool));
            lifelines.Columns.Add("Label", typeof(string));

            GeometryFeatureProvider lifelinesGFP = new GeometryFeatureProvider(lifelines);

            linesLayer = new VectorLayer("LifeLines");
            linesLayer.DataSource = lifelinesGFP;
            linesLayer.CoordinateTransformation = MapTransforms.Transform();
            linesLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle linestyle = new VectorStyle();
            linestyle.Line = new Pen(Color.Red, 2f);
            linestyle.Line.EndCap = LineCap.Triangle;
            linestyle.PointColor = new SolidBrush(Color.Green);
            linestyle.PointSize = 10; // for single fact individuals, start & end points
            linesLayer.Style = linestyle;
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
            style.Offset = new PointF(0, 25);
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
            if (!isloading)
                BuildMap();
        }

        private void BuildMap()
        {
            this.Cursor = Cursors.WaitCursor;
            lifelines.Clear();
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            foreach (DataGridViewRow row in dgIndividuals.SelectedRows)
            {
                Individual ind = row.DataBoundItem as Individual;
                if (ind.AllGeocodedFacts.Count > 0)
                {
                    displayFacts.AddRange(ind.AllGeocodedFacts);
                    MapLifeLine line = new MapLifeLine(ind);
                    FeatureDataRow fdr = line.AddFeatureDataRow(lifelines);
                }
            }
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            txtCount.Text = dgIndividuals.SelectedRows.Count + " Individual(s) selected, " + dgFacts.RowCount + " Geolocated fact(s) displayed";

            Envelope bbox = new Envelope();
            foreach (FeatureDataRow row in lifelines)
                foreach (Coordinate c in row.Geometry.Coordinates)
                    if (c != null)
                        bbox.ExpandToInclude(c);
            IMathTransform transform = linesLayer.CoordinateTransformation.MathTransform;
            Envelope expand;
            if (bbox.Centre == null)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
                expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
            mapBox1.Map.ZoomToBox(expand);
            expand.ExpandBy(mapBox1.Map.PixelSize * 40);
            mapBox1.Map.ZoomToBox(expand);
            if (mapBox1.Map.Zoom < mapBox1.Map.MinimumZoom)
                mapBox1.Map.Zoom = mapBox1.Map.MinimumZoom;
            if (mapBox1.Map.Zoom > mapBox1.Map.MaximumZoom)
                mapBox1.Map.Zoom = mapBox1.Map.MaximumZoom;
            mapBox1.Refresh();
            this.Cursor = Cursors.Default;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void dgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IDisplayFact fact = (IDisplayFact)dgFacts.CurrentRow.DataBoundItem;
            ft.OpenGeoLocations(fact.Location);
            this.Cursor = Cursors.Default;
        }

        private void addAllFamilyMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Individual ind = dgIndividuals.CurrentRow.DataBoundItem as Individual;
            isloading = true;
            foreach (Individual i in ft.GetFamily(ind))
                SelectIndividual(i);
            isloading = false;
            BuildMap();
        }

        private void SelectIndividual(Individual i)
        {
            DataGridViewRow row = dgIndividuals.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["IndividualID"].Value.ToString().Equals(i.IndividualID)).FirstOrDefault();
            if (row != null)
                dgIndividuals.Rows[row.Index].Selected = true;
        }

        private void SelectIndividuals(Func<Individual,List<Individual>> method)
        {
            this.Cursor = Cursors.WaitCursor;
            isloading = true; 
            Individual ind = dgIndividuals.CurrentRow.DataBoundItem as Individual;
            foreach (Individual i in method(ind))
                SelectIndividual(i);
            isloading = false;
            BuildMap();
            this.Cursor = Cursors.Default;
        }

        private void selectAllAncestorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetAncestors);
        }

        private void selectAllDescendantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetDescendants);
        }

        private void selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIndividuals(ft.GetAllRelations);
        }

        private void LifeLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            this.Dispose();
        }

        private void dgIndividuals_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Individual ind = (Individual)dgIndividuals.Rows[e.RowIndex].DataBoundItem;
                if (ind.GeoLocationCount == 0)
                    e.ToolTipText = ind.Name + " has no geolocated facts to show on map";
                else
                    e.ToolTipText = "Click to display " + ind.Name + "'s geolocated facts on the map. Right click to add their relatives";
            }
        }

        private void dgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                e.ToolTipText = "Double click to edit location.";
            }
        }
    }
}
