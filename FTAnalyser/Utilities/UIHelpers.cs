using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class UIHelpers
    {
        public static bool ShowYesNo(string message)
        {
            DialogResult result = MessageBox.Show(message, "Continue Loading?", MessageBoxButtons.YesNo);
            return result == DialogResult.Yes;
         }
    }
}
