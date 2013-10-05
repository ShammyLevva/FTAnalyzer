using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NetTopologySuite.Geometries;
using SharpMap;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems.Transformations;
using SharpMap.Data;
using System.Data;

namespace FTAnalyzer.Utilities
{
    public class MarkerClusterer : FeatureDataTable
    {
        private SharpMap.Map map;
        private List<MapLocation> markers;
        private List<MapCluster> clusters;
        private double gridsize;
        private int minClusterSize;
        private Envelope box;

        public MarkerClusterer(FeatureDataTable source)
        {
            foreach (DataColumn column in source.Columns)
                this.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
            foreach (FeatureDataRow row in source.Rows)
            {
                FeatureDataRow r = this.NewRow();
                r.Geometry = row.Geometry;
                foreach (DataColumn c in source.Columns)
                    r[c.ColumnName] = row[c.ColumnName];
                this.AddRow(r);
            }
        }

        public MarkerClusterer(Map map, List<MapLocation> markers)
        {
            this.map = map;
            this.markers = markers;
            this.clusters = new List<MapCluster>(minClusterSize);
            this.gridsize = map.PixelSize * 3;
            this.minClusterSize = 2;
            this.box = map.GetExtents();
            this.box.ExpandBy(gridsize);
            foreach (MapLocation ml in markers)
                if (box.Covers(ml.Point.X, ml.Point.Y))
                    AddToClosestCluster(ml);
        }
        
        private void FitMaptoMarkers()
        {
            foreach (MapLocation ml in markers)
            {
                if (!box.Covers(ml.Location.Longitude, ml.Location.Latitude))
                    box.ExpandToInclude(ml.Location.Longitude, ml.Location.Latitude);
            }
            IMathTransform transform = MapTransforms.MathTransform;
            box = new Envelope(transform.Transform(box.TopLeft()), transform.Transform(box.BottomRight()));
            box.ExpandBy(this.map.PixelSize * 5);
            this.map.ZoomToBox(box);
        }

        private double DistanceBetweenPoints(Point p1, Point p2)
        {
            if (p1 == null || p2 == null)
                return 0;

            double R = 6371; // Radius of the Earth in km
            double dLat = (p2.X - p1.X) * Math.PI / 180;
            double dLon = (p2.Y - p1.Y) * Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(p1.X * Math.PI / 180) * Math.Cos(p2.X * Math.PI / 180) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }

        public void CreateCluster()
        {

        }

        private void AddToClosestCluster(MapLocation marker) 
        {
            double distance = 40000; // Some large number
            MapCluster clusterToAddTo = null;
            foreach(MapCluster cluster in this.clusters) 
            {
                Point centre = cluster.Centre;
                if (centre.X != 0 && centre.Y != 0) 
                {
                    double d = DistanceBetweenPoints(centre, marker.Point);
                    if (d < distance)
                    {
                        distance = d;
                        clusterToAddTo = cluster;
                    }
                }
            }
            if (clusterToAddTo != null && clusterToAddTo.IsMarkerInClusterBounds(marker))
            {
                clusterToAddTo.AddMarker(marker);
            }
            else
            {
                MapCluster cluster = new MapCluster(minClusterSize, gridsize);
                cluster.AddMarker(marker);
                clusters.Add(cluster);
            }
        }
    }
}