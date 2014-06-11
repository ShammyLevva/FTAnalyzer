using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FTAnalyzer.Utilities
{
    public class EnhancedTextInfo
    {
        private static TextInfo txtInfo = new CultureInfo("en-GB", false).TextInfo;

        public static string ToTitleCase(string input)
        {
            string output = txtInfo.ToTitleCase(input);
            output = output.Replace(" At ", " at ")
                           .Replace(" In ", " in ")
                           .Replace(" And ", " and ")
                           .Replace(" Of ", " of ")
                           .Replace(" For ", " for ")
                           .Replace(" In ", " in ")
                           .Replace(" A ", " a ")
                           .Replace(" An ", " an ")
                           .Replace(" To ", " to ")
                           .Replace(" On ", " on ")
                           .Replace(" Or ", " or ")
                           .Replace(" As ", " as ")
                           .Replace(" Is ", " is ")
                           .Replace(" No ", " no ")
                           .Replace("1St", "1st")
                           .Replace("2Nd", "2nd")
                           .Replace("3Rd", "3rd")
                           .Replace("4Th", "4th")
                           .Replace("5Th", "5th")
                           .Replace("6Th", "6th")
                           .Replace("7Th", "7th")
                           .Replace("8Th", "8th")
                           .Replace("9Th", "9th");
            return output;
        }

        public static string ConvertStringArrayToString(string[] array)
        {
            char[] charsToTrim = {',', '.', ' '};
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(", ");
            }
            return builder.ToString().TrimEnd(charsToTrim);
        }

        public static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
