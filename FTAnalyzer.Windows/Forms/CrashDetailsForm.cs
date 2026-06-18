#pragma warning disable CA2000 // WinForms controls added to Controls collection are owned and disposed by the parent form
namespace FTAnalyzer.Forms
{
    public sealed class CrashDetailsForm : Form
    {
        public CrashDetailsForm(string crashReport)
        {
            Text = "Crash Report Details";
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(500, 400);
            ClientSize = new Size(680, 520);
            Icon = SystemIcons.Error;

            Panel buttonPanel = new()
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnCopy = new()
            {
                Text = "Copy to Clipboard",
                Size = new Size(150, 30),
                Location = new Point(12, 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnCopy.Click += async (_, _) =>
            {
                Clipboard.SetText(crashReport);
                btnCopy.Text = "Copied!";
                await Task.Delay(1500);
                btnCopy.Text = "Copy to Clipboard";
            };

            Button btnClose = new()
            {
                Text = "Close",
                Size = new Size(80, 30),
                Location = new Point(588, 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                DialogResult = DialogResult.Cancel
            };
            CancelButton = btnClose;

            buttonPanel.Controls.AddRange([btnCopy, btnClose]);

            RichTextBox rtb = new()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = SystemColors.Window,
                Text = crashReport,
                Font = new Font("Consolas", 9f),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };

            Controls.AddRange([rtb, buttonPanel]);
        }
    }
}
