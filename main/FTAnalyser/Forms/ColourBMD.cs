﻿using System;
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

        public enum ColourValue { EMPTY = 0, UNKNOWN_DATE = 1, VERY_WIDE_DATE = 2, WIDE_DATE = 3, NARROW_DATE = 4, APPROX_DATE = 5, 
                         EXACT_DATE = 6, NO_SPOUSE = 7, NO_PARTNER = 8, NO_MARRIAGE = 9, ISLIVING = 10, OVER90 = 11 };

        private Dictionary<ColourValue, DataGridViewCellStyle> styles;
        private int birthColumnIndex;
        private int burialColumnIndex;
        private SortableBindingList<IDisplayColourBMD> reportList;
        private Font boldFont;

        public ColourBMD(List<IDisplayColourBMD> reportList)
        {
            InitializeComponent();
            dgBMDReportSheet.AutoGenerateColumns = false;

            this.reportList = new SortableBindingList<IDisplayColourBMD>(reportList);
            reportFormHelper = new ReportFormHelper(this, "Colour BMD Report", dgBMDReportSheet, this.ResetTable, "Colour BMD");
            ExtensionMethods.DoubleBuffered(dgBMDReportSheet, true);
            
            boldFont = new Font(dgBMDReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
            styles = new Dictionary<ColourValue, DataGridViewCellStyle>();
            DataGridViewCellStyle notRequired = new DataGridViewCellStyle();
            notRequired.BackColor = notRequired.ForeColor = Color.DarkGray;
            styles.Add(ColourValue.EMPTY, notRequired);
            DataGridViewCellStyle missingData = new DataGridViewCellStyle();
            missingData.BackColor = missingData.ForeColor = Color.Red;
            styles.Add(ColourValue.UNKNOWN_DATE, missingData);
            DataGridViewCellStyle verywideDateRange = new DataGridViewCellStyle();
            verywideDateRange.BackColor = verywideDateRange.ForeColor = Color.Orange;
            styles.Add(ColourValue.VERY_WIDE_DATE, verywideDateRange);
            DataGridViewCellStyle wideDateRange = new DataGridViewCellStyle();
            wideDateRange.BackColor = wideDateRange.ForeColor = Color.DarkOrange;
            styles.Add(ColourValue.WIDE_DATE, wideDateRange);
            DataGridViewCellStyle narrowDateRange = new DataGridViewCellStyle();
            narrowDateRange.BackColor = narrowDateRange.ForeColor = Color.Yellow;
            styles.Add(ColourValue.NARROW_DATE, narrowDateRange);
            DataGridViewCellStyle approxDate = new DataGridViewCellStyle();
            approxDate.BackColor = approxDate.ForeColor = Color.PaleGreen;
            styles.Add(ColourValue.APPROX_DATE, approxDate);
            DataGridViewCellStyle exactDate = new DataGridViewCellStyle();
            exactDate.BackColor = exactDate.ForeColor = Color.Green;
            styles.Add(ColourValue.EXACT_DATE, exactDate);
            DataGridViewCellStyle noSpouse = new DataGridViewCellStyle();
            noSpouse.BackColor = noSpouse.ForeColor = Color.PeachPuff;
            styles.Add(ColourValue.NO_SPOUSE, noSpouse);
            DataGridViewCellStyle hasChildren = new DataGridViewCellStyle();
            hasChildren.BackColor = hasChildren.ForeColor = Color.SkyBlue;
            styles.Add(ColourValue.NO_PARTNER, hasChildren);
            DataGridViewCellStyle noMarriage = new DataGridViewCellStyle();
            noMarriage.BackColor = noMarriage.ForeColor = Color.RoyalBlue;
            styles.Add(ColourValue.NO_MARRIAGE, noMarriage);
            DataGridViewCellStyle isLiving = new DataGridViewCellStyle();
            isLiving.BackColor = isLiving.ForeColor = Color.DarkSlateGray;
            styles.Add(ColourValue.ISLIVING, isLiving);
            DataGridViewCellStyle over90 = new DataGridViewCellStyle();
            over90.BackColor = over90.ForeColor = Color.DarkGray;
            styles.Add(ColourValue.OVER90, over90);

            birthColumnIndex = dgBMDReportSheet.Columns["Birth"].Index;
            burialColumnIndex = dgBMDReportSheet.Columns["CremBuri"].Index;
            dgBMDReportSheet.DataSource = this.reportList;
            reportFormHelper.LoadColumnLayout("ColourBMDColumns.xml");
            tsRecords.Text = Properties.Messages.Count + reportList.Count + " records listed.";
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
                defaultProvider = "FamilySearch";
            if (defaultProvider.Equals("FreeCen"))
                defaultProvider = "FreeBMD";
            cbBMDSearchProvider.Text = defaultProvider;
            cbFilter.Text = "All Individuals";
        }

        private void ResetTable()
        {
            dgBMDReportSheet.Sort(dgBMDReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgBMDReportSheet.Sort(dgBMDReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgBMDReportSheet.Sort(dgBMDReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            foreach (DataGridViewColumn column in dgBMDReportSheet.Columns)
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
                DataGridViewCell cell = dgBMDReportSheet.Rows[e.RowIndex].Cells["Relation"];
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
                DataGridViewCellStyle style = dgBMDReportSheet.DefaultCellStyle;
                DataGridViewCell cell = dgBMDReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                ColourValue value = (ColourValue)cell.Value;
                styles.TryGetValue(value, out style);
                if (style != null)
                {
                    e.CellStyle.BackColor = style.BackColor;
                    e.CellStyle.ForeColor = style.ForeColor;
                    e.CellStyle.SelectionForeColor = e.CellStyle.SelectionBackColor;

                    cell.InheritedStyle.BackColor = style.BackColor;
                    cell.InheritedStyle.ForeColor = style.ForeColor;
                    cell.InheritedStyle.SelectionForeColor = cell.InheritedStyle.SelectionBackColor;

                    switch (value)
                    {
                        case ColourValue.EMPTY: // Grey
                            if (e.ColumnIndex == burialColumnIndex - 1) // death column
                                cell.ToolTipText = "Individual is probably still alive"; // if OVER90 still grey cell but use different tooltip
                            else
                                cell.ToolTipText = string.Empty;
                            break;
                        case ColourValue.UNKNOWN_DATE: // Red
                            cell.ToolTipText = "Unknown date.";
                            break;
                        case ColourValue.VERY_WIDE_DATE: // Dark Orange
                            cell.ToolTipText = "Date only accurate to more than ten year date range.";
                            break;
                        case ColourValue.WIDE_DATE: // Orange
                            cell.ToolTipText = "Date covers up to a ten year date range.";
                            break;
                        case ColourValue.NARROW_DATE: // Yellow
                            cell.ToolTipText = "Date accurate to within two year period, but longer than 3 months.";
                            break;
                        case ColourValue.APPROX_DATE: // Pale Green 
                            cell.ToolTipText = "Date accurate to within 3 months (note may be date of registration not event date)";
                            break;
                        case ColourValue.EXACT_DATE: // Green
                            cell.ToolTipText = "Exact date.";
                            break;
                        case ColourValue.NO_SPOUSE: // pale grey
                            cell.ToolTipText = "Of marrying age but no spouse recorded";
                            break;
                        case ColourValue.NO_PARTNER: // light blue
                            cell.ToolTipText = "No partner but has shared fact or children";
                            break;
                        case ColourValue.NO_MARRIAGE: // dark blue
                            cell.ToolTipText = "Has partner but no marriage fact";
                            break;
                        case ColourValue.ISLIVING: // dark grey
                            cell.ToolTipText = "Is flagged as living";
                            break;
                        case ColourValue.OVER90:
                            cell.ToolTipText = "Individual may be still alive";
                            break;
                    }
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = "Colour BMD Report";
            reportFormHelper.PrintReport("Colour BMD Report");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void dgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FamilyTree ft = FamilyTree.Instance;
                if (e.ColumnIndex >= birthColumnIndex && e.ColumnIndex <= burialColumnIndex)
                {
                    DataGridViewCell cell = dgBMDReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    ColourValue value = (ColourValue)cell.Value;
                    if (value != ColourValue.EXACT_DATE)
                    {
                        IDisplayColourBMD person = (IDisplayColourBMD)dgBMDReportSheet.Rows[e.RowIndex].DataBoundItem;
                        Individual ind = ft.GetIndividual(person.IndividualID);
                        if (e.ColumnIndex == birthColumnIndex || e.ColumnIndex == birthColumnIndex + 1)
                        {
                            ft.SearchBMD(FamilyTree.SearchType.BIRTH, ind, ind.BirthDate, cbBMDSearchProvider.SelectedIndex);
                        }
                        else if (e.ColumnIndex >= birthColumnIndex + 2 && e.ColumnIndex <= birthColumnIndex + 4)
                        {
                            FactDate marriageDate = FactDate.UNKNOWN_DATE;
                            if (e.ColumnIndex == birthColumnIndex + 2)
                                marriageDate = ind.FirstMarriageDate;
                            if (e.ColumnIndex == birthColumnIndex + 3)
                                marriageDate = ind.SecondMarriageDate;
                            if (e.ColumnIndex == birthColumnIndex + 4)
                                marriageDate = ind.ThirdMarriageDate;
                            ft.SearchBMD(FamilyTree.SearchType.MARRIAGE, ind, marriageDate, cbBMDSearchProvider.SelectedIndex);
                        }
                        else if (e.ColumnIndex == burialColumnIndex || e.ColumnIndex == burialColumnIndex - 1)
                        {
                            ft.SearchBMD(FamilyTree.SearchType.DEATH, ind, ind.DeathDate, cbBMDSearchProvider.SelectedIndex);
                        }
                    }
                }
                else if (e.ColumnIndex >= 0)
                {
                    string indID = (string)dgBMDReportSheet.CurrentRow.Cells["IndividualID"].Value;
                    Individual ind = ft.GetIndividual(indID);
                    Facts factForm = new Facts(ind);
                    factForm.Show();
                }
            }
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            string provider = cbBMDSearchProvider.SelectedItem.ToString();
            if (provider.Equals("FreeBMD"))
                provider = "FreeCen";
            Application.UserAppDataRegistry.SetValue("Default Search Provider", provider);
            dgBMDReportSheet.Refresh(); // forces refresh of tooltips
            dgBMDReportSheet.Focus();
        }

        private List<IDisplayColourBMD> BuildFilter(Forms.ColourBMD.ColourValue toFind, bool all)
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
                    dgBMDReportSheet.DataSource = this.reportList;
                    break;
                case 1: // None Found (All Red)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.UNKNOWN_DATE, true));
                    break;
                case 2: // All Found (All Green)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.EXACT_DATE, true));
                    break;
                case 3: // All Wide date ranges (Dark Orange)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.VERY_WIDE_DATE, true));
                    break;
                case 4: // All Wide date ranges (Orange)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.WIDE_DATE, true));
                    break;
                case 5: // All Narrow date ranges (Yellow)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.NARROW_DATE, true));
                    break;
                case 6: // All Approx date ranges (Light Green)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.APPROX_DATE, true));
                    break;
                case 7: // Some Missing (Some Red)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.UNKNOWN_DATE, false));
                    break;
                case 8: // Some found (Some Green)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.EXACT_DATE, false));
                    break;
                case 9: // Some Wide date ranges (Orange)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.WIDE_DATE, false));
                    break;
                case 10: // Some Narrow date ranges (Yellow)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.NARROW_DATE, false));
                    break;
                case 11: // Some Approx date ranges (Light Green)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.APPROX_DATE, false));
                    break;
                case 12: // Of Marrying age (Peach)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.NO_SPOUSE, false));
                    break;
                case 13: // No Partner shared fact/children (Light Blue)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.NO_PARTNER, false));
                    break;
                case 14: // Partner but no marriage (Dark Blue)
                    dgBMDReportSheet.DataSource = new SortableBindingList<IDisplayColourBMD>(BuildFilter(ColourValue.NO_MARRIAGE, false));
                    break;
            }
            dgBMDReportSheet.Focus();
            tsRecords.Text = Properties.Messages.Count + dgBMDReportSheet.RowCount + " records listed.";
            this.Cursor = Cursors.Default;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayColourBMD>();
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("ColourBMDColumns.xml");
        }

        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("ColourBMDColumns.xml");
            MessageBox.Show("Form Settings Saved", "BMD Colour");
        }

        private void mnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgBMDReportSheet.CurrentRow != null)
            {
                IDisplayColourBMD ds = (IDisplayColourBMD)dgBMDReportSheet.CurrentRow.DataBoundItem;
                Individual ind = FamilyTree.Instance.GetIndividual(ds.IndividualID);
                Facts factForm = new Facts(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void dgBMDReportSheet_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgBMDReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void ColourBMD_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}