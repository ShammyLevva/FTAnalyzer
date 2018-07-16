using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class NonGedcomDateSettingsUI : UserControl, IOptions
    {
        private static Regex _regex;
        public enum FormatSelected { DD_MM_YYYY = 1, MM_DD_YYYY = 2, YYYY_MM_DD = 3, YYYY_DD_MM = 4 }

        public static Regex NonGEDCOMDateFormatRegex
        {
            get { return _regex ?? new Regex(Properties.NonGedcomDate.Default.Regex, RegexOptions.Compiled | RegexOptions.IgnoreCase); }
        }

        public NonGedcomDateSettingsUI()
        {
            InitializeComponent();
            chkUseNonGedcomDates.Checked = Properties.NonGedcomDate.Default.UseNonGedcomDates;
            rbSlash.Checked = Properties.NonGedcomDate.Default.Separator == "/";
            rbDot.Checked = Properties.NonGedcomDate.Default.Separator == ".";
            rbDash.Checked = Properties.NonGedcomDate.Default.Separator == "-";
            rbSpace.Checked = Properties.NonGedcomDate.Default.Separator == " ";
            rbddmmyyyy.Checked = Properties.NonGedcomDate.Default.FormatSelected == (int)FormatSelected.DD_MM_YYYY;
            rbmmddyyyy.Checked = Properties.NonGedcomDate.Default.FormatSelected == (int)FormatSelected.MM_DD_YYYY;
            rbyyyyddmm.Checked = Properties.NonGedcomDate.Default.FormatSelected == (int)FormatSelected.YYYY_DD_MM;
            rbyyyymmdd.Checked = Properties.NonGedcomDate.Default.FormatSelected == (int)FormatSelected.YYYY_MM_DD;
        }

        #region IOptions Members

        public void Save()
        {
            Properties.NonGedcomDate.Default.Save();
            _regex = new Regex(Properties.NonGedcomDate.Default.Regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
            get { return "Non GEDCOM Date Settings"; }
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

        private void ChkUseNonGedcomDates_CheckedChanged(object sender, EventArgs e)
        {
            Properties.NonGedcomDate.Default.UseNonGedcomDates = chkUseNonGedcomDates.Checked;
            gbDateFormat.Enabled = chkUseNonGedcomDates.Checked;
            gbSeparator.Enabled = chkUseNonGedcomDates.Checked;
        }

        private void NonGedcomDateSettingsUI_Leave(object sender, EventArgs e)
        {
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
                Properties.NonGedcomDate.Default.FormatSelected = (int)FormatSelected.DD_MM_YYYY;
            }
            if(rbmmddyyyy.Checked)
            {
                dateformat = "MM" + separator + "dd" + separator + "yyyy";
                regex = @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                Properties.NonGedcomDate.Default.FormatSelected = (int)FormatSelected.MM_DD_YYYY;
            }
            if (rbyyyyddmm.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                Properties.NonGedcomDate.Default.FormatSelected = (int)FormatSelected.YYYY_DD_MM;
            }
            if (rbyyyymmdd.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                Properties.NonGedcomDate.Default.FormatSelected = (int)FormatSelected.YYYY_MM_DD;
            }
            Properties.NonGedcomDate.Default.DateFormat = dateformat;
            Properties.NonGedcomDate.Default.Regex = regex;
            Properties.NonGedcomDate.Default.Separator = separator;
            Properties.GeneralSettings.Default.ReloadRequired = true;
        }

        private void RbSlash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSlash.Checked)
            {
                rbddmmyyyy.Text = "dd/mm/yyyy";
                rbmmddyyyy.Text = "mm/dd/yyyy";
                rbyyyymmdd.Text = "yyyy/mm/dd";
                rbyyyyddmm.Text = "yyyy/dd/mm";
            }
        }

        private void RbDot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDot.Checked)
            {
                rbddmmyyyy.Text = "dd.mm.yyyy";
                rbmmddyyyy.Text = "mm.dd.yyyy";
                rbyyyymmdd.Text = "yyyy.mm.dd";
                rbyyyyddmm.Text = "yyyy.dd.mm";
            }

        }

        private void RbDash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDash.Checked)
            {
                rbddmmyyyy.Text = "dd-mm-yyyy";
                rbmmddyyyy.Text = "mm-dd-yyyy";
                rbyyyymmdd.Text = "yyyy-mm-dd";
                rbyyyyddmm.Text = "yyyy-dd-mm";
            }

        }

        private void RbSpace_CheckedChanged(object sender, EventArgs e)
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
