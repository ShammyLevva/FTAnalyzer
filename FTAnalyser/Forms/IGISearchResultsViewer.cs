using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FTAnalyzer.Forms
{
    public partial class IGISearchResultsViewer : Form
    {
        private string folder;
        private string lasttip = string.Empty;

        public IGISearchResultsViewer(string folder)
        {
            InitializeComponent();
            this.folder = folder;
            this.Text = "IGI Search results in folder " + folder;
            SetupResults();
        }

        public bool ResultsPresent
        { get { return lbResults.Items.Count > 0; } }

        private void SetupResults()
        {
            lbResults.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles("*.html");
            int additionalDays = (int) upIGIResultsFDayilter.Value;
            foreach (FileInfo fi in files)
            {
                if (fi.LastWriteTime.AddDays(additionalDays) >= DateTime.Now)
                    lbResults.Items.Add(fi);
            }
            labFileCount.Text = "Found " + lbResults.Items.Count + " result files";
            if (lbResults.Items.Count > 0)
                lbResults.SelectedIndex = 0;
        }

        private void lbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo fi = (FileInfo) lbResults.SelectedItem;
            webBrowser.Navigate(fi.FullName);
        }

        private void lbResults_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            int index = listBox.IndexFromPoint(e.Location);
            if (index > -1 && index < listBox.Items.Count)
            {
                string tip = listBox.Items[index].ToString();
                if (tip != lasttip)
                {
                    tooltips.SetToolTip(listBox, tip);
                    lasttip = tip;
                }
            }
        }

        private void upIGIResultsFDayilter_ValueChanged(object sender, EventArgs e)
        {
            if (upIGIResultsFDayilter.Value == 1)
                labDays.Text = "day";
            else
                labDays.Text = "days";
            webBrowser.Navigate("about:blank");
            SetupResults();
        }

    }
}
