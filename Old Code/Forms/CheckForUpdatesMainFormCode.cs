using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Old_Code.Forms
{
    class CheckForUpdatesMainFormCode
    {
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
    }
}
