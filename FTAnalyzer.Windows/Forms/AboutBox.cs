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
            Theme.FormTheme.Apply(this);
            ApplyTheme();
        }

        void ApplyTheme()
        {
            BackColor = Theme.ActiveColors.Background;
            tableLayoutPanel.BackColor = Theme.ActiveColors.Background;
            labelProductName.ForeColor = Theme.ActiveColors.GoldTitle;
            labelVersion.ForeColor = Theme.ActiveColors.Text;
            labelCopyright.ForeColor = Theme.ActiveColors.Text;
            textBoxDescription.BackColor = Theme.ActiveColors.Card;
            textBoxDescription.ForeColor = Theme.ActiveColors.Text;
            okButton.BackColor = Theme.ActiveColors.Primary;
            okButton.ForeColor = Theme.ActiveColors.Card;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderColor = Theme.ActiveColors.Primary;
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
