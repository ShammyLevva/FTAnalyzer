using FTAnalyzer.Properties;

namespace FTAnalyzer.Utilities
{
    public static class ErrorHandler
    {
        public static void Show(string errorNum, Exception ex)
        {
            Show(errorNum, ex, MessageBoxIcon.Error);
        }

        public static void Show(string errorNum, Exception ex, MessageBoxIcon icon)
        {
            string message = ErrorMessages.ResourceManager.GetString(errorNum);
            int.TryParse(errorNum.Substring(4), out _);
            MessageBox.Show(message + "\n" + ex.Message,
                        "Error: " + errorNum,
                        MessageBoxButtons.OK,
                        icon);
        }
    }
}
