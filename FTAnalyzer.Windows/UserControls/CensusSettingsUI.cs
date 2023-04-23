using FTAnalyzer.Properties;

namespace FTAnalyzer.UserControls
{
    public partial class CensusSettingsUI : UserControl, IOptions
    {
        public CensusSettingsUI()
        {
            InitializeComponent();
            //cannot be in load, because its possible this tab won't show, and the values will not be initialized.
            //if this happens, then the users settings will be cleared.
            chkCensusResidence.Checked = GeneralSettings.Default.UseResidenceAsCensus;
            chkTolerateInaccurateCensus.Checked = GeneralSettings.Default.TolerateInaccurateCensusDate;
            chkFamilyCensus.Checked = GeneralSettings.Default.OnlyCensusParents;
            chkCompactCensusRef.Checked = GeneralSettings.Default.UseCompactCensusRef;
            chkHideMissingTagged.Checked = GeneralSettings.Default.HidePeopleWithMissingTag;
            chkAutoCreateCensus.Checked = GeneralSettings.Default.AutoCreateCensusFacts;
            chkAddCreatedLocations.Checked = GeneralSettings.Default.AddCreatedLocations;
            chkSkipCensusReferences.Checked = GeneralSettings.Default.SkipCensusReferences;
            chkConvertResidenceFacts.Checked = GeneralSettings.Default.ConvertResidenceFacts;
        }

        #region IOptions Members

        public void Save()
        {
            GeneralSettings.Default.UseResidenceAsCensus = chkCensusResidence.Checked;
            GeneralSettings.Default.TolerateInaccurateCensusDate = chkTolerateInaccurateCensus.Checked;
            GeneralSettings.Default.OnlyCensusParents = chkFamilyCensus.Checked;
            GeneralSettings.Default.UseCompactCensusRef = chkCompactCensusRef.Checked;
            GeneralSettings.Default.HidePeopleWithMissingTag = chkHideMissingTagged.Checked;
            GeneralSettings.Default.AutoCreateCensusFacts = chkAutoCreateCensus.Checked;
            GeneralSettings.Default.AddCreatedLocations = chkAddCreatedLocations.Checked;
            GeneralSettings.Default.SkipCensusReferences = chkSkipCensusReferences.Checked;
            GeneralSettings.Default.ConvertResidenceFacts = chkConvertResidenceFacts.Checked;
            GeneralSettings.Default.Save();
            OnCompactCensusRefChanged();
        }

        public void Cancel()
        {
            //NOOP;
        }

        public bool HasValidationErrors => CheckChildrenValidation(this);

        bool CheckChildrenValidation(Control control)
        {
            bool invalid = false;

            for (int i = 0; i < control.Controls.Count; i++)
            {
                if (!string.IsNullOrEmpty(errorProvider1.GetError(control.Controls[i])))
                {
                    invalid = true;
                    break;
                }
                else
                {
                    invalid = CheckChildrenValidation(control.Controls[i]);
                    if (invalid)
                        break;
                }
            }
            return invalid;
        }

        public string DisplayName => "Census Settings";

        public string TreePosition => DisplayName;

        public Image? MenuIcon => null;

        #endregion

        public static event EventHandler CompactCensusRefChanged;
        protected static void OnCompactCensusRefChanged()
        {
            CompactCensusRefChanged?.Invoke(null, EventArgs.Empty);
        }

        void ChkTolerateInaccurateCensus_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkCensusResidence_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkConvertResidenceFact_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkFamilyCensus_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkAutoCreateCensus_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkAddCreatedLocations_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkSkipCensusReferences_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;
    }
}
