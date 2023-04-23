using FTAnalyzer.Utilities;
using FTAnalyzer.Exports;
using SharpMap;
using FTAnalyzer.Mapping;

namespace FTAnalyzer.Windows
{
    internal static class Program
    {
        public static HttpClient Client = new();
        public static LostCousinsClient LCClient = new();
        public static GoogleClient GoogleClient = new();
        
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}