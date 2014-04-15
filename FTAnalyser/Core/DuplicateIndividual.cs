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
            if (IndividualA.Surname.Equals(IndividualB.Surname) && IndividualA.Surname != Individual.UNKNOWN_NAME)
                Score+=20;
            if(IndividualA.Forename.Equals(IndividualB.Forename) && IndividualA.Forename != Individual.UNKNOWN_NAME)
                Score+=20;
            ScoreDates(IndividualA.BirthDate, IndividualB.BirthDate);
            ScoreDates(IndividualA.DeathDate, IndividualB.DeathDate);
            LocationScore();
            Score += SharedParents() + SharedChildren() - DifferentParents();
        }

        private void LocationScore()
        {
            if (IndividualA.BirthLocation.IsBlank() || IndividualB.BirthLocation.IsBlank())
                return;
            if (IndividualA.BirthLocation.Equals(IndividualB.BirthLocation))
                Score += 75;
            if (IndividualA.BirthLocation.Country.Equals(IndividualB.BirthLocation.Country))
                Score += 10;
            if (IndividualA.BirthLocation.Region.Equals(IndividualB.BirthLocation.Region))
                Score += 10;
            if (IndividualA.BirthLocation.SubRegion.Equals(IndividualB.BirthLocation.SubRegion))
                Score += 20;
            if (IndividualA.BirthLocation.Address.Equals(IndividualB.BirthLocation.Address))
                Score += 40;
            if (IndividualA.BirthLocation.Place.Equals(IndividualB.BirthLocation.Place))
                Score += 40;

            if (IndividualA.BirthLocation.CountryMetaphone.Equals(IndividualB.BirthLocation.CountryMetaphone))
                Score += 5;
            if (IndividualA.BirthLocation.RegionMetaphone.Equals(IndividualB.BirthLocation.RegionMetaphone))
                Score += 5;
            if (IndividualA.BirthLocation.SubRegionMetaphone.Equals(IndividualB.BirthLocation.SubRegionMetaphone))
                Score += 10;
            if (IndividualA.BirthLocation.AddressMetaphone.Equals(IndividualB.BirthLocation.AddressMetaphone))
                Score += 20;
            if (IndividualA.BirthLocation.PlaceMetaphone.Equals(IndividualB.BirthLocation.PlaceMetaphone))
                Score += 20;

            if (IndividualA.BirthLocation.IsKnownCountry && IndividualB.BirthLocation.IsKnownCountry &&
                !IndividualA.BirthLocation.Country.Equals(IndividualB.BirthLocation.Country))
                Score -= 250;
        }

        private void ScoreDates(FactDate dateA, FactDate dateB)
        {
            if (dateA.IsKnown && dateB.IsKnown)
            {
                if (dateA.Equals(dateB))
                    Score += 50;
                else if (dateA.Distance(dateB) <= .25)
                    Score += 50;
                else if (dateA.Distance(dateB) <= .5)
                    Score += 20;
                else if (dateA.Distance(dateB) <= 1)
                    Score += 10;
                else if (dateA.Distance(dateB) <= 2)
                    Score += 5;
                else if (dateA.Distance(dateB) > 5)
                    Score -= (int)(dateA.Distance(dateB) * dateA.Distance(dateB));
                if (dateA.IsExact && dateB.IsExact)
                    Score += 100;
            }
        }

        private int SharedParents()
        {
            int score = 0;
            foreach(ParentalRelationship parentA in IndividualA.FamiliesAsChild)
            {
                foreach (ParentalRelationship parentB in IndividualB.FamiliesAsChild)
                {
                    if (parentA.Father == parentB.Father)
                        score += 50;
                    if (parentA.Mother == parentB.Mother)
                        score += 50;
                }
            }
            return score;
        }

        private int DifferentParents()
        {
            int score = 0;
            if(IndividualA.FamiliesAsChild.Count > 0 && IndividualB.FamiliesAsChild.Count > 0 && SharedParents() == 0)
            { // both individuals have parents but none of them are shared parents so heavy penalty
                score = -200;
            }
            return score;
        }

        private int SharedChildren()
        {
            int score = 0;
            foreach (Family familyA in IndividualA.FamiliesAsParent)
            {
                foreach (Family familyB in IndividualB.FamiliesAsParent)
                {
                    foreach (Individual familyBchild in familyB.Children)
                        if (familyA.Children.Contains(familyBchild))
                            score += 50;
                }
            }
            return score;
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
