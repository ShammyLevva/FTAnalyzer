namespace FTAnalyzer.Forms.Controls
{
    // Reskins MenuStrip/StatusStrip chrome (background, borders, selection) to match
    // the ported web-app palette (Theme.Colors). Text colour is handled separately
    // in OnRenderItemText since ProfessionalColorTable has no text-colour hooks.
    class ChromeToolStripRenderer : ToolStripProfessionalRenderer
    {
        public ChromeToolStripRenderer() : base(new ChromeColorTable()) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            bool highlighted = e.Item.Selected || e.Item.Pressed;
            e.TextColor = highlighted ? Theme.Colors.BgCard : Theme.Colors.SecondaryCharcoalBark;
            base.OnRenderItemText(e);
        }
    }

    class ChromeColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => Theme.Colors.BgParchment;
        public override Color MenuStripGradientEnd => Theme.Colors.BgParchment;
        public override Color MenuItemSelected => Theme.Colors.PrimaryForestGreen;
        public override Color MenuItemSelectedGradientBegin => Theme.Colors.PrimaryForestGreen;
        public override Color MenuItemSelectedGradientEnd => Theme.Colors.PrimaryForestGreen;
        public override Color MenuItemPressedGradientBegin => Theme.Colors.PrimaryForestGreen;
        public override Color MenuItemPressedGradientEnd => Theme.Colors.PrimaryForestGreen;
        public override Color MenuItemBorder => Theme.Colors.PrimaryForestGreen;
        public override Color MenuBorder => Theme.Colors.Border;
        public override Color ImageMarginGradientBegin => Theme.Colors.BgCard;
        public override Color ImageMarginGradientMiddle => Theme.Colors.BgCard;
        public override Color ImageMarginGradientEnd => Theme.Colors.BgCard;
        public override Color ToolStripDropDownBackground => Theme.Colors.BgCard;
        public override Color SeparatorDark => Theme.Colors.Border;
        public override Color SeparatorLight => Theme.Colors.BgCard;
        public override Color StatusStripGradientBegin => Theme.Colors.BgParchment;
        public override Color StatusStripGradientEnd => Theme.Colors.BgParchment;
        public override Color ToolStripBorder => Theme.Colors.Border;
    }
}
