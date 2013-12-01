using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Mapping;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Rendering.Decoration.ScaleBar;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private int geocodedRange;
        private int yearLimit;
        private Color backgroundColour;
        private ClusterLayer clusters;
        
        public TimeLine()
        {
            InitializeComponent();
            mnuMapStyle.Setup(linkLabel1, mapBox1);
            mapZoomToolStrip.Items.Add(mnuMapStyle);
            mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            tbYears.MouseWheel += new MouseEventHandler(tbYears_MouseWheel);
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for (int i = 7; i <= 10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(mapBox1_MapViewOnChange);
            ft = FamilyTree.Instance;
            cbLimitFactDates.Text = "No Limit";
            CheckIfGeocodingNeeded();
            mapBox1.Refresh();
        }

        private void CheckIfGeocodingNeeded()
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)) - 1);
            if (notsearched > 0 && !ft.Geocoding)
            {
                DialogResult res = MessageBox.Show("You have " + notsearched + " places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                    StartGeocoding();
            }
        }

        private void StartGeocoding()
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                this.Cursor = Cursors.WaitCursor;
                GeocodeLocations geo = new GeocodeLocations();
                MainForm.DisposeDuplicateForms(geo);
                geo.Show();
                geo.StartGeoCoding(false);
                geo.BringToFront();
                geo.Focus();
                this.Cursor = Cursors.Default;
            }
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

        private void SetGeoCodedYearRange()
        {
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            List<MapLocation> yearRange = FilterToRelationsIncluded(ft.AllMapLocations);
            foreach (MapLocation ml in yearRange)
            {
                if (ml.Location.IsGeoCoded(false) && ml.FactDate.IsKnown)
                {
                    if (ml.FactDate.StartDate != FactDate.MINDATE && ml.FactDate.StartDate.Year < minGeoCodedYear)
                        minGeoCodedYear = ml.FactDate.StartDate.Year;
                    if (ml.FactDate.EndDate != FactDate.MAXDATE && ml.FactDate.EndDate.Year > maxGeoCodedYear)
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

        private void geocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGeocoding();
        }

        public void DisplayLocationsForYear(string year)
        {
            int result = 0;
            int.TryParse(year, out result);
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
                    bbox.ExpandToInclude(row.Geometry.Coordinate);
                }
                if (!mnuKeepZoom.Checked)
                {
                    Envelope expand;
                    if (bbox.Centre == null)
                        expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
                    else
                    {
                        expand = new Envelope(bbox.TopLeft(), bbox.BottomRight());
                        expand.ExpandBy(bbox.Width * FamilyTree.SCALEBY);
                    }
                    mapBox1.Map.ZoomToBox(expand);
                }
                RefreshTimeline();
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

        private void tbYears_Scroll(object sender, EventArgs e)
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

        private void tbYears_MouseWheel(object sender, EventArgs e)
        {
            // do nothing if using mousewheel this prevents year scrolling when map should scroll
        }

        private void Relations_CheckedChanged(object sender, EventArgs e)
        {
            SetGeoCodedYearRange(); // need to refresh range of years when filters change
            DisplayLocationsForYear(labValue.Text);
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
                RefreshTimeline();
            }
        }

        private void TimeLine_Load(object sender, EventArgs e)
        {
            SetGeoCodedYearRange();
            SetupMap();
            DisplayLocationsForYear(labValue.Text);
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
            MapIndividuals ind = new MapIndividuals(locations, labValue.Text, this);
            ind.Show();
            this.Cursor = Cursors.Default;
        }

        private void mapBox1_MapViewOnChange()
        {
            clusters.Refresh();
        }

        private void mapBox1_MapZoomChanged(double zoom)
        {
            RefreshTimeline();
        }

        public void RefreshTimeline()
        {
            clusters.Refresh();
            mapBox1.Refresh();
        }

        private void mapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshTimeline();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;
            btnStop.Visible = true;
            timer.Enabled = true;
            txtTimeInterval.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (tbYears.Value < tbYears.Maximum)
            {
                tbYears.Value++;
                UpdateMap();
            }
            else
                StopTimer();
        }

        private void txtTimeInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtTimeInterval_Validated(object sender, EventArgs e)
        {
            int result;
            if (Int32.TryParse(txtTimeInterval.Text, out result))
            {
                timer.Interval = result;
            }
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

        private void btnBack10_Click(object sender, EventArgs e)
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

        private void btnBack1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Minimum)
                tbYears.Value -= 1;
            UpdateMap();
        }

        private void btnForward1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Maximum)
                tbYears.Value += 1;
            UpdateMap();
        }

        private void btnForward10_Click(object sender, EventArgs e)
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

        private void mnuDisableTimeline_Click(object sender, EventArgs e)
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

        private void cbLimitFactDates_SelectedIndexChanged(object sender, EventArgs e)
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

        private void mnuHideScaleBar_Click(object sender, EventArgs e)
        {
            if (mnuHideScaleBar.Checked)
                RemoveScaleBar();
            else
                AddScaleBar();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
