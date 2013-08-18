using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayOccupation : IDisplayOccupation
    {
        public string Occupation { get; private set; }
        public int Count { get; set; }

        public DisplayOccupation(string occupation,int count)
        {
            this.Occupation = occupation;
            this.Count = count;
        }

        public int CompareTo(IDisplayOccupation that)
        {
            return this.Occupation.CompareTo(that.Occupation);
        }
    }
}
