namespace FTAnalyzer
{
    public class FactLocationImage
    {
        public FactLocation.Geocode ErrorLevel { get; private set; }
        public Bitmap Icon { get; private set; }

        static readonly FactLocationImage IMG_NOT_SEARCHED = new (FactLocation.Geocode.NOT_SEARCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\QuestionMark.png")));
        static readonly FactLocationImage IMG_MATCHED = new (FactLocation.Geocode.MATCHED,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\GoogleMatch.png")));
        static readonly FactLocationImage IMG_PARTIAL_MATCH = new (FactLocation.Geocode.PARTIAL_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\GooglePartial.png")));
        static readonly FactLocationImage IMG_GEDCOM_USER = new(FactLocation.Geocode.GEDCOM_USER,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Complete_OK.png")));
        static readonly FactLocationImage IMG_NO_MATCH = new(FactLocation.Geocode.NO_MATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\CriticalError.png")));
        static readonly FactLocationImage IMG_INCORRECT = new(FactLocation.Geocode.INCORRECT,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Flagged.png")));
        static readonly FactLocationImage IMG_OUT_OF_BOUNDS = new(FactLocation.Geocode.OUT_OF_BOUNDS,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OutOfBounds.png")));
        static readonly FactLocationImage IMG_LEVEL_MISMATCH = new(FactLocation.Geocode.LEVEL_MISMATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\Warning.png")));
        static readonly FactLocationImage IMG_OS50k_MATCH = new(FactLocation.Geocode.OS_50KMATCH,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kMatch.png")));
        static readonly FactLocationImage IMG_OS50k_PARTIAL = new(FactLocation.Geocode.OS_50KPARTIAL,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kPartial.png")));
        static readonly FactLocationImage IMG_OS50k_FUZZY = new(FactLocation.Geocode.OS_50KFUZZY,
            Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\OS50kFuzzy.png")));

        public FactLocationImage(FactLocation.Geocode errorLevel, Image img)
        {
            this.ErrorLevel = errorLevel;
            this.Icon = img as Bitmap;
        }

        public static FactLocationImage ErrorIcon(FactLocation.Geocode errorLevel)
        {
            return errorLevel switch
            {
                FactLocation.Geocode.NOT_SEARCHED => IMG_NOT_SEARCHED,
                FactLocation.Geocode.MATCHED => IMG_MATCHED,
                FactLocation.Geocode.PARTIAL_MATCH => IMG_PARTIAL_MATCH,
                FactLocation.Geocode.GEDCOM_USER => IMG_GEDCOM_USER,
                FactLocation.Geocode.NO_MATCH => IMG_NO_MATCH,
                FactLocation.Geocode.INCORRECT => IMG_INCORRECT,
                FactLocation.Geocode.OUT_OF_BOUNDS => IMG_OUT_OF_BOUNDS,
                FactLocation.Geocode.LEVEL_MISMATCH => IMG_LEVEL_MISMATCH,
                FactLocation.Geocode.OS_50KMATCH => IMG_OS50k_MATCH,
                FactLocation.Geocode.OS_50KPARTIAL => IMG_OS50k_PARTIAL,
                FactLocation.Geocode.OS_50KFUZZY => IMG_OS50k_FUZZY,
                _ => IMG_NO_MATCH,
            };
        }
    }
}
