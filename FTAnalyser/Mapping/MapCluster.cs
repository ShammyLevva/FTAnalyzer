using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using GeoAPI.Geometries.Prepared;
using NetTopologySuite.Geometries.Prepared;

namespace FTAnalyzer.Mapping
{
    class MapCluster
    {
        private List<FeatureDataRow> cluster;
        private int minSize;
        private double gridSize;
        private List<IPoint> points;
        private IPoint centroid;

        public static string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.points = new List<IPoint>();
        }

        public IGeometry Geometry { get { return centroid; } }

        public string ClusterType { get { return (cluster.Count < minSize) ? FEATURE : CLUSTER; } }

        public IPoint Centroid
        {
            get { return centroid; }
        }

        public void AddFeature(FeatureDataRow row)
        {
            cluster.Add(row);
            IPoint p = (IPoint)row.Geometry;
            UpdateCentroid(p);
            points.Add(p);
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return centroid.Distance(row.Geometry) <= gridSize;
        }

        public void UpdateCentroid(IPoint point)
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
