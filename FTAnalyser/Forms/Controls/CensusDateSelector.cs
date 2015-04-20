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
        private string country = Countries.SCOTLAND;
        private CensusDate defaultDate = CensusDate.UKCENSUS1881;
        private CensusDate previousDate;
        private bool _loading = false;

        public CensusDateSelector()
        {
            InitializeComponent();
            cbCensusDate.Items.Clear();
            previousDate = defaultDate;
        }

        public void AddAllCensusItems()
        {
            cbCensusDate.Items.Clear();
            AddCensusItems(Countries.UNITED_KINGDOM);
            AddCensusItems(Countries.IRELAND);
            AddCensusItems(Countries.UNITED_STATES);
            AddCensusItems(Countries.CANADA);
            AddValuationItems();
            defaultDate = CensusDate.UKCENSUS1881;
            previousDate = defaultDate;
            RevertToDefaultDate();
            SetControlWidth();
        }

        private void AddValuationItems()
        {
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1865);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1875);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1885);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1895);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1905);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1915);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1920);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1925);
        }

        public void AddLCCensusItems()
        {
            cbCensusDate.Items.Clear();
            cbCensusDate.Items.Add(CensusDate.UKCENSUS1841);
            cbCensusDate.Items.Add(CensusDate.UKCENSUS1881);
            cbCensusDate.Items.Add(CensusDate.UKCENSUS1911);
            cbCensusDate.Items.Add(CensusDate.USCENSUS1880);
            cbCensusDate.Items.Add(CensusDate.CANADACENSUS1881);
            cbCensusDate.Items.Add(CensusDate.IRELANDCENSUS1911);
            previousDate = defaultDate;
            RevertToDefaultDate();
            SetControlWidth();
        }

        public string Country
        {
            get { return country; }
            set
            {
                _loading = true;
                country = value;
                cbCensusDate.Items.Clear();
                if (Countries.IsUnitedKingdom(country))
                {
                    defaultDate = (previousDate.Country == Countries.UNITED_KINGDOM) ? previousDate : CensusDate.UKCENSUS1881;
                }
                else if (country == Countries.UNITED_STATES)
                {
                    defaultDate = (previousDate.Country == Countries.UNITED_STATES) ? previousDate : CensusDate.USCENSUS1880;
                }
                else if (country == Countries.CANADA)
                {
                    defaultDate = (previousDate.Country == Countries.CANADA) ? previousDate : CensusDate.CANADACENSUS1881;
                }
                AddCensusItems(defaultDate.Country);
                SetControlWidth();
                _loading = false;
                cbCensusDate.Text = defaultDate.ToString();
                previousDate = defaultDate;
            }
        }

        private void AddCensusItems(string location)
        {
            if (location.Equals(Countries.UNITED_KINGDOM))
            {
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1841);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1851);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1861);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1871);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1881);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1891);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1901);
                cbCensusDate.Items.Add(CensusDate.UKCENSUS1911);
            }
            else if (location.Equals(Countries.IRELAND))
            {
                cbCensusDate.Items.Add(CensusDate.IRELANDCENSUS1901);
                cbCensusDate.Items.Add(CensusDate.IRELANDCENSUS1911);
            }
            else if (location.Equals(Countries.UNITED_STATES))
            {
                cbCensusDate.Items.Add(CensusDate.USCENSUS1790);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1800);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1810);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1820);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1830);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1840);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1850);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1860);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1870);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1880);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1890);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1900);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1910);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1920);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1930);
                cbCensusDate.Items.Add(CensusDate.USCENSUS1940);
            }
            else if (location.Equals(Countries.CANADA))
            {
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1851);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1861);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1871);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1881);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1891);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1901);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1906);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1911);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1916);
                cbCensusDate.Items.Add(CensusDate.CANADACENSUS1921);
            }
        }

        #region Properties

        public CensusDate SelectedDate
        {
            get { return (CensusDate)cbCensusDate.SelectedItem; }
        }

        public FactDate DefaultDate
        {
            get { return defaultDate; }
        }

        public string CensusCountry
        {
            get { return SelectedDate.Country; }
        }
        #endregion

        public void RevertToDefaultDate()
        {
            cbCensusDate.SelectedItem = defaultDate;
        }

        private void SetControlWidth()
        {
            cbCensusDate.Width = 10;
            Graphics g = cbCensusDate.CreateGraphics();
            foreach (CensusDate cd in cbCensusDate.Items)
            {
                // use s + "xxx" to add bit extra for drop down icon
                float itemWidth = g.MeasureString(cd.ToString() + "xxx", cbCensusDate.Font).Width;
                if (itemWidth > cbCensusDate.Width)
                    cbCensusDate.Width = (int)itemWidth;
            }
            g.Dispose();
        }

        public event EventHandler CensusChanged;

        protected void OnCensusChanged(EventArgs e)
        {
            if (CensusChanged != null)
            {
                CensusChanged(this, e);
                this.previousDate = (CensusDate)cbCensusDate.SelectedItem;
            }
        }

        private void cbCensusDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
                OnCensusChanged(e);
        }


    }
}
