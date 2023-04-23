using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using System.ComponentModel;
using static FTAnalyzer.ColourValues;

namespace FTAnalyzer.Forms
{
    public partial class ColourCensus : Form
    {
        readonly ReportFormHelper reportFormHelper;
        readonly Dictionary<int, DataGridViewCellStyle> styles;
        int startColumnIndex;
        int endColumnIndex;
        readonly SortableBindingList<IDisplayColourCensus> _reportList;
        readonly Font boldFont;
        readonly string _country;
        bool settingSelections;
        
        public ColourCensus(string country, List<IDisplayColourCensus> reportList)
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                dgReportSheet.AutoGenerateColumns = false;
                ExtensionMethods.DoubleBuffered(dgReportSheet, true);
                settingSelections = false;
                _country = country;
                _reportList = new SortableBindingList<IDisplayColourCensus>(reportList);
                reportFormHelper = new ReportFormHelper(this, "Colour Census Report", dgReportSheet, ResetTable, "Colour Census");

                boldFont = new(dgReportSheet.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Bold);
                styles = new Dictionary<int, DataGridViewCellStyle>();
                DataGridViewCellStyle notAlive = new();
                notAlive.BackColor = notAlive.ForeColor = CensusColourValues[(int)CensusColours.NOT_ALIVE];
                styles.Add(0, notAlive);
                DataGridViewCellStyle missingCensus = new();
                missingCensus.BackColor = missingCensus.ForeColor = CensusColourValues[(int)CensusColours.NO_CENSUS];
                styles.Add(1, missingCensus);
                DataGridViewCellStyle censusMissingLC = new();
                censusMissingLC.BackColor = censusMissingLC.ForeColor = CensusColourValues[(int)CensusColours.CENSUS_PRESENT_LC_MISSING];
                styles.Add(2, censusMissingLC);
                DataGridViewCellStyle notCensusEnterednotLCYear = new();
                notCensusEnterednotLCYear.BackColor = notCensusEnterednotLCYear.ForeColor = CensusColourValues[(int)CensusColours.CENSUS_PRESENT_NOT_LC_YEAR];
                styles.Add(3, notCensusEnterednotLCYear);
                DataGridViewCellStyle allEntered = new();
                allEntered.BackColor = allEntered.ForeColor = CensusColourValues[(int)CensusColours.CENSUS_PRESENT_LC_PRESENT];
                styles.Add(4, allEntered);
                DataGridViewCellStyle lcNoCensus = new();
                lcNoCensus.BackColor = lcNoCensus.ForeColor = CensusColourValues[(int)CensusColours.LC_PRESENT_NO_CENSUS];
                styles.Add(5, lcNoCensus);
                DataGridViewCellStyle onOtherCensus = new();
                onOtherCensus.BackColor = onOtherCensus.ForeColor = CensusColourValues[(int)CensusColours.OVERSEAS_CENSUS];
                styles.Add(6, onOtherCensus);
                DataGridViewCellStyle outsideUKCensus = new();
                outsideUKCensus.BackColor = outsideUKCensus.ForeColor = CensusColourValues[(int)CensusColours.OUT_OF_COUNTRY];
                styles.Add(7, onOtherCensus);
                DataGridViewCellStyle knownMissing = new();
                knownMissing.BackColor = knownMissing.ForeColor = CensusColourValues[(int)CensusColours.KNOWN_MISSING];
                styles.Add(8, knownMissing);
                DataGridViewCellStyle diedInCensusRange = new();
                diedInCensusRange.BackColor = diedInCensusRange.ForeColor = CensusColourValues[(int)CensusColours.DIED_DURING_CENSUS];
                styles.Add(9, diedInCensusRange);
                SetColumns(country);
                dgReportSheet.DataSource = _reportList;
                dgReportSheet.RowTemplate.Height = (int)(FontSettings.Default.FontHeight * GraphicsUtilities.GetCurrentScaling());
                dgReportSheet.AllowUserToResizeColumns = true;
                reportFormHelper.LoadColumnLayout("ColourCensusLayout.xml");
                tsRecords.Text = $"{Messages.Count}{reportList.Count} records listed.";
                string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
                defaultProvider ??= "FamilySearch";
                string defaultRegion = (string)Application.UserAppDataRegistry.GetValue("Default Region");
                defaultRegion ??= ".co.uk";
                cbCensusSearchProvider.Text = defaultProvider;
                cbRegion.Text = defaultRegion;
                cbFilter.Text = "All Individuals";
            }
            catch (Exception) { }
        }

        void SetColumns(string country)
        {
            // make all census columns hidden
            for (int index = dgReportSheet.Columns["C1841"].Index; index <= dgReportSheet.Columns["Ire1911"].Index; index++)
                dgReportSheet.Columns[index].Visible = false;

            if (country.Equals(Countries.UNITED_STATES))
            {
                startColumnIndex = dgReportSheet.Columns["US1790"].Index;
                endColumnIndex = dgReportSheet.Columns["US1950"].Index;
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

        void ResetTable()
        {
            ApplyDefaultSort();
            foreach (DataGridViewColumn column in dgReportSheet.Columns)
                column.Width = column.MinimumWidth;
        }

        void ApplyDefaultSort()
        {
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
        }

        void DgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                styles.TryGetValue(value, out DataGridViewCellStyle style);
                if (style is not null)
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
                            cell.ToolTipText = "Census entered and not a Lost Cousins year/country.";
                            break;
                        case 4:
                            cell.ToolTipText = "Census entered and flagged as entered on Lost Cousins.";
                            break;
                        case 5:
                            cell.ToolTipText = "Lost Cousins flagged but no Census entered.";
                            break;
                        case 6:
                            cell.ToolTipText = $"On Census outside {_country}";
                            break;
                        case 7:
                            cell.ToolTipText = $"Likely outside {_country} on census date";
                            break;
                        case 8:
                            cell.ToolTipText = "Known to be missing from the census";
                            break;
                        case 9:
                            cell.ToolTipText = "Died within range of dates census was taken";
                            break;
                    }
                }
            }
        }

        void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = "Colour Census Report";
            reportFormHelper.PrintReport("Missing from Census Report");
        }

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void DgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                        if (_country.Equals(Countries.UNITED_STATES))
                            censusYear = 1790 + (e.ColumnIndex - startColumnIndex) * 10;
                        else if (_country.Equals(Countries.CANADA))
                            if (e.ColumnIndex <= dgReportSheet.Columns["Can1901"].Index)
                                censusYear = 1851 + (e.ColumnIndex - startColumnIndex) * 10;
                            else
                                censusYear = 1901 + (e.ColumnIndex - dgReportSheet.Columns["Can1901"].Index) * 5;
                        else if (_country.Equals(Countries.IRELAND))
                            censusYear = 1901 + (e.ColumnIndex - startColumnIndex) * 10;
                        else
                        {
                            if (e.ColumnIndex == C1939.Index)
                                censusYear = 1939;
                            else
                                censusYear = 1841 + (e.ColumnIndex - startColumnIndex) * 10;
                        }
                        string censusCountry = person.BestLocation(new FactDate(censusYear.ToString())).CensusCountry;
                        if (censusYear == 1939 && 
                            !cbCensusSearchProvider.SelectedItem.Equals("Find My Past") && 
                            !cbCensusSearchProvider.SelectedItem.Equals("Ancestry"))
                            MessageBox.Show($"Unable to search the 1939 National Register on {cbCensusSearchProvider.SelectedItem}.", "FTAnalyzer");
                        else
                        {
                            try
                            {
                                FamilyTree.SearchCensus(censusCountry, censusYear, ft.GetIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex, cbRegion.Text);
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
                    Facts factForm = new(ind);
                    factForm.Show();
                }
            }
        }

        void CbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgReportSheet.Refresh(); // forces update of tooltips
            dgReportSheet.Focus();
        }

        void CbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Region", cbRegion.SelectedItem.ToString());
            Settings.Default.defaultURLRegion = cbRegion.SelectedItem.ToString();
            Settings.Default.Save();
            dgReportSheet.Refresh(); // forces update of tooltips
            dgReportSheet.Focus();
        }

        List<IDisplayColourCensus> BuildFilter(CensusColours toFind, bool all)
        {
            List<IDisplayColourCensus> result = new();
            foreach (IDisplayColourCensus row in _reportList)
            {
                if (all)
                {
                    if (toFind == CensusColours.CENSUS_PRESENT_NOT_LC_YEAR)
                    {  // also check for value of 4 which is also green
                        if ((row.C1841 == CensusColours.CENSUS_PRESENT_NOT_LC_YEAR || row.C1841 == CensusColours.CENSUS_PRESENT_LC_PRESENT || row.C1841 == CensusColours.NOT_ALIVE) &&
                            (row.C1851 == toFind || row.C1851 == CensusColours.NOT_ALIVE) &&
                            (row.C1861 == toFind || row.C1861 == CensusColours.NOT_ALIVE) &&
                            (row.C1871 == toFind || row.C1871 == CensusColours.NOT_ALIVE) &&
                            (row.C1881 == toFind || row.C1881 == CensusColours.CENSUS_PRESENT_LC_PRESENT || row.C1881 == CensusColours.NOT_ALIVE) &&
                            (row.C1891 == toFind || row.C1891 == CensusColours.NOT_ALIVE) &&
                            (row.C1901 == toFind || row.C1901 == CensusColours.NOT_ALIVE) &&
                            (row.C1911 == toFind || row.C1911 == CensusColours.CENSUS_PRESENT_LC_PRESENT || row.C1911 == CensusColours.NOT_ALIVE) &&
                            (row.C1921 == toFind || row.C1921 == CensusColours.CENSUS_PRESENT_LC_PRESENT || row.C1921 == CensusColours.NOT_ALIVE) &&
                            (row.C1939 == toFind || row.C1939 == CensusColours.CENSUS_PRESENT_LC_PRESENT || row.C1939 == CensusColours.NOT_ALIVE) &&
                            !(row.C1841 == CensusColours.NOT_ALIVE && row.C1851 == CensusColours.NOT_ALIVE && row.C1861 == CensusColours.NOT_ALIVE && row.C1871 == CensusColours.NOT_ALIVE &&
                              row.C1881 == CensusColours.NOT_ALIVE && row.C1891 == CensusColours.NOT_ALIVE && row.C1901 == CensusColours.NOT_ALIVE && row.C1911 == CensusColours.NOT_ALIVE &&
                              row.C1921 == CensusColours.NOT_ALIVE && row.C1939 == CensusColours.NOT_ALIVE && toFind != 0)) // exclude all greys
                            result.Add(row);
                    }
                    else
                        if ((row.C1841 == toFind || row.C1841 == CensusColours.NOT_ALIVE) && (row.C1851 == toFind || row.C1851 == CensusColours.NOT_ALIVE) &&
                            (row.C1861 == toFind || row.C1861 == CensusColours.NOT_ALIVE) && (row.C1871 == toFind || row.C1871 == CensusColours.NOT_ALIVE) &&
                            (row.C1881 == toFind || row.C1881 == CensusColours.NOT_ALIVE) && (row.C1891 == toFind || row.C1891 == CensusColours.NOT_ALIVE) &&
                            (row.C1901 == toFind || row.C1901 == CensusColours.NOT_ALIVE) && (row.C1911 == toFind || row.C1911 == CensusColours.NOT_ALIVE) &&
                            (row.C1921 == toFind || row.C1921 == CensusColours.NOT_ALIVE) && (row.C1939 == toFind || row.C1939 == CensusColours.NOT_ALIVE) &&
                            !(row.C1841 == CensusColours.NOT_ALIVE && row.C1851 == CensusColours.NOT_ALIVE && row.C1861 == CensusColours.NOT_ALIVE &&
                              row.C1871 == CensusColours.NOT_ALIVE && row.C1881 == CensusColours.NOT_ALIVE && row.C1891 == CensusColours.NOT_ALIVE &&
                              row.C1901 == CensusColours.NOT_ALIVE && row.C1911 == CensusColours.NOT_ALIVE && row.C1921 == CensusColours.NOT_ALIVE && 
                              row.C1939 == CensusColours.NOT_ALIVE &&
                              toFind != CensusColours.NOT_ALIVE)) // exclude all greys
                        result.Add(row);
                }
                else
                {
                    if (row.C1841 == toFind || row.C1851 == toFind || row.C1861 == toFind || row.C1871 == toFind ||
                       row.C1881 == toFind || row.C1891 == toFind || row.C1901 == toFind || row.C1911 == toFind || row.C1921 == toFind || row.C1939 == toFind)
                        result.Add(row);
                }

            }
            return result;
        }

        void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            List<IDisplayColourCensus> list;
            switch (cbFilter.SelectedIndex)
            {
                case -1: // nothing selected
                case 0: // All Individuals
                    dgReportSheet.DataSource = _reportList;
                    break;
                case 1: // None Found (All Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColours.NO_CENSUS, true));
                    break;
                case 2: // All Found (All Green)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColours.CENSUS_PRESENT_NOT_LC_YEAR, true));
                    list.AddRange(BuildFilter(CensusColours.CENSUS_PRESENT_LC_PRESENT, true));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 3: // Lost Cousins Missing (Yellows)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColours.CENSUS_PRESENT_LC_MISSING, false));
                    break;
                case 4: // Lost Cousins Present (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColours.LC_PRESENT_NO_CENSUS, false));
                    break;
                case 5: // Some Outside UK (Some Dark Grey)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColours.OVERSEAS_CENSUS, false));
                    list.AddRange(BuildFilter(CensusColours.OUT_OF_COUNTRY, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 6: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColours.NO_CENSUS, false));
                    break;
                case 7: // Some Found (Some Green)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(CensusColours.CENSUS_PRESENT_NOT_LC_YEAR, false));
                    list.AddRange(BuildFilter(CensusColours.CENSUS_PRESENT_LC_PRESENT, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list.Distinct().ToList<IDisplayColourCensus>());
                    break;
                case 8: // Known Missing (Mid Green)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(CensusColours.KNOWN_MISSING, false));
                    break;
            }
            dgReportSheet.Focus();
            ApplyDefaultSort();
            tsRecords.Text = $"{Messages.Count} {dgReportSheet.RowCount} records listed.";
            Cursor = Cursors.Default;
        }

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplayColourCensus>(dgReportSheet.Columns);

        void MnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("ColourCensusLayout.xml");
            MessageBox.Show("Form Settings Saved", "Colour Census");
        }

        void MnuResetCensusColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("ColourCensusLayout.xml");

        void MnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgReportSheet.CurrentRow is not null)
            {
                IDisplayColourCensus ds = (IDisplayColourCensus)dgReportSheet.CurrentRow.DataBoundItem;
                Individual ind = FamilyTree.Instance.GetIndividual(ds.IndividualID);
                Facts factForm = new(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        void DgReportSheet_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        void ColourCensus_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void ColourCensus_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);

        void DgReportSheet_SelectionChanged(object sender, EventArgs e)
        {
            if (!settingSelections && dgReportSheet.CurrentRow is not null)
            {
                settingSelections = true;
                foreach (DataGridViewCell cell in dgReportSheet.CurrentRow.Cells)
                {
                    if(cell.Visible)
                        cell.Selected = cell.ColumnIndex < startColumnIndex || cell.ColumnIndex > endColumnIndex;
                }
                settingSelections = false;
            }
        }
    }
}
