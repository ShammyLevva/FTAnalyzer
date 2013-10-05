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

namespace FTAnalyzer.Utilities
{
    public class MarkerClusterer
    {

        private List<MapCluster> clusters;
        private double gridsize;
        private int minClusterSize;
        private Envelope bounds;

        public MarkerClusterer(FeatureDataTable source, double gridSize, Envelope bounds)
        {
            this.gridsize = gridSize;
            this.minClusterSize = 2;
            this.clusters = new List<MapCluster>();
            this.bounds = bounds;
            this.bounds.ExpandBy(gridsize);
            foreach (FeatureDataRow row in source)
                if (bounds.Covers((Envelope)row.Geometry.Envelope))
                    AddToClosestCluster(row);
        }

        private void AddToClosestCluster(FeatureDataRow row)
        {
            double distance = 40000; // Some large number
            MapCluster clusterToAddTo = null;
            foreach (MapCluster cluster in this.clusters)
            {
                Point centre = cluster.Centre;
                if (centre.X != 0 && centre.Y != 0)
                {
                    double d = centre.Distance(row.Geometry.Centroid);
                    if (d < distance)
                    {
                        distance = d;
                        clusterToAddTo = cluster;
                    }
                }
            }
            if (clusterToAddTo != null && clusterToAddTo.IsFeatureInClusterBounds(row))
            {
                clusterToAddTo.AddFeature(row);
            }
            else
            {
                MapCluster cluster = new MapCluster(minClusterSize, gridsize);
                cluster.AddFeature(row);
                clusters.Add(cluster);
            }
        }

        public FeatureDataTable FeatureDataTable
        {
            get
            {
                FeatureDataTable result = new FeatureDataTable();
                return result;
            }
        }

        //private void FitMaptoMarkers()
        //{
        //    foreach (MapLocation ml in markers)
        //    {
        //        if (!bounds.Covers(ml.Location.Longitude, ml.Location.Latitude))
        //            bounds.ExpandToInclude(ml.Location.Longitude, ml.Location.Latitude);
        //    }
        //    IMathTransform transform = MapTransforms.MathTransform;
        //    bounds = new Envelope(transform.Transform(bounds.TopLeft()), transform.Transform(bounds.BottomRight()));
        //    bounds.ExpandBy(this.map.PixelSize * 5);
        //    this.map.ZoomToBox(bounds);
        //}
    }
}