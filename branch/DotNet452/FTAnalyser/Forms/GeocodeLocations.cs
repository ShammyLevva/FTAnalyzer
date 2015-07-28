﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Events;
using FTAnalyzer.Filters;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using SharpMap;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Rendering;
using SharpMap.Styles;
using System.Text;
using SharpMap.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class GeocodeLocations : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FamilyTree ft;
        private Font italicFont;
        private ReportFormHelper reportFormHelper;
        private List<IDisplayGeocodedLocation> locations;
        private bool formClosing;
        private string statusText;
        private bool refreshingMenus;
        private ISet<string> noneOfTheAbove;
        private ToolStripMenuItem[] noneOfTheAboveMenus;
        private ConcurrentQueue<FactLocation> queue;
        private IDictionary<string, IList<OS50kGazetteer>> OS50kDictionary;
        private IList<OS50kGazetteer> OS50k;

        private FactLocation CopyLocation;

        public GeocodeLocations()
        {
            InitializeComponent();
            ft = FamilyTree.Instance;
            this.refreshingMenus = false;
            this.locations = ft.AllGeocodingLocations;
            this.queue = new ConcurrentQueue<FactLocation>();
            this.CopyLocation = FactLocation.UNKNOWN_LOCATION;
            mnuPasteLocation.Enabled = false;
            dgLocations.AutoGenerateColumns = false;
            reportFormHelper = new ReportFormHelper(this, this.Text, dgLocations, this.ResetTable, "Geocode Locations");
            italicFont = new Font(dgLocations.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("GeocodeLocationsColumns.xml");
            mnuGoogleGeocodeLocations.Enabled = !ft.Geocoding; // disable menu if already geocoding
            mnuEditLocation.Enabled = !ft.Geocoding;
            mnuReverseGeocode.Enabled = !ft.Geocoding;
            mnuOSGeocodeLocations.Enabled = !ft.Geocoding;
            SetupFilterMenu();
            SetStatusText();
            CheckGoogleStatusCodes(locations);
            UpdateGridWithFilters(false);
        }

        private void SetStatusText()
        {
            int gedcom = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER));
            int found = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED));
            int osmatch = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KMATCH));
            int osfuzzy = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KFUZZY));
            int ospartial = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OS_50KPARTIAL));
            int partial = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH));
            int levelpartial = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.LEVEL_MISMATCH));
            int notsearched = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED));
            int notfound = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NO_MATCH));
            int outofbounds = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OUT_OF_BOUNDS));
            int incorrect = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.INCORRECT));
            int total = FactLocation.LocationsCount;
            txtGoogleWait.Text = string.Empty;
            statusText = "Already Geocoded: " + (gedcom + found + osmatch + osfuzzy) +
                ", partials: " + (partial + levelpartial + ospartial + notfound + incorrect + outofbounds)
                + ", yet to search: " + notsearched + " of " + total + " locations.";
            txtLocations.Text = statusText;
        }

        private void SetupFilterMenu()
        {
            foreach (KeyValuePair<FactLocation.Geocode, string> item in FactLocation.Geocodes.OrderBy(x => x.Value))
            {
                string geocode = item.Value;
                ToolStripMenuItem menu = new ToolStripMenuItem(geocode);
                menu.Name = geocode;
                menu.Checked = Application.UserAppDataRegistry.GetValue(geocode, "True").Equals("True");
                menu.CheckOnClick = true;
                menu.CheckedChanged += new EventHandler(menuGeocode_CheckedChanged);
                menu.Image = FactLocationImage.ErrorIcon(item.Key).Icon;
                menu.TextImageRelation = TextImageRelation.TextBeforeImage;
                mnuGeocodeStatus.DropDownItems.Add(menu);
            }

            ToolStripMenuItem placesMenu = new ToolStripMenuItem("Places");
            placesMenu.Name = "Places";

            foreach (string resultType in GoogleMap.RESULT_TYPES)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(resultType);
                menu.Name = resultType;
                menu.Checked = Application.UserAppDataRegistry.GetValue(resultType, "True").Equals("True");
                menu.CheckOnClick = true;
                menu.CheckedChanged += new EventHandler(menuResultType_CheckedChanged);
                if (GoogleMap.PLACES.Contains(resultType))
                    placesMenu.DropDownItems.Add(menu);
                else
                    mnuFoundResultType.DropDownItems.Add(menu);
            }
            mnuFoundResultType.DropDownItems.Add(placesMenu);
            if (AllFiltersActive(true))
                mnuSelectClear.Text = "Clear All";
            else
                mnuSelectClear.Text = "Select All";
        }

        private bool AllFiltersActive(bool GoogleOnly)
        {
            int count = 0;
            int menus = 0;
            if (!GoogleOnly)
            {
                foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
                {
                    if (menu.Text != "Select All" && menu.Text != "Clear All")
                    {
                        menus++;
                        if (menu.Checked)
                            count++;
                    }
                }
                if (menus == count)
                    mnuStatusSelectAll.Text = "Clear All";
                else
                    mnuStatusSelectAll.Text = "Select All";
            }
            foreach (ToolStripMenuItem menu in mnuFoundResultType.DropDownItems)
            {
                if (menu.Text != "Places" && menu.Text != "Select All" && menu.Text != "Clear All")
                {
                    menus++;
                    if (menu.Checked)
                        count++;
                }
            }
            ToolStripMenuItem places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
            foreach (ToolStripMenuItem menu in places.DropDownItems)
            {
                if (menu.Text != "Select All" && menu.Text != "Clear All")
                {
                    menus++;
                    if (menu.Checked)
                        count++;
                }
            }
            return count == menus;
        }

        private void UpdateGridWithFilters(bool keepCurrentLocation)
        {
            this.Cursor = Cursors.WaitCursor;
            FactLocation loc = dgLocations.CurrentRow != null ? dgLocations.CurrentRow.DataBoundItem as FactLocation : null;
            SortableBindingList<IDisplayGeocodedLocation> filteredLocations = ApplyFilters(null);
            // store sort order
            DataGridViewColumn sortCol = dgLocations.SortedColumn;
            ListSortDirection sortOrder = dgLocations.SortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            dgLocations.DataSource = filteredLocations;
            //restore sort order
            if (sortCol != null)
                dgLocations.Sort(sortCol, sortOrder);
            dgLocations.Refresh();
            txtLocations.Text = statusText + " Displaying: " + dgLocations.RowCount + ". ";
            if(loc != null) 
                SelectLocation(loc);
            this.Cursor = Cursors.Default;
        }

        private bool StatusFilter(IDisplayGeocodedLocation loc)
        {
            bool result = false;
            foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
            {
                if (menu.Checked && loc.Geocoded == menu.Name)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private SortableBindingList<IDisplayGeocodedLocation> ApplyFilters(FactLocation mustDisplay)
        {
            if (AllFiltersActive(false))
                return new SortableBindingList<IDisplayGeocodedLocation>(locations);
            List<IDisplayGeocodedLocation> results = new List<IDisplayGeocodedLocation>();
            ToolStripMenuItem places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
            ToolStripMenuItem[] list = new ToolStripMenuItem[places.DropDownItems.Count + mnuFoundResultType.DropDownItems.Count + noneOfTheAboveMenus.Count()];
            mnuFoundResultType.DropDownItems.CopyTo(list, 0);
            places.DropDownItems.CopyTo(list, mnuFoundResultType.DropDownItems.Count);
            noneOfTheAboveMenus.CopyTo(list, mnuFoundResultType.DropDownItems.Count + places.DropDownItems.Count); // add any missing elements to always display them
            foreach (IDisplayGeocodedLocation loc in locations)
            {
                if (StatusFilter(loc) || (mustDisplay != null && loc.Equals(mustDisplay)))
                {
                    if (loc.FoundResultType == null || loc.FoundResultType.Length == 0 || (mustDisplay != null && loc.Equals(mustDisplay)))
                        results.Add(loc);
                    else
                    {
                        foreach (ToolStripMenuItem menu in list)
                        {
                            // filter locations on menu items that are ticked
                            if (menu.Checked && loc.FoundResultType.Contains(menu.Name))
                            {
                                results.Add(loc);
                                break;
                            }
                        }
                    }
                }
            }
            return new SortableBindingList<IDisplayGeocodedLocation>(results);
        }

        private void CheckGoogleStatusCodes(List<IDisplayGeocodedLocation> input)
        {
            noneOfTheAbove = new HashSet<string>();
            Dictionary<string, List<IDisplayGeocodedLocation>> results = new Dictionary<string, List<IDisplayGeocodedLocation>>();
            foreach (IDisplayGeocodedLocation loc in input)
            {
                if (loc.FoundResultType.Length > 0)
                {
                    string[] parts = loc.FoundResultType.Split(',');
                    foreach (string part in parts)
                    {
                        string key = part.Trim();
                        if (!results.ContainsKey(key))
                        {
                            results[key] = new List<IDisplayGeocodedLocation>();
                            if (!GoogleMap.RESULT_TYPES.Contains(key))
                                noneOfTheAbove.Add(key);
                        }
                        results[key].Add(loc);
                    }
                }
            }
            noneOfTheAboveMenus = new ToolStripMenuItem[noneOfTheAbove.Count];
            if (noneOfTheAbove.Count > 0)
            {
                int index = 0;
                foreach (string resultType in noneOfTheAbove)
                {
                    ToolStripMenuItem menu = new ToolStripMenuItem(resultType);
                    menu.Name = resultType;
                    menu.Checked = true;
                    noneOfTheAboveMenus[index++] = menu;
                }
            }
        }

        private void menuGeocode_CheckedChanged(object sender, EventArgs e)
        {
            if (!refreshingMenus)
                UpdateGeocodeStatusMenus();
        }

        private void UpdateGeocodeStatusMenus()
        {
            foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters(false);
        }

        private void menuResultType_CheckedChanged(object sender, EventArgs e)
        {
            if (!refreshingMenus)
                UpdateGoogleStatusMenus();
        }

        private void UpdateGoogleStatusMenus()
        {
            foreach (ToolStripMenuItem menu in mnuFoundResultType.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters(false);
        }

        private void mnuSelectClear_Click(object sender, EventArgs e)
        {
            refreshingMenus = true;
            ToolStripMenuItem places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
            if (mnuSelectClear.Text.Equals("Clear All"))
            {
                mnuSelectClear.Text = "Select All";
                foreach (ToolStripMenuItem menu in mnuFoundResultType.DropDownItems)
                    menu.Checked = false;
                foreach (ToolStripMenuItem menu in places.DropDownItems)
                    menu.Checked = false;
            }
            else
            {
                mnuSelectClear.Text = "Clear All";
                foreach (ToolStripMenuItem menu in mnuFoundResultType.DropDownItems)
                    menu.Checked = true;
                foreach (ToolStripMenuItem menu in places.DropDownItems)
                    menu.Checked = true;
                mnuSelectClear.Checked = false; // make sure the clear all isn't checked
                mnuStatusSelectAll.Checked = false;
                places.Checked = false;
            }
            refreshingMenus = false;
            UpdateGoogleStatusMenus();
        }

        private void mnuStatusSelectAll_Click(object sender, EventArgs e)
        {
            refreshingMenus = true;
            if (mnuStatusSelectAll.Text.Equals("Clear All"))
            {
                mnuStatusSelectAll.Text = "Select All";
                foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
                    menu.Checked = false;
            }
            else
            {
                mnuStatusSelectAll.Text = "Clear All";
                foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
                    menu.Checked = true;
                mnuStatusSelectAll.Checked = false; // make sure clear all isn't checked
            }
            refreshingMenus = false;
            UpdateGeocodeStatusMenus();
        }

        private void ResetTable()
        {
            dgLocations.Sort(dgLocations.Columns["GeocodedLocation"], ListSortDirection.Ascending);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Locations report");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void Facts_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayGeocodedLocation>();
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("GeocodeLocationsColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("GeocodeLocationsColumns.xml");
            MessageBox.Show("Form Settings Saved", "Geocode Locations");
        }

        private void dgLocations_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                IDisplayGeocodedLocation loc = dgLocations.Rows[e.RowIndex].DataBoundItem as IDisplayGeocodedLocation;
                e.ToolTipText = "Geocoding status: " + loc.Geocoded;
            }
        }

        private void dgLocations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ft.Geocoding && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                FactLocation loc = dgLocations.Rows[e.RowIndex].DataBoundItem as FactLocation;
                EditLocation(loc);
            }
        }

        private void mnuEditLocation_Click(object sender, EventArgs e)
        {
            if (!ft.Geocoding)
            {
                this.Cursor = Cursors.WaitCursor;
                FactLocation loc = dgLocations.CurrentRow.DataBoundItem as FactLocation;
                EditLocation(loc);
            }
        }

        private void EditLocation(FactLocation loc)
        {
            EditLocation editform = new EditLocation(loc);
            this.Cursor = Cursors.Default;
            mnuPasteLocation.Enabled = false;
            CopyLocation = FactLocation.UNKNOWN_LOCATION;
            DialogResult result = editform.ShowDialog(this);
            if (editform.UserSavedPoint)
                AddLocationToQueue(loc);  // we have edited the location so add reverse geocode to queue
            editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
            // force refresh of locations from new edited data
            dgLocations.Refresh();
        }

        private void mnuCopyLocation_Click(object sender, EventArgs e)
        {
            CopyLocation = dgLocations.CurrentRow.DataBoundItem as FactLocation;
            mnuPasteLocation.Enabled = true;
        }

        private void mnuPasteLocation_Click(object sender, EventArgs e)
        {
            if (CopyLocation.IsGeoCoded(false))
            {
                FactLocation pasteLocation = dgLocations.CurrentRow.DataBoundItem as FactLocation;
                FactLocation.CopyLocationDetails(CopyLocation, pasteLocation);
                UpdateDatabase(pasteLocation, true);
                dgLocations.Refresh();
            }
        }

        #region Google Geocode Threading
        private void googleGeocodingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GoogleGeoCode(googleGeocodeBackgroundWorker, e);
        }

        private void googleGeocodingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbGeocoding.Value = (e.ProgressPercentage < 0) ? 1 : e.ProgressPercentage; ;
            txtLocations.Text = (string)e.UserState;
        }

        private void googleGeocodingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkFinished(sender);
        }

        private void WorkFinished(object sender)
        {
            pbGeocoding.Value = 100;
            pbGeocoding.Visible = false;
            txtGoogleWait.Text = string.Empty;
            mnuGoogleGeocodeLocations.Enabled = true;
            mnuEditLocation.Enabled = true;
            mnuReverseGeocode.Enabled = true;
            mnuOSGeocodeLocations.Enabled = true;
            string title = sender == OSGeocodeBackgroundWorker ? "OS Geocoding Results:" : "Google Geocoding Results:";
            ft.WriteGeocodeStatstoRTB(title);
            ft.Geocoding = false;
            if (formClosing)
                this.Close();
            else
                UpdateGridWithFilters(true);
        }

        private void GeocodeLocations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (googleGeocodeBackgroundWorker.IsBusy)
            {
                googleGeocodeBackgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                e.Cancel = true;
                formClosing = true;
            }
            if (reverseGeocodeBackgroundWorker.IsBusy)
            {
                reverseGeocodeBackgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                e.Cancel = true;
                formClosing = true;
            }
            if (OSGeocodeBackgroundWorker.IsBusy)
            {
                OSGeocodeBackgroundWorker.CancelAsync();
                e.Cancel = true;
                formClosing = true;
            }
        }

        public void GoogleMap_WaitingForGoogle(object sender, GoogleWaitingEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => GoogleMap_WaitingForGoogle(sender, args)));
                return;
            }
            txtGoogleWait.Text = args.Message;
        }

        private void reverseGeocodeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReverseGeoCode(reverseGeocodeBackgroundWorker, e);
        }

        #endregion

        #region Google Geocoding

        public void StartGoogleGeoCoding(bool retryPartials)
        {
            if (googleGeocodeBackgroundWorker.IsBusy || OSGeocodeBackgroundWorker.IsBusy)
            {
                MessageBox.Show("A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FT Analyzer");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                pbGeocoding.Visible = true;
                mnuGoogleGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                mnuOSGeocodeLocations.Enabled = false;
                ft.Geocoding = true;
                googleGeocodeBackgroundWorker.RunWorkerAsync(retryPartials);
                this.Cursor = Cursors.Default;
            }
        }

        public void GoogleGeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                bool retryPartial = (bool)e.Argument;
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                DatabaseHelper dbh = DatabaseHelper.Instance;

                int count = 0;
                int googled = 0;
                int geocoded = 0;
                int skipped = 0;
                int total = FactLocation.LocationsCount;
                GoogleMap.ThreadCancelled = false;

                foreach (FactLocation loc in FactLocation.AllLocations)
                {
                    if (loc.IsGeoCoded(retryPartial))
                        geocoded++;
                    else if (loc.GeocodeStatus == FactLocation.Geocode.INCORRECT || loc.Country.Equals(Countries.AT_SEA))
                        skipped++; // don't re-geocode incorrect ones as that would reset incorrect flag back to what user already identified was wrong
                    else
                    {
                        bool inDatabase = dbh.IsLocationInDatabase(loc.ToString());
                        if (loc.ToString().Length > 0)
                        {
                            GeoResponse res = null;
                            if (loc.GeocodeStatus == FactLocation.Geocode.NOT_SEARCHED ||
                                (retryPartial &&
                                    (loc.GeocodeStatus == FactLocation.Geocode.PARTIAL_MATCH ||
                                     loc.GeocodeStatus == FactLocation.Geocode.LEVEL_MISMATCH ||
                                     loc.GeocodeStatus == FactLocation.Geocode.OS_50KPARTIAL)))
                            {
                                log.Info("Searching Google for '" + loc.GoogleFixed + "' original text was '" + loc.GEDCOMLocation + "'.");
                                res = SearchGoogle(loc.GoogleFixed);
                            }
                            if (res != null && ((res.Status == "OK" && res.Results.Length > 0) || res.Status == "ZERO_RESULTS"))
                            {
                                double latitude = 0;
                                double longitude = 0;
                                int foundLevel = -1;
                                string address = string.Empty;
                                string resultType = string.Empty;
                                GeoResponse.CResult.CGeometry.CViewPort viewport = new GeoResponse.CResult.CGeometry.CViewPort();
                                loc.GeocodeStatus = FactLocation.Geocode.NO_MATCH;
                                Envelope bbox = Countries.BoundingBox(loc.Country);
                                if (res.Status == "OK")
                                {
                                    int checkresultsPass = 1;
                                    GeoResponse.CResult originalResult = res.Results[0];
                                    while (checkresultsPass <= 2 && res.Status == "OK") // check for ok on second pass
                                    {
                                        foreach (GeoResponse.CResult result in res.Results)
                                        {
                                            foundLevel = GoogleMap.GetFactLocation(result.Types);
                                            address = result.ReturnAddress;
                                            viewport = result.Geometry.ViewPort;
                                            resultType = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                                            if (foundLevel >= loc.Level && bbox.Covers(new Coordinate(result.Geometry.Location.Long, result.Geometry.Location.Lat)))
                                            {
                                                latitude = result.Geometry.Location.Lat;
                                                longitude = result.Geometry.Location.Long;
                                                loc.GeocodeStatus = result.PartialMatch ? FactLocation.Geocode.PARTIAL_MATCH : FactLocation.Geocode.MATCHED;
                                                loc.ViewPort = MapTransforms.TransformViewport(viewport);
                                                if (!result.PartialMatch)
                                                {
                                                    if (checkresultsPass == 2)
                                                        log.Info("Geocoding found a match with " + loc.GEDCOMLocation + " previously failed with " + loc.GoogleFixed);
                                                    checkresultsPass = 3; // force exit
                                                    break; // we've got a good match so exit
                                                }
                                            }
                                        }
                                        if (loc.GeocodeStatus != FactLocation.Geocode.MATCHED && checkresultsPass == 1)
                                        {
                                            if (loc.GEDCOMLocation.Equals(loc.GoogleFixed))
                                                checkresultsPass++;  // if we have the same string skip checking GEDCOM location
                                            else
                                            {
                                                log.Info("Searching Google for original text '" + loc.GEDCOMLocation + "'.");
                                                res = SearchGoogle(loc.GEDCOMLocation);
                                            }
                                        }
                                        checkresultsPass++;
                                    }
                                    if (loc.GeocodeStatus != FactLocation.Geocode.MATCHED)
                                    {
                                        // we checked all the google results and no joy so take first result as level partial match
                                        foundLevel = GoogleMap.GetFactLocation(originalResult.Types);
                                        address = originalResult.ReturnAddress;
                                        latitude = originalResult.Geometry.Location.Lat;
                                        longitude = originalResult.Geometry.Location.Long;
                                        viewport = originalResult.Geometry.ViewPort;
                                        resultType = EnhancedTextInfo.ConvertStringArrayToString(originalResult.Types);
                                        if (loc.GeocodeStatus == FactLocation.Geocode.NO_MATCH) // we are still on no match so we don't even have a Google partial
                                        {
                                            if (bbox.Covers(new Coordinate(originalResult.Geometry.Location.Long, originalResult.Geometry.Location.Lat)))
                                                loc.GeocodeStatus = FactLocation.Geocode.LEVEL_MISMATCH;
                                            else
                                                loc.GeocodeStatus = FactLocation.Geocode.OUT_OF_BOUNDS;
                                        }
                                    }
                                    googled++;
                                }
                                else if (res.Status == "ZERO_RESULTS")
                                {
                                    skipped++;
                                    foundLevel = -2;
                                }
                                Coordinate mpoint = MapTransforms.TransformCoordinate(new Coordinate(longitude, latitude));
                                loc.Latitude = latitude;
                                loc.Longitude = longitude;
                                loc.LongitudeM = mpoint.X;
                                loc.LatitudeM = mpoint.Y;
                                loc.FoundLocation = address;
                                loc.FoundResultType = resultType;
                                loc.ViewPort = MapTransforms.TransformViewport(viewport);
                                loc.FoundLevel = foundLevel;
                                UpdateDatabase(loc, inDatabase);
                            }
                            else
                            {
                                skipped++;
                            }
                        }
                    }
                    count++;
                    int percent = (int)Math.Truncate((count - 1) * 100.0 / total);
                    string status = "Previously geocoded: " + geocoded + ", skipped: " + skipped +
                                    ", Googled: " + googled + ". Done " + (count - 1) + " of " + total + ".  ";
                    worker.ReportProgress(percent, status);

                    if (worker.CancellationPending ||
                        ((txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text.Substring(0, 3).Equals("Max"))))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                ft.ClearLocations(); // Locations tab needs to be invalidated so it refreshes
                if (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text.Substring(0, 3).Equals("Max"))
                    MessageBox.Show("Finished Google Geocoding.\n" + txtGoogleWait.Text + "\nPlease wait 24hrs before trying again as Google\nwill not allow further geocoding before then.", "FTAnalyzer Geocoding");
                else
                    MessageBox.Show("Finished Google Geocoding.", "FTAnalyzer Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Google Geocoding : " + ex.Message, "FTAnalyzer Geocoding");
            }
        }

        private GeoResponse SearchGoogle(string location)
        {
            // This call is the real workhorse that does the actual Google lookup
            GeoResponse res = GoogleMap.GoogleGeocode(location, 8);
            if (res != null && res.Status == "Maxed")
            {
                googleGeocodeBackgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                res = null;
            }
            return res;
        }

        private void UpdateDatabase(FactLocation loc, bool inDatabase)
        {
            if (inDatabase)
                DatabaseHelper.Instance.UpdateGeocode(loc);
            else
                DatabaseHelper.Instance.InsertGeocode(loc);
            RefreshTreeNode(loc);
        }

        private void RefreshTreeNode(FactLocation loc)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshTreeNode(loc)));
                return;
            }
            FamilyTree.Instance.RefreshTreeNodeIcon(loc);
        }

        private void mnuGeocodeLocations_Click(object sender, EventArgs e)
        {
            StartGoogleGeoCoding(false);
        }

        #endregion

        private void updateChangesWithoutAskingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Ask to update database", updateChangesWithoutAskingToolStripMenuItem.Checked);
        }

        private void dgLocations_CellToolTipTextNeeded_1(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgLocations.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Double click to edit location.";
            }
        }

        private void mnuVerified_Click(object sender, EventArgs e)
        {
            FactLocation loc = dgLocations.CurrentRow.DataBoundItem as FactLocation;
            loc.GeocodeStatus = FactLocation.Geocode.GEDCOM_USER;
            UpdateDatabase(loc, true);
            dgLocations.Refresh();
        }

        private void mnuIncorrect_Click(object sender, EventArgs e)
        {
            FactLocation loc = dgLocations.CurrentRow.DataBoundItem as FactLocation;
            loc.GeocodeStatus = FactLocation.Geocode.INCORRECT;
            UpdateDatabase(loc, true);
            dgLocations.Refresh();
        }

        private void mnuNotSearched_Click(object sender, EventArgs e)
        {
            FactLocation loc = dgLocations.CurrentRow.DataBoundItem as FactLocation;
            loc.GeocodeStatus = FactLocation.Geocode.NOT_SEARCHED;
            loc.FoundLocation = string.Empty;
            loc.FoundResultType = string.Empty;
            loc.Latitude = 0d;
            loc.Longitude = 0d;
            loc.LatitudeM = 0d;
            loc.LongitudeM = 0d;
            loc.ViewPort.NorthEast.Lat = 0d;
            loc.ViewPort.NorthEast.Long = 0d;
            loc.ViewPort.SouthWest.Lat = 0d;
            loc.ViewPort.SouthWest.Long = 0d;
            loc.FoundLevel = -2;
            UpdateDatabase(loc, true);
            dgLocations.Refresh();
        }

        private void dgLocations_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgLocations.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                FactLocation loc = dgLocations.Rows[e.RowIndex].DataBoundItem as FactLocation;
                mnuCopyLocation.Enabled = loc.IsGeoCoded(false);
            }
        }

        private void mnuReverseGeocde_Click(object sender, EventArgs e)
        {
            StartReverseGeoCoding();
        }

        #region Reverse Geocoding

        public void AddLocationToQueue(FactLocation loc)
        {
            queue.Enqueue(loc);
            if (!ft.Geocoding)
                StartReverseGeoCoding();
        }

        public void StartReverseGeoCoding()
        {
            if (reverseGeocodeBackgroundWorker.IsBusy)
            {
                MessageBox.Show("A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FT Analyzer");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                pbGeocoding.Visible = true;
                mnuGoogleGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                txtLocations.Text = string.Empty;
                txtGoogleWait.Text = string.Empty;
                ft.Geocoding = true;
                DatabaseHelper.Instance.AddEmptyLocationsToQueue(queue);
                reverseGeocodeBackgroundWorker.RunWorkerAsync();
                this.Cursor = Cursors.Default;
            }
        }

        public void ReverseGeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                GoogleMap.ThreadCancelled = false;
                FactLocation loc;
                int count = 0;
                int total = queue.Count;
                DatabaseHelper dbh = DatabaseHelper.Instance;
                Dictionary<string, Tuple<string, string>> LatLongIndex = dbh.GetLatLongIndex();
                while (!queue.IsEmpty)
                {
                    if (queue.TryDequeue(out loc))
                    {
                        if (loc.ToString().Length > 0 && loc.Latitude != 0 && loc.Longitude != 0 && !loc.Country.Equals(Countries.AT_SEA))
                        {
                            GeoResponse res = null;
                            double latitude = loc.Latitude;
                            double longitude = loc.Longitude;
                            string hashkey = dbh.LatLongHashKey(latitude, longitude);
                            if (LatLongIndex.ContainsKey(hashkey))
                            {
                                loc.FoundLocation = LatLongIndex[hashkey].Item1;
                                loc.FoundResultType = LatLongIndex[hashkey].Item2;
                                UpdateDatabase(loc, true);
                            }
                            else
                            {
                                res = GoogleMap.GoogleReverseGeocode(latitude, longitude, 8);
                                if (res != null && res.Status == "Maxed")
                                {
                                    googleGeocodeBackgroundWorker.CancelAsync();
                                    GoogleMap.ThreadCancelled = true;
                                    res = null;
                                }
                                if (res != null && ((res.Status == "OK" && res.Results.Length > 0) || res.Status == "ZERO_RESULTS"))
                                {
                                    ProcessReverseResult(loc, res);
                                    UpdateDatabase(loc, true);
                                }
                            }
                        }
                    }
                    count++;
                    if (count > total) total = count; // incase main thread has added extra items to queue
                    int percent = (int)Math.Truncate(count * 100.0 / total);
                    string status = "Finding locations from Latitude/Longitude. Done " + count + " of " + total + ".  (Includes all blank locations in database)";
                    worker.ReportProgress(percent, status);

                    if (worker.CancellationPending ||
                        ((txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text.Substring(0, 3).Equals("Max"))))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                ft.ClearLocations(); // Locations tab needs to be invalidated so it refreshes
                if (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text.Substring(0, 3).Equals("Max"))
                    MessageBox.Show("Finished Reverse Geocoding.\n" + txtGoogleWait.Text + "\nPlease wait 24hrs before trying again as Google\nwill not allow further reverse geocoding before then.", "FTAnalyzer Geocoding");
                else
                    MessageBox.Show("Finished Reverse Geocoding.", "FTAnalyzer Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Reverse Geocoding : " + ex.Message, "FTAnalyzer Geocoding");
            }
        }

        public static void ProcessReverseResult(FactLocation loc, GeoResponse res)
        {
            int foundLevel = -1;
            GeoResponse.CResult.CGeometry.CViewPort viewport = new GeoResponse.CResult.CGeometry.CViewPort();
            if (res.Status == "OK")
            {
                LogResults(loc, res);
                foreach (GeoResponse.CResult result in res.Results)
                {
                    foundLevel = GoogleMap.GetFactLocation(result.Types);
                    viewport = result.Geometry.ViewPort;
                    string resultTypes = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                    if (((foundLevel == FactLocation.PLACE && loc.PixelSize < 10) ||
                         (foundLevel == FactLocation.ADDRESS && loc.PixelSize < 30) ||
                         (foundLevel == FactLocation.SUBREGION && loc.PixelSize < 100) ||
                         (foundLevel == loc.Level)) &&
                        (resultTypes != GoogleMap.POSTALCODE &&
                         resultTypes != GoogleMap.POSTALCODEPREFIX)) // prefer more detailed results than postal codes
                    {
                        loc.FoundLocation = result.ReturnAddress;
                        loc.FoundResultType = resultTypes;
                        log.Info("Decided to use: Pixelsize: " + loc.PixelSize + ", level: " + foundLevel + "=" + result.ReturnAddress + ". Type: " + resultTypes);
                        break;
                    }
                }
                if (loc.FoundLocation.Length == 0)
                {
                    // we haven't got a good match so try again with level <=
                    foreach (GeoResponse.CResult result in res.Results)
                    {
                        foundLevel = GoogleMap.GetFactLocation(result.Types);
                        viewport = result.Geometry.ViewPort;
                        if (foundLevel <= loc.Level)
                        {
                            loc.FoundLocation = result.ReturnAddress;
                            loc.FoundResultType = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                            break;
                        }
                    }
                }
            }
            else if (res.Status == "ZERO_RESULTS")
            {
                loc.FoundLocation = "Not Found";
                loc.FoundResultType = string.Empty;
            }
        }

        private static void LogResults(FactLocation loc, GeoResponse res)
        {
            log.Info("Pixelsize: " + loc.PixelSize + ", Found " + res.Results.Count() + " results for " + loc.ToString());
            foreach (GeoResponse.CResult result in res.Results)
            {
                log.Info("Level: " + GoogleMap.GetFactLocation(result.Types) + "=" + result.ReturnAddress + ". Type: " + EnhancedTextInfo.ConvertStringArrayToString(result.Types));
            }
        }
        #endregion

        private void mnuRetryPartial_Click(object sender, EventArgs e)
        {
            StartGoogleGeoCoding(true);
        }

        private void resetAllPartialMatchesToNotSearchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset all Partial Matches (Google, Level and Ord Surv) to not searched?",
                "Reset ALL Partials", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DatabaseHelper.Instance.ResetPartials();
                ft.LoadGeoLocationsFromDataBase(null);
                ft.WriteGeocodeStatstoRTB("Geocoding status after reset partials:");
                MessageBox.Show("Partials have been reset", "FT Analyzer");
            }
        }

        public void SelectLocation(FactLocation location)
        {
            Predicate<DataGridViewRow> condition = r => r.Cells["GeocodedLocation"].Value.ToString().Equals(location.SortableLocation);
            DataGridViewRow row = dgLocations.Rows.Cast<DataGridViewRow>().Filter(condition).FirstOrDefault();
            if (row == null)
            {
                dgLocations.DataSource = ApplyFilters(location);  // forces location to appear in list
                dgLocations.Refresh();
                row = dgLocations.Rows.Cast<DataGridViewRow>().Filter(condition).FirstOrDefault();
            }
            if (row != null)
            {
                dgLocations.Rows[row.Index].Selected = true;
                dgLocations.FirstDisplayedScrollingRowIndex = row.Index;
            }
        }

        private void GeocodeLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #region OS Geocoding
        public void StartOSGeoCoding()
        {
            if (googleGeocodeBackgroundWorker.IsBusy || OSGeocodeBackgroundWorker.IsBusy)
            {
                MessageBox.Show("A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FT Analyzer");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                mnuGoogleGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                mnuOSGeocodeLocations.Enabled = false;
                ft.Geocoding = true;
                txtLocations.Text = "Initialising OS Geocoding.";
                OSGeocodeBackgroundWorker.RunWorkerAsync();
                this.Cursor = Cursors.Default;
            }
        }

        private void OSGeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (LoadOS50kGazetteer())
            {
                ProcessOS50kGazetteerData(worker, e);
                MessageBox.Show("Finished Ordnance Survey Geocoding", "FTAnalyzer Geocoding");
            }
        }

        public bool LoadOS50kGazetteer()
        {
            if (OS50kDictionary != null)
                return true; // already loaded
            OS50kDictionary = new Dictionary<string, IList<OS50kGazetteer>>();
            OS50k = new List<OS50kGazetteer>();
            try
            {
                string startPath;
                if (Application.StartupPath.Contains("Common7\\IDE")) // running unit tests
                    startPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..");
                else
                    startPath = Application.StartupPath;
                string filename = Path.Combine(startPath, @"Resources\OS50kGazetteer.txt");
                if (File.Exists(filename))
                    ReadOS50kGazetteer(filename);
                return true;
            }
            catch (Exception e)
            {
                log.Warn("Failed to load OS50k Gazetteer error was : " + e.Message);
                MessageBox.Show("Failed to load OS50k Gazetteer error was : " + e.Message);
            }
            OS50kDictionary = null; // only reach here on exception so discard partially loaded file
            OS50k = null;
            return false;
        }

        public void ReadOS50kGazetteer(string filename)
        {
            Encoding isoWesternEuropean = Encoding.GetEncoding(28591);
            using (StreamReader reader = new StreamReader(filename, isoWesternEuropean))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.IndexOf(':') > 0)
                    {
                        OS50kGazetteer gaz = new OS50kGazetteer(line);
                        string key = gaz.DefinitiveName.ToLower();
                        IList<OS50kGazetteer> list = null;
                        if (!OS50kDictionary.TryGetValue(key, out list))
                        {
                            list = new List<OS50kGazetteer>();
                            OS50kDictionary.Add(key, list);
                        }
                        list.Add(gaz);
                        OS50k.Add(gaz);
                    }
                }
            }
            //CheckGazetteer();
        }

        public void CheckGazetteer()
        {
            List<string> endings = new List<string>();
            using (StreamWriter sw = new StreamWriter(@"C:\Maps\FTAnalyzer\endings.csv", false))
            {
                sw.WriteLine("Ending,PlaceName,CountyCode,CountyName,FeatureCode,Latitude,Longitude,ParishName");
                foreach(KeyValuePair<string, IList<OS50kGazetteer>> kvp in OS50kDictionary)
                {
                    string name = kvp.Key;
                    OS50kGazetteer gaz = kvp.Value[0];
                    if(name.LastIndexOf(" ") > 0 && name.LastIndexOf(" ") + 4 >= name.ToString().Length)
                    {
                        string ending = name.Substring(name.LastIndexOf(" ") + 1).Trim();
                        if (ending != "tor" && ending != "bay" && ending != "way" && ending != "law" && ending != "fen" && ending != "row" && ending != "lea"
                             && ending != "top" && ending != "ure" && ending != "end" && ending != "oak" && ending != "den" && ending != "dun" && ending != "lee"
                             && ending != "dam" && ending != "gap" && ending != "sea" && ending != "dee" && ending != "don" && ending != "dye" && ending != "bog"
                             && ending != "bar" && ending != "low" && ending != "mor" && ending != "ard" && ending != "ark" && ending != "ash" && ending != "ayr"
                             && ending != "ban" && ending != "bed" && ending != "beg" && ending != "box" && ending != "boy" && ending != "cap" && ending != "cat"
                             && ending != "dhu" && ending != "dod" && ending != "elm" && ending != "esk")
                        {
                            if (!endings.Contains(ending))
                                endings.Add(ending);
                            sw.WriteLine(ending + "," + gaz.DefinitiveName + "," + gaz.CountyCode + "," + gaz.CountyName + "," + gaz.FeatureCode + "," + gaz.Latitude + "," + gaz.Longitude + "," + gaz.ParishName);
                        }
                    }
                }
            }
        }

        private Dictionary<FactLocation, IList<OS50kGazetteer>> noCounty;

        public void ProcessOS50kGazetteerData(BackgroundWorker worker, DoWorkEventArgs e)
        {
            IEnumerable<FactLocation> toSearch = FactLocation.AllLocations;
            List<FactLocation> failedToFind = new List<FactLocation>();
            noCounty = new Dictionary<FactLocation, IList<OS50kGazetteer>>();
            int total = FactLocation.LocationsCount;
            int count = 0;
            int matched = 0;
            int previous = 0;
            int skipped = 0;
            foreach (FactLocation loc in toSearch)
            {
                // IsGeoCoded(true) will include OS_50KPARTIALS but we don't want to recheck them in normal operation ok for beta
                if (loc.IsGeoCoded(true) || loc.GeocodeStatus == FactLocation.Geocode.OS_50KPARTIAL)
                    previous++;
                else
                {
                    if (!Countries.IsUnitedKingdom(loc.Country) || loc.GeocodeStatus == FactLocation.Geocode.INCORRECT)
                        skipped++;
                    else
                    {
                        if (GazetteerMatchMethodA(loc))
                            matched++;
                        else if (GazetteerMatchMethodB(loc))
                            matched++;
                        else
                            failedToFind.Add(loc);
                    }
                }
                count++;

                int percent = (int)Math.Truncate((count - 1) * 100.0 / total);
                string status = "Previously geocoded: " + previous + ", skipped: " + skipped +
                                    ", OS matched: " + matched + ". Done " + (count - 1) + " of " + total + ".  ";
                worker.ReportProgress(percent, status);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
            if (MainForm.VERSION.Contains("beta"))
            {
                GenerateTestGedcom(failedToFind, "OS50k Gazetteer failed matches.ged", null);
                GenerateTestGedcom(null, "OS50k Gazetteer no Counties.ged", noCounty);
            }
        }

        private void GenerateTestGedcom(List<FactLocation> failedToFind, string name, Dictionary<FactLocation, IList<OS50kGazetteer>> noCounty)
        {
            if (Directory.Exists(Properties.MappingSettings.Default.CustomMapPath))
            {
                string filename = Path.Combine(Properties.MappingSettings.Default.CustomMapPath, name);
                using (StreamWriter stream = new StreamWriter(filename))
                {
                    stream.WriteLine("0 HEAD");
                    stream.WriteLine("0 @I@ INDI");
                    stream.WriteLine("1 NAME Test /Person/");
                    DateTime date = new DateTime(1800, 1, 1);
                    if (failedToFind != null)
                    {
                        foreach (FactLocation loc in failedToFind)
                        {
                            stream.WriteLine("1 RESI");
                            stream.WriteLine("2 DATE " + date.ToString("dd MMM yyyy").ToUpper());
                            stream.WriteLine("2 PLAC " + loc.ToString());
                            date = date.AddDays(1);
                        }
                    }
                    if (noCounty != null)
                    {
                        foreach (KeyValuePair<FactLocation, IList<OS50kGazetteer>> kvp in noCounty)
                        {
                            stream.WriteLine("1 RESI");
                            stream.WriteLine("2 DATE " + date.ToString("dd MMM yyyy").ToUpper());
                            stream.WriteLine("2 PLAC " + kvp.Key.ToString());
                            StringBuilder sb = new StringBuilder();
                            sb.Append("2 NOTE Found " + kvp.Value[0].DefinitiveName + " in ");
                            foreach (OS50kGazetteer gaz in kvp.Value)
                                sb.Append(gaz.CountyCode + ": " + gaz.CountyName + ", ");
                            stream.WriteLine(sb.ToString());
                            date = date.AddDays(1);
                        }
                    }
                }
            }
        }

        private bool GazetteerMatchMethodA(FactLocation loc)
        {
            if (CheckLocationMatch(loc.PlaceNoNumerics.ToLower(), loc))
                return true;
            if (CheckLocationMatch(loc.AddressNoNumerics.ToLower(), loc))
                return true;
            if (CheckLocationMatch(loc.SubRegion.ToLower(), loc))
                return true;
            log.Info("OS Geocoder Failed to match: " + loc.ToString());
            return false;
        }

        private bool GazetteerMatchMethodB(FactLocation loc)
        {
            if (loc.Level >= FactLocation.ADDRESS)
            {
                Predicate<OS50kGazetteer> match = x => (x.FuzzyMatch == loc.FuzzyMatch || x.FuzzyNoParishMatch == loc.FuzzyNoParishMatch) && x.IsCountyMatch(loc);
                List<OS50kGazetteer> results = OS50k.Filter(match).ToList<OS50kGazetteer>();
                if (results.Count > 0)
                {
                    if(loc.GeocodeStatus == FactLocation.Geocode.PARTIAL_MATCH || loc.GeocodeStatus == FactLocation.Geocode.LEVEL_MISMATCH)
                        return CheckNearest(loc, results);
                    else
                        SetOSGeocoding(loc, results[0], FactLocation.ADDRESS, true);
                    return true;
                }
                log.Info("OS Geocoder Fuzzy match Failed to match: " + loc.ToString());
            }
            return false;
        }

        private bool CheckNearest(FactLocation loc, List<OS50kGazetteer> results)
        {
            double minDistance = double.MaxValue;
            OS50kGazetteer selected = null;
            int foundLevel = loc.FoundLevel >= 0 ? loc.FoundLevel : loc.Level;
            foreach(OS50kGazetteer gaz in results)
            {
                Coordinate c = new Coordinate(loc.Longitude,loc.Latitude);
                double distance = GeoSpatialMath.GreatCircleDistance(loc.Longitude, loc.Latitude, gaz.Longitude, gaz.Latitude);
                if (distance < minDistance &&
                   (distance < 2500 ||
                    foundLevel == FactLocation.ADDRESS && distance < 5000 ||
                    foundLevel == FactLocation.SUBREGION && distance < 10000 ||
                    foundLevel == FactLocation.REGION && distance < 50000 ||
                    foundLevel == FactLocation.COUNTRY)) 
                {
                    selected = gaz;
                    minDistance = distance;
                }
            }
            if (selected != null)
            {
                SetOSGeocoding(loc, selected, FactLocation.ADDRESS, true);
                log.Info("Accepted " + selected.ToString() + " for " + loc.ToString() + ". Distance: " + minDistance + " Level: " + foundLevel);
            }
            return selected != null;
        }

        private bool CheckLocationMatch(string key, FactLocation loc)
        {
            IList<OS50kGazetteer> results = null;
            if (key.Length > 0 && OS50kDictionary.TryGetValue(key, out results))
            {
                IEnumerable<OS50kGazetteer> placeMatches = results.Filter(x => x.IsCountyMatch(loc));
                if (placeMatches.Count() > 0)
                    return ProcessOS50kMatches(placeMatches, loc, FactLocation.PLACE);
                else
                {
                    if (!noCounty.ContainsKey(loc))
                        noCounty.Add(loc, results);
                }
            }
            return false;
        }

        private bool ProcessOS50kMatches(IEnumerable<OS50kGazetteer> matches, FactLocation loc, int level)
        {
            int count = matches.Count();
            OS50kGazetteer gazA = matches.First<OS50kGazetteer>();
            if (count == 1)
            {  // we only have one match so its good
                SetOSGeocoding(loc, gazA, level, false);
                return true;
            }
            else
            {
                // we have several places of same name in matching county loop through matching check for matching parish
                foreach (OS50kGazetteer gaz in matches)
                {
                    if (gaz.ParishName.Equals(loc.SubRegion, StringComparison.InvariantCultureIgnoreCase))
                    {   // we match on parish name so we found a match on name, parish & county
                        SetOSGeocoding(loc, gaz, level, false);
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetOSGeocoding(FactLocation location, OS50kGazetteer gaz, int level, bool fuzzy)
        {
            int expandBy = 2500;
            Coordinate p = new Coordinate(gaz.Point.X, gaz.Point.Y);
            Envelope env = new Envelope(p, p);
            env.ExpandBy(expandBy); // OS50k is 1km sq expand by 2.5km to ensure text is visible.
            location.Latitude = gaz.Latitude;
            location.Longitude = gaz.Longitude;
            location.LatitudeM = gaz.Point.Y;
            location.LongitudeM = gaz.Point.X;
            location.ViewPort.NorthEast.Lat = env.Top();
            location.ViewPort.NorthEast.Long = env.Right();
            location.ViewPort.SouthWest.Lat = env.Bottom();
            location.ViewPort.SouthWest.Long = env.Left();
            location.PixelSize = (double)expandBy / 40.0;
            location.FoundLocation = string.Empty;
            if (fuzzy)
                location.GeocodeStatus = FactLocation.Geocode.OS_50KFUZZY;
            else if (level == location.Level)
                location.GeocodeStatus = FactLocation.Geocode.OS_50KMATCH;
            else if (level == FactLocation.ADDRESS)
                location.GeocodeStatus = FactLocation.Geocode.OS_50KMATCH;
            else
                location.GeocodeStatus = FactLocation.Geocode.OS_50KPARTIAL;
            location.FoundLevel = level;
            location.FoundLocation = gaz.ToString();
            location.FoundResultType = GoogleMap.OS_FEATURE;
            bool inDatabase = DatabaseHelper.Instance.IsLocationInDatabase(location.ToString());
            UpdateDatabase(location, inDatabase);
        }

        private void mnuOSGeocodeLocations_Click(object sender, EventArgs e)
        {
            StartOSGeoCoding();
        }
        #endregion

        #region OS Geocoding Threading
        private void OSGeocodeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OSGeoCode(OSGeocodeBackgroundWorker, e);
        }

        private void OSGeocodeBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbGeocoding.Visible = true;
            pbGeocoding.Value = (e.ProgressPercentage < 0) ? 1 : e.ProgressPercentage;
            txtLocations.Text = (string)e.UserState;
        }

        private void OSGeocodeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkFinished(sender);
        }
        #endregion
    }
}
