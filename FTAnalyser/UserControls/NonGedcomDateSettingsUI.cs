using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class NonGedcomDateSettingsUI : UserControl, IOptions
    {
        public NonGedcomDateSettingsUI()
        {
            InitializeComponent();
            chkUseNonGedcomDates.Checked = Properties.NonGedcomDate.Default.UseNonGedcomDates;
            rbSlash.Checked = Properties.NonGedcomDate.Default.Separator == "/";
            rbDot.Checked = Properties.NonGedcomDate.Default.Separator == ".";
            rbDash.Checked = Properties.NonGedcomDate.Default.Separator == "-";
            rbSpace.Checked = Properties.NonGedcomDate.Default.Separator == " ";
            rbddmmyyyy.Checked = Properties.NonGedcomDate.Default.FormatSelected == "dd/mm/yyyy";
            rbmmddyyyy.Checked = Properties.NonGedcomDate.Default.FormatSelected == "mm/dd/yyyy";
            rbyyyyddmm.Checked = Properties.NonGedcomDate.Default.FormatSelected == "yyyy/dd/mm";
            rbyyyymmdd.Checked = Properties.NonGedcomDate.Default.FormatSelected == "yyyy/mm/dd";
        }

        #region IOptions Members

        public void Save()
        {
            Properties.NonGedcomDate.Default.Save();
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
                regex = @"$\d{1,2}" + regexSeparator + @"$\d{1,2}" + regexSeparator + @"\d{4}^";
                Properties.NonGedcomDate.Default.FormatSelected = "dd/mm/yyyy";
            }
            if(rbmmddyyyy.Checked)
            {
                dateformat = "MM" + separator + "dd" + separator + "yyyy";
                regex = @"$\d{1,2}" + regexSeparator + @"$\d{1,2}" + regexSeparator + @"\d{4}^";
                Properties.NonGedcomDate.Default.FormatSelected = "mm/dd/yyyy";
            }
            if (rbyyyyddmm.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"$\d{4}" + regexSeparator + @"$\d{1,2}" + regexSeparator + @"\d{1,2}^";
                Properties.NonGedcomDate.Default.FormatSelected = "yyyy/mm/dd";
            }
            if (rbyyyymmdd.Checked)
            {
                dateformat = "yyyy" + separator + "MM" + separator + "dd";
                regex = @"$\d{4}" + regexSeparator + @"$\d{1,2}" + regexSeparator + @"\d{1,2}^";
                Properties.NonGedcomDate.Default.FormatSelected = "yyyy/mm/dd";
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
