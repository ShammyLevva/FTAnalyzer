using FTAnalyzer.Filters;
using FTAnalyzer.Windows;
using System.Diagnostics;
using System.Text;

namespace FTAnalyzer
{
    class Statistics
    {
        static Statistics instance;
        static FamilyTree ft;
        List<SurnameStats> surnames;

        Statistics() => ft = FamilyTree.Instance;

        public static Statistics Instance
        {
            get
            {
                instance ??= new Statistics();
                return instance;
            }
        }

        public static int[,,] ChildrenBirthProfiles()
        {
            int[,,] stats = new int[2, 20, 3];
            foreach (Family f in ft.AllFamilies)
            {
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband is not null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            AddAgeData(0, stats, age, child.Gender);
                        }
                        if (f.Wife is not null && f.Wife.BirthDate.IsKnown)
                        {
                            Age age = f.Wife.GetAge(child.BirthDate);
                            AddAgeData(1, stats, age, child.Gender);
                        }
                    }
                }
            }
            return stats;
        }

        static void AddAgeData(int parent, int[,,] stats, Age age, string gender)
        {
            int child = gender == "M" ? 0 : (gender == "F" ? 1 : 2);
            int fiveyear = age.MinAge / 5;
            if (fiveyear >= 3 && fiveyear < 20)
                stats[parent, fiveyear, child]++;
        }

        public static string BuildOutput(int[,,] minAge)
        {
            StringBuilder output = new();
            output.AppendLine("Fathers Age When Child Born");
            for (int fiveyear = 3; fiveyear < 20; fiveyear++)
            {
                string range = $"{(fiveyear * 5)} to {(fiveyear * 5 + 4)} : ";
                output.AppendLine($"{range}{minAge[0, fiveyear, 0]} Males, {minAge[0, fiveyear, 1]} Females, {minAge[0, fiveyear, 2]} unknown.");
            }
            output.AppendLine();
            output.AppendLine("Mothers Age When Child Born");
            for (int fiveyear = 3; fiveyear < 13; fiveyear++)
            {
                string range = $"{(fiveyear * 5)} to {(fiveyear * 5 + 4)} : ";
                output.AppendLine($"{range}{minAge[1, fiveyear, 0]} Males, {minAge[1, fiveyear, 1]} Females, {minAge[1, fiveyear, 2]} unknown.");
            }
            int over65males = minAge[1, 13, 0] + minAge[1, 14, 0] + minAge[1, 15, 0] + minAge[1, 16, 0] + minAge[1, 17, 0] + minAge[1, 18, 0] + minAge[1, 19, 0];
            int over65females = minAge[1, 13, 1] + minAge[1, 14, 1] + minAge[1, 15, 1] + minAge[1, 16, 1] + minAge[1, 17, 1] + minAge[1, 18, 1] + minAge[1, 19, 1];
            int over65unknown = minAge[1, 13, 2] + minAge[1, 14, 2] + minAge[1, 15, 2] + minAge[1, 16, 2] + minAge[1, 17, 2] + minAge[1, 18, 2] + minAge[1, 19, 2];
            output.AppendLine($"Over 65 : {over65males} Males, {over65females} Females, {over65unknown} unknown.");
            return output.ToString();
        }

        public void Clear() => surnames = null;

        public List<IDisplaySurnames> Surnames(Predicate<Individual> indFilter, Predicate<Family> famFilter, IProgress<int> progress, bool ignoreCase)
        {
            IEnumerable<Individual> list = ft.AllIndividuals.Where(x => x.Surname != Individual.UNKNOWN_NAME).Filter(indFilter).GroupBy(x => x.Surname).Select(group => group.First());
            surnames = list.Select(x => new SurnameStats(x.Surname)).ToList();
            int maximum = list.Count();
            int value = 0;
            foreach (SurnameStats stat in surnames)
            {
                stat.Individuals = ignoreCase ?
                        ft.AllIndividuals.Filter(indFilter).Count(x => x.Surname.Equals(stat.Surname, StringComparison.OrdinalIgnoreCase))
                      : ft.AllIndividuals.Filter(indFilter).Count(x => x.Surname.Equals(stat.Surname));
                stat.Families = ft.AllFamilies.Filter(famFilter).Count(x => x.ContainsSurname(stat.Surname, ignoreCase));
                stat.Marriages = ft.AllFamilies.Filter(famFilter).Count(x => x.ContainsSurname(stat.Surname, ignoreCase) && x.MaritalStatus == Family.MARRIED);
                value++;
                if (value % 25 == 0)
                    progress.Report((100 * value) / maximum);
            }
            return surnames.Distinct(new SurnameStatsComparer()).ToList();
        }

        public static async void DisplayGOONSpage(string? surname)
        {
            if (string.IsNullOrEmpty(surname))
                return;
            try
            {
                Dictionary<string, string> parameters = new()
                    {
                        { "surname", surname },
                        { "_wpnonce", "4cc97f97c8" },
                        { "_wp_http_referer", "/Results" },
                        { "submit", "Search" }
                    };
                HttpRequestMessage req = new(HttpMethod.Post, "https://one-name.org/Results")
                {
                    Content = new FormUrlEncodedContent(parameters)
                };
                req.Content.Headers.Clear();
                req.Content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                HttpResponseMessage response = await Program.Client.SendAsync(req);
                response.EnsureSuccessStatusCode();
                string responsebody = await response.Content.ReadAsStringAsync();
                string filename = Path.Combine(Path.GetTempPath(), "FTA-GOONS.html");
                File.WriteAllText(filename, responsebody);
                Process p = new()
                {
                    StartInfo = new ProcessStartInfo(filename)
                    {
                        UseShellExecute = true
                    }
                };
                p.Start();
            }
            catch (Exception)
            { // silently fail
            }
        }
    }
}
