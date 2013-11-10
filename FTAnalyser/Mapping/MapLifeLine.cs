using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpMap.Data;
using NetTopologySuite.Geometries;
using GeoAPI.Geometries;

namespace FTAnalyzer.Mapping
{
    public class MapLifeLine
    {
        private Individual ind;
        private Geometry Geometry { get; set; }

        public MapLifeLine(Individual ind)
        {
            this.ind = ind;
            if (ind.AllGeocodedFacts.Count > 1)
            {
                Coordinate[] points = new Coordinate[ind.AllGeocodedFacts.Count];
                int index = 0;
                foreach (Fact f in ind.AllGeocodedFacts)
                    points[index++] = new Coordinate(f.Location.Longitude, f.Location.Latitude);
                this.Geometry = new NetTopologySuite.Geometries.LineString(points);
            }
            else if (ind.AllGeocodedFacts.Count == 1)
            {
                FactLocation loc = ind.AllGeocodedFacts.First().Location;
                this.Geometry = new NetTopologySuite.Geometries.Point(loc.Longitude, loc.Latitude);
            }
            else
                this.Geometry = null;
        }

        public FeatureDataRow AddFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLifeLine"] = this;
            r["Label"] = ind.Name;
            r.Geometry = Geometry;
            table.AddRow(r);
            return r;
        }

    }
}
