using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Printing.DataGridViewPrint.Tools;
using FTAnalyzer.Utilities;
using System.Web;
using System.Diagnostics;
using System.IO;

namespace FTAnalyzer.Forms
{
    public partial class ColourBMD : Form
    {

        private PrintingDataGridViewProvider printProvider;
        private Dictionary<int, DataGridViewCellStyle> styles;
        private int birthColumnIndex;
        private int burialColumnIndex;
        private SortableBindingList<IDisplayColourBMD> reportList;

        public ColourBMD(SortableBindingList<IDisplayColourBMD> reportList)
        {
            InitializeComponent();
            this.reportList = reportList;
            styles = new Dictionary<int, DataGridViewCellStyle>();
            DataGridViewCellStyle notRequired = new DataGridViewCellStyle();
            notRequired.BackColor = notRequired.ForeColor = Color.DarkGray;
            styles.Add(0, notRequired);
            DataGridViewCellStyle missingData = new DataGridViewCellStyle();
            missingData.BackColor = missingData.ForeColor = Color.Red;
            styles.Add(1, missingData);
            DataGridViewCellStyle dateRange = new DataGridViewCellStyle();
            dateRange.BackColor = dateRange.ForeColor = Color.DarkOrange;
            styles.Add(2, dateRange);
            DataGridViewCellStyle approxDate = new DataGridViewCellStyle();
            approxDate.BackColor = approxDate.ForeColor = Color.Yellow;
            styles.Add(3, approxDate);
            DataGridViewCellStyle exactDate = new DataGridViewCellStyle();
            exactDate.BackColor = exactDate.ForeColor = Color.Green;
            styles.Add(4, exactDate);
            
            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(15,15,15,15);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgReportSheet, true, true, true,
                new TitlePrintBlock("Colour BMD Report"), null, null);

            printDocument.DefaultPageSettings.Landscape = true;

            dgReportSheet.DataSource = reportList;
            // Sort by birth date, then forenames, then surname to get the final order required.
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            birthColumnIndex = dgReportSheet.Columns["Birth"].Index;
            burialColumnIndex = dgReportSheet.Columns["CremBuri"].Index;
            LoadColumnLayout();
            ResizeColumns();
            tsRecords.Text = "Count : " + reportList.Count + " records listed.";
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbBMDSearchProvider.Text = defaultProvider;
            cbFilter.Text = "All Individuals";
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgReportSheet.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            for (int i = birthColumnIndex; i <= burialColumnIndex; i++)
                dgReportSheet.Columns[i].Width = 60;
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex < birthColumnIndex || e.ColumnIndex > burialColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells["Relation"];
                string relation = (string)cell.Value;
                if (relation == "Direct Ancestor")
                {
                    e.CellStyle.Font = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
                }
                if (relation == "Root Person")
                {
                    e.CellStyle.Font = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            else
            {
                DataGridViewCellStyle style = dgReportSheet.DefaultCellStyle;
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                styles.TryGetValue(value, out style);
                if (style != null)
                {
                    e.CellStyle.BackColor = style.BackColor;
                    e.CellStyle.ForeColor = style.ForeColor;
                    e.CellStyle.SelectionForeColor = e.CellStyle.SelectionBackColor;
                    switch (value)
                    {
                        case 0: // Grey
                            cell.ToolTipText = string.Empty;
                            break;
                        case 1: // Red
                            cell.ToolTipText = "Unknown date.";
                            break;
                        case 2: // Orange
                            cell.ToolTipText = "Wide date range.";
                            break;
                        case 3: // Yellow
                            cell.ToolTipText = "Approximate date.";
                            break;
                        case 4: // Green
                            cell.ToolTipText = "Exact date.";
                            break;
                    }
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.DocumentName = "Colour BDM Report";
                printDocument.Print();
            }
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printPreviewDialog.ShowDialog(this);
            }
        }

