﻿using System;
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
            chkIncludePartialGeocoded.Checked = Properties.GeneralSettings.Default.IncludePartials;
            chkFamilyCensus.Checked = Properties.GeneralSettings.Default.OnlyCensusParents;
            upDownAge.Value = Properties.GeneralSettings.Default.MinParentalAge;
			Properties.GeneralSettings.Default.ReloadRequired = false;
		}

		#region IOptions Members

		public void Save()
		{
			Properties.GeneralSettings.Default.UseBaptismDates = chkUseBaptisms.Checked;
            Properties.GeneralSettings.Default.UseBurialDates = chkUseBurials.Checked;
            Properties.GeneralSettings.Default.AllowEmptyLocations = chkAllowEmptyLocations.Checked;
			Properties.GeneralSettings.Default.UseResidenceAsCensus = chkCensusResidence.Checked;
			Properties.GeneralSettings.Default.TolerateInaccurateCensusDate = chkTolerateInaccurateCensus.Checked;
            Properties.GeneralSettings.Default.IncludePartials = chkIncludePartialGeocoded.Checked;
            Properties.GeneralSettings.Default.OnlyCensusParents = chkFamilyCensus.Checked;
            Properties.GeneralSettings.Default.MinParentalAge = (int)upDownAge.Value;
			Properties.GeneralSettings.Default.Save();
			OnUseBaptismDatesChanged();
			OnAllowEmptyLocationsChanged();
			OnUseResidenceAsCensusChanged();
			OnTolerateInaccurateCensusChanged();
            OnIncludePartialGeocodedChanged();
            OnOnlyCensusParentsChanged();
            OnMinParentalAgeChanged();
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

		public static event EventHandler UseBaptismDatesChanged;
		protected static void OnUseBaptismDatesChanged()
		{
			if (UseBaptismDatesChanged != null)
				UseBaptismDatesChanged(null, EventArgs.Empty);
		}

        public static event EventHandler UseBurialDatesChanged;
        protected static void OnUseBurialDatesChanged()
        {
            if (UseBurialDatesChanged != null)
                UseBurialDatesChanged(null, EventArgs.Empty);
        }

        public static event EventHandler AllowEmptyLocationsChanged;
		protected static void OnAllowEmptyLocationsChanged()
		{
			if (AllowEmptyLocationsChanged != null)
				AllowEmptyLocationsChanged(null, EventArgs.Empty);
		}

		public static event EventHandler UseResidenceAsCensusChanged;
		protected static void OnUseResidenceAsCensusChanged()
		{
			if (UseResidenceAsCensusChanged != null)
				UseResidenceAsCensusChanged(null, EventArgs.Empty);
		}

		public static event EventHandler TolerateInaccurateCensusChanged;
		protected static void OnTolerateInaccurateCensusChanged()
		{
			if (TolerateInaccurateCensusChanged != null)
				TolerateInaccurateCensusChanged(null, EventArgs.Empty);
		}

        public static event EventHandler IncludePartialGeocodedChanged;
        protected static void OnIncludePartialGeocodedChanged()
        {
            if (IncludePartialGeocodedChanged != null)
                IncludePartialGeocodedChanged(null, EventArgs.Empty);
        }

        public static event EventHandler OnlyCensusParentsChanged;
        protected static void OnOnlyCensusParentsChanged()
        {
            if (OnlyCensusParentsChanged != null)
                OnlyCensusParentsChanged(null, EventArgs.Empty);
        }

        public static event EventHandler MinParentalAgeChanged;
                protected static void OnMinParentalAgeChanged()
        {
            if (MinParentalAgeChanged != null)
                MinParentalAgeChanged(null, EventArgs.Empty);
        }

        private void chkAllowEmptyLocations_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

		private void chkTolerateInaccurateCensus_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

		private void chkCensusResidence_CheckedChanged(object sender, EventArgs e)
		{
			Properties.GeneralSettings.Default.ReloadRequired = true;
		}

        private void chkFamilyCensus_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }
	}
}