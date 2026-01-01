#if !__MACOS__
#endif
namespace FTAnalyzer.Graphics
{
    public class FactImage(Fact.FactError errorLevel, Image img)
    {
        public Fact.FactError ErrorLevel { get; private set; } = errorLevel;
        public Bitmap Icon { get; private set; } = GraphicsUtilities.ResizeImageToCurrentScale(img);

        static readonly FactImage GOOD = new(Fact.FactError.GOOD,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        static readonly FactImage WARNINGALLOW = new(Fact.FactError.WARNINGALLOW,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        static readonly FactImage WARNINGIGNORE = new(Fact.FactError.WARNINGIGNORE,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\SeriousWarning.png")));
        static readonly FactImage ERROR = new(Fact.FactError.ERROR,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));
        static readonly FactImage QUESTIONABLE = new(Fact.FactError.QUESTIONABLE,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\QuestionMark.png")));

        public static FactImage ErrorIcon(Fact.FactError errorLevel)
        {
            return errorLevel switch
            {
                Fact.FactError.GOOD or Fact.FactError.IGNORE => GOOD,
                Fact.FactError.WARNINGALLOW => WARNINGALLOW,
                Fact.FactError.WARNINGIGNORE => WARNINGIGNORE,
                Fact.FactError.ERROR => ERROR,
                Fact.FactError.QUESTIONABLE => QUESTIONABLE,
                _ => GOOD,
            };
        }
    }
}
