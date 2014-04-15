using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Filters;

namespace FTAnalyzer.Forms
{
    public partial class LostCousinsReferral : Form
    {
        private ReportFormHelper reportFormHelper;

        public LostCousinsReferral(bool onlyInCommon)
        {
            InitializeComponent();
            FamilyTree ft = FamilyTree.Instance;
            Predicate<Individual> lostCousinsFact = new Predicate<Individual>(x => x.HasLostCousinsFact);
            List<Individual> lostCousinsFacts = ft.AllIndividuals.Where(lostCousinsFact).ToList<Individual>();
            List<ExportReferrals> referrals = new List<ExportReferrals>();
            foreach (Individual ind in lostCousinsFacts)
                foreach (Fact f in ind.GetFacts(Fact.LOSTCOUSINS))
                {
                    if((onlyInCommon && ind.IsBloodDirect) || !onlyInCommon)
                        referrals.Add(new ExportReferrals(ind, f));
                }
            referrals.Sort(new LostCousinsReferralComparer());
            dgLCReferrals.AutoGenerateColumns = false;
            dgLCReferrals.DataSource = referrals;
            reportFormHelper = new ReportFormHelper(this, this.Text, dgLCReferrals, this.ResetTable, "Lost Cousins Referrals");
        }

        private void ResetTable()
        {
            dgLCReferrals.Sort(new LostCousinsReferralComparer());
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("LCReferralsColumns.xml");
            MessageBox.Show("Form Settings Saved", "Lost Cousins Referrals");
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("LCReferralsColumns.xml");
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Lost Cousins Referral Report");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayFact>();
        }
    }
}
