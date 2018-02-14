using System;
using System.Reflection;

namespace FTAnalyzer.Utilities
{
    public static class ExtensionMethods
    {
#if !__MACOS__
        public static void DoubleBuffered(this System.Windows.Forms.DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
#endif

        public static DateTime TryAddYears(this DateTime date, int years)
        {
            // Make sure adding/subtracting years won't put date 
            // over DateTime.MaxValue or below DateTime.MinValue
            try
            {
                date = date.AddYears(years);
            }
            catch (ArgumentOutOfRangeException)
            {
                date = years >= 0 ? FactDate.MAXDATE : FactDate.MINDATE;
            }
            if (date > FactDate.MAXDATE) date = FactDate.MAXDATE;
            if (date < FactDate.MINDATE) date = FactDate.MINDATE;
            return date;
        }

        public static Boolean StartsWithNumeric(this String input)
        {
            if (input.Length == 0) return false;
            char first = input[0];
            return (first == '0' || first == '1' || first == '2' || first == '3' || first == '4' || first == '5' || first == '6' || first == '7' || first == '8' || first == '9');
        }
    }
}
