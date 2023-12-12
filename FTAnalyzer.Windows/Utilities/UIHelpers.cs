using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class UIHelpers
    {
        public static int ShowYesNo(string message, string title)
        {
            DialogResult result = MessageBox.Show(Form.ActiveForm, message, title, MessageBoxButtons.YesNo);
            return (int)result;
        }

        public static int ShowMessage(string message) => ShowMessage(message, "FTAnalyzer");
        public static int ShowMessage(string message, string title) => (int)MessageBox.Show(Form.ActiveForm, message, title);

        public static int ShowMessage(Form form, string message) => (int)MessageBox.Show(form, message, string.Empty);
        public static int ShowMessage(Form form, string message, string title) => (int)MessageBox.Show(form, message, title);

        public static DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) =>
            MessageBox.Show(Form.ActiveForm, message, title, buttons, icon);

        public static int Yes => (int)DialogResult.Yes;
        public static int No => (int)DialogResult.No;
        public static int Cancel => (int)DialogResult.Cancel;
    }
}
