namespace FTAnalyzer.UserControls
{
    public static class InputBox
    {
        public static DialogResult Show(string title, string promptText, ref string value)
        {
            Form form = new();
            Label label = new();
            TextBox textBox = new();
            Button buttonOk = new();
            Button buttonCancel = new();
            float scale = Properties.FontSettings.Default.FontSize / 8f;

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds((int)(9f * scale), (int)(20f * scale), (int)(372f * scale), (int)(13f * scale));
            textBox.SetBounds((int)(12f * scale), (int)(48f * scale), (int)(372f * scale), (int)(32f * scale));
            buttonOk.SetBounds((int)(228f * scale), (int)(72f * scale), (int)(75f * scale), (int)(32f * scale));
            buttonCancel.SetBounds((int)(309f * scale), (int)(72f * scale), (int)(75f * scale), (int)(32f * scale));

            label.AutoSize = true;
            textBox.Anchor |= AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.Size = new Size((int)(416f * scale), (int)(180f * scale));
            form.Controls.AddRange([label, textBox, buttonOk, buttonCancel]);
            form.ClientSize = new Size(Math.Max((int)(300f * scale), label.Right + (int)(30f * scale)), (int)(160f * scale));
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }

    public class InputBoxResult
    {
        public DialogResult ReturnCode { get; set; }
        public string Text { get; set; }
    }
}
