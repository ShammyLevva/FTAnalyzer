using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class RelationFilter<T> : Filter<T> where T: IRelationFilterable
    {

        private int filterType;
        
        public RelationFilter(int filterType) {
            this.filterType = filterType;
        }
        
        public bool select (T t) {
            return t.RelationType == filterType;
        }
    }
}