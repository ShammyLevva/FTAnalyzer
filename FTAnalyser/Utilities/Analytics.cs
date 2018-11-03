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
            OSVersion = SetWindowsVersion(os.Version.ToString());
            DeploymentType = ApplicationDeployment.IsNetworkDeployed ? "ClickOnce" : "Zip File";
        }

        public static async Task CheckProgramUsageAsync() // pre demise of Windows 7 add tracker to check how many machines still use old versions
        {
            try
            {
                await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "Load Program", AppVersion).ConfigureAwait(false);
                await SpecialMethods.TrackScreenviewAsync(tracker, "FTAnalyzer Startup").ConfigureAwait(false);
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
                await SpecialMethods.TrackScreenviewAsync(tracker, category).ConfigureAwait(false);
            }
            catch (Exception e)
                { Console.WriteLine(e.Message); }
        }

        static string SetWindowsVersion(string version)
        {
            if (version.StartsWith("6.1.7600")) return "Windows 7";
            if (version.StartsWith("6.1.7601")) return "Windows 7 SP1";
            if (version.StartsWith("6.2.9200")) return "Windows 8";
            if (version.StartsWith("6.3.9200")) return "Windows 8.1";
            if (version.StartsWith("6.3.9600")) return "Windows 8.1 Update 1";
            if (version.StartsWith("10.0.10240")) return "Windows 10";
            if (version.StartsWith("10.0.10586")) return "Windows 10 (1511)";
            if (version.StartsWith("10.0.14393")) return "Windows 10 (1607)";
            if (version.StartsWith("10.0.15063")) return "Windows 10 (1703)";
            if (version.StartsWith("10.0.16299")) return "Windows 10 (1709)";
            if (version.StartsWith("10.0.17134")) return "Windows 10 (1803)";
            if (version.StartsWith("10.0.17763")) return "Windows 10 (1809)";
            return version;
        }
    }
}
