using FTAnalyzer.Forms.Controls;
using FTAnalyzer.Mapping;
using FTAnalyzer.Properties;
using FTAnalyzer.Shared.Utilities;
using FTAnalyzer.Utilities;
using Microsoft.Win32;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using SharpMap.Layers;

namespace FTAnalyzer.Forms
{
    public partial class Places : Form
    {
        readonly FamilyTree ft = FamilyTree.Instance;
        readonly MapHelper mh = MapHelper.Instance;
        ClusterLayer clusters;
        bool isloading;
        readonly IProgress<string> outputText;
        CancellationTokenSource? buildMapCts;

        public Places(IProgress<string> outputText)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            isloading = true;
            this.outputText = outputText;
            mnuMapStyle.Setup(linkLabel1, mapBox1, tbOpacity);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            foreach (ToolStripItem item in mapZoomToolStrip.Items)
                item.Enabled = true;
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(MapBox1_MapViewOnChange);
            mnuHideScaleBar.Checked = MappingSettings.Default.HideScaleBar;
            SetupMap();
            dgFacts.AutoGenerateColumns = false;
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            int splitheight = RegistrySettings.GetIntRegistryValue("Places Facts Splitter Distance",-1);
            if (splitheight != -1)
                splitContainerFacts.SplitterDistance = Height - splitheight;
            splitContainerMap.SplitterDistance = RegistrySettings.GetIntRegistryValue("Places Map Splitter Distance", splitContainerMap.SplitterDistance);
        }

