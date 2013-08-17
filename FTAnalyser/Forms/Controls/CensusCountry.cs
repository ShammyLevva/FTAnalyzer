using System;
using System.Windows.Forms;
using FTAnalyzer;
using FTAnalyzer.Filters;

namespace Controls
{
    public partial class CensusCountry : UserControl
    {
        public CensusCountry()
        {
            InitializeComponent();
            groupBox1.Text = "Census Country";
        }

        public bool Scotland { get { return rbScotland.Checked; } }
        public bool England { get { return rbEngland.Checked; } }
        public bool Wales { get { return rbWales.Checked; } }
        public bool UK { get { return rbUK.Checked; } }
        public bool Canada { get { return rbCanada.Checked; } }
        public bool USA { get { return rbUSA.Checked; } }

        public bool UKEnabled
        {
            get { return rbUK.Enabled; }
            set { rbUK.Enabled = value; }
        }

        public string Country
        {
            get
            {
                if (Scotland) return Countries.SCOTLAND;
                if (England) return Countries.ENGLAND;
                if (Wales) return Countries.WALES;
                if (UK) return Countries.UNITED_KINGDOM;
                if (Canada) return Countries.CANADA;
                if (USA) return Countries.UNITED_STATES;
                return Countries.ENGLAND;
            }
        }

        public string Title {
            get { return groupBox1.Text; }
            set { 
                if(value != string.Empty)
                    groupBox1.Text = value; 
            } 
        }

        public Func<FactDate, Func<T, bool>> BuildFilter<T>(Func<FactDate, T, FactLocation> location)
        {
            Func<FactDate, Func<T, bool>> locationFilter = d => FilterUtils.TrueFilter<T>();
            Func<FactLocation, string> country = x => x.Country;
            if (Scotland)
                locationFilter = FilterUtils.LocationFilter<T>(location, country, Countries.SCOTLAND);
            else if (England)
                locationFilter = FilterUtils.LocationFilter<T>(location, country, Countries.ENGLAND);
            else if (Wales)
                locationFilter = FilterUtils.LocationFilter<T>(location, country, Countries.WALES);
            else if (UK)
                locationFilter = FilterUtils.OrFilter<FactDate, T>(FilterUtils.LocationFilter<T>(location, country, Countries.SCOTLAND),
                                        FilterUtils.LocationFilter<T>(location, country, Countries.ENGLAND),
                                        FilterUtils.LocationFilter<T>(location, country, Countries.WALES));
            else if (Canada)
                locationFilter = FilterUtils.LocationFilter<T>(location, country, Countries.CANADA);
            else if (USA)
                locationFilter = FilterUtils.LocationFilter<T>(location, country, Countries.UNITED_STATES);
            return locationFilter;
        }

        public FactLocation GetLocation
        {
            get {

                if (Scotland)
                    return new FactLocation(Countries.SCOTLAND);
                else if (England)
                    return new FactLocation(Countries.ENGLAND);
                else if (Wales)
                    return new FactLocation(Countries.WALES);
                else if (UK)
                    return new FactLocation(Countries.UNITED_KINGDOM);
                else if (Canada)
                    return new FactLocation(Countries.CANADA);
                else if (USA)
                    return new FactLocation(Countries.UNITED_STATES);
                else
                    return null;
            }
       }

        public event EventHandler CountryChanged;

        protected void OnCountryChanged(EventArgs e)
        {
            if (CountryChanged != null)
                CountryChanged(this, e);
        }

        private void rbScotland_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }

        private void rbEngland_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }

        private void rbWales_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }

        private void rbUK_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }

        private void rbCanada_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }

        private void rbUSA_CheckedChanged(object sender, EventArgs e)
        {
            OnCountryChanged(e);
        }
    }
}
