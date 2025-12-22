using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
{
    public class MapLifeLine
    {
        readonly Individual ind;
        Geometry Geometry { get; set; }
        Geometry StartPoint { get; set; }
        Geometry EndPoint { get; set; }
        int Count { get; set; }
        Envelope Viewport { get; set; }

        public static readonly string LINE = "Line", START = "Start", END = "End";

        public MapLifeLine(Individual ind)
        {
            this.ind = ind;
            int index = 1;
            List<Coordinate> points = new();
            Viewport = new Envelope();
            Coordinate? previousPoint = null;
            foreach (IDisplayFact f in ind.AllLifeLineFacts)
            {
                Coordinate point = new(f.Location.LongitudeM, f.Location.LatitudeM);
                if (index == 1)
                    StartPoint = new NetTopologySuite.Geometries.Point(point);
                if (index == ind.AllLifeLineFacts.Count)
                    EndPoint = new NetTopologySuite.Geometries.Point(point);
                index++;
                if (points.Count == 0 || (points.Count > 0 && !point.Equals2D(previousPoint)))
                {
                    points.Add(point); // don't add point if same as last one
                    previousPoint = point;
                }
                GeoResponse.CResult.CGeometry.CViewPort vp = f.Location.ViewPort;
                Envelope env = new(vp.NorthEast.Long, vp.SouthWest.Long, vp.NorthEast.Lat, vp.SouthWest.Lat);
                if (!Viewport.Contains(env))
                    Viewport.ExpandToInclude(env);
            }
            if (points.Count > 1)
                Geometry = new LineString(points.ToArray());
            else
                Geometry = StartPoint;
            Count = points.Count;
        }

        public FeatureDataRow AddFeatureDataRow(FeatureDataTable table)
        {
            FeatureDataRow r = table.NewRow();
            r["MapLifeLine"] = this;
            r["LineCap"] = LINE;
            r["Label"] = ind.Name;
            r["ViewPort"] = Viewport;
            r.Geometry = Geometry;
            table.AddRow(r);

            r = table.NewRow();
            r["MapLifeLine"] = this;
            r["LineCap"] = Count > 1 ? START : LINE;
            r["ViewPort"] = Viewport;
            if (Count == 1)
                r["Label"] = ind.Name;
            r.Geometry = StartPoint;
            table.AddRow(r);

            if (Count > 1)
            {
                r = table.NewRow();
                r["MapLifeLine"] = this;
                r["LineCap"] = END;
                r["ViewPort"] = Viewport;
                r.Geometry = EndPoint;
                table.AddRow(r);
            }
            return r;
        }

        public override string ToString() => ind.ToString();
    }
}
