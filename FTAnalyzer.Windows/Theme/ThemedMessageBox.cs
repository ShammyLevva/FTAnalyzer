using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Theme
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

            // Base pixel sizes below are tuned for FontScale level 1 (8.25pt, roughly what a
            // native MessageBox uses). This app's Font Settings option can scale the UI font up
            // to 14pt (level 4) - fixed pixel widths sized for the smallest font wrapped that much
            // larger text onto far more lines than a native dialog would, looking cramped even
            // though the box itself wasn't actually narrower in raw pixels. Scale every dimension
            // by how much bigger the active level's font is than that baseline.
            float scale = FontScale.ForLevel(FontSettings.Default.FontNumber).FontSize / FontScale.ForLevel(1).FontSize;

            Label lblMessage = new()
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size((int)(640 * scale), 0),
                Location = new Point(textLeft, 20)
            };
            Controls.Add(lblMessage);

            // Scale fonts before measuring/positioning anything below - FontScaler resizes
            // lblMessage's Font (AutoSize labels recompute their size synchronously on a Font
            // change), so measuring its Width/Bottom beforehand would use stale pre-scale
            // metrics and leave every button positioned for a size that no longer matches what's
            // rendered. Buttons added afterwards don't need their own FontScaler pass - they
            // inherit this Form's own (now-scaled) Font automatically since they never set one.
            FontScaler.Apply(this);

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

            // Sized from the actual (already font-scaled) button text rather than a fixed 90x30 -
            // a fixed height clipped button text once FontScaler enlarged the font past what 30px
            // of height could fit.
            const int buttonSpacing = 12;
            int buttonWidth = 110;
            int buttonHeight = 34;
            foreach ((string text, _) in buttonSpecs)
            {
                Size measured = TextRenderer.MeasureText(text, Font);
                buttonWidth = Math.Max(buttonWidth, measured.Width + 40);
                buttonHeight = Math.Max(buttonHeight, measured.Height + 20);
            }
            // A native MessageBox never shrinks below roughly this width/height even for a short
            // one-line message - match that floor (scaled the same way as the wrap width above)
            // rather than letting a short message produce a cramped-looking little box.
            int minimumFormWidth = (int)(560 * scale);
            int minimumFormHeight = (int)(200 * scale);
            int formWidth = Math.Max(minimumFormWidth,
                Math.Max(textLeft + lblMessage.Width + 20, buttonSpecs.Length * (buttonWidth + buttonSpacing) + 10));
            int formHeight = Math.Max(minimumFormHeight, contentBottom + buttonHeight + 20);
            // Bottom-anchor the buttons against the final height (not contentBottom directly) -
            // when the minimum-height floor kicks in, contentBottom no longer reflects where the
            // bottom of the dialog actually ends up.
            int buttonTop = formHeight - buttonHeight - 20;

            Button[] createdButtons = new Button[buttonSpecs.Length];
            int buttonRight = formWidth - 20;
            for (int i = buttonSpecs.Length - 1; i >= 0; i--)
            {
                Button button = new()
                {
                    Text = buttonSpecs[i].Text,
                    DialogResult = buttonSpecs[i].Result,
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(buttonRight - buttonWidth, buttonTop)
                };
                Controls.Add(button);
                createdButtons[i] = button;
                buttonRight -= buttonWidth + buttonSpacing;
            }

            AcceptButton = createdButtons[0];
            CancelButton = Array.Find(createdButtons, b => b.DialogResult == DialogResult.Cancel)
                ?? Array.Find(createdButtons, b => b.DialogResult == DialogResult.No);

            ClientSize = new Size(formWidth, formHeight);

            FormTheme.Apply(this);
        }

        public static DialogResult Show(IWin32Window? owner, string message, string title,
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            using ThemedMessageBox box = new(message, title, buttons, icon);
            return owner is Form ownerForm ? box.ShowDialog(ownerForm) : box.ShowDialog();
        }
    }
}
