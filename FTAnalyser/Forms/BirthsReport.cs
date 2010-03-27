using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public partial class BirthsReport : Form
    {
        public BirthsReport()
        {
            InitializeComponent();
        }

        public void setup(List<Registration> regs)
        {
            List<BirthRegistration> temp = new List<BirthRegistration>();
            foreach (BirthRegistration r in regs)
            {
                temp.Add(r);
            }
            BirthRegistrationBindingSource.DataSource = temp;
        }

        private void ReportTest_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }
    }
}
