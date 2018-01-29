using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class FactLocationImage
    {
        public FactLocation.Geocode ErrorLevel { get; private set; }
        public Bitmap Icon { get; private set; }

        private static FactLocationImage IMG_NOT_SEARCHED = new FactLocationImage(FactLocation.Geocode.NOT_SEARCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\QuestionMark.png")));
        private static FactLocationImage IMG_MATCHED = new FactLocationImage(FactLocation.Geocode.MATCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\GoogleMatch.png")));
        private static FactLocationImage IMG_PARTIAL_MATCH = new FactLocationImage(FactLocation.Geocode.PARTIAL_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\GooglePartial.png")));
        private static FactLocationImage IMG_GEDCOM_USER = new FactLocationImage(FactLocation.Geocode.GEDCOM_USER,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        private static FactLocationImage IMG_NO_MATCH = new FactLocationImage(FactLocation.Geocode.NO_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));
        private static FactLocationImage IMG_INCORRECT = new FactLocationImage(FactLocation.Geocode.INCORRECT,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Flagged.png")));
        private static FactLocationImage IMG_OUT_OF_BOUNDS = new FactLocationImage(FactLocation.Geocode.OUT_OF_BOUNDS,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OutOfBounds.png")));
        private static FactLocationImage IMG_LEVEL_MISMATCH = new FactLocationImage(FactLocation.Geocode.LEVEL_MISMATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        private static FactLocationImage IMG_OS50k_MATCH = new FactLocationImage(FactLocation.Geocode.OS_50KMATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kMatch.png")));
        private static FactLocationImage IMG_OS50k_PARTIAL = new FactLocationImage(FactLocation.Geocode.OS_50KPARTIAL,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kPartial.png")));
        private static FactLocationImage IMG_OS50k_FUZZY = new FactLocationImage(FactLocation.Geocode.OS_50KFUZZY,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kFuzzy.png")));

        public FactLocationImage(FactLocation.Geocode errorLevel, Image img)
        {
            this.ErrorLevel = errorLevel;
            this.Icon = img as Bitmap;
        }

        public static FactLocationImage ErrorIcon(FactLocation.Geocode errorLevel)
        {
            switch (errorLevel)
            {
                case FactLocation.Geocode.NOT_SEARCHED:
                    return IMG_NOT_SEARCHED;
                case FactLocation.Geocode.MATCHED:
                    return IMG_MATCHED;
                case FactLocation.Geocode.PARTIAL_MATCH:
                    return IMG_PARTIAL_MATCH;
                case FactLocation.Geocode.GEDCOM_USER:
                    return IMG_GEDCOM_USER;
                case FactLocation.Geocode.NO_MATCH:
                    return IMG_NO_MATCH;
                case FactLocation.Geocode.INCORRECT:
                    return IMG_INCORRECT;
                case FactLocation.Geocode.OUT_OF_BOUNDS:
                    return IMG_OUT_OF_BOUNDS;
                case FactLocation.Geocode.LEVEL_MISMATCH:
                    return IMG_LEVEL_MISMATCH;
                case FactLocation.Geocode.OS_50KMATCH:
                    return IMG_OS50k_MATCH;
                case FactLocation.Geocode.OS_50KPARTIAL:
                    return IMG_OS50k_PARTIAL;
                case FactLocation.Geocode.OS_50KFUZZY:
                    return IMG_OS50k_FUZZY;
            }
            return IMG_NO_MATCH;
        }
    }
}
