using FTAnalyzer.Windows.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
	public partial class MappingSettingsUI : UserControl, IOptions
	{
        public MappingSettingsUI()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
            txtMapPath.Text = MappingSettings.Default.CustomMapPath;
            ckbUseParishBoundaries.Checked = MappingSettings.Default.UseParishBoundaries;
            chkIncludePartialGeocoded.Checked = MappingSettings.Default.IncludePartials;
            ckbHideScaleBar.Checked = MappingSettings.Default.HideScaleBar;
            txtGoogleAPI.Text = MappingSettings.Default.GoogleAPI;
		}

		#region IOptions Members

		public void Save()
		{
            MappingSettings.Default.CustomMapPath = txtMapPath.Text;
            MappingSettings.Default.UseParishBoundaries = ckbUseParishBoundaries.Checked;
            MappingSettings.Default.HideScaleBar = ckbHideScaleBar.Checked;
            MappingSettings.Default.IncludePartials = chkIncludePartialGeocoded.Checked;
            MappingSettings.Default.GoogleAPI = txtGoogleAPI.Text;
            MappingSettings.Default.Save();
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

        void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
                txtMapPath.Text = folderBrowserDialog.SelectedPath;
        }
    }
}
