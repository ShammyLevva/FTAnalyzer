using FTAnalyzer.Filters;
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
            StringComparer comparer = ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

            // Single pass over individuals: count per surname and capture canonical display name
            Dictionary<string, int> individualCounts = new(comparer);
            Dictionary<string, string> canonicalName = new(comparer);
            foreach (Individual ind in ft.AllIndividuals)
            {
                if (ind.Surname == Individual.UNKNOWN_NAME || !indFilter(ind)) continue;
                individualCounts[ind.Surname] = individualCounts.GetValueOrDefault(ind.Surname) + 1;
                canonicalName.TryAdd(ind.Surname, ind.Surname);
            }

            // Single pass over families: count families and marriages per surname
            Dictionary<string, int> familyCounts = new(comparer);
            Dictionary<string, int> marriageCounts = new(comparer);
            foreach (Family fam in ft.AllFamilies)
            {
                if (!famFilter(fam)) continue;
                HashSet<string> seenInFamily = new(comparer);
                foreach (Individual member in fam.Members)
                {
                    if (member.Surname != Individual.UNKNOWN_NAME)
                        seenInFamily.Add(member.Surname);
                }
                foreach (string surname in seenInFamily)
                {
                    familyCounts[surname] = familyCounts.GetValueOrDefault(surname) + 1;
                    if (fam.MaritalStatus == Family.MARRIED)
                        marriageCounts[surname] = marriageCounts.GetValueOrDefault(surname) + 1;
                }
            }

            // Build result list and report progress against the canonical surname count
            int total = canonicalName.Count;
            int processed = 0;
            surnames = [];
            foreach (KeyValuePair<string, string> kvp in canonicalName)
            {
                SurnameStats stat = new(kvp.Value)
                {
                    Individuals = individualCounts.GetValueOrDefault(kvp.Key),
                    Families = familyCounts.GetValueOrDefault(kvp.Key),
                    Marriages = marriageCounts.GetValueOrDefault(kvp.Key),
                };
                surnames.Add(stat);
                if (++processed % 25 == 0)
                    progress.Report((100 * processed) / total);
            }
            return [.. surnames.Distinct(new SurnameStatsComparer())];
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