        void DatabaseHelper_GeoLocationUpdated(object? location, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DatabaseHelper_GeoLocationUpdated(location, e)));
                return;
            }
            _ = BuildMapAsync();
        }

        void SetupMap()
        {
            clusters = new ClusterLayer(mapBox1.Map);
            MapHelper.AddParishLayers(mapBox1.Map);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            MapHelper.SetScaleBar(mapBox1);
        }

        async Task BuildMapAsync()
        {
            if (isloading) return;
            // cancel any previous in-flight build and create a new token
            buildMapCts?.Cancel();
            buildMapCts = new CancellationTokenSource();
            CancellationToken token = buildMapCts.Token;
            Cursor = Cursors.WaitCursor;
            try
            {
                clusters.Clear();
                dgFacts.DataSource = null;
                List<IDisplayFact> displayFacts = [];
                List<Individual> list = [];
                List<Tuple<FactLocation, int>> locations = [];
                foreach (TreeNode node in tvPlaces.SelectedNodes)
                {
                    FactLocation? loc = (FactLocation?)node.Tag;
                    if (loc is not null)
                    {
                        Tuple<FactLocation, int> location = new(loc, node.Level);
                        list.AddUnique(ft.GetIndividualsAtLocation(location.Item1, location.Item2));
                        locations.Add(location);
                    }
                }
                if (list.Count == 0)
                {
                    Cursor = Cursors.Default;
                    RefreshClusters();
                    return;
                }

                pbPlaces.Visible = true;
                pbPlaces.Minimum = 0;
                pbPlaces.Maximum = list.Count;
                IProgress<(int processed, int total)> progress = new Progress<(int processed, int total)>(p =>
                {
                    int processed = p.processed;
                    int total = p.total;
                    if (processed < pbPlaces.Minimum)
                        processed = pbPlaces.Minimum;
                    if (processed > pbPlaces.Maximum)
                        processed = pbPlaces.Maximum;
                    pbPlaces.Value = processed;
                    if (processed % 10 == 0 || processed == total)
                        txtCount.Text = $"Processed {processed} Individuals from list of {total}";
                });

                // run the expensive per-individual work on a background thread
                await Task.Run(() =>
                {
                    int count = 0;
                    foreach (Individual ind in list)
                    {
                        if (token.IsCancellationRequested)
                            return; // stop early if a newer build was requested
                        foreach (DisplayFact dispfact in ind.AllGeocodedFacts.Cast<DisplayFact>())
                        {
                            foreach (Tuple<FactLocation, int> location in locations)
                            {
                                if (dispfact.Location.CompareTo(location.Item1, location.Item2) == 0)
                                {
                                    displayFacts.Add(dispfact);
                                    MapLocation loc = new(ind, dispfact.Fact, dispfact.FactDate);
                                    loc.AddFeatureDataRow(clusters.FactLocations);
                                    break;
                                }
                            }
                        }
                        count++;
                        progress.Report((count, list.Count));
                    }
                }, token);

                pbPlaces.Visible = false;
                if (!token.IsCancellationRequested)
                {
                    txtCount.Text = $"Downloading map tiles and computing clusters for {displayFacts.Count} facts. Please wait";
                    dgFacts.DataSource = new SortableBindingList<IDisplayFact>(displayFacts);

                    Envelope expand = MapHelper.GetExtents(clusters.FactLocations);
                    mapBox1.Map.ZoomToBox(expand);
                    mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
                    RefreshClusters();
                    txtCount.Text = $"{dgFacts.RowCount} Geolocated fact(s) displayed";
                }
            }
            catch (OperationCanceledException)
            {
                // swallow cancellations – a newer BuildMapAsync will take over
            }
            catch (Exception)
            { }
            Cursor = Cursors.Default;
        }

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SpecialMethods.VisitWebsite(e.Link?.LinkData?.ToString() ?? string.Empty);

        void DgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Cursor = Cursors.WaitCursor;
                IDisplayFact? fact = (IDisplayFact?)dgFacts.CurrentRow.DataBoundItem;
                if (fact is not null)
                    MapHelper.OpenGeoLocations(fact.Location, outputText);
                Cursor = Cursors.Default;
            }
        }

        void Places_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // cancel any in-flight map building triggered by this form
                buildMapCts?.Cancel();
                buildMapCts = null;
                DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
                tvPlaces.Nodes.Clear();
                Dispose();
            }
            catch
            (Exception)
            { }
        }

        void DgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                e.ToolTipText = "Double click to edit location.";
        }

		async void Places_Load(object sender, EventArgs e)
		{
            try
            {
				TreeNode[] nodes = await TreeViewHandler.Instance.BuildAllLocationsTreeNodesAsync(false, pbPlaces);
                tvPlaces.Nodes.AddRange(nodes);
                int Width = RegistrySettings.GetIntRegistryValue("Places size - width", this.Width);
                int Height = RegistrySettings.GetIntRegistryValue("Places size - height", this.Height);
                int Top = RegistrySettings.GetIntRegistryValue("Places position - top", this.Top);
                int Left = RegistrySettings.GetIntRegistryValue("Places position - left", this.Left);
                this.Width = Width;
                this.Height = Height;
                this.Top = Top;
                this.Left = Left;
                isloading = false; // only turn off building map if completely done initializing
                if (tvPlaces.Nodes.Count > 0)
                {   // update map using first node as selected node
                    tvPlaces.SelectedNode = tvPlaces.Nodes[0];
                }
                mh.CheckIfGeocodingNeeded(this, outputText);
                Cursor = Cursors.Default;
                SpecialMethods.SetFonts(this);
            }
            catch (Exception) { }
        }

        async void TvPlaces_AfterSelect(object sender, TreeViewEventArgs e) => await BuildMapAsync();

        void TvPlaces_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                FactLocation? location = e.Node.Tag as FactLocation;
                if (location is not null)
                {
                    People frmInd = new();
                    frmInd.SetLocation(location, e.Node.Level);
                    MainForm.DisposeDuplicateForms(frmInd);
                    frmInd.Show();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception) { }
        }

        void MapBox1_MapViewOnChange() => clusters.Refresh();

        void MapBox1_MapZoomChanged(double zoom) => RefreshClusters();

        public void RefreshClusters()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RefreshClusters()));
                return;
            }
            Cursor = Cursors.WaitCursor;
            clusters.Refresh();
            RefreshMap();
            Cursor = Cursors.Default;
        }

        void RefreshMap()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RefreshMap()));
                return;
            }
            SetOpacity();
            mapBox1.Refresh();
        }

        void MapBox1_MapCenterChanged(Coordinate center) => RefreshClusters();

        void MapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
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

        void MnuHideScaleBar_Click(object sender, EventArgs e) => MapHelper.MnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);

        void BtnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.Checked = true;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        void MapBox1_ActiveToolChanged(SharpMap.Forms.MapBox.Tools tool)
        {
            if (mapBox1.ActiveTool != SharpMap.Forms.MapBox.Tools.QueryPoint)
                btnSelect.Checked = false;
        }

        void MapBox1_MapQueried(FeatureDataTable data)
        {
            Cursor = Cursors.WaitCursor;
            List<MapLocation> locations = [];
            foreach (FeatureDataRow row in data)
            {
                IList<FeatureDataRow> features = (List<FeatureDataRow>)row["Features"];
                foreach (FeatureDataRow feature in features)
                    locations.Add((MapLocation)feature["MapLocation"]);
            }
            MapIndividuals ind = new(locations, null, this);
            ind.Show();
            Cursor = Cursors.Default;

        }

        void SplitContainerFacts_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            RegistrySettings.SetRegistryValue("Places Facts Splitter Distance", Height - splitter.SplitterDistance, RegistryValueKind.String);
        }

        void SplitContainerMap_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SplitContainer splitter = (SplitContainer)sender;
            RegistrySettings.SetRegistryValue("Places Map Splitter Distance", splitter.SplitterDistance, RegistryValueKind.String);
        }

        void ResetFormDefaultSizeAndPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isloading = true;
            Height = 628;
            Width = 1129;
            Top = 50;
            Left = 50;
            isloading = false;
            SavePosition();
        }

        void Places_Move(object sender, EventArgs e) => SavePosition();

        void Places_Resize(object sender, EventArgs e) => SavePosition();

        void SavePosition()
        {
            if (!isloading && WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                RegistrySettings.SetRegistryValue("Places size - width", Width, RegistryValueKind.DWord);
                RegistrySettings.SetRegistryValue("Places size - height", Height, RegistryValueKind.DWord);
                RegistrySettings.SetRegistryValue("Places position - top", Top, RegistryValueKind.DWord);
                RegistrySettings.SetRegistryValue("Places position - left", Left, RegistryValueKind.DWord);
            }
        }

        void TbOpacity_Scroll(object sender, EventArgs e) => RefreshMap();

        void SetOpacity()
        {
            if (mapBox1 is not null && mapBox1.Map is not null && mapBox1.Map.BackgroundLayer.Count > 1)
            {
                float opacity = tbOpacity.Value / 100.0f;
                TileAsyncLayer layer = (TileAsyncLayer)mapBox1.Map.BackgroundLayer[1];
                layer.SetOpacity(opacity);
            }
        }
    }
}
