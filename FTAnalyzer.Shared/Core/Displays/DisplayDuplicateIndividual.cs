using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayDuplicateIndividual : IDisplayDuplicateIndividual
    {
        private Individual IndA { get; set; }
        private Individual IndB { get; set; }
        public int Score { get; private set; }
        public bool IgnoreNonDuplicate { get; set; }

        public DisplayDuplicateIndividual(DuplicateIndividual dup)
        {
            this.IndA = dup.IndividualA;
            this.IndB = dup.IndividualB;
            this.Score = dup.Score;
        }

        public string IndividualID { get { return IndA.IndividualID; } }
        public string Name { get { return IndA.Name; } }
        public string Forenames { get { return IndA.Forenames; } }
        public string Surname { get { return IndA.Surname; } }
        public FactDate BirthDate { get { return IndA.BirthDate; } }
        public FactLocation BirthLocation { get { return IndA.BirthLocation; } }

        public string MatchIndividualID { get { return IndB.IndividualID; } }
        public string MatchName { get { return IndB.Name; } }
        public FactDate MatchBirthDate { get { return IndB.BirthDate; } }
        public FactLocation MatchBirthLocation { get { return IndB.BirthLocation; } }
    }
}
