using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Relationship
    {
        public static string CalculateRelationship(Individual rootPerson, Individual toFind)
        {
            if (rootPerson.Equals(toFind))
                return "root person";
            CommonAncestor commonAncestor = toFind.CommonAncestor;
            Int64 rootDistance = (Int64)(Math.Log(commonAncestor.Ind.Ahnentafel) / Math.Log(2.0));
            Int64 toFindDistance = commonAncestor.Distance;

            // DIRECT DESCENDANT - PARENT
            if (toFindDistance == 0)
            {
                string relation = toFind.IsMale ? "father" : "mother";
                return (commonAncestor.Step ? "step " : string.Empty) + AggrandiseRelationship(relation, rootDistance, 0);
            }
            // DIRECT DESCENDANT - CHILD
            if (rootDistance == 0)
            {
                string relation = toFind.IsMale ? "son" : "daughter";
                return (commonAncestor.Step ? "step " : string.Empty) + AggrandiseRelationship(relation, toFindDistance, 0);
            }
            // EQUAL DISTANCE - SIBLINGS / PERFECT COUSINS
            if (rootDistance == toFindDistance)
            {
                switch (toFindDistance)
                {
                    case 1:
                        return (commonAncestor.Step ? "half " : string.Empty) + (toFind.IsMale ? "brother" : "sister");
                    case 2:
                        return "cousin";
                    default:
                        return OrdinalSuffix(toFindDistance - 1) + " cousin";
                }
            }
            // AUNT / UNCLE
            if (toFindDistance == 1)
            {
                string relation = toFind.IsMale ? "uncle" : "aunt";
                return AggrandiseRelationship(relation, rootDistance, 1);
            }
            // NEPHEW / NIECE
            if (rootDistance == 1)
            {
                string relation = toFind.IsMale ? "nephew" : "niece";
                return AggrandiseRelationship(relation, toFindDistance, 1);
            }
            // COUSINS, GENERATIONALLY REMOVED
            Int64 cousinOrdinal = Math.Min(rootDistance, toFindDistance) - 1;
            Int64 cousinGenerations = Math.Abs(rootDistance - toFindDistance);
            return OrdinalSuffix(cousinOrdinal) + " cousin " + FormatPlural(cousinGenerations) + " removed";
        }

        private static string FormatPlural(Int64 count)
        {
            if (Math.Abs(count) == 1)
                return "once";
            if (Math.Abs(count) == 2)
                return "twice";
            return count + " times";
        }

        private static string AggrandiseRelationship(string relation, Int64 distance, int offset)
        {
            distance -= offset;
            switch (distance)
            {
                case 1:
                    return relation;
                case 2:
                    return "grand" + relation;
                case 3:
                    return "great grand" + relation;
                default:
                    return OrdinalSuffix(distance - 2) + " great grand" + relation;
            }
        }

        private static string OrdinalSuffix(Int64 number)
        {
            string os = string.Empty;
            if (number % 100 > 10 && number % 100 < 14)
                os = "th";
            else if (number == 0)
                os = "";
            else
            {
                Int64 last = number % 10;
                switch (last)
                {
                    case 1:
                        os = "st";
                        break;
                    case 2:
                        os = "nd";
                        break;
                    case 3:
                        os = "rd";
                        break;
                    default:
                        os = "th";
                        break;
                }
            }
            return number + os;
        }
    }
}
