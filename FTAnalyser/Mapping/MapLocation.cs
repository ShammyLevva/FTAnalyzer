using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
{
    public class MapLocation
    {
        public Individual Individual { get; private set;}
        public Fact Fact { get; private set; }
        public FactLocation Location { get; private set; }

        public MapLocation(Individual ind, Fact fact) : this(ind, fact, fact.Location) { }

        public MapLocation(Individual ind, Fact fact, FactLocation loc)
        {
            this.Individual = ind;
            this.Fact = fact;
            this.Location = loc;
        }
        

        public FeatureDataRow GetFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLocation"] = this;
            r["Label"] = Individual.Name + " at " + Location;
            r.Geometry = new NetTopologySuite.Geometries.Point(Location.Longitude, Location.Latitude);
            return r;
        }

        public FactDate FactDate { get { return Fact.FactDate; } }
    }
}


