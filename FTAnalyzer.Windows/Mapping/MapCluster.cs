using NetTopologySuite.Geometries;
using SharpMap.Data;
using System.Collections.Generic;

namespace FTAnalyzer.Mapping
{
    class MapCluster
    {
        readonly List<FeatureDataRow> cluster;
        readonly int minSize;
        readonly double gridSize;
        readonly List<Point> points;
        public static string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.points = new List<Point>();
        }

        public Geometry Geometry { get { return Centroid; } }

        public string ClusterType { get { return (cluster.Count < minSize) ? FEATURE : CLUSTER; } }

        public Point Centroid { get; private set; }

        public void AddFeature(FeatureDataRow row)
        {
            cluster.Add(row);
            Point p = (Point)row.Geometry;
            UpdateCentroid(p);
            points.Add(p);
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return Centroid.Distance(row.Geometry) <= gridSize;
        }

        public void UpdateCentroid(Point point)
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
                Centroid = new Point(new Coordinate(X, Y));
            }
        }

    }
}
