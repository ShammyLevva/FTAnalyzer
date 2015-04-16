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
        //Database online. Class: HO107; Piece: 1782; Folio: 719; Page: 25; GSU
        //Database online. Class: HO107; Piece 709; Book: 6; Civil Parish: StLeonard Shoreditch; County: Middlesex; Enumeration District: 19;Folio: 53; Page: 15; Line: 16; GSU roll: 438819.
        //Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU
        //RG14PN22623 RG78PN1327 RD455 SD10 ED13 SN183
        //Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69
        //Class: RG14; Piece: 21983
        //Class: RG14; Piece: 12577; Schedule Number: 103
        //Year: 1900; Census Place: South Prairie, Pierce,Washington; Roll: T623_1748; Page: 4B; Enumeration District: 160.
        private static readonly string EW_CENSUS_PATTERN = @"RG *(\d{1,3})[;,]? *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN2 = @"RG *(\d{1,3})[;,]? *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})";
        private static readonly string EW_CENSUS_PATTERN3 = @"(\d{4}) Census.* *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN4 = @"(\d{4}) Census.* *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})";
        private static readonly string EW_CENSUS_PATTERN5 = @"Census[: ]*(\d{4}).* *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN6 = @"Census[: ]*(\d{4}).* *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})";
        private static readonly string EW_MISSINGCLASS_PATTERN = @"Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_MISSINGCLASS_PATTERN2 = @"Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})";
        private static readonly string EW_CENSUS_PATTERN_FH = @"RG *(\d{1,2})/(\d{1,5}) F(olio)? ?(\d{1,4})[a-z]? p(age)? ?(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN_FH2 = @"RG *(\d{1,2})/(\d{1,5}) ED *(\d{1,4}[a-z]?) F(olio)? ?(\d{1,4})[a-z]? p(age)? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN = @"HO *107[;,]? *Piece:? *(\d{1,5})[;,]? *Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN2 = @"HO *107[;,]? *Piece:? *(\d{1,5})[;,]? *Book:? *(\d{1,3})[;,]?.*Folio:? *(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN3 = @"HO *107[;,]? *Piece:? *(\d{1,5})[;,]? *Book/Folio:? *(\d{1,4})[a-z]?/(\d{1,4})[a-z]?[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN4 = @"HO *107[;,]? *Piece:? *(\d{1,5})[;,]? *Book:? *(\d{1,3})[;,]?.*Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN5 = @"HO *107[;,]? *Piece:? *(\d{1,5})[;,]?.*Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH = @"HO *107/(\d{1,5})/(\d{1,3}) .*F(olio)? *(\d{1,3})[a-z]? p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH2 = @"HO *107\/(\d{1,5}) ED *(\d{1,4}[a-z]?) F(olio)? *(\d{1,4})[a-z]? p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH3 = @"HO *107/(\d{1,5}) .*F(olio)? *(\d{1,4})[a-z]?/(\d{1,4}) p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1841_51_PATTERN_FH4 = @"HO *107/(\d{1,5}) .*F(olio)? *(\d{1,4})[a-z]? p(age)? *(\d{1,3})";
        private static readonly string EW_CENSUS_1911_PATTERN = @"RG *14/? *PN(\d{1,6}) .*SN(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN78 = @"RG *78/? *PN(\d{1,6}) .*SN(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN2 = @"RG *14[;,/]? *Piece:? *(\d{1,6})[;,]? *SN:? *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN3 = @"RG *14[;,/]? *Piece:? *(\d{1,6})[;,]? *Schedule Number:? *(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN4 = @"RG *14[;,/]? *Piece:? *(\d{1,6})[;,]?$";
        private static readonly string EW_CENSUS_1911_PATTERN5 = @"RG *14[;,/]? *Piece:? *(\d{1,6})[;,]? *Page:? *(\d{1,3})";
        private static readonly string EW_CENSUS_1911_PATTERN6 = @"RG *14[;,/]? *RD:? *(\d{1,4})[;,]? *ED:? *(\d{1,3}) (\d{1,5})";
        private static readonly string EW_CENSUS_1911_PATTERN_FH = @"RG *14/PN(\d{1,6}) .*SN(\d{1,4})";
        private static readonly string SCOT_CENSUS_PATTERN = @"Parish:? *([A-Z .'-]+)[;,]? *ED:? *(\d{1,3}[AB]?)[;,]? *Page:? *(\d{1,4})[;,]? *Line:? *(\d{1,2})";
        private static readonly string SCOT_CENSUS_PATTERN2 = @"(\d{3}/\d{1,2}[AB]?) (\d{3}/\d{2}) (\d{3,4})";
        private static readonly string SCOT_CENSUS_PATTERN3 = @"(\d{3}[AB]?)/(\d{2}[AB]?) Page:? *(\d{1,4})";
        private static readonly string US_CENSUS_PATTERN = @"Year: *(\d{4});? *Census Place:? *(.*)[;,]? *Roll:? *(.*)[;,]? *Page:? *(\d{1,4}[AB]?);? *(Enumeration District:? *(.*))?";
        private static readonly string LC_CENSUS_PATTERN_EW = @"(\d{1-5})\/(\d{1-3})\/(d{1-3}) .* England & Wales (1841|1881)";
        private static readonly string LC_CENSUS_PATTERN_1911_EW = @"(\d{1-5})\/(\d{1-3}) .* England & Wales 1911";
        private static readonly string LC_CENSUS_PATTERN_SCOT = @"(\d{1-5}-?[AB12]?)\/(\d{1-3})\/(d{1-3}) .* Scotland 1881";

        public enum ReferenceStatus { BLANK = 0, UNRECOGNISED = 1, INCOMPLETE = 2, GOOD = 3 };
        public static readonly CensusReference UNKNOWN = new CensusReference();
        private static readonly string MISSING = "Missing";

        private string unknownCensusRef;
        private string Place { get; set; }
        private string Class { get; set; }
        private string Roll { get; set; }
        private string Piece { get; set; }
        private string Folio { get; set; }
        private string Page { get; set; }
        private string Book { get; set; }
        private string Schedule { get; set; }
        private string Parish { get; set; }
        private string RD { get; set; }
        private string ED { get; set; }
        private string ReferenceText { get; set; }
        private CensusLocation CensusLocation { get; set; }

        public Fact Fact { get; private set; }
        public bool IsUKCensus { get; private set; }
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
            this.ReferenceText = string.Empty;
            this.IsUKCensus = false;
            this.Status = ReferenceStatus.BLANK;
            this.unknownCensusRef = string.Empty;
            this.MatchString = string.Empty;
            this.Country = Countries.UNKNOWN_COUNTRY;
            this.URL = string.Empty;
            this.CensusYear = FactDate.UNKNOWN_DATE;
            this.CensusLocation = CensusLocation.UNKNOWN;
        }

        public CensusReference(Fact fact, XmlNode node)
            : this()
        {
            this.Fact = fact;
            if (GetCensusReference(node))
            {
                this.unknownCensusRef = string.Empty;
                this.CensusYear = GetCensusYearFromReference();
                this.CensusLocation = CensusLocation.GetCensusLocation(this.CensusYear.StartDate.Year.ToString(), this.Piece);
                this.URL = GetCensusURLFromReference();
            }
            if (fact.FactDate.IsKnown)
                this.CensusYear = fact.FactDate;
            else
                this.Fact.UpdateFactDate(this.CensusYear);
        }

        public CensusReference(string individualID, string notes, bool source)
            : this()
        {
            this.Fact = new Fact(individualID, Fact.CENSUS_FTA, FactDate.UNKNOWN_DATE, string.Empty, false, true);
            if (GetCensusReference(notes))
            {
                if (this.Class.Length > 0)
                {  // don't create fact if we don't know what year its for
                    this.unknownCensusRef = string.Empty;
                    this.CensusYear = GetCensusYearFromReference();
                    this.CensusLocation = CensusLocation.GetCensusLocation(this.CensusYear.StartDate.Year.ToString(), this.Piece);
                    this.URL = GetCensusURLFromReference();
                    this.Fact.UpdateFactDate(this.CensusYear);
                    if (source)
                        this.Fact.SetCensusReferenceDetails(this, CensusLocation, "Fact created by FTAnalyzer after finding census ref: " + this.MatchString + " in a source for this individual");
                    else
                        this.Fact.SetCensusReferenceDetails(this, CensusLocation, "Fact created by FTAnalyzer after finding census ref: " + this.MatchString + " in the notes for this individual");
                }
            }
        }

        private bool GetCensusReference(XmlNode n)
        {
            // aggressively remove multi spaces to allow for spaces in the census references
            string text = FamilyTree.GetText(n, "PAGE").Replace("   ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            return GetCensusReference(text);
        }

        private bool GetCensusReference(string text)
        {
            text = text.Replace(System.Environment.NewLine, "");
            if (text.Length > 0)
            {
                if (CheckPatterns(text))
                {
                    ReferenceText = text;
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

        private bool CheckPatterns(string text)
        {
            if (text.Length == 0)
                return false;
            Match matcher = Regex.Match(text, EW_CENSUS_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN3, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = GetCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN4, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = GetCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN5, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = GetCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN6, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = GetCensusClass(matcher.Groups[1].ToString());
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = MISSING;
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN_FH, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_PATTERN_FH2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG" + matcher.Groups[1].ToString();
                this.Piece = matcher.Groups[2].ToString();
                this.ED = matcher.Groups[3].ToString();
                this.Folio = matcher.Groups[5].ToString();
                this.Page = matcher.Groups[7].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN3, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[3].ToString(); ;
                this.Page = matcher.Groups[4].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN4, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = matcher.Groups[2].ToString();
                this.Folio = MISSING;
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN5, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Book = MISSING;
                this.Folio = MISSING;
                this.Page = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN_FH, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN_FH2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Folio = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN_FH3, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.ED = matcher.Groups[4].ToString();
                this.Page = matcher.Groups[6].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1841_51_PATTERN_FH4, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[5].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN78, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN3, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN4, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = MISSING;
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN5, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Page = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN6, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.RD = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Schedule = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = Countries.ENG_WALES;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN_FH, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, SCOT_CENSUS_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Parish = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = Countries.SCOTLAND;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, SCOT_CENSUS_PATTERN2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Parish = matcher.Groups[1].ToString().Replace("/00", "").Replace("/", "-");
                this.ED = matcher.Groups[2].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[3].ToString().TrimStart('0');
                this.IsUKCensus = true;
                this.Country = Countries.SCOTLAND;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, SCOT_CENSUS_PATTERN3, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Parish = matcher.Groups[1].ToString().TrimStart('0');
                this.ED = matcher.Groups[2].ToString().Replace("/00", "").TrimStart('0');
                this.Page = matcher.Groups[3].ToString().TrimStart('0');
                this.IsUKCensus = true;
                this.Country = Countries.SCOTLAND;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, US_CENSUS_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "US" + matcher.Groups[1].ToString();
                this.Place = matcher.Groups[2].ToString();
                this.Roll = matcher.Groups[3].ToString();
                this.Page = matcher.Groups[4].ToString();
                if (matcher.Groups.Count == 7)
                    this.ED = matcher.Groups[6].ToString();
                this.IsUKCensus = false;
                this.Country = Countries.UNITED_STATES;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, LC_CENSUS_PATTERN_EW, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                if (matcher.Groups[4].ToString().Equals("1881"))
                    this.Class = "RG11";
                else
                    this.Class = "HO107";
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, LC_CENSUS_PATTERN_1911_EW, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG14";
                this.Piece = matcher.Groups[1].ToString();
                this.Schedule = matcher.Groups[2].ToString();
                this.IsUKCensus = true;
                this.Country = GetCensusReferenceCountry(Class, Piece);
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, LC_CENSUS_PATTERN_SCOT, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Class = "RG11";
                this.Parish = matcher.Groups[1].ToString();
                this.ED = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = Countries.SCOTLAND;
                this.Status = ReferenceStatus.GOOD;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_MISSINGCLASS_PATTERN, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = matcher.Groups[3].ToString();
                this.IsUKCensus = true;
                this.Country = Countries.ENG_WALES;
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            matcher = Regex.Match(text, EW_MISSINGCLASS_PATTERN2, RegexOptions.IgnoreCase);
            if (matcher.Success)
            {
                this.Piece = matcher.Groups[1].ToString();
                this.Folio = matcher.Groups[2].ToString();
                this.Page = MISSING;
                this.IsUKCensus = true;
                this.Country = Countries.ENG_WALES;
                this.Status = ReferenceStatus.INCOMPLETE;
                this.MatchString = matcher.Value;
                return true;
            }
            return false;
        }

        private string GetCensusClass(string year)
        {
            if (Countries.IsUnitedKingdom(this.Country))
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
            }
            return string.Empty;
        }

        private FactDate GetCensusYearFromReference()
        {
            if (this.Class.Equals("HO107"))
            {
                int piecenumber = 0;
                Int32.TryParse(this.Piece, out piecenumber);
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
            if (this.Class.StartsWith("US"))
                return CensusDate.GetUSCensusDateFromReference(this.Class);
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
                if (Countries.IsUnitedKingdom(Country))
                {
                    string querystring = string.Empty;
                    if (!Country.Equals(Countries.SCOTLAND))
                    {
                        if (Piece.Length > 0)
                            querystring = @"pieceno=" + this.Piece;
                        if (Folio.Length > 0)
                            querystring = querystring + @"&folio=" + this.Folio;
                        if (Page.Length > 0)
                            querystring = querystring + @"&page=" + this.Page;
                    }
                    if (year.Equals("1841"))
                    {
                        if (this.Book.Length > 0)
                            return baseURL + @"1841&p=http://search.findmypast.co.uk/results/world-records/1841-england-wales-and-scotland-census?" + querystring + @"&book=" + this.Book;
                    }
                    else if (querystring.Length > 0)
                        return baseURL + year + @"&p=http://search.findmypast.co.uk/results/world-records/" + year + "-england-wales-and-scotland-census?" + querystring;
                }
            }
            return string.Empty;
        }

        private string GetCensusReferenceCountry(string censusClass, string censusPiece)
        {
            int piece;
            Int32.TryParse(censusPiece, out piece);
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

        public string Reference
        {
            get
            {
                if (Roll.Length > 0)
                {
                    if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                        return Roll + "/" + Page + (ED.Length > 0 ? "/" + ED : "");
                    else
                        return "Roll: " + Roll + ", Page: " + Page + (ED.Length > 0 ? ", ED: " + ED : "");
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
                    }
                }
                else if (Parish.Length > 0)
                {
                    if (Fact.Location.Country.Equals(Countries.SCOTLAND) && (Fact.FactDate.Overlaps(CensusDate.UKCENSUS1841) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1851) ||
                        Fact.FactDate.Overlaps(CensusDate.UKCENSUS1861) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1871) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1881) ||
                        Fact.FactDate.Overlaps(CensusDate.UKCENSUS1891) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1901) || Fact.FactDate.Overlaps(CensusDate.UKCENSUS1911)))
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            return Parish + Parishes.Reference(Parish) + "/" + ED + "/" + Page;
                        else
                            return "Parish: " + Parish + Parishes.Reference(Parish) + " ED: " + ED + ", Page: " + Page;
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
