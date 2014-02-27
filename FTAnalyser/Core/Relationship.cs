using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Core
{
    public class Relationship
    {

        private Individual a;
        private Individual b;
        private string atob;
        private string btoa;

        private static string UNKNOWN = "Unknown";

        public Relationship(Individual a, Individual b)
        {
            this.a = a;
            this.b = b;
            atob = CalculateRelationship(a, b);
            btoa = CalculateRelationship(b, a);
        }

        public string GetRelation(Individual x)
        {
            if (x.Equals(a)) return atob;
            if (x.Equals(b)) return btoa;
            return UNKNOWN;
        }

        private string CalculateRelationship(Individual a, Individual b)
        {
            if (a.Equals(b)) 
                return "self";
            Tuple<Individual, int, int> lca = GetLowestCommonAncestor(a, b);
            if(lca == null)
                return UNKNOWN;
            int adist = lca.Item2;
            int bdist = lca.Item3;

            // DIRECT DESCENDANT - PARENT
            if (adist == 0)
            {
                string relation = a.IsMale ? "father" : "mother";
                return AggrandiseRelationship(relation, bdist, 0);
            }
            // DIRECT DESCENDANT - CHILD
            if (bdist == 0)
            {
                string relation = a.IsMale ? "son" : "daughter";
                return AggrandiseRelationship(relation, adist, 0);
            }
            // EQUAL DISTANCE - SIBLINGS / PERFECT COUSINS
            if(adist == bdist)
            {
                switch (adist)
                {
                  case 1:
                    return a.IsMale ? "brother" : "sister";
                  case 2:
                    return "cousin";
                  default:
                    return OrdinalSuffix(adist - 2) + " cousin";
                }
            }
            // AUNT / UNCLE
            if(adist == 1)
            {
                string relation = a.IsMale ? "uncle" : "aunt";
                return AggrandiseRelationship(relation, bdist, 1);
            }
            // NEPHEW / NIECE
            if(bdist == 1)
            {
                string relation = a.IsMale ? "nephew" : "neice";
                return AggrandiseRelationship(relation, adist, 1);
            }
            // COUSINS, GENERATIONALLY REMOVED
            int cousinOrdinal = Math.Min(adist, bdist) - 1;
            int cousinGenerations = Math.Abs(adist - bdist);
            return OrdinalSuffix(cousinOrdinal) + " cousin " + FormatPlural(cousinGenerations, "time", "times") + " removed";
        }

        private string FormatPlural(int cousinGenerations, string p1, string p2)
        {
            throw new NotImplementedException();
        }

        private string AggrandiseRelationship(string relation, int distance, int offset)
        {
            distance -= offset;
            switch(distance)
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

        private string OrdinalSuffix(int p)
        {
            throw new NotImplementedException();
        }

        private Tuple<Individual,int, int> GetLowestCommonAncestor(Individual a, Individual b)
        {
            List<Individual> commonAncestors = CommonAncestors(a, b);
            int index = -1;
            int leastDistance = -1;
            foreach(Individual ind in commonAncestors)
            {
               // int distance = 
            }
            return new Tuple<Individual, int, int>(a,index,leastDistance);
        }

        private List<Individual> CommonAncestors(Individual a, Individual b)
        {
            throw new NotImplementedException();
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

//function get_parents($id)
//{
//  return array(get_father($id), get_mother($id));
//} //END function get_parents

//function get_father($id)
//{
//  $res = db_result(db_query("SELECT father_id FROM child WHERE child_id = %s", $id));
//  return $res ? $res : 0;
//} //END function get_father

//function get_mother($id)
//{
//  $res = db_result(db_query("SELECT mother_id FROM child WHERE child_id = %s", $id));
//  return $res ? $res : 0;
//} //END function get_mother

//function get_gender($id)
//{
//  return intval(db_result(db_query("SELECT gender FROM individual WHERE id = %s", $id)));
//}

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
