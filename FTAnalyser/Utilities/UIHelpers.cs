using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class UIHelpers
    {
        public static int ShowYesNo(string message)
        {
            DialogResult result = MessageBox.Show(message, "Continue Loading?", MessageBoxButtons.YesNo);
            return (int)result;
         }

        public static int ShowMessage(string message) => ShowMessage(message, "FTAnalyzer");
        public static int ShowMessage(string message, string title)
        {
            return (int)MessageBox.Show(message, title);
        }

        public static int Yes => (int)DialogResult.Yes;
        public static int No => (int)DialogResult.No;
        public static int Cancel => (int)DialogResult.Cancel;
    }
}
