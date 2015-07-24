using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Globalization;


namespace DgvFilterPopup {

    /// <summary>
    /// Is the standard implementation of DgvBaseFilterHost
    /// </summary>
    [ToolboxItem(false)]
    public partial class DgvFilterHost : DgvBaseFilterHost {

        /// <summary>
        /// Initializes a new instance of the <see cref="DgvFilterHost"/> class.
        /// </summary>
        public DgvFilterHost() {
            InitializeComponent();
            this.CurrentColumnFilterChanged += new EventHandler(DgvFilterHost_CurrentColumnFilterChanged);
        }

        void DgvFilterHost_CurrentColumnFilterChanged(object sender, EventArgs e) {
            lblColumnName.Text = CurrentColumnFilter.OriginalDataGridViewColumnHeaderText;
        }

        /// <summary>
        /// Return the effective area to which the <i>column filters</i> will be added.
        /// </summary>
        /// <value></value>
        public override Control FilterClientArea {
            get {
                return this.panelFilterArea;
            }
        }

        private void tsOK_Click(object sender, EventArgs e) {
            FilterManager.ActivateFilter(true);
            this.Popup.Close();
        }

        private void tsRemove_Click(object sender, EventArgs e) {
            FilterManager.ActivateFilter(false);
            this.Popup.Close();
        }

        private void tsRemoveAll_Click(object sender, EventArgs e) {
            FilterManager.ActivateAllFilters(false);
            this.Popup.Close();
        }


    }
}
