using FTAnalyzer.Properties;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.Simple;
using System;
using System.Collections.Generic;

namespace FTAnalyzer.Utilities
{
    class Analytics 
    {
        static readonly SimpleTrackerEnvironment trackerEnvironment;
        static readonly SimpleTracker tracker;

        static Analytics()
        {
            if (Settings.Default.GUID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                Settings.Default.GUID = Guid.NewGuid();
                Settings.Default.Save();
            }
            OperatingSystem os = Environment.OSVersion;
            trackerEnvironment = new SimpleTrackerEnvironment(os.Platform.ToString(), os.Version.ToString(), os.VersionString);
            tracker = new SimpleTracker("UA-125850339-2", trackerEnvironment);
        }

        public static async void CheckProgramUsage() // pre demise of Windows 7 add tracker to check how many machines still use old versions
        {
            try
            {
                await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "LoadProgram", MainForm.VERSION).ConfigureAwait(false);
                await SpecialMethods.TrackEventAsync(tracker, "FTAnalyzer Startup", "RecordOSVersion", trackerEnvironment.OsVersion).ConfigureAwait(false);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public static async void MainFormAction(string action, string value)
        {
            try
            {
                await SpecialMethods.TrackEventAsync(tracker, "Main Form Action", action, value).ConfigureAwait(false);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
