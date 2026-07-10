using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using Microsoft.Win32;

namespace FTAnalyzer.Theme
{
    public enum AppThemeMode { SystemDefault, Light, Dark }

    // Resolves the ported web-app palette (Colors) to whichever of the light/dark variants is
    // currently active. Kept in FTAnalyzer.Windows rather than FTAnalyzer.Shared deliberately -
    // Colors.cs is documented as a plain-data class shared with the Blazor web app, which switches
    // its own dark mode client-side via CSS and has no need for this runtime toggle state.
    static class ActiveColors
    {
        public static event EventHandler? Changed;

        public static AppThemeMode Mode { get; private set; } = FontSettings.Default.ThemeMode;

        // Cached rather than re-read on every access (as SystemDefault's registry lookup
        // previously was): controls constructed moments apart during startup - grids color
        // themselves inside their own constructors, which run during MainForm's
        // InitializeComponent(), earlier than FormTheme.Apply() colors the rest of the chrome -
        // could observe different resolved values if that early registry read was ever
        // inconsistent, leaving grids dark-themed while the rest of the UI came out light in the
        // same, never-toggled session. Resolved once and kept stable until SetMode() explicitly
        // changes it (which already fires Changed for live re-theming). Mid-session OS theme
        // changes while running in SystemDefault mode still aren't picked up live - a documented,
        // accepted trade-off - but that was already true in practice since nothing hooks
        // SystemEvents.UserPreferenceChanged.
        public static bool IsDark { get; private set; } = ComputeIsDark(FontSettings.Default.ThemeMode);

        public static void SetMode(AppThemeMode mode)
        {
            if (Mode == mode)
                return;
            Mode = mode;
            IsDark = ComputeIsDark(mode);
            FontSettings.Default.ThemeMode = mode;
            UIHelpers.SafeSaveSettings(FontSettings.Default);
            Changed?.Invoke(null, EventArgs.Empty);
        }

        static bool ComputeIsDark(AppThemeMode mode) => mode switch
        {
            AppThemeMode.Dark => true,
            AppThemeMode.Light => false,
            _ => !SystemUsesLightTheme(),
        };

        static bool SystemUsesLightTheme()
        {
            try
            {
                using RegistryKey? key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                return key?.GetValue("AppsUseLightTheme") is not int value || value != 0;
            }
            catch (Exception)
            {
                return true; // assume light if the registry value is unreadable (older Windows, locked-down environment, etc.)
            }
        }

        public static Color Primary => IsDark ? Colors.DarkPrimary : Colors.PrimaryForestGreen;
        public static Color PrimaryPale => IsDark ? Colors.DarkPrimaryPale : Colors.PrimaryForestGreenPale;
        // Text/icon color for content sitting ON TOP of Primary (buttons, selected menu items,
        // grid headers). NOT the same as Card: dark mode's primary is a lightened green chosen
        // specifically so DARK text reads on it (matching the web app's --rz-on-primary), so this
        // flips to near-black in dark mode rather than staying white like a card surface would.
        public static Color OnPrimary => IsDark ? Colors.DarkBgMain : Colors.BgCard;
        public static Color Background => IsDark ? Colors.DarkBgMain : Colors.BgParchment;
        public static Color Card => IsDark ? Colors.DarkBgCard : Colors.BgCard;
        public static Color Text => IsDark ? Colors.DarkSecondaryText : Colors.SecondaryCharcoalBark;
        public static Color Border => IsDark ? Colors.DarkBorder : Colors.Border;
        public static Color AccentWarm => IsDark ? Colors.DarkAccentWarm : Colors.AccentWarmBronze;
        public static Color AccentAction => IsDark ? Colors.DarkAccentAction : Colors.AccentActionAmber;
        public static Color GoldTitle => IsDark ? Colors.DarkGoldLight : Colors.GoldDark;
        public static Color GenderMale => IsDark ? Colors.DarkGenderMale : Colors.GenderMale;
        public static Color GenderFemale => IsDark ? Colors.DarkGenderFemale : Colors.GenderFemale;
        public static Color Warning => IsDark ? Colors.DarkWarning : Colors.Warning;
        public static Color Info => IsDark ? Colors.DarkInfo : Colors.Info;
        public static Color Danger => IsDark ? Colors.DarkDanger : Colors.Danger;

        // For owner-drawn disabled-state text (ThemedCheckBox/ThemedRadioButton) where WinForms'
        // own hardcoded SystemColors.GrayText - tuned for a light background and unreadable
        // against dark mode's near-black one - isn't an option. Blended from this theme's own
        // Text/Background rather than a fixed value, so it stays legible-but-muted in both themes.
        public static Color DisabledText => Blend(Text, Background, 0.5);

        static Color Blend(Color from, Color to, double amount) => Color.FromArgb(
            (int)(from.R + (to.R - from.R) * amount),
            (int)(from.G + (to.G - from.G) * amount),
            (int)(from.B + (to.B - from.B) * amount));
    }
}
