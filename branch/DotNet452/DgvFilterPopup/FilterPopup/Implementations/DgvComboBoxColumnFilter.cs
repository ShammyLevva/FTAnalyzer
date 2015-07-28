using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup {


    /// <summary>
    /// A standard <i>column filter</i> implementation for ComboBox columns.
    /// </summary>
    /// <remarks>
    /// If the <b>DataGridView</b> column to which this <i>column filter</i> is applied
    /// is not a ComboBox column, it automatically creates a distinct list of values from the bound <b>DataView</b> column.
    /// If the DataView changes, you should do an explicit call to <see cref="DgvComboBoxColumnFilter.RefreshValues"/> method.
    /// </remarks>
    public partial class DgvComboBoxColumnFilter : DgvBaseColumnFilter {

        /// <summary>
        /// Initializes a new instance of the <see cref="DgvComboBoxColumnFilter"/> class.
        /// </summary>
        public DgvComboBoxColumnFilter() {
            InitializeComponent();
            comboBoxOperator.SelectedValueChanged += new EventHandler(onFilterChanged);
            comboBoxValue.SelectedValueChanged += new EventHandler(onFilterChanged);
        }

        /// <summary>
        /// Gets the ComboBox control containing the available operators.
        /// </summary>
        public ComboBox ComboBoxOperator { get { return comboBoxOperator; }}

        
        /// <summary>
        /// Gets the ComboBox control containing the available values.
        /// </summary>
        public ComboBox ComboBoxValue { get { return comboBoxValue; }}



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
            comboBoxOperator.Items.AddRange (new object[] { "=", "<>", "= Ø", "<> Ø" });
            comboBoxOperator.SelectedIndex = 0;
            if (this.DataGridViewColumn is DataGridViewComboBoxColumn) {
                comboBoxValue.ValueMember = ((DataGridViewComboBoxColumn)DataGridViewColumn).ValueMember;
                comboBoxValue.DisplayMember = ((DataGridViewComboBoxColumn)DataGridViewColumn).DisplayMember;
                comboBoxValue.DataSource = ((DataGridViewComboBoxColumn)DataGridViewColumn).DataSource;
            }
            else {
                comboBoxValue.ValueMember = this.DataGridViewColumn.DataPropertyName;
                comboBoxValue.DisplayMember = this.DataGridViewColumn.DataPropertyName;
                RefreshValues();
            }
            this.FilterHost.RegisterComboBox(comboBoxOperator);
            this.FilterHost.RegisterComboBox(comboBoxValue);
        }

        /// <summary>
        /// For non-combobox columns, refreshes the list of the <b>DataView</b> values in the column.
        /// </summary>
        public void RefreshValues() {
            if (!(this.DataGridViewColumn is DataGridViewComboBoxColumn)) {
                DataTable DistinctDataTable = this.BoundDataView.ToTable(true, new string[] { this.DataGridViewColumn.DataPropertyName });
                DistinctDataTable.DefaultView.Sort = this.DataGridViewColumn.DataPropertyName;
                comboBoxValue.DataSource = DistinctDataTable;
            }
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

            object FilterValue = comboBoxValue.SelectedValue;
            string FormattedValue = "";
            
            if (ColumnDataType == typeof(string)) {
                // Managing the string-column case
                string EscapedFilterValue = StringEscape(FilterValue.ToString());
                ResultFilterExpression = this.DataGridViewColumn.DataPropertyName + " " + comboBoxOperator.Text + "'" + EscapedFilterValue + "'";
                ResultFilterCaption += "\n" + comboBoxOperator.Text + " " + comboBoxValue.Text;
            }
            else {
                // Managing the other cases
                FormattedValue = FormatValue(FilterValue, this.ColumnDataType);
                if (FormattedValue != "") {
                    ResultFilterExpression = this.DataGridViewColumn.DataPropertyName + " " + comboBoxOperator.Text + FormattedValue ;
                    ResultFilterCaption += "\n" + comboBoxOperator.Text + " " + comboBoxValue.Text ;
                }

            }
            if (ResultFilterExpression != "") {
                FilterExpression = ResultFilterExpression;
                FilterCaption = ResultFilterCaption;
                FilterManager.RebuildFilter();
            }
        }

        private void onFilterChanged(object sender, EventArgs e){
            if (!FilterApplySoon || !this.Visible) return;
            Active = true;
            FilterExpressionBuild();

        }





    }
}
