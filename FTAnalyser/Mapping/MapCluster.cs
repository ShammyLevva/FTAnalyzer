using NetTopologySuite.Geometries;
using SharpMap.Data;
using System.Collections.Generic;

namespace FTAnalyzer.Mapping
{
    class MapCluster
    {
        private List<FeatureDataRow> cluster;
        private int minSize;
        private double gridSize;
        private List<Point> points;
        private Point centroid;

        public static string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.points = new List<Point>();
        }

        public Geometry Geometry { get { return centroid; } }

        public string ClusterType { get { return (cluster.Count < minSize) ? FEATURE : CLUSTER; } }

        public Point Centroid
        {
            get { return centroid; }
        }

        public void AddFeature(FeatureDataRow row)
        {
            cluster.Add(row);
            Point p = (Point)row.Geometry;
            UpdateCentroid(p);
            points.Add(p);
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return centroid.Distance(row.Geometry) <= gridSize;
        }

        public void UpdateCentroid(Point point)
        {
            double oldCount = points.Count;
            if (oldCount == 0)
            {
                centroid = point;
            }
            else
            {
                double newCount = oldCount + 1;
                double X = (centroid.X * oldCount + point.X) / newCount;
                double Y = (centroid.Y * oldCount + point.Y) / newCount;
                centroid = new Point(new Coordinate(X, Y));
            }
        }

    }
}
