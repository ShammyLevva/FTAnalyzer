using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using FTAnalyzer.Filters;
using System.Collections.Specialized;
using System.Diagnostics;

namespace FTAnalyzer
{
    class Statistics
    {
        private static Statistics instance;
        private static FamilyTree ft;
        private List<SurnameStats> surnames;

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

        public int[,,] ChildrenBirthProfiles()
        {
            int[,,] stats = new int[2, 20, 3];
            foreach (Family f in ft.AllFamilies)
            {
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband != null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            AddAgeData(0, stats, age, child.Gender);
                        }
                        if (f.Wife != null && f.Wife.BirthDate.IsKnown)
                        {
                            Age age = f.Wife.GetAge(child.BirthDate);
                            AddAgeData(1, stats, age, child.Gender);
                        }
                    }
                }
            }
            return stats;
        }

        private void AddAgeData(int parent, int[,,] stats, Age age, string gender)
        {
            int child = gender == "M" ? 0 : (gender == "F" ? 1 : 2);
            int fiveyear = age.MinAge / 5;
            if (fiveyear >= 3 && fiveyear < 20)
                stats[parent, fiveyear, child]++;
        }

        public string BuildOutput(int[,,] minAge)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Fathers Age When Child Born");
            for (int fiveyear = 3; fiveyear < 20; fiveyear++)
            {
                string range = (fiveyear * 5) + " to " + (fiveyear * 5 + 4) + " : ";
                output.AppendLine(range + minAge[0, fiveyear, 0] + " Males, " + minAge[0, fiveyear, 1] + " Females " + minAge[0, fiveyear, 2] + " unknown.");
            }
            output.AppendLine();
            output.AppendLine("Mothers Age When Child Born");
            for (int fiveyear = 3; fiveyear < 13; fiveyear++)
            {
                string range = (fiveyear * 5) + " to " + (fiveyear * 5 + 4) + " : ";
                output.AppendLine(range + minAge[1, fiveyear, 0] + " Males, " + minAge[1, fiveyear, 1] + " Females " + minAge[1, fiveyear, 2] + " unknown.");
            }
            int over65males = minAge[1, 13, 0] + minAge[1, 14, 0] + minAge[1, 15, 0] + minAge[1, 16, 0] + minAge[1, 17, 0] + minAge[1, 18, 0] + minAge[1, 19, 0];
            int over65females = minAge[1, 13, 1] + minAge[1, 14, 1] + minAge[1, 15, 1] + minAge[1, 16, 1] + minAge[1, 17, 1] + minAge[1, 18, 1] + minAge[1, 19, 1];
            int over65unknown = minAge[1, 13, 2] + minAge[1, 14, 2] + minAge[1, 15, 2] + minAge[1, 16, 2] + minAge[1, 17, 2] + minAge[1, 18, 2] + minAge[1, 19, 2];
            output.AppendLine("over 65 : " + over65males + " Males, " + over65females + " Females " + over65unknown + " unknown.");
            return output.ToString();
        }

        public void Clear()
        {
            this.surnames = null;
        }

        public List<SurnameStats> Surnames(Predicate<Individual> indFilter, Predicate<Family> famFilter, IProgress<int> progress)
        {
            IEnumerable<Individual> list = ft.AllIndividuals.Filter(indFilter).GroupBy(x => x.Surname).Select(group => group.First());
            surnames = list.Select(x => new SurnameStats(x.Surname)).ToList();
            int maximum = list.Count();
            int value = 0;
            foreach (SurnameStats stat in surnames)
            {
                stat.Individuals = ft.AllIndividuals.Filter(indFilter).Where(x => x.Surname.Equals(stat.Surname, StringComparison.InvariantCultureIgnoreCase)).Count();
                stat.Families = ft.AllFamilies.Filter(famFilter).Where(x => x.ContainsSurname(stat.Surname)).Count();
                stat.Marriages = ft.AllFamilies.Filter(famFilter).Where(x => x.ContainsSurname(stat.Surname) && x.MaritalStatus == Family.MARRIED).Count();
                value++;
                if (value % 25 == 0)
                    progress.Report((100 * value)/maximum);
            }
            return surnames;
        }

        public static void DisplayGOONSpage(string surname)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string website = "http://one-name.org/Results";
                    NameValueCollection reqparm = new NameValueCollection
                    {
                        { "surname", surname },
                        { "_wpnonce", "4cc97f97c8" },
                        { "_wp_http_referer", "/Results" },
                        { "submit", "Search" }
                    };
                    byte[] responsebytes = client.UploadValues(website, "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    string filename = Path.Combine(Path.GetTempPath(), "FTA-GOONS.html");
                    File.WriteAllText(filename, responsebody);
                    Process.Start(filename);
                }
            }
            catch (Exception)
            { // silently fail
            }
        }
    }
}
