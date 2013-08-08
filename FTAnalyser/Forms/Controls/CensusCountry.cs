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
                if (Scotland) return FactLocation.SCOTLAND;
                if (England) return FactLocation.ENGLAND;
                if (Wales) return FactLocation.WALES;
                if (UK) return FactLocation.UNITED_KINGDOM;
                if (Canada) return FactLocation.CANADA;
                if (USA) return FactLocation.UNITED_STATES;
                return FactLocation.ENGLAND;
            }
        }

        public string Title {
            get { return groupBox1.Text; }
            set { 
                if(value != string.Empty)
                    groupBox1.Text = value; 
            } 
        }

        public Filter<T> BuildFilter<T>() where T: ILocationFilterable, IFactsFilterable
        {
            Filter<T> locationFilter = new TrueFilter<T>();
            if (Scotland)
                locationFilter = LocationFilter<T>.SCOTLAND;
            else if (England)
                locationFilter = LocationFilter<T>.ENGLAND;
            else if (Wales)
                locationFilter = LocationFilter<T>.WALES;
            else if (UK)
                locationFilter = new OrFilter<T>(LocationFilter<T>.SCOTLAND, LocationFilter<T>.ENGLAND, LocationFilter<T>.WALES);
            else if (Canada)
                locationFilter = LocationFilter<T>.CANADA;
            else if (USA)
                locationFilter = LocationFilter<T>.USA;
            return locationFilter;
        }

        public FactLocation GetLocation
        {
            get {

                if (Scotland)
                    return new FactLocation(FactLocation.SCOTLAND);
                else if (England)
                    return new FactLocation(FactLocation.ENGLAND);
                else if (Wales)
                    return new FactLocation(FactLocation.WALES);
                else if (UK)
                    return new FactLocation(FactLocation.UNITED_KINGDOM);
                else if (Canada)
                    return new FactLocation(FactLocation.CANADA);
                else if (USA)
                    return new FactLocation(FactLocation.UNITED_STATES);
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
