using System.Drawing;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using System;

namespace FTAnalyzer.Mapping
{
    public class MapLocation
    {
        public Image Icon { get; private set; }
        public Individual Individual { get; private set; }
        public Fact Fact { get; private set; }
        public FactLocation Location { get; private set; }
        public Geometry Geometry { get; private set; }
        private FactDate year;
        public string FoundLocation { get; private set; }

        private static GeoAPI.Geometries.IPoint centre = new NetTopologySuite.Geometries.Point(0, 0);

        public MapLocation(Individual ind, Fact fact, FactDate year) : this(ind, fact, fact.Location, year) { }

        public MapLocation(Individual ind, Fact fact, FactLocation loc, FactDate year)
        {
            this.Individual = ind;
            this.Fact = fact;
            this.Location = loc;
            this.year = year;
            this.Icon = FactLocationImage.ErrorIcon(loc.GeocodeStatus).Icon;
            this.Geometry = new NetTopologySuite.Geometries.Point(Location.LongitudeM, Location.LatitudeM);
            this.FoundLocation = loc.FoundLocation;
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

        public void UpdateIcon()
        {
            this.Icon = FactLocationImage.ErrorIcon(Location.GeocodeStatus).Icon;
        }

        public FactDate FactDate { get { return Fact.FactDate; } }

        public string IndividualID { get { return Individual.IndividualID; } }

        public string Name { get { return Individual.Name; } }

        public string TypeOfFact { get { return Fact.FactTypeDescription; } }

        public Age AgeAtFact { get { return Individual?.GetAge(year); } }

        public string Relation { get { return Individual.Relation; } }

        public string RelationToRoot { get { return Individual.RelationToRoot; } }

        public Int64 Ahnentafel { get { return Individual.Ahnentafel; } }

        public double SortDistance { get { return centre.Distance(Geometry); } }

        public override string ToString()
        {
            return Individual.IndividualID + ": " + Individual.Name + ", " + Fact.ToString();
        }
    }
}


