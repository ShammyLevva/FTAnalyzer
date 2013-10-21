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

        private static FactLocationImage NOT_SEARCHED = new FactLocationImage(FactLocation.Geocode.NOT_SEARCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\QuestionMark.png")));
        private static FactLocationImage MATCHED = new FactLocationImage(FactLocation.Geocode.MATCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Validate.png")));
        private static FactLocationImage PARTIAL_MATCH = new FactLocationImage(FactLocation.Geocode.PARTIAL_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        private static FactLocationImage GEDCOM_USER = new FactLocationImage(FactLocation.Geocode.GEDCOM_USER,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        private static FactLocationImage NO_MATCH = new FactLocationImage(FactLocation.Geocode.NO_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));
        private static FactLocationImage INCORRECT = new FactLocationImage(FactLocation.Geocode.INCORRECT,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Flagged.png")));
        private static FactLocationImage OUT_OF_BOUNDS = new FactLocationImage(FactLocation.Geocode.OUT_OF_BOUNDS,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OutOfBounds.png")));

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
                    return NOT_SEARCHED;
                case FactLocation.Geocode.MATCHED:
                    return MATCHED;
                case FactLocation.Geocode.PARTIAL_MATCH:
                    return PARTIAL_MATCH;
                case FactLocation.Geocode.GEDCOM_USER:
                    return GEDCOM_USER;
                case FactLocation.Geocode.NO_MATCH:
                    return NO_MATCH;
                case FactLocation.Geocode.INCORRECT:
                    return INCORRECT;
                case FactLocation.Geocode.OUT_OF_BOUNDS:
                    return OUT_OF_BOUNDS;
            }
            return NO_MATCH;
        }
    }
}
