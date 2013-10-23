using NetTopologySuite.Geometries;
using SharpMap.Data;
using System.Drawing;
using GeoAPI.Geometries;

namespace FTAnalyzer.Mapping
{
    public class MapLocation
    {
        public Image Icon { get; private set; }
        public Individual Individual { get; private set; }
        public Fact Fact { get; private set; }
        public FactLocation Location { get; private set; }
        private Geometry Geometry { get; set; }
        private FactDate year;

        private static GeoAPI.Geometries.IPoint centre = new NetTopologySuite.Geometries.Point(0, 0);

        public MapLocation(Individual ind, Fact fact, FactDate year) : this(ind, fact, fact.Location, year) { }

        public MapLocation(Individual ind, Fact fact, FactLocation loc, FactDate year)
        {
            this.Individual = ind;
            this.Fact = fact;
            this.Location = loc;
            this.year = year;
            this.Icon = FactLocationImage.ErrorIcon(loc.GeocodeStatus).Icon;
            this.Geometry = new NetTopologySuite.Geometries.Point(Location.Longitude, Location.Latitude);
        }

        public FeatureDataRow AddFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLocation"] = this;
            r["Label"] = Individual.Name + " at " + Location;
            r.Geometry = Geometry;
            table.AddRow(r);
            return r;
        }

        public void UpdateIcon()
        {
            this.Icon = FactLocationImage.ErrorIcon(Location.GeocodeStatus).Icon;
        }

        public FactDate FactDate { get { return Fact.FactDate; } }

        public string Ind_ID { get { return Individual.Ind_ID; } }

        public string Name { get { return Individual.Name; } }

        public string TypeOfFact { get { return Fact.GetFactTypeDescription(Fact.FactType); } }

        public Age AgeAtFact { get { return Individual == null ? null : Individual.GetAge(year); } }

        public string Relation { get { return Individual.Relation; } }

        public int Ahnentafel { get { return Individual.Ahnentafel; } }

        public double SortDistance { get { return centre.Distance(Geometry); } }
    }
}


