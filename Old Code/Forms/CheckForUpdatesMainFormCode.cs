using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Old_Code.Forms
{
    class CheckForUpdatesMainFormCode
    {
        //private bool _checkForUpdatesEnabled = false;
        //private bool _showNoUpdateMessage = false;
        //private System.Threading.Timer _timerCheckForUpdates;
        public void Mainform_Load()
        {
            //_timerCheckForUpdates = new System.Threading.Timer(new System.Threading.TimerCallback(_timerCheckForUpdates_Callback));
            //_timerCheckForUpdates.Change(3000, 1000 * 60 * 60 * 8); //Check for updates 3 sec after the form loads, and then again every 8 hours
        }
        //private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    _checkForUpdatesEnabled = true;
        //    _showNoUpdateMessage = true;
        //    _timerCheckForUpdates_Callback(null);
        //    _showNoUpdateMessage = false;
        //}

        //private void _timerCheckForUpdates_Callback(object data)
        //{
        //    if (_checkForUpdatesEnabled)
        //    {
        //        Version currentVersion = new Version(VERSION);
        //        string strLatestVersion = new Utilities.WebRequestWrapper().GetLatestVersionString();
        //        if (!string.IsNullOrEmpty(strLatestVersion))
        //        {
        //            Version latestVersion = new Version(strLatestVersion);
        //            if (currentVersion < latestVersion)
        //            {
        //                _checkForUpdatesEnabled = false;
        //                DialogResult result = MessageBox.Show(string.Format("A new version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?",
        //                    strLatestVersion), "New Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //                if (result == DialogResult.Yes)
        //                    Process.Start("http://FTAnalyzer.codeplex.com/");
        //            }
        //            else if (_showNoUpdateMessage)
        //            {
        //                MessageBox.Show("You are running the latest version of FTAnalyzer");
        //            }
        //        }
        //        string strBetaVersion = new Utilities.WebRequestWrapper().GetBetaVersionString();
        //        if (!string.IsNullOrEmpty(strBetaVersion))
        //        {
        //            Version betaVersion = new Version(strBetaVersion);
        //            if (currentVersion < betaVersion)
        //            {
        //                _checkForUpdatesEnabled = false;
        //                DialogResult result = MessageBox.Show(string.Format("A new TEST version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?\nPlease note this version is possibly unstable and should only be used by testers.",
        //                    strBetaVersion), "New TEST Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //                if (result == DialogResult.Yes)
        //                    Process.Start("http://FTAnalyzer.codeplex.com/");
        //            }
        //        }
        //    }
        //}

    }
}
