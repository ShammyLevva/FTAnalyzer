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
        public Point Centre { get; private set; }
        public Envelope Bounds { get; private set; }
        private IMultiPoint multiPoint;

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<FeatureDataRow>();
            this.Centre = null;
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.Bounds = new Envelope();
            multiPoint = MultiPoint.Empty;
        }

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
            Centre = multiPoint.Centroid as Point;
            Bounds = multiPoint.Envelope as Envelope;
            return true;
        }

        public bool IsFeatureInClusterBounds(FeatureDataRow row)
        {
            return Bounds.Covers((Envelope)row.Geometry.Envelope);
        }
    }
}
