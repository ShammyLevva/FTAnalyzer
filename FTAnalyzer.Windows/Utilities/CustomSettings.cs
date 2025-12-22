using static FTAnalyzer.FactDate;

namespace FTAnalyzer.Utilities
{
    public class CustomSettings
    {
        readonly Properties.Settings _Settings;
        readonly Properties.NonGedcomDate _NonGEDCOMDateSettings;

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
            _NonGEDCOMDateSettings.Regex = SetRegex(formatSelected, separator);
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

        static string SetRegex(NonGEDCOMFormatSelected formatSelected, string regexSeparator)
        {
            return formatSelected switch
            {
                NonGEDCOMFormatSelected.DD_MM_YYYY => @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})",
                NonGEDCOMFormatSelected.MM_DD_YYYY => @"(\d{1,2})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{4})",
                NonGEDCOMFormatSelected.YYYY_DD_MM => @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})",
                NonGEDCOMFormatSelected.YYYY_MM_DD => @"(\d{4})" + regexSeparator + @"(\d{1,2})" + regexSeparator + @"(\d{1,2})",
                NonGEDCOMFormatSelected.NONE => string.Empty,
                _ => string.Empty,
            };
        }
    }
}

