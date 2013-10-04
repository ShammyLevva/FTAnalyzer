using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Utilities;

namespace FTAnalyzer
{
    class MapCluster
    {
        private List<MapLocation> cluster;
        private int minSize;
        public GeoResponse.CResult.CGeometry.CLocation Centre { get; private set; }

        public MapCluster(int minSize)
        {
            this.cluster = new List<MapLocation>();
            this.Centre = null;
            this.minSize = minSize;
        }

        public bool AddMarker(MapLocation marker)
        {
            if (this.cluster.Contains(marker))
                return false;

            if (this.Centre == null)
            {
                this.Centre = marker.GetPosition();
                CalculateBounds();
            }
            else
            {
                if (AverageCenter != null)
                {
                    int l = cluster.Count + 1;
                    double lat = (Centre.Lat * (l - 1) + marker.GetPosition().Lat) / l;
                    double lng = (Centre.Long * (l - 1) + marker.GetPosition().Long) / l;
                    Centre.Lat = lat;
                    Centre.Long = lng;
                    CalculateBounds();
                }
            }

            cluster.Add(marker);
            if (cluster.Count < minSize)
            {
                // Min cluster size not reached so show the marker.
                marker.setMap(this.map_);
            }

            if (cluster.Count == this.minSize)
            {
                // Hide the markers that were showing.
                foreach(MapLocation ml in cluster)
                {
                    ml.setMap(null);
                }
            }

            if (cluster.Count >= this.minSize)
            {
                marker.setMap(null);
            }

            this.updateIcon();
            return true;
        }
    }
}
