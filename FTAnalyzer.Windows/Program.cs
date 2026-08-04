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
            // Must run before anything reads a Settings.Default/GeneralSettings.Default/etc.
            // property - even indirectly, e.g. ConfigureGridTheme() below touches Theme.ActiveColors,
            // whose static field initializers read FontSettings.Default.ThemeMode.
            SettingsBootstrap.Initialize();

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
        // than baking FTAnalyzer's branding into the package itself. Callable again after a
        // light/dark toggle (see Theme.ActiveColors), not just once at startup.
        internal static void ConfigureGridTheme()
        {
            // Normal state uses a lighter green than the header background so the button
            // reads as part of the header row rather than a patch stuck on top of it; hover
            // flips to the gold accent for a clear, contrasting "you're here" signal.
            // (PrimaryForestGreenPale, one step lighter still, is reserved for row selection
            // instead - using the same tone for both made the button and a selected row bleed
            // into each other visually.) The grid header itself is always a dark green surface
            // regardless of the app's overall light/dark mode, so these stay fixed to the light
            // palette's tones rather than switching with ActiveColors.
            Zuby.ADGV.GridTheme.HeaderButtonBorder = Theme.Colors.PrimaryForestGreenLight;
            Zuby.ADGV.GridTheme.HeaderButtonNormalFill = Theme.Colors.PrimaryForestGreenLight;
            Zuby.ADGV.GridTheme.HeaderButtonHoverFill = Theme.Colors.AccentActionAmber;
            // The filter/sort glyph artwork is dark and reads poorly on a dark green header -
            // recolor it solid gold instead (DarkGoldLight was designed for dark surfaces).
            Zuby.ADGV.GridTheme.HeaderIconColor = Theme.Colors.DarkGoldLight;
            // Gap between the icon and its button edge - tweak here if the icon ever looks
            // too cramped/too dominant; no package change needed. Now that the button itself
            // is capped at HeaderButtonMaxSize, this only needs to be a small breathing-room
            // gap, not a large offset to compensate for an oversized button.
            Zuby.ADGV.GridTheme.HeaderIconPadding = 3;
            // The package's own default (20) was clamping the button below what its own
            // font-based sizing calculates (~1.3x the header font's line height, roughly
            // 23-26px for our header font) - raise the ceiling so that calculation isn't
            // artificially suppressed.
            Zuby.ADGV.GridTheme.HeaderButtonMaxSize = 32;

            // The filter dialog/dropdown chrome, on the other hand, is regular UI surface and
            // should follow the app's light/dark toggle like everything else.
            Zuby.ADGV.GridTheme.DropDownBackground = Theme.ActiveColors.Card;
            Zuby.ADGV.GridTheme.DropDownForeground = Theme.ActiveColors.Text;
            // Drives the filter checklist's own native scrollbar theme (SetWindowTheme) - a
            // separate flag rather than deriving it from DropDownBackground because that's an
            // exact color check, not a light/dark classification.
            Zuby.ADGV.GridTheme.DarkMode = Theme.ActiveColors.IsDark;

            Zuby.ADGV.GridTheme.ButtonFlatStyle = FlatStyle.Flat;
            Zuby.ADGV.GridTheme.ButtonUseVisualStyleBackColor = false;

            Zuby.ADGV.GridTheme.PrimaryButtonBack = Theme.ActiveColors.Primary;
            Zuby.ADGV.GridTheme.PrimaryButtonFore = Theme.ActiveColors.OnPrimary;
            Zuby.ADGV.GridTheme.PrimaryButtonBorder = Theme.ActiveColors.Primary;

            Zuby.ADGV.GridTheme.SecondaryButtonBack = Theme.ActiveColors.Card;
            Zuby.ADGV.GridTheme.SecondaryButtonFore = Theme.ActiveColors.Text;
            Zuby.ADGV.GridTheme.SecondaryButtonBorder = Theme.ActiveColors.AccentWarm;

            Zuby.ADGV.GridTheme.DialogBackground = Theme.ActiveColors.Background;
        }
    }
}