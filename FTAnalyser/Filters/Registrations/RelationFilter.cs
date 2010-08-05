using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class RelationFilter : Filter<Registration>
    {

        private int filterType;
        
        public RelationFilter(int filterType) {
            this.filterType = filterType;
        }
        
        public bool select (Registration r) {
            return r.Relation == filterType;
        }
    }
}