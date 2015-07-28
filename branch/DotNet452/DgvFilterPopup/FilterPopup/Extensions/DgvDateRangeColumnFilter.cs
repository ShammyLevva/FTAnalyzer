using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup {

    /// <summary>
    /// An extended <i>column filter</i> implementation allowing filters on date ranges.
    /// </summary>
    public partial class DgvDateRangeColumnFilter : DgvBaseColumnFilter {


        /// <summary>
        /// Initializes a new instance of the <see cref="DgvDateRangeColumnFilter"/> class.
        /// </summary>
        public DgvDateRangeColumnFilter() {
            InitializeComponent();
            comboBoxOperator.SelectedValueChanged += new EventHandler(onFilterChanged);
            dateTimePickerValue.TextChanged += new EventHandler(onFilterChanged);
            dateTimePickerValue2.TextChanged += new EventHandler(onFilterChanged);
        }

        /// <summary>
        /// Gets the ComboBox control containing the available operators.
        /// </summary>
        public ComboBox ComboBoxOperator { get { return comboBoxOperator; }}

        
        /// <summary>
        /// Gets the DateTimePicker control containing the date value.
        /// </summary>
        public DateTimePicker DateTimePickerValue { get { return dateTimePickerValue; }}

        /// <summary>
        /// Gets the DateTimePicker control containing the second date value.
        /// </summary>
        public DateTimePicker DateTimePickerValue2 { get { return dateTimePickerValue2; }}

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
            comboBoxOperator.Items.AddRange (new object[] { "[...]","=", "<>", ">", "<", "<=", ">=", "= Ø", "<> Ø" });
            comboBoxOperator.SelectedIndex = 0;
            this.FilterHost.RegisterComboBox(comboBoxOperator);
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
        protected override void OnFilterExpressionBuilding(object sender, CancelEventArgs e){
         	base.OnFilterExpressionBuilding(sender, e);
            if (e.Cancel) {
                FilterManager.RebuildFilter();
                return;
            } 

            string ResultFilterExpression = "";
            string ResultFilterCaption = OriginalDataGridViewColumnHeaderText ;

            // Managing the NULL and NOT NULL cases which are type-independent
            if (comboBoxOperator.Text == "= Ø") ResultFilterExpression = GetNullCondition(this.DataGridViewColumn.DataPropertyName);
            if (comboBoxOperator.Text == "<> Ø") ResultFilterExpression = GetNotNullCondition(this.DataGridViewColumn.DataPropertyName);

            if (ResultFilterExpression != "") {
                FilterExpression = ResultFilterExpression;
                FilterCaption = ResultFilterCaption + "\n " + comboBoxOperator.Text;
                FilterManager.RebuildFilter();
                return;
            }

            string FormattedValue = "";
            string FormattedValueUntil = "";
            
            FormattedValue = FormatValue(dateTimePickerValue.Value, this.ColumnDataType);
            FormattedValueUntil = FormatValue(dateTimePickerValue2.Value.AddDays(1), this.ColumnDataType);

            if (comboBoxOperator.Text == "[...]") {
                FilterExpression = this.DataGridViewColumn.DataPropertyName + ">=" + FormattedValue
                                   + " AND " + this.DataGridViewColumn.DataPropertyName + "<" + FormattedValueUntil;

                FilterCaption = OriginalDataGridViewColumnHeaderText +  "\n = [" + dateTimePickerValue.Text + " , "+ dateTimePickerValue2.Text + "]";
            }
            else { 
                FilterExpression = this.DataGridViewColumn.DataPropertyName + " " + comboBoxOperator.Text + FormattedValue;
                FilterCaption = OriginalDataGridViewColumnHeaderText  + "\n" + comboBoxOperator.Text + " " + dateTimePickerValue.Text ;
            }

            FilterManager.RebuildFilter();
        }

        private void onFilterChanged(object sender, EventArgs e){
            if (sender == comboBoxOperator) { 
                dateTimePickerValue2.Visible = (comboBoxOperator.Text == "[...]");
            }
            if (!FilterApplySoon || !this.Visible) return;
            Active = true;
            FilterExpressionBuild();
        }
    }
}
