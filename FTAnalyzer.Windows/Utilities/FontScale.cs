namespace FTAnalyzer.Utilities
{
    /// <summary>
    /// Single canonical source of the four font-scale levels (1-4) offered by the
    /// Font Settings option. Both <see cref="UserControls.FontSettingsUI"/> and
    /// <see cref="MainForm"/> read from this table instead of keeping their own copies.
    /// </summary>
    public readonly record struct FontScaleLevel(float FontSize, float FontWidth, int FontHeight, float HandwritingFontSize);

    public static class FontScale
    {
        public const int MinLevel = 1;
        public const int MaxLevel = 4;
        const int DefaultLevel = 1;

        static readonly FontScaleLevel[] Levels =
        [
            new(8.25f, 5.8f, 22, 46.0f), // level 1
            new(10f, 6.6f, 27, 60.0f),   // level 2
            new(12f, 8.0f, 32, 68.0f),   // level 3
            new(14f, 9.6f, 37, 76.0f),   // level 4
        ];

        public static FontScaleLevel ForLevel(int level)
        {
            int index = level is >= MinLevel and <= MaxLevel ? level - 1 : DefaultLevel - 1;
            return Levels[index];
        }
    }
}
