using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Filters;
using FTAnalyzer.UserControls;

namespace FTAnalyzer.Forms
{
    public partial class LostCousinsReferral : Form
    {
        private ReportFormHelper reportFormHelper;
        List<ExportReferrals> referrals;

        public LostCousinsReferral(Individual referee, bool onlyInCommon)
        {
            InitializeComponent();
            FamilyTree ft = FamilyTree.Instance;
            this.Text = "Lost Cousins Referral for " + referee.ToString();
            reportFormHelper = new ReportFormHelper(this, this.Text, dgLCReferrals, this.ResetTable, "Lost Cousins Referrals");
            dgLCReferrals.AutoGenerateColumns = false;
            GeneralSettings.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
            Predicate<Individual> lostCousinsFact = new Predicate<Individual>(x => x.HasLostCousinsFact);
            List<Individual> lostCousinsFacts = ft.AllIndividuals.Where(lostCousinsFact).ToList<Individual>();
            referrals = new List<ExportReferrals>();
            foreach (Individual ind in lostCousinsFacts)
                foreach (Fact f in ind.GetFacts(Fact.LOSTCOUSINS))
                {
                    if((onlyInCommon && ind.IsBloodDirect) || !onlyInCommon)
                        referrals.Add(new ExportReferrals(ind, f));
                }
            reportFormHelper.LoadColumnLayout("LCReferralsColumns.xml");
            tsRecords.Text = GetCountofRecords();
        }

        private string GetCountofRecords()
        {
            int total = referrals.Count();
            int direct = referrals.Count(x => x.RelationType.Equals(Properties.Messages.Referral_Direct));
            int blood = referrals.Count(x => x.RelationType.Equals(Properties.Messages.Referral_Blood));
            int marriage = referrals.Count(x => x.RelationType.Equals(Properties.Messages.Referral_Marriage));
            int others = referrals.Count(x => x.RelationType.Equals(string.Empty));
            return total + " Lost Cousins Records listed made up of " + direct + " Direct Ancestors, " + blood + " Blood Relatives, "
                + marriage + " Marriage and " + others + " Others.";
        }

        private void ResetTable()
        {
            referrals.Sort(new LostCousinsReferralComparer());
            dgLCReferrals.DataSource = referrals;
        }

        private void RefreshCensusReferences(object sender, EventArgs e)
        {
            dgLCReferrals.Refresh();
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

        private void LostCousinsReferral_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
