using FTAnalyzer.Properties;
using FTAnalyzer.Windows.Properties;

namespace FTAnalyzer.UserControls
{
    public partial class FileHandlingUI : UserControl, IOptions
	{
        public FileHandlingUI()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
            chkRetryFailedLines.Checked = FileHandling.Default.RetryFailedLines;
            chkConvertDiacritics.Checked = FileHandling.Default.ConvertDiacritics;
		}

		#region IOptions Members

		public void Save()
		{
            FileHandling.Default.RetryFailedLines = chkRetryFailedLines.Checked;
            FileHandling.Default.ConvertDiacritics = chkConvertDiacritics.Checked;
            FileHandling.Default.Save();
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

        public string DisplayName => "File Handling Settings";

        public string TreePosition => DisplayName;

        public Image MenuIcon => null;

        #endregion
        void ChkLoadWithFilters_CheckedChanged(object sender, EventArgs e) =>  GeneralSettings.Default.ReloadRequired = true;

        void ChkRetryFailedLines_CheckedChanged(object sender, EventArgs e) => GeneralSettings.Default.ReloadRequired = true;

        void ChkConvertDiacritics_CheckedChanged(object sender, EventArgs e) =>  GeneralSettings.Default.ReloadRequired = true;

    }
}
