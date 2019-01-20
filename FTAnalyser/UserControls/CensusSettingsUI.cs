using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class CensusSettingsUI : UserControl, IOptions
    {
        public CensusSettingsUI()
        {
            InitializeComponent();
            //cannot be in load, because its possible this tab won't show, and the values will not be initialized.
            //if this happens, then the users settings will be cleared.
            chkCensusResidence.Checked = Properties.GeneralSettings.Default.UseResidenceAsCensus;
            chkTolerateInaccurateCensus.Checked = Properties.GeneralSettings.Default.TolerateInaccurateCensusDate;
            chkFamilyCensus.Checked = Properties.GeneralSettings.Default.OnlyCensusParents;
            chkCompactCensusRef.Checked = Properties.GeneralSettings.Default.UseCompactCensusRef;
            chkHideMissingTagged.Checked = Properties.GeneralSettings.Default.HidePeopleWithMissingTag;
            chkAutoCreateCensus.Checked = Properties.GeneralSettings.Default.AutoCreateCensusFacts;
            chkAddCreatedLocations.Checked = Properties.GeneralSettings.Default.AddCreatedLocations;
            chkSkipCensusReferences.Checked = Properties.GeneralSettings.Default.SkipCensusReferences;
            chkConvertResidenceFacts.Checked = Properties.GeneralSettings.Default.ConvertResidenceFacts;
        }

        #region IOptions Members

        public void Save()
        {
            Properties.GeneralSettings.Default.UseResidenceAsCensus = chkCensusResidence.Checked;
            Properties.GeneralSettings.Default.TolerateInaccurateCensusDate = chkTolerateInaccurateCensus.Checked;
            Properties.GeneralSettings.Default.OnlyCensusParents = chkFamilyCensus.Checked;
            Properties.GeneralSettings.Default.UseCompactCensusRef = chkCompactCensusRef.Checked;
            Properties.GeneralSettings.Default.HidePeopleWithMissingTag = chkHideMissingTagged.Checked;
            Properties.GeneralSettings.Default.AutoCreateCensusFacts = chkAutoCreateCensus.Checked;
            Properties.GeneralSettings.Default.AddCreatedLocations = chkAddCreatedLocations.Checked;
            Properties.GeneralSettings.Default.SkipCensusReferences = chkSkipCensusReferences.Checked;
            Properties.GeneralSettings.Default.ConvertResidenceFacts = chkConvertResidenceFacts.Checked;
            Properties.GeneralSettings.Default.Save();
            OnCompactCensusRefChanged();
        }

        public void Cancel()
        {
            //NOOP;
        }

        public bool HasValidationErrors() => CheckChildrenValidation(this);

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

        public Image MenuIcon => null;

        #endregion

        public static event EventHandler CompactCensusRefChanged;
        protected static void OnCompactCensusRefChanged()
        {
            CompactCensusRefChanged?.Invoke(null, EventArgs.Empty);
        }

        void ChkTolerateInaccurateCensus_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;

        void ChkCensusResidence_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;

        void ChkConvertResidenceFact_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;

        void ChkFamilyCensus_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;

        void ChkAutoCreateCensus_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;

        void ChkAddCreatedLocations_CheckedChanged(object sender, EventArgs e) => Properties.GeneralSettings.Default.ReloadRequired = true;
    }
}
