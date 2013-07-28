using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ParishBatch
    {

        private String batch;
        private String parishID;
        private Parish parish;
        private String startYear;
        private String endYear;
        private String comments;

        public ParishBatch(String batch, String parishID)
        {
            this.batch = batch;
            this.parish = null;
            this.parishID = parishID;
            this.startYear = "";
            this.endYear = "";
            this.comments = "";
        }

        #region Properties

        public String Batch
        {
            get { return batch; }
        }

        public Parish Parish
        {
            get { return parish; }
            set { this.parish = value; }
        }

        public String ParishID
        {
            get { return parishID; }
        }

        public String Comments
        {
            get { return comments == null ? "" : "(" + comments + ")"; }
            set { this.comments = value; }
        }

        public String EndYear
        {
            get { return endYear; }
            set { this.endYear = value; }
        }

        public String StartYear
        {
            get { return startYear; }
            set { this.startYear = value; }
        }

        #endregion
    }
}