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
            // Normal state uses a lighter green than the header background so the button
            // reads as part of the header row rather than a patch stuck on top of it; hover
            // flips to the gold accent for a clear, contrasting "you're here" signal.
            // (PrimaryForestGreenPale, one step lighter still, is reserved for row selection
            // instead - using the same tone for both made the button and a selected row bleed
            // into each other visually.)
            Zuby.ADGV.GridTheme.HeaderButtonBorder = Theme.Colors.PrimaryForestGreenLight;
            Zuby.ADGV.GridTheme.HeaderButtonNormalFill = Theme.Colors.PrimaryForestGreenLight;
            Zuby.ADGV.GridTheme.HeaderButtonHoverFill = Theme.Colors.AccentActionAmber;
            // The filter/sort glyph artwork is dark and reads poorly on a dark green header -
            // recolor it solid gold instead (DarkGoldLight was designed for dark surfaces).
            Zuby.ADGV.GridTheme.HeaderIconColor = Theme.Colors.DarkGoldLight;
            // Gap between the icon and its button edge - tweak here if the icon ever looks
            // too cramped/too dominant; no package change needed.
            Zuby.ADGV.GridTheme.HeaderIconPadding = 6;

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