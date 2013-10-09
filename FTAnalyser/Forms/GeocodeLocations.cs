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
        private SortableBindingList<IDisplayGeocodedLocation> locations;
        private bool formClosing;

        public GeocodeLocations()
        {
            InitializeComponent();
            ft = FamilyTree.Instance;
            this.locations = ft.AllGeocodingLocations;
            dgLocations.AutoGenerateColumns = false;
            dgLocations.DataSource = this.locations;
            reportFormHelper = new ReportFormHelper(this.Text, dgLocations, this.ResetTable);
            italicFont = new Font(dgLocations.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("GeocodeLocationsColumns.xml");
            mnuGeocodeLocations.Enabled = !ft.Geocoding; // disable menu if already geocoding
            SetStatusText();
        }

        private void SetStatusText()
        {
            int gedcom = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.GEDCOM_USER));
            int found = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.MATCHED));
            int notfound = FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.PARTIAL_MATCH));
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)) - 1);
            int total = FactLocation.AllLocations.Count() - 1;

            txtGoogleWait.Text = string.Empty;
            txtLocations.Text = "Already Geocoded: " + (gedcom + found) + ", not found: " + notfound + " yet to search: " + notsearched + " of " + total + " locations";
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
            reportFormHelper.DoExportToExcel<IDisplayFact>(this);
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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                FactLocation loc = dgLocations.Rows[e.RowIndex].DataBoundItem as FactLocation;
                EditLocation editform = new EditLocation(loc);
                editform.Show();
                this.Cursor = Cursors.Default;
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
    }
}
