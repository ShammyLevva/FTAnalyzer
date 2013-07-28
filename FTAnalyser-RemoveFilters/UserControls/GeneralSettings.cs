using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
	public partial class GeneralSettings : UserControl, IOptions
	{
        public GeneralSettings()
		{
			InitializeComponent();
			//cannot be in load, because its possible this tab won't show, and the values will not be initialized.
			//if this happens, then the users settings will be cleared.
            CK_Option1.Checked = Properties.GeneralSettings.Default.UseMultithreading;

        }

		#region IOptions Members

		public void Save()
		{
            string message = string.Empty;
            string title = string.Empty;
			Properties.GeneralSettings.Default.UseMultithreading = CK_Option1.Checked;
			Properties.GeneralSettings.Default.Save();
            OnDisplayBuffChanged();
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
			get { return "General Settings"; }
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

        public static event EventHandler DisplayBuffChanged;
        protected static void OnDisplayBuffChanged()
        {
            if (DisplayBuffChanged != null)
                DisplayBuffChanged(null, EventArgs.Empty);
        }
    }
}
