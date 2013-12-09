using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Rendering.Decoration.ScaleBar;
using SharpMap.Styles;

namespace FTAnalyzer.Forms
{
    public partial class LifeLine : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private MapHelper mh = MapHelper.Instance;
        private Color backgroundColour;
        private FeatureDataTable lifelines;
        private VectorLayer linesLayer;
        private LabelLayer labelLayer;
        private TearDropLayer points;
        private TearDropLayer selections;
        private bool isLoading;
        private bool isQuerying;

        public LifeLine()
        {
            InitializeComponent();
            isLoading = true;
            isQuerying = false;
            mnuMapStyle.Setup(linkLabel1, mapBox1);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            foreach (ToolStripItem item in mapZoomToolStrip.Items)
                item.Enabled = true;
            //mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mnuHideScaleBar.Checked = Properties.MappingSettings.Default.HideScaleBar;
            SetupMap();
            dgFacts.AutoGenerateColumns = false;
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = new SortableBindingList<Individual>(ft.AllIndividuals);
            dgIndividuals.Sort(dgIndividuals.Columns["BirthDate"], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns["SortedName"], ListSortDirection.Ascending);
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            int splitheight = (int)Application.UserAppDataRegistry.GetValue("Lifeline Facts Splitter Distance", -1);
            if (splitheight != -1)
                splitContainerFacts.SplitterDistance = this.Height - splitheight;
            splitContainerMap.SplitterDistance = (int)Application.UserAppDataRegistry.GetValue("Lifeline Map Splitter Distance", splitContainerMap.SplitterDistance);
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
            lifelines.Columns.Add("ViewPort", typeof(Envelope));

            GeometryFeatureProvider lifelinesGFP = new GeometryFeatureProvider(lifelines);

            linesLayer = new VectorLayer("LifeLines");
            linesLayer.DataSource = lifelinesGFP;
            
            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle linestyle = new VectorStyle();
            linestyle.Line = new Pen(Color.Green, 2f);
            linestyle.Line.EndCap = LineCap.NoAnchor;
            linesLayer.Style = linestyle;
            mapBox1.Map.Layers.Add(linesLayer);

            labelLayer = new LabelLayer("Label");
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

            points = new TearDropLayer("Points");
            mapBox1.Map.Layers.Add(points);
            selections = new TearDropLayer("Selections");
            mapBox1.Map.VariableLayers.Add(selections);

            mh.AddEnglishParishLayer(mapBox1.Map);
            mh.SetScaleBar(mapBox1);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void dgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (!isLoading)
                BuildMap();
        }

        private void BuildMap()
        {
            this.Cursor = Cursors.WaitCursor;
            lifelines.Clear();
            points.Clear();
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            foreach (DataGridViewRow row in dgIndividuals.SelectedRows)
            {
                Individual ind = row.DataBoundItem as Individual;
                if (ind.AllGeocodedFacts.Count > 0)
                {
                    displayFacts.AddRange(ind.AllGeocodedFacts);
                    MapLifeLine line = new MapLifeLine(ind);
                    line.AddFeatureDataRow(lifelines);
                    points.AddFeatureDataRows(ind);
                }
            }
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            txtCount.Text = dgIndividuals.SelectedRows.Count + " Individual(s) selected, " + dgFacts.RowCount + " Geolocated fact(s) displayed";

            Envelope expand = mh.GetExtents(lifelines);
            mapBox1.Map.ZoomToBox(expand);
            if (mapBox1.Map.Zoom < mapBox1.Map.MaximumZoom)
            {
                expand.ExpandBy(mapBox1.Map.PixelSize * 40);
                mapBox1.Map.ZoomToBox(expand);
            }
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.Cursor = Cursors.Default;
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
            isLoading = true;
            foreach (Individual i in ft.GetFamily(ind))
                SelectIndividual(i);
            isLoading = false;
            BuildMap();
        }

