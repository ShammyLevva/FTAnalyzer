using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTAnalyzer.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Windows.Forms;

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

        public string ChildrenBirthProfiles()
        {
            int[, ,] stats = new int[2, 20, 3];
            foreach (Family f in ft.AllFamilies)
            {
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband != null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            addAgeData(0, stats, age, child.Gender);
                        }
                        if (f.Wife != null && f.Wife.BirthDate.IsKnown)
                        {
                            Age age = f.Wife.GetAge(child.BirthDate);
                            addAgeData(1, stats, age, child.Gender);
                        }
                    }
                }
            }
            Chart chart = new Chart();
            chart.BuildChildBirthProfile(stats);
            MainForm.DisposeDuplicateForms(chart);
            chart.Show();
            return buildOutput(stats);
        }
        private void addAgeData(int parent, int[, ,] stats, Age age, string gender)
        {
            int child = gender == "M" ? 0 : (gender == "F" ? 1 : 2);
            int fiveyear = age.MinAge / 5;
            if (fiveyear >= 3 && fiveyear < 20)
                stats[parent, fiveyear, child]++;
        }

        private string buildOutput(int[, ,] minAge)
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

        public List<SurnameStats> Surnames(ToolStripProgressBar pb)
        {
            if (surnames != null)
                return surnames;
            IEnumerable<Individual> list = ft.AllIndividuals.GroupBy(x => x.Surname).Select(group => group.First());
            surnames = list.Select(x => new SurnameStats(x.Surname)).ToList();
            pb.Value = 0;
            pb.Minimum = 0;
            pb.Maximum = list.Count() + 260;
            LoadGOONS(pb);
            foreach (SurnameStats stat in surnames)
            {
                string upper = stat.Surname.ToUpper();
                stat.Individuals = ft.AllIndividuals.Where(x => x.SurnameUpper.Equals(upper)).Count();
                stat.Families = ft.AllFamilies.Where(x => x.ContainsSurname(upper)).Count();
                stat.Marriages = ft.AllFamilies.Where(x => x.ContainsSurname(upper) && x.MaritalStatus == Family.MARRIED).Count();
                pb.Value++;
                if (pb.Value % 25 == 0)
                    Application.DoEvents();
            }
            return surnames;
        }

        public void LoadGOONS(ToolStripProgressBar pb)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    for (char c = 'A'; c <= 'Z'; c++)
                    {
                        string filename = Path.GetTempFileName();
                        string website = "http://www.one-name.org/index_" + c.ToString() + ".html";
                        client.DownloadFile(website, filename);

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.Load(filename);
                        HtmlNode table = doc.DocumentNode.SelectSingleNode("//table");

                        List<HtmlNode> nodes = table.Descendants("a").ToList();
                        foreach (HtmlNode node in nodes)
                        {
                            string surname = node.InnerText.Trim();
                            SurnameStats stat = surnames.Find(x => x.Surname.Equals(surname));
                            if(stat != null)
                                stat.URI = new Uri("http://www.one-name.org" + node.GetAttributeValue("href", ""));
                        }
                        // now add links for those that are on GOONS but as a default page
                        nodes = table.Descendants("td").ToList();
                        foreach (HtmlNode node in nodes)
                        {
                            string surname = node.InnerText.Trim();
                            SurnameStats stat = surnames.Find(x => x.Surname.Equals(surname));
                            if (stat != null && stat.URI == null)
                                stat.URI = new Uri(website);
                        }
                        pb.Value+=10;
                        Application.DoEvents();
                    }
                }
            }
            catch(Exception)
            { // silently fail
            }
        }
    }
}
