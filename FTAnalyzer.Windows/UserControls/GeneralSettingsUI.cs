using FTAnalyzer.Properties;

namespace FTAnalyzer.UserControls
{
    public partial class GeneralSettingsUI : UserControl, IOptions
    {
        public GeneralSettingsUI()
        {
            InitializeComponent();
            //cannot be in load, because its possible this tab won't show, and the values will not be initialized.
            //if this happens, then the users settings will be cleared.
            chkUseBaptisms.Checked = GeneralSettings.Default.UseBaptismDates;
            chkUseBurials.Checked = GeneralSettings.Default.UseBurialDates;
            chkAllowEmptyLocations.Checked = GeneralSettings.Default.AllowEmptyLocations;
            chkMultipleFactForms.Checked = GeneralSettings.Default.MultipleFactForms;
            upDownAge.Value = GeneralSettings.Default.MinParentalAge;
            chkUseAlias.Checked = GeneralSettings.Default.ShowAliasInName;
            chkReverseLocations.Checked = GeneralSettings.Default.ReverseLocations;
            chkShowWorldEvents.Checked = GeneralSettings.Default.ShowWorldEvents;
            chkIgnoreFactTypeWarnings.Checked = GeneralSettings.Default.IgnoreFactTypeWarnings;
            chkTreatFemaleAsUnknown.Checked = GeneralSettings.Default.TreatFemaleSurnamesAsUnknown;
            chkMultiAncestor.Checked = GeneralSettings.Default.ShowMultiAncestors;
            chkSkipFixingLocations.Checked = GeneralSettings.Default.SkipFixingLocations;
            chkHideIgnoredDuplicates.Checked = GeneralSettings.Default.HideIgnoredDuplicates;
            chkIncludeAlternateFacts.Checked = GeneralSettings.Default.IncludeAlternateFacts;
        }

        #region IOptions Members

        public void Save()
        {
            GeneralSettings.Default.UseBaptismDates = chkUseBaptisms.Checked;
            GeneralSettings.Default.UseBurialDates = chkUseBurials.Checked;
            GeneralSettings.Default.AllowEmptyLocations = chkAllowEmptyLocations.Checked;
            GeneralSettings.Default.MinParentalAge = (int)upDownAge.Value;
            GeneralSettings.Default.MultipleFactForms = chkMultipleFactForms.Checked;
            GeneralSettings.Default.ShowAliasInName = chkUseAlias.Checked;
            GeneralSettings.Default.ReverseLocations = chkReverseLocations.Checked;
            GeneralSettings.Default.ShowWorldEvents = chkShowWorldEvents.Checked;
            GeneralSettings.Default.IgnoreFactTypeWarnings = chkIgnoreFactTypeWarnings.Checked;
            GeneralSettings.Default.TreatFemaleSurnamesAsUnknown = chkTreatFemaleAsUnknown.Checked;
            GeneralSettings.Default.ShowMultiAncestors = chkMultiAncestor.Checked;
            GeneralSettings.Default.SkipFixingLocations = chkSkipFixingLocations.Checked;
            GeneralSettings.Default.HideIgnoredDuplicates = chkHideIgnoredDuplicates.Checked;
            GeneralSettings.Default.IncludeAlternateFacts = chkIncludeAlternateFacts.Checked;
            Utilities.UIHelpers.SafeSaveSettings(GeneralSettings.Default);
            OnMinParentalAgeChanged();
            OnAliasInNameChanged();
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
                    {
                        break;
                    }
                }
            }

            return invalid;
        }

        public string DisplayName => "General Settings";

        public string TreePosition => DisplayName;

        public Image? MenuIcon => null;

        #endregion

        public static event EventHandler MinParentalAgeChanged;
        protected static void OnMinParentalAgeChanged() => MinParentalAgeChanged?.Invoke(null, EventArgs.Empty);

        public static event EventHandler AliasInNameChanged;
        protected static void OnAliasInNameChanged() => AliasInNameChanged?.Invoke(null, EventArgs.Empty);

        void ChkAllowEmptyLocations_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkReverseLocations_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkAddCreatedLocations_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkTreatFemaleAsUnknown_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkSkipFixingLocations_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void UpDownAge_ValueChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkIncludeAlternateFacts_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;
    }
}
