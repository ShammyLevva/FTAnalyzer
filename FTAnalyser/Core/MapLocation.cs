using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Utilities;

namespace FTAnalyzer
{
    public class MapLocation
    {
        public Individual Individual { get; private set;}
        public FactLocation Location { get; private set; }
        public FactDate FactDate { get; private set; }
        
        public MapLocation(Individual ind, FactLocation loc, FactDate date)
        {
            this.Individual = ind;
            this.Location = loc;
            this.FactDate = date;
        }

        public GeoResponse.CResult.CGeometry.CLocation GetPosition()
        {
            GeoResponse.CResult.CGeometry.CLocation result = new GeoResponse.CResult.CGeometry.CLocation();
            result.Lat = Location.Latitude;
            result.Long = Location.Longitude;
            return result;
        }
    }
}

