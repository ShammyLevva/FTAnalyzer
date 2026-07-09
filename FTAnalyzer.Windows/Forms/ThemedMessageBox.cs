using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    // MessageBox.Show is a fully native Windows dialog - not a WinForms Form, so FormTheme has no
    // hook into it, and it always renders in the OS's light chrome regardless of the app's theme.
    // This is a themed drop-in replacement, built as an ordinary Form so it picks up
    // FontScaler/FormTheme exactly like every other dialog - UIHelpers uses this instead of
    // MessageBox.Show directly.
    public sealed class ThemedMessageBox : Form
    {
        ThemedMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Text = title;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;

            Icon? sysIcon = icon switch
            {
                MessageBoxIcon.Error => SystemIcons.Error,
                MessageBoxIcon.Warning => SystemIcons.Warning,
                MessageBoxIcon.Question => SystemIcons.Question,
                MessageBoxIcon.Information => SystemIcons.Information,
                _ => null,
            };
            if (sysIcon is not null)
                base.Icon = sysIcon;

            int textLeft = sysIcon is null ? 20 : 68;
            if (sysIcon is not null)
            {
                Controls.Add(new PictureBox
                {
                    Image = sysIcon.ToBitmap(),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(32, 32),
                    Location = new Point(20, 20)
                });
            }

            Label lblMessage = new()
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size(360, 0),
                Location = new Point(textLeft, 20)
            };
            Controls.Add(lblMessage);

            int contentBottom = Math.Max(lblMessage.Bottom, 20 + 32) + 24;

            (string Text, DialogResult Result)[] buttonSpecs = buttons switch
            {
                MessageBoxButtons.OKCancel => [("OK", DialogResult.OK), ("Cancel", DialogResult.Cancel)],
                MessageBoxButtons.YesNo => [("Yes", DialogResult.Yes), ("No", DialogResult.No)],
                MessageBoxButtons.YesNoCancel => [("Yes", DialogResult.Yes), ("No", DialogResult.No), ("Cancel", DialogResult.Cancel)],
                MessageBoxButtons.RetryCancel => [("Retry", DialogResult.Retry), ("Cancel", DialogResult.Cancel)],
                MessageBoxButtons.AbortRetryIgnore => [("Abort", DialogResult.Abort), ("Retry", DialogResult.Retry), ("Ignore", DialogResult.Ignore)],
                _ => [("OK", DialogResult.OK)],
            };

            const int buttonWidth = 90;
            const int buttonHeight = 30;
            const int buttonSpacing = 10;
            int formWidth = Math.Max(textLeft + lblMessage.Width + 20, buttonSpecs.Length * (buttonWidth + buttonSpacing) + 10);

            Button[] createdButtons = new Button[buttonSpecs.Length];
            int buttonRight = formWidth - 20;
            for (int i = buttonSpecs.Length - 1; i >= 0; i--)
            {
                Button button = new()
                {
                    Text = buttonSpecs[i].Text,
                    DialogResult = buttonSpecs[i].Result,
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(buttonRight - buttonWidth, contentBottom)
                };
                Controls.Add(button);
                createdButtons[i] = button;
                buttonRight -= buttonWidth + buttonSpacing;
            }

            AcceptButton = createdButtons[0];
            CancelButton = Array.Find(createdButtons, b => b.DialogResult == DialogResult.Cancel)
                ?? Array.Find(createdButtons, b => b.DialogResult == DialogResult.No);

            ClientSize = new Size(formWidth, contentBottom + buttonHeight + 20);

            FontScaler.Apply(this);
            Theme.FormTheme.Apply(this);
        }

        public static DialogResult Show(IWin32Window? owner, string message, string title,
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            using ThemedMessageBox box = new(message, title, buttons, icon);
            return owner is Form ownerForm ? box.ShowDialog(ownerForm) : box.ShowDialog();
        }
    }
}
