using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusReference : IComparable<CensusReference>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string EW_CENSUS_PATTERN = @"RG *(\d{1,3}) *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN1 = @"RG *(\d{1,3}) *Piece\/Folio *(\d{1,5})[\/ ]*(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN2 = @"RG *(\d{1,3}) *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?)";
        private static readonly string EW_CENSUS_PATTERN3 = @"(\d{4}) Census.*? *Piece *(\d{1,5}) *Book *(\d{1,3}).*?Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN4 = @"(\d{4}) Census.*? *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN5 = @"(\d{4}) Census.*? *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?)";
        private static readonly string EW_CENSUS_PATTERN6 = @"Census *(\d{4}).*? *Piece *(\d{1,5}) *Book *(\d{1,3}).*?Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN7 = @"Census *(\d{4}).*? *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN8 = @"Census *(\d{4}).*? *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?)";

        private static readonly string EW_CENSUS_PATTERN_FH = @"RG *(\d{1,2})\/(\d{1,5}) F(olio)? ?(\d{1,4}[a-z]?) p(age)? ?(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN_FH2 = @"RG *(\d{1,2})\/(\d{1,5}) ED *(\d{1,4}[a-z]?) F(olio)? ?(\d{1,4}[a-z]?) p(age)? ?(\d{1,3})";

        private static readonly string EW_MISSINGCLASS_PATTERN = @"Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_MISSINGCLASS_PATTERN2 = @"Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?)";

        private static readonly string EW_CENSUS_1841_51_PATTERN = @"HO *107 *Piece *(\d{1,5}) *Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN2 = @"HO *107 *Piece *(\d{1,5}) *Book *(\d{1,3}).*?Folio *(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN3 = @"HO *107 *Piece *(\d{1,5}) *(Book\/)?Folio *(\d{1,3}[a-z]?)?\/?(\d{1,4}[a-z]?) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN4 = @"HO *107 *Piece *(\d{1,5}) *Book *(\d{1,3}).*?Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN5 = @"HO *107 *Piece *(\d{1,5}).*?Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH = @"HO *107\/(\d{1,5})\/(\d{1,3}) .*?F(olio)? *(\d{1,4}[a-z]?) p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH2 = @"HO *107\/(\d{1,5}) ED *(\d{1,4}[a-z]?) F(olio)? *(\d{1,4}[a-z]?) p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH3 = @"HO *107\/(\d{1,5}) .*?F(olio)? *(\d{1,4}[a-z]?)\/(\d{1,4}) p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH4 = @"HO *107\/(\d{1,5}) .*?F(olio)? *(\d{1,4}[a-z]?) p(age)? *(\d{1,3})";

        private static readonly string EW_CENSUS_1911_PATTERN = @"RG *14\/? *Piece *(\d{1,6}) .*?SN *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN78 = @"RG *78\/? *Piece *(\d{1,6}) .*?SN *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN2 = @"1911 Census.*? *Piece *(\d{1,6}) *SN *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN3 = @"Census *1911.*? *Piece *(\d{1,6}) *SN *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN4 = @"RG *14\/? *Piece *(\d{1,6})$";
        private static readonly string EW_CENSUS_1911_PATTERN5 = @"RG *14\/? *Piece *(\d{1,6}) *Page *(\d{1,3})";
        private static readonly string EW_CENSUS_1911_PATTERN6 = @"RG *14\/? *RD *(\d{1,4}) *ED *(\d{1,3}) (\d{1,5})";

        private static readonly string EW_1939_REGISTER_PATTERN1 = @"RG *101\/?\\? *(\d{1,6}[A-Z]?) *.\/?\\? *(\d{1,3}) *.\/?\\? *(\d{1,3}).+([A-Z]{4})$";
        
        private static readonly string SCOT_CENSUSYEAR_PATTERN = @"(1[89]\d[15]).{1,10}(\(?GROS *\)?)?Parish *([A-Z .'-]+) *ED *(\d{1,3}[AB]?) *Page *(\d{1,4}) *Line *(\d{1,2})";
        private static readonly string SCOT_CENSUSYEAR_PATTERN2 = @"(1[89]\d[15]).{1,10}(\(?GROS *\)?)?(\d{3}\/\d{1,2}[AB]?) (\d{3}\/\d{2}) (\d{3,4})";
        private static readonly string SCOT_CENSUSYEAR_PATTERN3 = @"(1[89]\d[15]).{1,10}(\(?GROS *\)?)?(\d{3}[AB]?)\/(\d{2}[AB]?) Page *(\d{1,4})";
        private static readonly string SCOT_CENSUSYEAR_PATTERN4 = @"SCT(1[89]\d[15])\/?(\d{3}[AB]?) *f(olio)? *(\d{1,3}[AB]?) *p(age)? *(\d{1,4})";
        private static readonly string SCOT_CENSUS_PATTERN = @"Parish *([A-Z .'-]+) *ED *(\d{1,3}[AB]?) *Page *(\d{1,4}) *Line *(\d{1,2})";
        private static readonly string SCOT_CENSUS_PATTERN2 = @"(\(?GROS *\)?)?(\d{3}\/\d{1,2}[AB]?) (\d{3}\/\d{2}) (\d{3,4})";
        private static readonly string SCOT_CENSUS_PATTERN3 = @"(\(?GROS *\)?)?(\d{3}[AB]?)\/(\d{2}[AB]?) Page *(\d{1,4})";

        private static readonly string US_CENSUS_PATTERN = @"Year *(\d{4}) *Census *(.*?) *Roll *(.*?) *Film (.*?) *P(age)? *(\d{1,4}[ABCD]?) *ED *(\d{1,5}[AB]?-?\d{0,4}[AB]?)";
        private static readonly string US_CENSUS_PATTERN1A = @"Year *(\d{4}) *Census *(.*?) *Roll *(.*?) *P(age)? *(\d{1,4}[ABCD]?) *ED *(\d{1,5}[AB]?-?\d{0,4}[AB]?)";
        private static readonly string US_CENSUS_PATTERN2 = @"Census *(\d{4}) *(.*?) *Roll *(.*?) *P(age)? *(\d{1,4}[ABCD]?) *ED *(\d{1,5}[AB]?-?\d{0,4}[AB]?)";
        private static readonly string US_CENSUS_PATTERN3 = @"Census *(\d{4}) *(.*?) *Ward *(.*?) *ED *(\d{1,5}[ABCD]?-?\d{0,4}[AB]?) *P(age)? *(\d{1,4}[AB]?)";
        private static readonly string US_CENSUS_PATTERN4 = @"Census *(\d{4}) *(.*?) *ED *(\d{1,5}[AB]?-?\d{0,4}[ABCD]?) *P(age)? *(\d{1,4}[AB]?)";
        private static readonly string US_CENSUS_1940_PATTERN = @"District *(\d{1,5}[AB]?-?\d{0,4}[AB]?).*?P(age)? *(\d{1,3}[ABCD]?).*?T627 ?,? *(\d{1,5}-?[AB]?)";
        private static readonly string US_CENSUS_1940_PATTERN2 = @"ED *(\d{1,5}[AB]?-?\d{0,4}[AB]?).*? *P(age)? *(\d{1,3}[ABCD]?).*?T627.*?roll ?(\d{1,5}-?[AB]?)";
        private static readonly string US_CENSUS_1940_PATTERN3 = @"1940 *(.*?)(Roll)? *T627_(.*?) *P(age)? *(\d{1,4}[ABCD]?) *ED *(\d{1,5}[AB]?-?\d{0,4}[AB]?)";

        private static readonly string CANADA_CENSUS_PATTERN = @"Year *(\d{4}) *Census *(.*?) *Roll *(.*?) *P(age)? *(\d{1,4}[ABCD]?) *Family *(\d{1,4})";
        private static readonly string CANADA_CENSUS_PATTERN2 = @"(\d{4}) *Census[ -]*District *(\d{1,5})\/(\d{0,4}[A-Z]{0,4}) *P(age)? *(\d{1,4}[ABCD]?) *Family *(\d{1,4})";

        private static readonly string LC_CENSUS_PATTERN_EW = @"(\d{1,5})\/(\d{1,3})\/(\d{1,3}).*?England & Wales (1841|1881)";
        private static readonly string LC_CENSUS_PATTERN_1911_EW = @"(\d{1,5})\/(\d{1,3}).*?England & Wales 1911";
        private static readonly string LC_CENSUS_PATTERN_SCOT = @"(\d{1,5}-?[AB12]?)\/(\d{1,3})\/(\d{1,3}).*?Scotland 1881";
        private static readonly string LC_CENSUS_PATTERN_1940US = @"(T627[-_])(\d{1,5}-?[AB]?)\/(\d{1,2}[AB]?-\d{1,2}[AB]?)\/(\d{1,3}[AB]?).*?US 1880";
        private static readonly string LC_CENSUS_PATTERN_1881CANADA = @"(\d{1,5})\/(\d{0,4}[A-Z]{0,4})\/(\d{0,3})\/(\d{1,3})\/?(\d{1,3})?.*?Canada 1881";

        private static Dictionary<string, Regex> censusRegexs;

        static CensusReference()
        {
            censusRegexs = new Dictionary<string, Regex>
            {
                ["EW_CENSUS_PATTERN"] = new Regex(EW_CENSUS_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN1"] = new Regex(EW_CENSUS_PATTERN1, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN2"] = new Regex(EW_CENSUS_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN3"] = new Regex(EW_CENSUS_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN4"] = new Regex(EW_CENSUS_PATTERN4, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN5"] = new Regex(EW_CENSUS_PATTERN5, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN6"] = new Regex(EW_CENSUS_PATTERN6, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN7"] = new Regex(EW_CENSUS_PATTERN7, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN8"] = new Regex(EW_CENSUS_PATTERN8, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["EW_CENSUS_PATTERN_FH"] = new Regex(EW_CENSUS_PATTERN_FH, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_PATTERN_FH2"] = new Regex(EW_CENSUS_PATTERN_FH2, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["EW_MISSINGCLASS_PATTERN"] = new Regex(EW_MISSINGCLASS_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_MISSINGCLASS_PATTERN2"] = new Regex(EW_MISSINGCLASS_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["EW_CENSUS_1841_51_PATTERN"] = new Regex(EW_CENSUS_1841_51_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN2"] = new Regex(EW_CENSUS_1841_51_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN3"] = new Regex(EW_CENSUS_1841_51_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN4"] = new Regex(EW_CENSUS_1841_51_PATTERN4, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN5"] = new Regex(EW_CENSUS_1841_51_PATTERN5, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN_FH"] = new Regex(EW_CENSUS_1841_51_PATTERN_FH, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN_FH2"] = new Regex(EW_CENSUS_1841_51_PATTERN_FH2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN_FH3"] = new Regex(EW_CENSUS_1841_51_PATTERN_FH3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1841_51_PATTERN_FH4"] = new Regex(EW_CENSUS_1841_51_PATTERN_FH4, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["EW_CENSUS_1911_PATTERN"] = new Regex(EW_CENSUS_1911_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN78"] = new Regex(EW_CENSUS_1911_PATTERN78, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN2"] = new Regex(EW_CENSUS_1911_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN3"] = new Regex(EW_CENSUS_1911_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN4"] = new Regex(EW_CENSUS_1911_PATTERN4, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN5"] = new Regex(EW_CENSUS_1911_PATTERN5, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["EW_CENSUS_1911_PATTERN6"] = new Regex(EW_CENSUS_1911_PATTERN6, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["EW_1939_REGISTER_PATTERN1"] = new Regex(EW_1939_REGISTER_PATTERN1, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["SCOT_CENSUSYEAR_PATTERN"] = new Regex(SCOT_CENSUSYEAR_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUSYEAR_PATTERN2"] = new Regex(SCOT_CENSUSYEAR_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUSYEAR_PATTERN3"] = new Regex(SCOT_CENSUSYEAR_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUSYEAR_PATTERN4"] = new Regex(SCOT_CENSUSYEAR_PATTERN4, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUS_PATTERN"] = new Regex(SCOT_CENSUS_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUS_PATTERN2"] = new Regex(SCOT_CENSUS_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["SCOT_CENSUS_PATTERN3"] = new Regex(SCOT_CENSUS_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["US_CENSUS_PATTERN"] = new Regex(US_CENSUS_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_PATTERN1A"] = new Regex(US_CENSUS_PATTERN1A, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_PATTERN2"] = new Regex(US_CENSUS_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_PATTERN3"] = new Regex(US_CENSUS_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_PATTERN4"] = new Regex(US_CENSUS_PATTERN4, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_1940_PATTERN"] = new Regex(US_CENSUS_1940_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_1940_PATTERN2"] = new Regex(US_CENSUS_1940_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["US_CENSUS_1940_PATTERN3"] = new Regex(US_CENSUS_1940_PATTERN3, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["CANADA_CENSUS_PATTERN"] = new Regex(CANADA_CENSUS_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["CANADA_CENSUS_PATTERN2"] = new Regex(CANADA_CENSUS_PATTERN2, RegexOptions.Compiled | RegexOptions.IgnoreCase),

                ["LC_CENSUS_PATTERN_EW"] = new Regex(LC_CENSUS_PATTERN_EW, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["LC_CENSUS_PATTERN_1911_EW"] = new Regex(LC_CENSUS_PATTERN_1911_EW, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["LC_CENSUS_PATTERN_SCOT"] = new Regex(LC_CENSUS_PATTERN_SCOT, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["LC_CENSUS_PATTERN_1940US"] = new Regex(LC_CENSUS_PATTERN_1940US, RegexOptions.Compiled | RegexOptions.IgnoreCase),
                ["LC_CENSUS_PATTERN_1881CANADA"] = new Regex(LC_CENSUS_PATTERN_1881CANADA, RegexOptions.Compiled | RegexOptions.IgnoreCase)
            };
        }

        public enum ReferenceStatus { BLANK = 0, UNRECOGNISED = 1, INCOMPLETE = 2, GOOD = 3 };
        public static readonly CensusReference UNKNOWN = new CensusReference();
        private static readonly string MISSING = "Missing";

        private string unknownCensusRef;
        private string Place { get; set; }
        private string Class { get; set; }
        public string Roll { get; private set; }
        public string Piece { get; private set; }
        public string Folio { get; private set; }
        public string Page { get; private set; }
        public string Book { get; private set; }
        public string Schedule { get; private set; }
        public string Parish { get; private set; }
        private string RD { get; set; }
        public string ED { get; private set; }
        public string SD { get; private set; }
        public string Family { get; private set; }
        private string ReferenceText { get; set; }
        private CensusLocation CensusLocation { get; set; }
        public Fact Fact { get; private set; }
        public bool IsUKCensus { get; private set; }
        public bool IsLCCensusFact { get; private set; }
        public ReferenceStatus Status { get; private set; }
        public FactDate CensusYear { get; private set; }
        public string MatchString { get; private set; }
        public string Country { get; private set; }
        public string URL { get; private set; }

        private CensusReference()
        {
            this.Class = string.Empty;
            this.Roll = string.Empty;
            this.Place = string.Empty;
            this.Piece = string.Empty;
            this.Folio = string.Empty;
            this.Book = string.Empty;
            this.Page = string.Empty;
            this.Schedule = string.Empty;
            this.Parish = string.Empty;
            this.RD = string.Empty;
            this.ED = string.Empty;
            this.SD = string.Empty;
            this.Family = string.Empty;
            this.ReferenceText = string.Empty;
            this.IsUKCensus = false;
            this.IsLCCensusFact = false;
            this.Status = ReferenceStatus.BLANK;
            this.unknownCensusRef = string.Empty;
            this.MatchString = string.Empty;
            this.Country = Countries.UNKNOWN_COUNTRY;
            this.URL = string.Empty;
            this.CensusYear = FactDate.UNKNOWN_DATE;
            this.CensusLocation = CensusLocation.UNKNOWN;
        }

        public CensusReference(Fact fact, XmlNode node, CensusReference pageRef = null)
            : this()
        {
            this.Fact = fact;
            if (GetCensusReference(node))
                SetCensusReferenceDetails();
            else
            {
                if (GetCensusReference(Fact.Comment))
                    SetCensusReferenceDetails();
            }
            if (fact.FactDate.IsKnown)
            {
                if (this.CensusYear.IsKnown && !fact.FactDate.Overlaps(this.CensusYear))
                    fact.SetError((int)FamilyTree.Dataerror.FACT_ERROR, Fact.FactError.WARNINGALLOW, "Census Fact dated " + fact.FactDate.ToString() + " doesn't match census reference " + Reference + " date of " + this.CensusYear);
                else
                    this.CensusYear = fact.FactDate;
            }
            else
                fact.UpdateFactDate(this.CensusYear);
            if (pageRef != null && !pageRef.IsKnownStatus && !this.IsKnownStatus)
                unknownCensusRef = pageRef.unknownCensusRef + "\n" + unknownCensusRef; 
            fact.SetCensusReferenceDetails(this, CensusLocation, string.Empty);
        }

        public CensusReference(string individualID, string notes, bool source)
            : this()
        {
            this.Fact = new Fact(individualID, Fact.CENSUS_FTA, FactDate.UNKNOWN_DATE, FactLocation.UNKNOWN_LOCATION, string.Empty, false, true);
            if (GetCensusReference(notes))
            {
                if (this.Class.Length > 0)
                {  // don't create fact if we don't know what class it is
                    SetCensusReferenceDetails();
                    this.Fact.UpdateFactDate(this.CensusYear);
                    if (source)
                        this.Fact.SetCensusReferenceDetails(this, CensusLocation, "Fact created by FTAnalyzer after finding census ref: " + this.MatchString + " in a source for this individual");
                    else
                        this.Fact.SetCensusReferenceDetails(this, CensusLocation, "Fact created by FTAnalyzer after finding census ref: " + this.MatchString + " in the notes for this individual");
                }
            }
        }

        private void SetCensusReferenceDetails()
        {
            this.unknownCensusRef = string.Empty;
            if (this.Class.Equals("SCOT"))
            {
                this.CensusLocation = CensusLocation.SCOTLAND;
                if (this.Parish.Length > 0)
                {
                    ScottishParish sp = ScottishParish.FindParish(this.Parish);
                    if (sp != ScottishParish.UNKNOWN_PARISH)
                        this.CensusLocation = new CensusLocation(string.Empty, string.Empty, sp.RD, sp.Name, sp.Region, sp.Location.ToString());
                }
            }
            else if (this.Class.StartsWith("US"))
            {
                this.CensusYear = GetCensusYearFromReference();
                if (this.Place.Length > 0)
                    this.CensusLocation = new CensusLocation(this.Place);
                else
                    this.CensusLocation = CensusLocation.UNITED_STATES;
            }
            else if (this.Class.StartsWith("CAN"))
            {
                this.CensusYear = GetCensusYearFromReference();
                if (this.Place.Length > 0)
                    this.CensusLocation = new CensusLocation(this.Place);
                else
                    this.CensusLocation = CensusLocation.CANADA;
            }
            else
            {
                this.CensusYear = GetCensusYearFromReference();
                this.CensusLocation = CensusLocation.GetCensusLocation(this.CensusYear.StartDate.Year.ToString(), this.Piece);
                this.URL = GetCensusURLFromReference();
            }
        }

        private bool GetCensusReference(XmlNode n)
        {
            string text = FamilyTree.GetText(n, "PAGE", true);
            if (GetCensusReference(text))
                return true;
            text = FamilyTree.GetNotes(n);
            return GetCensusReference(text);
        }

        private bool GetCensusReference(string text)
        {
            if (text.Length > 0)
            {
                if (CheckPatterns(text))
                {
                    ReferenceText = text.Trim();
                    return true;
                }
                // no match so store text 
                this.Status = ReferenceStatus.UNRECOGNISED;
                if (unknownCensusRef.Length == 0)
                    unknownCensusRef = "Unknown Census Ref: " + text;
                else
                    unknownCensusRef += "\n" + text;
            }
            // now check sources to see if census reference is in title page
            foreach (FactSource fs in Fact.Sources)
            {
                if (CheckPatterns(fs.SourceTitle))
                {
                    ReferenceText = fs.SourceTitle;
                    return true;
                }
                if (CheckPatterns(fs.Publication))
                {
                    ReferenceText = fs.Publication;
                    return true;
                }
            }
            return false;
        }

        public static string ClearCommonPhrases(string input)
        {
            return input.Replace(".", " ").Replace(",", " ").Replace("(", " ")
                        .Replace(")", " ").Replace("{", " ").Replace("}", " ")
                        .Replace("Registration District", "RD", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Pg", "Page", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("PN", "Piece", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Schedule No", "SN", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Schedule Number", "SN", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Schedule", "SN", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Enumeration District ED", "ED", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Enumeration District", "ED", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Sub District", "SD", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Sheet number and letter", "Page", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Sheet", "Page", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Affiliate Film Number", " ", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Family History Film", "Film ", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("FamilyHistory Film", "Film ", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Place", " ", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Family Number", "Family", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Family No", "Family", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Page Number", "Page", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Page No", "Page", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Piece Number", "Piece", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("Piece No", "Piece", StringComparison.InvariantCultureIgnoreCase)
                        .ClearWhiteSpace();
        }

        private bool CheckPatterns(string originalText)
        {
            string text = ClearCommonPhrases(originalText);
            if (text.Length == 0)
                return false;
            Match matcher = censusRegexs["EW_CENSUS_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN1"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN_FH"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN_FH2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.ED = matcher.Groups[3].ToString();
                this.Folio = matcher.Groups[5].ToString();
                this.Page = matcher.Groups[7].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[3].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN4"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[2].ToString();
                this.Folio = MISSING;
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN5"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = MISSING;
                this.Folio = MISSING;
                this.Page = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN_FH"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN_FH2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN_FH3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.ED = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1841_51_PATTERN_FH4"].Match(text);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN78"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN4"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = MISSING;
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN5"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Page = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_1911_PATTERN6"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.RD = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Schedule = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, false, Countries.ENG_WALES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Book = matcher.Groups[3].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN4"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN5"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN6"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Book = matcher.Groups[3].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN7"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_CENSUS_PATTERN8"].Match(text);
            if (matcher.Success)
            {
                this.Class = GetUKCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                SetFlagsandCountry(true, false, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_1939_REGISTER_PATTERN1"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG101";
                this.Piece = matcher.Groups[1].ToString();
                this.Page = matcher.Groups[2].ToString();
                this.Schedule = matcher.Groups[3].ToString();
                string letterCode = matcher.Groups[4].ToString();
                this.ED = CheckLetterCode(letterCode);
                SetFlagsandCountry(true, false, Countries.ENG_WALES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUSYEAR_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = CensusDate.GetUKCensusDateFromYear(matcher.Groups[1].ToString());
                this.Parish = matcher.Groups[3].ToString();
                this.ED = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUSYEAR_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = CensusDate.GetUKCensusDateFromYear(matcher.Groups[1].ToString());
                this.Parish = matcher.Groups[3].ToString().Replace("/00", "").Replace("/", "-");
                this.ED = matcher.Groups[4].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[5].ToString().TrimStart('0');
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUSYEAR_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = CensusDate.GetUKCensusDateFromYear(matcher.Groups[1].ToString());
                this.Parish = matcher.Groups[3].ToString().TrimStart('0');
                this.ED = matcher.Groups[4].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[5].ToString().TrimStart('0');
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUSYEAR_PATTERN4"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = CensusDate.GetUKCensusDateFromYear(matcher.Groups[1].ToString());
                this.Parish = matcher.Groups[2].ToString().TrimStart('0');
                this.ED = matcher.Groups[4].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[6].ToString().TrimStart('0');
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUS_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = FactDate.UNKNOWN_DATE;
                this.Parish = matcher.Groups[1].ToString().Trim();
                this.ED = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUS_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = FactDate.UNKNOWN_DATE;
                this.Parish = matcher.Groups[2].ToString().Replace("/00", "").Replace("/", "-").Replace("-0", "-");
                this.ED = matcher.Groups[3].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[4].ToString().TrimStart('0');
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["SCOT_CENSUS_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "SCOT";
                this.CensusYear = FactDate.UNKNOWN_DATE;
                this.Parish = matcher.Groups[2].ToString().TrimStart('0');
                this.ED = matcher.Groups[3].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[4].ToString().TrimStart('0');
                SetFlagsandCountry(true, false, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "ROLL");
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.ED = matcher.Groups[7].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_PATTERN1A"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "ROLL");
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                this.ED = matcher.Groups[6].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "ROLL");
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                this.ED = matcher.Groups[6].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "WARD");
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.ED = matcher.Groups[4].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_PATTERN4"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "ED");
                this.Page = matcher.Groups[5].ToString();
                this.ED = matcher.Groups[3].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_1940_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US1940";
                this.Roll = "T627_" + matcher.Groups[4].ToString();
                this.ED = matcher.Groups[1].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_1940_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US1940";
                this.Roll = "T627_" + matcher.Groups[4].ToString();
                this.ED = matcher.Groups[1].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["US_CENSUS_1940_PATTERN3"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US1940";
                this.Place = GetOriginalPlace(matcher.Groups[1].ToString(), originalText, "T627");
                this.Roll = "T627_" + matcher.Groups[3].ToString();
                this.ED = matcher.Groups[6].ToString();
                this.Page = matcher.Groups[5].ToString();
                SetFlagsandCountry(false, false, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["CANADA_CENSUS_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Class = "CAN" + matcher.Groups[1].ToString();
                this.Place = GetOriginalPlace(matcher.Groups[2].ToString(), originalText, "ROLL");
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                this.Family = matcher.Groups[6].ToString();
                SetFlagsandCountry(false, false, Countries.CANADA, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["CANADA_CENSUS_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Class = "CAN" + matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.SD = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                this.Family = matcher.Groups[6].ToString();
                SetFlagsandCountry(false, false, Countries.CANADA, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["LC_CENSUS_PATTERN_EW"].Match(text);
            if (matcher.Success)
            {
                if (matcher.Groups[4].ToString().Equals("1881"))
                    this.Class = "RG11";
                else
                    this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, true, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["LC_CENSUS_PATTERN_1911_EW"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                SetFlagsandCountry(true, true, GetCensusReferenceCountry(Class, Piece), ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["LC_CENSUS_PATTERN_SCOT"].Match(text);
            if (matcher.Success)
            {
                this.Class = "RG11";
                this.Parish = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, true, Countries.SCOTLAND, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["LC_CENSUS_PATTERN_1940US"].Match(text);
            if (matcher.Success)
            {
                this.Class = "US1940";
                this.Roll = matcher.Groups[2].ToString();
                this.ED = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                SetFlagsandCountry(false, true, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["LC_CENSUS_PATTERN_1881CANADA"].Match(text);
            if (matcher.Success)
            {
                this.Class = "CAN1881";
                this.CensusYear = CensusDate.CANADACENSUS1881;
                this.ED = matcher.Groups[1].ToString();
                this.SD = matcher.Groups[2].ToString();
                if (matcher.Groups[5].Length > 0)
                {
                    this.Page = matcher.Groups[4].ToString();
                    this.Family = matcher.Groups[5].ToString();
                }
                else
                {
                    this.Page = matcher.Groups[3].ToString();
                    this.Family = matcher.Groups[4].ToString();

                }
                SetFlagsandCountry(false, true, Countries.UNITED_STATES, ReferenceStatus.GOOD, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_MISSINGCLASS_PATTERN"].Match(text);
            if (matcher.Success)
            {
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                SetFlagsandCountry(true, false, Countries.ENG_WALES, ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            matcher = censusRegexs["EW_MISSINGCLASS_PATTERN2"].Match(text);
            if (matcher.Success)
            {
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = MISSING;
                SetFlagsandCountry(true, false, Countries.ENG_WALES, ReferenceStatus.INCOMPLETE, matcher.Value);
                return true;
            }
            return false;
        }

        private string CheckLetterCode(string letterCode)
        {
            if (letterCode.Equals("CODE"))
                return "UNKNOWN";
            //TODO: Check that the code is one of the valid codes 
            return letterCode;
        }

        private void SetFlagsandCountry(bool ukCensus, bool LCcensuFact, string country, ReferenceStatus status, string matchstring)
        {
            this.IsUKCensus = ukCensus;
            this.IsLCCensusFact = LCcensuFact;
            this.Country = country;
            this.Status = status;
            this.MatchString = matchstring;
        }

        private string GetOriginalPlace(string match, string originalText, string stopText)
        {
            int spacePos = match.IndexOf(" ");
            if(spacePos == -1)
                return match.ClearWhiteSpace();
            string startPlace = match.Substring(0, spacePos);
            int matchPos = originalText.ToUpper().IndexOf(startPlace.ToUpper());
            int stopPos = originalText.ToUpper().IndexOf(stopText);
            if (matchPos > -1 && stopPos > -1 && stopPos - matchPos > 0)
                return originalText.Substring(matchPos, stopPos - matchPos).ClearWhiteSpace();
            else
                return match.ClearWhiteSpace();
        }

        private string GetUKCensusClass(string year)
        {
            if (year.Equals("1841") || year.Equals("1851"))
                return "HO107";
            if (year.Equals("1861"))
                return "RG9";
            if (year.Equals("1871"))
                return "RG10";
            if (year.Equals("1881"))
                return "RG11";
            if (year.Equals("1891"))
                return "RG12";
            if (year.Equals("1901"))
                return "RG13";
            if (year.Equals("1911"))
                return "RG14";
            return string.Empty;
        }

        private FactDate GetCensusYearFromReference()
        {
            if (this.Class.Equals("SCOT"))
                return FactDate.UNKNOWN_DATE;
            if (this.Class.Equals("HO107"))
            {
                Int32.TryParse(this.Piece, out int piecenumber);
                if (piecenumber > 1465) // piece numbers go 1-1465 for 1841 and 1466+ for 1851.
                    return CensusDate.UKCENSUS1851;
                else
                    return CensusDate.UKCENSUS1841;
            }
            if (this.Class.Equals("RG9") || this.Class.Equals("RG09"))
                return CensusDate.UKCENSUS1861;
            if (this.Class.Equals("RG10"))
                return CensusDate.UKCENSUS1871;
            if (this.Class.Equals("RG11"))
                return CensusDate.UKCENSUS1881;
            if (this.Class.Equals("RG12"))
                return CensusDate.UKCENSUS1891;
            if (this.Class.Equals("RG13"))
                return CensusDate.UKCENSUS1901;
            if (this.Class.Equals("RG14"))
                return CensusDate.UKCENSUS1911;
            if (this.Class.Equals("RG101"))
                return CensusDate.UKCENSUS1939;
            if (this.Class.StartsWith("US"))
                return CensusDate.GetUSCensusDateFromReference(this.Class);
            if (this.Class.StartsWith("CAN"))
                return CensusDate.GetCanadianCensusDateFromReference(this.Class);
            return FactDate.UNKNOWN_DATE;
        }

        private string GetCensusURLFromReference()
        {
            if (CensusDate.IsUKCensusYear(CensusYear, true))
            {
                string year = CensusYear.StartDate.Year.ToString();
                string baseURL = @"http://www.awin1.com/cread.php?awinmid=2114&awinaffid=88963&clickref=FTA";
                if (year.Equals("1911") && Countries.IsEnglandWales(this.Country) && this.Piece.Length > 0 && this.Schedule.Length > 0)
                    return baseURL + @"1911&p=http://search.findmypast.co.uk/results/world-records/1911-census-for-england-and-wales?pieceno=" + this.Piece + @"&schedule=" + this.Schedule;
                if (year.Equals("1939") && Countries.IsEnglandWales(this.Country) && this.Piece.Length > 0 && !this.ED.Equals("UNKNOWN"))
                {
                    string dir = this.Piece.Length > 1 ? this.Piece.Substring(0, this.Piece.Length - 1) : this.Piece; //strip last letter from piece
                    return baseURL + @"1939&p=https://search.findmypast.co.uk/record?id=tna%2fr39%2f" + dir + "%2f" + this.Piece.ToLower() + "%2f" + this.Page + "%2f" + this.Schedule;
                }
                if (Countries.IsUnitedKingdom(Country))
                {
                    string querystring = string.Empty;
                    if (!Country.Equals(Countries.SCOTLAND))
                    {
                        if (Piece.Length > 0 && !Piece.Equals(MISSING))
                            querystring = @"pieceno=" + this.Piece;
                        if (Folio.Length > 0 && !Folio.Equals(MISSING))
                        {
                            string lastChar = Folio.Substring(Folio.Length).ToUpper();
                            if (!lastChar.Equals("F") && !lastChar.Equals("R") && !lastChar.Equals("O"))
                                querystring = querystring + @"&folio=" + this.Folio;
                        }
                        if (Page.Length > 0 && !Page.Equals(MISSING))
                            querystring = querystring + @"&page=" + this.Page;
                    }
                    if (year.Equals("1841") && Book.Length > 0 && !Book.Equals(MISSING))
                        return baseURL + @"1841&p=http://search.findmypast.co.uk/results/world-records/1841-england-wales-and-scotland-census?" + querystring + @"&book=" + this.Book;
                    else if (querystring.Length > 0)
                        return baseURL + year + @"&p=http://search.findmypast.co.uk/results/world-records/" + year + "-england-wales-and-scotland-census?" + querystring;
                }
            }
            return string.Empty;
        }

        private string GetCensusReferenceCountry(string censusClass, string censusPiece)
        {
            Int32.TryParse(censusPiece, out int piece);
            if (censusClass.Length > 0 && censusPiece.Length > 0 && piece > 0)
            {
                if (censusClass.Equals("HO107")) //1841 & 1851
                {
                    if (piece <= 1357)
                        return Countries.ENGLAND;
                    if (piece <= 1459)
                        return Countries.WALES;
                    if (piece <= 1462)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 1465)
                        return Countries.ISLE_OF_MAN;
                    // 1466+ is 1851 census class was still HO107
                    if (piece <= 2442)
                        return Countries.ENGLAND;
                    if (piece <= 1522)
                        return Countries.WALES;
                    if (piece <= 1526)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 2531)
                        return Countries.CHANNEL_ISLANDS;
                }
                else if (censusClass.Equals("RG9") || censusClass.Equals("RG09")) //1861
                {
                    if (piece <= 3973)
                        return Countries.ENGLAND;
                    if (piece <= 4373)
                        return Countries.WALES;
                    if (piece <= 4408)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 4432)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 4540)
                        return Countries.OVERSEAS_UK;
                }
                else if (censusClass.Equals("RG10")) //1871
                {
                    if (piece <= 5291)
                        return Countries.ENGLAND;
                    if (piece <= 5754)
                        return Countries.WALES;
                    if (piece <= 5770)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 5778)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 5785)
                        return Countries.OVERSEAS_UK;
                }
                else if (censusClass.Equals("RG11")) //1881
                {
                    if (piece <= 5216)
                        return Countries.ENGLAND;
                    if (piece <= 5595)
                        return Countries.WALES;
                    if (piece <= 5609)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 5632)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 5643)
                        return Countries.OVERSEAS_UK;
                }
                else if (censusClass.Equals("RG12")) // 1891
                {
                    if (piece <= 4334)
                        return Countries.ENGLAND;
                    if (piece <= 4681)
                        return Countries.WALES;
                    if (piece <= 4692)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 4707)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 4708)
                        return Countries.OVERSEAS_UK;
                }
                else if (censusClass.Equals("RG13")) //1901
                {
                    if (piece <= 4914)
                        return Countries.ENGLAND;
                    if (piece <= 5299)
                        return Countries.WALES;
                    if (piece <= 5308)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 5324)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 5338)
                        return Countries.OVERSEAS_UK;
                }
                else if (censusClass.Equals("RG14")) //1911
                {
                    if (piece <= 31678)
                        return Countries.ENGLAND;
                    if (piece <= 34628)
                        return Countries.WALES;
                    if (piece <= 34751)
                        return Countries.ISLE_OF_MAN;
                    if (piece <= 34969)
                        return Countries.CHANNEL_ISLANDS;
                    if (piece <= 34998)
                        return Countries.OVERSEAS_UK;
                }
            }
            return Countries.ENG_WALES;
        }

        public bool IsKnownStatus
        {
            get { return Status.Equals(ReferenceStatus.GOOD) || Status.Equals(ReferenceStatus.INCOMPLETE); }
        }

        public string Reference
        {
            get
            {
                if (Family.Length > 0)
                {
                    if (Roll.Length > 0)
                    {
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            return Roll + "/" + Page + "/" + Family;
                        else
                            return "Roll: " + Roll + ", Page: " + Page + ", Family: " + Family;
                    }
                    else
                    {
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            return ED + "/" + SD + "/" + Page + "/" + Family;
                        else
                            return "District: " + ED + ", Sub-District: " + SD + ", Page: " + Page + ", Family: " + Family;
                    }
                }
                if (Roll.Length > 0)
                {
                    if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                        return Roll + (ED.Length > 0 ? "/" + ED : "") + "/" + Page;
                    else
                        return "Roll: " + Roll + (ED.Length > 0 ? ", ED: " + ED : "") + ", Page: " + Page;
                }
                else if (Piece.Length > 0)
                {
                    if (Countries.IsEnglandWales(Fact.Location.Country) || Fact.IsOverseasUKCensus(Fact.Location.Country))
                    {
                        if ((Fact.FactDate.Overlaps(CensusDate.UKCENSUS1851) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1861) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1871) ||
                            Fact.FactDate.Overlaps(CensusDate.UKCENSUS1881) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1891) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1901)))
                            if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                return Piece + "/" + Folio + "/" + Page;
                            else
                                return "Piece: " + Piece + ", Folio: " + Folio + ", Page: " + Page;
                        if (Fact.FactDate.Overlaps(CensusDate.UKCENSUS1841))
                        {
                            if (Book.Length > 0)
                                if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                    return Piece + "/" + Book + "/" + Folio + "/" + Page;
                                else
                                    return "Piece: " + Piece + ", Book: " + Book + ", Folio: " + Folio + ", Page: " + Page;
                            else
                                if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                    return Piece + "/see image/" + Folio + "/" + Page;
                                else
                                    return "Piece: " + Piece + ", Book: see census image (stamped on the census page after the piece number), Folio: " + Folio + ", Page: " + Page;
                        }
                        if (Fact.FactDate.Overlaps(CensusDate.UKCENSUS1911))
                        {
                            if (Schedule.Length > 0)
                                if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                    return Piece + "/" + Schedule;
                                else
                                    return "Piece: " + Piece + ", Schedule: " + Schedule;
                            else
                                if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                    return Piece + "/" + Page;
                                else
                                    return "Piece: " + Piece + ", Page: " + Page;
                        }
                        if (Fact.FactDate.Overlaps(CensusDate.UKCENSUS1939))
                        {
                            if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                return "RG101/" + Piece + "/" + Page + "/" + Schedule + " (" + ED + ")";
                            else
                                return "Piece: " + Piece + ", Page: " + Page + ", Schedule " + Schedule + ", ED: " + ED;

                        }
                    }
                }
                else if (Parish.Length > 0)
                {
                    if (Fact.Location.Country.Equals(Countries.SCOTLAND) && (Fact.FactDate.Overlaps(CensusDate.UKCENSUS1841) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1851) ||
                        Fact.FactDate.Overlaps(CensusDate.UKCENSUS1861) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1871) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1881) ||
                        Fact.FactDate.Overlaps(CensusDate.UKCENSUS1891) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1901) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1911)))
                    {
                        ScottishParish sp = ScottishParish.FindParish(Parish);
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            if (sp == ScottishParish.UNKNOWN_PARISH)
                                return Parish + "/" + ED + "/" + Page;
                            else
                                return sp.Reference + "/" + ED + "/" + Page;
                        else
                            if (sp == ScottishParish.UNKNOWN_PARISH)
                                return "Parish: " + Parish + ", ED: " + ED + ", Page: " + Page;
                            else
                                return "Parish: " + sp.Reference + ", ED: " + ED + ", Page: " + Page;
                    }
                }
                else if (RD.Length > 0)
                {
                    if (Fact.Location.IsEnglandWales && Fact.FactDate.Overlaps(CensusDate.UKCENSUS1911))
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            return RD + "/" + ED + "/" + Schedule;
                        else
                            return "RD: " + RD + ", ED: " + ED + ", Schedule: " + Schedule;
                }
                if (unknownCensusRef.Length > 0)
                    return unknownCensusRef;
                if (ReferenceText.Length > 0)
                    log.Warn("Census reference text not generated for :" + ReferenceText);
                return string.Empty;
            }
        }

        public override string ToString()
        {
            return Reference;
        }

        public int CompareTo(CensusReference that)
        {
            return this.Reference.CompareTo(that.Reference);
        }
    }
}
