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
        private IMultiPoint multiPoint;
        private IGeometry bufferedMultiPoint;
        private IPoint centroid;
        
        public static readonly string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";
        private static readonly int CENTROID_THRESHOLD = 100;

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.points = new List<IPoint>();
            multiPoint = MultiPoint.Empty;
            bufferedMultiPoint = multiPoint.Envelope.Buffer(gridSize);
        }

        public IGeometry Geometry { get { return multiPoint.Centroid; } }
        
        public string ClusterType { get { return (cluster.Count < minSize) ? FEATURE : CLUSTER; } }

        public IPoint Centroid
        {
            get {
                if (points.Count < CENTROID_THRESHOLD)
                    return multiPoint.Centroid;
                else
                    return centroid;
            }
        }
        
        public void AddFeature(FeatureDataRow row)
        {
            cluster.Add(row);

            points.Add((IPoint)row.Geometry);
            if (points.Count == CENTROID_THRESHOLD)
                centroid = multiPoint.Centroid; // save centroid when we reach 500 points
            multiPoint = new MultiPoint(points.ToArray());
            bufferedMultiPoint = multiPoint.Envelope.Buffer(gridSize);
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return bufferedMultiPoint.Contains(row.Geometry);
        }

        //public void UpdateCentre(IPoint point)
        //{
        //    int oldCount = points.Count;
        //    int newCount = oldCount + 1;
        //    double X = (Centre.X * (oldCount / newCount)) + point.X / newCount;
        //    double Y = (Centre.Y * (oldCount / newCount)) + point.Y / newCount;
        //    Centre = new Point(new Coordinate(X, Y));
        //}

    }
}
