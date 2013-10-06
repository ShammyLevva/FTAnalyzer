using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
{
    public class LocationFeatureDataTable : FeatureDataTable
    {
        public LocationFeatureDataTable()
        {
            this.Columns.Add("Location", typeof(FactLocation));
            this.Columns.Add("Individual", typeof(Individual));
            this.Columns.Add("Relation", typeof(int));
            this.Columns.Add("Cluster", typeof(string));
            this.Columns.Add("Label", typeof(string));
        }
    }
}
