using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Org.System.Xml.Sax;
using System.Xml;

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
//            InputSource input = new InputSource();
//            input.SystemId = "C:/Users/Alexander/Documents/Genealogy/Bisset Tree.ged";

//            GedcomSaxParser parser = new GedcomSaxParser();
//            parser.Parse(input);
            String path = "C:/Users/Alexander/Documents/Genealogy/Bisset Tree.ged";
            XmlDocument document = GedcomToXml.Load(path);
            document.Save("GedcomOutput.xml");
        }
    }
}
