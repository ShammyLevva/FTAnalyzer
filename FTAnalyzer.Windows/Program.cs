using FTAnalyzer.Exports;
using FTAnalyzer.Mapping;
using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using Microsoft.Extensions.Configuration;
using SharpMap;

namespace FTAnalyzer
{
    internal static partial class Program
    {
        public static readonly HttpClient Client = new();
        public static readonly LostCousinsClient LCClient = new();
        public static readonly GoogleClient GoogleClient = new();

        [STAThread]
        static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<MainForm>()
                .Build();
            Analytics.Initialize(config["Analytics:ApiSecret"] ?? AnalyticsSecrets.GA4ApiSecret);

            ApplicationConfiguration.Initialize();
            SharpMapUtility.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm formMain = new();
            Application.Run(formMain);
        }
    }
}