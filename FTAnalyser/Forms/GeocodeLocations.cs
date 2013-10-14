using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer;
using FTAnalyzer.Utilities;
using FTAnalyzer.Mapping;
using FTAnalyzer.Events;
using System.Data.SQLite;

namespace FTAnalyzer.Forms
{
    public partial class GeocodeLocations : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Font italicFont;
        private ReportFormHelper reportFormHelper;
        private List<IDisplayGeocodedLocation> locations;
        private bool formClosing;
        private string statusText;

        public GeocodeLocations()
        {
            InitializeComponent();
            ft = FamilyTree.Instance;
            this.locations = ft.AllGeocodingLocations;
            dgLocations.AutoGenerateColumns = false;
            reportFormHelper = new ReportFormHelper(this.Text, dgLocations, this.ResetTable);
            italicFont = new Font(dgLocations.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("GeocodeLocationsColumns.xml");
            mnuGeocodeLocations.Enabled = !ft.Geocoding; // disable menu if already geocoding
            SetupFilterMenu();
            SetStatusText();
            UpdateGridWithFilters(locations);
        }

        private void SetStatusText()
        {
            int gedcom = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER));
            int found = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED));
            int notfound = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH));
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)) - 1);
            int total = FactLocation.AllLocations.Count() - 1;

            txtGoogleWait.Text = string.Empty;
            statusText = "Already Geocoded: " + (gedcom + found) + ", not found: " + notfound + " yet to search: " + notsearched + " of " + total + " locations.";
            txtLocations.Text = statusText;
        }

        private void SetupFilterMenu()
        {
            foreach (string geocode in FactLocation.Geocodes.Values)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(geocode);
                menu.Name = geocode;
                menu.Checked = Application.UserAppDataRegistry.GetValue(geocode, "True").Equals("True");
                menu.CheckOnClick = true;
                menu.CheckedChanged += new EventHandler(menuGeocode_CheckedChanged);
                mnuGeocodeStatus.DropDownItems.Add(menu);
            }

            foreach (string resultType in GoogleMap.RESULT_TYPES)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(resultType);
                menu.Name = resultType;
                menu.Checked = Application.UserAppDataRegistry.GetValue(resultType, "True").Equals("True");
                menu.CheckOnClick = true;
                menu.CheckedChanged += new EventHandler(menuResultType_CheckedChanged);
                mnuGoogleResultType.DropDownItems.Add(menu);
            }
        }

        private void UpdateGridWithFilters(List<IDisplayGeocodedLocation> input)
        {
            this.Cursor = Cursors.WaitCursor;
            SortableBindingList<IDisplayGeocodedLocation> filteredLocations = ApplyFilters(input);
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

        private SortableBindingList<IDisplayGeocodedLocation> ApplyFilters(List<IDisplayGeocodedLocation> input)
        {
            List<IDisplayGeocodedLocation> results = new List<IDisplayGeocodedLocation>();
            foreach (IDisplayGeocodedLocation loc in input)
            {
                if (StatusFilter(loc))
                {
                    if (loc.GoogleResultType == null || loc.GoogleResultType.Length == 0)
                        results.Add(loc);
                    else
                    {
                        foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
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

        private void menuGeocode_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in mnuGeocodeStatus.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters(locations);
        }

        private void menuResultType_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menu in mnuGoogleResultType.DropDownItems)
            {
                Application.UserAppDataRegistry.SetValue(menu.Name, menu.Checked.ToString()); // remember checked state for next time
            }
            UpdateGridWithFilters(locations);
        }

        private void ResetTable()
        {
            dgLocations.AutoResizeColumns();
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
                EditLocation editform = new EditLocation(loc);
                this.Cursor = Cursors.Default;
                DialogResult result = editform.ShowDialog(this);
                editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
                // force refresh of locations from new edited data
                UpdateGridWithFilters(locations);
                for (int i = 0; i < dgLocations.RowCount; i++)
                {
                    dgLocations.Rows[i].Selected = (dgLocations.Rows[i].DataBoundItem.Equals(loc));
                }
                dgLocations.FirstDisplayedScrollingRowIndex = dgLocations.SelectedRows[0].Index;
            }
        }

        #region Threading
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GeoCode(backgroundWorker, e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbGeocoding.Value = e.ProgressPercentage;
            txtLocations.Text = (string)e.UserState;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbGeocoding.Value = 100;
            pbGeocoding.Visible = false;
            txtGoogleWait.Text = string.Empty;
            mnuGeocodeLocations.Enabled = true;
            ft.Geocoding = false;
            if (formClosing)
                this.Close();
            else
                dgLocations.Refresh();
        }

        private void GeocodeLocations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
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


        #endregion

        #region Google Geocoding

        public void StartGeoCoding()
        {
            if (backgroundWorker.IsBusy)
            {
                MessageBox.Show("A previous Geocoding session didn't complete correctly.\nYou may need to wait or restart program to fix this.");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                pbGeocoding.Visible = true;
                mnuGeocodeLocations.Enabled = false;
                ft.Geocoding = true;
                backgroundWorker.RunWorkerAsync();
                this.Cursor = Cursors.Default;
            }
        }

        public void GeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                DatabaseHelper dbh = DatabaseHelper.Instance;
                SQLiteCommand cmd = dbh.GetLocation();
                SQLiteCommand insertCmd = dbh.InsertGeocode();
                SQLiteCommand updateCmd = dbh.UpdateGeocode();

                int count = 0;
                int good = 0;
                int bad = 0;
                int geocoded = 0;
                int skipped = 0;
                int total = FactLocation.AllLocations.Count() - 1;
                GoogleMap.ThreadCancelled = false;

                foreach (FactLocation loc in FactLocation.AllLocations)
                {
                    if (loc.IsGeoCoded)
                    {
                        geocoded++;
                        //Console.WriteLine("Already Geocoded : " + loc.ToString());
                    }
                    else
                    {
                        cmd.Parameters[0].Value = loc.ToString();
                        SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                        bool inDatabase = reader.Read();
                        if (loc.ToString().Length > 0)
                        {
                            GeoResponse res = null;
                            if (!(!mnuRetryPartial.Checked && inDatabase))
                            {
                                res = GoogleMap.CallGeoWSCount(loc.ToString(), 8);
                                if (res != null && res.Status == "Maxed")
                                {
                                    backgroundWorker.CancelAsync();
                                    GoogleMap.ThreadCancelled = true;
                                    res = null;
                                }
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
                                if (res.Status == "OK")
                                {
                                    foreach (GeoResponse.CResult result in res.Results)
                                    {
                                        foundLevel = GoogleMap.GetFactLocation(result.Types);
                                        address = result.ReturnAddress;
                                        viewport = result.Geometry.ViewPort;
                                        resultType = EnhancedTextInfo.ConvertStringArrayToString(result.Types);
                                        if (foundLevel >= loc.Level)
                                        {
                                            latitude = result.Geometry.Location.Lat;
                                            longitude = result.Geometry.Location.Long;
                                            loc.GeocodeStatus = FactLocation.Geocode.MATCHED;
                                            loc.ViewPort = viewport;
                                            good++;
                                            break;
                                        }
                                    }
                                    if (loc.GeocodeStatus != FactLocation.Geocode.MATCHED)
                                    {   // we checked all the google results and no joy so take first result as partial match
                                        latitude = res.Results[0].Geometry.Location.Lat;
                                        longitude = res.Results[0].Geometry.Location.Long;
                                        viewport = res.Results[0].Geometry.ViewPort;
                                        resultType = EnhancedTextInfo.ConvertStringArrayToString(res.Results[0].Types);
                                        loc.GeocodeStatus = FactLocation.Geocode.PARTIAL_MATCH;
                                        bad++;
                                    }

                                }
                                else if (res.Status == "ZERO_RESULTS")
                                {
                                    skipped++;
                                    foundLevel = -2;

                                }
                                if (inDatabase)
                                {
                                    updateCmd.Parameters[0].Value = loc.Level;
                                    updateCmd.Parameters[1].Value = latitude;
                                    updateCmd.Parameters[2].Value = longitude;
                                    updateCmd.Parameters[3].Value = address;
                                    updateCmd.Parameters[4].Value = foundLevel;
                                    updateCmd.Parameters[5].Value = viewport.NorthEast.Lat;
                                    updateCmd.Parameters[6].Value = viewport.NorthEast.Long;
                                    updateCmd.Parameters[7].Value = viewport.SouthWest.Lat;
                                    updateCmd.Parameters[8].Value = viewport.SouthWest.Long;
                                    updateCmd.Parameters[9].Value = loc.GeocodeStatus;
                                    updateCmd.Parameters[10].Value = resultType;
                                    updateCmd.Parameters[11].Value = loc.ToString();
                                    updateCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    insertCmd.Parameters[0].Value = loc.ToString();
                                    insertCmd.Parameters[1].Value = loc.Level;
                                    insertCmd.Parameters[2].Value = latitude;
                                    insertCmd.Parameters[3].Value = longitude;
                                    insertCmd.Parameters[4].Value = address;
                                    insertCmd.Parameters[5].Value = foundLevel;
                                    insertCmd.Parameters[6].Value = viewport.NorthEast.Lat;
                                    insertCmd.Parameters[7].Value = viewport.NorthEast.Long;
                                    insertCmd.Parameters[8].Value = viewport.SouthWest.Lat;
                                    insertCmd.Parameters[9].Value = viewport.SouthWest.Long;
                                    insertCmd.Parameters[10].Value = loc.GeocodeStatus;
                                    insertCmd.Parameters[11].Value = resultType;
                                    insertCmd.ExecuteNonQuery();
                                }
                                loc.Latitude = latitude;
                                loc.Longitude = longitude;
                                loc.GoogleLocation = address;
                                loc.GoogleResultType = resultType;
                                loc.ViewPort = viewport;
                            }
                            else
                            {
                                skipped++;
                                //Console.WriteLine("Skipped : " + loc.ToString());
                            }
                        }
                        reader.Close();
                    }
                    count++;
                    int percent = (int)Math.Truncate((count - 1) * 100.0 / total);
                    string status = "Googled " + good + " good, " + bad + " partial. Skip " + geocoded + " prev found and " + skipped + " partial/not found. Done " + (count - 1) +
                            " of " + total + ".  ";
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
                    MessageBox.Show("Finished Geocoding.\n" + txtGoogleWait.Text, "Timeline Geocoding");
                else
                    MessageBox.Show("Finished Geocoding.", "Timeline Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error geocoding : " + ex.Message);
            }
        }

        private void mnuGeocodeLocations_Click(object sender, EventArgs e)
        {
            StartGeoCoding();
        }

        #endregion

        private void updateChangesWithoutAskingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Ask to update database", updateChangesWithoutAskingToolStripMenuItem.Checked);
        }

        private void dgLocations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgLocations.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Double click to edit location.";
            }
        }
    }
}
