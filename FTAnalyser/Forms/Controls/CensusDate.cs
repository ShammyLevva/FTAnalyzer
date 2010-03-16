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
    public partial class CensusDate : UserControl
    {
        private string country = FactLocation.SCOTLAND;
        private FactDate defaultDate = FactDate.UKCENSUS1881;

        public CensusDate()
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
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1841.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1851.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1861.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1871.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1881.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1891.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1901.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.UKCENSUS1911.DisplayYear);
                    defaultDate = FactDate.UKCENSUS1881;
                } else if (country == FactLocation.USA)
                {
                    cbCensusDate.Items.Add(FactDate.USCENSUS1790.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1800.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1810.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1820.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1830.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1840.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1850.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1860.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1870.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1880.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1890.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1900.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1910.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1920.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.USCENSUS1930.DisplayYear);
                    defaultDate = FactDate.USCENSUS1880;
                } else if (country == FactLocation.CANADA)
                {
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1851.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1861.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1871.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1881.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1891.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1901.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1906.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1911.DisplayYear);
                    cbCensusDate.Items.Add(FactDate.CANADACENSUS1906.DisplayYear);
                    defaultDate = FactDate.CANADACENSUS1881;
              }
            }
        }

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
            cbCensusDate.Text = defaultDate.DisplayYear;
        }
    }
}
