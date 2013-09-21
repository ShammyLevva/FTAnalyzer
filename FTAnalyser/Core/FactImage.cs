using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class FactImage
    {
        public Fact.FactError ErrorLevel { get; private set; }
        public Bitmap Icon { get; private set; }

        private static FactImage GOOD = new FactImage(Fact.FactError.GOOD,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        private static FactImage WARNINGALLOW = new FactImage(Fact.FactError.WARNINGALLOW,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        private static FactImage WARNINGIGNORE = new FactImage(Fact.FactError.WARNINGIGNORE,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\SeriousWarning.png")));
        private static FactImage ERROR = new FactImage(Fact.FactError.ERROR,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));

        public FactImage(Fact.FactError errorLevel, Image img)
        {
            this.ErrorLevel = errorLevel;
            this.Icon = img as Bitmap;
        }

        public static FactImage ErrorIcon(Fact.FactError errorLevel)
        {
            switch (errorLevel)
            {
                case Fact.FactError.GOOD:
                    return GOOD;
                case Fact.FactError.WARNINGALLOW:
                    return WARNINGALLOW;
                case Fact.FactError.WARNINGIGNORE:
                    return WARNINGIGNORE;
                case Fact.FactError.ERROR:
                    return ERROR;
            }
            return GOOD;
        }
    }
}
