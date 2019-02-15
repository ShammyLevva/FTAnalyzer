using static FTAnalyzer.FactDate;

namespace FTAnalyzer.Utilities
{
    public class CustomSettings
    {
        Properties.Settings _Settings;
        Properties.NonGedcomDate _NonGEDCOMDateSettings;

        public CustomSettings()
        {
            _Settings = Properties.Settings.Default;
            _NonGEDCOMDateSettings = Properties.NonGedcomDate.Default;
        }

        public void SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected formatSelected, string dateformat, string separator)
        {
            _NonGEDCOMDateSettings.UseNonGedcomDates = true;
            _NonGEDCOMDateSettings.FormatSelected = (int)formatSelected;
            _NonGEDCOMDateSettings.DateFormat = dateformat;
            _NonGEDCOMDateSettings.Regex = SetRegex(formatSelected,separator);
            _NonGEDCOMDateSettings.Separator = separator;
            Save();
        }

        public void ClearNonGEDCOMDateSettings()
        {
            _NonGEDCOMDateSettings.UseNonGedcomDates = false;
            _NonGEDCOMDateSettings.FormatSelected = (int)NonGEDCOMFormatSelected.DD_MM_YYYY;
            _NonGEDCOMDateSettings.DateFormat = "dd/mm/yyyy";
            _NonGEDCOMDateSettings.Regex = SetRegex(NonGEDCOMFormatSelected.DD_MM_YYYY, "/");
            _NonGEDCOMDateSettings.Separator = "/";
            Save();
        }

        public void Save() { _Settings.Save(); _NonGEDCOMDateSettings.Save(); }

        string SetRegex(NonGEDCOMFormatSelected formatSelected, string regexSeparator)
        {
            switch(formatSelected)
            {
                case NonGEDCOMFormatSelected.DD_MM_YYYY:
                    return @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                case NonGEDCOMFormatSelected.MM_DD_YYYY:
                    return @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})";
                case NonGEDCOMFormatSelected.YYYY_DD_MM:
                    return @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
                case NonGEDCOMFormatSelected.YYYY_MM_DD:
                    return @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})";
            }
            return string.Empty;
        }
    }
}

