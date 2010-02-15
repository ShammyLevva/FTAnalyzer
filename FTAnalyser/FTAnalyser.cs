using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FTAnalyser
{
    static class FTAnalyser
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
