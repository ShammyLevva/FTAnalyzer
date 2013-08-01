using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class Statistics
    {
        private static Statistics instance;
        private static FamilyTree ft;

        private Statistics()
        {
            ft = FamilyTree.Instance;
        }

        public static Statistics Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Statistics();
                }
                return instance;
            }
        }

        public string ChildrenBirthProfiles()
        {
            int[,,] minAge = new int[2,10,3];
            int[,,] maxAge = new int[2,10,3];
            foreach (Family f in ft.AllFamilies)
            {
                foreach (Individual child in f.children)
                {
                    if (child.BirthDate != FactDate.UNKNOWN_DATE)
                    {
                        if (f.husband != null && f.husband.BirthDate != FactDate.UNKNOWN_DATE)
                        {
                            Age age = f.husband.getAge(child.BirthDate);
                            addAgeData(0, minAge, maxAge, age, child.Gender);
                        }
                        if (f.wife != null && f.wife.BirthDate != FactDate.UNKNOWN_DATE)
                        {
                            Age age = f.wife.getAge(child.BirthDate);
                            addAgeData(1, minAge, maxAge, age, child.Gender);
                        }
                    }
                }
            }
            return buildOutput(minAge, maxAge);
        }
        private void addAgeData(int parent, int[,,] minAge, int[,,] maxAge, Age age, string gender)
        {
            int child = gender == "M" ? 0 : (gender == "F" ? 1 : 2);
            int decade = age.MinAge / 10;
            if(decade>0 && decade < 10)
                minAge[parent, decade, child]++;
            decade = age.MaxAge / 10;
            if (decade > 0 && decade < 10)
                maxAge[parent, decade, child]++;
        }

        private string buildOutput(int[, ,] minAge, int[, ,] maxAge)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Fathers Age When Child Born");
            for (int decade = 1; decade < 10; decade++)
            {
                output.AppendLine(decade + "0's : " + minAge[0, decade, 0] + " Males, " + minAge[0, decade, 1] + " Females " + minAge[0, decade, 2] + " unknown.");
            }
            output.AppendLine();
            output.AppendLine("Mothers Age When Child Born");
            for (int decade = 1; decade < 10; decade++)
            {
                output.AppendLine(decade + "0's : " + minAge[1, decade, 0] + " Males, " + minAge[1, decade, 1] + " Females " + minAge[1, decade, 2] + " unknown.");
            }
            return output.ToString();
        }
    }
}
