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
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            SharpMapUtility.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm formMain = new();
            Application.Run(formMain);
        }
    }
}