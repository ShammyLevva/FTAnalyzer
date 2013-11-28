using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap.Data;

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
            List<Coordinate> points = new List<Coordinate>();
            Coordinate previousPoint = null;
            foreach (IDisplayFact f in ind.AllGeocodedFacts)
            {
                Coordinate point = new Coordinate(f.Location.LongitudeM, f.Location.LatitudeM);
                if(index == 1) 
                    StartPoint = new NetTopologySuite.Geometries.Point(point);
                if(index == ind.AllGeocodedFacts.Count)
                    EndPoint = new NetTopologySuite.Geometries.Point(point);
                index++;
                if (points.Count == 0 || (points.Count > 0 && !point.Equals2D(previousPoint)))
                {
                    points.Add(point); // don't add point if same as last one
                    previousPoint = point;
                }
            }
            if (points.Count > 1)
                this.Geometry = new NetTopologySuite.Geometries.LineString(points.ToArray());
            else
                this.Geometry = StartPoint;
            Count = points.Count;
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
