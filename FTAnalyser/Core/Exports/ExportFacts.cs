using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ExportFacts
    {
        private Individual ind;
        private Fact f;

        public ExportFacts(Individual ind, Fact f)
        {
            this.ind = ind;
            this.f = f;
        }

        public string ID
        {
            get { return ind.IndividualID; }
        }

        public string Forenames
        {
            get { return ind.Forenames; }
        }

        public string Surname
        {
            get { return ind.Surname; }
        }

        public string Gender
        {
            get { return ind.Gender; }
        }

        public string FactType
        {
            get { return f.FactType.ToString(); }
        }

        public string FactDate
        {
            get { return f.FactDate.ToString(); }
        }

        public string FactLocation
        {
            get { return f.Location.ToString(); }
        }

        public string FactComment
        {
            get { return f.Comment.ToString(); }
        }

        public string SortableLocation
        {
            get { return f.Location.SortableLocation; }
        }
    }
}
