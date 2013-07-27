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

namespace FTAnalyzer.Forms
{
    public partial class LCReport : Form
    {

        private PrintingDataGridViewProvider printProvider;
        private Dictionary<int, DataGridViewCellStyle> styles;
        private int c1841ColumnIndex;
        private int c1911ColumnIndex;

        public LCReport(SortableBindingList<IDisplayLCReport> reportList)
        {
            InitializeComponent();
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
            notCensusEnterednotLCYear.BackColor = notCensusEnterednotLCYear.ForeColor = Color.Green;
            styles.Add(3, notCensusEnterednotLCYear);
            DataGridViewCellStyle allEntered = new DataGridViewCellStyle();
            allEntered.BackColor = allEntered.ForeColor = Color.Green;
            styles.Add(4, allEntered);

            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(40, 40, 40, 40);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgReportSheet, true, true, true,
                new TitlePrintBlock(this.Text), null, null);

            printDocument.DefaultPageSettings.Landscape = true;

            dgReportSheet.DataSource = reportList;
            // Sort by birth date, then forenames, then surname to get the final order required.
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            dgReportSheet.Columns["MarriedName"].Visible = false;
            c1841ColumnIndex = dgReportSheet.Columns["C1841"].Index;
            c1911ColumnIndex = dgReportSheet.Columns["C1911"].Index;
            ResizeColumns();
            tsRecords.Text = CountText(reportList);

            cbCensusSearchProvider.SelectedIndex = 0;
        }

        private string CountText(SortableBindingList<IDisplayLCReport> reportList)
        {

            StringBuilder output = new StringBuilder("Count : " + reportList.Count + " records listed.");

            //Dictionary<int, int> totals = new Dictionary<int, int>();
            //for (int census = 1841; census <= 1911; census += 10)
            //    for (int i = 0; i <= 4; i++)
            //        totals[census * 10 + i] = 0;

            //foreach (IDisplayLCReport r in reportList)
            //{
            //    totals[18410 + r.C1841]++;
            //    totals[18510 + r.C1851]++;
            //    totals[18610 + r.C1861]++;
            //    totals[18710 + r.C1871]++;
            //    totals[18810 + r.C1881]++;
            //    totals[18910 + r.C1891]++;
            //    totals[19010 + r.C1901]++;
            //    totals[19110 + r.C1911]++;
            //}

            //for (int census = 1841; census <= 1911; census += 10)
            //{
            //    output.Append(census);
            //    output.Append(":");
            //    output.Append(totals[census * 10 + 1]);
            //    output.Append("/");
            //    output.Append(totals[census * 10 + 2]);
            //    output.Append("/");
            //    output.Append(totals[census * 10 + 3] + totals[census * 10 + 4]);
            //    output.Append(" ");
            //}

            return output.ToString();
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgReportSheet.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            for (int i = c1841ColumnIndex; i <= c1911ColumnIndex; i++)
                dgReportSheet.Columns[i].Width = 50;
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex < c1841ColumnIndex || e.ColumnIndex > c1911ColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells["Relation"];
                string relation = (string)cell.Value;
                if (relation == "Direct Ancestor")
                {
                    e.CellStyle.Font = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
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
                        case 0:
                            cell.ToolTipText = "Not alive at time of census.";
                            break;
                        case 1:
                            cell.ToolTipText = "No census information entered. Double click to search " + cbCensusSearchProvider.SelectedItem + ".";
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
                    }
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
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

        private void dgReportSheet_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
            int value = (int)cell.Value;
            if (value == 1 || value == 2)
            {
                IDisplayLCReport person = (IDisplayLCReport)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                UriBuilder uri = null;
                int censusYear = (1841 + (e.ColumnIndex - c1841ColumnIndex) * 10);
                switch (cbCensusSearchProvider.SelectedIndex)
                {
                    case 0: uri = BuildAncestryQuery(censusYear, person); break;
                    case 1: uri = BuildFindMyPastQuery(censusYear, person); break;
                    case 2: uri = BuildFreeCenQuery(censusYear, person); break;
                }
                if (uri != null)
                {
                    Process.Start(uri.ToString());
                }
            }
        }

