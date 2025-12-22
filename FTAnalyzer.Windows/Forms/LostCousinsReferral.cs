using FTAnalyzer.Filters;
using FTAnalyzer.Properties;
using FTAnalyzer.UserControls;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class LostCousinsReferral : Form
    {
        readonly ReportFormHelper reportFormHelper;
        readonly List<ExportReferrals> referrals;

        public LostCousinsReferral(Individual referee, bool onlyInCommon)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            FamilyTree ft = FamilyTree.Instance;
            Text = $"Lost Cousins Referral for {referee}";
            reportFormHelper = new ReportFormHelper(this, Text, dgLCReferrals, ResetTable, "Lost Cousins Referrals");
            dgLCReferrals.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgLCReferrals, true);
            CensusSettingsUI.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
            Predicate<Individual> lostCousinsFact = new(x => x.HasLostCousinsFact);
            List<Individual> lostCousinsFacts = ft.AllIndividuals.Filter(lostCousinsFact).ToList();
            referrals = new List<ExportReferrals>();
            foreach (Individual ind in lostCousinsFacts)
            {
                List<Fact> indLCFacts = new();
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

        string GetCountofRecords()
        {
            int total = referrals.Count;
            int direct = referrals.Count(x => x.RelationType.Equals(Messages.Referral_Direct));
            int blood = referrals.Count(x => x.RelationType.Equals(Messages.Referral_Blood));
            int marriage = referrals.Count(x => x.RelationType.Equals(Messages.Referral_Marriage));
            int others = referrals.Count(x => string.IsNullOrEmpty(x.RelationType));
            return total + $" Lost Cousins Records listed made up of {direct} Direct Ancestors, {blood} Blood Relatives, {marriage} Marriage and {others} Others.";
        }

        void ResetTable()
        {
            referrals.Sort(new LostCousinsReferralComparer());
            dgLCReferrals.DataSource = new SortableBindingList<ExportReferrals>(referrals);
        }

        void RefreshCensusReferences(object? sender, EventArgs e) => dgLCReferrals.Refresh();

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("LCReferralsColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Lost Cousins Referrals");
        }

        void MnuResetColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("LCReferralsColumns.xml");

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Lost Cousins Referral Report");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel(referrals.ToList<IExportReferrals>());

        void LostCousinsReferral_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void LostCousinsReferral_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
