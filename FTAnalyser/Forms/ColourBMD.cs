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
        private ReportFormHelper reportFormHelper;

        private Dictionary<int, DataGridViewCellStyle> styles;
        private int birthColumnIndex;
        private int burialColumnIndex;
        private SortableBindingList<IDisplayColourBMD> reportList;
        private Font boldFont;

        public ColourBMD(List<IDisplayColourBMD> reportList)
        {
            InitializeComponent();
            dgReportSheet.AutoGenerateColumns = false;

            this.reportList = new SortableBindingList<IDisplayColourBMD>(reportList);
            reportFormHelper = new ReportFormHelper("Colour BMD Report", dgReportSheet, this.ResetTable);
    
            boldFont = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
            styles = new Dictionary<int, DataGridViewCellStyle>();
            DataGridViewCellStyle notRequired = new DataGridViewCellStyle();
            notRequired.BackColor = notRequired.ForeColor = Color.DarkGray;
            styles.Add(0, notRequired);
            DataGridViewCellStyle missingData = new DataGridViewCellStyle();
            missingData.BackColor = missingData.ForeColor = Color.Red;
            styles.Add(1, missingData);
            DataGridViewCellStyle wideDateRange = new DataGridViewCellStyle();
            wideDateRange.BackColor = wideDateRange.ForeColor = Color.DarkOrange;
            styles.Add(2, wideDateRange);
            DataGridViewCellStyle narrowDateRange = new DataGridViewCellStyle();
            narrowDateRange.BackColor = narrowDateRange.ForeColor = Color.Yellow;
            styles.Add(3, narrowDateRange);
            DataGridViewCellStyle approxDate = new DataGridViewCellStyle();
            approxDate.BackColor = approxDate.ForeColor = Color.PaleGreen;
            styles.Add(4, approxDate);
            DataGridViewCellStyle exactDate = new DataGridViewCellStyle();
            exactDate.BackColor = exactDate.ForeColor = Color.Green;
            styles.Add(5, exactDate);
            DataGridViewCellStyle noSpouse = new DataGridViewCellStyle();
            noSpouse.BackColor = noSpouse.ForeColor = Color.PeachPuff;
            styles.Add(6, noSpouse);
            DataGridViewCellStyle hasChildren = new DataGridViewCellStyle();
            hasChildren.BackColor = hasChildren.ForeColor = Color.SkyBlue;
            styles.Add(7, hasChildren);
            DataGridViewCellStyle noMarriage = new DataGridViewCellStyle();
            noMarriage.BackColor = noMarriage.ForeColor = Color.RoyalBlue;
            styles.Add(8, noMarriage);

            dgReportSheet.DataSource = this.reportList;
            birthColumnIndex = dgReportSheet.Columns["Birth"].Index;
            burialColumnIndex = dgReportSheet.Columns["CremBuri"].Index;
            reportFormHelper.LoadColumnLayout("ColourBMDColumns.xml");
            tsRecords.Text = Properties.Messages.Count + reportList.Count + " records listed.";
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbBMDSearchProvider.Text = defaultProvider;
            cbFilter.Text = "All Individuals";
        }

        private void ResetTable()
        {
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            foreach (DataGridViewColumn column in dgReportSheet.Columns)
                column.Width = column.MinimumWidth;
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
                    e.CellStyle.Font = boldFont;
                }
                if (relation == "Root Person")
                {
                    e.CellStyle.Font = boldFont;
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

                    cell.Style.BackColor = style.BackColor;
                    cell.Style.ForeColor = style.ForeColor;
                    cell.Style.SelectionForeColor = cell.Style.SelectionBackColor;

                    switch (value)
                    {
                        case 0: // Grey
                            cell.ToolTipText = string.Empty;
                            break;
                        case 1: // Red
                            cell.ToolTipText = "Unknown date.";
                            break;
                        case 2: // Orange
                            cell.ToolTipText = "Wide date range (>2 years).";
                            break;
                        case 3: // Yellow
                            cell.ToolTipText = "Narrow date range (over Quarter/Month up to 2y).";
                            break;
                        case 4: // Pale Green 
                            cell.ToolTipText = "Approximate date. (Quarter/Month)";
                            break;
                        case 5: // Green
                            cell.ToolTipText = "Exact date.";
                            break;
                        case 6: // pale grey
                            cell.ToolTipText = "Of marrying age but no spouse recorded";
                            break;
                        case 7: // light blue
                            cell.ToolTipText = "No partner but has shared fact or children";
                            break;
                        case 8: // dark blue
                            cell.ToolTipText = "Has partner but no marriage fact";
                            break;
                    }
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = "Colour BDM Report";
            reportFormHelper.PrintReport(this);
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport(this);
        }

        private void dgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FamilyTree ft = FamilyTree.Instance;
                if (e.ColumnIndex >= birthColumnIndex && e.ColumnIndex <= burialColumnIndex)
                {
                    DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    int value = (int)cell.Value;
                    if (value == 1 || value == 2)
                    {
                        IDisplayColourBMD person = (IDisplayColourBMD)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                        //string censusCountry = person.BestLocation(new FactDate(censusYear.ToString())).CensusCountry;
                        //ft.SearchCensus(censusCountry, censusYear, ft.GetIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex);
                        MessageBox.Show("Not yet available.");
                    }
                }
                else if (e.ColumnIndex >= 0)
                {
                    string indID = (string)dgReportSheet.CurrentRow.Cells["Ind_ID"].Value;
                    Individual ind = ft.GetIndividual(indID);
                    Facts factForm = new Facts(ind);
                    factForm.Show();
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
            foreach (IDisplayColourBMD row in this.reportList)
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
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(5, true));
                    break;
                case 3: // All Wide date ranges (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(2, true));
                    break;
                case 4: // All Narrow date ranges (Yellow)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(3, true));
                    break;
                case 5: // All Approx date ranges (Light Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(4, true));
                    break;
                case 6: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(1, false));
                    break;
                case 7: // Some found (Some Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(5, false));
                    break;
                case 8: // Some Wide date ranges (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(2, false));
                    break;
                case 9: // Some Narrow date ranges (Yellow)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(3, false));
                    break;
                case 10: // Some Approx date ranges (Light Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(4, false));
                    break;
                case 11: // Of Marrying age (Peach)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(6, false));
                    break;
                case 12: // No Partner shared fact/children (Light Blue)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(7, false));
                    break;
                case 13: // Partner but no marriage (Dark Blue)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(8, false));
                    break;
            }
            ResetTable();
            dgReportSheet.Focus();
            tsRecords.Text = Properties.Messages.Count + dgReportSheet.RowCount + " records listed.";
            this.Cursor = Cursors.Default;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayColourBMD>(this);
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("ColourBMDColumns.xml");
        }

        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("ColourBMDColumns.xml");
            MessageBox.Show("Column Settings Saved", "BMD Colour");
        }
    }
}
