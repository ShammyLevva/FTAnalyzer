using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NetTopologySuite.Geometries;
using SharpMap;

namespace FTAnalyzer.Utilities
{
    public class MarkerClusterer
    {
        private SharpMap.Map map;
        private List<MapLocation> markers;
        private List<MapLocation> clusters;
        private int gridsize;
        private int mincluster;
       
        public MarkerClusterer(Map map, List<MapLocation> markers)
        {
            this.map = map;
            this.markers = markers;
            this.clusters = new List<MapLocation>();
            this.gridsize = 60;
            this.mincluster = 2;
        }

        private double DistanceBetweenPoints(GeoResponse.CResult.CGeometry.CLocation p1, GeoResponse.CResult.CGeometry.CLocation p2)
        {
            if (p1 == null || p2 == null)
                return 0;

            double R = 6371; // Radius of the Earth in km
            double dLat = (p2.Lat - p1.Lat) * Math.PI / 180;
            double dLon = (p2.Long - p1.Long) * Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(p1.Lat * Math.PI / 180) * Math.Cos(p2.Lat * Math.PI / 180) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }

        private void AddToClosestCluster(MapLocation marker) 
        {
            double distance = 40000; // Some large number
            MapLocation clusterToAddTo = null;
            GeoResponse.CResult.CGeometry.CLocation pos = marker.GetPosition();
            foreach(MapLocation cluster in this.clusters) 
            {
                GeoResponse.CResult.CGeometry.CLocation centre = cluster.GetPosition();
                if (centre.Lat != 0 && centre.Long != 0) 
                {
                    double d = DistanceBetweenPoints(centre, marker.GetPosition());
                    if (d < distance)
                    {
                        distance = d;
                        clusterToAddTo = cluster;
                    }
                }
            }
            if (clusterToAddTo && clusterToAddTo.IsMarkerInClusterBounds(marker))
            {
                clusterToAddTo.AddMarker(marker);
            }
            else
            {
                var cluster = new Cluster(this);
                cluster.addMarker(marker);
                this.clusters_.push(cluster);
            }
        }
    }
}