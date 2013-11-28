using System.Collections.Generic;
using GeoAPI.Geometries;
using SharpMap.Data;

namespace FTAnalyzer.Mapping
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
            clusteredDataTable.Columns.Add("Features", typeof(List<FeatureDataRow>)); ;
            clusteredDataTable.Columns.Add("Count", typeof(int));
            clusteredDataTable.Columns.Add("Label", typeof(string));
            clusteredDataTable.Columns.Add("Cluster", typeof(string));

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
            IPoint rowCentre = row.Geometry.Centroid;
            foreach (MapCluster cluster in this.clusters)
            {
                IPoint centre = cluster.Geometry.Centroid;
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

        private void BuildClusteredFeatureTable()
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
    }
}