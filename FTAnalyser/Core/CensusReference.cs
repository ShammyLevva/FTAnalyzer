using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusReference
    {
        //Database online. Class: HO107; Piece: 1782; Folio: 719; Page: 25; GSU
        //Database online. Class: HO107; Piece 709; Book: 6; Civil Parish: StLeonard Shoreditch; County: Middlesex; Enumeration District: 19;Folio: 53; Page: 15; Line: 16; GSU roll: 438819.
        //Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU
        //RG14PN22623 RG78PN1327 RD455 SD10 ED13 SN183
        //Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69
        //Class: RG14; Piece: 21983
        //Class: RG14; Piece: 12577; Schedule Number: 103
        //Year: 1900; Census Place: South Prairie, Pierce,Washington; Roll: T623_1748; Page: 4B; Enumeration District: 160.
        private static readonly string EW_CENSUS_PATTERN = @"RG ?(\d{1,3})[;,] ?Piece:? ?(\d{1,5})[;,] ?Folio:? ?(\d{1,4})[;,] ?Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_PATTERN2 = @"RG ?(\d{1,3})[;,] ?Piece:? ?(\d{1,5})[;,] ?Folio:? ?(\d{1,4})";
        private static readonly string EW_MISSINGCLASS_PATTERN = @"Piece:? ?(\d{1,5})[;,] ?Folio:? ?(\d{1,4})[;,] ?Page:? ?(\d{1,3})";
        private static readonly string EW_MISSINGCLASS_PATTERN2 = @"Piece:? ?(\d{1,5})[;,] ?Folio:? ?(\d{1,4})";
        private static readonly string EW_CENSUS_PATTERN_FH = @"RG ?(\d{1,2})/(\d{1,5}) F(\d{1,4}) p(\d{1,3})";
        private static readonly string EW_CENSUS_1841_PATTERN = @"HO ?107[;,] ?Piece:? ?(\d{1,5})[;,] ?Folio:? ?(\d{1,4})[;,] ?Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1841_PATTERN2 = @"HO ?107[;,] ?Piece:? ?(\d{1,5})[;,] ?Book:? ?(\d{1,3});.*Folio:? ?(\d{1,4})[;,] ?Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1841_PATTERN3 = @"HO ?107[;,] ?Piece:? ?(\d{1,5})[;,] ?Book:? ?(\d{1,3});.*Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1841_PATTERN4 = @"HO ?107[;,] ?Piece:? ?(\d{1,5});.*Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1841_PATTERN_FH = @"HO ?107/(\d{1,5})/(\d{1,3}) .*F(\d{1,3}) p(\d{1,3})";
        private static readonly string EW_CENSUS_1911_PATTERN = @"RG ?14 ?PN(\d{1,6}) .*SN(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN2 = @"RG ?14[;,] ?Piece:? ?(\d{1,6})[;,] ?SN:? ?(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN3 = @"RG ?14[;,] ?Piece:? ?(\d{1,6});?$";
        private static readonly string EW_CENSUS_1911_PATTERN4 = @"RG ?14[;,] ?Piece:? ?(\d{1,6})[;,] ?Schedule Number:? ?(\d{1,4})";
        private static readonly string EW_CENSUS_1911_PATTERN5 = @"RG ?14[;,] ?Piece:? ?(\d{1,6})[;,] ?Page:? ?(\d{1,3})";
        private static readonly string EW_CENSUS_1911_PATTERN_FH = @"RG ?14/PN(\d{1,6}) .*SN(\d{1,4})";
        private static readonly string SCOT_CENSUS_PATTERN = @"Parish:? ?([A-Z .'-]+)[;,] ?ED:? ?(\d{1,3}[AB]?)[;,] ?Page:? ?(\d{1,4})[;,] ?Line:? ?(\d{1,2})";
        private static readonly string SCOT_CENSUS_PATTERN2 = @"(\d{3}/\d{1,2}[AB]?) (\d{3}/\d{2}) (\d{3,4})";
        private static readonly string SCOT_CENSUS_PATTERN3 = @"(\d{3}[AB]?)/(\d{2}[AB]?) Page:? ?(\d{1,4})";
        private static readonly string US_CENSUS_PATTERN = @"Year: ?(\d{4});? ?Census Place:? ?(.*)[;,] ?Roll:? ?(.*)[;,] ?Page:? ?(\d{1,4}[AB]?)";

        public enum ReferenceStatus { BLANK = 0, UNRECOGNISED = 1, INCOMPLETE = 2, GOOD = 3 };
        private static readonly string MISSING = "Missing";

        private string unknownCensusRef;
        private Fact fact;
        private string Place { get; set; }
        private string Roll { get; set; }
        private string Piece { get; set; }
        private string Folio { get; set; }
        private string Page { get; set; }
        private string Book { get; set; }
        private string Schedule { get; set; }
        private string Parish { get; set; }
        private string ED { get; set; }
        
        public bool IsUKCensus { get; private set; }
        public ReferenceStatus Status { get; private set; }

        public CensusReference(Fact fact, XmlNode node)
        {
            this.fact = fact;
            this.Roll = string.Empty;
            this.Place = string.Empty;
            this.Piece = string.Empty;
            this.Folio = string.Empty;
            this.Book = string.Empty;
            this.Page = string.Empty;
            this.Schedule = string.Empty;
            this.Parish = string.Empty;
            this.ED = string.Empty;
            this.IsUKCensus = false;
            this.Status = ReferenceStatus.BLANK;
            this.unknownCensusRef = string.Empty;
            if (GetCensusReference(node))
                unknownCensusRef = string.Empty;
        }

        private bool GetCensusReference(XmlNode n)
        {
            string text = FamilyTree.GetText(n, "PAGE");
            if (text.Length > 0)
            {
                Match matcher = Regex.Match(text, EW_CENSUS_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_PATTERN2, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = MISSING;
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.INCOMPLETE;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Folio = matcher.Groups[2].ToString();
                    this.Page = matcher.Groups[3].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN2, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Book = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN3, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Book = matcher.Groups[2].ToString();
                    this.Folio = MISSING;
                    this.Page = matcher.Groups[3].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.INCOMPLETE;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1841_PATTERN4, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Book = MISSING;
                    this.Folio = MISSING;
                    this.Page = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.INCOMPLETE;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN2, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN3, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = MISSING;
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.INCOMPLETE;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN4, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_CENSUS_1911_PATTERN5, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Page = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, SCOT_CENSUS_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Parish = matcher.Groups[1].ToString();
                    this.ED = matcher.Groups[2].ToString();
                    this.Page = matcher.Groups[3].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, SCOT_CENSUS_PATTERN2, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Parish = matcher.Groups[1].ToString().Replace("/00", "").Replace("/", "-");
                    this.ED = matcher.Groups[2].ToString().Replace("/00", "").TrimStart('0');
                    this.Page = matcher.Groups[3].ToString().TrimStart('0');
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, SCOT_CENSUS_PATTERN3, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Parish = matcher.Groups[1].ToString().TrimStart('0');
                    this.ED = matcher.Groups[2].ToString().Replace("/00", "").TrimStart('0');
                    this.Page = matcher.Groups[3].ToString().TrimStart('0');
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, US_CENSUS_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Place = matcher.Groups[2].ToString();
                    this.Roll = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                    this.IsUKCensus = false;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_MISSINGCLASS_PATTERN, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Folio = matcher.Groups[2].ToString();
                    this.Page = matcher.Groups[3].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(text, EW_MISSINGCLASS_PATTERN2, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Folio = matcher.Groups[2].ToString();
                    this.Page = MISSING;
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.INCOMPLETE;
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
            foreach (FactSource fs in fact.Sources)
            {
                Match matcher = Regex.Match(fs.SourceTitle, EW_CENSUS_1841_PATTERN_FH, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Book = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(fs.SourceTitle, EW_CENSUS_PATTERN_FH, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[2].ToString();
                    this.Folio = matcher.Groups[3].ToString();
                    this.Page = matcher.Groups[4].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
                matcher = Regex.Match(fs.SourceTitle, EW_CENSUS_1911_PATTERN_FH, RegexOptions.IgnoreCase);
                if (matcher.Success)
                {
                    this.Piece = matcher.Groups[1].ToString();
                    this.Schedule = matcher.Groups[2].ToString();
                    this.IsUKCensus = true;
                    this.Status = ReferenceStatus.GOOD;
                    return true;
                }
            }
            return false;
        }

        public string Reference
        {
            get
            {
                if(Roll.Length > 0)
                {
                    if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                        return Roll + "/" + Page;
                    else
                        return "Roll: " + Roll + ", Page: " + Page;
                }
                else if (Piece.Length > 0)
                {
                    if (Countries.IsEnglandWales(fact.Location.Country) || fact.IsOverseasUKCensus(fact.Location.Country))
                    {
                        if ((fact.FactDate.Overlaps(CensusDate.UKCENSUS1851) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1861) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1871) ||
                            fact.FactDate.Overlaps(CensusDate.UKCENSUS1881) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1891) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1901)))
                            if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                                return Piece + "/" + Folio + "/" + Page;
                            else
                                return "Piece: " + Piece + ", Folio: " + Folio + ", Page: " + Page;
                        if (fact.FactDate.Overlaps(CensusDate.UKCENSUS1841))
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
                        if (fact.FactDate.Overlaps(CensusDate.UKCENSUS1911))
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
                    if (fact.Location.Country.Equals(Countries.SCOTLAND) && (fact.FactDate.Overlaps(CensusDate.UKCENSUS1851) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1861) ||
                        fact.FactDate.Overlaps(CensusDate.UKCENSUS1871) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1881) || fact.FactDate.Overlaps(CensusDate.UKCENSUS1891) ||
                        fact.FactDate.Overlaps(CensusDate.UKCENSUS1901)))
                        if (Properties.GeneralSettings.Default.UseCompactCensusRef)
                            return Parish + Parishes.Reference(Parish) + "/" + ED + "/" + Page;
                        else
                            return "Parish: " + Parish + Parishes.Reference(Parish) + " ED: " + ED + ", Page: " + Page;
                }
                if (unknownCensusRef.Length > 0)
                    return unknownCensusRef;
                return string.Empty;
            }
        }
    }
}
