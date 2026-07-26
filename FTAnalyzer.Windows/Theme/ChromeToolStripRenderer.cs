namespace FTAnalyzer.Theme
{
    // Reskins MenuStrip/StatusStrip chrome (background, borders, selection) to match
    // the ported web-app palette (ActiveColors). Text colour is handled separately
    // in OnRenderItemText since ProfessionalColorTable has no text-colour hooks.
    class ChromeToolStripRenderer : ToolStripProfessionalRenderer
    {
        public ChromeToolStripRenderer() : base(new ChromeColorTable()) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            bool highlighted = e.Item.Selected || e.Item.Pressed;
            e.TextColor = highlighted ? ActiveColors.OnPrimary : ActiveColors.Text;
            base.OnRenderItemText(e);
        }

        // The submenu ► glyph (e.g. "Recent Files", "Geocode Database") has its own render hook
        // separate from OnRenderItemText - ProfessionalColorTable has no color property for it at
        // all, so left unhandled it falls back to ToolStripRenderer's hardcoded black, invisible
        // against a dark menu background.
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            bool highlighted = e.Item is not null && (e.Item.Selected || e.Item.Pressed);
            e.ArrowColor = highlighted ? ActiveColors.OnPrimary : ActiveColors.Text;
            base.OnRenderArrow(e);
        }
    }

    class ChromeColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => ActiveColors.Background;
        public override Color MenuStripGradientEnd => ActiveColors.Background;
        public override Color MenuItemSelected => ActiveColors.Primary;
        public override Color MenuItemSelectedGradientBegin => ActiveColors.Primary;
        public override Color MenuItemSelectedGradientEnd => ActiveColors.Primary;
        public override Color MenuItemPressedGradientBegin => ActiveColors.Primary;
        public override Color MenuItemPressedGradientEnd => ActiveColors.Primary;
        public override Color MenuItemBorder => ActiveColors.Primary;
        public override Color MenuBorder => ActiveColors.Border;
        public override Color ImageMarginGradientBegin => ActiveColors.Card;
        public override Color ImageMarginGradientMiddle => ActiveColors.Card;
        public override Color ImageMarginGradientEnd => ActiveColors.Card;
        public override Color ToolStripDropDownBackground => ActiveColors.Card;
        public override Color SeparatorDark => ActiveColors.Border;
        public override Color SeparatorLight => ActiveColors.Card;
        public override Color StatusStripGradientBegin => ActiveColors.Background;
        public override Color StatusStripGradientEnd => ActiveColors.Background;
        public override Color ToolStripBorder => ActiveColors.Border;
        // Plain ToolStrip (e.g. report windows' Print/Export toolbars) paints its background from
        // these, not the MenuStrip/StatusStrip gradients above - without them a themed report
        // window's toolbar stayed stuck at the professional renderer's default light gradient.
        public override Color ToolStripGradientBegin => ActiveColors.Background;
        public override Color ToolStripGradientMiddle => ActiveColors.Background;
        public override Color ToolStripGradientEnd => ActiveColors.Background;
    }
}
