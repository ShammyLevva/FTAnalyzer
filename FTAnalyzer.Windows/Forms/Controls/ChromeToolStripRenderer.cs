namespace FTAnalyzer.Forms.Controls
{
    // Reskins MenuStrip/StatusStrip chrome (background, borders, selection) to match
    // the ported web-app palette (Theme.ActiveColors). Text colour is handled separately
    // in OnRenderItemText since ProfessionalColorTable has no text-colour hooks.
    class ChromeToolStripRenderer : ToolStripProfessionalRenderer
    {
        public ChromeToolStripRenderer() : base(new ChromeColorTable()) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            bool highlighted = e.Item.Selected || e.Item.Pressed;
            e.TextColor = highlighted ? Theme.ActiveColors.Card : Theme.ActiveColors.Text;
            base.OnRenderItemText(e);
        }
    }

    class ChromeColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => Theme.ActiveColors.Background;
        public override Color MenuStripGradientEnd => Theme.ActiveColors.Background;
        public override Color MenuItemSelected => Theme.ActiveColors.Primary;
        public override Color MenuItemSelectedGradientBegin => Theme.ActiveColors.Primary;
        public override Color MenuItemSelectedGradientEnd => Theme.ActiveColors.Primary;
        public override Color MenuItemPressedGradientBegin => Theme.ActiveColors.Primary;
        public override Color MenuItemPressedGradientEnd => Theme.ActiveColors.Primary;
        public override Color MenuItemBorder => Theme.ActiveColors.Primary;
        public override Color MenuBorder => Theme.ActiveColors.Border;
        public override Color ImageMarginGradientBegin => Theme.ActiveColors.Card;
        public override Color ImageMarginGradientMiddle => Theme.ActiveColors.Card;
        public override Color ImageMarginGradientEnd => Theme.ActiveColors.Card;
        public override Color ToolStripDropDownBackground => Theme.ActiveColors.Card;
        public override Color SeparatorDark => Theme.ActiveColors.Border;
        public override Color SeparatorLight => Theme.ActiveColors.Card;
        public override Color StatusStripGradientBegin => Theme.ActiveColors.Background;
        public override Color StatusStripGradientEnd => Theme.ActiveColors.Background;
        public override Color ToolStripBorder => Theme.ActiveColors.Border;
    }
}
