using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
	public partial class NetworkSettings : UserControl, IOptions
	{
		private const string IE6 = "Internet Explorer 6";
		private const string IE7 = "Internet Explorer 7";
		private const string FIREFOX2 = "FireFox 2";
		public NetworkSettings()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
			string value = FIREFOX2;
			if (Properties.NetworkSettings.Default.UserAgent_IE6 == Properties.NetworkSettings.Default.UserAgent)
			{
				value = IE6;
			}else if (Properties.NetworkSettings.Default.UserAgent_IE7 == Properties.NetworkSettings.Default.UserAgent)
			{
				value = IE7;
			}
			
			for (int i = 0; i < UserAgentDropDown.Items.Count; i++)
			{
				if (UserAgentDropDown.Items[i].ToString() == value)
				{
					UserAgentDropDown.SelectedItem = UserAgentDropDown.Items[i];
					break;
				}
			}
			MaxRequests.Value = Properties.NetworkSettings.Default.MaxHttpRequests;
			this.extendedToolTipLabel2.ToolTipText = 
                    "The user agent string that FTAnalyzer will use when connecting to the Web." + Environment.NewLine +
					"It is recommended to only change this if you are experiencing an issue" + Environment.NewLine +
                    "that you know is related to this setting.";
		}


		#region IOptions Members

		public void Save()
		{
			Properties.NetworkSettings.Default.MaxHttpRequests = Convert.ToInt32(MaxRequests.Value);
			
			switch(UserAgentDropDown.SelectedItem.ToString())
			{
				case IE6:
					Properties.NetworkSettings.Default.UserAgent = Properties.NetworkSettings.Default.UserAgent_IE6;
					break;
				case IE7:
					Properties.NetworkSettings.Default.UserAgent = Properties.NetworkSettings.Default.UserAgent_IE7;
					break;
				default:
					Properties.NetworkSettings.Default.UserAgent = Properties.NetworkSettings.Default.UserAgent_FireFox2;
					break;
			}
			Properties.NetworkSettings.Default.Save();
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
			get { return "Http Settings"; }
		}


		public string TreePosition
		{
			get { return Options.NETWORK_SETTINGS +  Options.MENU_DELIMETER + DisplayName; }
		}

		public Image MenuIcon
		{
			get { return null; }
		}

		#endregion
	}
}
