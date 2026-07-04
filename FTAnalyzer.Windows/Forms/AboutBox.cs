using FTAnalyzer.Utilities;
using System.Reflection;

namespace FTAnalyzer.Core.Displays
{
    partial class AboutBox1 : Form
    {
        readonly Font? heroFont;

        public AboutBox1(string version, Font? heroFont = null)
        {
            InitializeComponent();
            this.heroFont = heroFont;
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? Icon;
            logoPictureBox.Image = Properties.Resources._256;
            Text = $"About {AssemblyTitle}";
            labelProductName.Text = "Family Tree Analyzer";
            labelVersion.Text = $"Version {version}";
            labelCopyright.Text = AssemblyCopyright;
            textBoxDescription.Text = AssemblyDescription;
            Top += NativeMethods.TopTaskbarOffset;
            ApplyTheme();
        }

        void ApplyTheme()
        {
            BackColor = Theme.Colors.BgParchment;
            tableLayoutPanel.BackColor = Theme.Colors.BgParchment;
            labelProductName.ForeColor = Theme.Colors.GoldDark;
            labelVersion.ForeColor = Theme.Colors.SecondaryCharcoalBark;
            labelCopyright.ForeColor = Theme.Colors.SecondaryCharcoalBark;
            textBoxDescription.BackColor = Theme.Colors.BgCard;
            textBoxDescription.ForeColor = Theme.Colors.SecondaryCharcoalBark;
            okButton.BackColor = Theme.Colors.PrimaryForestGreen;
            okButton.ForeColor = Theme.Colors.BgCard;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderColor = Theme.Colors.PrimaryForestGreen;
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
                        return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion

        void AboutBox1_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void AboutBox1_Load(object sender, System.EventArgs e)
        {
            FontScaler.Apply(this);
            if (heroFont is not null)
            {
                labelProductName.Font = heroFont;
                tableLayoutPanel.PerformLayout();
                Rectangle workArea = Screen.GetWorkingArea(this);

                int requiredWidth = TextRenderer.MeasureText(labelProductName.Text, heroFont).Width
                    + labelProductName.Margin.Horizontal + tableLayoutPanel.Margin.Horizontal + Padding.Horizontal;
                if (requiredWidth > ClientSize.Width)
                    Width += Math.Min(requiredWidth - ClientSize.Width, workArea.Width - Width);

                int requiredHeight = tableLayoutPanel.PreferredSize.Height + Padding.Vertical;
                if (requiredHeight > ClientSize.Height)
                    Height += Math.Min(requiredHeight - ClientSize.Height, workArea.Height - Height);
            }
        }
    }
}
