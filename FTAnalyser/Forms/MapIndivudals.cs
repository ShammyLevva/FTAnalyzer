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
        private SortableBindingList<MapLocation> locations;

        public MapIndividuals(List<MapLocation> locations, string year)
        {
            InitializeComponent();
            this.locations = new SortableBindingList<MapLocation>(locations);
            dgIndividuals.AutoGenerateColumns = false;
            dgIndividuals.DataSource = this.locations;
            reportFormHelper = new ReportFormHelper(this.Text, dgIndividuals, this.ResetTable);
            italicFont = new Font(dgIndividuals.DefaultCellStyle.Font, FontStyle.Italic);
            reportFormHelper.LoadColumnLayout("MapIndividualColumns.xml");
            tsRecords.Text = locations.Count + " Records. " + Properties.Messages.Hints_Individual;
            MapLocation mostCommon = locations.MostCommon();
            string titleText = mostCommon.Location.ToString() + " in " + year;
            this.Text = locations.Count < 2 ? titleText : "Centred near " + titleText;
        }

        private void ResetTable()
        {
            //dgIndividuals.Sort(dgIndividuals.Columns["Location"], ListSortDirection.Ascending);
            dgIndividuals.AutoResizeColumns();
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
            reportFormHelper.DoExportToExcel<MapLocation>(this);
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
            string indID = (string)dgIndividuals.CurrentRow.Cells["Ind_ID"].Value;
            Individual ind = ft.GetIndividual(indID);
            Facts factForm = new Facts(ind);
            MainForm.DisposeDuplicateForms(factForm);
            factForm.Show();
        }
    }
}
