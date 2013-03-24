using System;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
namespace FTAnalyzer
{
    public abstract class IGISearchForm
    {
        public const int MARRIAGESEARCH = 1;
        public const int CHILDRENSEARCH = 2;

        protected abstract void Initialise();

        protected Dictionary<string, string> parameters;
        protected TextWriter resultFile;
        protected RichTextBox rtbOutput;
        protected FactLocation defaultLocation = new FactLocation(FactLocation.SCOTLAND);
        protected IGILocation defLoc = null;
        protected int level;
        protected int resultCount = 0;
        protected int relationTypes = Individual.UNSET;
        protected string surname = "";
        protected bool surnameSearch = false;

        protected static readonly FactDate IGIMAX = new FactDate("31 DEC 1874");
        protected static readonly FactDate IGIPARENTBIRTHMAX = new FactDate("31 DEC 1860"); //if parents born after than then children are born after IGIMAX

        public void SearchIGI(Family family, string dirname, int searchType)
        {
            if (family != null)
            {
                if (family.getPreferredFact(Fact.IGISEARCH) == null && family.Husband != null && family.Wife != null)
                {   // or we have already flagged marriage fact as having been searched
                    // or either the husband or wife is not present
                    if (surnameSearch)
                    {
                        if (!family.Husband.Surname.ToUpper().Equals(this.surname) &&
                           !family.Wife.Surname.ToUpper().Equals(this.surname))
                            return; // we are doing a surname search and neither of the surnames match.
                    }
                    if (searchType == IGISearchForm.CHILDRENSEARCH)
                        ChildrenSearch(family, dirname);
                    else
                        MarriageSearch(family, dirname);
                }
            }
        }

        protected abstract void SetLocationParameters(FactLocation location);
            
        protected string verifyName(string name)
        {
            string value = name.Replace('?', ' ').Trim();
            if (value.Equals("UNKNOWN"))
            {
                value = string.Empty;
            }
            return value;
        }

        protected void setParameter(string key, string value)
        {
            if (parameters.ContainsKey(key))
                parameters[key] = value;
            else
                parameters.Add(key, value);
        }

        protected NameValueCollection getEncodedParameters()
        {
            NameValueCollection result = new NameValueCollection();
            foreach (var entry in parameters)
            {
                result.Add(entry.Key, entry.Value);
            }
            return result;
        }

        protected abstract void fixBaseURL(StringBuilder str);

        private void MarriageSearch(Family family, string dirname)
        {
            Individual husband = family.Husband;
            Individual wife = family.Wife;
            if (validRelationType(husband, wife))
            {
                string filename = dirname + "\\" + family.MarriageFilename;
                if (!File.Exists(filename))
                {
                    Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                    if (marriage == null)
                        marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
                    FactDate marriageDate = marriage.FactDate;
                    if (!marriageDate.isAfter(IGIMAX) && husband.BirthDate.isBefore(IGIMAX) && wife.BirthDate.isBefore(IGIMAX))
                    {
                        // proceed if marriage date within IGI Range and both were alive before IGI max date
                        // but don't bother processing if file already exists.
                        if (!marriageDate.isExact())
                        {
                            Initialise();
                            if (SetMarriageParameters(husband, wife))
                            {
                                List<FactLocation> locations = GetLocations(husband, wife, marriage);
                                CheckIGIAtLocations(locations, filename, IGISearchForm.MARRIAGESEARCH, null);
                            }
                        }
                    }
                }
            }
        }

        protected abstract void CheckIGIAtLocations(List<FactLocation> locations, string filename, int searchType, string surname);

        private void ChildrenSearch(Family family, string dirname)
        {
            if (family.getPreferredFact(Fact.CHILDLESS) == null)
            {
                string filename = dirname + "\\" + family.ChildrenFilename;
                if (!File.Exists(filename))
                {
                    Individual husband = family.Husband;
                    Individual wife = family.Wife;
                    if (validRelationType(husband, wife))
                    {
                        if (husband.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate && wife.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate)
                        {
                            Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                            SearchForChildren(husband, wife, marriage, filename);
                        }
                    }
                }
            }
        }

        private bool validRelationType(Individual i1, Individual i2)
        {
            int checkInd1 = i1.RelationType & relationTypes;
            int checkInd2 = i2.RelationType & relationTypes;
            return checkInd1 != 0 || checkInd2 != 0;
        }

        protected abstract void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename);

        protected abstract bool SetMarriageParameters(Individual i1, Individual i2);
    
        protected List<FactLocation> GetLocations(Individual i1, Individual i2, Fact marriage)
        {
            List<FactLocation> result = new List<FactLocation>();
            if (marriage != null && marriage.Location != null && marriage.Location.SupportedLocation(level))
            {
                FactLocation location = marriage.Location.getLocation(level);
                result.Add(location);
            }
            if (i1.BestLocation != null && i1.BestLocation.SupportedLocation(level))
            {
                FactLocation location = i1.BestLocation.getLocation(level);
                if (!result.Contains(location))
                    result.Add(location);
            }
            if (i2.BestLocation != null && i2.BestLocation.SupportedLocation(level))
            {
                FactLocation location = i2.BestLocation.getLocation(level);
                if (!result.Contains(location))
                    result.Add(location);
            }
            if (result.Count == 0)
            {   // if we have got a random text for country field then use the default country.
                //rtbOutput.AppendText("Country '" + country + "' not recognised/supported. Trying '" + defaultLocation + "' instead.\n");
                if (!result.Contains(defaultLocation))
                    result.Add(defaultLocation);
            }
            return result;
        }

        protected abstract void FetchIGIDataAndWriteResult(string filename);
    }
}
