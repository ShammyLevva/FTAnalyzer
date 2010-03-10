using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayCensus
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

        string FamilyGed { get { return familyGed; } }
        string RegistrationLocation { get { return regLocation; } }
        string CensusName { get { return ind.CensusName; } }
        Age Age { get { return ind.getAge(regDate); } }
        string Occupation { get { return ind.Occupation; } }
        string DateOfBirth { get { return ind.DateOfBirth; } }
        string BirthLocation { get { return ind.BirthLocation; } }
        string Status { get { return ind.Status; } }

    }
}
