using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class FontSettingsUI : UserControl, IOptions
    {
        private Font selectedFont;
        private int fontNumber;
        private float fontWidth;
        private float fontSize;

        public FontSettingsUI()
        {
            InitializeComponent();
            fontNumber = Properties.FontSettings.Default.FontNumber;
            tbFontScale.Value = fontNumber;
            SetSelectedFont(fontNumber);
        }

        #region IOptions Members

        public void Save()
        {
            Properties.FontSettings.Default.SelectedFont = selectedFont;
            Properties.FontSettings.Default.FontNumber = tbFontScale.Value;
            Properties.FontSettings.Default.FontWidth = fontWidth;
            Properties.FontSettings.Default.FontSize = fontSize;
            Properties.FontSettings.Default.Save();
            OnFontChanged();
        }

        public void Cancel()
        {
            //NOOP;
        }

        public bool HasValidationErrors()
        {
            return CheckChildrenValidation(this);
        }

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
            get { return "Font Settings"; }
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

        public static event EventHandler GlobalFontChanged;
        protected static void OnFontChanged()
        {
            //Update Fonts on all forms
            GlobalFontChanged?.Invoke(null, EventArgs.Empty);
        }

        void SetSelectedFont(int value)
        {
            switch (value)
            {
                case 1:
                    selectedFont = new Font(lbSample.Font.Name, 8.25f);
                    fontWidth = 5.8f;
                    fontSize = 8.25f;
                    break;
                case 2:
                    selectedFont = new Font(lbSample.Font.Name, 10f);
                    fontWidth = 6.6f;
                    fontSize = 10f;
                    break;
                case 3:
                    selectedFont = new Font(lbSample.Font.Name, 12f);
                    fontWidth = 8.0f;
                    fontSize = 12f;
                    break;
                case 4:
                    selectedFont = new Font(lbSample.Font.Name, 14f);
                    fontWidth = 9.6f;
                    fontSize = 14f;
                    break;
                default:
                    selectedFont = new Font(lbSample.Font.Name, 8.25f);
                    fontWidth = 5.8f;
                    fontSize = 8.25f;
                    break;
            }
            lbSample.Font = selectedFont;
        }

        void tbFontScale_Scroll(object sender, EventArgs e)
        {
            SetSelectedFont(tbFontScale.Value);
        }
    }
}
