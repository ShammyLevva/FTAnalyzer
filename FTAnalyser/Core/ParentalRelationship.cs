using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Core
{
    public class ParentalRelationship
    {
        public enum ParentalRelationshipType { NATURAL, ADOPTED, STEP, FOSTER, RELATED, GUARDIAN, SEALED, PRIVATE, UNKNOWN };

        public Family Family { get; private set; }
        public ParentalRelationshipType FatherRelationship { get; private set; }
        public ParentalRelationshipType MotherRelationship { get; private set; }

        public ParentalRelationship(Family family, ParentalRelationshipType father, ParentalRelationshipType mother)
        {
            this.Family = family;
            this.FatherRelationship = father;
            this.MotherRelationship = mother;
        }
    }
}
