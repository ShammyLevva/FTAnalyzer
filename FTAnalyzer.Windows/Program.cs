using FTAnalyzer.Exports;
using FTAnalyzer.Mapping;
using SharpMap;
using System.Runtime.InteropServices;

namespace FTAnalyzer.Windows
{
    internal static partial class Program
    {
        public static readonly HttpClient Client = new();
        public static readonly LostCousinsClient LCClient = new();
        public static readonly GoogleClient GoogleClient = new();

        /// <summary>
        /// Enable high DPI
        /// </summary>
        static readonly bool HighDPIEnabled = true;

        /// <summary>
        /// Load for high DPI
        /// </summary>
        /// <returns></returns>
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetProcessDPIAware();

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
                SetProcessDPIAware();
            SharpMapUtility.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm formMain = new();
            if (HighDPIEnabled)
                formMain.AutoScaleMode = AutoScaleMode.Dpi;
            Application.Run(formMain);
        }
    }
}