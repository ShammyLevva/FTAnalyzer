using System.Collections.Generic;
using GeoAPI.Geometries;
using SharpMap.Data;
using System;

namespace FTAnalyzer.Mapping
{
    public class MarkerClusterer : IDisposable
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
            clusteredDataTable.Columns.Add("Features", typeof(List<FeatureDataRow>)); ;
            clusteredDataTable.Columns.Add("Count", typeof(int));
            clusteredDataTable.Columns.Add("Label", typeof(string));
            clusteredDataTable.Columns.Add("Cluster", typeof(string));

            reclustering = false;
        }

        public void Recluster(double gridSize, Envelope bounds)
        {
            if (!reclustering)
            {
                reclustering = true;
                this.gridsize = gridSize;
                this.clusters.Clear();
                int count = 0;
                foreach (FeatureDataRow row in sourceDataTable)
                {
                    if (bounds.Contains(row.Geometry.Coordinate))
                    {
                        AddToClosestCluster(row);
                        count++;
                    }
                }
                BuildClusteredFeatureTable();
                reclustering = false;
            }
        }

        void AddToClosestCluster(FeatureDataRow row)
        {
            double distance = double.MaxValue;
            MapCluster clusterToAddTo = null;
            IPoint rowCentre = row.Geometry.Centroid;
            foreach (MapCluster cluster in this.clusters)
            {
                IPoint centre = cluster.Centroid;
                if (centre.X != 0 && centre.Y != 0)
                {
                    double d = centre.Distance(rowCentre);
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

        void BuildClusteredFeatureTable()
        {
            clusteredDataTable.Clear();
            foreach (MapCluster cluster in this.clusters)
            {
                FeatureDataRow row = clusteredDataTable.NewRow();
                row.Geometry = cluster.Geometry;
                row["Features"] = cluster.Features;
                row["Count"] = cluster.Features.Count;
                row["Label"] = cluster.Features.Count >= minClusterSize ? cluster.Features.Count.ToString() : string.Empty;
                row["Cluster"] = cluster.ClusterType;
                clusteredDataTable.AddRow(row);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                clusteredDataTable.Dispose();
                sourceDataTable.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}