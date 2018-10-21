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
        static Analytics()
        {
            if (Settings.Default.GUID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                Settings.Default.GUID = Guid.NewGuid();
                Settings.Default.Save();
            }
        }

        public static async void CheckProgramUsage() // pre demise of Windows 7 add tracker to check how many machines still use old versions
        {
            try
            {
                OperatingSystem os = Environment.OSVersion;
                SimpleTrackerEnvironment ste = new SimpleTrackerEnvironment(os.Platform.ToString(), os.Version.ToString(), os.VersionString);
                SimpleTracker st = new SimpleTracker("UA-125850339-2", ste);
                await SpecialMethods.TrackEventAsync(st, "FTAnalyzer Startup", "LoadProgram", MainForm.VERSION).ConfigureAwait(false);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }


    }
}
