using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Org.System.Xml.Sax;

namespace FTAnalyser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            GedcomParser parser = new GedcomParser();
            InputSource input = new InputSource();
            input.SystemId = "C:/Users/Alexander/Documents/Genealogy/Bisset Tree.ged";
            parser.Parse(input);
        }
    }
}
