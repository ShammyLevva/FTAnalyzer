using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
	public partial class ProxySettings : UserControl, IOptions
	{
		public ProxySettings()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
			ProxyType.SelectedItem = ProxyType.Items[0];
			RequiresAuthCheckBox.Checked = Properties.NetworkSettings.Default.ProxyRequiresAuthentication;
			Password.Text = Properties.NetworkSettings.Default.ProxyPassword;
            DomainTextBox.Text = Properties.NetworkSettings.Default.ProxyDomain;
            ProxyPort.Text = Properties.NetworkSettings.Default.ProxyPort.ToString();
			ProxyHost.Text = Properties.NetworkSettings.Default.ProxyServer;
			ProxyType.Text = Properties.NetworkSettings.Default.ProxyType;
			UserName.Text = Properties.NetworkSettings.Default.ProxyUserName;
			UseDefaultProxySettingsCheckBox.Checked = Properties.NetworkSettings.Default.UseDefaultProxySettings;
            UseDefaultCredentials.Checked = Properties.NetworkSettings.Default.UseDefaultAuthenticationForProxy;

            checkBox1_CheckedChanged(null, null);
			RequiresAuthCheckBox_CheckedChanged(null, null);
			ProxyType_SelectedIndexChanged(null, null);
			UseDefaultProxySettingsCheckBox_CheckedChanged(null, null);
		}

		private void UseDefaultProxySettingsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (UseDefaultProxySettingsCheckBox.Checked)
			{
				ProxyHost.Enabled = false;
				ProxyPort.Enabled = false;
			}
			else
			{
				ProxyPort.Enabled = true;
				ProxyHost.Enabled = true;
			}
		}

		private void ProxyType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ProxyType.SelectedItem != null && ProxyType.SelectedItem.ToString() == "None")
			{
				SettingsGroupBox.Enabled = false;
			}
			else
			{
				SettingsGroupBox.Enabled = true;
			}
		}

		private void RequiresAuthCheckBox_CheckedChanged(object sender, EventArgs e)
		{
            if (RequiresAuthCheckBox.Checked)
            {
                AuthGroupBox.Enabled = true;
            }
            else
            {
                AuthGroupBox.Enabled = false;
            }
		}

		private void ProxyPort_Validating(object sender, CancelEventArgs e)
		{
			int port;
			if (UseDefaultProxySettingsCheckBox.Checked == false && (!int.TryParse(ProxyPort.Text, out port) || port < 0))
			{
				errorProvider1.SetError(ProxyPort, "Value must be an integer greater then 0");
			}
			else
			{
				errorProvider1.SetError(ProxyPort, "");
			}
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!UseDefaultCredentials.Checked)
            {
                UserName.Enabled = true;
                Password.Enabled = true;
                DomainTextBox.Enabled = true;
            }
            else
            {
                UserName.Enabled = false;
                Password.Enabled = false;
                DomainTextBox.Enabled = false;
            }
        }


		#region IOptions Members


		public void Save()
		{
			if (HasValidationErrors())
			{
				throw new Exception("Settings are not in a valid state.  Cannot Save");
			}
			else
			{
				Properties.NetworkSettings.Default.ProxyRequiresAuthentication = RequiresAuthCheckBox.Checked;
				Properties.NetworkSettings.Default.ProxyPassword = Password.Text;
				Properties.NetworkSettings.Default.ProxyPort = int.Parse(ProxyPort.Text);
				Properties.NetworkSettings.Default.ProxyServer = ProxyHost.Text;
				Properties.NetworkSettings.Default.ProxyType = ProxyType.Text;
				Properties.NetworkSettings.Default.ProxyUserName = UserName.Text;
				Properties.NetworkSettings.Default.UseDefaultProxySettings = UseDefaultProxySettingsCheckBox.Checked;
                Properties.NetworkSettings.Default.UseDefaultAuthenticationForProxy = UseDefaultCredentials.Checked;
                Properties.NetworkSettings.Default.ProxyDomain = DomainTextBox.Text;
				Properties.NetworkSettings.Default.Save();
			}
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
			get { return "Proxy Settings"; }
		}



		public string TreePosition
		{
			get { return Options.NETWORK_SETTINGS + Options.MENU_DELIMETER + DisplayName; }
		}

		public Image MenuIcon
		{
			get { return OptionImageList.Images[0]; }
		}

		#endregion
	}
}
