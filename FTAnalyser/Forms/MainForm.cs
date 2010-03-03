using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FTAnalyser
{
    public partial class MainForm : Form
    {
        private Cursor storedCursor = Cursors.Default;

        public MainForm()
        {
            InitializeComponent();
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
                    XmlDocument document = GedcomToXml.Load(openFileDialog1.FileName);
                    document.Save("GedcomOutput.xml");
                    FamilyTree ft = FamilyTree.Instance;
                    ft.LoadTree(document, pbSources, pbIndividuals, pbFamilies);
                    dgIndividuals.DataSource = ft.AllDisplayIndividuals;
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
    }
}
