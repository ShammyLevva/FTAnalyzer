using static FTAnalyzer.UserControls.NonGedcomDateSettingsUI;

namespace FTAnalyzer.Utilities
{
    public class TestSettings
    {
        Properties.Settings _Settings;
        Properties.NonGedcomDate _NonGEDCOMDateSettings;

        public TestSettings()
        {
            _Settings = Properties.Settings.Default;
            _NonGEDCOMDateSettings = Properties.NonGedcomDate.Default;
        }

        public void SetNonGEDCOMDateSettings(FormatSelected formatSelected, string dateformat, string separator)
        {
            _NonGEDCOMDateSettings.UseNonGedcomDates = true;
            _NonGEDCOMDateSettings.FormatSelected = (int)formatSelected;
            _NonGEDCOMDateSettings.DateFormat = dateformat;
            _NonGEDCOMDateSettings.Regex = SetRegex(formatSelected,separator);
            _NonGEDCOMDateSettings.Separator = separator;
            Save();
        }

        public void Save() { _Settings.Save(); _NonGEDCOMDateSettings.Save(); }

        private string SetRegex(FormatSelected formatSelected, string regexSeparator)
        {
            switch(formatSelected)
            {
                case FormatSelected.DD_MM_YYYY:
                    return @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                case FormatSelected.MM_DD_YYYY:
                    return @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                case FormatSelected.YYYY_DD_MM:
                    return @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                case FormatSelected.YYYY_MM_DD:
                    return @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
            }
            return string.Empty;
        }
    }
}

