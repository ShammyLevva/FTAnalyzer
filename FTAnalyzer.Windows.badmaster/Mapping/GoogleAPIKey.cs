using FTAnalyzer.Utilities;
using FTAnalyzer.Properties;
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
                        if (string.IsNullOrEmpty(MappingSettings.Default.GoogleAPI))
                            APIkeyValue = "AIzaSyDJCForfeivoVF03Sr04rN9MMulO6KwA_M";
                        else
                        {
                            APIkeyValue = MappingSettings.Default.GoogleAPI;
                          UIHelpers.ShowMessage("Using your private Google API Key.\nPlease observe monthly usage limits to avoid a large bill from Google.");
                        }
                    }
                    catch (Exception)
                    {
                        APIkeyValue = "AIzaSyDJCForfeivoVF03Sr04rN9MMulO6KwA_M";
                    }
                }
                return APIkeyValue;
            }
        }
    }
}