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
            chkAllowEmptyLocations.Checked = Properties.GeneralSettings.Default.AllowEmptyLocations;
        }

		#region IOptions Members

		public void Save()
		{
            string message = string.Empty;
            string title = string.Empty;
			Properties.GeneralSettings.Default.UseBaptismDates = chkUseBaptisms.Checked;
            Properties.GeneralSettings.Default.AllowEmptyLocations = chkAllowEmptyLocations.Checked;
			Properties.GeneralSettings.Default.Save();
            OnUseBaptismDatesChanged();
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

        public static event EventHandler AllowEmptyLocationsChanged;
        protected static void OnAllowEmptyLocationsChanged()
        {
            if (AllowEmptyLocationsChanged != null)
                AllowEmptyLocationsChanged(null, EventArgs.Empty);
        }

        private void chkUseBaptisms_CheckedChanged(object sender, EventArgs e)
        {
            //if (FTAnalyzer.Properties.GeneralSettings.Default.UseBaptismDates)
            //    MessageBox.Show("Baptism dates will now be used if no birth date is present");
            //else
            //    MessageBox.Show("If no birth date is present, unknown will be shown");
        }

        private void chkAllowEmptyLocations_CheckedChanged(object sender, EventArgs e)
        {
            //if (FTAnalyzer.Properties.GeneralSettings.Default.AllowEmptyLocations)
            //    MessageBox.Show("Empty parts of a location will be allowed when you load the next GEDCOM file");
            //else
            //    MessageBox.Show("Locations with empty parts will be ignored when you load the next GEDCOM file");
        }
    }
}
