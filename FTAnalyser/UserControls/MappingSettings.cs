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
	public partial class MappingSettings : UserControl, IOptions
	{
        public MappingSettings()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
            txtMapPath.Text = Properties.MappingSettings.Default.CustomMapPath;
            ckbUseParishBoundaries.Checked = Properties.MappingSettings.Default.UseEnglishParishBoundaries;
            ckbHideScaleBar.Checked = Properties.MappingSettings.Default.HideScaleBar;
		}

		#region IOptions Members

		public void Save()
		{
            Properties.MappingSettings.Default.CustomMapPath = txtMapPath.Text;
            Properties.MappingSettings.Default.UseEnglishParishBoundaries = ckbUseParishBoundaries.Checked;
            Properties.MappingSettings.Default.HideScaleBar = ckbHideScaleBar.Checked;
            Properties.MappingSettings.Default.Save();
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
			get { return "Mapping Settings"; }
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

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
                txtMapPath.Text = folderBrowserDialog.SelectedPath;
        }
    }
}
