using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static FTAnalyzer.ColourValues;

namespace FTAnalyzer.Forms
{
    public partial class ColourCensus : Form
    {

        private ReportFormHelper reportFormHelper;

        private Dictionary<int, DataGridViewCellStyle> styles;
        private int startColumnIndex;
        private int endColumnIndex;
        private SortableBindingList<IDisplayColourCensus> reportList;
        private Font boldFont;
        private string country;

        public ColourCensus(string country, List<IDisplayColourCensus> reportList)
        {
            InitializeComponent();
            dgReportSheet.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgReportSheet, true);
            this.country = country;
            this.reportList = new SortableBindingList<IDisplayColourCensus>(reportList);
            reportFormHelper = new ReportFormHelper(this, "Colour Census Report", dgReportSheet, ResetTable, "Colour Census");

            boldFont = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
            styles = new Dictionary<int, DataGridViewCellStyle>();
            DataGridViewCellStyle notAlive = new DataGridViewCellStyle();
            notAlive.BackColor = notAlive.ForeColor = Color.DarkGray;
            styles.Add(0, notAlive);
            DataGridViewCellStyle missingCensus = new DataGridViewCellStyle();
            missingCensus.BackColor = missingCensus.ForeColor = Color.Red;
            styles.Add(1, missingCensus);
            DataGridViewCellStyle censusMissingLC = new DataGridViewCellStyle();
            censusMissingLC.BackColor = censusMissingLC.ForeColor = Color.Yellow;
            styles.Add(2, censusMissingLC);
            DataGridViewCellStyle notCensusEnterednotLCYear = new DataGridViewCellStyle();
            notCensusEnterednotLCYear.BackColor = notCensusEnterednotLCYear.ForeColor = Color.LawnGreen;
            styles.Add(3, notCensusEnterednotLCYear);
            DataGridViewCellStyle allEntered = new DataGridViewCellStyle();
            allEntered.BackColor = allEntered.ForeColor = Color.LawnGreen;
            styles.Add(4, allEntered);
            DataGridViewCellStyle lcNoCensus = new DataGridViewCellStyle();
            lcNoCensus.BackColor = lcNoCensus.ForeColor = Color.DarkOrange;
            styles.Add(5, lcNoCensus);
            DataGridViewCellStyle onOtherCensus = new DataGridViewCellStyle();
            onOtherCensus.BackColor = onOtherCensus.ForeColor = Color.DarkSlateGray;
            styles.Add(6, onOtherCensus);
            DataGridViewCellStyle outsideUKCensus = new DataGridViewCellStyle();
            outsideUKCensus.BackColor = outsideUKCensus.ForeColor = Color.DarkSlateGray;
            styles.Add(7, onOtherCensus);
            DataGridViewCellStyle knownMissing = new DataGridViewCellStyle();
            knownMissing.BackColor = knownMissing.ForeColor = Color.MediumSeaGreen;
            styles.Add(8, knownMissing);
            SetColumns(country);
            dgReportSheet.DataSource = this.reportList;
            reportFormHelper.LoadColumnLayout("ColourCensusLayout.xml");
            tsRecords.Text = Properties.Messages.Count + reportList.Count + " records listed.";
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
                defaultProvider = "FamilySearch";
            cbCensusSearchProvider.Text = defaultProvider;
            cbFilter.Text = "All Individuals";
        }

