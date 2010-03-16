using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer;

namespace Controls
{
    public partial class CensusDateSelector : UserControl
    {
        private string country = FactLocation.SCOTLAND;
        private CensusDate defaultDate = CensusDate.UKCENSUS1881;

        public CensusDateSelector()
        {
            InitializeComponent();
            cbCensusDate.Items.Clear();
        }

        public string Country
        {
            get { return country; }
            set { 
                country = value; 
                cbCensusDate.Items.Clear();
                if (country == FactLocation.SCOTLAND || country == FactLocation.ENGLAND || country == FactLocation.WALES)
                {
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1841.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1851.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1861.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1871.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1881.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1891.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1901.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.UKCENSUS1911.DisplayName);
                    defaultDate = CensusDate.UKCENSUS1881;
                } else if (country == FactLocation.USA)
                {
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1790.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1800.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1810.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1820.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1830.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1840.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1850.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1860.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1870.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1880.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1890.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1900.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1910.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1920.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.USCENSUS1930.DisplayName);
                    defaultDate = CensusDate.USCENSUS1880;
                } else if (country == FactLocation.CANADA)
                {
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1851.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1861.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1871.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1881.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1891.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1901.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1906.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1911.DisplayName);
                    cbCensusDate.Items.Add(CensusDate.CANADACENSUS1906.DisplayName);
                    defaultDate = CensusDate.CANADACENSUS1881;
                }
                cbCensusDate.Text = defaultDate.DisplayName;
                SetControlWidth();
            }
        }

        #region Properties

        public FactDate SelectedDate
        {
            get { return new FactDate(cbCensusDate.Text); }
        }

        public FactDate DefaultDate
        {
            get { return defaultDate; }
        }

        public void RevertToDefaultDate()
        {
            cbCensusDate.Text = defaultDate.DisplayName;
        }
        #endregion


        private void SetControlWidth()
        {
            cbCensusDate.Width = 10;
            Graphics g = cbCensusDate.CreateGraphics();
            foreach(String s in cbCensusDate.Items)
            {
                // use s + "xxx" to add bit extra for drop down icon
                float itemWidth = g.MeasureString(s + "xxx", cbCensusDate.Font).Width;
                if (itemWidth > cbCensusDate.Width)
                    cbCensusDate.Width = (int)itemWidth;
            }
            g.Dispose();
        }
    }
}
