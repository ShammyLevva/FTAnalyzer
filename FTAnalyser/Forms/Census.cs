using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        private Dictionary<int, Color> rowColour;

        public Census()
        {
            InitializeComponent();
        }

        public void setupCensus(RegistrationsProcessor rp, FactDate date, bool censusDone, int maxAge)
        {
            FamilyTree ft = FamilyTree.Instance;
            List<Registration> regs = ft.getAllCensusRegistrations(date, censusDone);
            List<Registration> census = rp.processRegistrations(regs);
            List<DisplayCensus> ds = new List<DisplayCensus>();
            rowColour = new Dictionary<int, Color>();
            foreach (CensusRegistration r in census)
                foreach (Individual i in r.Members)
                {
                    if (i.getAge(date).MinAge <= maxAge) 
                        ds.Add(new DisplayCensus(r.FamilyGed, r.RegistrationLocation, r.registrationDate, i));
                }
            // ds.sort(new IndividualNameComparator());
            dgCensus.DataSource = ds;
            tsRecords.Text = ds.Count + " Records.";
            ResizeColumns();
            ColourRows();
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgCensus.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void ColourRows()
        {
            string currentFamilyGed = "";
            bool highlighted = true;
            foreach (DataGridViewRow r in dgCensus.Rows)
            {
                DisplayCensus cr = (DisplayCensus)r.DataBoundItem;
                if (cr.FamilyGed != currentFamilyGed)
                {
                    currentFamilyGed = cr.FamilyGed;
                    highlighted = !highlighted;
                }
                if (highlighted)
                    rowColour.Add(r.Index, Color.DarkGray);
                else
                    rowColour.Add(r.Index, Color.White);
            }
        }

        private void dgCensus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Color colour = Color.White;
            rowColour.TryGetValue(e.RowIndex, out colour);
            e.CellStyle.BackColor = colour;
        }
    }
}
