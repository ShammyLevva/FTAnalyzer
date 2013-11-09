using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
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

        public bool IsNaturalFather
        {
            get
            {
                return FatherRelationship == ParentalRelationshipType.NATURAL ||
                       FatherRelationship == ParentalRelationshipType.UNKNOWN ||
                       FatherRelationship == ParentalRelationshipType.PRIVATE;
            }
        }

        public bool IsNaturalMother
        {
            get { return MotherRelationship == ParentalRelationshipType.NATURAL || 
                         MotherRelationship == ParentalRelationshipType.UNKNOWN ||
                         MotherRelationship == ParentalRelationshipType.PRIVATE;
            }
        }

        public static ParentalRelationshipType GetRelationshipType(XmlNode node)
        {
            if (node == null)
                return ParentalRelationshipType.UNKNOWN;
            switch (node.InnerText.ToLower())
            {
                case "natural":
                    return ParentalRelationshipType.NATURAL;
                case "adopted":
                    return ParentalRelationshipType.ADOPTED;
                case "step":
                    return ParentalRelationshipType.STEP;
                case "foster":
                    return ParentalRelationshipType.FOSTER;
                case "related":
                    return ParentalRelationshipType.RELATED;
                case "guardian":
                    return ParentalRelationshipType.GUARDIAN;
                case "sealed":
                    return ParentalRelationshipType.SEALED;
                case "private":
                    return ParentalRelationshipType.PRIVATE;
                default:
                    return ParentalRelationshipType.UNKNOWN;
            }
        }
    }
}
