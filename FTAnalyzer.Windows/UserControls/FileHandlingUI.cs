using FTAnalyzer.Properties;

namespace FTAnalyzer.UserControls
{
    public partial class FileHandlingUI : UserControl, IOptions
    {
        // Options.cs constructs every settings tab's UserControl via reflection when the Options
        // dialog opens, regardless of which tab is actually shown, so this constructor always runs.
        // Without this guard, assigning a saved true/non-default value below fires the matching
        // CheckedChanged handler and spuriously sets ReloadRequired - even if the user never
        // touched this tab or changed anything (e.g. just changing the font level elsewhere).
        readonly bool _loading = true;

        public FileHandlingUI()
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            //cannot be in load, because its possible this tab won't show, and the values will not be initialized.
            //if this happens, then the users settings will be cleared.
            chkRetryFailedLines.Checked = FileHandling.Default.RetryFailedLines;
            chkConvertDiacritics.Checked = FileHandling.Default.ConvertDiacritics;
            _loading = false;
        }

        #region IOptions Members

        public void Save()
        {
            FileHandling.Default.RetryFailedLines = chkRetryFailedLines.Checked;
            FileHandling.Default.ConvertDiacritics = chkConvertDiacritics.Checked;
            Utilities.UIHelpers.SafeSaveSettings(FileHandling.Default);
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

        public Image? MenuIcon => Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Menu\description.png"));

        #endregion
        void ChkLoadWithFilters_CheckedChanged(object sender, EventArgs e) { if (!_loading) GeneralSettings.Default.ReloadRequired = true; }

        void ChkRetryFailedLines_CheckedChanged(object sender, EventArgs e) { if (!_loading) GeneralSettings.Default.ReloadRequired = true; }

        void ChkConvertDiacritics_CheckedChanged(object sender, EventArgs e) { if (!_loading) GeneralSettings.Default.ReloadRequired = true; }

    }
}
