using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

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
            UpdateReportSources("FTAnalyzer_BirthRegistration", "FTAnalyzer.Reports.BirthReport.rdlc");
            this.Text = "Birth Registrations to find";
        }

        public void SetupDeathRegistration(List<Registration> regs)
        {
            List<DeathRegistration> temp = new List<DeathRegistration>();
            foreach (DeathRegistration r in regs)
            {
                temp.Add(r);
            }
            RegistrationBindingSource.DataSource = temp;
            UpdateReportSources("FTAnalyzer_DeathRegistration", "FTAnalyzer.Reports.DeathReport.rdlc");
            this.Text = "Death Registrations to find";
        }

        public void SetupMarriageRegistration(List<Registration> regs)
        {
            List<MarriageRegistration> temp = new List<MarriageRegistration>();
            foreach (MarriageRegistration r in regs)
            {
                temp.Add(r);
            }
            RegistrationBindingSource.DataSource = temp; 
            UpdateReportSources("FTAnalyzer_MarriageRegistration", "FTAnalyzer.Reports.MarriageReport.rdlc");
            this.Text = "Marriage Registrations to find";
        }

        private void UpdateReportSources(string dataSourceName, string reportName)
        {
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = dataSourceName;
            reportDataSource1.Value = this.RegistrationBindingSource;
            reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer.LocalReport.ReportEmbeddedResource = reportName;
            reportViewer.RefreshReport();
        }
    }
}
