using NetTopologySuite.Geometries;

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
    }
}

