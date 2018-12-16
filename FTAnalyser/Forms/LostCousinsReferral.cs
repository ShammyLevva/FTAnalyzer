using FTAnalyzer.Filters;
using FTAnalyzer.UserControls;
using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            Text = "Lost Cousins Referral for " + referee.ToString();
            reportFormHelper = new ReportFormHelper(this, Text, dgLCReferrals, ResetTable, "Lost Cousins Referrals");
            dgLCReferrals.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgLCReferrals, true);
            CensusSettingsUI.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
            Predicate<Individual> lostCousinsFact = new Predicate<Individual>(x => x.HasLostCousinsFact);
            List<Individual> lostCousinsFacts = ft.AllIndividuals.Filter(lostCousinsFact).ToList<Individual>();
            referrals = new List<ExportReferrals>();
            foreach (Individual ind in lostCousinsFacts)
            {
                List<Fact> indLCFacts = new List<Fact>();
                indLCFacts.AddRange(ind.GetFacts(Fact.LOSTCOUSINS));
                indLCFacts.AddRange(ind.GetFacts(Fact.LC_FTA));
                foreach (Fact f in indLCFacts)
                {
                    if ((onlyInCommon && ind.IsBloodDirect) || !onlyInCommon)
                        referrals.Add(new ExportReferrals(ind, f));
                }
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
            int others = referrals.Count(x => string.IsNullOrEmpty(x.RelationType));
            return total + $" Lost Cousins Records listed made up of {direct} Direct Ancestors, {blood} Blood Relatives, {marriage} Marriage and {others} Others.";
        }

        void ResetTable()
        {
            referrals.Sort(new LostCousinsReferralComparer());
            dgLCReferrals.DataSource = new SortableBindingList<ExportReferrals>(referrals);
        }

        void RefreshCensusReferences(object sender, EventArgs e)
        {
            dgLCReferrals.Refresh();
        }

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("LCReferralsColumns.xml");
            MessageBox.Show("Form Settings Saved", "Lost Cousins Referrals");
        }

        void MnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("LCReferralsColumns.xml");
        }

        void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Lost Cousins Referral Report");
        }

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        void MnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel(referrals.ToList<IExportReferrals>());
        }

        void LostCousinsReferral_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        void LostCousinsReferral_Load(object sender, EventArgs e)
        {
            SpecialMethods.SetFonts(this);
        }
    }
}
