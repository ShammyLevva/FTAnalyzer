using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.Simple;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class SpecialMethods
    {
        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();

            stack.Push(aControl);

            while (stack.Any())
            {
                var nextControl = stack.Pop();

                foreach (Control childControl in nextControl.Controls)
                {
                    stack.Push(childControl);
                }

                yield return nextControl;
            }
        }

        public static void SetFonts(Form form)
        {
            foreach (Control theControl in GetAllControls(form))
                if (theControl.Font.Name.Equals(Properties.FontSettings.Default.SelectedFont.Name))
                    theControl.Font = Properties.FontSettings.Default.SelectedFont;
        }

        public static async Task<TrackingResult> TrackEventAsync(this SimpleTracker tracker, string category, string action, string label, long value = 1)
        {
            var eventTrackingParameters = new EventTracking
            {
                ClientId = Properties.Settings.Default.GUID.ToString(),
                UserId = Properties.Settings.Default.GUID.ToString(),
                
                ApplicationName = "FTAnalyzer",
                ApplicationVersion = MainForm.VERSION,
                Category = category,
                Action = action,
                Label = label,
                Value = value,
                ScreenName = category,
                ScreenResolution = Screen.PrimaryScreen.Bounds.ToString(),
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };
            return await tracker.TrackAsync(eventTrackingParameters);
        }

        public static async Task<TrackingResult> TrackScreenviewAsync(this SimpleTracker tracker, string screen)
        {
            var screenViewTrackingParameters = new ScreenviewTracking
            {
                ClientId = Properties.Settings.Default.GUID.ToString(),
                UserId = Properties.Settings.Default.GUID.ToString(),

                ApplicationName = "FTAnalyzer",
                ApplicationVersion = MainForm.VERSION,
                ScreenName = screen,
                ScreenResolution = Screen.PrimaryScreen.Bounds.ToString(),
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };
            return await tracker.TrackAsync(screenViewTrackingParameters);
        }
    }
}