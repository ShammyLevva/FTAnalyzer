using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public partial class CensusDateSelector : UserControl
    {
        string country = Countries.SCOTLAND;
        private CensusDate defaultDate = CensusDate.UKCENSUS1881;
        private CensusDate previousDate;
        bool _loading;

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

        void AddValuationItems()
        {
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1855);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1865);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1875);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1885);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1895);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1905);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1915);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1920);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1925);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1930);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1935);
            cbCensusDate.Items.Add(CensusDate.SCOTVALUATION1940);
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

        void AddCensusItems(string location)
        {
            if (location.Equals(Countries.UNITED_KINGDOM))
            {
                foreach (CensusDate censusDate in CensusDate.UK_CENSUS)
                    cbCensusDate.Items.Add(censusDate);
            }
            else if (location.Equals(Countries.IRELAND))
            {
                cbCensusDate.Items.Add(CensusDate.IRELANDCENSUS1901);
                cbCensusDate.Items.Add(CensusDate.IRELANDCENSUS1911);
            }
            else if (location.Equals(Countries.UNITED_STATES))
            {
                foreach (CensusDate censusDate in CensusDate.US_FEDERAL_CENSUS)
                    cbCensusDate.Items.Add(censusDate);
            }
            else if (location.Equals(Countries.CANADA))
            {
                foreach(CensusDate censusDate in CensusDate.CANADIAN_CENSUS)
                    cbCensusDate.Items.Add(censusDate);
            }
        }

        #region Properties

        public CensusDate SelectedDate => (CensusDate)cbCensusDate.SelectedItem;

        public FactDate DefaultDate => defaultDate;

        public string CensusCountry => SelectedDate.Country;
        #endregion

        public void RevertToDefaultDate() => cbCensusDate.SelectedItem = defaultDate;

        void SetControlWidth()
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
            try
            {
                g.Dispose();
            }
            catch (Exception) { }
        }

        public event EventHandler CensusChanged;

        protected void OnCensusChanged(EventArgs e)
        {
            if (CensusChanged != null)
            {
                CensusChanged(this, e);
                previousDate = (CensusDate)cbCensusDate.SelectedItem;
            }
        }

        void CbCensusDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
                OnCensusChanged(e);
        }
    }
}