        private void SetColumns(string country)
        {
            // make all census columns hidden
            for (int index = dgReportSheet.Columns["C1841"].Index; index <= dgReportSheet.Columns["Ire1911"].Index; index++)
                dgReportSheet.Columns[index].Visible = false;

            if (country.Equals(Countries.UNITED_STATES))
            {
                startColumnIndex = dgReportSheet.Columns["US1790"].Index;
                endColumnIndex = dgReportSheet.Columns["US1940"].Index;
                cbFilter.Items[5] = "Outside USA (Dark Grey)";
            }
            else if (country.Equals(Countries.CANADA))
            {
                startColumnIndex = dgReportSheet.Columns["Can1851"].Index;
                endColumnIndex = dgReportSheet.Columns["Can1921"].Index;
                cbFilter.Items[5] = "Outside Canada (Dark Grey)";
            }
            else if (country.Equals(Countries.IRELAND))
            {
                startColumnIndex = dgReportSheet.Columns["Ire1901"].Index;
                endColumnIndex = dgReportSheet.Columns["Ire1911"].Index;
                cbFilter.Items[5] = "Outside Ireland (Dark Grey)";
            }
            else
            {
                startColumnIndex = dgReportSheet.Columns["C1841"].Index;
                endColumnIndex = dgReportSheet.Columns["C1939"].Index;
                cbFilter.Items[5] = "Outside UK (Dark Grey)";
            }
            // show those columns that should be visible for the country in use
            for (int index = startColumnIndex; index <= endColumnIndex; index++)
                dgReportSheet.Columns[index].Visible = true;
        }

        private void ResetTable()
        {
            ApplyDefaultSort();
            foreach (DataGridViewColumn column in dgReportSheet.Columns)
                column.Width = column.MinimumWidth;
        }

        private void ApplyDefaultSort()
        {
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
        }

        private void DgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex < startColumnIndex || e.ColumnIndex > endColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells["Relation"];
                string relation = (string)cell.Value;
                if (relation == "Direct Ancestor")
                    e.CellStyle.Font = boldFont;
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

                    cell.InheritedStyle.BackColor = style.BackColor;
                    cell.InheritedStyle.ForeColor = style.ForeColor;
                    cell.InheritedStyle.SelectionForeColor = cell.InheritedStyle.SelectionBackColor;

