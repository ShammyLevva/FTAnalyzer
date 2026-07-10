using FTAnalyzer.Graphics;
using FTAnalyzer.Properties;
using System.Text.RegularExpressions;
using static FTAnalyzer.FactDate;

namespace FTAnalyzer.UserControls
{
    public partial class NonGedcomDateSettingsUI : UserControl, IOptions
    {

        public NonGedcomDateSettingsUI()
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            chkUseNonGedcomDates.Checked = NonGedcomDate.Default.UseNonGedcomDates;
            rbSlash.Checked = NonGedcomDate.Default.Separator == "/";
            rbDot.Checked = NonGedcomDate.Default.Separator == ".";
            rbDash.Checked = NonGedcomDate.Default.Separator == "-";
            rbSpace.Checked = NonGedcomDate.Default.Separator == " ";
            rbddmmyyyy.Checked = NonGedcomDate.Default.FormatSelected == (int)NonGEDCOMFormatSelected.DD_MM_YYYY;
            rbmmddyyyy.Checked = NonGedcomDate.Default.FormatSelected == (int)NonGEDCOMFormatSelected.MM_DD_YYYY;
            rbyyyyddmm.Checked = NonGedcomDate.Default.FormatSelected == (int)NonGEDCOMFormatSelected.YYYY_DD_MM;
            rbyyyymmdd.Checked = NonGedcomDate.Default.FormatSelected == (int)NonGEDCOMFormatSelected.YYYY_MM_DD;
        }

        #region IOptions Members

        public void Save()
        {
            Utilities.UIHelpers.SafeSaveSettings(NonGedcomDate.Default);
            NonGEDCOMDateFormatRegex = new Regex(NonGedcomDate.Default.Regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
                if (string.IsNullOrEmpty(errorProvider1.GetError(control.Controls[i])))
                {
                    invalid = CheckChildrenValidation(control.Controls[i]);
                    if (invalid)
                        break;
                }
                else
                {
                    invalid = true;
                    break;
                }
            }
            return invalid;
        }

        public string DisplayName => "Non GEDCOM Date Settings";

        public string TreePosition => DisplayName;

        public Image? MenuIcon => Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Menu\event.png"));

        #endregion

        void ChkUseNonGedcomDates_CheckedChanged(object sender, EventArgs e)
        {
            NonGedcomDate.Default.UseNonGedcomDates = chkUseNonGedcomDates.Checked;
            gbDateFormat.Enabled = chkUseNonGedcomDates.Checked;
            gbSeparator.Enabled = chkUseNonGedcomDates.Checked;
        }

        // The default (OS-drawn) GroupBox border is a thin etched line that ignores app colors
        // entirely - near-invisible in dark mode. Same fix as MainForm/CensusCountry/RelationTypes.
        // Shared by both group boxes on this page.
        void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender is GroupBox box)
                GraphicsUtilities.DrawGroupBox(box, e.Graphics, Theme.ActiveColors.Border, 2);
        }

        void NonGedcomDateSettingsUI_Leave(object sender, EventArgs e)
        {
            // Options.cs constructs every settings tab's UserControl via reflection when the
            // Options dialog opens (each starting Visible = false before being added to the
            // panel), regardless of which tab the user actually views. Adding controls to a
            // container in that loop can shuffle focus transiently and fire this Leave handler
            // for tabs the user never visited. Only a tab that's actually been shown (Visible)
            // can have had genuine user interaction worth recomputing settings and flagging a
            // reload for.
            if (!Visible)
                return;

            string separator = string.Empty;
            string regexSeparator = string.Empty;
            string dateformat = string.Empty;
            string regex = string.Empty;

            if (rbSlash.Checked) { separator = "/"; regexSeparator = @"\/"; }
            if (rbDot.Checked) { separator = "."; regexSeparator = @"\."; }
            if (rbDash.Checked) { separator = "-"; regexSeparator = @"-"; }
            if (rbSpace.Checked) { separator = " "; regexSeparator = @" "; }

            if (rbddmmyyyy.Checked)
            {
                dateformat = "dd" + separator + "MM" + separator + "yyyy";
                regex = @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                NonGedcomDate.Default.FormatSelected = (int)NonGEDCOMFormatSelected.DD_MM_YYYY;
            }
            if (rbmmddyyyy.Checked)
            {
                dateformat = "MM" + separator + "dd" + separator + "yyyy";
                regex = @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                NonGedcomDate.Default.FormatSelected = (int)NonGEDCOMFormatSelected.MM_DD_YYYY;
            }
            if (rbyyyyddmm.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                NonGedcomDate.Default.FormatSelected = (int)NonGEDCOMFormatSelected.YYYY_DD_MM;
            }
            if (rbyyyymmdd.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                NonGedcomDate.Default.FormatSelected = (int)NonGEDCOMFormatSelected.YYYY_MM_DD;
            }
            NonGedcomDate.Default.DateFormat = dateformat;
            NonGedcomDate.Default.Regex = regex;
            NonGedcomDate.Default.Separator = separator;
            GeneralSettings.Default.ReloadRequired = true;
        }

        void RbSlash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSlash.Checked)
            {
                rbddmmyyyy.Text = "dd/mm/yyyy";
                rbmmddyyyy.Text = "mm/dd/yyyy";
                rbyyyymmdd.Text = "yyyy/mm/dd";
                rbyyyyddmm.Text = "yyyy/dd/mm";
            }
        }

        void RbDot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDot.Checked)
            {
                rbddmmyyyy.Text = "dd.mm.yyyy";
                rbmmddyyyy.Text = "mm.dd.yyyy";
                rbyyyymmdd.Text = "yyyy.mm.dd";
                rbyyyyddmm.Text = "yyyy.dd.mm";
            }

        }

        void RbDash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDash.Checked)
            {
                rbddmmyyyy.Text = "dd-mm-yyyy";
                rbmmddyyyy.Text = "mm-dd-yyyy";
                rbyyyymmdd.Text = "yyyy-mm-dd";
                rbyyyyddmm.Text = "yyyy-dd-mm";
            }

        }

        void RbSpace_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpace.Checked)
            {
                rbddmmyyyy.Text = "dd mm yyyy";
                rbmmddyyyy.Text = "mm dd yyyy";
                rbyyyymmdd.Text = "yyyy mm dd";
                rbyyyyddmm.Text = "yyyy dd mm";
            }
        }
    }
}
