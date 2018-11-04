using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.Simple;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class SpecialMethods
    {
#if __PC__
        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();
            stack.Push(aControl);
            while (stack.Any())
            {
                var nextControl = stack.Pop();
                foreach (Control childControl in nextControl.Controls)
                    stack.Push(childControl);
                yield return nextControl;
            }
        }

        public static void SetFonts(Form form)
        {
            foreach (Control theControl in GetAllControls(form))
                if (theControl.Font.Name.Equals(Properties.FontSettings.Default.SelectedFont.Name))
                    theControl.Font = Properties.FontSettings.Default.SelectedFont;
        }
#endif

        public static async Task<TrackingResult> TrackEventAsync(this SimpleTracker tracker, string category, string action, string label, long value = 1)
        {
#if __PC__
            string resolution = Screen.PrimaryScreen.Bounds.ToString()
#elif __MACOS__
            string resolution = NSScreen.MainScreen.Frame.ToString();
#endif
            var eventTrackingParameters = new EventTracking
            {
                ClientId = Properties.Settings.Default.GUID.ToString(),
                UserId = Properties.Settings.Default.GUID.ToString(),
                ApplicationName = "FTAnalyzer",
                ApplicationVersion = Analytics.AppVersion,
                Category = category,
                Action = action,
                Label = label,
                Value = value,
                ScreenName = category,
                ScreenResolution = resolution.Length > 11 ? resolution.Substring(9, resolution.Length - 10) : resolution,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster(),
                CustomDimension1 = Analytics.DeploymentType,
                CustomDimension2 = Analytics.OSVersion,
                GoogleAdWordsId = "201-455-7333",
                UserLanguage = CultureInfo.CurrentUICulture.EnglishName
            };
            return await tracker.TrackAsync(eventTrackingParameters);
        }

        public static async Task<TrackingResult> TrackScreenviewAsync(this SimpleTracker tracker, string screen)
        {
#if __PC__
            string resolution = Screen.PrimaryScreen.Bounds.ToString()
#elif __MACOS__
            string resolution = NSScreen.MainScreen.Frame.ToString();
#endif
            var screenViewTrackingParameters = new ScreenviewTracking
            {
                ClientId = Analytics.GUID.ToString(),
                UserId = Analytics.GUID.ToString(),

                ApplicationName = "FTAnalyzer",
                ApplicationVersion = Analytics.AppVersion,
                ScreenName = screen,
                ScreenResolution = resolution.Length > 11 ? resolution.Substring(9, resolution.Length - 10) : resolution,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster(),
                CustomDimension1 = Analytics.DeploymentType,
                CustomDimension2 = Analytics.OSVersion,
                GoogleAdWordsId = "201-455-7333",
                UserLanguage = CultureInfo.CurrentUICulture.EnglishName
            };
            return await tracker.TrackAsync(screenViewTrackingParameters);
        }
    }
}