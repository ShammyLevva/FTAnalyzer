using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer
{
    public class MapLocation
    {
        public Individual Individual { get; private set;}
        public FactLocation Location { get; private set; }
        public FactDate FactDate { get; private set; }
        public Point Point { get; private set; }
        public bool DrawPoint { get; set; }

        public MapLocation(Individual ind, FactLocation loc, FactDate date)
        {
            this.Individual = ind;
            this.Location = loc;
            this.FactDate = date;
            this.Point = new Point(Location.Latitude, Location.Longitude);
            this.DrawPoint = true;
        }

        public FeatureDataRow GetFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["Location"] = Location;
            r["Individual"] = Individual;
            r["Relation"] = Individual.RelationType;
            r["Cluster"] = MapCluster.FEATURE;
            r["Label"] = Individual.Name + " at " + Location;
            r.Geometry = new NetTopologySuite.Geometries.Point(Location.Longitude, Location.Latitude);
            return r;
        }
    }
}

