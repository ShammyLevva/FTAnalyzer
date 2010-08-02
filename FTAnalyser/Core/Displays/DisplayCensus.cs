using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayCensus : IDisplayCensus
    {
        private Individual ind;
        private CensusRegistration reg;
        private int pos;

        public DisplayCensus(int pos, CensusRegistration reg, Individual ind)
        {
            this.pos = pos;
            this.reg = reg;
            this.ind = ind;
        }

        public string FamilyGed { get { return reg.FamilyGed; } }
        public string RegistrationLocation { get { return reg.RegistrationLocation; } }
        public string CensusName { get { return ind.CensusName; } }
        public Age Age { get { return ind.getAge(reg.registrationDate); } }
        public string Occupation { get { return ind.Occupation; } }
        public string DateOfBirth { get { return ind.DateOfBirth; } }
        public string BirthLocation { get { return ind.BirthLocation; } }
        public string Status { get { return ind.Status; } }
        public string Relation { get { return ind.Relation; } }
        public int Ahnentafel { get { return ind.Ahnentafel; } }

        public CensusRegistration Registration { get { return reg; } }
        public Individual Individual { get { return ind; } }
        public int Position { get { return pos; } }

        public bool isAlive { get { return reg.registrationDate.isAfter(ind.BirthDate) && reg.registrationDate.isBefore(ind.DeathDate); } }
        public int RelationType { get { return ind.RelationType; } }

    }
}
