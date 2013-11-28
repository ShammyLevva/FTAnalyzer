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

        public static readonly string CLUSTER = "Cluster", FEATURE = "Feature", UNKNOWN = "Unknown";

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

        public void AddFeature(FeatureDataRow row)
        {
            //if (this.cluster.Contains(row))
            //    return false;
            cluster.Add(row);

            points.Add((IPoint)row.Geometry);
            multiPoint = new MultiPoint(points.ToArray());
            bufferedMultiPoint = multiPoint.Envelope.Buffer(gridSize);
            //multiPoint.Normalize(); didn't seem to make a difference if anything slower
            //return true;
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
