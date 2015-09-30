using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

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
    }
}
