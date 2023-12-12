using FTAnalyzer.Properties;

namespace FTAnalyzer.Utilities
{
    public static class ErrorHandler
    {
        public static void Show(string errorNum, Exception ex) => Show(errorNum, ex, MessageBoxIcon.Error);

        public static void Show(string errorNum, Exception ex, MessageBoxIcon icon)
        {
            string message = ErrorMessages.ResourceManager.GetString(errorNum) ?? string.Empty;
            _ = int.TryParse(errorNum[4..], out _);
            UIHelpers.ShowMessage($"{message}\n{ex.Message}", $"Error: {errorNum}", MessageBoxButtons.OK, icon);
        }
    }
}
