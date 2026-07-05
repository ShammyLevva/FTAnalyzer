#pragma warning disable CA2000 // Modeless WinForms forms are owned by the Windows message loop; lifetime is managed externally
using FTAnalyzer.Exports;
using FTAnalyzer.Mapping;
using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using Microsoft.Extensions.Configuration;
using SharpMap;

namespace FTAnalyzer
{
    internal static partial class Program
    {
        public static readonly HttpClient Client = new();
        public static readonly LostCousinsClient LCClient = new();
        public static readonly GoogleClient GoogleClient = new();

        [STAThread]
        static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<MainForm>()
                .Build();
            Analytics.Initialize(config["Analytics:ApiSecret"] ?? AnalyticsSecrets.GA4ApiSecret);

            ApplicationConfiguration.Initialize();
            SharpMapUtility.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureGridTheme();

            MainForm formMain = new();
            Application.Run(formMain);
        }

        // Zuby.ADGV (AdvancedDataGridView) is a general-purpose package, so it defaults to an
        // unthemed look - point its overridable palette at our own Theme.Colors here rather
        // than baking FTAnalyzer's branding into the package itself.
        static void ConfigureGridTheme()
        {
            Zuby.ADGV.GridTheme.HeaderButtonBorder = Theme.Colors.AccentWarmBronze;
            Zuby.ADGV.GridTheme.HeaderButtonHoverFill = Theme.Colors.BgParchment;
            Zuby.ADGV.GridTheme.HeaderButtonNormalFill = Theme.Colors.BgCard;

            Zuby.ADGV.GridTheme.DropDownBackground = Theme.Colors.BgCard;

            Zuby.ADGV.GridTheme.ButtonFlatStyle = FlatStyle.Flat;
            Zuby.ADGV.GridTheme.ButtonUseVisualStyleBackColor = false;

            Zuby.ADGV.GridTheme.PrimaryButtonBack = Theme.Colors.PrimaryForestGreen;
            Zuby.ADGV.GridTheme.PrimaryButtonFore = Theme.Colors.BgCard;
            Zuby.ADGV.GridTheme.PrimaryButtonBorder = Theme.Colors.PrimaryForestGreen;

            Zuby.ADGV.GridTheme.SecondaryButtonBack = Theme.Colors.BgCard;
            Zuby.ADGV.GridTheme.SecondaryButtonFore = Theme.Colors.SecondaryCharcoalBark;
            Zuby.ADGV.GridTheme.SecondaryButtonBorder = Theme.Colors.AccentWarmBronze;

            Zuby.ADGV.GridTheme.DialogBackground = Theme.Colors.BgParchment;
        }
    }
}