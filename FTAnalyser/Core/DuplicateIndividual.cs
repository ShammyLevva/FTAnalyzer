using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DuplicateIndividual
    {
        public Individual IndividualA { get; private set; }
        public Individual IndividualB { get; private set; }
        public int Score { get; private set; }

        public DuplicateIndividual(Individual a, Individual b)
        {
            IndividualA = a;
            IndividualB = b;
            CalculateScore();
        }

        public void CalculateScore()
        {
            Score = 0;
            if (IndividualA.Surname.Equals(IndividualB.Surname) && IndividualA.Forename.Equals(IndividualB.Forename))
                Score++;
            if (IndividualA.BirthDate.Equals(IndividualB.BirthDate))
                Score += 100;
            else if (IndividualA.BirthDate.Distance(IndividualB.BirthDate) <= 2)
                Score += 5;
            if (IndividualA.BirthLocation != null && IndividualA.BirthLocation.Equals(IndividualB.BirthLocation))
                Score += 10;
            if (IndividualA.DeathDate.Equals(IndividualB.DeathDate))
                Score += 10;
        }

        public override bool Equals(object that)
        {
            if (that is DuplicateIndividual)
                return (this.IndividualA.Equals(((DuplicateIndividual)that).IndividualA) && this.IndividualB.Equals(((DuplicateIndividual)that).IndividualB))
                    || (this.IndividualA.Equals(((DuplicateIndividual)that).IndividualB) && this.IndividualB.Equals(((DuplicateIndividual)that).IndividualA));
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
