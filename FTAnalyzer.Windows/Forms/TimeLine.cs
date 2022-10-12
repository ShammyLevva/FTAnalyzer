using FTAnalyzer.Mapping;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using SharpMap.Layers;
using FTAnalyzer.Utilities;
using FTAnalyzer.Properties;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        readonly MapHelper mh = MapHelper.Instance;
        int minGeoCodedYear;
        int maxGeoCodedYear;
        int geocodedRange;
        int yearLimit;
        ClusterLayer clusters;
        bool loading;
        readonly IProgress<string> outputText;

        public TimeLine(IProgress<string> outputText)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            loading = true;
            this.outputText = outputText;
            mnuMapStyle.Setup(linkLabel1, mapBox1, tbOpacity);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            tbYears.MouseWheel += new MouseEventHandler(TbYears_MouseWheel);
            mnuHideScaleBar.Checked = MappingSettings.Default.HideScaleBar;
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(MapBox1_MapViewOnChange);
            cbLimitFactDates.Text = "No Limit";
        }

        void SetupMap()
        {
            clusters = new ClusterLayer(mapBox1.Map);
            mh.AddParishLayers(mapBox1.Map);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            SetOpacity();
            MapHelper.SetScaleBar(mapBox1);
        }

        void SetGeoCodedYearRange()
        {
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            List<MapLocation> yearRange = FilterToRelationsIncluded(MapHelper.Instance.AllMapLocations);
            foreach (MapLocation ml in yearRange)
            {
                if (ml.Location.IsGeoCoded(false) && ml.FactDate.IsKnown)
                {
                    if (ml.FactDate.StartDate.Year != FactDate.MINDATE.Year && ml.FactDate.StartDate.Year < minGeoCodedYear)
                        minGeoCodedYear = ml.FactDate.StartDate.Year;
                    if (ml.FactDate.EndDate.Year != FactDate.MAXDATE.Year && ml.FactDate.EndDate.Year > maxGeoCodedYear)
                        maxGeoCodedYear = ml.FactDate.EndDate.Year;
                }
            }
            if (minGeoCodedYear == FactDate.MAXDATE.Year || maxGeoCodedYear == FactDate.MINDATE.Year)
            {
                tbYears.Enabled = false;
                labMin.Text = string.Empty;
                labMax.Text = string.Empty;
                labValue.Text = string.Empty;
                geocodedRange = 0;
            }
            else
            {
                tbYears.Enabled = true;
                tbYears.Minimum = minGeoCodedYear;
                tbYears.Maximum = maxGeoCodedYear;
                tbYears.Value = minGeoCodedYear + (maxGeoCodedYear - minGeoCodedYear) / 2;
                labMin.Text = minGeoCodedYear.ToString();
                labMax.Text = maxGeoCodedYear.ToString();
                labValue.Text = tbYears.Value.ToString();
                geocodedRange = maxGeoCodedYear - minGeoCodedYear;
            }
        }

        void GeocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mh.StartGeocoding(outputText);
            Cursor = Cursors.Default;
        }

        public void DisplayLocationsForYear(string year)
        {
            year ??= string.Empty;
            _ = int.TryParse(year, out int result);
            if (year.Length == 4 && result != 0)
            {
                List<MapLocation> locations;
                var mh = MapHelper.Instance;
                if (result == 9999)
                {
                    locations = FilterToRelationsIncluded(mh.AllMapLocations);
                    txtLocations.Text = locations.Count + " Locations in total";
                }
                else
                {
                    locations = FilterToRelationsIncluded(MapHelper.YearMapLocations(new FactDate(year), yearLimit));
                    txtLocations.Text = locations.Count + " Locations in total for year " + year;
                }
                txtLocations.Text += " (you may need to zoom out to see them all). Use arrow tool then select icon to view ancestors at location";
                clusters.Clear();
                foreach (MapLocation loc in locations)
                {
                    FeatureDataRow row = loc.AddFeatureDataRow(clusters.FactLocations);
                }
                if (!mnuKeepZoom.Checked)
                {
                    Envelope expand = MapHelper.GetExtents(clusters.FactLocations);
                    mapBox1.Map.ZoomToBox(expand);
                }
                RefreshClusters();
            }
        }

        List<MapLocation> FilterToRelationsIncluded(List<MapLocation> locations)
        {
            List<MapLocation> result = new();
            foreach (MapLocation ml in locations)
                if (RelationIncluded(ml.Individual.RelationType))
                    result.Add(ml);
            return result;
        }

        bool RelationIncluded(int relationtype)
        {
            return relationtype switch
            {
                Individual.DIRECT => directAncestorsToolStripMenuItem.Checked,
                Individual.BLOOD => bloodRelativesToolStripMenuItem.Checked,
                Individual.MARRIAGE => relatedByMarriageToolStripMenuItem.Checked,
                Individual.MARRIEDTODB => marriedToDirectOrBloodToolStripMenuItem.Checked,
                Individual.DESCENDANT => descendantToolStripMenuItem.Checked,
                Individual.LINKED => linkedByMarriageToolStripMenuItem.Checked,
                _ => unknownToolStripMenuItem.Checked,
            };
        }

        void TbYears_Scroll(object sender, EventArgs e) => UpdateMap();

        void UpdateMap()
        {
            Cursor = Cursors.WaitCursor;
            labValue.Text = tbYears.Value.ToString();
            if (mnuDisableTimeline.Checked)
                DisplayLocationsForYear("9999");
            else
                DisplayLocationsForYear(labValue.Text);
            Cursor = Cursors.Default;
        }

        void TbYears_MouseWheel(object sender, EventArgs e)
        {
            // do nothing if using mousewheel this prevents year scrolling when map should scroll
        }

        void Relations_CheckedChanged(object sender, EventArgs e)
        {
            SetGeoCodedYearRange(); // need to refresh range of years when filters change
            DisplayLocationsForYear(labValue.Text);
        }

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

        void TimeLine_Load(object sender, EventArgs e)
        {
            SetGeoCodedYearRange();
            SetupMap();
            DisplayLocationsForYear(labValue.Text);
            mh.CheckIfGeocodingNeeded(this, outputText);
            int Width = (int)Application.UserAppDataRegistry.GetValue("Timeline size - width", this.Width);
            int Height = (int)Application.UserAppDataRegistry.GetValue("Timeline size - height", this.Height);
            int Top = (int)Application.UserAppDataRegistry.GetValue("Timeline position - top", this.Top);
            int Left = (int)Application.UserAppDataRegistry.GetValue("Timeline position - left", this.Left);
            this.Width = Width;
            this.Height = Height;
            this.Top = Top + NativeMethods.TopTaskbarOffset;
            this.Left = Left;
            SpecialMethods.SetFonts(this);
            RefreshMap();
            loading = false;
        }

        void MapBox1_MapQueried(FeatureDataTable data)
        {
            Cursor = Cursors.WaitCursor;
            List<MapLocation> locations = new();
            foreach (FeatureDataRow row in data)
            {
                IList<FeatureDataRow> features = (List<FeatureDataRow>)row["Features"];
                foreach (FeatureDataRow feature in features)
                {
                    locations.Add((MapLocation)feature["MapLocation"]);
                }
            }
            MapIndividuals ind = new(locations, labValue.Text, this);
            ind.Show();
            Cursor = Cursors.Default;
        }

        void MapBox1_MapViewOnChange() => clusters.Refresh();

        void MapBox1_MapZoomChanged(double zoom) => RefreshClusters();

        public void RefreshClusters()
        {
            clusters.Refresh();
            RefreshMap();
        }

        void MapBox1_MapCenterChanged(Coordinate center) => RefreshClusters();

        void BtnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;
            btnStop.Visible = true;
            timer.Enabled = true;
            txtTimeInterval.Enabled = false;
        }

        void BtnStop_Click(object sender, EventArgs e) => StopTimer();

        void StopTimer()
        {
            btnPlay.Visible = !mnuDisableTimeline.Checked; // don't make visible if showing all locations
            btnStop.Visible = false;
            timer.Enabled = false;
            txtTimeInterval.Enabled = true;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (tbYears.Value < tbYears.Maximum)
            {
                tbYears.Value++;
                UpdateMap();
            }
            else
                StopTimer();
        }

        void TxtTimeInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        void TxtTimeInterval_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(txtTimeInterval.Text, out int result))
            {
                timer.Interval = result;
            }
        }

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

        void BtnBack10_Click(object sender, EventArgs e)
        {
            int step = 10;
            if (geocodedRange <= 150)
                step = 5;
            if (tbYears.Value < tbYears.Minimum + step)
                tbYears.Value = tbYears.Minimum;
            else
                tbYears.Value -= step;
            UpdateMap();
        }

        void BtnBack1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Minimum)
                tbYears.Value -= 1;
            UpdateMap();
        }

        void BtnForward1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Maximum)
                tbYears.Value += 1;
            UpdateMap();
        }

        void BtnForward10_Click(object sender, EventArgs e)
        {
            int step = 10;
            if (geocodedRange <= 150)
                step = 5;
            if (tbYears.Value > tbYears.Maximum - step)
                tbYears.Value = tbYears.Maximum;
            else
                tbYears.Value += step;
            UpdateMap();
        }

        void MnuDisableTimeline_Click(object sender, EventArgs e)
        {
            tbYears.Visible = !mnuDisableTimeline.Checked;
            btnBack1.Visible = !mnuDisableTimeline.Checked;
            btnBack10.Visible = !mnuDisableTimeline.Checked;
            btnForward1.Visible = !mnuDisableTimeline.Checked;
            btnForward10.Visible = !mnuDisableTimeline.Checked;
            btnPlay.Visible = !mnuDisableTimeline.Checked;
            btnStop.Visible = !btnPlay.Visible;
            toolStripSeparator2.Visible = !mnuDisableTimeline.Checked;
            toolStripSeparator3.Visible = !mnuDisableTimeline.Checked;
            labValue.Visible = !mnuDisableTimeline.Checked;
            labMin.Visible = !mnuDisableTimeline.Checked;
            labMax.Visible = !mnuDisableTimeline.Checked;
            toolStripLabel1.Visible = !mnuDisableTimeline.Checked;
            toolStripLabel2.Visible = !mnuDisableTimeline.Checked;
            txtTimeInterval.Visible = !mnuDisableTimeline.Checked;
            mnuLimitFactDates.Enabled = !mnuDisableTimeline.Checked;
            mnuKeepZoom.Enabled = !mnuDisableTimeline.Checked;
            if (mnuDisableTimeline.Checked)
                StopTimer(); // make sure we aren't playing timeline if we disable timeline
            txtLocations.Text = string.Empty; // set empty so looks better during redraw
            Application.DoEvents(); // force screen refresh
            UpdateMap();
        }

        void CbLimitFactDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            yearLimit = GetYearLimit();
            UpdateMap();
            mnuOptions.HideDropDown();
        }

        int GetYearLimit()
        {
            //check the 
            return cbLimitFactDates.Text switch
            {
                "No Limit" => int.MaxValue,
                "Exact year only" => 1,
                "+/- 2 years" => 2,
                "+/- 5 years" => 5,
                "+/-10 years" => 10,
                "+/-20 years" => 20,
                "+/-50 years" => 50,
                "+/-100 years" => 100,
                _ => int.MaxValue,
            };
        }

        void MnuHideScaleBar_Click(object sender, EventArgs e) => MapHelper.MnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);

        void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SpecialMethods.VisitWebsite(e.Link.LinkData as string);

        void TimeLine_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void TimeLine_Resize(object sender, EventArgs e) => SavePosition();

        void TimeLine_Move(object sender, EventArgs e) => SavePosition();

        void SavePosition()
        {
            if (!loading && WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                Application.UserAppDataRegistry.SetValue("Timeline size - width", Width);
                Application.UserAppDataRegistry.SetValue("Timeline size - height", Height);
                Application.UserAppDataRegistry.SetValue("Timeline position - top", Top);
                Application.UserAppDataRegistry.SetValue("Timeline position - left", Left);
            }
        }

        void ResetFormToDefaultPostiionAndSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loading = true;
            Height = 622;
            Width = 937;
            Top = 50;
            Left = 50;
            loading = false;
            SavePosition();
        }

        void TbOpacity_Scroll(object sender, EventArgs e) => RefreshMap();

        void SetOpacity()
        {
            if (mapBox1 != null && mapBox1.Map != null && mapBox1.Map.BackgroundLayer.Count > 1)
            {
                float opacity = tbOpacity.Value / 100.0f;
                TileAsyncLayer layer = (TileAsyncLayer)mapBox1.Map.BackgroundLayer[1];
                layer.SetOpacity(opacity);
            }
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
    }
}
