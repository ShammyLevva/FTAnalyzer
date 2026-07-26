using FTAnalyzer.Properties;

namespace FTAnalyzer.Utilities
{
    internal static class DebugLogger
    {
        static readonly object writeLock = new();

        public static string LogFolder { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Family Tree Analyzer");

        public static string LogFilePath { get; } = Path.Combine(LogFolder, "FTAnalyzer-debug.log");

        public static bool Enabled => GeneralSettings.Default.EnableDebugLogging;

        public static void Log(string message)
        {
            if (!Enabled)
                return;
            try
            {
                lock (writeLock)
                {
                    Directory.CreateDirectory(LogFolder);
                    File.AppendAllText(LogFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {message}{Environment.NewLine}");
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}
