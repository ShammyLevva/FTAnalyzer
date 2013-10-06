using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
{
    public class MapLocation
    {
        public Individual Individual { get; private set;}
        public FactLocation Location { get; private set; }
        public FactDate FactDate { get; private set; }

        public MapLocation(Individual ind, FactLocation loc, FactDate date)
        {
            this.Individual = ind;
            this.Location = loc;
            this.FactDate = date;
        }

        public FeatureDataRow GetFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLocation"] = this;
            r["Label"] = Individual.Name + " at " + Location;
            r.Geometry = new NetTopologySuite.Geometries.Point(Location.Longitude, Location.Latitude);
            return r;
        }
    }
}

