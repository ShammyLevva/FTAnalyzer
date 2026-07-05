namespace FTAnalyzer.Theme
{
    // Applies the ported web-app palette (Colors) to a form/control tree. Conservative by
    // design: only recolors controls that are still at their default system color, so it
    // doesn't clobber colors already set deliberately elsewhere (e.g. ColourBMD/ColourCensus
    // per-cell data coloring, or a warning label set to red). DataGridView header/selection
    // styling is intentionally left alone here - that goes through the AdvancedDataGridView
    // source repo (see UI modernisation plan, Phase 4) so every grid picks it up consistently.
    static class FormTheme
    {
        public static void Apply(Control root)
        {
            if (root is Form form)
                form.BackColor = Colors.BgParchment;
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
                        if (groupBox.BackColor == SystemColors.Control)
                            groupBox.BackColor = Colors.BgParchment;
                        if (groupBox.ForeColor == SystemColors.ControlText)
                            groupBox.ForeColor = Colors.SecondaryCharcoalBark;
                        break;
                    case TabPage tabPage:
                        // TabPage ignores BackColor while UseVisualStyleBackColor is on - it
                        // paints the OS visual-style (usually white) background instead.
                        tabPage.UseVisualStyleBackColor = false;
                        if (tabPage.BackColor == SystemColors.Control)
                            tabPage.BackColor = Colors.BgParchment;
                        break;
                    case Panel panel:
                        if (panel.BackColor == SystemColors.Control)
                            panel.BackColor = Colors.BgParchment;
                        break;
                    case Label label:
                        if (label.ForeColor == SystemColors.ControlText)
                            label.ForeColor = Colors.SecondaryCharcoalBark;
                        break;
                    case Button button:
                        if (button.BackColor == SystemColors.Control)
                        {
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = Colors.PrimaryForestGreen;
                            button.ForeColor = Colors.BgCard;
                            button.FlatAppearance.BorderColor = Colors.PrimaryForestGreen;
                        }
                        break;
                    case TextBoxBase textBox:
                        if (textBox.BackColor == SystemColors.Window)
                        {
                            // A ReadOnly RichTextBox/TextBox ignores BackColor changes made
                            // while ReadOnly is already true (long-standing RichEdit quirk) -
                            // toggle it off around the assignment so the color actually sticks.
                            bool wasReadOnly = textBox.ReadOnly;
                            if (wasReadOnly)
                                textBox.ReadOnly = false;
                            textBox.BackColor = Colors.BgCard;
                            textBox.ForeColor = Colors.SecondaryCharcoalBark;
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
    }
}
