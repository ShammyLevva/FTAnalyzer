using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        private Cursor storedCursor = Cursors.Default;
        private FamilyTree ft = FamilyTree.Instance;
        private FactDate censusDate = FactDate.CENSUS1841;
            
        public MainForm()
        {
            InitializeComponent();
            tabCensus.Hide();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "../..";
            openFileDialog1.FileName = "*.ged";
            openFileDialog1.Filter = "GED files (*.ged)|*.ged|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    HourGlass(true);
                    closeIndividualForms();
                    tabControl.SelectTab(tabDisplayProgress);
                    rtbOutput.Text = "";
                    pbSources.Value = pbIndividuals.Value = pbFamilies.Value = 0;
                    Application.DoEvents();
                    XmlDocument document = GedcomToXml.Load(openFileDialog1.FileName);
                    document.Save("GedcomOutput.xml");
                    ft.LoadTree(document, pbSources, pbIndividuals, pbFamilies, rtbOutput);
                    HourGlass(false);
                    MessageBox.Show("Gedcom File Loaded");
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void closeIndividualForms()
        {
            List<Form> toClose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f is Forms.Individuals || f is Forms.Census)
                    toClose.Add(f);
            }
            foreach (Form f in toClose)
                f.Close();
        }

        private void HourGlass(bool on)
        {
            if (on)
            {
                storedCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = storedCursor;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabDisplayProgress)
            {
                tsCountLabel.Text = "";
            } else if (tabControl.SelectedTab == tabIndividuals)
            {
                List<IDisplayIndividual> list = ft.AllDisplayIndividuals;
                dgIndividuals.DataSource = list;
                tsCountLabel.Text = "Count : " + list.Count;
            }
            else if (tabControl.SelectedTab == tabCensus)
            {
                cbCensusDate.Text = "1911";
                tsCountLabel.Text = "";
            } else if (tabControl.SelectedTab == tabLooseDeaths)
            {
                HourGlass(true);
                List<IDisplayLooseDeath> looseDeathList = ft.GetLooseDeaths();
                dgLooseDeaths.DataSource = looseDeathList;
                tsCountLabel.Text = "Count : " + looseDeathList.Count;
                HourGlass(false);
            }
            else if (tabControl.SelectedTab == tabLocations)
            {
                HourGlass(true);
                tsCountLabel.Text = "";
                List<IDisplayLocation> countries = ft.AllCountries;
                List<IDisplayLocation> regions = ft.AllRegions;
                List<IDisplayLocation> parishes = ft.AllParishes;
                List<IDisplayLocation> addresses = ft.AllAddresses;
                countries.Sort(new FactLocationComparer(FactLocation.COUNTRY));
                regions.Sort(new FactLocationComparer(FactLocation.REGION));
                parishes.Sort(new FactLocationComparer(FactLocation.PARISH));
                addresses.Sort(new FactLocationComparer(FactLocation.ADDRESS));
                dgCountries.DataSource = countries;
                dgRegions.DataSource = regions;
                dgParishes.DataSource = parishes;
                dgAddresses.DataSource = addresses;
                HourGlass(false);
            }
        }

        private void dgIndividuals_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Comparer<IDisplayIndividual> comparer;
            switch (e.ColumnIndex) 
            {
                case 0: // ID
                    comparer = new DefaultIndividualComparer();
                    break;
                case 1: // Forename
                    comparer = new IndividualNameComparer();
                    break;
                default: 
                    comparer = new DefaultIndividualComparer();
                    break;
            }

            List<IDisplayIndividual> list = ft.AllDisplayIndividuals;
            list.Sort(comparer);
            dgIndividuals.DataSource = list;
            tsCountLabel.Text = "Count : " + list.Count; 
        }

        private void dgCountries_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgCountries.CurrentRow.DataBoundItem;
            Forms.Individuals frmInd = new Forms.Individuals();
            frmInd.setLocation(loc, FactLocation.COUNTRY);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgRegions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgRegions.CurrentRow.DataBoundItem;
            Forms.Individuals frmInd = new Forms.Individuals();
            frmInd.setLocation(loc, FactLocation.REGION);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgParishes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgParishes.CurrentRow.DataBoundItem;
            Forms.Individuals frmInd = new Forms.Individuals();
            frmInd.setLocation(loc, FactLocation.PARISH);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgAddresses_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
            Forms.Individuals frmInd = new Forms.Individuals();
            frmInd.setLocation(loc, FactLocation.ADDRESS);
            frmInd.Show();
            HourGlass(false);
        }

        private void btnShowResults_Click(object sender, EventArgs e)
        {
            RegistrationFilter filter = createRegistrationFilter();
            MultiComparator<Registration> censusComparator = new MultiComparator<Registration>();
            censusComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            censusComparator.addComparator(new DateComparator());
            RegistrationsProcessor censusRP = new RegistrationsProcessor(filter, censusComparator);

            Forms.Census census = new Forms.Census();
            census.setupCensus(censusRP, censusDate);
            census.Text = censusDate.StartDate.Year.ToString() + " Census Records to search for";
            census.Show();
        }

        private RegistrationFilter createRegistrationFilter()
        {
            RegistrationFilter locationFilter = new TrueFilter();
            if (rbScotland.Checked)
                locationFilter = LocationFilter.SCOTLAND_FILTER;
            if (rbEngland.Checked)
                locationFilter = LocationFilter.ENGLAND_FILTER;
            if (rbWales.Checked)
                locationFilter = LocationFilter.WALES_FILTER;
            if (rbGB.Checked)
                locationFilter = new AndFilter(LocationFilter.SCOTLAND_FILTER, LocationFilter.ENGLAND_FILTER, LocationFilter.WALES_FILTER);
            if (rbCanada.Checked)
                locationFilter = LocationFilter.CANADA_FILTER;
            if (rbUSA.Checked)
                locationFilter = LocationFilter.USA_FILTER;
            
            RegistrationFilter relationFilter = new FalseFilter();
            if (ckbBlood.Checked)
                relationFilter = new OrFilter(new RelationFilter(Individual.BLOOD), relationFilter);
            if (ckbDirects.Checked)
                relationFilter = new OrFilter(new RelationFilter(Individual.DIRECT), relationFilter);
            if (ckbMarriage.Checked)
                relationFilter = new OrFilter(new RelationFilter(Individual.MARRIAGE), relationFilter);
            if (ckbMarriageDB.Checked)
                relationFilter = new OrFilter(new RelationFilter(Individual.MARRIAGEDB), relationFilter);
            if (ckbUnknown.Checked)
                relationFilter = new OrFilter(new RelationFilter(Individual.UNKNOWN), relationFilter);

            return new AndFilter(locationFilter, relationFilter, new DateFilter(censusDate));
        }

        private void cbCensusDate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCensusDate.Text == "1841")
                censusDate = FactDate.CENSUS1841;
            else if (cbCensusDate.Text == "1851")
                censusDate = FactDate.CENSUS1851;
            else if (cbCensusDate.Text == "1861")
                censusDate = FactDate.CENSUS1861;
            else if (cbCensusDate.Text == "1871")
                censusDate = FactDate.CENSUS1871;
            else if (cbCensusDate.Text == "1881")
                censusDate = FactDate.CENSUS1881;
            else if (cbCensusDate.Text == "1891")
                censusDate = FactDate.CENSUS1891;
            else if (cbCensusDate.Text == "1901")
                censusDate = FactDate.CENSUS1901;
            else if (cbCensusDate.Text == "1911")
                censusDate = FactDate.CENSUS1911; 
        }
    }
}
