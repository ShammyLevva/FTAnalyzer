using System;
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
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using SharpMap;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Rendering;
using SharpMap.Styles;

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
            reportFormHelper = new ReportFormHelper(this.Text, dgLocations, this.ResetTable);
            italicFont = new Font(dgLocations.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("GeocodeLocationsColumns.xml");
            mnuGeocodeLocations.Enabled = !ft.Geocoding; // disable menu if already geocoding
            mnuEditLocation.Enabled = !ft.Geocoding;
            mnuReverseGeocode.Enabled = !ft.Geocoding;
            SetupFilterMenu();
            SetStatusText();
            CheckGoogleStatusCodes(locations);
            UpdateGridWithFilters();
        }

        private void SetStatusText()
        {
            int gedcom = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER));
            int found = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED));
            int partial = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH));
            int levelpartial = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.LEVEL_MISMATCH));
            int notsearched = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED));
            int notfound = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NO_MATCH));
            int outofbounds = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.OUT_OF_BOUNDS));
            int incorrect = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.INCORRECT));
            int total = FactLocation.LocationsCount;

            txtGoogleWait.Text = string.Empty;
            statusText = "Already Geocoded: " + (gedcom + found) + ", partials: " + (partial + levelpartial + notfound + incorrect + outofbounds) + ", yet to search: " + notsearched + " of " + total + " locations.";
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
                    mnuGoogleResultType.DropDownItems.Add(menu);
            }
            mnuGoogleResultType.DropDownItems.Add(placesMenu);
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
            foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
            {
                if (menu.Text != "Places" && menu.Text != "Select All" && menu.Text != "Clear All")
                {
                    menus++;
                    if (menu.Checked)
                        count++;
                }
            }
            ToolStripMenuItem places = mnuGoogleResultType.DropDownItems["Places"] as ToolStripMenuItem;
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

        private void UpdateGridWithFilters()
        {
            this.Cursor = Cursors.WaitCursor;
            SortableBindingList<IDisplayGeocodedLocation> filteredLocations = ApplyFilters(null);
            // store sort order
            DataGridViewColumn sortCol = dgLocations.SortedColumn;
            ListSortDirection sortOrder = dgLocations.SortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            dgLocations.DataSource = filteredLocations;
            //restore sort order
            if (sortCol != null)
                dgLocations.Sort(sortCol, sortOrder);
            dgLocations.Refresh();
            txtLocations.Text = statusText + " Displaying: " + dgLocations.RowCount;
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
            ToolStripMenuItem places = mnuGoogleResultType.DropDownItems["Places"] as ToolStripMenuItem;
            ToolStripMenuItem[] list = new ToolStripMenuItem[places.DropDownItems.Count + mnuGoogleResultType.DropDownItems.Count + noneOfTheAboveMenus.Count()];
            mnuGoogleResultType.DropDownItems.CopyTo(list, 0);
            places.DropDownItems.CopyTo(list, mnuGoogleResultType.DropDownItems.Count);
            noneOfTheAboveMenus.CopyTo(list, mnuGoogleResultType.DropDownItems.Count + places.DropDownItems.Count); // add any missing elements to always display them
            foreach (IDisplayGeocodedLocation loc in locations)
            {
                if (StatusFilter(loc) || (mustDisplay != null && loc.Equals(mustDisplay)))
                {
                    if (loc.GoogleResultType == null || loc.GoogleResultType.Length == 0 || (mustDisplay != null && loc.Equals(mustDisplay)))
                        results.Add(loc);
                    else
                    {
                        foreach (ToolStripMenuItem menu in list)
                        {
                            // filter locations on menu items that are ticked
                            if (menu.Checked && loc.GoogleResultType.Contains(menu.Name))
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
                if (loc.GoogleResultType.Length > 0)
                {
                    string[] parts = loc.GoogleResultType.Split(',');
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
            UpdateGridWithFilters();
        }

        private void menuResultType_CheckedChanged(object sender, EventArgs e)
        {
            if (!refreshingMenus)
                UpdateGoogleStatusMenus();
        }

        private void UpdateGoogleStatusMenus()
        {
            foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters();
        }

        private void mnuSelectClear_Click(object sender, EventArgs e)
        {
            refreshingMenus = true;
            ToolStripMenuItem places = mnuGoogleResultType.DropDownItems["Places"] as ToolStripMenuItem;
            if (mnuSelectClear.Text.Equals("Clear All"))
            {
                mnuSelectClear.Text = "Select All";
                foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
                    menu.Checked = false;
                foreach (ToolStripMenuItem menu in places.DropDownItems)
                    menu.Checked = false;
            }
            else
            {
                mnuSelectClear.Text = "Clear All";
                foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
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
            reportFormHelper.PrintReport(this);
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport(this);
        }

        private void Facts_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayGeocodedLocation>(this);
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("GeocodeLocationsColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("GeocodeLocationsColumns.xml");
            MessageBox.Show("Column Settings Saved", "Geocode Locations");
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

        #region Geocode Threading
        private void geocodingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GeoCode(geocodeBackgroundWorker, e);
        }

        private void geocodingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbGeocoding.Value = e.ProgressPercentage;
            txtLocations.Text = (string)e.UserState;
        }

        private void geocodingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbGeocoding.Value = 100;
            pbGeocoding.Visible = false;
            txtGoogleWait.Text = string.Empty;
            mnuGeocodeLocations.Enabled = true;
            mnuEditLocation.Enabled = true;
            mnuReverseGeocode.Enabled = true;
            if (sender == geocodeBackgroundWorker)
                ft.WriteGeocodeStatstoRTB(true);
            ft.Geocoding = false;
            if (formClosing)
                this.Close();
            else
                dgLocations.Refresh();
        }

        private void GeocodeLocations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (geocodeBackgroundWorker.IsBusy)
            {
                geocodeBackgroundWorker.CancelAsync();
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

        public void StartGeoCoding(bool retryPartials)
        {
            if (geocodeBackgroundWorker.IsBusy)
            {
                MessageBox.Show("A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FT Analyzer");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                pbGeocoding.Visible = true;
                mnuGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                ft.Geocoding = true;
                geocodeBackgroundWorker.RunWorkerAsync(retryPartials);
                this.Cursor = Cursors.Default;
            }
        }

        public void GeoCode(BackgroundWorker worker, DoWorkEventArgs e)
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
                    else if (loc.GeocodeStatus == FactLocation.Geocode.INCORRECT)
                        skipped++; // don't re-geocode incorrect ones as that would reset incorrect flag back to what user already identified was wrong
                    else
                    {
                        bool inDatabase = dbh.IsLocationInDatabase(loc.ToString());
                        if (loc.ToString().Length > 0)
                        {
                            GeoResponse res = null;
                            if (loc.GeocodeStatus == FactLocation.Geocode.NOT_SEARCHED ||
                                (retryPartial &&
                                    (loc.GeocodeStatus == FactLocation.Geocode.PARTIAL_MATCH || loc.GeocodeStatus == FactLocation.Geocode.LEVEL_MISMATCH)))
                            {
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
                                                res = SearchGoogle(loc.GEDCOMLocation);
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
                                loc.GoogleLocation = address;
                                loc.GoogleResultType = resultType;
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
                                    " Googled: " + googled + ". Done " + (count - 1) + " of " + total + ".  ";
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
                    MessageBox.Show("Finished Geocoding.\n" + txtGoogleWait.Text + "\nPlease wait 24hrs before trying again as Google\nwill not allow further geocoding before then.", "Timeline Geocoding");
                else
                    MessageBox.Show("Finished Geocoding.", "Timeline Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error geocoding : " + ex.Message, "FT Analyzer");
            }
        }

        private GeoResponse SearchGoogle(string location)
        {
            // This call is the real workhorse that does the actual Google lookup
            GeoResponse res = GoogleMap.GoogleGeocode(location, 8);
            if (res != null && res.Status == "Maxed")
            {
                geocodeBackgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                res = null;
            }
            return res;
        }

        private static void UpdateDatabase(FactLocation loc, bool inDatabase)
        {
            DatabaseHelper dbh = DatabaseHelper.Instance;
            if (inDatabase)
                dbh.UpdateGeocode(loc);
            else
            {
                dbh.InsertGeocode(loc);
            }
        }

        private void mnuGeocodeLocations_Click(object sender, EventArgs e)
        {
            StartGeoCoding(false);
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
            loc.GoogleLocation = string.Empty;
            loc.GoogleResultType = string.Empty;
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
                mnuGeocodeLocations.Enabled = false;
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
                while (!queue.IsEmpty)
                {
                    if (queue.TryDequeue(out loc))
                    {
                        if (loc.ToString().Length > 0)
                        {
                            GeoResponse res = null;
                            double latitude = loc.Latitude;
                            double longitude = loc.Longitude;
                            res = GoogleMap.GoogleReverseGeocode(latitude, longitude, 8);
                            if (res != null && res.Status == "Maxed")
                            {
                                geocodeBackgroundWorker.CancelAsync();
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
                    count++;
                    if (count > total) total = count; // incase main thread has added extra items to queue
                    int percent = (int)Math.Truncate(count * 100.0 / total);
                    string status = "Looking up location names from Latitude/Longitude. Done " + count + " of " + total + ".  ";
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
                    MessageBox.Show("Finished Reverse Geocoding.\n" + txtGoogleWait.Text + "\nPlease wait 24hrs before trying again as Google\nwill not allow further reverse geocoding before then.", "Timeline Geocoding");
                else
                    MessageBox.Show("Finished Reverse Geocoding.", "Timeline Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reverse geocoding : " + ex.Message, "FT Analyzer");
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
                        loc.GoogleLocation = result.ReturnAddress;
                        loc.GoogleResultType = resultTypes;
                        log.Info("Decided to use: Pixelsize: " + loc.PixelSize + ", level: " + foundLevel + "=" + result.ReturnAddress + ". Type: " + resultTypes);
                        break;
                    }
                }
                if (loc.GoogleLocation.Length == 0)
                {
                    // we haven't got a good match so try again with level <=
                    foreach (GeoResponse.CResult result in res.Results)
                    {
                        foundLevel = GoogleMap.GetFactLocation(result.Types);
                        viewport = result.Geometry.ViewPort;
                        if (foundLevel <= loc.Level)
                        {
                            loc.GoogleLocation = result.ReturnAddress;
                            loc.GoogleResultType = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                            break;
                        }
                    }
                }
            }
            else if (res.Status == "ZERO_RESULTS")
            {
                loc.GoogleLocation = "Not Found";
                foundLevel = -2;
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
            StartGeoCoding(true);
        }

        private void resetAllPartialMatchesToNotSearchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset all Google Partials and Level Mismatch Partials to not searched?",
                "Reset ALL Partials", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DatabaseHelper.Instance.ResetPartials();
                ft.LoadGeoLocationsFromDataBase();
                ft.WriteGeocodeStatstoRTB(true);
                MessageBox.Show("Partials have been reset", "FT Analyzer");
            }
        }

        public void SelectLocation(FactLocation location)
        {
            DataGridViewRow row = dgLocations.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["GeocodedLocation"].Value.ToString().Equals(location.SortableLocation)).FirstOrDefault();
            if (row == null)
            {
                dgLocations.DataSource = ApplyFilters(location);  // forces location to appear in list
                dgLocations.Refresh();
                row = dgLocations.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["GeocodedLocation"].Value.ToString().Equals(location.SortableLocation)).FirstOrDefault();
            }
            if(row != null)
            {
                dgLocations.Rows[row.Index].Selected = true;
                dgLocations.FirstDisplayedScrollingRowIndex = row.Index;
            }
        }

        private void GeocodeLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
