using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ParishBatch
    {

        public string Batch { get; private set; }
        public string ParishID { get; private set; }
        public Parish Parish { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        private string comments;

        public ParishBatch(String batch, String parishID)
        {
            this.Batch = batch;
            this.Parish = null;
            this.ParishID = parishID;
            this.StartYear = "";
            this.EndYear = "";
            this.comments = "";
        }

        #region Properties

        public string Comments
        {
            get { return comments == null ? "" : "(" + comments + ")"; }
            set { this.comments = value; }
        }

        #endregion
    }
}