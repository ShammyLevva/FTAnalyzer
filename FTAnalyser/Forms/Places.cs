using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Rendering.Decoration.ScaleBar;

namespace FTAnalyzer.Forms
{
    public partial class Places : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Color backgroundColour;
        private ClusterLayer clusters;
        private bool isloading;
        private FactLocation currentLocation;
        private int currentLevel;

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
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(mapBox1_MapViewOnChange);
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
        
        private void BuildMap()
        {
            this.Cursor = Cursors.WaitCursor;
            clusters.Clear();
            dgFacts.DataSource = null;
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            FactLocation location = tvPlaces.SelectedNode.Tag as FactLocation;
            int level = tvPlaces.SelectedNode.Level;
            if (isloading || location == null || !location.IsGeoCoded(false) || (location == currentLocation && level == currentLevel))
            {
                this.Cursor = Cursors.Default;
                return;
            }
            currentLevel = level;
            currentLocation = location;
            List<Individual> list = new List<Individual>(ft.GetIndividualsAtLocation(location, level));
            int count = 0;
            progressbar.Visible = true;
            progressbar.Maximum = list.Count;
            foreach (Individual ind in list)
            {
                foreach (DisplayFact dispfact in ind.AllGeocodedFacts)
                {
                    if (dispfact.Location.CompareTo(location, level) == 0)
                    {
                        displayFacts.Add(dispfact);
                        MapLocation loc = new MapLocation(ind, dispfact.Fact, dispfact.FactDate);
                        FeatureDataRow fdr = loc.AddFeatureDataRow(clusters.FactLocations);
                    }
                }
                progressbar.Value = ++count;
                txtCount.Text = "Processed " + count + " Individuals from list of " + list.Count;
                Application.DoEvents();
            }
            progressbar.Visible = false;
            txtCount.Text = "Loading map tiles and computing clusters for " + displayFacts.Count + " facts. Please wait";
            Application.DoEvents();
            dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);
            Envelope bbox = new Envelope();
            foreach (FeatureDataRow row in clusters.FactLocations)
                foreach (Coordinate c in row.Geometry.Coordinates)
                    if (c != null)
                        bbox.ExpandToInclude(c);
            Envelope expand;
            if (bbox.Centre == null)
                expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
            else
                expand = new Envelope(bbox.TopLeft(), bbox.BottomRight());
            expand.ExpandBy(mapBox1.Map.PixelSize);
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            RefreshPlaces();
            txtCount.Text = dgFacts.RowCount + " Geolocated fact(s) displayed";
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

        private void Places_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            tvPlaces.Nodes.Clear();
            this.Dispose();
        }

        private void dgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                e.ToolTipText = "Double click to edit location.";
            }
        }

        private void Places_Load(object sender, EventArgs e)
        {
            TreeNode[] nodes = ft.GetAllLocationsTreeNodes(tvPlaces.Font, false);
            tvPlaces.Nodes.AddRange(nodes); 
            isloading = false; // only turn off building map if completely done initializing
            if (tvPlaces.Nodes.Count > 0)
            {   // update map using first node as selected node
                tvPlaces.SelectedNode = tvPlaces.Nodes[0];
            }
            this.Cursor = Cursors.Default;
        }

        private bool preventExpand = false;
        private bool settingIcon = false;

        private void tvPlaces_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvPlaces.SelectedNode != e.Node)
                tvPlaces.SelectedNode = e.Node;
            if (!settingIcon)
                BuildMap();
            if (tvPlaces.SelectedImageIndex != e.Node.ImageIndex)
            {
                settingIcon = true;
                tvPlaces.SelectedImageIndex = e.Node.ImageIndex;
                settingIcon = false;
            }
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

        private void mapBox1_MapViewOnChange()
        {
            clusters.Refresh();
        }

        private void mapBox1_MapZoomChanged(double zoom)
        {
            RefreshPlaces();
        }

        public void RefreshPlaces()
        {
            clusters.Refresh();
            mapBox1.Refresh();
        }

        private void mapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshPlaces();
        }

        private void mapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool zoomed = false;
            if (e.Button == MouseButtons.Left && mapBox1.Map.Zoom > mapBox1.Map.MinimumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom *= 1d / 1.5d;
            }
            else if (e.Button == MouseButtons.Right && mapBox1.Map.Zoom < mapBox1.Map.MaximumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom *= 1.5d;
            }
            if (zoomed)
            {
                Coordinate p = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y));
                mapBox1.Map.Center.X = p.X;
                mapBox1.Map.Center.Y = p.Y;
                RefreshPlaces();
            }
        }
    }
}