        private void SelectIndividual(Individual i)
        {
            DataGridViewRow row = dgIndividuals.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["IndividualID"].Value.ToString().Equals(i.IndividualID)).FirstOrDefault();
            if (row != null)
                dgIndividuals.Rows[row.Index].Selected = true;
        }

        private void SelectIndividuals(Func<Individual, List<Individual>> method)
        {
            this.Cursor = Cursors.WaitCursor;
            isLoading = true;
            Individual ind = dgIndividuals.CurrentRow.DataBoundItem as Individual;
            foreach (Individual i in method(ind))
                SelectIndividual(i);
            isLoading = false;
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

        private void LifeLine_Load(object sender, EventArgs e)
        {
            isLoading = false; // only turn off building map if completely done initializing
            if (dgIndividuals.RowCount > 0)
            {   // update map using first row as selected row
                BuildMap();
            }
            mh.CheckIfGeocodingNeeded(this);
        }

        private void hideLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideLabelsToolStripMenuItem.Checked)
                mapBox1.Map.Layers.Remove(labelLayer);
            else
                mapBox1.Map.Layers.Add(labelLayer);
            mapBox1.Refresh();
        }

        private void mnuHideScaleBar_Click(object sender, EventArgs e)
        {
            mh.mnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);
        }

        private void splitContainerFacts_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Lifeline Facts Splitter Distance", this.Height - splitter.SplitterDistance);
        }

        private void splitContainerMap_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Lifeline Map Splitter Distance", splitter.SplitterDistance);
        }

        private void dgFacts_SelectionChanged(object sender, EventArgs e)
        {
            if (!isQuerying)
                UpdateSelection();
        }

        private void UpdateSelection()
        {
            selections.AddSelections(dgFacts.SelectedRows);
            mapBox1.Refresh();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.Checked = true;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        private void mapBox1_ActiveToolChanged(SharpMap.Forms.MapBox.Tools tool)
        {
            if (mapBox1.ActiveTool != SharpMap.Forms.MapBox.Tools.QueryPoint)
                btnSelect.Checked = false;
        }

        private void mapBox1_MapQueried(FeatureDataTable data)
        {
            this.Cursor = Cursors.WaitCursor;
            isQuerying = true;
            dgFacts.ClearSelection();
            foreach (FeatureDataRow row in data)
            {
                if (row["DisplayFact"] != null && row["Colour"] == TearDropLayer.GREY)
                {
                    DisplayFact dispFact = (DisplayFact)row["DisplayFact"];
                    SelectFact(dispFact);
                }
            }
            isQuerying = false;
            this.Cursor = Cursors.Default;
        }

        private void SelectFact(DisplayFact dispFact)
        {
            foreach (DataGridViewRow row in dgFacts.Rows)
            {
                DisplayFact rowFact = (DisplayFact)row.DataBoundItem;
                if(rowFact.Equals(dispFact))
                    dgFacts.Rows[row.Index].Selected = true;
            }
        }

        private void mapBox1_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            string tooltip = string.Empty;
            Envelope infoPoint = new Envelope(worldPos.CoordinateValue);
            infoPoint.ExpandBy(mapBox1.Map.PixelSize * 30);
            foreach (Layer layer in mapBox1.Map.Layers)
            {
                if (layer is TearDropLayer)
                {
                    TearDropLayer tdl = (TearDropLayer)layer;
                    FeatureDataSet ds = new FeatureDataSet();
                    if (!tdl.DataSource.IsOpen)
                        tdl.DataSource.Open();
                    tdl.DataSource.ExecuteIntersectionQuery(infoPoint, ds);
                    tdl.DataSource.Close();
                    foreach (FeatureDataRow row in ds.Tables[0].Rows)
                    {
                        MapLocation line = (MapLocation)row["MapLocation"];
                        string colour = (string)row["Colour"];
                        if(colour == TearDropLayer.GREY)
                            tooltip += line.ToString() + "\n";
                    }
                }
            }
            if (!tooltip.Equals(mapTooltip.GetToolTip(mapBox1)))
                mapTooltip.SetToolTip(mapBox1, tooltip);
        }
    }
}

