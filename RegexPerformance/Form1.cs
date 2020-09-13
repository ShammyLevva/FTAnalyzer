using FTAnalyzer;
using System;
using System.Windows.Forms;

namespace RegexPerformance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void BtnStart_Click(object sender, EventArgs e)
        {
            rtbOutput.Text = string.Empty;
            RunTests();
        }

        void RunTests()
        {
            var outputText = new Progress<string>(value => { rtbOutput.AppendText(value); });
            CensusReference censusRef;
            //censusRef = new CensusReference("Age: 30; Marital status: Married; Relation to Head of House: Wife", outputText);
            //censusRef = new CensusReference("St Louis Ward 17, enumeration district 0261, page 1A, dwelling number 1, family number 2, lines 10 - 14, John Roselauf household", outputText);
            //censusRef = new CensusReference("1900 U.S. census, population schedule, Missouri, City of St Louis, St Louis Ward 17, enumeration district 0261, page 1A, dwelling number 1, family number 2, lines 10 - 14, John Roselauf household; NARA microfilm publication T623, roll 896; digital image, Ancestry.com (www.ancestry.com : accessed 4 July 2011).", outputText);
            //censusRef = new CensusReference("Year: 1930; Census Place: Sea Cliff, Nassau, New York; Roll: 1462; Page: 14B; Enumeration District: 193;", outputText);
            //censusRef = new CensusReference("United States Census, 1880, database with images, FamilySearch(https://familysearch.org/ark:/61903/1:1:M4J9-Z86 : 12 August 2017), John Smith, Spring Hill, Barbour, Alabama, United States; citing enumeration district ED 13, sheet 155B, NARA microfilm publication T9 (Washington D.C.: National Archives and Records Administration, n.d.), roll 0002; FHL microfilm 1,254,002.", outputText);
            censusRef = new CensusReference(@"Mr Russell Eugene Stoner Age 58
Full Background Report Available

Current Address
Map
521 Paddock Ln
Celina, TX 75009 - 4642

Phone Numbers
(214) 732 - 4048 - Wireless
(972) 390 - 1875 - LandLine / Services
(469) 223 - 1382 - Wireless

Full Background Report(Sponsored by PeopleFinders)
Arrest Records
Court Records
Marriage & Divorce Records
Birth & Death Records
Police Records
Search Warrants
Criminal Records Data
Property Records
Bankruptcies, Judgments, Liens
Complete Background Report
View Full Background Report

Email Addresses
russell.stoner@hotmail.com

Associated Names
Eugene Stoner Russell
Russell Eugene Stoner
Russell Stoner
Show Less...
Mr Russell E Stoner
Sponsored Links

Previous Addresses
Map
1204 Aberdeen Dr
Allen, TX 75002 - 8665
(10 / 27 / 2009 - 2 / 24 / 2017)
Map
304 Fairfax Dr
Allen, TX 75013 - 3605
(2 / 25 / 2011 - 2 / 25 / 2011)
Map
2727 Bens Branch Dr, APT 321
Kingwood, TX 77339 - 3740
(11 / 30 / 2009 - 11 / 30 / 2009)
Show Less...
Map
4640 Hedgcoxe Rd
Plano, TX 75024 - 3885
(2 / 9 / 2012 - 2 / 9 / 2012)
Map
6201 Windhaven Pkwy, # PY3123
Plano, TX 75093 - 8097
(2 / 24 / 2011 - 2 / 24 / 2011)
Map
2727 Bens Branch Dr, APT 321
Humble, TX 77339 - 3740
(12 / 1 / 2009 - 12 / 27 / 2009)
Map
124 W 1480 N
Tooele, UT 84074 - 8811
(7 / 13 / 2000 - 10 / 25 / 2005)
Map
1323 Thoreau Ln
Allen, TX 75002 - 2926
(2 / 13 / 1992 - 10 / 30 / 2003)
Map
530 NE Malibu Dr
Lees Summit, MO 64064 - 2003
(1 / 1 / 1989 - 12 / 7 / 2001)
Map
539 High Meadow Dr
Allen, TX 75002
(7 / 15 / 2001 - 7 / 18 / 2001)
Map
539 High Mdw
Allen, TX 75002
(7 / 18 / 2001 - 7 / 18 / 2001)
Map
Po Box 596
Mc Kinney, TX 75070 - 8140
(7 / 13 / 2001 - 7 / 15 / 2001)
Map
Po Box 596
Mckinney, TX 75070 - 8140
(7 / 13 / 2001 - 7 / 13 / 2001)
Map
1206 Hearthstone Ct
Allen, TX 75002 - 8619
(3 / 9 / 1997 - 5 / 8 / 2001)
Map
1430 S Main St
Salt Lake City, UT 84115 - 5338
(11 / 13 / 2000 - 11 / 15 / 2000)
Map
539 Hightrail Dr
Allen, TX 75002 - 4073
(8 / 19 / 1989 - 3 / 29 / 1999)
Map
5706 Warren Ave
East Saint Louis, IL 62204 - 2924
(8 / 31 / 1985 - 12 / 1 / 1993)
Map
105 Kensington Heights Rd
Belleville, IL 62226 - 5030
(9 / 1 / 1985 - 6 / 30 / 1986)

Possible Relatives
Kathleen A Stoner, Bridgette A Stoner, Mathew G Stoner, Matthew Gregory Stoner, Alicia A Richardson, Dann Bone, Dann S Bone, Deann Ann Bone,
Show Less...
James O Bone, James Wesley Bone, Matthew G Stoner, Andie Cabral, Andrea M Cabral Perez, Arturo Cabral, Brandon L Crowson, Catherine Michele Goodner, Daniel Shad Goodner, Danielle N Champion, Danielle Champion, Dayla M Thrasher, Diane B Bradford, Felix Gale Vanhollebeke, Fernanda Cabral, Fernanda M Cabral, Fernando J Cabral, Fernando Jose Cabral, Georgia F Richards, James Bone, James I Campbell, James V Richards, James Dwayne Richardson Jr, Jonathan C Whatley Sr, Juana E Crowson, Lillie Mae Campbell, Magali G Cabral, Magali Cabral Perez, Marci G Richardson, Maria Fernanda Crowson, Patsy W Campbell, Russell A Whatley, Tara L Burton
Sponsored Links

Possible Associates
Bob T Em, Don Harold Mordecai, Gibson M Braswell, Paul C Young, Ray E Everett, Ruth Ann Mordecai, Wendi M Braswell, Jacklyn L Sellers,
Show Less...
Jaime M Henneman, James R Garza, Jonathan R Cruz, Margaret H Cruz, Marie A Stover, Mary Susan Astorga, Melissa A Griffin, Michael J Woodward, Patrick Claude Beck
Mr Russell Eugene Stoner is 58 years old.Russell's phone numbers include (214) 732-4048, (972) 390-1875, (469) 223-1382. Russell's email addresses include russell.stoner@hotmail.com.Russell's possible relatives include Kathleen A Stoner, Bridgette A Stoner, Mathew G Stoner, Matthew Gregory Stoner, Alicia A Richardson.

Russell's most recent address is 521 Paddock Ln, Celina, TX 75009-4642. Russell previously lived at 1204 Aberdeen Dr, Allen, TX 75002-8665, for 7 years. Russell previously lived at 304 Fairfax Dr, Allen, TX 75013-3605.", outputText);
        }
    }
}
