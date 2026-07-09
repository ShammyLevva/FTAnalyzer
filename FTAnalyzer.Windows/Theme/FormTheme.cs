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
                    // TabControl is intentionally NOT opted out of visual styles here (unlike
                    // ProgressBar/TrackBar) - doing so previously reverted each tab button to
                    // classic Win32 rendering, which draws its own hard-coded 3D relief around
                    // every tab outside what OwnerDrawFixed's WM_DRAWITEM callback controls, so
                    // recoloring our own drawn rectangle (see HighlightTabControl) had no visible
                    // effect. Leaving visual styles on for this control instead.
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
                        // The default (System-rendered) glyph is drawn by the OS visual-style
                        // handler as a light/white square regardless of app colors - same class
                        // of bug as ProgressBar/TrackBar. FlatStyle.Flat switches to .NET's own
                        // GDI+ drawing, which does respect BackColor for the box fill. Only
                        // applied in dark mode - the default glyph already reads fine against
                        // light mode's pale background. Both prior-applied states are matched
                        // (not just the untouched default) so re-toggling flips it back too.
                        if (checkBox.FlatStyle is FlatStyle.Standard or FlatStyle.Flat)
                        {
                            checkBox.FlatStyle = ActiveColors.IsDark ? FlatStyle.Flat : FlatStyle.Standard;
                            checkBox.BackColor = ActiveColors.IsDark ? ActiveColors.Background : SystemColors.Control;
                        }
                        break;
                    case RadioButton radioButton:
                        if (IsDefaultText(radioButton.ForeColor))
                            radioButton.ForeColor = ActiveColors.Text;
                        if (radioButton.FlatStyle is FlatStyle.Standard or FlatStyle.Flat)
                        {
                            radioButton.FlatStyle = ActiveColors.IsDark ? FlatStyle.Flat : FlatStyle.Standard;
                            radioButton.BackColor = ActiveColors.IsDark ? ActiveColors.Background : SystemColors.Control;
                        }
                        break;
                    case TreeView treeView:
                        if (IsDefaultWindow(treeView.BackColor))
                        {
                            treeView.BackColor = ActiveColors.Card;
                            treeView.ForeColor = ActiveColors.Text;
                        }
                        break;
                    // CheckedListBox (ckbDataErrors/ckbFactExclude/ckbFactSelect) is ListBox-based,
                    // not CheckBox-based, so it wasn't covered by the CheckBox case above at all -
                    // it's .NET-drawn like a normal ListBox, so plain BackColor/ForeColor is enough,
                    // no FlatStyle/visual-style workaround needed here.
                    case CheckedListBox checkedListBox:
                        if (IsDefaultWindow(checkedListBox.BackColor))
                        {
                            checkedListBox.BackColor = ActiveColors.Card;
                            checkedListBox.ForeColor = ActiveColors.Text;
                        }
                        break;
                    case FTAnalyzer.Forms.Controls.ThemedProgressBar progressBar:
                        progressBar.BackColor = ActiveColors.Card;
                        progressBar.ForeColor = ActiveColors.Primary;
                        break;
                    case TrackBar trackBar:
                        // tbDuplicateScore's designer explicitly sets BackColor to
                        // ControlLightLight (pure white) rather than leaving it at the plain
                        // Control default IsDefaultBackground checks for - match that too so this
                        // slider isn't silently skipped.
                        if (IsDefaultBackground(trackBar.BackColor) || trackBar.BackColor == SystemColors.ControlLightLight)
                            trackBar.BackColor = ActiveColors.Background;
                        break;
                    case Button button:
                        if (IsDefaultBackground(button.BackColor) || IsPrimary(button.BackColor))
                        {
                            button.FlatStyle = FlatStyle.Flat;
                            // UseVisualStyleBackColor (true by default on nearly every button in
                            // this designer) can leave background painting to the OS visual-style
                            // renderer even after switching FlatStyle away from System/Standard,
                            // silently ignoring the BackColor set below on some buttons (seen on
                            // the Census tab's "Record Reports" group) - explicitly turn it off so
                            // our color always wins.
                            button.UseVisualStyleBackColor = false;
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

        // Color.White is matched alongside SystemColors.Window because .NET's Color equality
        // considers the named/system-color identity, not just the underlying ARGB value - a
        // designer-set literal Color.White (e.g. rtbLostCousins) doesn't == SystemColors.Window
        // even though they render identically, so it would otherwise slip past this check the
        // same way ControlLightLight did for TrackBar.
        static bool IsDefaultWindow(Color color) =>
            color == SystemColors.Window || color == Color.White || color == Colors.BgCard || color == Colors.DarkBgCard;

        static bool IsPrimary(Color color) =>
            color == Colors.PrimaryForestGreen || color == Colors.DarkPrimary;
    }
}
