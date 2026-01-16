using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
{
    class MapCluster(int minSize, double gridSize)
    {
        readonly List<FeatureDataRow> cluster = [];
        readonly int _minSize = minSize;
        readonly double _gridSize = gridSize;
        readonly List<NetTopologySuite.Geometries.Point> points = [];
        public static string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public Geometry Geometry { get { return Centroid; } }

        public string ClusterType { get { return (cluster.Count < _minSize) ? FEATURE : CLUSTER; } }

        public NetTopologySuite.Geometries.Point Centroid { get; private set; }

        public void AddFeature(FeatureDataRow row)
        {
            cluster.Add(row);
            NetTopologySuite.Geometries.Point p = (NetTopologySuite.Geometries.Point)row.Geometry;
            UpdateCentroid(p);
            points.Add(p);
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return Centroid.Distance(row.Geometry) <= _gridSize;
        }

        public void UpdateCentroid(NetTopologySuite.Geometries.Point point)
        {
            double oldCount = points.Count;
            if (oldCount == 0)
            {
                Centroid = point;
            }
            else
            {
                double newCount = oldCount + 1;
                double X = (Centroid.X * oldCount + point.X) / newCount;
                double Y = (Centroid.Y * oldCount + point.Y) / newCount;
                Centroid = new NetTopologySuite.Geometries.Point(new Coordinate(X, Y));
            }
        }

    }
}
