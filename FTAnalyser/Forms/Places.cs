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
        private MapHelper mh = MapHelper.Instance;
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
            //mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(mapBox1_MapViewOnChange);
            mnuHideScaleBar.Checked = Properties.MappingSettings.Default.HideScaleBar;
            SetupMap();
            tvPlaces.Nodes.Clear();
            dgFacts.AutoGenerateColumns = false;
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            int splitheight = (int)Application.UserAppDataRegistry.GetValue("Places Facts Splitter Distance", -1);
            if (splitheight != -1)
                splitContainerFacts.SplitterDistance = this.Height - splitheight;
            splitContainerMap.SplitterDistance = (int)Application.UserAppDataRegistry.GetValue("Places Map Splitter Distance", splitContainerMap.SplitterDistance);
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
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            mh.SetScaleBar(mapBox1);
        }
        
        private void BuildMap()
        {
            if (isloading) return;
            this.Cursor = Cursors.WaitCursor;
            clusters.Clear();
            dgFacts.DataSource = null;
            List<IDisplayFact> displayFacts = new List<IDisplayFact>();
            List<Individual> list = new List<Individual>();
            List<Tuple<FactLocation, int>> locations = new List<Tuple<FactLocation, int>>();
            foreach (TreeNode node in tvPlaces.SelectedNodes)
            {
                Tuple<FactLocation, int> location = new Tuple<FactLocation, int>((FactLocation)node.Tag, node.Level);
                list.AddRange(ft.GetIndividualsAtLocation(location.Item1, location.Item2));
                locations.Add(location);
            }
            if (list.Count == 0)
            {
                this.Cursor = Cursors.Default;
                RefreshClusters();
                return;
            }
            int count = 0;
            progressbar.Visible = true;
            progressbar.Maximum = list.Count;
            foreach (Individual ind in list)
            {
                foreach (DisplayFact dispfact in ind.AllGeocodedFacts)
                {
                    foreach (Tuple<FactLocation, int> location in locations)
                    {
                        if (dispfact.Location.CompareTo(location.Item1, location.Item2) == 0)
                        {
                            displayFacts.Add(dispfact);
                            MapLocation loc = new MapLocation(ind, dispfact.Fact, dispfact.FactDate);
                            FeatureDataRow fdr = loc.AddFeatureDataRow(clusters.FactLocations);
                            break;
                        }
                    }
                }
                progressbar.Value = ++count;
                txtCount.Text = "Processed " + count + " Individuals from list of " + list.Count;
                Application.DoEvents();
            }
            progressbar.Visible = false;
            txtCount.Text = "Downloading map tiles and computing clusters for " + displayFacts.Count + " facts. Please wait";
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
            {
                expand = new Envelope(bbox.TopLeft(), bbox.BottomRight());
                expand.ExpandBy(bbox.Width * FamilyTree.SCALEBY);
            }
            mapBox1.Map.ZoomToBox(expand);
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            RefreshClusters();
            txtCount.Text = dgFacts.RowCount + " Geolocated fact(s) displayed";
            this.Cursor = Cursors.Default;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void dgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                IDisplayFact fact = (IDisplayFact)dgFacts.CurrentRow.DataBoundItem;
                ft.OpenGeoLocations(fact.Location);
                this.Cursor = Cursors.Default;
            }
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
            mh.CheckIfGeocodingNeeded(this);
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
            RefreshClusters();
        }

        public void RefreshClusters()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshClusters()));
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            clusters.Refresh();
            mapBox1.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void mapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshClusters();
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
                RefreshClusters();
            }
        }

        private void mnuHideScaleBar_Click(object sender, EventArgs e)
        {
            mh.mnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);    
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
            List<MapLocation> locations = new List<MapLocation>();
            foreach (FeatureDataRow row in data)
            {
                IList<FeatureDataRow> features = (List<FeatureDataRow>)row["Features"];
                foreach (FeatureDataRow feature in features)
                {
                    locations.Add((MapLocation)feature["MapLocation"]);
                }
            }
            MapIndividuals ind = new MapIndividuals(locations, "none", this);
            ind.Show();
            this.Cursor = Cursors.Default;
        }

        private void splitContainerFacts_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Places Facts Splitter Distance", this.Height - splitter.SplitterDistance);
        }

        private void splitContainerMap_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            Application.UserAppDataRegistry.SetValue("Places Map Splitter Distance", splitter.SplitterDistance);
        }
    }
}
