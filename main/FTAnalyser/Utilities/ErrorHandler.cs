﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Properties;
using System.Windows.Forms;
using System.Diagnostics;

namespace FTAnalyzer.Utilities
{
    public static class ErrorHandler
    {
        public static void Show(string errorNum, Exception ex)
        {
            Show(errorNum, ex, MessageBoxIcon.Error);
        }

        public static void Show(string errorNum, Exception ex, MessageBoxIcon icon)
        {
            string message = ErrorMessages.ResourceManager.GetString(errorNum);
            int eventId = 0;
            int.TryParse(errorNum.Substring(4), out eventId);
            MessageBox.Show(message + "\n" + ex.Message,
                        "Error: " + errorNum,
                        MessageBoxButtons.OK,
                        icon);
        }
    }
}
