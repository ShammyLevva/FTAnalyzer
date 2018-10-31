using FTAnalyzer.Properties;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Simple;
using System;
using System.Deployment.Application;
using System.Threading.Tasks;

namespace FTAnalyzer.Utilities
{
    class Analytics
    {
        static readonly SimpleTrackerEnvironment trackerEnvironment;
        static readonly SimpleTracker tracker;
        static readonly AnalyticsSession analyticsSession;

        public const string MainFormAction = "Main Form Action", FactsFormAction = "Facts Form Action", CensusTabAction = "Census Tab Action",
                            ReportsAction = "Reports Action", LostCousinsAction = "Lost Cousins Action", GeocodingAction = "Geocoding Action",
                            ExportAction = "Export Action", MapsAction = "Maps Action", CensusSearchAction = "Census Search Action",
                            BMDSearchAction = "BMD Search Action";

        static public string AppVersion { get; }
        static public string OSVersion { get; }
        static public string DeploymentType { get; }

        static Analytics()
        {
            if (Settings.Default.GUID == "00000000-0000-0000-0000-000000000000")
            {
                Settings.Default.GUID = Guid.NewGuid().ToString();
                Settings.Default.Save();
            }
            OperatingSystem os = Environment.OSVersion;
            trackerEnvironment = new SimpleTrackerEnvironment(os.Platform.ToString(), os.Version.ToString(), os.VersionString);
            analyticsSession = new AnalyticsSession();
            tracker = new SimpleTracker("UA-125850339-2", analyticsSession, trackerEnvironment);
            AppVersion = MainForm.VERSION;
            OSVersion = os.Version.ToString();
            DeploymentType = ApplicationDeployment.IsNetworkDeployed ? "ClickOnce" : "Zip File";
        }

        public static async Task CheckProgramUsageAsync() // pre demise of Windows 7 add tracker to check how many machines still use old versions
        {
            try
            {
                await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "Load Program", AppVersion).ConfigureAwait(false);
             //   await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "Record OS Version", OSVersion).ConfigureAwait(false);
              //  await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "Deployment Type", DeploymentType).ConfigureAwait(false);
                await SpecialMethods.TrackScreenviewAsync(tracker, "FTAnalyzer Startup");
            }
            catch (Exception e)
                { Console.WriteLine(e.Message); }
        }

        public static async Task EndProgramAsync()
        {
            try
            {
                TimeSpan duration = DateTime.Now - Settings.Default.StartTime;
                await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Shutdown", "Usage Time", duration.ToString("c"));
            }
            catch (Exception e)
                { Console.WriteLine(e.Message); }
        }

        public static Task TrackAction(string category, string action) => TrackActionAsync(category, action, "default");
        public static async Task TrackActionAsync(string category, string action, string value)
        {
            try
            {
                await SpecialMethods.TrackEventAsync(tracker, category, action, value).ConfigureAwait(false);
                await SpecialMethods.TrackScreenviewAsync(tracker, category);
            }
            catch (Exception e)
                { Console.WriteLine(e.Message); }
        }
    }
}
