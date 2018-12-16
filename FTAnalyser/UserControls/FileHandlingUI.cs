using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class FileHandlingUI : UserControl, IOptions
	{
        public FileHandlingUI()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
            chkLoadWithFilters.Checked = Properties.FileHandling.Default.LoadWithFilters;
            chkRetryFailedLines.Checked = Properties.FileHandling.Default.RetryFailedLines;
		}

		#region IOptions Members

		public void Save()
		{
            Properties.FileHandling.Default.LoadWithFilters = chkLoadWithFilters.Checked;
            Properties.FileHandling.Default.RetryFailedLines = chkRetryFailedLines.Checked;
            Properties.FileHandling.Default.Save();
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
			get { return "File Handling Settings"; }
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
        void ChkLoadWithFilters_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        void ChkRetryFailedLines_CheckedChanged(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

    }
}
