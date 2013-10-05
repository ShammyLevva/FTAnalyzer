using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap.Data;

namespace FTAnalyzer
{
    class MapCluster
    {
        private List<FeatureDataRow> cluster;
        private int minSize;
        private double gridSize;
        private IMultiPoint multiPoint;

        public IList<FeatureDataRow> Features { get { return cluster; } }

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.minSize = minSize;
            this.gridSize = gridSize;
            multiPoint = MultiPoint.Empty;
        }

        public IGeometry Geometry { get { return multiPoint.Centroid; } }

        public bool IsCluster { get { return (cluster.Count >= minSize); } }

        public bool AddFeature(FeatureDataRow row)
        {
            if (this.cluster.Contains(row))
                return false;
            cluster.Add(row);

            IPoint[] points = new IPoint[cluster.Count];
            int index = 0;
            foreach (FeatureDataRow ml in cluster)
                points[index++] = (IPoint)ml.Geometry;
            multiPoint = new MultiPoint(points);
            return true;
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return multiPoint.Distance(row.Geometry) <= gridSize;
        }
    }
}