                    switch (value)
                    {
                        case 0:
                            cell.ToolTipText = "Not alive at time of census.";
                            break;
                        case 1:
                            if (e.ColumnIndex == C1939.Index)
                                if (cbCensusSearchProvider.SelectedItem.Equals("Find My Past"))
                                    cell.ToolTipText = $"No census information entered. Double click to search {cbCensusSearchProvider.SelectedItem}.";
                                else
                                    cell.ToolTipText = $"No census information entered. No search on {cbCensusSearchProvider.SelectedItem} available.";
                            else
                                cell.ToolTipText = $"No census information entered. Double click to search {cbCensusSearchProvider.SelectedItem}.";
                            break;
                        case 2:
                            cell.ToolTipText = "Census entered but no Lost Cousins flag set.";
                            break;
                        case 3:
                            cell.ToolTipText = "Census entered and not a Lost Cousins year.";
                            break;
                        case 4:
                            cell.ToolTipText = "Census entered and flagged as entered on Lost Cousins.";
                            break;
                        case 5:
                            cell.ToolTipText = "Lost Cousins flagged but no Census entered.";
                            break;
                        case 6:
                            cell.ToolTipText = $"On Census outside {country}";
                            break;
                        case 7:
                            cell.ToolTipText = $"Likely outside {country} on census date";
                            break;
                        case 8:
                            cell.ToolTipText = "Known to be missing from the census";
                            break;
                    }
                }
            }
        }

        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = "Colour Census Report";
            reportFormHelper.PrintReport("Missing from Census Report");
        }

        private void PrintPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void DgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FamilyTree ft = FamilyTree.Instance;
                if (e.ColumnIndex >= startColumnIndex && e.ColumnIndex <= endColumnIndex)
                {
                    DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    int value = (int)cell.Value;
                    if (value >= 1 && value <= 7) // allows any type of record to search census
                    {
                        IDisplayColourCensus person = (IDisplayColourCensus)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                        int censusYear;
                        if (country.Equals(Countries.UNITED_STATES))
                            censusYear = (1790 + (e.ColumnIndex - startColumnIndex) * 10);
                        else if (country.Equals(Countries.CANADA))
                            if (e.ColumnIndex <= dgReportSheet.Columns["Can1901"].Index)
                                censusYear = (1851 + (e.ColumnIndex - startColumnIndex) * 10);
                            else
                                censusYear = (1901 + (e.ColumnIndex - dgReportSheet.Columns["Can1901"].Index) * 5);
                        else if (country.Equals(Countries.IRELAND))
                            censusYear = (1901 + (e.ColumnIndex - startColumnIndex) * 10);
                        else
                        {
                            if (e.ColumnIndex == C1939.Index)
                                censusYear = 1939;
                            else
                                censusYear = (1841 + (e.ColumnIndex - startColumnIndex) * 10);
                        }
                        string censusCountry = person.BestLocation(new FactDate(censusYear.ToString())).CensusCountry;
                        if (censusYear == 1939 && !cbCensusSearchProvider.SelectedItem.Equals("Find My Past"))
                            MessageBox.Show($"Unable to search the 1939 National Register on {cbCensusSearchProvider.SelectedItem}.", "FTAnalyzer");
                        else
                        {
                            try
                            {
                                ft.SearchCensus(censusCountry, censusYear, ft.GetIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex);
                            }
                            catch (CensusSearchException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else if (e.ColumnIndex >= 0)
                {
                    string indID = (string)dgReportSheet.CurrentRow.Cells["IndividualID"].Value;
                    Individual ind = ft.GetIndividual(indID);
                    Facts factForm = new Facts(ind);
                    factForm.Show();
                }
            }
        }

        private void CbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgReportSheet.Refresh(); // forces update of tooltips
            dgReportSheet.Focus();
        }

        private List<IDisplayColourCensus> BuildFilter(CensusColour toFind, bool all)
        {
            List<IDisplayColourCensus> result = new List<IDisplayColourCensus>();
            foreach (IDisplayColourCensus row in reportList)
            {
                if (all)
                {
                    if (toFind == CensusColour.CENSUS_PRESENT_NOT_LC_YEAR)
                    {  // also check for value of 4 which is also green
                        if ((row.C1841 == CensusColour.CENSUS_PRESENT_NOT_LC_YEAR || row.C1841 == CensusColour.CENSUS_PRESENT_LC_PRESENT || row.C1841 == CensusColour.NOT_ALIVE) &&
                            (row.C1851 == toFind || row.C1851 == CensusColour.NOT_ALIVE) &&
                            (row.C1861 == toFind || row.C1861 == CensusColour.NOT_ALIVE) &&
                            (row.C1871 == toFind || row.C1871 == CensusColour.NOT_ALIVE) &&
                            (row.C1881 == toFind || row.C1881 == CensusColour.CENSUS_PRESENT_LC_PRESENT || row.C1881 == CensusColour.NOT_ALIVE) &&
                            (row.C1891 == toFind || row.C1891 == CensusColour.NOT_ALIVE) &&
                            (row.C1901 == toFind || row.C1901 == CensusColour.NOT_ALIVE) &&
                            (row.C1911 == toFind || row.C1911 == CensusColour.CENSUS_PRESENT_LC_PRESENT || row.C1911 == CensusColour.NOT_ALIVE) &&
                            (row.C1939 == toFind || row.C1939 == CensusColour.CENSUS_PRESENT_LC_PRESENT || row.C1939 == CensusColour.NOT_ALIVE) &&
                            !(row.C1841 == CensusColour.NOT_ALIVE && row.C1851 == CensusColour.NOT_ALIVE && row.C1861 == CensusColour.NOT_ALIVE && row.C1871 == CensusColour.NOT_ALIVE &&
                              row.C1881 == CensusColour.NOT_ALIVE && row.C1891 == CensusColour.NOT_ALIVE && row.C1901 == CensusColour.NOT_ALIVE && row.C1911 == CensusColour.NOT_ALIVE && row.C1939 == CensusColour.NOT_ALIVE && toFind != 0)) // exclude all greys
                            result.Add(row);
                    }
                    else
                        if ((row.C1841 == toFind || row.C1841 == CensusColour.NOT_ALIVE) && (row.C1851 == toFind || row.C1851 == CensusColour.NOT_ALIVE) &&
                            (row.C1861 == toFind || row.C1861 == CensusColour.NOT_ALIVE) && (row.C1871 == toFind || row.C1871 == CensusColour.NOT_ALIVE) &&
                            (row.C1881 == toFind || row.C1881 == CensusColour.NOT_ALIVE) && (row.C1891 == toFind || row.C1891 == CensusColour.NOT_ALIVE) &&
                            (row.C1901 == toFind || row.C1901 == CensusColour.NOT_ALIVE) && (row.C1911 == toFind || row.C1911 == CensusColour.NOT_ALIVE) &&
                            (row.C1939 == toFind || row.C1939 == CensusColour.NOT_ALIVE) &&
                            !(row.C1841 == CensusColour.NOT_ALIVE && row.C1851 == CensusColour.NOT_ALIVE && row.C1861 == CensusColour.NOT_ALIVE &&
                              row.C1871 == CensusColour.NOT_ALIVE && row.C1881 == CensusColour.NOT_ALIVE && row.C1891 == CensusColour.NOT_ALIVE &&
                              row.C1901 == CensusColour.NOT_ALIVE && row.C1911 == CensusColour.NOT_ALIVE && row.C1939 == CensusColour.NOT_ALIVE &&
                              toFind != CensusColour.NOT_ALIVE)) // exclude all greys
                        result.Add(row);
                }
                else
                {
                    if (row.C1841 == toFind || row.C1851 == toFind || row.C1861 == toFind || row.C1871 == toFind ||
                       row.C1881 == toFind || row.C1891 == toFind || row.C1901 == toFind || row.C1911 == toFind || row.C1939 == toFind)
                        result.Add(row);
                }

            }
            return result;
        }

        private void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            List<IDisplayColourCensus> list;
            switch (cbFilter.SelectedIndex)
            {
                case -1: // nothing selected
                case 0: // All Individuals
                    dgReportSheet.DataSource = reportList;
                    break;
                case 1: // None Found (All Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColour.NO_CENSUS, true));
                    break;
                case 2: // All Found (All Green)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColour.CENSUS_PRESENT_NOT_LC_YEAR, true));
                    list.AddRange(BuildFilter(CensusColour.CENSUS_PRESENT_LC_PRESENT, true));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 3: // Lost Cousins Missing (Yellows)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColour.CENSUS_PRESENT_LC_MISSING, false));
                    break;
                case 4: // Lost Cousins Present (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColour.LC_PRESENT_NO_CENSUS, false));
                    break;
                case 5: // Some Outside UK (Some Dark Grey)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColour.OVERSEAS_CENSUS, false));
                    list.AddRange(BuildFilter(CensusColour.OUT_OF_COUNTRY, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 6: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColour.NO_CENSUS, false));
                    break;
                case 7: // Some Found (Some Green)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColour.CENSUS_PRESENT_NOT_LC_YEAR, false));
                    list.AddRange(BuildFilter(CensusColour.CENSUS_PRESENT_LC_PRESENT, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 8: // Known Missing (Mid Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColour.KNOWN_MISSING, false));
                    break;
            }
            dgReportSheet.Focus();
            ApplyDefaultSort();
            tsRecords.Text = Properties.Messages.Count + dgReportSheet.RowCount + " records listed.";
            Cursor = Cursors.Default;
        }

        private void MnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayColourCensus>(dgReportSheet.Columns);
        }

        private void MnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("ColourCensusLayout.xml");
            MessageBox.Show("Form Settings Saved", "Colour Census");
        }

        private void MnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("ColourCensusLayout.xml");
        }

        private void MnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgReportSheet.CurrentRow != null)
            {
                IDisplayColourCensus ds = (IDisplayColourCensus)dgReportSheet.CurrentRow.DataBoundItem;
                Individual ind = FamilyTree.Instance.GetIndividual(ds.IndividualID);
                Facts factForm = new Facts(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void DgReportSheet_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void ColourCensus_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void ColourCensus_Load(object sender, EventArgs e)
        {
            SpecialMethods.SetFonts(this);
        }
    }
}
