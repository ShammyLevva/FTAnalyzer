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
    public partial class Places : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Color backgroundColour;
        private ClusterLayer clusters;
        private bool isloading;

        public Places()
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
            tvPlaces.Nodes.Clear();
            dgFacts.AutoGenerateColumns = false;
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
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
            clusters = new ClusterLayer(mapBox1.Map);
            GeocodeLocations.AddEnglishParishLayer(mapBox1.Map);
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
            clusters.Clear();
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            FactLocation location = tvPlaces.SelectedNode.Tag as FactLocation;
            if (location == null)
                return;
            foreach (Individual ind in ft.GetIndividualsAtLocation(location, tvPlaces.SelectedNode.Level))
            {
                foreach(Fact fact in ind.AllFacts)
                {
                    if (fact.Location.IsGeoCoded(false) && fact.Location.Matches(location.ToString(), tvPlaces.SelectedNode.Level))
                    {
                        displayFacts.Add(new DisplayFact(ind, fact));
                        MapLocation loc = new MapLocation(ind, fact, fact.FactDate);
                        FeatureDataRow fdr = loc.AddFeatureDataRow(clusters.FactLocations);
                    }
                }
            }
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            txtCount.Text = dgFacts.RowCount + " Geolocated fact(s) displayed";

            Envelope bbox = new Envelope();
            foreach (FeatureDataRow row in clusters.FactLocations)
                foreach (Coordinate c in row.Geometry.Coordinates)
                    if (c != null)
                        bbox.ExpandToInclude(c);
            IMathTransform transform = clusters.MathTransform;
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

        private void LifeLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            this.Dispose();
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
            // add nodes after constructor so that bold issues don't interfere
            tvPlaces.Nodes.AddRange(ft.GetAllLocationsTreeNodes(tvPlaces.Font));
            isloading = false; // only turn off building map if completely done initializing
            if (tvPlaces.Nodes.Count > 0)
            {   // update map using first node as selected node
                BuildMap();
            }
            this.Cursor = Cursors.Default;
        }
        
        private bool preventExpand;

        private void tvPlaces_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvPlaces.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void tvPlaces_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tvPlaces.SelectedNode != e.Node)
                tvPlaces.SelectedNode = e.Node;
            BuildMap();
        }

        private void tvPlaces_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (preventExpand && e.Action == TreeViewAction.Collapse);
        }

        private void tvPlaces_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (preventExpand && e.Action == TreeViewAction.Expand);
        }

        private void tvPlaces_MouseDown(object sender, MouseEventArgs e)
        {
            preventExpand = e.Clicks > 1;
        }

        private void tvPlaces_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor; ;
            FactLocation location = e.Node.Tag as FactLocation;
            if (location != null)
            {
                Forms.People frmInd = new Forms.People();
                frmInd.SetLocation(location, e.Node.Level);
                MainForm.DisposeDuplicateForms(frmInd);
                frmInd.Show();
            }
            Cursor = Cursors.Default;
        }
    }
}
