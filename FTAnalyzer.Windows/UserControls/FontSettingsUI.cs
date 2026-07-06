using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.UserControls
{
    public partial class FontSettingsUI : UserControl, IOptions
    {
        Font? selectedFont;
        readonly int fontNumber;
        float fontWidth;
        float fontSize;
        int fontHeight;

        public FontSettingsUI()
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            fontNumber = FontSettings.Default.FontNumber;
            tbFontScale.Value = fontNumber;
            SetSelectedFont(fontNumber);
            switch (Theme.ActiveColors.Mode)
            {
                case Theme.AppThemeMode.Light:
                    rbThemeLight.Checked = true;
                    break;
                case Theme.AppThemeMode.Dark:
                    rbThemeDark.Checked = true;
                    break;
                default:
                    rbThemeSystemDefault.Checked = true;
                    break;
            }
        }

        #region IOptions Members

        public void Save()
        {
            if (selectedFont is not null) FontSettings.Default.SelectedFont = selectedFont;
            FontSettings.Default.FontNumber = tbFontScale.Value;
            FontSettings.Default.FontWidth = fontWidth;
            FontSettings.Default.FontSize = fontSize;
            FontSettings.Default.FontHeight = fontHeight;
            Utilities.UIHelpers.SafeSaveSettings(FontSettings.Default);
            OnFontChanged();
            Theme.AppThemeMode mode = rbThemeLight.Checked ? Theme.AppThemeMode.Light
                : rbThemeDark.Checked ? Theme.AppThemeMode.Dark
                : Theme.AppThemeMode.SystemDefault;
            Theme.ActiveColors.SetMode(mode);
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

        public string DisplayName => "Graphics Mode/Font Settings";

        public string TreePosition => DisplayName;

        public Image? MenuIcon => Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Menu\format_size.png"));

        #endregion

        public static event EventHandler? GlobalFontChanged;
        protected static void OnFontChanged()
        {
            //Update Fonts on all forms
            GlobalFontChanged?.Invoke(null, EventArgs.Empty);
        }

        void SetSelectedFont(int value)
        {
            try
            {
                FontScaleLevel level = FontScale.ForLevel(value);
                fontWidth = level.FontWidth;
                fontSize = level.FontSize;
                fontHeight = level.FontHeight;
                selectedFont = new(lbSample.Font.Name, fontSize);
                lbSample.Font = selectedFont;
            }
            catch (Exception) { }
        }

        void TbFontScale_Scroll(object sender, EventArgs e) => SetSelectedFont(tbFontScale.Value);
    }
}
