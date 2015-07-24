using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    public class Utility
    {
        private Utility();

        public static string Soundex(string data)
        {
            StringBuilder result = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                string previousCode = "";
                string currentCode = "";
                string currentLetter = "";
                result.Append(data.Substring(0, 1));

                for (int i = 1; i < data.Length; i++)
                {
                    currentLetter = data.Substring(i, 1).ToLower();
                    currentCode = "";
                    if ("bfpv".IndexOf(currentLetter) > -1)
                        currentCode = "1";
                    else if ("cgjkqsxz".IndexOf(currentLetter) > -1)
                        currentCode = "2";
                    else if ("dt".IndexOf(currentLetter) > -1)
                        currentCode = "3";
                    else if ("mn".IndexOf(currentLetter) > -1)
                        currentCode = "4";
                    else if (currentLetter == "r")
                        currentCode = "6";

                    if (currentCode != previousCode)
                        result.Append(currentCode);
                    if (result.Length == 4) break;
                    if (currentCode != "")
                        previousCode = currentCode;
                }
            }
            if (result.Length < 4)
                result.Append(new String('0', 4 - result.Length));
            return result.ToString().ToUpper();
        }
    }
}
