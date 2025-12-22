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

        public enum Direction { LEFT, RIGHT, ABOVE, BELOW };
        public static void MoveControl(Control ctrl, Control relativeTo, Direction direction)
        {
            if (direction == Direction.LEFT) // Control to be to the left of relativeTo
            {
                int newLeft = relativeTo.Left - relativeTo.Margin.Left - ctrl.Width;
                if (newLeft > 0)
                    ctrl.Left = newLeft;
                ctrl.Top = relativeTo.Top;
            }
            else if (direction == Direction.RIGHT) // Control to be to the left of relativeTo
            {
                int newLeft = relativeTo.Right + relativeTo.Margin.Right;
                ctrl.Left = newLeft;
                ctrl.Top = relativeTo.Top;
            }
            else if (direction == Direction.ABOVE) // Control to be above relativeTo
            {
                int newTop = relativeTo.Top - relativeTo.Margin.Top;
                if (newTop > 0)
                    ctrl.Top = newTop;
                ctrl.Left = relativeTo.Left;
            }
            else if (direction == Direction.BELOW) // Control to be below relativeTo
            {
                int newTop = relativeTo.Bottom + relativeTo.Margin.Bottom;
                ctrl.Top = newTop;
                ctrl.Left = relativeTo.Left;
            }
        }
    }
}
