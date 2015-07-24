using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using Printing.DataGridViewPrint.Tools;
using System.IO;
using FTAnalyzer.Mapping;
using FTAnalyzer.Filters;
using FTAnalyzer.Forms;

namespace FTAnalyzer
{
    public partial class MapIndividuals : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Font italicFont;
        private ReportFormHelper reportFormHelper;
        private List<MapLocation> locations;
        private Form mapForm;

        public MapIndividuals(List<MapLocation> locations, string year, Form mapForm)
        {
            InitializeComponent();
            this.mapForm = mapForm;
            this.locations = locations;
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = new SortableBindingList<MapLocation>(this.locations);
            reportFormHelper = new ReportFormHelper(this, this.Text, dgIndividuals, this.ResetTable, "Map Individuals");
            italicFont = new Font(dgIndividuals.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("MapIndividualColumns.xml");
            tsRecords.Text = this.locations.Count + " Records. " + Properties.Messages.Hints_Individual;
            MapLocation mostCommon = this.locations.MostCommon();
            string titleText = mostCommon.Location.ToString();
            if (mapForm is TimeLine)
                titleText += " in " + year;
            this.Text = this.locations.Count < 2 ? titleText : "Centred near " + titleText;
            DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
        }

        private void ResetTable()
        {
            dgIndividuals.Sort(dgIndividuals.Columns["IndividualID"], ListSortDirection.Ascending);
            dgIndividuals.AutoResizeColumns();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Map Individuals");
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
            reportFormHelper.DoExportToExcel<MapLocation>();
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("MapIndividualColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("MapIndividualColumns.xml");
            MessageBox.Show("Form Settings Saved", "Map Individuals");
        }

        private void dgIndividuals_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                MapLocation loc = dgIndividuals.Rows[e.RowIndex].DataBoundItem as MapLocation;
                e.ToolTipText = "Geocoding status: " + loc.Location.Geocoded;
            }
        }

        private void dgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                Individual ind = ft.GetIndividual(indID);
                Facts factForm = new Facts(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void mnuEditLocation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            MapLocation loc = dgIndividuals.SelectedRows[0].DataBoundItem as MapLocation;
            EditLocation editform = new EditLocation(loc.Location);
            this.Cursor = Cursors.Default;
            DialogResult result = editform.ShowDialog(this);
            editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
            if (mapForm != null && mapForm.Visible)
            {
                if (mapForm is TimeLine)
                    ((TimeLine)mapForm).RefreshClusters();
                else if (mapForm is Places)
                    ((Places)mapForm).RefreshClusters();
            }       
            UpdateIcons(loc.Location);  
        }

        private void UpdateIcons(FactLocation changed)
        {
            foreach (MapLocation loc in locations)
            {
                if (loc.Location.Equals(changed))
                    loc.UpdateIcon();
            }
            dgIndividuals.Refresh();
        }

        private void editLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ft.Geocoding)
            {
                this.Cursor = Cursors.WaitCursor;
                MapLocation loc = dgIndividuals.CurrentRow.DataBoundItem as MapLocation;
                EditLocation(loc.Location);
            }
        }

        private void EditLocation(FactLocation loc)
        {
            EditLocation editform = new EditLocation(loc);
            this.Cursor = Cursors.Default;
            DialogResult result = editform.ShowDialog(this);
            editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
            // force refresh of locations from new edited data
            dgIndividuals.Refresh();
        }

        private void dgIndividuals_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgIndividuals.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void DatabaseHelper_GeoLocationUpdated(object location, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DatabaseHelper_GeoLocationUpdated(location, e)));
                return;
            }
            UpdateIcons((FactLocation)location);
        }

        private void MapIndividuals_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            this.Dispose();
        }
    }
}
