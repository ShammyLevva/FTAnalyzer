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
    public partial class RegistrationReport : Form
    {
        public RegistrationReport()
        {
            InitializeComponent();
        }

        public void SetupBirthRegistration(List<Registration> regs)
        {
            List<BirthRegistration> temp = new List<BirthRegistration>();
            foreach (BirthRegistration r in regs)
            {
                temp.Add(r);
            }
            RegistrationBindingSource.DataSource = temp;
            reportViewer.LocalReport.ReportEmbeddedResource = "FTAnalyzer.Reports.BirthReport.rdlc";
            reportViewer.RefreshReport();
        }

        public void SetupDeathRegistration(List<Registration> regs)
        {
            List<DeathRegistration> temp = new List<DeathRegistration>();
            foreach (DeathRegistration r in regs)
            {
                temp.Add(r);
            }
            RegistrationBindingSource.DataSource = temp;
            reportViewer.LocalReport.ReportEmbeddedResource = "FTAnalyzer.Reports.DeathReport.rdlc";
            reportViewer.RefreshReport();
        }

        public void SetupMarriageRegistration(List<Registration> regs)
        {
            List<MarriageRegistration> temp = new List<MarriageRegistration>();
            foreach (MarriageRegistration r in regs)
            {
                temp.Add(r);
            }
            RegistrationBindingSource.DataSource = temp;
            reportViewer.LocalReport.ReportEmbeddedResource = "FTAnalyzer.Reports.MarriageReport.rdlc";
            reportViewer.RefreshReport();
        }
    }
}
