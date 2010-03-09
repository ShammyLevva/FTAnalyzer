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
            
        public MainForm()
        {
            InitializeComponent();
            tabTestFactDate.Hide();
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
                    pbSources.Value = pbIndividuals.Value = pbFamilies.Value = 0;
                    Application.DoEvents();
                    XmlDocument document = GedcomToXml.Load(openFileDialog1.FileName);
                    document.Save("GedcomOutput.xml");
                    ft.LoadTree(document, pbSources, pbIndividuals, pbFamilies);
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
                if (f is Forms.Individuals)
                    toClose.Add(f);
            }
            foreach (Form f in toClose)
                f.Close();
        }

        private void btnTestDates_Click(object sender, EventArgs e)
        {
            FactDate fd = new FactDate(txtTestDate.Text);
            txtStartDate.Text = FactDate.Format(FactDate.FULL, fd.StartDate);
            txtEndDate.Text = FactDate.Format(FactDate.FULL, fd.EndDate);
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
            else if (tabControl.SelectedTab == tabIndividuals)
            {
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

        private void mnu1911Census_Click(object sender, EventArgs e)
        {
            RegistrationFilter directOrBlood = new OrFilter(
                new OrFilter(new RelationFilter(Individual.DIRECT),
                             new RelationFilter(Individual.BLOOD)),
                new RelationFilter(Individual.MARRIAGEDB)); 
            MultiComparator<Registration> byCensusLocation = new MultiComparator<Registration>();
            byCensusLocation.addComparator(new LocationComparator(FactLocation.PARISH));
            byCensusLocation.addComparator(new DateComparator());
            CensusOutputFormatter censusFormatter = new CensusOutputFormatter();
            RegistrationsProcessor censusRP = new RegistrationsProcessor(
                new AndFilter(directOrBlood, LocationFilter.ENGLAND_FILTER), byCensusLocation);
            List<Registration> census = ft.getAllCensusRegistrations(FactDate.CENSUS1911);
            ft.processRegistration("c:/temp/GROS/1911_census", censusRP, census, censusFormatter);
            Console.WriteLine("1911 Census Details written.");
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
    }
}
