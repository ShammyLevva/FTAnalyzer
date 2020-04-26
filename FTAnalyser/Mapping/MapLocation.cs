using NetTopologySuite.Geometries;
using SharpMap.Data;
using System.Drawing;
using System.Numerics;

namespace FTAnalyzer.Mapping
{
    public class MapLocation
    {
        public Image Icon { get; private set; }
        public Individual Individual { get; private set; }
        public Fact Fact { get; private set; }
        public FactLocation Location { get; private set; }
        public Geometry Geometry { get; private set; }
        public string FoundLocation { get; private set; }

        readonly FactDate _year;

        static readonly NetTopologySuite.Geometries.Point centre = new NetTopologySuite.Geometries.Point(0, 0);

        public MapLocation(Individual ind, Fact fact, FactDate year) : this(ind, fact, fact.Location, year) { }

        public MapLocation(Individual ind, Fact fact, FactLocation loc, FactDate year)
        {
            Individual = ind;
            Fact = fact;
            Location = loc;
            _year = year;
            Icon = FactLocationImage.ErrorIcon(loc.GeocodeStatus).Icon;
            Geometry = new NetTopologySuite.Geometries.Point(Location.LongitudeM, Location.LatitudeM);
            FoundLocation = loc.FoundLocation;
        }

        public FeatureDataRow AddFeatureDataRow(FeatureDataTable table)
        {
            GeoResponse.CResult.CGeometry.CViewPort vp = Location.ViewPort;
            FeatureDataRow r = table.NewRow();
            r["MapLocation"] = this;
            r["ViewPort"] = new Envelope(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
            r["Label"] = Individual.Name + " at " + Location;
            r.Geometry = Geometry;
            table.AddRow(r);
            return r;
        }

        public void UpdateIcon() => Icon = FactLocationImage.ErrorIcon(Location.GeocodeStatus).Icon;

        public FactDate FactDate => Fact.FactDate;

        public string IndividualID => Individual.IndividualID;

        public string Name => Individual.Name;

        public string TypeOfFact => Fact.FactTypeDescription;

        public Age AgeAtFact => Individual?.GetAge(_year);

        public string Relation => Individual.Relation;

        public string RelationToRoot => Individual.RelationToRoot;

        public BigInteger Ahnentafel => Individual.Ahnentafel;

        public double SortDistance => centre.Distance(Geometry);

        public override string ToString() => Individual.IndividualID + ": " + Individual.Name + ", " + Fact.ToString();
    }
}


