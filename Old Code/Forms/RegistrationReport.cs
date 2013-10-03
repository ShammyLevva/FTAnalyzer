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
        //private void BirthRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> birthComparator = new MultiComparator<Registration>();
        //    birthComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    birthComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), birthComparator);

        //    List<Registration> regs = ft.getAllBirthRegistrations();
        //    List<Registration> result = onlineBirthsRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupBirthRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

        //private void deathRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> deathComparator = new MultiComparator<Registration>();
        //    deathComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    deathComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), deathComparator);

        //    List<Registration> regs = ft.getAllDeathRegistrations();
        //    List<Registration> result = onlineDeathsRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupDeathRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

        //private void marriageRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> marriageComparator = new MultiComparator<Registration>();
        //    marriageComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    marriageComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), marriageComparator);

        //    List<Registration> regs = ft.getAllMarriageRegistrations();
        //    List<Registration> result = onlineMarriagesRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupMarriageRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

    }
}
