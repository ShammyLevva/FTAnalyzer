#pragma warning disable CA2000 // WinForms controls added to Controls collection are owned and disposed by the parent form
using System.Text;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public sealed class StartupErrorForm : Form
    {
        readonly Exception _ex;
        readonly string _crashReport;

        public StartupErrorForm(Exception ex, string version)
        {
            _ex = ex;
            _crashReport = BuildCrashReport(ex, version);

            Text = "FTAnalyzer — Startup Error";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(560, 172);
            Icon = SystemIcons.Error;

            PictureBox iconBox = new()
            {
                Image = SystemIcons.Error.ToBitmap(),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(36, 36),
                Location = new Point(12, 16)
            };

            Label lblHeader = new()
            {
                Text = "FTAnalyzer encountered a problem whilst starting up.",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(56, 16),
                Size = new Size(492, 20),
                AutoSize = false
            };

            Label lblHint = new()
            {
                Text = "Click 'View Details' to see the technical information, or 'Report Issue on GitHub' to help us fix it.",
                Location = new Point(56, 40),
                Size = new Size(492, 72),
                AutoSize = false
            };

            Button btnGitHub = new()
            {
                Text = "Report Issue on GitHub",
                Size = new Size(190, 30),
                Location = new Point(12, 130)
            };
            btnGitHub.Click += (_, _) => OpenGitHubIssue();

            Button btnDetails = new()
            {
                Text = "View Details",
                Size = new Size(105, 30),
                Location = new Point(210, 130)
            };
            btnDetails.Click += (_, _) => new CrashDetailsForm(_crashReport).ShowDialog(this);

            Button btnClose = new()
            {
                Text = "Close",
                Size = new Size(80, 30),
                Location = new Point(468, 130),
                DialogResult = DialogResult.Cancel
            };
            CancelButton = btnClose;

            Controls.AddRange([iconBox, lblHeader, lblHint, btnGitHub, btnDetails, btnClose]);
        }

        void OpenGitHubIssue()
        {
            string bodyText = _crashReport.Length > 5000
                ? _crashReport[..5000] + "\n\n...(truncated — please attach full log if available)"
                : _crashReport;

            string rawTitle = $"Startup crash: {_ex.GetType().Name}: {_ex.Message}";
            string title = Uri.EscapeDataString(rawTitle.Length > 100 ? rawTitle[..100] : rawTitle);
            string encodedBody = Uri.EscapeDataString(bodyText);
            SpecialMethods.VisitWebsite($"https://github.com/ShammyLevva/FTAnalyzer/issues/new?labels=bug&title={title}&body={encodedBody}");
        }

        internal static string BuildCrashReport(Exception ex, string version)
        {
            StringBuilder sb = new();
            sb.AppendLine("## Startup Crash Report");
            sb.AppendLine();
            sb.AppendLine($"**FTAnalyzer Version:** {version}");
            sb.AppendLine($"**OS:** {Environment.OSVersion}");
            sb.AppendLine($"**.NET Runtime:** {Environment.Version}");
            sb.AppendLine();
            sb.AppendLine("## Exception");
            sb.AppendLine($"**Type:** `{ex.GetType().FullName}`");
            sb.AppendLine($"**Message:** {ex.Message}");
            sb.AppendLine();
            sb.AppendLine("### Stack Trace");
            sb.AppendLine("```");
            sb.AppendLine(ex.StackTrace ?? "(no stack trace available)");
            sb.AppendLine("```");
            if (ex.InnerException is not null)
            {
                sb.AppendLine();
                sb.AppendLine("### Inner Exception");
                sb.AppendLine($"**Type:** `{ex.InnerException.GetType().FullName}`");
                sb.AppendLine($"**Message:** {ex.InnerException.Message}");
                sb.AppendLine();
                sb.AppendLine("```");
                sb.AppendLine(ex.InnerException.StackTrace ?? "(no stack trace available)");
                sb.AppendLine("```");
            }
            return sb.ToString();
        }
    }
}
