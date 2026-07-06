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

        // Evaluated fresh each time rather than cached, so SystemDefault picks up the current
        // Windows setting whenever the theme is (re-)applied, without needing to hook
        // SystemEvents.UserPreferenceChanged for a fully live system-theme-change response.
        public static bool IsDark => Mode switch
        {
            AppThemeMode.Dark => true,
            AppThemeMode.Light => false,
            _ => !SystemUsesLightTheme(),
        };

        public static void SetMode(AppThemeMode mode)
        {
            if (Mode == mode)
                return;
            Mode = mode;
            FontSettings.Default.ThemeMode = mode;
            UIHelpers.SafeSaveSettings(FontSettings.Default);
            Changed?.Invoke(null, EventArgs.Empty);
        }

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
    }
}
