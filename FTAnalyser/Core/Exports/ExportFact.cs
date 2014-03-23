using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ExportFact
    {
        private Individual ind;
        private Fact f;

        public ExportFact(Individual ind, Fact f)
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
            get { return f.FactTypeDescription; }
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
            get { return f.Comment; }
        }

        public string SortableLocation
        {
            get { return f.Location.SortableLocation; }
        }

        public DateTime StartDate
        {
            get { return f.FactDate.StartDate; }
        }

        public DateTime EndDate
        {
            get { return f.FactDate.EndDate; }
        }

        public int RelationType
        {
            get { return ind.RelationType; }
        }
    }
}
