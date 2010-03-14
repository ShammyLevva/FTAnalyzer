using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayCensus : IDisplayCensus
    {
        private string familyGed;
        private string regLocation;
        private FactDate regDate;
        private Individual ind;

        public DisplayCensus(string familyGed, string regLocation, FactDate regDate, Individual ind)
        {
            this.familyGed = familyGed;
            this.regLocation = regLocation;
            this.regDate = regDate;
            this.ind = ind;
        }

        public string FamilyGed { get { return familyGed; } }
        public string RegistrationLocation { get { return regLocation; } }
        public string CensusName { get { return ind.CensusName; } }
        public Age Age { get { return ind.getAge(regDate); } }
        public string Occupation { get { return ind.Occupation; } }
        public string DateOfBirth { get { return ind.DateOfBirth; } }
        public string BirthLocation { get { return ind.BirthLocation; } }
        public string Status { get { return ind.Status; } }
        public string Relation { get { return ind.Relation; } }
        public int Ahnentafel { get { return ind.Ahnentafel; } }

    }
}
