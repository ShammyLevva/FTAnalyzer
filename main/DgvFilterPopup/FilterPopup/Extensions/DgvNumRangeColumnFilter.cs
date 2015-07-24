using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup {

    /// <summary>
    /// An extended <i>column filter</i> implementation allowing filters on numeric ranges.
    /// </summary>
    public partial class DgvNumRangeColumnFilter : DgvBaseColumnFilter {

        /// <summary>
        /// Initializes a new instance of the <see cref="DgvNumRangeColumnFilter"/> class.
        /// </summary>
        public DgvNumRangeColumnFilter() {
            InitializeComponent();
            comboBoxOperator.SelectedValueChanged += new EventHandler(onFilterChanged);
            textBoxValue.TextChanged += new EventHandler(onFilterChanged);
            textBoxValue2.TextChanged += new EventHandler(onFilterChanged);
        }

        /// <summary>
        /// Gets the ComboBox control containing the available operators.
        /// </summary>
        public ComboBox ComboBoxOperator { get { return comboBoxOperator; }}

        /// <summary>
        /// Gets the TextBox control containing the first value.
        /// </summary>
        public TextBox TextBoxValue { get { return textBoxValue; } }


        /// <summary>
        /// Gets the TextBox control containing the second value.
        /// </summary>
        public TextBox TextBoxValue2 { get { return textBoxValue2; } }


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
            comboBoxOperator.Items.AddRange(new object[] { "[...]","=", "<>", ">", "<", "<=", ">=", "= Ø", "<> Ø" });
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

            // Managing the NULL and NOT NULL cases which are type-independent
            if (comboBoxOperator.Text == "= Ø") ResultFilterExpression = GetNullCondition(this.DataGridViewColumn.DataPropertyName);
            if (comboBoxOperator.Text == "<> Ø") ResultFilterExpression = GetNotNullCondition(this.DataGridViewColumn.DataPropertyName);

            if (ResultFilterExpression != "") {
                FilterExpression = ResultFilterExpression;
                FilterCaption = OriginalDataGridViewColumnHeaderText + "\n " + comboBoxOperator.Text;
                FilterManager.RebuildFilter();
                return;
            }
            
            string FormattedValue = FormatValue(textBoxValue.Text, this.ColumnDataType);
            string FormattedValue2 = FormatValue(textBoxValue2.Text, this.ColumnDataType);

            if (comboBoxOperator.Text == "[...]") {
                if (FormattedValue != "" && FormattedValue2 != "" ) {
                    FilterExpression = this.DataGridViewColumn.DataPropertyName + ">=" + FormattedValue
                                       + " AND " + this.DataGridViewColumn.DataPropertyName + "<=" + FormattedValue2;
                    FilterCaption = OriginalDataGridViewColumnHeaderText +  "\n = [" + textBoxValue.Text + " , "+ textBoxValue2.Text  + "]";
                    FilterManager.RebuildFilter();
                }
            } 
            else {
                if (FormattedValue != "") {
                    FilterExpression = this.DataGridViewColumn.DataPropertyName + " " + comboBoxOperator.Text + FormattedValue ;
                    FilterCaption = OriginalDataGridViewColumnHeaderText +  "\n" + comboBoxOperator.Text + " " + textBoxValue.Text;
                    FilterManager.RebuildFilter();
                }
            }
        }

        private void onFilterChanged(object sender, EventArgs e){
            if (sender == comboBoxOperator) { 
                textBoxValue2.Visible = (comboBoxOperator.Text == "[...]");
            }
            if (!FilterApplySoon || !this.Visible) return;
            Active = true;
            FilterExpressionBuild();
        }
    }
}
