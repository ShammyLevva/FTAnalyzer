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
        private Geometry StartPoint { get; set; }
        private Geometry EndPoint { get; set; }
        private int Count { get; set; }

        public MapLifeLine(Individual ind)
        {
            this.ind = ind;
            int index = 1;
            Count = 0;
            Coordinate[] points = new Coordinate[ind.AllGeocodedFacts.Count];
            foreach (IDisplayFact f in ind.AllGeocodedFacts)
            {
                Coordinate c = new Coordinate(f.Location.Longitude, f.Location.Latitude);
                if(index == 1) 
                    StartPoint = new NetTopologySuite.Geometries.Point(c);
                if(index == ind.AllGeocodedFacts.Count)
                    EndPoint = new NetTopologySuite.Geometries.Point(c);
                index++;
                if (Count == 0 || (Count > 0 && !points[Count - 1].Equals2D(c)))
                    points[Count++] = c; // don't add point if same as last one
            }
            if (Count > 1)
                this.Geometry = new NetTopologySuite.Geometries.LineString(points);
            else
                this.Geometry = StartPoint;
        }

        public FeatureDataRow AddFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLifeLine"] = this;
            r["StartPoint"] = false;
            r["EndPoint"] = false;
            r["Label"] = ind.Name;
            r.Geometry = Geometry;
            table.AddRow(r);

            r = table.NewRow();
            r["MapLifeLine"] = this;
            r["StartPoint"] = true;
            r["EndPoint"] = false;
            if(Count < 2)
                r["Label"] = ind.Name;
            r.Geometry = StartPoint;
            table.AddRow(r);

            r = table.NewRow();
            r["MapLifeLine"] = this;
            r["StartPoint"] = false;
            r["EndPoint"] = true;
            r.Geometry = EndPoint;
            table.AddRow(r);

            return r;
        }

    }
}
