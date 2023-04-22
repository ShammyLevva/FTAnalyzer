using System.Collections.Generic;
using NetTopologySuite.Geometries;
using SharpMap.Data;
using System;

namespace FTAnalyzer.Mapping
{
    public class MarkerClusterer : IDisposable
    {
        readonly List<MapCluster> clusters;
        double gridsize;
        readonly int minClusterSize;
        readonly FeatureDataTable sourceDataTable;
        bool reclustering;

        public MarkerClusterer(FeatureDataTable source)
        {
            sourceDataTable = source;
            minClusterSize = 2;
            clusters = new List<MapCluster>();
            FeatureDataTable = new FeatureDataTable();
            FeatureDataTable.Columns.Add("Features", typeof(List<FeatureDataRow>));
            FeatureDataTable.Columns.Add("Count", typeof(int));
            FeatureDataTable.Columns.Add("Label", typeof(string));
            FeatureDataTable.Columns.Add("Cluster", typeof(string));

            reclustering = false;
        }

        public void Recluster(double gridSize, Envelope bounds)
        {
            if (!reclustering)
            {
                reclustering = true;
                gridsize = gridSize;
                clusters.Clear();
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
            NetTopologySuite.Geometries.Point rowCentre = row.Geometry.Centroid;
            foreach (MapCluster cluster in clusters)
            {
                NetTopologySuite.Geometries.Point centre = cluster.Centroid;
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
                MapCluster cluster = new(minClusterSize, gridsize);
                cluster.AddFeature(row);
                clusters.Add(cluster);
            }
        }

        public FeatureDataTable FeatureDataTable { get; }

        void BuildClusteredFeatureTable()
        {
            FeatureDataTable.Clear();
            foreach (MapCluster cluster in clusters)
            {
                FeatureDataRow row = FeatureDataTable.NewRow();
                row.Geometry = cluster.Geometry;
                row["Features"] = cluster.Features;
                row["Count"] = cluster.Features.Count;
                row["Label"] = cluster.Features.Count >= minClusterSize ? cluster.Features.Count.ToString() : string.Empty;
                row["Cluster"] = cluster.ClusterType;
                FeatureDataTable.AddRow(row);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    FeatureDataTable.Dispose();
                    sourceDataTable.Dispose();
                }
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}