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
        public Census()
        {
            InitializeComponent();
        }

        public void setupCensus(RegistrationsProcessor rp, FactDate date, bool censusDone)
        {
            FamilyTree ft = FamilyTree.Instance;
            List<Registration> regs = ft.getAllCensusRegistrations(date, censusDone);
            List<Registration> census = rp.processRegistrations(regs);
            List<DisplayCensus> ds = new List<DisplayCensus>();
            foreach (CensusRegistration r in census)
                foreach (Individual i in r.Members)
                    ds.Add(new DisplayCensus(r.FamilyGed, r.RegistrationLocation, r.registrationDate, i));
            // ds.sort(new IndividualNameComparator());
            dgCensus.DataSource = ds;
            tsRecords.Text = ds.Count + " Records.";
        }
    }
}
