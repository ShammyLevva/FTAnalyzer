using FTAnalyzer.Utilities;
using System;

namespace FTAnalyzer.Mapping
{
    static class GoogleAPIKey
    {
        static string APIkeyValue = string.Empty;

        public static string KeyValue
        {
            get
            {
                if (string.IsNullOrEmpty(APIkeyValue))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Properties.MappingSettings.Default.GoogleAPI))
                            APIkeyValue = "default key string goes here";
                        else
                        {
                            APIkeyValue = Properties.MappingSettings.Default.GoogleAPI;
                          UIHelpers.ShowMessage("Using your private Google API Key.\nPlease observe monthly usage limits to avoid a large bill from Google.");
                        }
                    }
                    catch (Exception)
                    {
                        APIkeyValue = "default key string goes here";
                    }
                }
                return APIkeyValue;
            }
        }
    }
}