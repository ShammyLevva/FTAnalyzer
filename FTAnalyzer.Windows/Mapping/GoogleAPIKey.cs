using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Mapping
{
    static class GoogleAPIKey
    {
        static string? APIkeyValue;

        public static string KeyValue
        {
            get
            {
                if (APIkeyValue is null)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(MappingSettings.Default.GoogleAPI))
                        {
                            APIkeyValue = string.Empty;
                            UIHelpers.ShowMessage("No Google API Key set.\nGeocoding needs your own FREE Google API Key for up to 10,000 lookups a month.\nSee Help menu, 'Google API Setup Guide' to get one, then enter it under Mapping Settings.");
                        }
                        else
                        {
                            APIkeyValue = MappingSettings.Default.GoogleAPI;
                            UIHelpers.ShowMessage("Using your private Google API Key.\nPlease observe monthly usage limits to avoid a large bill from Google.");
                        }
                    }
                    catch (Exception)
                    {
                        APIkeyValue = string.Empty;
                    }
                }
                return APIkeyValue;
            }
        }
    }
}