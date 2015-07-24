using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup {
    public partial class DgvMonthYearColumnFilter : DgvBaseColumnFilter {


        private static string mMonthCsvList = "January,February,March,April,May,June,July,August,September,October,November,December";
        private int mYearMin;
        private int mYearMax;


        #region PROPERTIES

        /// <summary>
        /// Gets or sets the month comma separated list. 
        /// </summary>
        /// <value>The month CSV list.</value>
        /// <remarks>
        /// Allows you to set once your culture-specific comma separated list of months. 
        /// </remarks>
        public static string MonthCsvList {
            get { return DgvMonthYearColumnFilter.mMonthCsvList; }
            set { DgvMonthYearColumnFilter.mMonthCsvList = value; }
        }

        /// <summary>
        /// Gets or sets the minimum year shown in the years combo.
        /// </summary>
        /// <value>The year min.</value>
        public int YearMin {
            get { return mYearMin; }
            set { mYearMin = value;
                  SetYearList();
                }
        }

        /// <summary>
        /// Gets or sets the maximum year shown in the years combo.
        /// </summary>
        /// <value>The year min.</value>
        public int YearMax {
            get { return mYearMax; }
            set { mYearMax = value;
                  SetYearList();
            }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="DgvMonthYearColumnFilter"/> class.
        /// </summary>
        public DgvMonthYearColumnFilter() {
            InitializeComponent();
            mYearMin = DateTime.Today.Year - 10;
            mYearMax = DateTime.Today.Year;
            //Month combo initializing
            comboBoxMonth.Items.Add("---");
            comboBoxMonth.Items.AddRange(mMonthCsvList.Split(','));
            comboBoxMonth.SelectedIndex = 0;

            //Year combo initializing
            mYearMin = DateTime.Today.Year - 10;
            mYearMax = DateTime.Today.Year;
            SetYearList();
            
        }

        /// <summary>
        /// Gets the ComboBox control containing the months list.
        /// </summary>
        public ComboBox ComboBoxMonth { get { return comboBoxMonth; } }


        /// <summary>
        /// Gets the ComboBox control containing the years list.
        /// </summary>
        public ComboBox ComboBoxYear { get { return comboBoxYear; } }

        /// <summary>
        /// Perform filter initialitazion and raises the FilterInitializing event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// When this <i>column filter</i> control is added to the <i>column filters</i> array of the <i>filter manager</i>,
        /// the latter calls the <see cref="DgvBaseColumnFilter.Init"/> method which, in turn, calls this method.
        /// You can ovverride this method to provide initialization code or you can create an event handler and 
        /// set the <i>Cancel</i> property of event argument to true, to skip standard initialization.
        /// </remarks>
        protected override void OnFilterInitializing(object sender, CancelEventArgs e) {
            base.OnFilterInitializing(sender, e);
            if (e.Cancel) return;
            comboBoxMonth.SelectedValueChanged += new EventHandler(onFilterChanged);
            comboBoxYear.SelectedValueChanged += new EventHandler(onFilterChanged);
            comboBoxYear.SelectedIndex = comboBoxYear.Items.Count - 1;
            this.FilterHost.RegisterComboBox(comboBoxMonth);
            this.FilterHost.RegisterComboBox(comboBoxYear);
        }

        /// <summary>
        /// Builds the filter expression and raises the FilterExpressionBuilding event
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Override <b>OnFilterExpressionBuilding</b> to provide a filter expression construction
        /// logic and to set the values of the <see cref="DgvBaseColumnFilter.FilterExpression"/> and <see cref="DgvBaseColumnFilter.FilterCaption"/> properties.
        /// The <see cref="DgvFilterManager"/> will use these properties in constructing the whole filter expression and to change the header text of the filtered column.
        /// Otherwise, you can create an event handler and set the <i>Cancel</i> property of event argument to true, to skip standard filter expression building logic.
        /// </remarks>
        protected override void OnFilterExpressionBuilding(object sender, CancelEventArgs e) {
            base.OnFilterExpressionBuilding(sender, e);
            if (e.Cancel) {
                FilterManager.RebuildFilter();
                return;
            }

            DateTime MinDate, MaxDate;

            if (comboBoxMonth.SelectedIndex == 0) {
                MinDate = new DateTime(mYearMin + comboBoxYear.SelectedIndex, 1, 1, 0, 0,0);
                MaxDate = MinDate.AddYears(1);
                FilterCaption = OriginalDataGridViewColumnHeaderText + "\n = " + comboBoxYear.Text;
            }
            else {
                MinDate = new DateTime(mYearMin + comboBoxYear.SelectedIndex, comboBoxMonth.SelectedIndex, 1, 0, 0, 0);
                MaxDate = MinDate.AddMonths(1);
                FilterCaption = OriginalDataGridViewColumnHeaderText + "\n = " + comboBoxMonth.Text+ " " + comboBoxYear.Text;
            }

            FilterExpression = this.DataGridViewColumn.DataPropertyName + " >= " + FormatValue(MinDate, this.ColumnDataType) + " AND " + this.DataGridViewColumn.DataPropertyName + " < " + FormatValue(MaxDate, this.ColumnDataType);
            FilterManager.RebuildFilter();
            return;
        }


        private void onFilterChanged(object sender, EventArgs e) {
            if (!FilterApplySoon || !this.Visible) return;
            Active = true;
            FilterExpressionBuild();
        }


        private void SetYearList() {
            comboBoxYear.Items.Clear();
            for (int y = mYearMin; y <= mYearMax; y++) {
                comboBoxYear.Items.Add(y.ToString());
            }
            comboBoxYear.SelectedIndex = comboBoxYear.Items.Count - 1;
        }
    }
}