        private UriBuilder BuildAncestryQuery(int censusYear, IDisplayLCReport person)
        {
            UriBuilder uri = new UriBuilder();
            uri.Host = "search.ancestry.co.uk";
            uri.Path = "cgi-bin/sse.dll";
            StringBuilder query = new StringBuilder();
            query.Append("gl=" + censusYear + "uki&");
            query.Append("rank=1&");
            query.Append("new=1&");
            query.Append("so=3&");
            query.Append("MSAV=1&");
            query.Append("msT=1&");
            query.Append("gss=ms_f-68&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                query.Append("gsfn=" + HttpUtility.UrlEncode(person.Forenames) + "&");
            }
            string surname = string.Empty;
            if (person.Surname != "?" && person.Surname.ToUpper() != "UNKNOWN")
            {
                surname = person.Surname;
            }
            if (person.MarriedName != "?" && person.MarriedName.ToUpper() != "UNKNOWN" && person.MarriedName != person.Surname)
            {
                surname += " " + person.MarriedName;
            }
            surname = surname.Trim();
            query.Append("gsln=" + HttpUtility.UrlEncode(surname) + "&");
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear + 1;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear - 1;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 1) / 2;
                    if (2 < range && range < 5) range = 5;
                    if (range > 5) range = 10;
                }
                query.Append("msbdy=" + year + "&");
                query.Append("msbdp=" + range + "&");
            }
            if (person.BirthLocation != null)
            {
                string location = person.BirthLocation.getLocation(FactLocation.PARISH).ToString();
                query.Append("msbpn__ftp=" + HttpUtility.UrlEncode(location) + "&");
            }
            query.Append("uidh=2t2");
            uri.Query = query.ToString();
            return uri;
        }

        private UriBuilder BuildFreeCenQuery(int censusYear, IDisplayLCReport person)
        {
            FactDate censusFactDate = new FactDate(censusYear.ToString());
            UriBuilder uri = new UriBuilder();
            uri.Host = "www.freecen.org.uk";
            uri.Path = "/cgi/search.pl";
            StringBuilder query = new StringBuilder();
            query.Append("y=" + censusYear + "&");
            if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            {
                int pos = person.Forenames.IndexOf(" ");
                string forename = person.Forenames;
                if(pos>0)
                    forename = person.Forenames.Substring(0,pos); //strip out any middle names as FreeCen searches better without then
                query.Append("g=" + HttpUtility.UrlEncode(forename) + "&");
            }
            string surname = person.SurnameAtDate(censusFactDate);
            if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            {
                query.Append("s=" + HttpUtility.UrlEncode(surname) + "&");
                query.Append("p=on&");
            }
            if (person.BirthDate != FactDate.UNKNOWN_DATE)
            {
                int startYear = person.BirthDate.StartDate.Year;
                int endYear = person.BirthDate.EndDate.Year;
                int year, range;
                if (startYear == FactDate.MINDATE.Year)
                {
                    year = endYear + 1;
                    range = 10;
                }
                else if (endYear == FactDate.MAXDATE.Year)
                {
                    year = startYear - 1;
                    range = 10;
                }
                else
                {
                    year = (endYear + startYear + 1) / 2;
                    range = (endYear - startYear + 1) / 2;
                }
                if (range == 0)
                {
                    query.Append("r=0&");
                }
                else if (range <= 2)
                {
                    query.Append("r=2&");
                }
                else if (range <= 5)
                {
                    query.Append("r=5&");
                }
                else 
                {
                    query.Append("r=10&");
                }
                query.Append("a=" + year + "&");
            }
            if (person.BirthLocation != null)
            {
                string location = person.BirthLocation.Parish;
                query.Append("t=" + HttpUtility.UrlEncode(location) + "&");
                query.Append("b=" + person.BirthLocation.FreeCenCountyCode + "&"); 
            }
            query.Append("c=all&"); // initially set to search all counties need a routine to return FreeCen county codes 
            query.Append("z=Find&"); // executes search
            uri.Query = query.ToString();
            return uri;
        }

        private UriBuilder BuildFindMyPastQuery(int censusYear, IDisplayLCReport person)
        {
            //POST /CensusPersonSearchResultServlet?censusYear=1881
            //[truncated] recordPosition=0&pageDirection=&startNewSearch=startNewSearch&basicSearch=false&
            //    route=&censusYear=1881&forenames=Alexander&fns=fns&lastName=Bisset&yearOfBirth=1863&
            //    yearOfBirthVariation=2&occupation=&birthPlace=aberdeen&residenc

            MessageBox.Show("Find My Past searching coming soon in a future version");
            return null;
            //FactDate censusFactDate = new FactDate(censusYear.ToString());
            //UriBuilder uri = new UriBuilder();
            //uri.Host = "www.findmypast.co.uk";
            //uri.Path = "/CensusPersonSearchResultServlet";
            //StringBuilder query = new StringBuilder();
            //query.Append("recordPosition=0&");
            //query.Append("startNewSearch=startNewSearch&");
            //query.Append("pageDirection=&");
            //query.Append("route=&");
            //query.Append("basicSearch=true&");
            //query.Append("searchHouseholds=6,15&");
            //query.Append("searchInstitutions=9&");
            //query.Append("searchVessels=11,12&");

            //query.Append("censusYear=" + censusYear + "&");
            //if (person.Forenames != "?" && person.Forenames.ToUpper() != "UNKNOWN")
            //{
            //    int pos = person.Forenames.IndexOf(" ");
            //    string forenames = person.Forenames;
            //    if (pos > 0)
            //        forenames = person.Forenames.Substring(0, pos); //strip out any middle names as FreeCen searches better without then
            //    query.Append("forenames=" + HttpUtility.UrlEncode(forenames) + "&");
            //    query.Append("fns=fns&");
            //}
            //string surname = person.SurnameAtDate(censusFactDate);
            //if (surname != "?" && surname.ToUpper() != "UNKNOWN")
            //{
            //    query.Append("lastName=" + HttpUtility.UrlEncode(surname) + "&");
            //    query.Append("sns=sns");
            //}
            //if (person.MarriedName != "?" && person.MarriedName.ToUpper() != "UNKNOWN" && person.MarriedName != person.Surname)
            //{
            //    query.Append("otherLastName=" + HttpUtility.UrlEncode(surname) + "&");
            //}
            //if (person.BirthDate != FactDate.UNKNOWN_DATE)
            //{
            //    int startYear = person.BirthDate.StartDate.Year;
            //    int endYear = person.BirthDate.EndDate.Year;
            //    int year, range;
            //    if (startYear == FactDate.MINDATE.Year)
            //    {
            //        year = endYear + 1;
            //        range = 10;
            //    }
            //    else if (endYear == FactDate.MAXDATE.Year)
            //    {
            //        year = startYear - 1;
            //        range = 10;
            //    }
            //    else
            //    {
            //        year = (endYear + startYear + 1) / 2;
            //        range = (endYear - startYear + 1) / 2;
            //        if (range > 5) range = 10;
            //    }
            //    query.Append("yearOfBirth=" + year + "&");
            //    query.Append("yearOfBirthVariation=" + range + "&");
            //}
            //if (person.BirthLocation != null)
            //{
            //    string location = person.BirthLocation.Parish;
            //    Tuple<string,string> area = person.BirthLocation.FindMyPastCountyCode
            //    query.Append("birthPlace=" + HttpUtility.UrlEncode(location) + "&");
            //    query.Append("country=" + HttpUtility.UrlEncode(area.item1));
            //    query.Append("coIdList=" + HttpUtility.UrlEncode(area.item2));
            //}
            //query.Append("sortOrder=nameAsc&");
            //uri.Query = query.ToString();
            //return uri;
        }

    }
}
