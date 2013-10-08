using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.Mapping;

namespace FTAnalyzer.Forms
{
    public partial class GeocodeLocations : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private Font italicFont;
        private ReportFormHelper reportFormHelper;
        private SortableBindingList<MapLocation> locations;

        public GeocodeLocations()
        {
            InitializeComponent();
            this.locations = new SortableBindingList<MapLocation>(locations);
            dgLocations.AutoGenerateColumns = false;
            dgLocations.DataSource = this.locations;
            reportFormHelper = new ReportFormHelper(this.Text, dgLocations, this.ResetTable);
            italicFont = new Font(dgLocations.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("MapIndividualColumns.xml");
            tsRecords.Text = locations.Count + " Records. " + Properties.Messages.Hints_Individual;
        }

        private void ResetTable()
        {
            //dgLocations.Sort(dgLocations.Columns["Location"], ListSortDirection.Ascending);
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
            reportFormHelper.ResetColumnLayout("MapIndividualColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("MapIndividualColumns.xml");
            MessageBox.Show("Column Settings Saved", "Map Individuals");
        }

        private void dgLocations_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                MapLocation loc = dgLocations.Rows[e.RowIndex].DataBoundItem as MapLocation;
                e.ToolTipText = "Geocoding status: " + loc.Location.Geocoded;
            }
        }

        private void dgLocations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string indID = (string)dgLocations.CurrentRow.Cells["Ind_ID"].Value;
            Individual ind = ft.GetIndividual(indID);
            Facts factForm = new Facts(ind);
            MainForm.DisposeDuplicateForms(factForm);
            factForm.Show();
        }
    }
}
