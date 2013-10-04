using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace FTAnalyzer
{
    class MapCluster
    {
        private List<MapLocation> cluster;
        private int minSize;
        private double gridSize;
        public Point Centre { get; private set; }
        public Envelope Bounds { get; private set; }
        private IMultiPoint multiPoint;

        public MapCluster(int minSize, double gridSize)
        {
            this.cluster = new List<MapLocation>();
            this.Centre = null;
            this.minSize = minSize;
            this.gridSize = gridSize;
            this.Bounds = new Envelope();
            multiPoint = MultiPoint.Empty;
        }

        public bool AddMarker(MapLocation marker)
        {
            if (this.cluster.Contains(marker))
                return false;
            cluster.Add(marker);

            Point[] points = new Point[cluster.Count];
            int index = 0;
            foreach (MapLocation ml in cluster)
                points[index++] = ml.Point;
            multiPoint = MultiPoint.DefaultFactory.CreateMultiPoint(points);
            Centre = multiPoint.Centroid as Point;
            Bounds = multiPoint.Envelope as Envelope;
            Bounds.ExpandBy(gridSize);

            if (cluster.Count < minSize)
            {   // Min cluster size not reached so show the marker.
                marker.DrawPoint = true;
            }
            if (cluster.Count == this.minSize)
            {   // Hide the markers that were showing.
                foreach(MapLocation ml in cluster)
                    ml.DrawPoint = false;
            }
            if (cluster.Count >= this.minSize)
                marker.DrawPoint = false;
            this.UpdateIcon();
            return true;
        }

        private void UpdateIcon()
        {
            // TODO needs to update icon
        }

        public bool IsMarkerInClusterBounds(MapLocation marker)
        {
            return Bounds.Covers(marker.Point.X, marker.Point.Y);
        }
    }
}
