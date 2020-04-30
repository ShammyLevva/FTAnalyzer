using System.IO;
#if !__MACOS__
using System.Drawing;
using System.Windows.Forms;
#endif
namespace FTAnalyzer
{
    public class FactImage
    {
        public Fact.FactError ErrorLevel { get; private set; }
        public Bitmap Icon { get; private set; }

        static readonly FactImage GOOD = new FactImage(Fact.FactError.GOOD,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        static readonly FactImage WARNINGALLOW = new FactImage(Fact.FactError.WARNINGALLOW,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        static readonly FactImage WARNINGIGNORE = new FactImage(Fact.FactError.WARNINGIGNORE,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\SeriousWarning.png")));
        static readonly FactImage ERROR = new FactImage(Fact.FactError.ERROR,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));
        static readonly FactImage QUESTIONABLE = new FactImage(Fact.FactError.QUESTIONABLE,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\QuestionMark.png")));

        public FactImage(Fact.FactError errorLevel, Image img)
        {
            ErrorLevel = errorLevel;
            Icon = img as Bitmap;
        }

        public static FactImage ErrorIcon(Fact.FactError errorLevel)
        {
            switch (errorLevel)
            {
                case Fact.FactError.GOOD:
                case Fact.FactError.IGNORE:
                    return GOOD;
                case Fact.FactError.WARNINGALLOW:
                    return WARNINGALLOW;
                case Fact.FactError.WARNINGIGNORE:
                    return WARNINGIGNORE;
                case Fact.FactError.ERROR:
                    return ERROR;
                case Fact.FactError.QUESTIONABLE:
                    return QUESTIONABLE;
            }
            return GOOD;
        }
    }
}
