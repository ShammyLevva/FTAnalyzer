using System;

namespace FTAnalyzer.Forms
{
    partial class ColourCensus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components is not null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);

                boldFont.Dispose();
                reportFormHelper.Dispose();
            }
            catch (Exception) { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourCensus));
            dgReportSheet = new DataGridView();
            IndividualID = new DataGridViewTextBoxColumn();
            Forenames = new DataGridViewTextBoxColumn();
            Surname = new DataGridViewTextBoxColumn();
            Relation = new DataGridViewTextBoxColumn();
            RelationToRoot = new DataGridViewTextBoxColumn();
            C1841 = new DataGridViewTextBoxColumn();
            C1851 = new DataGridViewTextBoxColumn();
            C1861 = new DataGridViewTextBoxColumn();
            C1871 = new DataGridViewTextBoxColumn();
            C1881 = new DataGridViewTextBoxColumn();
            C1891 = new DataGridViewTextBoxColumn();
            C1901 = new DataGridViewTextBoxColumn();
            C1911 = new DataGridViewTextBoxColumn();
            C1921 = new DataGridViewTextBoxColumn();
            C1939 = new DataGridViewTextBoxColumn();
            US1790 = new DataGridViewTextBoxColumn();
            US1800 = new DataGridViewTextBoxColumn();
            US1810 = new DataGridViewTextBoxColumn();
            US1820 = new DataGridViewTextBoxColumn();
            US1830 = new DataGridViewTextBoxColumn();
            US1840 = new DataGridViewTextBoxColumn();
            US1850 = new DataGridViewTextBoxColumn();
            US1860 = new DataGridViewTextBoxColumn();
            US1870 = new DataGridViewTextBoxColumn();
            US1880 = new DataGridViewTextBoxColumn();
            US1890 = new DataGridViewTextBoxColumn();
            US1900 = new DataGridViewTextBoxColumn();
            US1910 = new DataGridViewTextBoxColumn();
            US1920 = new DataGridViewTextBoxColumn();
            US1930 = new DataGridViewTextBoxColumn();
            US1940 = new DataGridViewTextBoxColumn();
            US1950 = new DataGridViewTextBoxColumn();
            Can1851 = new DataGridViewTextBoxColumn();
            Can1861 = new DataGridViewTextBoxColumn();
            Can1871 = new DataGridViewTextBoxColumn();
            Can1881 = new DataGridViewTextBoxColumn();
            Can1891 = new DataGridViewTextBoxColumn();
            Can1901 = new DataGridViewTextBoxColumn();
            Can1906 = new DataGridViewTextBoxColumn();
            Can1911 = new DataGridViewTextBoxColumn();
            Can1916 = new DataGridViewTextBoxColumn();
            Can1921 = new DataGridViewTextBoxColumn();
            Ire1901 = new DataGridViewTextBoxColumn();
            Ire1911 = new DataGridViewTextBoxColumn();
            Ire1926 = new DataGridViewTextBoxColumn();
            BirthDate = new DataGridViewTextBoxColumn();
            BirthLocation = new DataGridViewTextBoxColumn();
            DeathDate = new DataGridViewTextBoxColumn();
            DeathLocation = new DataGridViewTextBoxColumn();
            BestLocation = new DataGridViewTextBoxColumn();
            Ahnentafel = new DataGridViewTextBoxColumn();
            contextMenuStrip = new ContextMenuStrip(components);
            mnuViewFacts = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            tsRecords = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            mnuSaveCensusColumnLayout = new ToolStripButton();
            mnuResetCensusColumns = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuExportToExcel = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            cbCensusSearchProvider = new ToolStripComboBox();
            toolStripLabel3 = new ToolStripLabel();
            cbRegion = new ToolStripComboBox();
            toolStripLabel2 = new ToolStripLabel();
            cbFilter = new ToolStripComboBox();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDialog = new PrintDialog();
            printPreviewDialog = new PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)dgReportSheet).BeginInit();
            contextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgReportSheet
            // 
            dgReportSheet.AllowUserToAddRows = false;
            dgReportSheet.AllowUserToDeleteRows = false;
            dgReportSheet.AllowUserToOrderColumns = true;
            dgReportSheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgReportSheet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgReportSheet.Columns.AddRange(new DataGridViewColumn[] { IndividualID, Forenames, Surname, Relation, RelationToRoot, C1841, C1851, C1861, C1871, C1881, C1891, C1901, C1911, C1921, C1939, US1790, US1800, US1810, US1820, US1830, US1840, US1850, US1860, US1870, US1880, US1890, US1900, US1910, US1920, US1930, US1940, US1950, Can1851, Can1861, Can1871, Can1881, Can1891, Can1901, Can1906, Can1911, Can1916, Can1921, Ire1901, Ire1911, Ire1926, BirthDate, BirthLocation, DeathDate, DeathLocation, BestLocation, Ahnentafel });
            dgReportSheet.ContextMenuStrip = contextMenuStrip;
            dgReportSheet.Dock = DockStyle.Fill;
            dgReportSheet.Location = new Point(0, 39);
            dgReportSheet.Margin = new Padding(4);
            dgReportSheet.Name = "dgReportSheet";
            dgReportSheet.ReadOnly = true;
            dgReportSheet.RowHeadersWidth = 20;
            dgReportSheet.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgReportSheet.Size = new Size(1211, 602);
            dgReportSheet.TabIndex = 1;
            dgReportSheet.CellContextMenuStripNeeded += DgReportSheet_CellContextMenuStripNeeded;
            dgReportSheet.CellDoubleClick += DgReportSheet_CellDoubleClick;
            dgReportSheet.CellFormatting += DgReportSheet_CellFormatting;
            dgReportSheet.SelectionChanged += DgReportSheet_SelectionChanged;
            // 
            // IndividualID
            // 
            IndividualID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            IndividualID.DataPropertyName = "IndividualID";
            IndividualID.HeaderText = "Ind. ID";
            IndividualID.MinimumWidth = 60;
            IndividualID.Name = "IndividualID";
            IndividualID.ReadOnly = true;
            IndividualID.Width = 60;
            // 
            // Forenames
            // 
            Forenames.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Forenames.DataPropertyName = "Forenames";
            Forenames.HeaderText = "Forenames";
            Forenames.MinimumWidth = 100;
            Forenames.Name = "Forenames";
            Forenames.ReadOnly = true;
            Forenames.Width = 200;
            // 
            // Surname
            // 
            Surname.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Surname.DataPropertyName = "Surname";
            Surname.HeaderText = "Surname";
            Surname.MinimumWidth = 120;
            Surname.Name = "Surname";
            Surname.ReadOnly = true;
            Surname.Width = 120;
            // 
            // Relation
            // 
            Relation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Relation.DataPropertyName = "Relation";
            Relation.HeaderText = "Relation";
            Relation.MinimumWidth = 120;
            Relation.Name = "Relation";
            Relation.ReadOnly = true;
            Relation.Width = 120;
            // 
            // RelationToRoot
            // 
            RelationToRoot.DataPropertyName = "RelationToRoot";
            RelationToRoot.HeaderText = "Relation To Root";
            RelationToRoot.MinimumWidth = 100;
            RelationToRoot.Name = "RelationToRoot";
            RelationToRoot.ReadOnly = true;
            // 
            // C1841
            // 
            C1841.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1841.DataPropertyName = "C1841";
            C1841.HeaderText = "1841 Census";
            C1841.MinimumWidth = 80;
            C1841.Name = "C1841";
            C1841.ReadOnly = true;
            C1841.Width = 80;
            // 
            // C1851
            // 
            C1851.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1851.DataPropertyName = "C1851";
            C1851.HeaderText = "1851 Census";
            C1851.MinimumWidth = 80;
            C1851.Name = "C1851";
            C1851.ReadOnly = true;
            C1851.Width = 80;
            // 
            // C1861
            // 
            C1861.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1861.DataPropertyName = "C1861";
            C1861.HeaderText = "1861 Census";
            C1861.MinimumWidth = 80;
            C1861.Name = "C1861";
            C1861.ReadOnly = true;
            C1861.Width = 80;
            // 
            // C1871
            // 
            C1871.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1871.DataPropertyName = "C1871";
            C1871.HeaderText = "1871 Census";
            C1871.MinimumWidth = 80;
            C1871.Name = "C1871";
            C1871.ReadOnly = true;
            C1871.Width = 80;
            // 
            // C1881
            // 
            C1881.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1881.DataPropertyName = "C1881";
            C1881.HeaderText = "1881 Census";
            C1881.MinimumWidth = 80;
            C1881.Name = "C1881";
            C1881.ReadOnly = true;
            C1881.Width = 80;
            // 
            // C1891
            // 
            C1891.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1891.DataPropertyName = "C1891";
            C1891.HeaderText = "1891 Census";
            C1891.MinimumWidth = 80;
            C1891.Name = "C1891";
            C1891.ReadOnly = true;
            C1891.Width = 80;
            // 
            // C1901
            // 
            C1901.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1901.DataPropertyName = "C1901";
            C1901.HeaderText = "1901 Census";
            C1901.MinimumWidth = 80;
            C1901.Name = "C1901";
            C1901.ReadOnly = true;
            C1901.Width = 80;
            // 
            // C1911
            // 
            C1911.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1911.DataPropertyName = "C1911";
            C1911.HeaderText = "1911 Census";
            C1911.MinimumWidth = 80;
            C1911.Name = "C1911";
            C1911.ReadOnly = true;
            C1911.Width = 80;
            // 
            // C1921
            // 
            C1921.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1921.DataPropertyName = "C1921";
            C1921.HeaderText = "1921 Census";
            C1921.MinimumWidth = 80;
            C1921.Name = "C1921";
            C1921.ReadOnly = true;
            C1921.Width = 80;
            // 
            // C1939
            // 
            C1939.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            C1939.DataPropertyName = "C1939";
            C1939.HeaderText = "1939 National Register";
            C1939.MinimumWidth = 100;
            C1939.Name = "C1939";
            C1939.ReadOnly = true;
            C1939.Width = 175;
            // 
            // US1790
            // 
            US1790.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1790.DataPropertyName = "US1790";
            US1790.HeaderText = "1790 US Census";
            US1790.MinimumWidth = 80;
            US1790.Name = "US1790";
            US1790.ReadOnly = true;
            US1790.Width = 80;
            // 
            // US1800
            // 
            US1800.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1800.DataPropertyName = "US1800";
            US1800.HeaderText = "1800 US Census";
            US1800.MinimumWidth = 80;
            US1800.Name = "US1800";
            US1800.ReadOnly = true;
            US1800.Width = 80;
            // 
            // US1810
            // 
            US1810.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1810.DataPropertyName = "US1810";
            US1810.HeaderText = "1810 US Census";
            US1810.MinimumWidth = 80;
            US1810.Name = "US1810";
            US1810.ReadOnly = true;
            US1810.Width = 80;
            // 
            // US1820
            // 
            US1820.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1820.DataPropertyName = "US1820";
            US1820.HeaderText = "1820 US Census";
            US1820.MinimumWidth = 80;
            US1820.Name = "US1820";
            US1820.ReadOnly = true;
            US1820.Width = 80;
            // 
            // US1830
            // 
            US1830.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1830.DataPropertyName = "US1830";
            US1830.HeaderText = "1830 US Census";
            US1830.MinimumWidth = 80;
            US1830.Name = "US1830";
            US1830.ReadOnly = true;
            US1830.Width = 80;
            // 
            // US1840
            // 
            US1840.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1840.DataPropertyName = "US1840";
            US1840.HeaderText = "1840 US Census";
            US1840.MinimumWidth = 80;
            US1840.Name = "US1840";
            US1840.ReadOnly = true;
            US1840.Width = 80;
            // 
            // US1850
            // 
            US1850.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1850.DataPropertyName = "US1850";
            US1850.HeaderText = "1850 US Census";
            US1850.MinimumWidth = 80;
            US1850.Name = "US1850";
            US1850.ReadOnly = true;
            US1850.Width = 80;
            // 
            // US1860
            // 
            US1860.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1860.DataPropertyName = "US1860";
            US1860.HeaderText = "1860 US Census";
            US1860.MinimumWidth = 80;
            US1860.Name = "US1860";
            US1860.ReadOnly = true;
            US1860.Width = 80;
            // 
            // US1870
            // 
            US1870.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1870.DataPropertyName = "US1870";
            US1870.HeaderText = "1870 US Census";
            US1870.MinimumWidth = 80;
            US1870.Name = "US1870";
            US1870.ReadOnly = true;
            US1870.Width = 80;
            // 
            // US1880
            // 
            US1880.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1880.DataPropertyName = "US1880";
            US1880.HeaderText = "1880 US Census";
            US1880.MinimumWidth = 80;
            US1880.Name = "US1880";
            US1880.ReadOnly = true;
            US1880.Width = 80;
            // 
            // US1890
            // 
            US1890.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1890.DataPropertyName = "US1890";
            US1890.HeaderText = "1890 US Census";
            US1890.MinimumWidth = 80;
            US1890.Name = "US1890";
            US1890.ReadOnly = true;
            US1890.Width = 80;
            // 
            // US1900
            // 
            US1900.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1900.DataPropertyName = "US1900";
            US1900.HeaderText = "1900 US Census";
            US1900.MinimumWidth = 80;
            US1900.Name = "US1900";
            US1900.ReadOnly = true;
            US1900.Width = 80;
            // 
            // US1910
            // 
            US1910.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1910.DataPropertyName = "US1910";
            US1910.HeaderText = "1910 US Census";
            US1910.MinimumWidth = 80;
            US1910.Name = "US1910";
            US1910.ReadOnly = true;
            US1910.Width = 80;
            // 
            // US1920
            // 
            US1920.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1920.DataPropertyName = "US1920";
            US1920.HeaderText = "1920 US Census";
            US1920.MinimumWidth = 80;
            US1920.Name = "US1920";
            US1920.ReadOnly = true;
            US1920.Width = 80;
            // 
            // US1930
            // 
            US1930.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1930.DataPropertyName = "US1930";
            US1930.HeaderText = "1930 US Census";
            US1930.MinimumWidth = 80;
            US1930.Name = "US1930";
            US1930.ReadOnly = true;
            US1930.Width = 80;
            // 
            // US1940
            // 
            US1940.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1940.DataPropertyName = "US1940";
            US1940.HeaderText = "1940 US Census";
            US1940.MinimumWidth = 80;
            US1940.Name = "US1940";
            US1940.ReadOnly = true;
            US1940.Width = 80;
            // 
            // US1950
            // 
            US1950.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            US1950.DataPropertyName = "US1950";
            US1950.HeaderText = "1950 US Census";
            US1950.MinimumWidth = 80;
            US1950.Name = "US1950";
            US1950.ReadOnly = true;
            US1950.Width = 80;
            // 
            // Can1851
            // 
            Can1851.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1851.DataPropertyName = "Can1851";
            Can1851.HeaderText = "1851/2 Canada Census";
            Can1851.MinimumWidth = 80;
            Can1851.Name = "Can1851";
            Can1851.ReadOnly = true;
            Can1851.Width = 80;
            // 
            // Can1861
            // 
            Can1861.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1861.DataPropertyName = "Can1861";
            Can1861.HeaderText = "1861 Canada Census";
            Can1861.MinimumWidth = 80;
            Can1861.Name = "Can1861";
            Can1861.ReadOnly = true;
            Can1861.Width = 80;
            // 
            // Can1871
            // 
            Can1871.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1871.DataPropertyName = "Can1871";
            Can1871.HeaderText = "1871 Canada Census";
            Can1871.MinimumWidth = 80;
            Can1871.Name = "Can1871";
            Can1871.ReadOnly = true;
            Can1871.Width = 80;
            // 
            // Can1881
            // 
            Can1881.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1881.DataPropertyName = "Can1881";
            Can1881.HeaderText = "1881 Canada Census";
            Can1881.MinimumWidth = 80;
            Can1881.Name = "Can1881";
            Can1881.ReadOnly = true;
            Can1881.Width = 80;
            // 
            // Can1891
            // 
            Can1891.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1891.DataPropertyName = "Can1891";
            Can1891.HeaderText = "1891 Canada Census";
            Can1891.MinimumWidth = 80;
            Can1891.Name = "Can1891";
            Can1891.ReadOnly = true;
            Can1891.Width = 80;
            // 
            // Can1901
            // 
            Can1901.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1901.DataPropertyName = "Can1901";
            Can1901.HeaderText = "1901 Canada Census";
            Can1901.MinimumWidth = 80;
            Can1901.Name = "Can1901";
            Can1901.ReadOnly = true;
            Can1901.Width = 80;
            // 
            // Can1906
            // 
            Can1906.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1906.DataPropertyName = "Can1906";
            Can1906.HeaderText = "1906 Canada Census";
            Can1906.MinimumWidth = 80;
            Can1906.Name = "Can1906";
            Can1906.ReadOnly = true;
            Can1906.Width = 80;
            // 
            // Can1911
            // 
            Can1911.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1911.DataPropertyName = "Can1911";
            Can1911.HeaderText = "1911 Canada Census";
            Can1911.MinimumWidth = 80;
            Can1911.Name = "Can1911";
            Can1911.ReadOnly = true;
            Can1911.Resizable = DataGridViewTriState.False;
            Can1911.Width = 80;
            // 
            // Can1916
            // 
            Can1916.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1916.DataPropertyName = "Can1916";
            Can1916.HeaderText = "1916 Canada Census";
            Can1916.MinimumWidth = 80;
            Can1916.Name = "Can1916";
            Can1916.ReadOnly = true;
            Can1916.Width = 80;
            // 
            // Can1921
            // 
            Can1921.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Can1921.DataPropertyName = "Can1921";
            Can1921.HeaderText = "1921 Canada Census";
            Can1921.MinimumWidth = 80;
            Can1921.Name = "Can1921";
            Can1921.ReadOnly = true;
            Can1921.Width = 80;
            // 
            // Ire1901
            // 
            Ire1901.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Ire1901.DataPropertyName = "Ire1901";
            Ire1901.HeaderText = "1901 Irish Census";
            Ire1901.MinimumWidth = 80;
            Ire1901.Name = "Ire1901";
            Ire1901.ReadOnly = true;
            Ire1901.Width = 80;
            // 
            // Ire1911
            // 
            Ire1911.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Ire1911.DataPropertyName = "Ire1911";
            Ire1911.HeaderText = "1911 Irish Census";
            Ire1911.MinimumWidth = 80;
            Ire1911.Name = "Ire1911";
            Ire1911.ReadOnly = true;
            Ire1911.Width = 80;
            // 
            // Ire1926
            // 
            Ire1926.DataPropertyName = "Ire1926";
            Ire1926.HeaderText = "1926 Irish Census";
            Ire1926.Name = "Ire1926";
            Ire1926.ReadOnly = true;
            Ire1926.Width = 112;
            // 
            // BirthDate
            // 
            BirthDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            BirthDate.DataPropertyName = "BirthDate";
            BirthDate.HeaderText = "Birth Date";
            BirthDate.MinimumWidth = 100;
            BirthDate.Name = "BirthDate";
            BirthDate.ReadOnly = true;
            BirthDate.Width = 200;
            // 
            // BirthLocation
            // 
            BirthLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            BirthLocation.DataPropertyName = "BirthLocation";
            BirthLocation.HeaderText = "Birth Location";
            BirthLocation.MinimumWidth = 120;
            BirthLocation.Name = "BirthLocation";
            BirthLocation.ReadOnly = true;
            BirthLocation.Width = 200;
            // 
            // DeathDate
            // 
            DeathDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DeathDate.DataPropertyName = "DeathDate";
            DeathDate.HeaderText = "Death Date";
            DeathDate.MinimumWidth = 100;
            DeathDate.Name = "DeathDate";
            DeathDate.ReadOnly = true;
            DeathDate.Width = 200;
            // 
            // DeathLocation
            // 
            DeathLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DeathLocation.DataPropertyName = "DeathLocation";
            DeathLocation.HeaderText = "Death Location";
            DeathLocation.MinimumWidth = 120;
            DeathLocation.Name = "DeathLocation";
            DeathLocation.ReadOnly = true;
            DeathLocation.Width = 200;
            // 
            // BestLocation
            // 
            BestLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            BestLocation.DataPropertyName = "BestLocation";
            BestLocation.HeaderText = "Best Location";
            BestLocation.MinimumWidth = 120;
            BestLocation.Name = "BestLocation";
            BestLocation.ReadOnly = true;
            BestLocation.Width = 200;
            // 
            // Ahnentafel
            // 
            Ahnentafel.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Ahnentafel.DataPropertyName = "Ahnentafel";
            Ahnentafel.HeaderText = "Ahnentafel";
            Ahnentafel.MinimumWidth = 50;
            Ahnentafel.Name = "Ahnentafel";
            Ahnentafel.ReadOnly = true;
            Ahnentafel.Width = 90;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(32, 32);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { mnuViewFacts });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(190, 26);
            // 
            // mnuViewFacts
            // 
            mnuViewFacts.Name = "mnuViewFacts";
            mnuViewFacts.Size = new Size(189, 22);
            mnuViewFacts.Text = "View Individuals Facts";
            mnuViewFacts.Click += MnuViewFacts_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(32, 32);
            statusStrip.Items.AddRange(new ToolStripItem[] { tsRecords });
            statusStrip.Location = new Point(0, 641);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 17, 0);
            statusStrip.Size = new Size(1211, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            tsRecords.Name = "tsRecords";
            tsRecords.Size = new Size(118, 17);
            tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(32, 32);
            toolStrip1.Items.AddRange(new ToolStripItem[] { mnuSaveCensusColumnLayout, mnuResetCensusColumns, toolStripSeparator3, printToolStripButton, printPreviewToolStripButton, toolStripSeparator1, mnuExportToExcel, toolStripSeparator2, toolStripLabel1, cbCensusSearchProvider, toolStripLabel3, cbRegion, toolStripLabel2, cbFilter });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 3, 0);
            toolStrip1.Size = new Size(1211, 39);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            mnuSaveCensusColumnLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuSaveCensusColumnLayout.Image = (Image)resources.GetObject("mnuSaveCensusColumnLayout.Image");
            mnuSaveCensusColumnLayout.ImageTransparentColor = Color.Magenta;
            mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            mnuSaveCensusColumnLayout.Size = new Size(36, 36);
            mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            mnuSaveCensusColumnLayout.Click += MnuSaveCensusColumnLayout_Click;
            // 
            // mnuResetCensusColumns
            // 
            mnuResetCensusColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuResetCensusColumns.Image = (Image)resources.GetObject("mnuResetCensusColumns.Image");
            mnuResetCensusColumns.ImageTransparentColor = Color.Magenta;
            mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            mnuResetCensusColumns.Size = new Size(36, 36);
            mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            mnuResetCensusColumns.Click += MnuResetCensusColumns_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 39);
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(36, 36);
            printToolStripButton.Text = "&Print";
            printToolStripButton.Click += PrintToolStripButton_Click;
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(36, 36);
            printPreviewToolStripButton.Text = "Print Preview...";
            printPreviewToolStripButton.Click += PrintPreviewToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 39);
            // 
            // mnuExportToExcel
            // 
            mnuExportToExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuExportToExcel.Image = (Image)resources.GetObject("mnuExportToExcel.Image");
            mnuExportToExcel.ImageTransparentColor = Color.Magenta;
            mnuExportToExcel.Name = "mnuExportToExcel";
            mnuExportToExcel.Size = new Size(36, 36);
            mnuExportToExcel.Text = "Export to Excel";
            mnuExportToExcel.Click += MnuExportToExcel_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 39);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(117, 36);
            toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            cbCensusSearchProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCensusSearchProvider.Items.AddRange(new object[] { "Ancestry", "Find My Past", "FreeCen", "FamilySearch", "Scotlands People" });
            cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            cbCensusSearchProvider.Size = new Size(141, 39);
            cbCensusSearchProvider.SelectedIndexChanged += CbCensusSearchProvider_SelectedIndexChanged;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(47, 36);
            toolStripLabel3.Text = "Region:";
            // 
            // cbRegion
            // 
            cbRegion.Items.AddRange(new object[] { ".com", ".co.uk", ".ca", ".com.au" });
            cbRegion.Name = "cbRegion";
            cbRegion.Size = new Size(141, 39);
            cbRegion.Text = ".co.uk";
            cbRegion.SelectedIndexChanged += CbRegion_SelectedIndexChanged;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(39, 36);
            toolStripLabel2.Text = "Filter :";
            // 
            // cbFilter
            // 
            cbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilter.DropDownWidth = 330;
            cbFilter.Items.AddRange(new object[] { "All Individuals", "None Found (All Red)", "All Found (All Green)", "Lost Cousins Missing (Yellow)", "Lost Cousins Present (Orange)", "Outside UK (Dark Grey)", "Some Missing (Some Red)", "Some Found (Some Green)", "Known Missing (Mid Green)" });
            cbFilter.Name = "cbFilter";
            cbFilter.Size = new Size(150, 39);
            cbFilter.SelectedIndexChanged += CbFilter_SelectedIndexChanged;
            // 
            // printDialog
            // 
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.Document = printDocument;
            printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.Enabled = true;
            printPreviewDialog.Icon = (Icon)resources.GetObject("printPreviewDialog.Icon");
            printPreviewDialog.Name = "printPreviewDialog";
            printPreviewDialog.Visible = false;
            // 
            // ColourCensus
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 663);
            Controls.Add(dgReportSheet);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "ColourCensus";
            Text = "Colour Census Report Result";
            FormClosed += ColourCensus_FormClosed;
            Load += ColourCensus_Load;
            ((System.ComponentModel.ISupportInitialize)dgReportSheet).EndInit();
            contextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgReportSheet;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbCensusSearchProvider;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbFilter;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuSaveCensusColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetCensusColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFacts;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private DataGridViewTextBoxColumn IndividualID;
        private DataGridViewTextBoxColumn Forenames;
        private DataGridViewTextBoxColumn Surname;
        private DataGridViewTextBoxColumn Relation;
        private DataGridViewTextBoxColumn RelationToRoot;
        private DataGridViewTextBoxColumn C1841;
        private DataGridViewTextBoxColumn C1851;
        private DataGridViewTextBoxColumn C1861;
        private DataGridViewTextBoxColumn C1871;
        private DataGridViewTextBoxColumn C1881;
        private DataGridViewTextBoxColumn C1891;
        private DataGridViewTextBoxColumn C1901;
        private DataGridViewTextBoxColumn C1911;
        private DataGridViewTextBoxColumn C1921;
        private DataGridViewTextBoxColumn C1939;
        private DataGridViewTextBoxColumn US1790;
        private DataGridViewTextBoxColumn US1800;
        private DataGridViewTextBoxColumn US1810;
        private DataGridViewTextBoxColumn US1820;
        private DataGridViewTextBoxColumn US1830;
        private DataGridViewTextBoxColumn US1840;
        private DataGridViewTextBoxColumn US1850;
        private DataGridViewTextBoxColumn US1860;
        private DataGridViewTextBoxColumn US1870;
        private DataGridViewTextBoxColumn US1880;
        private DataGridViewTextBoxColumn US1890;
        private DataGridViewTextBoxColumn US1900;
        private DataGridViewTextBoxColumn US1910;
        private DataGridViewTextBoxColumn US1920;
        private DataGridViewTextBoxColumn US1930;
        private DataGridViewTextBoxColumn US1940;
        private DataGridViewTextBoxColumn US1950;
        private DataGridViewTextBoxColumn Can1851;
        private DataGridViewTextBoxColumn Can1861;
        private DataGridViewTextBoxColumn Can1871;
        private DataGridViewTextBoxColumn Can1881;
        private DataGridViewTextBoxColumn Can1891;
        private DataGridViewTextBoxColumn Can1901;
        private DataGridViewTextBoxColumn Can1906;
        private DataGridViewTextBoxColumn Can1911;
        private DataGridViewTextBoxColumn Can1916;
        private DataGridViewTextBoxColumn Can1921;
        private DataGridViewTextBoxColumn Ire1901;
        private DataGridViewTextBoxColumn Ire1911;
        private DataGridViewTextBoxColumn Ire1926;
        private DataGridViewTextBoxColumn BirthDate;
        private DataGridViewTextBoxColumn BirthLocation;
        private DataGridViewTextBoxColumn DeathDate;
        private DataGridViewTextBoxColumn DeathLocation;
        private DataGridViewTextBoxColumn BestLocation;
        private DataGridViewTextBoxColumn Ahnentafel;
    }
}