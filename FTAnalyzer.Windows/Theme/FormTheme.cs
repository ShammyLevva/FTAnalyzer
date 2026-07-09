using FTAnalyzer.Utilities;

namespace FTAnalyzer.Theme
{
    // Applies the ported web-app palette (ActiveColors, which resolves to the light or dark
    // variant of Colors depending on the user's toggle) to a form/control tree. Conservative by
    // design: only recolors controls that are still at a "default" color - either the untouched
    // system color, or whichever theme variant we ourselves applied last time - so it doesn't
    // clobber colors set deliberately elsewhere (e.g. ColourBMD/ColourCensus per-cell data
    // coloring, or a warning label set to red), while still allowing re-applying this after a
    // light/dark toggle to actually take effect on a form that was already themed once.
    // DataGridView header/selection styling is intentionally left alone here - that goes through
    // the AdvancedDataGridView source repo (see UI modernisation plan, Phase 4) so every grid
    // picks it up consistently.
    static class FormTheme
    {
        public static void Apply(Control root)
        {
            if (root is Form form)
            {
                form.BackColor = ActiveColors.Background;
                NativeMethods.SetImmersiveDarkMode(form, ActiveColors.IsDark);
            }
            ApplyToChildren(root);
        }

        static void ApplyToChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case DataGridView:
                        continue; // themed at the source (AdvancedDataGridView), not per-instance
                    case GroupBox groupBox:
                        if (IsDefaultBackground(groupBox.BackColor))
                            groupBox.BackColor = ActiveColors.Background;
                        if (IsDefaultText(groupBox.ForeColor))
                            groupBox.ForeColor = ActiveColors.Text;
                        break;
                    case TabControl tabControl:
                        // The tab strip itself (HighlightTabControl) paints its own tabs via a
                        // paint-loop override, but the surrounding content-area border/chrome is
                        // still drawn by the OS visual-style handler and ignores our colors -
                        // opt out the same way as ProgressBar.
                        NativeMethods.DisableVisualStyles(tabControl);
                        break;
                    case TabPage tabPage:
                        // TabPage ignores BackColor while UseVisualStyleBackColor is on - it
                        // paints the OS visual-style (usually white) background instead.
                        tabPage.UseVisualStyleBackColor = false;
                        if (IsDefaultBackground(tabPage.BackColor))
                            tabPage.BackColor = ActiveColors.Background;
                        break;
                    case Panel panel:
                        if (IsDefaultBackground(panel.BackColor))
                            panel.BackColor = ActiveColors.Background;
                        break;
                    case Label label:
                        if (IsDefaultText(label.ForeColor))
                            label.ForeColor = ActiveColors.Text;
                        break;
                    case CheckBox checkBox:
                        if (IsDefaultText(checkBox.ForeColor))
                            checkBox.ForeColor = ActiveColors.Text;
                        break;
                    case RadioButton radioButton:
                        if (IsDefaultText(radioButton.ForeColor))
                            radioButton.ForeColor = ActiveColors.Text;
                        break;
                    case TreeView treeView:
                        if (IsDefaultWindow(treeView.BackColor))
                        {
                            treeView.BackColor = ActiveColors.Card;
                            treeView.ForeColor = ActiveColors.Text;
                        }
                        break;
                    case FTAnalyzer.Forms.Controls.ThemedProgressBar progressBar:
                        progressBar.BackColor = ActiveColors.Card;
                        progressBar.ForeColor = ActiveColors.Primary;
                        break;
                    case Button button:
                        if (IsDefaultBackground(button.BackColor) || IsPrimary(button.BackColor))
                        {
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = ActiveColors.Primary;
                            button.ForeColor = ActiveColors.OnPrimary;
                            button.FlatAppearance.BorderColor = ActiveColors.Primary;
                        }
                        break;
                    case TextBoxBase textBox:
                        if (IsDefaultWindow(textBox.BackColor))
                        {
                            // A ReadOnly RichTextBox/TextBox ignores BackColor changes made
                            // while ReadOnly is already true (long-standing RichEdit quirk) -
                            // toggle it off around the assignment so the color actually sticks.
                            bool wasReadOnly = textBox.ReadOnly;
                            if (wasReadOnly)
                                textBox.ReadOnly = false;
                            textBox.BackColor = ActiveColors.Card;
                            textBox.ForeColor = ActiveColors.Text;
                            if (wasReadOnly)
                                textBox.ReadOnly = true;
                        }
                        if (textBox.BorderStyle == BorderStyle.Fixed3D)
                            textBox.BorderStyle = BorderStyle.FixedSingle;
                        break;
                }

                if (control.HasChildren)
                    ApplyToChildren(control);
            }
        }

        // "Default" means either an untouched system color, or whichever background/text/window
        // shade WE applied under either theme last time - both are safe to overwrite; anything
        // else is a deliberate override that must be left alone.
        static bool IsDefaultBackground(Color color) =>
            color == SystemColors.Control || color == Colors.BgParchment || color == Colors.DarkBgMain;

        static bool IsDefaultText(Color color) =>
            color == SystemColors.ControlText || color == Colors.SecondaryCharcoalBark || color == Colors.DarkSecondaryText;

        static bool IsDefaultWindow(Color color) =>
            color == SystemColors.Window || color == Colors.BgCard || color == Colors.DarkBgCard;

        static bool IsPrimary(Color color) =>
            color == Colors.PrimaryForestGreen || color == Colors.DarkPrimary;
    }
}
