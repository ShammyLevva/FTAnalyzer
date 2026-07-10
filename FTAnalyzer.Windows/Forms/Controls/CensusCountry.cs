using FTAnalyzer.Filters;
using System.ComponentModel;

namespace FTAnalyzer.Forms.Controls
{
    public partial class CensusCountry : UserControl
    {
        public CensusCountry()
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            groupBox1.Text = "Census Country";
            // Each radio button is AutoSize and grows wider at larger font levels, but they were laid
            // out at fixed absolute positions - reposition off each other's actual rendered edges
            // (same pattern as CensusDateSelector) whenever the row-1 or row-2 anchor buttons resize,
            // instead of squishing together at their original fixed coordinates.
            // Subscribe all six (not just the row-starting ones): rbUK/rbUSA don't need to push a
            // sibling after them, but their own growth still needs to widen groupBox1 - missing that
            // left the container too narrow to hold them, clipping their text at its right edge even
            // though their gap from the previous sibling was itself correct.
            rbScotland.SizeChanged += (_, _) => RepositionRadioButtons();
            rbEngland.SizeChanged += (_, _) => RepositionRadioButtons();
            rbWales.SizeChanged += (_, _) => RepositionRadioButtons();
            rbUK.SizeChanged += (_, _) => RepositionRadioButtons();
            rbCanada.SizeChanged += (_, _) => RepositionRadioButtons();
            rbUSA.SizeChanged += (_, _) => RepositionRadioButtons();
        }

        void RepositionRadioButtons()
        {
            const int gap = 15;
            rbEngland.Left = rbScotland.Right + gap;
            rbWales.Left = rbEngland.Right + gap;
            rbUK.Left = rbWales.Right + gap;
            rbUSA.Left = rbCanada.Right + gap;
            int row2Top = rbScotland.Bottom + 7;
            rbCanada.Top = row2Top;
            rbUSA.Top = row2Top;
            groupBox1.Width = Math.Max(rbUK.Right, rbUSA.Right) + 10;
            groupBox1.Height = rbUSA.Bottom + 10;
            Size = groupBox1.Size;
        }

        public bool Scotland { get { return rbScotland.Checked; } }
        public bool England { get { return rbEngland.Checked; } }
        public bool Wales { get { return rbWales.Checked; } }
        public bool UK { get { return rbUK.Checked; } }
        public bool Canada { get { return rbCanada.Checked; } }
        public bool USA { get { return rbUSA.Checked; } }

        [DefaultValue(false)]
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

        [DefaultValue("Census Country")]
        public string Title
        {
            get { return groupBox1.Text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    groupBox1.Text = value;
            }
        }

        public Predicate<T> BuildFilter<T>(FactDate when, Func<FactDate, T, FactLocation> location)
        {
            Predicate<T> locationFilter = FilterUtils.TrueFilter<T>();

            static string country(FactLocation x) => x.Country;
            if (Scotland)
                locationFilter = FilterUtils.LocationFilter(when, location, country, Countries.SCOTLAND);
            else if (England)
                locationFilter = FilterUtils.LocationFilter(when, location, country, Countries.ENGLAND);
            else if (Wales)
                locationFilter = FilterUtils.LocationFilter(when, location, country, Countries.WALES);
            else if (UK)
                locationFilter = FilterUtils.OrFilter(FilterUtils.LocationFilter(when, location, country, Countries.SCOTLAND),
                                        FilterUtils.LocationFilter(when, location, country, Countries.ENGLAND),
                                        FilterUtils.LocationFilter(when, location, country, Countries.WALES));
            else if (Canada)
                locationFilter = FilterUtils.LocationFilter(when, location, country, Countries.CANADA);
            else if (USA)
                locationFilter = FilterUtils.LocationFilter(when, location, country, Countries.UNITED_STATES);
            return locationFilter;
        }

        public FactLocation? GetLocation
        {
            get
            {

                if (Scotland)
                    return FactLocation.GetLocation(Countries.SCOTLAND);
                else if (England)
                    return FactLocation.GetLocation(Countries.ENGLAND);
                else if (Wales)
                    return FactLocation.GetLocation(Countries.WALES);
                else if (UK)
                    return FactLocation.GetLocation(Countries.UNITED_KINGDOM);
                else if (Canada)
                    return FactLocation.GetLocation(Countries.CANADA);
                else if (USA)
                    return FactLocation.GetLocation(Countries.UNITED_STATES);
                else
                    return null;
            }
        }

        public event EventHandler? CountryChanged;

        protected void OnCountryChanged(EventArgs e) => CountryChanged?.Invoke(this, e);

        void RbScotland_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);

        void RbEngland_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);

        void RbWales_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);

        void RbUK_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);

        void RbCanada_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);

        void RbUSA_CheckedChanged(object sender, EventArgs e) => OnCountryChanged(e);
    }
}
