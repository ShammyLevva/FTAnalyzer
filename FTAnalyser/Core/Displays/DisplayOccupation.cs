using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayOccupation : IDisplayOccupation
    {
        private string occupation;
        private int count;

        public DisplayOccupation(string occupation,int count)
        {
            this.occupation = occupation;
            this.count = count;
        }

        public string Occupation { get { return occupation; } }
        public int Count { get { return count; } set { count = value; } }

        public int CompareTo(IDisplayOccupation that)
        {
            return this.Occupation.CompareTo(that.Occupation);
        }
    }
}
