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
            if(tabControl.SelectedTab == tabIndividuals)
            {
                    List<IDisplayIndividual> list = ft.AllDisplayIndividuals;
                    dgIndividuals.DataSource = list;
                    tsCountLabel.Text = "Count : " + list.Count; 
            } else if (tabControl.SelectedTab == tabLooseDeaths) {
                    HourGlass(true);
                    List<IDisplayLooseDeath> looseDeathList = ft.GetLooseDeaths();
                    dgLooseDeaths.DataSource = looseDeathList;
                    tsCountLabel.Text = "Count : " + looseDeathList.Count;
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
            MultiComparator<Registration> byCensusLocation = new MultiComparator<Registration>();
            byCensusLocation.addComparator(new LocationComparator(FactLocation.PARISH));
            byCensusLocation.addComparator(new DateComparator());
            List<Registration> census = ft.getAllCensusRegistrations(FactDate.CENSUS1911);
            CensusOutputFormatter censusFormatter = new CensusOutputFormatter(); 
            RegistrationsProcessor censusRP = new RegistrationsProcessor(new AllFilter(), byCensusLocation);
            ft.processRegistration("1911_census", censusRP, census, censusFormatter);
            Console.WriteLine("1911 Census Details written.");
        }
    }
}
