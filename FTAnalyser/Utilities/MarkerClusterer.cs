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
        private FeatureDataTable clusteredDataTable;
        private FeatureDataTable sourceDataTable;
        private bool reclustering;

        public MarkerClusterer(FeatureDataTable source)
        {
            this.sourceDataTable = source;
            this.minClusterSize = 2;
            this.clusters = new List<MapCluster>();
            clusteredDataTable = new FeatureDataTable();
            clusteredDataTable.Columns.Add("Features");
            clusteredDataTable.Columns.Add("Count", typeof(int));
            clusteredDataTable.Columns.Add("Relation", typeof(int));
            clusteredDataTable.Columns.Add("Cluster", typeof(string));
            clusteredDataTable.Columns.Add("Label", typeof(string));
            reclustering = false;
        }

        public void Recluster(double gridSize)
        {
            if (!reclustering)
            {
                reclustering = true;
                this.gridsize = gridSize;
                this.clusters.Clear();
                foreach (FeatureDataRow row in sourceDataTable)
                    AddToClosestCluster(row);
                BuildClusteredFeatureTable();
                reclustering = false;
            }
        }

        private void AddToClosestCluster(FeatureDataRow row)
        {
            double distance = double.MaxValue;
            MapCluster clusterToAddTo = null;
            foreach (MapCluster cluster in this.clusters)
            {
                IPoint centre = cluster.Geometry.Centroid;
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
                clusterToAddTo.AddFeature(row);
            else
            {
                MapCluster cluster = new MapCluster(minClusterSize, gridsize);
                cluster.AddFeature(row);
                clusters.Add(cluster);
            }
        }

        public FeatureDataTable FeatureDataTable { get { return clusteredDataTable; } }

        private void BuildClusteredFeatureTable()
        {
            clusteredDataTable.Clear();
            foreach (MapCluster cluster in this.clusters)
            {
                FeatureDataRow row = clusteredDataTable.NewRow();
                row.Geometry = cluster.Geometry;
                row["Features"] = cluster.Features;
                row["Count"] = cluster.Features.Count;
                row["Label"] = cluster.Features.Count.ToString();
                row["Relation"] = Individual.UNKNOWN;
                row["Cluster"] = cluster.ClusterType;
                clusteredDataTable.AddRow(row);
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