using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplaySurnamesAtLocation : IDisplaySurnamesAtLocation
    {
        private FactLocation location;
        private List<string> surnames;

        public DisplaySurnamesAtLocation(FactLocation location)
        {
            this.location = location;
            this.surnames = FamilyTree.Instance.getSurnamesAtLocation(location);
        }

        public List<string> Surnames
        {
            get
            {
                return surnames;
            }
        }

        #region IDisplayLocation Members

        public string Country
        {
            get { return location.Country; }
        }

        public string Region
        {
            get { return location.Region; }
        }

        public string Parish
        {
            get { return location.Parish; }
        }

        public string Address
        {
            get { return location.Address; }
        }

        public string Place
        {
            get { return location.Place; }
        }

        public int Level
        {
            get { return location.Level; }
        }

        public int CompareTo(IDisplayLocation loc, int level)
        {
            throw new NotImplementedException();
        }

        public FactLocation getLocation(int level)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
