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
        private static FactLocationImage EXACT_MATCH = new FactLocationImage(FactLocation.Geocode.EXACT_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Validate.png")));
        private static FactLocationImage PARTIAL_MATCH = new FactLocationImage(FactLocation.Geocode.PARTIAL_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        private static FactLocationImage GEDCOM = new FactLocationImage(FactLocation.Geocode.GEDCOM,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        private static FactLocationImage NO_MATCH = new FactLocationImage(FactLocation.Geocode.NO_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));

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
                case FactLocation.Geocode.EXACT_MATCH:
                    return EXACT_MATCH;
                case FactLocation.Geocode.PARTIAL_MATCH:
                    return PARTIAL_MATCH;
                case FactLocation.Geocode.GEDCOM:
                    return GEDCOM;
                case FactLocation.Geocode.NO_MATCH:
                    return NO_MATCH;
            }
            return NO_MATCH;
        }
    }
}