        private void dgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= birthColumnIndex && e.ColumnIndex <= burialColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                if (value == 1 || value == 2)
                {
                    IDisplayColourBMD person = (IDisplayColourBMD)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                    FamilyTree ft = FamilyTree.Instance;
                    //string censusCountry = person.BestLocation(new FactDate(censusYear.ToString())).CensusCountry;
                    //ft.SearchCensus(censusCountry, censusYear, ft.GetIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex);
                    MessageBox.Show("Not yet available.");
                }
            }
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbBMDSearchProvider.SelectedItem.ToString());
            dgReportSheet.Focus();
        }

        private List<IDisplayColourBMD> BuildFilter(int toFind, bool all)
        {
            List<IDisplayColourBMD> result = new List<IDisplayColourBMD>();
            foreach(IDisplayColourBMD row in this.reportList)
            {
                if (all)
                {
                    if ((row.Birth == toFind || row.Birth == 0) && (row.BaptChri == toFind || row.BaptChri == 0) &&
                        (row.Marriage1 == toFind || row.Marriage1 == 0) && (row.Marriage2 == toFind || row.Marriage2 == 0) &&
                        (row.Marriage3 == toFind || row.Marriage3 == 0) && (row.Death == toFind || row.Death == 0) &&
                        (row.CremBuri == toFind || row.CremBuri == 0))
                            result.Add(row);
                }
                else
                {
                    if (row.Birth == toFind || row.BaptChri == toFind || row.Marriage1 == toFind || row.Marriage2 == toFind ||
                        row.Marriage3 == toFind || row.Death == toFind || row.CremBuri == toFind)
                            result.Add(row);
                }
            }   
            return result;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            switch (cbFilter.SelectedIndex)
            {
                case -1: // nothing selected
                case 0: // All Individuals
                    dgReportSheet.DataSource = this.reportList;
                    break;
                case 1: // None Found (All Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(1, true));
                    break;
                case 2: // All Found (All Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(4, true));
                    break;
                case 3: // All Wide date ranges (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(2, true));
                    break;
                case 4: // All Narrow date ranges (Yellow)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(3, true));
                    break;
                case 5: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(1, false));
                    break;
                case 6: // Some found (Some Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(4, false));
                    break;
                case 7: // All Wide date ranges (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(2, false));
                    break;
                case 8: // All Narrow date ranges (Yellow)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(3, false));
                    break;
            }
            ResizeColumns();
            dgReportSheet.Focus();
            tsRecords.Text = "Count : " + dgReportSheet.RowCount + " records listed.";
            this.Cursor = Cursors.Default;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            List<IDisplayColourBMD> export = (dgReportSheet.DataSource as SortableBindingList<IDisplayColourBMD>).ToList<IDisplayColourBMD>();
            DataTable dt = convertor.ToDataTable(export);
            ExportToExcel.Export(dt);
            this.Cursor = Cursors.Default;
        }

        private void SaveColumnLayout()
        {
            DataTable dt = new DataTable("table");
            var query = from DataGridViewColumn col in dgReportSheet.Columns
                        orderby col.DisplayIndex
                        select col;

            foreach (DataGridViewColumn col in query)
            {
                dt.Columns.Add(col.Name);
            }
            string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, "ColourBMDColumns.xml");
            dt.WriteXmlSchema(path);
        }

        private void LoadColumnLayout()
        {
            try
            {
                DataTable dt = new DataTable();
                string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, "ColourBMDColumns.xml");
                dt.ReadXmlSchema(path);

                int i = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    dgReportSheet.Columns[col.ColumnName].DisplayIndex = i;
                    i++;
                }
            }
            catch (Exception)
            {
                ResetColumnLayout();
            }
        }

        private void ResetColumnLayout()
        {
            for (int i = 0; i < dgReportSheet.Columns.Count; i++)
                dgReportSheet.Columns[i].DisplayIndex = i;
            SaveColumnLayout();
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            ResetColumnLayout();
        }

        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            SaveColumnLayout();
            MessageBox.Show("Column Sort Order Saved", "BMD Colour Column Sorting");
        }
    }
}
