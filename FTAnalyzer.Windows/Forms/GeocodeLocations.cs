using FTAnalyzer.Events;
using FTAnalyzer.Filters;
using FTAnalyzer.Forms.Controls;
using FTAnalyzer.Mapping;
using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using NetTopologySuite.Geometries;
using SharpMap.Utilities;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace FTAnalyzer.Forms
{
    public partial class GeocodeLocations : Form
    {
        readonly FamilyTree ft;
        readonly Font italicFont;
        readonly ReportFormHelper reportFormHelper;
        readonly List<IDisplayGeocodedLocation> locations;
        bool formClosing;
        string statusText;
        bool refreshingMenus;
        HashSet<string> noneOfTheAbove;
        ToolStripMenuItem[] noneOfTheAboveMenus;
        readonly ConcurrentQueue<FactLocation> queue;
        Dictionary<string, IList<OS50kGazetteer>> OS50kDictionary;
        List<OS50kGazetteer> OS50k;
        readonly IProgress<string> outputText;

        FactLocation CopyLocation;

        public GeocodeLocations(IProgress<string> outputText)
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                ft = FamilyTree.Instance;
                refreshingMenus = false;
                locations = FamilyTree.AllGeocodingLocations;
                queue = new ConcurrentQueue<FactLocation>();
                CopyLocation = FactLocation.UNKNOWN_LOCATION;
                this.outputText = outputText;
                mnuPasteLocation.Enabled = false;
                dgLocations.AutoGenerateColumns = false;
                reportFormHelper = new ReportFormHelper(this, Text, dgLocations, ResetTable, "Geocode Locations");
                italicFont = new(dgLocations.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Italic);
                reportFormHelper.LoadColumnLayout("GeocodeLocationsColumns.xml");
                mnuGoogleGeocodeLocations.Enabled = !ft.Geocoding; // disable menu if already geocoding
                mnuEditLocation.Enabled = !ft.Geocoding;
                mnuReverseGeocode.Enabled = !ft.Geocoding;
                mnuOSGeocodeLocations.Enabled = !ft.Geocoding;
                SetupFilterMenu();
                SetStatusText();
                CheckGoogleStatusCodes(locations);
                UpdateGridWithFilters();
            }
            catch (Exception) { }
        }

        void SetStatusText()
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
                ", partials: " + (partial + levelpartial + ospartial) +
                ", notfound/incorrect: " + (notfound + incorrect + outofbounds) +
                ", yet to search: " + notsearched + " of " + total + " locations.";
            txtLocations.Text = statusText;
        }

        void SetupFilterMenu()
        {
            foreach (KeyValuePair<FactLocation.Geocode, string> item in FactLocation.GEOCODES.OrderBy(x => x.Value))
            {
                string geocode = item.Value;
                ToolStripMenuItem menu = new(geocode)
                {
                    Name = geocode,
                    Checked = Application.UserAppDataRegistry.GetValue(geocode, "True").Equals("True"),
                    CheckOnClick = true
                };
                menu.CheckedChanged += new EventHandler(MenuGeocode_CheckedChanged);
                menu.Image = FactLocationImage.ErrorIcon(item.Key).Icon;
                menu.TextImageRelation = TextImageRelation.TextBeforeImage;
                mnuGeocodeStatus.DropDownItems.Add(menu);
            }

            ToolStripMenuItem placesMenu = new("Places")
            {
                Name = "Places"
            };

            foreach (string resultType in GoogleMap.RESULT_TYPES)
            {
                ToolStripMenuItem menu = new(resultType)
                {
                    Name = resultType,
                    Checked = Application.UserAppDataRegistry.GetValue(resultType, "True").Equals("True"),
                    CheckOnClick = true
                };
                menu.CheckedChanged += new EventHandler(MenuResultType_CheckedChanged);
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

        bool AllFiltersActive(bool GoogleOnly)
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
            ToolStripMenuItem? places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
            if (places is not null)
            {
                foreach (ToolStripMenuItem menu in places.DropDownItems)
                {
                    if (menu.Text != "Select All" && menu.Text != "Clear All")
                    {
                        menus++;
                        if (menu.Checked)
                            count++;
                    }
                }
            }
            return count == menus;
        }

        void UpdateGridWithFilters()
        {
            Cursor = Cursors.WaitCursor;
            FactLocation? loc = dgLocations.CurrentRow is not null ? dgLocations.CurrentRow.DataBoundItem as FactLocation : null;
            SortableBindingList<IDisplayGeocodedLocation> filteredLocations = ApplyFilters(null);
            // store sort order
            DataGridViewColumn? sortCol = dgLocations.SortedColumn;
            ListSortDirection sortOrder = dgLocations.SortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            dgLocations.DataSource = filteredLocations;
            //restore sort order
            if (sortCol is not null)
                dgLocations.Sort(sortCol, sortOrder);
            dgLocations.Refresh();
            SetStatusText();
            txtLocations.Text = $"{statusText} Displaying: {dgLocations.RowCount}. ";
            if (loc is not null)
                SelectLocation(loc);
            Cursor = Cursors.Default;
        }

        bool StatusFilter(IDisplayGeocodedLocation loc)
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

        SortableBindingList<IDisplayGeocodedLocation> ApplyFilters(FactLocation mustDisplay)
        {
            if (AllFiltersActive(false))
                return new SortableBindingList<IDisplayGeocodedLocation>(locations);
            List<IDisplayGeocodedLocation> results = [];
            ToolStripMenuItem? places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
            ToolStripMenuItem[] list = new ToolStripMenuItem[places.DropDownItems.Count + mnuFoundResultType.DropDownItems.Count + noneOfTheAboveMenus.Length];
            mnuFoundResultType.DropDownItems.CopyTo(list, 0);
            places.DropDownItems.CopyTo(list, mnuFoundResultType.DropDownItems.Count);
            noneOfTheAboveMenus.CopyTo(list, mnuFoundResultType.DropDownItems.Count + places.DropDownItems.Count); // add any missing elements to always display them
            foreach (IDisplayGeocodedLocation loc in locations)
            {
                if (StatusFilter(loc) || (mustDisplay is not null && loc.Equals(mustDisplay)))
                {
                    if (loc.FoundResultType is null || loc.FoundResultType.Length == 0 || (mustDisplay is not null && loc.Equals(mustDisplay)))
                        results.Add(loc);
                    else
                    {
                        foreach (ToolStripMenuItem menu in list)
                        {
                            // filter locations on menu items that are ticked
                            if (menu.Checked && loc.FoundResultType.Contains(menu.Name ?? string.Empty))
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

        void CheckGoogleStatusCodes(List<IDisplayGeocodedLocation> input)
        {
            noneOfTheAbove = [];
            Dictionary<string, List<IDisplayGeocodedLocation>> results = [];
            foreach (IDisplayGeocodedLocation loc in input)
            {
                if (loc.FoundResultType.Length > 0)
                {
                    string[] parts = loc.FoundResultType.Split(',');
                    foreach (string part in parts)
                    {
                        string key = part.Trim();
                        if (!results.TryGetValue(key, out List<IDisplayGeocodedLocation>? value))
                        {
                            value = [];
                            results[key] = value;
                            if (!GoogleMap.RESULT_TYPES.Contains(key))
                                noneOfTheAbove.Add(key);
                        }

                        value.Add(loc);
                    }
                }
            }
            noneOfTheAboveMenus = new ToolStripMenuItem[noneOfTheAbove.Count];
            if (noneOfTheAbove.Count > 0)
            {
                int index = 0;
                foreach (string resultType in noneOfTheAbove)
                {
                    ToolStripMenuItem menu = new(resultType)
                    {
                        Name = resultType,
                        Checked = true
                    };
                    noneOfTheAboveMenus[index++] = menu;
                }
            }
        }

        void MenuGeocode_CheckedChanged(object? sender, EventArgs e)
        {
            if (!refreshingMenus)
                UpdateGeocodeStatusMenus();
        }

        void UpdateGeocodeStatusMenus()
        {
            foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters();
        }

        void MenuResultType_CheckedChanged(object? sender, EventArgs e)
        {
            if (!refreshingMenus)
                UpdateGoogleStatusMenus();
        }

        void UpdateGoogleStatusMenus()
        {
            foreach (ToolStripMenuItem menu in mnuFoundResultType.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters();
        }

        void MnuSelectClear_Click(object sender, EventArgs e)
        {
            refreshingMenus = true;
            ToolStripMenuItem? places = mnuFoundResultType.DropDownItems["Places"] as ToolStripMenuItem;
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

        void MnuStatusSelectAll_Click(object sender, EventArgs e)
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

        void ResetTable()
        {
            DataGridViewColumn? geoLocation = dgLocations.Columns["GeocodedLocation"];
            if (geoLocation != null)
                dgLocations.Sort(geoLocation, ListSortDirection.Ascending);
        }

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Locations report");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Facts_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = this.Text;

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplayGeocodedLocation>();

        void MnuResetColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("GeocodeLocationsColumns.xml");

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("GeocodeLocationsColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Geocode Locations");
        }

        void DgLocations_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                IDisplayGeocodedLocation? loc = (IDisplayGeocodedLocation?)dgLocations.Rows[e.RowIndex].DataBoundItem;
                e.ToolTipText = loc is null ? string.Empty : $"Geocoding status: {loc.Geocoded}";
            }
        }

        void DgLocations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ft.Geocoding && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cursor = Cursors.WaitCursor;
                FactLocation? loc = (FactLocation?)dgLocations.Rows[e.RowIndex].DataBoundItem;
                if (loc is not null)
                    EditLocation(loc);
            }
        }

        void MnuEditLocation_Click(object sender, EventArgs e)
        {
            if (!ft.Geocoding)
            {
                Cursor = Cursors.WaitCursor;
                FactLocation? loc = (FactLocation?)dgLocations.CurrentRow.DataBoundItem;
                if (loc is not null)
                    EditLocation(loc);
            }
        }

        void EditLocation(FactLocation loc)
        {
            try
            {
                EditLocation editform = new(loc);
                Cursor = Cursors.Default;
                mnuPasteLocation.Enabled = false;
                CopyLocation = FactLocation.UNKNOWN_LOCATION;
                editform.ShowDialog(this);
                if (editform.UserSavedPoint)
                    AddLocationToQueue(loc);  // we have edited the location so add reverse geocode to queue
                editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
                                    // force refresh of locations from new edited data
                dgLocations.Refresh();
            }
            catch (Exception) { }
        }

        void MnuCopyLocation_Click(object sender, EventArgs e)
        {
            CopyLocation = dgLocations.CurrentRow.DataBoundItem as FactLocation;
            mnuPasteLocation.Enabled = true;
        }

        void MnuPasteLocation_Click(object sender, EventArgs e)
        {
            if (CopyLocation.IsGeoCoded(false))
            {
                FactLocation? pasteLocation = (FactLocation?)dgLocations.CurrentRow.DataBoundItem;
                if (pasteLocation is not null)
                {
                    FactLocation.CopyLocationDetails(CopyLocation, pasteLocation);
                    UpdateDatabase(pasteLocation, true);
                    dgLocations.Refresh();
                }
            }
        }

        #region Google Geocode Threading

        void GoogleGeocodingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) => GoogleGeoCode(googleGeocodeBackgroundWorker, e);

        void GoogleGeocodingBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) => GeoCodingProgressChanged(e);

        void GeoCodingProgressChanged(ProgressChangedEventArgs e)
        {
            pbGeocoding.Visible = true;
            pbGeocoding.Value = (e.ProgressPercentage < 0) ? 1 : e.ProgressPercentage;
            txtLocations.Text = e.UserState.ToString() ?? string.Empty;
        }

        void GoogleGeocodingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => WorkFinished(sender);

        void WorkFinished(object sender)
        {
            pbGeocoding.Value = 100;
            pbGeocoding.Visible = false;
            txtGoogleWait.Text = string.Empty;
            mnuGoogleGeocodeLocations.Enabled = true;
            mnuEditLocation.Enabled = true;
            mnuReverseGeocode.Enabled = true;
            mnuOSGeocodeLocations.Enabled = true;
            mnuCheckEmptyViewPorts.Enabled = true;
            string title = sender == OSGeocodeBackgroundWorker ? "OS Geocoding Results:" : "Google Geocoding Results:";
            FamilyTree.WriteGeocodeStatstoRTB(title, outputText);
            ft.Geocoding = false;
            if (formClosing)
                Close();
            else
                UpdateGridWithFilters();
        }

        void GeocodeLocations_FormClosing(object sender, FormClosingEventArgs e)
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
            if (EmptyViewPortsBackgroundWorker.IsBusy)
            {
                EmptyViewPortsBackgroundWorker.CancelAsync();
                e.Cancel = true;
                formClosing = true;
            }
        }

        public void GoogleMap_WaitingForGoogle(object sender, GoogleWaitingEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => GoogleMap_WaitingForGoogle(sender, args)));
                return;
            }
            txtGoogleWait.Text = args.Message;
        }

        void ReverseGeocodeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) => ReverseGeoCode(reverseGeocodeBackgroundWorker, e);

        #endregion

        #region Google Geocoding

        void EmptyViewPortsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) => CheckEmptyViewPorts(EmptyViewPortsBackgroundWorker, e);

        public void StartGoogleGeoCoding(bool retryPartials)
        {
            try
            {
                if (googleGeocodeBackgroundWorker.IsBusy || OSGeocodeBackgroundWorker.IsBusy || EmptyViewPortsBackgroundWorker.IsBusy)
                {
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, "A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FTAnalyzer");
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        pbGeocoding.Visible = true;
                        mnuGoogleGeocodeLocations.Enabled = false;
                        mnuEditLocation.Enabled = false;
                        mnuReverseGeocode.Enabled = false;
                        mnuOSGeocodeLocations.Enabled = false;
                        mnuCheckEmptyViewPorts.Enabled = false;
                        ft.Geocoding = true;
                    }
                    catch (ArgumentException)
                    {
                        Debug.WriteLine("Race condition gets here sometimes");
                    }
                    googleGeocodeBackgroundWorker.RunWorkerAsync(retryPartials);
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // sometimes setting pbGeocoding.Visible triggers font error for resizing fonts
            }
        }

        void StartCheckEmptyViewPorts()
        {
            try
            {
                if (googleGeocodeBackgroundWorker.IsBusy || OSGeocodeBackgroundWorker.IsBusy || EmptyViewPortsBackgroundWorker.IsBusy)
                {
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, "A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FTAnalyzer");
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        pbGeocoding.Visible = true;
                        mnuGoogleGeocodeLocations.Enabled = false;
                        mnuEditLocation.Enabled = false;
                        mnuReverseGeocode.Enabled = false;
                        mnuOSGeocodeLocations.Enabled = false;
                        mnuCheckEmptyViewPorts.Enabled = false;
                        ft.Geocoding = true;
                    }
                    catch (ArgumentException)
                    {
                        Debug.WriteLine("Race condition gets here sometimes");
                    }
                    EmptyViewPortsBackgroundWorker.RunWorkerAsync();
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // sometimes setting pbGeocoding.Visible triggers font error for resizing fonts
            }
        }

        public void CheckEmptyViewPorts(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                DatabaseHelper dbh = DatabaseHelper.Instance;
                GoogleMap.ThreadCancelled = false;
                int vpchecked = 0;
                int updated = 0;
                int maxtoCheck = FactLocation.AllLocations.Count(x => x.EmptyViewPort);
                foreach (FactLocation loc in FactLocation.AllLocations.Where(x => x.EmptyViewPort).OrderBy(x => x.Level))
                {
                    if (loc != FactLocation.UNKNOWN_LOCATION && loc.Level <= FactLocation.REGION && loc.ToString().Length > 0)
                    {
                        vpchecked++;
                        GeoResponse? res = SearchGoogle(loc, loc.ToString());
                        Envelope bbox = Countries.BoundingBox(loc.Country);
                        if (res is not null && res.Status == "OK" && res.Results.Length > 0)
                        {
                            foreach (GeoResponse.CResult result in res.Results)
                            {
                                int foundLevel = GoogleMap.GetFactLocationType(result.Types, loc);
                                string address = result.ReturnAddress;
                                GeoResponse.CResult.CGeometry.CViewPort viewport = result.Geometry.ViewPort;
                                if (foundLevel == loc.Level && bbox.Covers(new Coordinate(result.Geometry.Location.Long, result.Geometry.Location.Lat)) &&
                                    !result.PartialMatch)
                                {
                                    loc.ViewPort = MapTransforms.TransformViewport(viewport);
                                    if (!loc.EmptyViewPort)
                                    {
                                        DatabaseHelper.UpdateGeocode(loc);
                                        updated++;
                                    }
                                }
                            }
                        }
                    }
                    int percent = (int)Math.Truncate(100.0 * vpchecked / maxtoCheck);
                    string status = $"Googled empty ViewPorts and have updated {updated}. Done {vpchecked} of {maxtoCheck}.  ";
                    worker.ReportProgress(percent, status);
                    if (worker.CancellationPending ||
                        (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text[..3].Equals("Max")))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(new Form() { TopMost = true }, $"Error Google Geocoding: {ex.Message}", "FTAnalyzer Geocoding");
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
                foreach (FactLocation loc in FactLocation.AllLocations.OrderBy(x => x.Level))
                {
                    if (loc == FactLocation.UNKNOWN_LOCATION || loc.IsGeoCoded(retryPartial))
                        geocoded++;
                    else if (loc.GeocodeStatus == FactLocation.Geocode.INCORRECT || loc.Country.Equals(Countries.AT_SEA))
                        skipped++; // don't re-geocode incorrect ones as that would reset incorrect flag back to what user already identified was wrong
                    else
                    {
                        bool inDatabase = DatabaseHelper.IsLocationInDatabase(loc.ToString());
                        if (loc.ToString().Length > 0)
                        {
                            GeoResponse? res = null;
                            if (loc.GeocodeStatus == FactLocation.Geocode.NOT_SEARCHED ||
                                (retryPartial &&
                                    (loc.GeocodeStatus == FactLocation.Geocode.PARTIAL_MATCH ||
                                     loc.GeocodeStatus == FactLocation.Geocode.LEVEL_MISMATCH ||
                                     loc.GeocodeStatus == FactLocation.Geocode.OS_50KPARTIAL)))
                            {
                                //                                log.Info("Searching Google for '" + loc.GoogleFixed + "' original text was '" + loc.GEDCOMLocation + "'.");
                                res = SearchGoogle(loc, loc.GoogleFixed);
                            }
                            if (res is not null && ((res.Status == "OK" && res.Results.Length > 0) || res.Status == "ZERO_RESULTS"))
                            {
                                double latitude = 0;
                                double longitude = 0;
                                int foundLevel = -1;
                                string address = string.Empty;
                                string resultType = string.Empty;
                                GeoResponse.CResult.CGeometry.CViewPort viewport = new();
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
                                            foundLevel = GoogleMap.GetFactLocationType(result.Types, loc);
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
                                                    //if (checkresultsPass == 2)
                                                    //log.Info("Geocoding found a match with " + loc.GEDCOMLocation + " previously failed with " + loc.GoogleFixed);
                                                    checkresultsPass = 3; // force exit
                                                    break; // we've got a good match so exit
                                                }
                                            }
                                        }
                                        if (loc.GeocodeStatus != FactLocation.Geocode.MATCHED && checkresultsPass == 1)
                                        {
                                            if (loc.OriginalText == loc.GoogleFixed)
                                                checkresultsPass++;  // if we have the same string skip checking Original Text location
                                            else
                                                res = SearchGoogle(loc, loc.OriginalText);
                                        }
                                        checkresultsPass++;
                                    }
                                    if (loc.GeocodeStatus != FactLocation.Geocode.MATCHED)
                                    {
                                        // we checked all the google results and no joy so take first result as level partial match
                                        foundLevel = GoogleMap.GetFactLocationType(originalResult.Types, loc);
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
                    string status = $"Previously geocoded: {geocoded}, skipped: {skipped}, Googled: {googled}. Done {count - 1} of {total}.  ";
                    worker.ReportProgress(percent, status);

                    if (worker.CancellationPending ||
                        (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text[..3].Equals("Max")))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                ft.ClearLocations(); // Locations tab needs to be invalidated so it refreshes
                if (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text[..3].Equals("Max"))
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, $"Finished Google Geocoding.\n{txtGoogleWait.Text}\nPlease wait 24hrs before trying again as Google\nwill not allow further geocoding before then.", "FTAnalyzer Geocoding");
                else
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, "Finished Google Geocoding.", "FTAnalyzer Geocoding");
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(new Form() { TopMost = true }, $"Error Google Geocoding: {ex.Message}", "FTAnalyzer Geocoding");
            }
        }

        GeoResponse? SearchGoogle(FactLocation location, string text)
        {
            // This call is the real workhorse that does the actual Google lookup
            GeoResponse? res = GoogleMap.GoogleGeocode(location, text, 8);
            if (res is not null && (res.Status == "Maxed" || res.Status == "REQUEST_DENIED"))
            {
                googleGeocodeBackgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                res = null;
            }
            return res;
        }

        void UpdateDatabase(FactLocation loc, bool inDatabase)
        {
            if (inDatabase)
                DatabaseHelper.UpdateGeocode(loc);
            else
                DatabaseHelper.InsertGeocode(loc);
            RefreshTreeNode(loc);
        }

        void RefreshTreeNode(FactLocation loc)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => RefreshTreeNode(loc)));
                return;
            }
            TreeViewHandler.Instance.RefreshTreeNodeIcon(loc);
        }

        void MnuGeocodeLocations_Click(object sender, EventArgs e) => StartGoogleGeoCoding(false);
        #endregion

        void UpdateChangesWithoutAskingToolStripMenuItem_Click(object sender, EventArgs e) =>
            Application.UserAppDataRegistry.SetValue("Ask to update database", updateChangesWithoutAskingToolStripMenuItem.Checked);

        void DgLocations_CellToolTipTextNeeded_1(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgLocations.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Double click to edit location.";
            }
        }

        void MnuVerified_Click(object sender, EventArgs e)
        {
            FactLocation? loc = (FactLocation?)dgLocations.CurrentRow.DataBoundItem;
            if (loc is not null)
            {
                loc.GeocodeStatus = FactLocation.Geocode.GEDCOM_USER;
                UpdateDatabase(loc, true);
                dgLocations.Refresh();
            }
        }

        void MnuIncorrect_Click(object sender, EventArgs e)
        {
            FactLocation? loc = (FactLocation?)dgLocations.CurrentRow.DataBoundItem;
            if (loc is not null)
            {
                loc.GeocodeStatus = FactLocation.Geocode.INCORRECT;
                UpdateDatabase(loc, true);
                dgLocations.Refresh();
            }
        }

        void MnuNotSearched_Click(object sender, EventArgs e)
        {
            FactLocation? loc = (FactLocation?)dgLocations.CurrentRow.DataBoundItem;
            if (loc is not null)
            {
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
        }

        void DgLocations_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgLocations.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                FactLocation? loc = (FactLocation?)dgLocations.Rows[e.RowIndex].DataBoundItem;
                if (loc is not null)
                    mnuCopyLocation.Enabled = loc.IsGeoCoded(false);
            }
        }

        void MnuReverseGeocde_Click(object sender, EventArgs e) => StartReverseGeoCoding();

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
                UIHelpers.ShowMessage(new Form() { TopMost = true }, "A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FTAnalyzer");
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                pbGeocoding.Visible = true;
                mnuGoogleGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                txtLocations.Text = string.Empty;
                txtGoogleWait.Text = string.Empty;
                ft.Geocoding = true;
                DatabaseHelper.AddEmptyLocationsToQueue(queue);
                reverseGeocodeBackgroundWorker.RunWorkerAsync();
                Cursor = Cursors.Default;
            }
        }

        public void ReverseGeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                GoogleMap.ThreadCancelled = false;
                int count = 0;
                int total = queue.Count;
                DatabaseHelper dbh = DatabaseHelper.Instance;
                Dictionary<string, Tuple<string, string>> LatLongIndex = DatabaseHelper.LatLongIndex;
                while (!queue.IsEmpty)
                {
                    if (queue.TryDequeue(out FactLocation? loc))
                    {
                        if (loc is not null && loc.ToString().Length > 0 &&
                            !ExtensionMethods.DoubleEquals(loc.Latitude, 0) &&
                            !ExtensionMethods.DoubleEquals(loc.Longitude, 0) &&
                            !loc.Country.Equals(Countries.AT_SEA))
                        {
                            GeoResponse? res = null;
                            double latitude = loc.Latitude;
                            double longitude = loc.Longitude;
                            string hashkey = DatabaseHelper.LatLongHashKey(latitude, longitude);
                            if (LatLongIndex.TryGetValue(hashkey, out Tuple<string, string>? value))
                            {
                                loc.FoundLocation = value.Item1;
                                loc.FoundResultType = value.Item2;
                                UpdateDatabase(loc, true);
                            }
                            else
                            {
                                res = GoogleMap.GoogleReverseGeocode(latitude, longitude, 8);
                                if (res is not null && res.Status == "Maxed")
                                {
                                    googleGeocodeBackgroundWorker.CancelAsync();
                                    GoogleMap.ThreadCancelled = true;
                                    res = null;
                                }
                                if (res is not null && ((res.Status == "OK" && res.Results.Length > 0) || res.Status == "ZERO_RESULTS"))
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
                    string status = $"Finding locations from Latitude/Longitude. Done {count} of {total}.  (Includes all blank locations in database)";
                    worker.ReportProgress(percent, status);

                    if (worker.CancellationPending ||
                        (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text[..3].Equals("Max")))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                ft.ClearLocations(); // Locations tab needs to be invalidated so it refreshes
                if (txtGoogleWait.Text.Length > 3 && txtGoogleWait.Text[..3].Equals("Max"))
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, $"Finished Reverse Geocoding.\n{txtGoogleWait.Text}\nPlease wait 24hrs before trying again as Google\nwill not allow further reverse geocoding before then.", "FTAnalyzer Geocoding");
                else
                    UIHelpers.ShowMessage(new Form() { TopMost = true }, "Finished Reverse Geocoding.", "FTAnalyzer Geocoding");
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(new Form() { TopMost = true }, "Error Reverse Geocoding : " + ex.Message, "FTAnalyzer Geocoding");
            }
        }

        public static void ProcessReverseResult(FactLocation loc, GeoResponse res)
        {
            int foundLevel;
            if (loc is null || res is null) return;
            GeoResponse.CResult.CGeometry.CViewPort viewport;
            if (res.Status == "OK")
            {
                //LogResults(loc, res);
                foreach (GeoResponse.CResult result in res.Results)
                {
                    foundLevel = GoogleMap.GetFactLocationType(result.Types, loc);
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
                        loc.ViewPort = MapTransforms.TransformViewport(viewport);
                        //log.Info("Decided to use: Pixelsize: " + loc.PixelSize + ", level: " + foundLevel + "=" + result.ReturnAddress + ". Type: " + resultTypes);
                        break;
                    }
                }
                if (loc.FoundLocation.Length == 0)
                {
                    // we haven't got a good match so try again with level <=
                    foreach (GeoResponse.CResult result in res.Results)
                    {
                        foundLevel = GoogleMap.GetFactLocationType(result.Types, loc);
                        viewport = result.Geometry.ViewPort;
                        if (foundLevel <= loc.Level)
                        {
                            loc.FoundLocation = result.ReturnAddress;
                            loc.FoundResultType = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                            loc.ViewPort = MapTransforms.TransformViewport(viewport);
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

        //      static void LogResults(FactLocation loc, GeoResponse res)
        //        {
        //log.Info("Pixelsize: " + loc.PixelSize + ", Found " + res.Results.Count() + " results for " + loc.ToString());
        //foreach (GeoResponse.CResult result in res.Results)
        //{
        //    log.Info("Level: " + GoogleMap.GetFactLocationType(result.Types, loc) + "=" + result.ReturnAddress + ". Type: " + EnhancedTextInfo.ConvertStringArrayToString(result.Types));
        //}
        //      }
        #endregion

        void MnuRetryPartial_Click(object sender, EventArgs e) => StartGoogleGeoCoding(true);

        void ResetAllPartialMatchesToNotSearchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = UIHelpers.ShowMessage("Are you sure you want to reset all Partial Matches (Google, Level and Ord Surv) to not searched?",
                "Reset ALL Partials", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DatabaseHelper.ResetPartials();
                if (FamilyTree.LoadGeoLocationsFromDataBase(outputText))
                {
                    FamilyTree.WriteGeocodeStatstoRTB("Geocoding status after reset partials:", outputText);
                    UIHelpers.ShowMessage("Partials have been reset", "FTAnalyzer");
                }
                else
                {
                    UIHelpers.ShowMessage("Problem loading Geocoded Locations from Database.", "FTAnalyzer");
                }
            }
        }

        public void SelectLocation(FactLocation location)
        {
            bool condition(DataGridViewRow r) => r.Cells["GeocodedLocation"].Value.ToString().Equals(location.SortableLocation);
            DataGridViewRow? row = dgLocations.Rows.Cast<DataGridViewRow>().Filter(condition).FirstOrDefault();
            if (row is null)
            {
                dgLocations.DataSource = ApplyFilters(location);  // forces location to appear in list
                dgLocations.Refresh();
                row = dgLocations.Rows.Cast<DataGridViewRow>().Filter(condition).FirstOrDefault();
            }
            if (row is not null)
            {
                dgLocations.Rows[row.Index].Selected = true;
                dgLocations.FirstDisplayedScrollingRowIndex = row.Index;
            }
        }

        #region OS Geocoding
        public void StartOSGeoCoding()
        {
            if (googleGeocodeBackgroundWorker.IsBusy || OSGeocodeBackgroundWorker.IsBusy)
            {
                UIHelpers.ShowMessage(new Form() { TopMost = true }, "A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.", "FTAnalyzer");
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                mnuGoogleGeocodeLocations.Enabled = false;
                mnuEditLocation.Enabled = false;
                mnuReverseGeocode.Enabled = false;
                mnuOSGeocodeLocations.Enabled = false;
                ft.Geocoding = true;
                txtLocations.Text = "Initialising OS Geocoding.";
                OSGeocodeBackgroundWorker.RunWorkerAsync();
                Cursor = Cursors.Default;
            }
        }

        void OSGeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (LoadOS50kGazetteer())
            {
                ProcessOS50kGazetteerData(worker, e);
                UIHelpers.ShowMessage(new Form() { TopMost = true }, "Finished Ordnance Survey Geocoding", "FTAnalyzer Geocoding");
            }
        }

        public bool LoadOS50kGazetteer()
        {
            if (OS50kDictionary is not null)
                return true; // already loaded
            OS50kDictionary = [];
            OS50k = [];
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
                //log.Warn("Failed to load OS50k Gazetteer error was : " + e.Message);
                UIHelpers.ShowMessage(new Form() { TopMost = true }, $"Failed to load OS50k Gazetteer error was : {e.Message}");
            }
            OS50kDictionary = null; // only reach here on exception so discard partially loaded file
            OS50k = null;
            return false;
        }

        public void ReadOS50kGazetteer(string filename)
        {
            Encoding isoWesternEuropean = Encoding.GetEncoding(28591);
            using StreamReader reader = new(filename, isoWesternEuropean);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine() ?? string.Empty;
                if (line.Contains(':'))
                {
                    OS50kGazetteer gaz = new(line);
                    string key = gaz.DefinitiveName.ToLower();
                    if (!OS50kDictionary.TryGetValue(key, out IList<OS50kGazetteer>? list))
                    {
                        list = [];
                        OS50kDictionary.Add(key, list);
                    }
                    list.Add(gaz);
                    OS50k.Add(gaz);
                }
            }
            //CheckGazetteer();
        }

        public void CheckGazetteer()
        {
            List<string> endings = [];
            using StreamWriter sw = new(@"C:\Maps\FTAnalyzer\endings.csv", false);
            sw.WriteLine("Ending,PlaceName,CountyCode,CountyName,FeatureCode,Latitude,Longitude,ParishName");
            foreach (KeyValuePair<string, IList<OS50kGazetteer>> kvp in OS50kDictionary)
            {
                string name = kvp.Key;
                OS50kGazetteer gaz = kvp.Value[0];
                if (name.LastIndexOf(' ') >= 0 && name.LastIndexOf(' ') + 4 >= name.ToString().Length)
                {
                    string ending = name[(name.LastIndexOf(' ') + 1)..].Trim();
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

        private Dictionary<FactLocation, IList<OS50kGazetteer>> noCounty;

        public void ProcessOS50kGazetteerData(BackgroundWorker worker, DoWorkEventArgs e)
        {
            IEnumerable<FactLocation> toSearch = FactLocation.AllLocations;
            List<FactLocation> failedToFind = [];
            noCounty = [];
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

        static void GenerateTestGedcom(List<FactLocation> failedToFind, string name, Dictionary<FactLocation, IList<OS50kGazetteer>> noCounty)
        {
            if (Directory.Exists(MappingSettings.Default.CustomMapPath))
            {
                string filename = Path.Combine(MappingSettings.Default.CustomMapPath, name);
                using StreamWriter stream = new(filename);
                stream.WriteLine("0 HEAD");
                stream.WriteLine("0 @I@ INDI");
                stream.WriteLine("1 NAME Test /Person/");
                DateTime date = new(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (failedToFind is not null)
                {
                    foreach (FactLocation loc in failedToFind)
                    {
                        stream.WriteLine("1 RESI");
                        stream.WriteLine("2 DATE " + date.ToString("dd MMM yyyy").ToUpper());
                        stream.WriteLine("2 PLAC " + loc.ToString());
                        date = date.AddDays(1);
                    }
                }
                if (noCounty is not null)
                {
                    foreach (KeyValuePair<FactLocation, IList<OS50kGazetteer>> kvp in noCounty)
                    {
                        stream.WriteLine("1 RESI");
                        stream.WriteLine("2 DATE " + date.ToString("dd MMM yyyy").ToUpper());
                        stream.WriteLine("2 PLAC " + kvp.Key.ToString());
                        StringBuilder sb = new();
                        sb.Append("2 NOTE Found " + kvp.Value[0].DefinitiveName + " in ");
                        foreach (OS50kGazetteer gaz in kvp.Value)
                            sb.Append(gaz.CountyCode + ": " + gaz.CountyName + ", ");
                        stream.WriteLine(sb.ToString());
                        date = date.AddDays(1);
                    }
                }
            }
        }

        bool GazetteerMatchMethodA(FactLocation loc)
        {
            if (CheckLocationMatch(loc.PlaceNoNumerics.ToLower(), loc))
                return true;
            if (CheckLocationMatch(loc.AddressNoNumerics.ToLower(), loc))
                return true;
            if (CheckLocationMatch(loc.SubRegion.ToLower(), loc))
                return true;
            //log.Info("OS Geocoder Failed to match: " + loc.ToString());
            return false;
        }

        bool GazetteerMatchMethodB(FactLocation loc)
        {
            if (loc.Level >= FactLocation.ADDRESS)
            {
                bool match(OS50kGazetteer x) => (x.FuzzyMatch == loc.FuzzyMatch || x.FuzzyNoParishMatch == loc.FuzzyNoParishMatch) && x.IsCountyMatch(loc);
                List<OS50kGazetteer> results = [.. OS50k.Filter(match)];
                if (results.Count > 0)
                {
                    if (loc.GeocodeStatus == FactLocation.Geocode.PARTIAL_MATCH || loc.GeocodeStatus == FactLocation.Geocode.LEVEL_MISMATCH)
                        return CheckNearest(loc, results);
                    else
                        SetOSGeocoding(loc, results[0], FactLocation.ADDRESS, true);
                    return true;
                }
                //log.Info("OS Geocoder Fuzzy match Failed to match: " + loc.ToString());
            }
            return false;
        }

        bool CheckNearest(FactLocation loc, List<OS50kGazetteer> results)
        {
            double minDistance = double.MaxValue;
            OS50kGazetteer? selected = null;
            int foundLevel = loc.FoundLevel >= 0 ? loc.FoundLevel : loc.Level;
            foreach (OS50kGazetteer gaz in results)
            {
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
            if (selected is not null)
            {
                SetOSGeocoding(loc, selected, FactLocation.ADDRESS, true);
                //log.Info("Accepted " + selected.ToString() + " for " + loc.ToString() + ". Distance: " + minDistance + " Level: " + foundLevel);
            }
            return selected is not null;
        }

        bool CheckLocationMatch(string key, FactLocation loc)
        {
            if (key.Length > 0 && OS50kDictionary.TryGetValue(key, out IList<OS50kGazetteer>? results))
            {
                IEnumerable<OS50kGazetteer> placeMatches = results.Filter(x => x.IsCountyMatch(loc));
                if (placeMatches.Any())
                    return ProcessOS50kMatches(placeMatches, loc, FactLocation.PLACE);
                else
                {
                    noCounty.TryAdd(loc, results);
                }
            }
            return false;
        }

        bool ProcessOS50kMatches(IEnumerable<OS50kGazetteer> matches, FactLocation loc, int level)
        {
            int count = matches.Count();
            OS50kGazetteer gazA = matches.First();
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

        void SetOSGeocoding(FactLocation location, OS50kGazetteer gaz, int level, bool fuzzy)
        {
            int expandBy = 2500;
            Coordinate p = new(gaz.Point.X, gaz.Point.Y);
            Envelope env = new(p, p);
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
            bool inDatabase = DatabaseHelper.IsLocationInDatabase(location.ToString());
            UpdateDatabase(location, inDatabase);
        }

        void MnuOSGeocodeLocations_Click(object sender, EventArgs e) => StartOSGeoCoding();
        #endregion

        #region OS Geocoding Threading
        void OSGeocodeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) => OSGeoCode(OSGeocodeBackgroundWorker, e);

        void OSGeocodeBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) => GeoCodingProgressChanged(e);

        void OSGeocodeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => WorkFinished(sender);
        #endregion
        void MnuCheckForEmptyViewPortsToolStripMenuItem_Click(object sender, EventArgs e) => StartCheckEmptyViewPorts();

        void GeocodeLocations_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void GeocodeLocations_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
