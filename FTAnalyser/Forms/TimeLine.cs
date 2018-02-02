using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using GeoAPI.Geometries;
using SharpMap.Data;
using System.Web;
using SharpMap.Layers;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private MapHelper mh = MapHelper.Instance;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private int geocodedRange;
        private int yearLimit;
        private Color backgroundColour;
        private ClusterLayer clusters;
        private bool loading;

        public TimeLine()
        {
            InitializeComponent();
            loading = true;
            mnuMapStyle.Setup(linkLabel1, mapBox1);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            //mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            tbYears.MouseWheel += new MouseEventHandler(TbYears_MouseWheel);
            mnuHideScaleBar.Checked = Properties.MappingSettings.Default.HideScaleBar;
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(MapBox1_MapViewOnChange);
            cbLimitFactDates.Text = "No Limit";
        }


        private void SetupMap()
        {
            clusters = new ClusterLayer(mapBox1.Map);
            mh.AddParishLayers(mapBox1.Map);
            mapBox1.Map.MinimumZoom = 500;
            mapBox1.Map.MaximumZoom = 50000000;
            mapBox1.QueryGrowFactor = 30;
            mapBox1.Map.ZoomToExtents();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            mh.SetScaleBar(mapBox1);
        }

        private void SetGeoCodedYearRange()
        {
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            List<MapLocation> yearRange = FilterToRelationsIncluded(ft.AllMapLocations);
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

        private void GeocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            mh.StartGeocoding();
            this.Cursor = Cursors.Default;
        }

        public void DisplayLocationsForYear(string year)
        {
            int.TryParse(year, out int result);
            if (year.Length == 4 && result != 0)
            {
                List<MapLocation> locations;
                if (result == 9999)
                {
                    locations = FilterToRelationsIncluded(ft.AllMapLocations);
                    txtLocations.Text = locations.Count() + " Locations in total";
                }
                else
                {
                    locations = FilterToRelationsIncluded(ft.YearMapLocations(new FactDate(year), yearLimit));
                    txtLocations.Text = locations.Count() + " Locations in total for year " + year;
                }
                txtLocations.Text += " (you may need to zoom out to see them all). Use arrow tool then select icon to view ancestors at location";
                clusters.Clear();
                Envelope bbox = new Envelope();
                foreach (MapLocation loc in locations)
                {
                    FeatureDataRow row = loc.AddFeatureDataRow(clusters.FactLocations);
                }
                if (!mnuKeepZoom.Checked)
                {
                    Envelope expand = mh.GetExtents(clusters.FactLocations);
                    mapBox1.Map.ZoomToBox(expand);
                }
                RefreshClusters();
            }
        }

        private List<MapLocation> FilterToRelationsIncluded(List<MapLocation> locations)
        {
            List<MapLocation> result = new List<MapLocation>();
            foreach (MapLocation ml in locations)
                if (RelationIncluded(ml.Individual.RelationType))
                    result.Add(ml);
            return result;
        }

        private bool RelationIncluded(int relationtype)
        {
            switch (relationtype)
            {
                case Individual.DIRECT:
                    return directAncestorsToolStripMenuItem.Checked;
                case Individual.BLOOD:
                    return bloodRelativesToolStripMenuItem.Checked;
                case Individual.MARRIAGE:
                    return relatedByMarriageToolStripMenuItem.Checked;
                case Individual.MARRIEDTODB:
                    return marriedToDirectOrBloodToolStripMenuItem.Checked;
                case Individual.UNKNOWN:
                default:
                    return unknownToolStripMenuItem.Checked;
            }
        }

        private void TbYears_Scroll(object sender, EventArgs e)
        {
            UpdateMap();
        }

        private void UpdateMap()
        {
            this.Cursor = Cursors.WaitCursor;
            labValue.Text = tbYears.Value.ToString();
            if (mnuDisableTimeline.Checked)
                DisplayLocationsForYear("9999");
            else
                DisplayLocationsForYear(labValue.Text);
            this.Cursor = Cursors.Default;
        }

        private void TbYears_MouseWheel(object sender, EventArgs e)
        {
            // do nothing if using mousewheel this prevents year scrolling when map should scroll
        }

        private void Relations_CheckedChanged(object sender, EventArgs e)
        {
            SetGeoCodedYearRange(); // need to refresh range of years when filters change
            DisplayLocationsForYear(labValue.Text);
        }

        private void MapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void TimeLine_Load(object sender, EventArgs e)
        {
            SetGeoCodedYearRange();
            SetupMap();
            DisplayLocationsForYear(labValue.Text);
            mh.CheckIfGeocodingNeeded(this);
            int Width = (int)Application.UserAppDataRegistry.GetValue("Timeline size - width", this.Width);
            int Height = (int)Application.UserAppDataRegistry.GetValue("Timeline size - height", this.Height);
            int Top = (int)Application.UserAppDataRegistry.GetValue("Timeline position - top", this.Top);
            int Left = (int)Application.UserAppDataRegistry.GetValue("Timeline position - left", this.Left);
            this.Width = Width;
            this.Height = Height;
            this.Top = Top;
            this.Left = Left;
            RefreshMap();
            loading = false;
        }

        private void MapBox1_MapQueried(FeatureDataTable data)
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
            MapIndividuals ind = new MapIndividuals(locations, labValue.Text, this);
            ind.Show();
            this.Cursor = Cursors.Default;
        }

        private void MapBox1_MapViewOnChange()
        {
            clusters.Refresh();
        }

        private void MapBox1_MapZoomChanged(double zoom)
        {
            RefreshClusters();
        }

        public void RefreshClusters()
        {
            clusters.Refresh();
            RefreshMap();
        }

        private void MapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshClusters();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;
            btnStop.Visible = true;
            timer.Enabled = true;
            txtTimeInterval.Enabled = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void StopTimer()
        {
            btnPlay.Visible = !mnuDisableTimeline.Checked; // don't make visible if showing all locations
            btnStop.Visible = false;
            timer.Enabled = false;
            txtTimeInterval.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (tbYears.Value < tbYears.Maximum)
            {
                tbYears.Value++;
                UpdateMap();
            }
            else
                StopTimer();
        }

        private void TxtTimeInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TxtTimeInterval_Validated(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtTimeInterval.Text, out int result))
            {
                timer.Interval = result;
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.Checked = true;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        private void MapBox1_ActiveToolChanged(SharpMap.Forms.MapBox.Tools tool)
        {
            if (mapBox1.ActiveTool != SharpMap.Forms.MapBox.Tools.QueryPoint)
                btnSelect.Checked = false;
        }

        private void BtnBack10_Click(object sender, EventArgs e)
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

        private void BtnBack1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Minimum)
                tbYears.Value -= 1;
            UpdateMap();
        }

        private void BtnForward1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Maximum)
                tbYears.Value += 1;
            UpdateMap();
        }

        private void BtnForward10_Click(object sender, EventArgs e)
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

        private void MnuDisableTimeline_Click(object sender, EventArgs e)
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

        private void CbLimitFactDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            yearLimit = GetYearLimit();
            UpdateMap();
            mnuOptions.HideDropDown();
        }

        private int GetYearLimit()
        {
            //check the 
            switch (cbLimitFactDates.Text)
            {
                case "No Limit":
                    return int.MaxValue;
                case "Exact year only":
                    return 1;
                case "+/- 2 years":
                    return 2;
                case "+/- 5 years":
                    return 5;
                case "+/-10 years":
                    return 10;
                case "+/-20 years":
                    return 20;
                case "+/-50 years":
                    return 50;
                case "+/-100 years":
                    return 100;
                default:
                    return int.MaxValue;
            }
        }

        private void MnuHideScaleBar_Click(object sender, EventArgs e)
        {
            mh.MnuHideScaleBar_Click(mnuHideScaleBar, mapBox1);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HttpUtility.VisitWebsite(e.Link.LinkData as string);
        }

        private void TimeLine_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void TimeLine_Resize(object sender, EventArgs e)
        {
            SavePosition();
        }

        private void TimeLine_Move(object sender, EventArgs e)
        {
            SavePosition();
        }

        private void SavePosition()
        {
            if (!loading && this.WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                Application.UserAppDataRegistry.SetValue("Timeline size - width", this.Width);
                Application.UserAppDataRegistry.SetValue("Timeline size - height", this.Height);
                Application.UserAppDataRegistry.SetValue("Timeline position - top", this.Top);
                Application.UserAppDataRegistry.SetValue("Timeline position - left", this.Left);
            }
        }

        private void ResetFormToDefaultPostiionAndSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loading = true;
            this.Height = 622;
            this.Width = 937;
            this.Top = 50;
            this.Left = 50;
            loading = false;
            SavePosition();
        }

        private void TbOpacity_Scroll(object sender, EventArgs e)
        {
            RefreshMap();
        }

        private void SetOpacity()
        {
            float opacity = tbOpacity.Value / 100.0f;
            TileAsyncLayer layer = (TileAsyncLayer)mapBox1.Map.BackgroundLayer[1];
            layer.SetOpacity(opacity);
        }

        private void RefreshMap()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshMap()));
                return;
            }
            SetOpacity();
            mapBox1.Refresh();
        }
    }
}
