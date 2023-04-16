using FTAnalyzer.Utilities;
using SharpMap;
using System.Net;

namespace FTAnalyzer.Windows
{
    internal static class Program
    {

        public static HttpClient Client { get; private set; }
        public static CookieContainer Cookies { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (Environment.OSVersion.Version.Major >= 10) 
                NativeMethods.SetProcessDpiAwarenessContext((int)NativeMethods.DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            SharpMapUtility.Configure();
            SetupHttpClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void SetupHttpClient()
        {
            Cookies = new();
            HttpClientHandler handler = new()
            {
                CookieContainer = Cookies
            };
            Client = new(handler);
        }
    }
}