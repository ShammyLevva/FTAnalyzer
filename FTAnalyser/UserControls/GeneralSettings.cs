using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FTAnalyzer.UserControls
{
	public partial class GeneralSettings : UserControl, IOptions
	{
		public GeneralSettings()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
			chkUseBaptisms.Checked = Properties.GeneralSettings.Default.UseBaptismDates;
            chkUseBurials.Checked = Properties.GeneralSettings.Default.UseBurialDates;
			chkAllowEmptyLocations.Checked = Properties.GeneralSettings.Default.AllowEmptyLocations;
			chkCensusResidence.Checked = Properties.GeneralSettings.Default.UseResidenceAsCensus;
			chkTolerateInaccurateCensus.Checked = Properties.GeneralSettings.Default.TolerateInaccurateCensusDate;
            chkFamilyCensus.Checked = Properties.GeneralSettings.Default.OnlyCensusParents;
            chkMultipleFactForms.Checked = Properties.GeneralSettings.Default.MultipleFactForms;
            chkCompactCensusRef.Checked = Properties.GeneralSettings.Default.UseCompactCensusRef;
            upDownAge.Value = Properties.GeneralSettings.Default.MinParentalAge;
            chkUseAlias.Checked = Properties.GeneralSettings.Default.ShowAliasInName;
            chkHideMissingTagged.Checked = Properties.GeneralSettings.Default.HidePeopleWithMissingTag;
            chkReverseLocations.Checked = Properties.GeneralSettings.Default.ReverseLocations;
            chkAutoCreateCensus.Checked = Properties.GeneralSettings.Default.AutoCreateCensusFacts;
            chkAddCreatedLocations.Checked = Properties.GeneralSettings.Default.AddCreatedLocations;
            chkShowWorldEvents.Checked = Properties.GeneralSettings.Default.ShowWorldEvents;
            chkIgnoreFactTypeWarnings.Checked = Properties.GeneralSettings.Default.IgnoreFactTypeWarnings;
            chkTreatFemaleAsUnknown.Checked = Properties.GeneralSettings.Default.TreatFemaleSurnamesAsUnknown;
		}

		#region IOptions Members

		public void Save()
		{
			Properties.GeneralSettings.Default.UseBaptismDates = chkUseBaptisms.Checked;
            Properties.GeneralSettings.Default.UseBurialDates = chkUseBurials.Checked;
            Properties.GeneralSettings.Default.AllowEmptyLocations = chkAllowEmptyLocations.Checked;
			Properties.GeneralSettings.Default.UseResidenceAsCensus = chkCensusResidence.Checked;
			Properties.GeneralSettings.Default.TolerateInaccurateCensusDate = chkTolerateInaccurateCensus.Checked;
            Properties.GeneralSettings.Default.OnlyCensusParents = chkFamilyCensus.Checked;
            Properties.GeneralSettings.Default.MinParentalAge = (int)upDownAge.Value;
            Properties.GeneralSettings.Default.MultipleFactForms = chkMultipleFactForms.Checked;
            Properties.GeneralSettings.Default.UseCompactCensusRef = chkCompactCensusRef.Checked;
            Properties.GeneralSettings.Default.ShowAliasInName = chkUseAlias.Checked;
            Properties.GeneralSettings.Default.HidePeopleWithMissingTag = chkHideMissingTagged.Checked;
            Properties.GeneralSettings.Default.ReverseLocations = chkReverseLocations.Checked;
            Properties.GeneralSettings.Default.AutoCreateCensusFacts = chkAutoCreateCensus.Checked;
            Properties.GeneralSettings.Default.AddCreatedLocations = chkAddCreatedLocations.Checked;
            Properties.GeneralSettings.Default.ShowWorldEvents = chkShowWorldEvents.Checked;
            Properties.GeneralSettings.Default.IgnoreFactTypeWarnings = chkIgnoreFactTypeWarnings.Checked;
            Properties.GeneralSettings.Default.TreatFemaleSurnamesAsUnknown = chkTreatFemaleAsUnknown.Checked;
            Properties.GeneralSettings.Default.Save();
            OnMinParentalAgeChanged();
            OnCompactCensusRefChanged();
            OnAliasInNameChanged();
        }

        public void Cancel()
		{
			//NOOP;
		}

		public bool HasValidationErrors()
		{
			return CheckChildrenValidation(this);
		}

		private bool CheckChildrenValidation(Control control)
		{
			bool invalid = false;

			for (int i = 0; i < control.Controls.Count; i++)
			{
				if (!String.IsNullOrEmpty(errorProvider1.GetError(control.Controls[i])))
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

		public string DisplayName
		{
			get { return "General Settings"; }
		}

		public string TreePosition
		{
			get { return DisplayName; }
		}

		public Image MenuIcon
		{
			get { return null; }
		}

        #endregion

        public static event EventHandler MinParentalAgeChanged;
        protected static void OnMinParentalAgeChanged()
        {
            MinParentalAgeChanged?.Invoke(null, EventArgs.Empty);
        }

        public static event EventHandler AliasInNameChanged;
        protected static void OnAliasInNameChanged()
        {
            AliasInNameChanged?.Invoke(null, EventArgs.Empty);
        }

        public static event EventHandler CompactCensusRefChanged;
        protected static void OnCompactCensusRefChanged()
        {
            CompactCensusRefChanged?.Invoke(null, EventArgs.Empty);
        }

        private void ChkAllowEmptyLocations_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

		private void ChkTolerateInaccurateCensus_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

		private void ChkCensusResidence_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

        private void ChkFamilyCensus_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        private void ChkReverseLocations_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        private void ChkAutoCreateCensus_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        private void ChkAddCreatedLocations_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        private void ChkTreatFemaleAsUnknown_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }
    }
}
