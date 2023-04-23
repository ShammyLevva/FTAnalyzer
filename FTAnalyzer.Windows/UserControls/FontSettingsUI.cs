using FTAnalyzer.Properties;

namespace FTAnalyzer.UserControls
{
    public partial class FontSettingsUI : UserControl, IOptions
    {
        Font selectedFont;
        readonly int fontNumber;
        float fontWidth;
        float fontSize;
        int fontHeight;

        public FontSettingsUI()
        {
            InitializeComponent();
            fontNumber = FontSettings.Default.FontNumber;
            tbFontScale.Value = fontNumber;
            SetSelectedFont(fontNumber);
        }

        #region IOptions Members

        public void Save()
        {
            FontSettings.Default.SelectedFont = selectedFont;
            FontSettings.Default.FontNumber = tbFontScale.Value;
            FontSettings.Default.FontWidth = fontWidth;
            FontSettings.Default.FontSize = fontSize;
            FontSettings.Default.FontHeight = fontHeight;
            FontSettings.Default.Save();
            OnFontChanged();
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

        public string DisplayName => "Font Settings";

        public string TreePosition => DisplayName;

        public Image? MenuIcon => null;

        #endregion

        public static event EventHandler GlobalFontChanged;
        protected static void OnFontChanged()
        {
            //Update Fonts on all forms
            GlobalFontChanged?.Invoke(null, EventArgs.Empty);
        }

        void SetSelectedFont(int value)
        {
            try
            {
                switch (value)
                {
                    case 1:
                        selectedFont = new(lbSample.Font.Name, 8.25f);
                        fontWidth = 5.8f;
                        fontSize = 8.25f;
                        fontHeight = 22;
                        break;
                    case 2:
                        selectedFont = new(lbSample.Font.Name, 10f);
                        fontWidth = 6.6f;
                        fontSize = 10f;
                        fontHeight = 27;
                        break;
                    case 3:
                        selectedFont = new(lbSample.Font.Name, 12f);
                        fontWidth = 8.0f;
                        fontSize = 12f;
                        fontHeight = 32;
                        break;
                    case 4:
                        selectedFont = new(lbSample.Font.Name, 14f);
                        fontWidth = 9.6f;
                        fontSize = 14f;
                        fontHeight = 37;
                        break;
                    default:
                        selectedFont = new(lbSample.Font.Name, 8.25f);
                        fontWidth = 5.8f;
                        fontSize = 8.25f;
                        fontHeight = 22;
                        break;
                }
                lbSample.Font = selectedFont;
            }
            catch (Exception) { }
        }

        void TbFontScale_Scroll(object sender, EventArgs e) => SetSelectedFont(tbFontScale.Value);
    }
}
