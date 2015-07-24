using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup {



    /// <summary>
    /// Represents the method that will handle an event related to a column filter.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ColumnFilterEventArgs"/> instance containing the event data.</param>
    public delegate void ColumnFilterEventHandler(object sender, ColumnFilterEventArgs e);


    /// <summary>
    /// Provides data for a column filter event. 
    /// </summary>
    public class ColumnFilterEventArgs : EventArgs {


        #region PRIVATE FIELDS
                
        private DataGridViewColumn mColumn;
        private DgvBaseColumnFilter mColumnFilter;
        private bool mHandled;


        #endregion 
        
        
        #region PROPERTIES 


        /// <summary>
        /// Gets the DataGridView column involved in the event.
        /// </summary>
        public DataGridViewColumn Column { get { return mColumn; }}


        /// <summary>
        /// Gets or sets the column filter instance.
        /// </summary>
        /// <value>A column filter instance.</value>
        public DgvBaseColumnFilter ColumnFilter {
            get { return mColumnFilter; }
            set { mColumnFilter = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnFilterEventArgs"/> is handled.
        /// </summary>
        /// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
        public bool Handled {
            get { return mHandled; }
            set { mHandled = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFilterEventArgs"/> class.
        /// </summary>
        /// <param name="Column">The DstaGridView column.</param>
        /// <param name="ColumnFilter">The column filter instance.</param>
        public ColumnFilterEventArgs(DataGridViewColumn Column, DgvBaseColumnFilter ColumnFilter) {
            this.mColumn = Column;
            this.mColumnFilter = ColumnFilter;
            this.mHandled = Handled;
        }
    }
}
