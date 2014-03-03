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
                return "self";
            CommonAncestor commonAncestor = toFind.CommonAncestor;
            string step = commonAncestor.step ? "step " : string.Empty;
            int rootDistance = (int)(Math.Log(commonAncestor.ind.Ahnentafel) / Math.Log(2.0));
            int toFindDistance = commonAncestor.distance;

            // DIRECT DESCENDANT - PARENT
            if (toFindDistance == 0)
            {
                string relation = toFind.IsMale ? "father" : "mother";
                return step + AggrandiseRelationship(relation, rootDistance, 0);
            }
            // DIRECT DESCENDANT - CHILD
            if (rootDistance == 0)
            {
                string relation = toFind.IsMale ? "son" : "daughter";
                return step + AggrandiseRelationship(relation, toFindDistance, 0);
            }
            // EQUAL DISTANCE - SIBLINGS / PERFECT COUSINS
            if (rootDistance == toFindDistance)
            {
                switch (toFindDistance)
                {
                    case 1:
                        return step + (toFind.IsMale ? "brother" : "sister");
                    case 2:
                        return step + "cousin";
                    default:
                        return step + OrdinalSuffix(toFindDistance - 2) + " cousin";
                }
            }
            // AUNT / UNCLE
            if (toFindDistance == 1)
            {
                string relation = toFind.IsMale ? "uncle" : "aunt";
                return step + AggrandiseRelationship(relation, rootDistance, 1);
            }
            // NEPHEW / NIECE
            if (rootDistance == 1)
            {
                string relation = toFind.IsMale ? "nephew" : "niece";
                return step + AggrandiseRelationship(relation, toFindDistance, 1);
            }
            // COUSINS, GENERATIONALLY REMOVED
            int cousinOrdinal = Math.Min(rootDistance, toFindDistance) - 1;
            int cousinGenerations = Math.Abs(rootDistance - toFindDistance);
            return step + OrdinalSuffix(cousinOrdinal) + " cousin " + FormatPlural(cousinGenerations, "time", "times") + " removed";
        }

        private static string FormatPlural(int count, string singular, string plural)
        {
            return count + " " + (count == 1 || count == -1 ? singular : plural);
        }

        private static string AggrandiseRelationship(string relation, int distance, int offset)
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

        private static string OrdinalSuffix(int number)
        {
            string os = string.Empty;
            if (number % 100 > 10 && number % 100 < 14)
                os = "th";
            else if (number == 0)
                os = "";
            else
            {
                Int32 last = number % 10;
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

        private static Tuple<Individual, int, int> GetLowestCommonAncestor(Individual rootPerson, Individual toFind)
        {
            List<Individual> commonAncestors = CommonAncestors(rootPerson, toFind);
            int index = -1;
            int leastDistance = -1;
            foreach (Individual ind in commonAncestors)
            {
                // int distance = 
            }
            return new Tuple<Individual, int, int>(rootPerson, index, leastDistance);
        }

        private static List<Individual> CommonAncestors(Individual rootPerson, Individual toFind)
        {
            List<Individual> common = new List<Individual>();
            if (toFind.RelationType == Individual.DIRECT)
                common.Add(toFind);
            return common;
        }


        //function lowest_common_ancestor($a_id, $b_id)
        //{
        //  $common_ancestors = common_ancestors($a_id, $b_id);

        //  $least_distance = -1;
        //  $ld_index = -1;

        //  foreach ($common_ancestors as $i => $c_anc) {
        //    $distance = $c_anc[1] + $c_anc[2];
        //    if ($least_distance < 0 || $least_distance > $distance) {
        //      $least_distance = $distance;
        //      $ld_index = $i;
        //    }
        //  }

        //  return $ld_index >= 0 ? $common_ancestors[$ld_index] : false;
        //} //END function lowest_common_ancestor

        //function common_ancestors($a_id, $b_id) {
        //  $common_ancestors = array();

        //  $a_ancestors = get_ancestors($a_id);
        //  $b_ancestors = get_ancestors($b_id);

        //  foreach ($a_ancestors as $a_anc) {
        //    foreach ($b_ancestors as $b_anc) {
        //      if ($a_anc[0] == $b_anc[0]) {
        //        $common_ancestors[] = array($a_anc[0], $a_anc[1], $b_anc[1]);
        //        break 1;
        //      }
        //    }
        //  }

        //  return $common_ancestors;
        //} //END function common_ancestors

        //function get_ancestors($id, $dist = 0)
        //{
        //  $ancestors = array();

        //  // SELF
        //  $ancestors[] = array($id, $dist);

        //  // PARENTS
        //  $parents = get_parents($id);
        //  foreach ($parents as $par) {
        //    if ($par != 0) {
        //      $par_ancestors = get_ancestors($par, $dist + 1);
        //      foreach ($par_ancestors as $par_anc) {
        //        $ancestors[] = $par_anc;
        //      }
        //    }
        //  }

        //  return $ancestors;
        //} //END function get_ancestors

        //function ordinal_suffix($number, $super = false)
        //{
        //  if ($number % 100 > 10 && $number %100 < 14) {
        //    $os = 'th';
        //  } else if ($number == 0) {
        //    $os = '';
        //  } else {
        //    $last = substr($number, -1, 1);

        //    switch($last) {
        //      case "1":
        //        $os = 'st';
        //        break;
        //      case "2":
        //        $os = 'nd';
        //        break;
        //      case "3":
        //        $os = 'rd';
        //        break;
        //      default:
        //        $os = 'th';
        //    }
        //  }

        //  $os = $super ? '<sup>'.$os.'</sup>' : $os;

        //  return $number.$os;
        //} //END function ordinal_suffix

        //function format_plural($count, $singular, $plural)
        //{
        //  return $count.' '.($count == 1 || $count == -1 ? $singular : $plural);
        //} //END function plural_format

        //?>

    }
}
