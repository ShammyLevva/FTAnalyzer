using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
            string path = "../../Bisset Tree.ged";
            XmlDocument document = GedcomToXml.Load(path);
            document.Save("GedcomOutput.xml");
            FamilyTree ft = FamilyTree.Instance;
            ft.LoadTree(document);
        }
    }
}
