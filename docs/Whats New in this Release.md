<div class="wikidoc">
<p><font color="#0000ff">Updated release &quot;Version 5.2.0.7&quot; for project FTAnalyzer, 31th Jan 2017</font>
</p>
<p><b>New Features</b><br>
Added filter to loading GEDCOM so that badly formatted CONC or CONT text that wraps onto a line leaving invalid GEDCOM syntax is treated as a continuation of the previous line of text.
If the Census checker finds a reference but its partial don't crash
</p>
<p><font color="#0000ff">Updated release &quot;Version 5.1.0.5&quot; for project FTAnalyzer, 11th Aug 2015</font>
</p>
<p><b>New Features</b><br>
<b>Updates</b><br>
Birth and death searches now use loose birth/death to narrow range if unknown date<br>
Ancestry searches now filter on marriage date and death date<br>
Census Ref and date pattern matching is now faster<br>
<b>Bug Fixes</b><br>
Now treats Unknown locations as geocoded<br>
When loading form locations added a check to make sure it is on one of the user's screens<br>
UK addresses geocode one level below rest of world due to Scotland, England, Wales etc being the country not UK.<br>
Status text on geocoding form wasn't correctly refreshing </p>
<p><font color="#0000ff">Updated release &quot;Version 5.1.0.4&quot; for project FTAnalyzer, 4th Aug 2015</font>
</p>
<p><b>New Features</b><br>
<b>Updates</b><br>
<b>Bug Fixes</b><br>
Geocoding of US locations have an extra level<br>
Fix for 40 year death ranges on FMP if unknown dates </p>
<p><font color="#0000ff">Updated release &quot;Version 5.1.0.3&quot; for project FTAnalyzer, 2nd August 2015</font>
</p>
<p><b>New Features</b><br>
<b>Updates</b><br>
Birth and death searches now use loose birth/death to narrow range if unknown date<br>
Ancestry searches now filter on marriage date and death date<br>
Census Ref and date pattern matching is now faster<br>
<b>Bug Fixes</b><br>
<font color="#0000ff">Updated release &quot;Version 5.1.0.2&quot; for project FTAnalyzer, 1 August 2015</font>
</p>
<p><b>New Features</b><br>
<b>Updates</b><br>
<b>Bug Fixes</b><br>
Geocoding of US locations have an extra level </p>
<p><font color="#0000ff">Updated release &quot;Version 5.1.0.1&quot; for project FTAnalyzer, 25 July 2015</font>
</p>
<p><b>New Features</b><br>
<b>Updates</b><br>
<b>Bug Fixes</b><br>
Residence and census facts that had invalid years note weren't getting checked for census references in the notes<br>
Use married surnames for death searches<br>
Family Search only include birth location for birth searches<br>
Always make birth year dates use range of at least 1 year </p>
<p><font color="#0000ff">Updated release &quot;Version 5.1.0.0&quot; for project FTAnalyzer, 23 July 2015</font>
</p>
<p><b>New Features</b><br>
Added Report to show a count of relatives of each type<br>
Added BMD searching for Ancestry, FindMyPast and FamilySearch from BMD Colour Report - Double click a birth, marriage or death on the colour BMD form to auto search the web for that entry. Similar to how the census searching works.<br>
<b>Updates</b><br>
Added Option to display females with unknown surname in brackets as unknown<br>
Set search dropdown on BMDs to FreeBMD rather than FreeCen<br>
<b>Bug Fixes</b><br>
Missing fields no longer passed to FindMyPast census searches causing searches to fail<br>
Surnames box wasn't filling grid<br>
Valid Census records on Lost Cousins and colour census reports now use year comparisons rather than exact date.<br>
Census ref search wasn't picking up schedule for 1911<br>
Unknown page census refs were getting lost by unknown census refs </p>
<p><font color="#0000ff"><font color="#0000ff">Updated </font>release &quot;Version 5.0.4.2&quot; for project FTAnalyzer, 11 May 2015</font></p>
<p><b>Updates</b><br>
Added Relation filter for Surname report<br>
<b>Bug Fixes</b><br>
Loading with old database no longer fails if latm/longm columns already exist.</p>
<p><font color="#0000ff"><font color="#0000ff">Updated </font>release &quot;Version 5.0.4.1&quot; for project FTAnalyzer, 9 May 2015</font></p>
<p><b>Updates</b><br>
Now recognises lat/long places from Legacy files<br>
<b>Bug Fixes</b><br>
Someone who is married twice no longer appears twice on the census reports if on their own in a previous family<br>
Arrival and departure facts are location facts not comment facts<br>
Change IsAlive to StartsBefore instead of IsBefore<br>
Status bar text wasn't clearing on a reload</p>
<p><font color="#0000ff">New release &quot;Version 5.0.4.0&quot; for project FTAnalyzer, 5 May 2015</font></p>
<p><b>Updates</b><br>
If a census reference cut n paste from Lost Cousins is detected then a created Lost Cousins fact is added if one didn't exist.<br>
<b>Bug Fixes</b><br>
ADDR &amp; PLAC records for fact are no longer concatenated<br>
Census ref wasn't picking up from fact notes if it found text it didn't recognise in source</p>
<p><font color="#0000ff"><font color="#0000ff">Updated </font>release &quot;Version 5.0.3.2&quot; for project FTAnalyzer, 5 May 2015</font></p>
<p><b>Updates</b><br>
Add support for 1881 census - District 146/B, Page 59, Family 273 - living at Rainham, Haldimand, Ontario, Canada. style Canadian census ref.<br>
<b>Bug Fixes</b><br>
</p>
<p><font color="#0000ff"><font color="#0000ff">Updated </font>release &quot;Version 5.0.3.1&quot; for project FTAnalyzer, 5 May 2015</font></p>
<p><b>Updates</b><br>
Add support for Canadian Ancestry census references<br>
<b>Bug Fixes</b><br>
Fix typo in census recognition for Lost Cousins census references</p>
<p><font color="#0000ff">New release &quot;Version 5.0.3.0&quot; for project FTAnalyzer, 3 May 2015</font></p>
<p><b>Updates</b><br>
Step value for &quot;on this day&quot; remembered between sessions<br>
Add function to export missing census references<br>
Various updates to US census reference checks<br>
Added Scottish Parish reference -&gt; name lookup<br>
Census references for existing facts now tries to update Fact location details if fact had no location<br>
Added code to load TNG format location files<br>
Add support for 1881 Canadian LC Census pattern<br>
Add check to see if census fact year matches census reference year fact shows as error if there is a mismatch<br>
Added census ref year to facts display<br>
<b>Bug Fixes</b><br>
Lost Cousins Colour status now also checks if likely to be out of country<br>
Fix for recognising Russia from Chapman Code</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.2.4&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
<b>Bug Fixes</b></p>
<p>Only copy census references if reference is good or partial<br>
</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.2.3&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
<b>Bug Fixes</b><br>
Notes continuation line was being read then ignored<br>
Set Census ref comment if length &gt; 0<br>
When a census fact exists and a note exists with a census ref copy the census ref across if the census fact doesnt have one</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.2.2&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
Add support for Scottish Census patterns with years<br>
Add recognition of Lost Cousins 1940 US Census reference format<br>
Added recognition of 1940 US census references as cut n paste from FamilySearch, FindMyPast &amp; Ancestry<br>
<b>Bug Fixes</b><br>
Fix text lookup was aggressively looking for text even on names etc</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.2.1&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
Added support for Legacy 8 default fact types recognition<br>
Dates with ~ now supported<br>
Added support for extra Scottish Valuation Rolls<br>
Add created census facts if partial recognition<br>
Report results window now shows Census Reference details found<br>
<b>Bug Fixes</b><br>
Lots of tweaks to handling of notes to find more census references<br>
Gedcom parser now allows CONT &amp; CONC lines to start with a @<br>
fix for WWI &amp; WWII abbreviations being converted.</p>
<p><font color="#0000ff">New release &quot;Version 5.0.2.0&quot; for project FTAnalyzer</font></p>
<b>Updates</b><br>
Parent and Children facts now include birth location if known<br>
Revised handling of notes to better find census references<br>
Recognises cut n paste formatted references from FindMyPast<br>
Calculated Birth facts now show Age string used<br>
&quot;1911 Census&quot; and &quot;Census 1911&quot; patterns now recognised<br>
<b>Bug Fixes</b><br>
Census Facts now check for notes on the fact as well as in general notes to find a census reference<br>
Fixed issue with double counting LC facts with bad/missing census facts<br>
&quot;1841 Census:&quot; reference types with Book: were getting skipped by mistake
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.5&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
<b>Bug Fixes</b><br>
Facts before birth shouldn't show errors for calculated birth facts<br>
Only show missing children status where both parents are alive and at least parenting age<br>
Fix Age calc being 1 day out<br>
Fix loose births getting confused when partial dates used with no year </p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.4&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
Added File Handling option menu to allow loading files with special character handling<br>
Added support for dates of format<br>
1914 to 1918<br>
1914 until 1918<br>
(also any day month variants)<br>
Added support for C1xxx and C2xxx dates<br>
<b>Bug Fixes</b><br>
Children status of ignore wasn't ignoring all records for missing Children status report.<br>
Warnings of file errors don't warn as frequently when loading very large files<br>
Fact Dates cope better with 3 digit years<br>
Loading families no longer fails if missing REF attribute<br>
Tidied up some reload required options not forcing a reload<br>
Added missing options current settings text to report window</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.3&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
Add extra Scottish Valuation Rolls dates<br>
<b>Bug Fixes</b><br>
Check Lost Cousins Fact Location before checking related census fact location<br>
Lost Cousins facts with missing census facts now shown separately from Census facts with missing Lost Cousins facts<br>
Fix issues with Date Calculations causing crash</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.2&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
<b>Bug Fixes</b><br>
Census reference URL doesn't append F, R or O<br>
Fixed loading GEDCOMs with individuals with null IDs </p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.1&quot; for project FTAnalyzer</font></p>
<p><b>Updates</b><br>
Census References now give better locations as taken from the census piece number descriptions on FindMyPast &amp; Ancestry<br>
Calculated Birth Facts now have their own FactType<br>
Added option to exclude auto created locations from locations list<br>
Census References now displays on all fact reports<br>
Added tooltip to Census Ref column in Facts report if census reference URL available<br>
<b>Bug Fixes</b><br>
Facts reduced minimum width of sources column<br>
Unicode diacritics no longer removed for displaying locations still removed for comparisons<br>
CensusReference checking now checks across continuation lines and keeps checking if a census record already exists<br>
Don't include occupation facts in On this Day's facts<br>
Allow duplicated created census facts if there are multiple census references for a single year to highlight to user the potential issue<br>
Duplicated created census facts now show in duplicated facts report<br>
Make all census reference searches lazy<br>
Tweaks to census references to fix folio numbers with alphabetic suffixes<br>
Census year check now fixed for Census XXXX or XXXX Census references</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.1.0&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
Census References now give better locations as taken from the census piece number descriptions on FindMyPast &amp; Ancestry<br>
Calculated Birth Facts now have their own FactType<br>
Added option to exclude auto created locations from locations list<br>
Census References now displays on all fact reports<br>
Added tooltip to Census Ref column in Facts report if census reference URL available</p>
<p><font color="#0000ff">Updated release &quot;Version 5.0.0.2&quot; for project FTAnalyzer</font></p>
<p><strong>Bug Fixes</strong><br>
Fixed issue with census references with no sources passing null dates around causing load GEDCOM to fail</p>
<p><font color="#0000ff">New release &quot;Version 5.0.0.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added support for auto creating census records from identified census references in notes and source records. This will go a long way to helping users with older family tree applications that have no census facts.</p>
<p>Today Tab now shows events that happened in your tree and world events from that date or that month in the past.<br>
Both of these features come with options to turn them on or off.<br>
Census Reference reports now have clickable links to see the census page results on Find My Past website.</p>
<p><strong>Updates</strong><br>
Renewed Code Signing Certificate<br>
Census reference checks allow multiple spaces between parts<br>
Census References now don't need ; or , to match.<br>
Update count in load screen to show created census facts<br>
Allow &lt; and &gt; in dates ie: replace with BEF and AFT<br>
Added Lost Cousins My Ancestors page census reference formats<br>
Facts now display comments if available.<br>
Added new and tweaked various census reference formats<br>
Children status errors now have own error type<br>
Family facts errors now shown separately from individual fact errors.<br>
Add support for Legacy Shared Census Facts</p>
<p><strong>Bug Fixes</strong><br>
Set duplicates trackbar to only have 20 ticks<br>
Fixed issue with AFT dates messing up duplicates calcs<br>
Allow for Unicoded files<br>
Duplicate Fact report no longer reports duplicates of created facts<br>
Changed Double Date warning<br>
Colour Census double clicks now work for all bar not alive and known missing<br>
Children status patterns weren't correctly setting the alive &amp; dead status<br>
Children Status form save button now displays<br>
Children Status grid colours changed to make it easier to see errors<br>
Children Status tooltips added to highlight why cell is coloured<br>
Loose Births and deaths grids can have columns ordered by user</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.1.4&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
Added Enumeration District detection to US census ref<br>
Individuals export now exports relation to root column<br>
<strong>Bug Fixes</strong><br>
29th or higher great grandparents now show correctly in relation to root calculations</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.1.3&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
US Census ref was being loaded but not displayed<br>
Fix issue with Channel Islands not counting as part of England &amp; Wales for census</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.1.2&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
Added Enumeration District detection to US census ref<br>
<strong>Bug Fixes</strong><br>
Region shifts now only consider UK regions<br>
Files with bad data now clear data they partially loaded</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.1.1&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Fix for loose birth ranged birth year and no other facts<br>
Children status now accepts Living as alternate for Alive<br>
Red for mismatched entries was on wrong columns<br>
Report unrecognised census refs wasn't linked to button<br>
Multiple marriages was wrongly affecting loose births</p>
<p><font color="#0000ff">New release &quot;Version 4.1.1.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>Updates</strong><br>
Added ignore option to mismatched census references. This allows you to hide results where the reason is two families on same census page but different addresses.<br>
Filter button on census ref facts report allows you to show/hide census reference mismatches<br>
Added At Sea as a special country that doesn't get Google lookups so it can be used for manual setting of locations where people were born/married/died etc at sea in various parts of the world.<br>
<strong>Bug Fixes</strong><br>
Isle of Wight fact location fix</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.0.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Loose births checks persons birth is before parents die<br>
<strong>Bug fixes</strong><br>
Editing locations no longer loses it's place when reverse geocoding updates list</p>
<p><font color="#0000ff">Updated release &quot;Version 4.1.0.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Add Children status family name column for easier sorting<br>
Add check for children status total = alive &#43; dead<br>
Loose births makes sure the individual would be of marrying age<br>
<strong>Bug fixes</strong><br>
typo on census tab button<br>
inconsistent facts report title wasn't being displayed<br>
Ignored facts weren't being ignored on census inconsistent report<br>
alive/dead totals were counting children born after census as not alive thus dead.<br>
AFT dates strings don't need to say 1 JAN if only a year</p>
<p><font color="#0000ff">New release &quot;Version 4.1.0.0&quot; for project <font color="#0000ff">
FTAnalyzer</font></font></p>
<p><strong>New Features</strong><br>
Added an Inconsistent Census refs vs locations report<br>
Added 1911 census families Missing a Children Status report<br>
Added 1911 census families Mismatched Children Status report<br>
Add recognition of Ignore for Children Status<br>
Added support for Close GEDCOM file menu<br>
Added Missing Facts Filter<br>
Added Duplicate Facts Filter<br>
<strong>Updates</strong><br>
Added random surname select to Colour Census<br>
OS fuzzy search now works on missing parish names<br>
OS fuzzy search checks to see if within suitable distance of previously found Google partial/level mismatch<br>
People form now allows shift double click to see family colour census report<br>
Children Status picked up from NOTE tag as well as EVEN tag<br>
Missing Facts report is now limited to a single report fact per person and double clicking an individual gives individuals facts<br>
<strong>Bug fixes</strong><br>
Beef up error message on null pointer on load to show inner exception text<br>
Strings for spouse names were missing on mismatch report</p>
<p><font color="#0000ff">Updated release &quot;Version 4.0.0.1&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
Census refs visible on source facts report<br>
OS Geocoding sets found location and type based on what was found<br>
OS Geocoding also now uses Fuzzy search and identifies matches with OS Fuzzy search. This allows matches where the names are not exact.<br>
<strong>Bug Fixes</strong></p>
<p><font color="#0000ff">New release &quot;Version 4.0.0.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p>This major release introduces a significant new feature the ability to search the UK Ordnance Survey 50k Gazetteer for small placenames that often don't appear on a modern Google Map. This means significant numbers of locations that were previously not found
 by Google Geocoding will now be found by using OS Geocoding.<br>
In addition I've added support for loading the Scottish 1930 Parish boundary maps, whilst slightly different from the parish boundaries in use in the 1800s that are familiar to family historians most rural areas have almost the same if not identical boundaries
 in the 1800s as is in the 1930s Parish boundary set. The major variances is in the cities where several smaller parishes are amalgamated into one large parish ie: Glasgow has one large city centre parish rather than several smaller ones.<br>
Being able to see your Scottish Ancestor's Locations plotted on a map complete with parish boundaries makes it easier to see how close they were to other neighbouring parishes.<br>
NB. To use this feature you need the separate <a href="https://ftanalyzer.codeplex.com/releases/view/115135">
https://ftanalyzer.codeplex.com/releases/view/115135</a>Scottish Parish Boundary file<a href="http://ftanalyzer.codeplex.com/wikipage?title=/url">/url</a>. Simply download it and unzip it into the folder you selected from the Tools | Mapping Options form.<br>
<strong>New Features</strong><br>
Scottish 1930 Parish Boundary maps are now supported<br>
OS Open Data 50k Gazetteer can now be used to lookup placenames<br>
Added support for various countries regions to be recognised - initially this is UK, US, Canada, Australia<br>
<strong>Updates</strong><br>
Colour BMD report now has adjusted tooltips for over 90s<br>
Locations sorting is now house number agnostic<br>
Add version string to database export filename<br>
Added Regions recognition and map historic to modern counties for UK counties<br>
Added regions and alternate spellings recognitions for England, Scotland, Wales, Northern Ireland, UK Islands, USA, Canada, Australia<br>
Locations tab Treeview and other locations sub tabs now display in bold recognised regions. This should help users tidy up regions<br>
Recent Files menu now greyed out if no recent files<br>
Added Canadian Province Postal code support<br>
Added Support for Custom tag Military Service<br>
World Wars results can now be limited to just those men with known military facts<br>
Add Random Surname Census Search to Census Tab<br>
<strong>Bug Fixes</strong><br>
Updated Fact Report to correct count of distinct individuals<br>
Add null protection to treeview image nodes<br>
Prevented opening a new file during loading</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.3.4&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Fix Fact Sources trying to display marriage facts caused a crash<br>
Updated source fact count to be 1 for each person in marriage instead of 1 for each shared fact<br>
Source IDs are now zero padded same as individual and family IDs which makes sorting work as expected<br>
Fix issue for brand new users not having an empty database setup correctly</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.3.3&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Added several more countries to recognised countries list including extras like Gibraltar and Hong Kong<br>
<strong>Bug Fixes</strong><br>
Fix Fact Sources trying to display marriage facts caused a crash<br>
Updated source fact count to be 1 for each person in marriage instead of 1 for each shared fact<br>
Source IDs are now zero padded same as individual and family IDs which makes sorting work as expected</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.3.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added support for Family Historian Living flag<br>
Added data error type flagged as Living but has death date<br>
<strong>Updates</strong><br>
Tweaks to colour BMD tooltips<br>
Move option &quot;Include Locations with Partial Match Status&quot; to Mapping settings<br>
<strong>Bug Fixes</strong><br>
Fixes for Lost Cousins tab census report<br>
Changed LC facts no census to LC Facts bad/missing census<br>
Wrapped web calls in helper routine with try catch so failure to launch website will no longer crash program<br>
Census report now checks to see if census done for bold highlighting<br>
Recent File List now checks if file exists before adding to or displaying list</p>
<p><font color="#0000ff"><font color="#0000ff">Updated </font>release &quot;Version 3.7.3.1&quot; for project
<font color="#0000ff">FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Added progress bar to Surnames tab display<br>
<strong>Bug Fixes</strong><br>
Removed now redundant no country no LC fact as covered by bad/missing<br>
Footer text clear on tab switch now consistent across tabs<br>
Don’t use date filter for family groups on colour census report</p>
<p><font color="#0000ff">New release &quot;Version 3.7.3.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
Added family drop down filter to colour reports so now you can view just a single family at a time<br>
Added support for Interpreted dates<br>
Added Help menu Link to Online Guides<br>
<strong>Updates</strong><br>
Added support for Scottish Valuation Rolls as &quot;census&quot; reports<br>
Facts report now has birth date - assists identifying multiple individuals of same name<br>
<strong>Bug Fixes</strong><br>
Added country check for LC census facts<br>
Fixed report of LC facts with no country</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.2.2&quot; for project FTAnalyzer</font></p>
<p><strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Strip spaces from census references to make matching space insensitive</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.2.1&quot; for project FTAnalyzer
</font></p>
<p><strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Family facts were getting hidden with v3.7.2.0</p>
<p><font color="#0000ff">New release &quot;Version 3.7.2.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>Updates</strong><br>
Update URL for Lives of First World War to cope now the site has gone live<br>
Change FindMyPast search to work with new search<br>
Add extra tags to recognise custom events as normal facts<br>
Lost Cousins stats now shows census records with no countries and no Lost Cousins facts<br>
Added report to show Census facts missing country and Lost Cousins flag<br>
<strong>Bug Fixes</strong><br>
Restoring a v3.1.2.0 database was failing<br>
Death Location width on census can now be resized<br>
Source Facts include error facts<br>
Add Datagridview double buffering to improve flicker on grid redraw<br>
Facts now log individuals</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.1.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Shift click on census form now shows facts<br>
<strong>Bug Fixes</strong><br>
Channel Islands now recognised as part of UK for census references</p>
<p><font color="#0000ff">New release &quot;Version 3.7.1.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
Census Tab now has four new reports showing census records that have :</p>
<ul>
<li>census references </li><li>are missing census references </li><li>partial census references </li><li>unrecognised census references </li></ul>
<p>Added button to export unrecognised census facts to a text file for reporting issues with patterns that aren't being recognised<br>
<strong>Updates</strong><br>
Before 1600 don't warn about marriages pre 13 years old<br>
Census facts now count for facts before birth<br>
Census facts now used for loose births<br>
Export to Excel referrals now uses custom interface<br>
Filter out Out of Country entries in census and colour census reports<br>
Reorganised Census tab to make it tidier<br>
Tidied up census tooltip text<br>
Added help button to link to census documentation<br>
Use upper case for fact types<br>
Added option to hide missing tagged people from census reports<br>
<strong>Bug Fixes</strong><br>
Fixed out of UK but on UK census people not appearing on Lost Cousins reports<br>
Export from People form was crashing<br>
Counts for Lost Cousins facts was double counting people from UK eg: counting them as both Scotland &amp; England<br>
Fix census refs only showing for LC years<br>
Out of UK Census refs weren’t being displayed<br>
Facts before birth &amp; after death no longer appear twice<br>
Export Lost Cousins Referrals and Export Sources were crashing<br>
Fix Column sizing of census refs</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.0.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Colour census filters now show only distinct records<br>
Lost Cousins facts for overseas UK census entries now checks census fact country rather than LC country</p>
<p><font color="#0000ff">Updated release &quot;Version 3.7.0.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Lost Cousins facts won't now show orange if any UK fact is present<br>
If no preferred facts get first fact of that type - usually calculated fact</p>
<p><font color="#0000ff">New release &quot;Version 3.7.0.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added US, Canadian and Irish Colour census reports<br>
Age facts that give a calculated birth add a calculated birth fact<br>
Add support for Alias fact type<br>
Added option to display alias in name displays<br>
<strong>Updates</strong><br>
Add filter to remove someone from colour report who was never in country<br>
Show preferred fact status on fact report<br>
Armed Services and At Sea UK census now included on colour census report as part of relevant UK census - needs appropriate census ref to work<br>
Colour Census now filters out records where person is too old to be on census but death date is unknown<br>
<strong>Bug Fixes</strong><br>
Fix to Age tag with BEF dates<br>
Census refs now tolerate lack of spaces after :<br>
Schedule numbers can now be 4 digits<br>
Only add preferred facts to preferred fact list</p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.1.5&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Added support for Missing tag so users can mark people as unable to be found on a census<br>
<strong>Bug Fixes</strong><br>
Fix relation types if people married to blood/direct are also blood relations</p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.1.4&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Colour Census report now shows dark grey if person likely to be out of UK on census date<br>
<strong>Bug Fixes</strong></p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.1.3&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Added features to exclude people with unknown births from census reports<br>
<strong>Bug Fixes</strong></p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.1.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Default FamilySearch UK census searches to England if unknown country<br>
GoogleFixes.xml allows empty to strings</p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.1.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
FamilySearch set as default search provider for Lost Cousins Searches<br>
<strong>Bug Fixes</strong><br>
Tweaks to Loose Births to ignore effects of long range dates<br>
FamilySearch now understands UK Searches</p>
<p><font color="#0000ff">New release &quot;Version 3.6.1.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added Export to Excel for Treetops and World Wars Reports<br>
Added Option to show compact census references<br>
<strong>Updates</strong><br>
Add Witnesses as custom fact type so that it functions the same as Witness custom fact type<br>
<strong>Bug Fixes</strong><br>
Colour census &quot;All green&quot; now includes all with flag of green</p>
<p><font color="#0000ff">Updated release &quot;Version 3.6.0.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Colour BMD Filter two values were wrong way round<br>
Fix for Western Australia being mistaken for Washington, USA<br>
Census refs can optionally end in a ;<br>
<strong>Library Updates</strong></p>
<p><br>
<font color="#0000ff">Updated release &quot;Version 3.6.0.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Allow census references to be less specific for matching<br>
Facts that have ADDR and PLAC tags now use both<br>
<strong>Library Updates</strong></p>
<p><font color="#0000ff">New release &quot;Version 3.6.0.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added Loose Birth &amp; Death export to Excel menu items<br>
Added Lost Cousins Referrals Report<br>
Added Export Sources to Excel<br>
<strong>Updates</strong><br>
Added notice of XP support removal<br>
Locations now understand ISO 3116-1 Alpha-3 country codes<br>
Duplicate check now uses standardised name file to compare forenames thus understanding that eg:Jacobus and James are variants of the same name, even though they don't sound alike.<br>
Referrals sorts using Lost Cousins Short Codes.<br>
Duplicate checks now give heavy penalty to known different parents<br>
Add filter function to show only matching referrals<br>
Referral report now has title and status bar count<br>
<strong>Bug Fixes</strong><br>
Printng, previewing and exporting to excel no longer fails if no records listed<br>
Facts before birth and facts after death now flags as errors<br>
Locations show maps buttons weren't showing correct viewports<br>
<strong>Library Updates</strong><br>
Updated SharpMap to v1.1<br>
Updated BruTile to v0.9.8<br>
Replaced TreeViewMS with MultiSelectTreeView</p>
<p><font color="#0000ff">New release &quot;Version 3.5.1.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>Updates</strong><br>
Include UNKNOWN &amp; Reference facts<br>
Sources form columns now resizable<br>
People form now has toolbar to allow printing and exporting to Excel<br>
<strong>Bug Fixes</strong><br>
More robustly deal with facts &amp; sources so that every fact is linked to its source and vice versa.<br>
Treetops and World Wars no longer multi select<br>
Hourglass change that broke hourglass now fixed again<br>
Apply common default sorting to people form<br>
<strong>Library Updates</strong><br>
Updated Log4Net to v1.2.13<br>
Update DotNetZip to 1.9.2<br>
Update SQLLite to 1.0.92</p>
<p><font color="#0000ff">New release &quot;Version 3.5.0.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
Added sources tab - double clicking lists all facts for that source<br>
Double clicking on a fact in the facts report lists all sources for that fact<br>
Individuals reports now lists notes in GEDCOM, right click on individuals reports to view notes where they exist in the GEDCOM<br>
Extra column in Individuals reports lists whether notes exist or not.<br>
Added Possible census facts report which lists all people who have a note with the word census in it. The idea is this should help people with census records as notes work out who has a census note.</p>
<p><font color="#0000ff">New release &quot;Version 3.4.1.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>Updates</strong><br>
Added Child Born facts to facts reports so that it gives a better timeline of facts for an individual<br>
<strong>Bug Fixes</strong><br>
Fixed crash if user clicks on duplicates grid or slider etc whilst duplicates data is being calculated.<br>
Updated Hourglass function<br>
Fixed flickering on duplicates tab</p>
<p><font color="#0000ff">New release &quot;Version 3.4.0.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
Added a duplicates report - allows you to check if you have any likely duplicates in your file<br>
Added a filter to the facts report tab<br>
Added a sources report available by double clicking on a fact in the facts report<br>
<strong>Updated Features</strong><br>
Added support for Family Historian Census references<br>
Facts report now shows surname at date field<br>
Multiple individual facts reports for different individuals can now be open at the same time<br>
Shift clicking on WWI searches now searches beta version of Lives of First World War site to try to find Life story page for that individual<br>
Added support for alternate name facts they appear as extra facts in the facts report<br>
Added parent fact type to facts report<br>
<strong>Bug Fixes</strong><br>
Relation to root wasn't showing on census reports<br>
Lost cousins census report tooltips were inaccurate<br>
Lost cousins census report now has an option of a shift click to search census<br>
Fix for null locations causing duplicates crash<br>
Fixed facts reports reached by double clicking on individuals in duplicates page<br>
Fixed 1911 Family Historian census references<br>
Reports multiple fact forms for duplicate option on load<br>
Allowed 1841 Family historian census reference to be more tolerant of variants.<br>
Added select all/deselect all to fact tab<br>
Single click now works for selecting fact types to filter<br>
Unknown fact types no longer have blank descriptions</p>
<p>&nbsp;</p>
<p><font color="#0000ff">New release &quot;Version 3.3.3.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added Relation to Root description column to various reports<br>
Add support for 1901 &amp; 1911 Ancestry Irish Census searches<br>
Double date errors now have reason message.<br>
Added relationship progress bar<br>
<strong>Bug Fixes</strong><br>
Lost Cousins and Census Facts were erroring for non UK census dates<br>
Printing forms was erroneously using same title for all reports<br>
Colour reports right columns now sizable</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.8&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added support for 1921 Canadian Census</p>
<p><strong>Updates</strong><br>
<strong>Bug Fixes</strong></p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.7&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong></p>
<p>1911 Irish Lost Cousins census wasn't showing people</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.6&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
<strong>Bug Fixes</strong><br>
Colour BMD &amp; Census column widths now save/reload properly<br>
1881 LostCousins years wasn't showing correct colours</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.5&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong></p>
<p>Double dates now uses the upper year not lower year as the upper bound on a date<br>
Force database update to clean up bad viewport items<br>
Add logging for re-trying with original location text<br>
Title bar now shows which GEDCOM file you are working on</p>
<p><strong>Bug Fixes</strong></p>
<p>Updating location geocode status now correctly updates treeviews</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.4&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Updates</strong><br>
Added restore tooltip<br>
Added remember location feature to most forms<br>
Added logging for Google fixes<br>
Google fixes now load when GEDCOM loads and reports to the stats window<br>
<strong>Bug Fixes</strong></p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.3&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added option for users to use their own GoogleFixes.xml file<br>
<strong>Bug Fixes</strong><br>
Fix for edit location not restoring pre-search value if clicked no to save on exit<br>
Fix for edit location search not moving to correct location on search<br>
Google geocoding viewport was getting wrongly set</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.2&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Bug Fixes</strong><br>
Places selection now actually shows a result<br>
Fix for max living date within 9 months of MINDATE.</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.2.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
<strong>Bug Fixes</strong><br>
Fix mitre limit to avoid mitred joins on line ends<br>
Fix for saving size and position of mainform<br>
Add end cap triangles to line.<br>
Places selection now actually shows a resultFix for max living date within 9 months of MINDATE.</p>
<p><font color="#0000ff">New release &quot;Version 3.3.2.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>New Features</strong><br>
Surname tab - reports counts of individuals, families &amp; marriages in a family.<br>
Also links to Guild of One Name Studies site where surname is a GOONS study<br>
<strong>Bug Fixes</strong><br>
Fix dates with huge numbers of spaces<br>
Count of not searched locations was wrong</p>
<p><font color="#0000ff">New release &quot;Version 3.3.1.0&quot; for project <font color="#0000ff">
FTAnalyzer</font> </font></p>
<p><strong>Updates</strong><br>
Lifelines Report now highlights birth facts (red teardrop), death facts (black teardrop) and currently selected fact (green teardrop).<br>
Changing the selected fact highlights the point on the map.<br>
Also works the other way using the query tool you can select a fact and see it highlighted on the map<br>
Tooltip also now shows the facts beneath the mouse.<br>
<strong>Bug Fixes</strong><br>
All times birth facts and death facts are looked at the rule to use christening/burial facts is implemented</p>
<p><font color="#0000ff">Updated release &quot;Version 3.3.0.1&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added Places form - You can now select a place or places and see who lived there on a map. Discover different branches of your family living in the same area perhaps uncover new leads.<br>
Clicking on a cluster or teardrop icon on new places map shows you who was at that location<br>
Database Upgrade to use projected LatM/LongM instead of lat/long this will make map drawing quicker.
<strong>It does mean that the first time the program loads it will need to update your database. PLEASE LET THIS FINISH.</strong><br>
<strong>Updates</strong><br>
Facts table now displays Relation<br>
Facts table now shows grey/white bands per individual<br>
Added TileCaches to maps<br>
Added Birth Reg, Marriage Reg and Death Reg custom facts as alternates for Birth, Marriage, Death facts<br>
Geocode locations filter menu now has icons<br>
MainForm now remembers position as well as size<br>
Hide Scale bar is now a global option on all maps<br>
Added support for Google Country/Region/SubRegion &amp; MultiLevel text substitution in FactLocationFixes.xml file this will translate old 19th Century locations to modern ones that Google can find without needing to touch the GEDCOM file.<br>
Added support for 10 APR-15 JUL 1918, 9-17 JUL 1824 and 10 APR 1914-15 APR 1918 style dates<br>
Places &amp; Lifeline remembers splitter positions<br>
ADDR tags in GEDCOM are now recognised as locations if the PLACe tag is empty<br>
<strong>Bug Fixes</strong><br>
Report Options reports all options now<br>
Fact Source text that was on a continuation line in the GEDCOM was missing text on subsequent lines<br>
Tweaks to Parent Age Profile report<br>
Facts form row selector was too narrow<br>
Lifeline expands to include the viewport as well as the coordinate eg: This should mean Scotland is no longer zoomed in to a point on Loch Tay<br>
Database restore now forces update if restoring an old database<br>
Added titles to all messageboxes<br>
Map Individuals Icons refreshed on edit location<br>
Clicking to open locations window always ensures that location isn't filtered<br>
If a file encounters too many errors and user says to quit it will now fail gracefully</p>
<p><font color="#0000ff">New release &quot;Version 3.2.0.0&quot; for project FTAnalyzer</font></p>
<p><strong>Major Features</strong><br>
Added Lifelines map - shows where ancestors lived with lines for how they moved over time. Allows selection of one or more individuals<br>
Added ability to display custom maps - <a href="https://ftanalyzer.codeplex.com/wikipage?title=Adding%20Custom%20Maps">
see documentation for details</a><br>
Added ability to display English &amp; Welsh parish boundaries on a map - <a href="http://ftanalyzer.codeplex.com/wikipage?title=Displaying%20England%20and%20Wales%20Parish%20Boundaries">
see documentation for details</a><br>
Added Facts Report this shows all facts recorded in your database for all individuals<br>
<strong>Lifelines Map</strong><br>
Shows lists of people and where they move in time<br>
Right click a person to select all family their members, ancestors, descendants or all relatives<br>
Displays Google &amp; geocoding data in lifeline facts<br>
Double click any lifeline fact to enable location editing for that fact location<br>
Added a save &amp; exit button to edit locations<br>
Added option to hide labels<br>
<strong>Updates</strong><br>
Relations check only now sets direct ancestor for natural parent ie: step/adopted etc parents<br>
Added remaining recognised countries<br>
When geocoding if no Google Match with country, county, region string try again with GEDCOM Location<br>
Add option to reset partials<br>
Add event to location editing to refresh lifeline facts<br>
<strong>Bug Fixes</strong><br>
Fix bug with valid double dates with double date year ending in 00<br>
Fixed double dates isbefore, startsbefore, isafter, endsafter<br>
Remove &quot;fix&quot; for Durham as it was causing towns in Durham to appear in Durham, Durham.<br>
Retried partial Google Matches now works<br>
Fixed locations tab bold text for known countries<br>
Lost Cousins census check in coloured reports now checks census country before flagging yellow.<br>
Exiting edit location checks for saved point if yes to save rather than search moving point.<br>
Turn off auto size mode on source column in facts<br>
Fix surname search for facts<br>
If GEDCOM CHAR set is UTF-8 then use the UTF-8 encoding</p>
<p><font color="#0000ff">New release &quot;Version 3.1.2.0&quot; for project FTAnalyzer </font>
</p>
<p><strong>New Features</strong><br>
Added database restore option<br>
Allow for natural/step/adopted etc relationships (uses _MREL &amp; _FREL tags)<br>
Added support for 3 digit years (year only)<br>
Added count of unknown facttypes on load<br>
Data Errors now show unknown fact types<br>
Save main form's size on close<br>
Reverse Geocoding now uses zoom level to pick place<br>
Logger added creates a FTAnalyzer.log file for reverse geocoding info and future debugging support<br>
<strong>Bug fixes</strong><br>
Fact dates tolerate spaces between dashes<br>
Force Treeview font to bold or regular to hopefully fix truncation issues</p>
<p>&nbsp;</p>
<p><font color="#0000ff">New release &quot;Version 3.1.1.0&quot; for project FTAnalyzer</font></p>
<h3>Edit Location Enhancements</h3>
<p>Double clicking now zooms in (left) or out (right)<br>
New Search box jumps to locations entered if Google can identify it<br>
New Edit button allows you to pick up marker even if its not on current zoom level<br>
Reset button will reset the location back to close to how it was on load<br>
Added option to copy and paste locations - useful when you have several locations in same street/place and you don't want to manually edit them all<br>
Marking a location as verified or editing a location now updates the Google location by reverse geocoding<br>
Added check for Ancestry 1911 census references<br>
Tweaks to Google Georeferencing levels<br>
Fixed map issue when behind a proxy<br>
Understands GEDCOM DEATH Y tags used when death date/location unknown but it is known the individual is dead.<br>
Added support for MON YYYY-MON YYYY format dates</p>
<p>&nbsp;</p>
<p><font color="#0000ff">New release &quot;Version 3.1.0.0&quot; for project FTAnalyzer</font></p>
<h4>New Features</h4>
<p>The Mapping feature can now view your locations using <strong>Historic OS Maps</strong> and modern OS Maps.</p>
<h4>Updates</h4>
<p>Map location list now shows Google location<br>
Backup of Geocodes database remembers last backup location<br>
Map selector remembers the last map used</p>
<h4>Bugfixes</h4>
<p>Don't add pre-marriage individuals if already in a family</p>
<p><font color="#0000ff">New release &quot;Version 3.0.3.0&quot; for project FTAnalyzer</font></p>
<p>Count Isle of Man as England &amp; Wales for Census<br>
Lost Cousins text was preventing drag and drop of files<br>
Lost Cousins no Countries form won’t open more than one copy<br>
Changed sort order of Lost Cousins buttons to match report which matches Lost Cousins website<br>
UK Census date verification is only applied to UK facts<br>
Counts duplicate Lost Cousins facts<br>
Added report for Lost Cousins facts but no census fact<br>
Added Census Duplicate and Census Missing Location reports<br>
Lost Cousins fact lists don't show people with no country<br>
Flag census families where checking treetops person before they married as &quot;Pre-Marriage&quot;<br>
Census facts were getting confused when user had BEF or AFT dates<br>
People who born or die on a census date are included in census families<br>
Cell style changes applied to Inherited style as a performance gain<br>
Census grid refreshes on change of provider thus updating tooltips<br>
First fact of a fact type for an individual is recorded as the preferred fact<br>
Individuals report now has a double click for facts<br>
People form shows proper split grid and scrollbars and now has double click for facts</p>
<p><font color="#0000ff">New release &quot;Version 3.0.2.4&quot; for project FTAnalyzer</font></p>
<p>Added Lost Cousins missing country report<br>
Tweaked Loose Births to take account of childrens ages<br>
Fixed bug with export to excel from census report</p>
<p><font color="#0000ff">New release &quot;Version 3.0.2.3&quot; for project FTAnalyzer</font></p>
<p>Fixed bug with census dates always being 1881.<br>
Lost Cousins tab now has a proper Relation Types filter<br>
Lost Cousins tab buttons have lost weight<br>
Add Individual ID to facts list<br>
Initial work on reverse geocoding - doesn't do anything with the results it finds yet</p>
<p><font color="#0000ff">New release &quot;Version 3.0.2.2&quot; for project FTAnalyzer</font></p>
<p>Fixed Select All/Clear All<br>
On finished Geocoding display results on stats tab<br>
Added terms of use links to maps<br>
Unknown fact types now display fact type description from GEDCOM<br>
Custom facts were erroneously checking for Birth/Marriage/Death text<br>
Reset any User entered/verified locations to set the Google location to empty</p>
<p><font color="#0000ff">New release &quot;Version 3.0.2.1&quot; for project FTAnalyzer</font></p>
<p>Added right click menus to census, Lost Cousins, and both colour reports to view facts for individuals.<br>
Fixed issue with 1851 &amp; 1901 census dates for colour report not working due to matching Canadian census dates<br>
Tolerated census dates now set the adjusted year<br>
Family facts weren't getting checked for data errors when added to facts list</p>
<p><font color="#0000ff">New release &quot;Version 3.0.2.0&quot; for project FTAnalyzer</font></p>
<p>Using Google Maps Geocoder v3 - <strong>NB. your old Google Matched results will be reset</strong><br>
Added Use Burial/Cremation dates option if no death date<br>
Fix US Census that were inaccurate dates<br>
Add option to set minimum parental age for loose births<br>
Added Select All/Clear All to Geocode status<br>
Colour BMDs won't now show red for people still likely to be alive<br>
Added extra 4 year double date option eg: 11 Mar 1747/1748 is now recognised<br>
Grey/white banding on census report now groups by sort column<br>
Added recent files list feature<br>
Added extra map background tiles to Timeline<br>
Fixes to loose births to use death fact when calculating earliest living date<br>
Loose Births &amp; Deaths sorted by surname, forename</p>
<p><font color="#0000ff">New release &quot;Version 3.0.1.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added a Loose Births report<br>
Added ability to backup Geocoding database<br>
Added option to only apply family census facts to parents<br>
People on a census outside UK now show as such in census report (includes &quot;equivalent&quot; US census year eg: 1880 for 1881 UK).<br>
<strong>Bug Fixes</strong><br>
Fix check for not<em>searched and partial</em>match not re-checking Google.</p>
<p>&nbsp;</p>
<p><font color="#0000ff">New release &quot;Version 3.0.1.0&quot; for project FTAnalyzer</font></p>
<p><strong>New Features</strong><br>
Added a Loose Births report<br>
Added ability to backup Geocoding database<br>
Added option to only apply family census facts to parents<br>
People on a census outside UK now show as such in census report (includes &quot;equivalent&quot; US census year eg: 1880 for 1881 UK).</p>
<p>&nbsp;</p>
<p><font color="#0000ff">New release &quot;Version 3.0.0.2&quot; for project FTAnalyzer </font>
</p>
<div id="ecxNotificationBody">
<div>
<h1>New Mapping Feature</h1>
Version 3 introduces a new mapping feature that allows you to see your ancestors plotted on a modern Google Map.<br>
<br>
This works by searching Google Maps for the locations in your GEDCOM and storing the results. This process is known as Geocoding. The program can then plot those results on various types of map.
<br>
<br>
In v3.0.0.0 there is one initial Timeline map showing where everyone in your file was in a particular year.<br>
<h2>New Features since v2.2.1.1</h2>
Added Geocoding of locations in file from Google Maps. This new form is also the report showing the list of locations and how they were geocoded.<br>
Added display of individuals on Google Map as a Timeline view <br>
What's New menu links to What's New page on website.<br>
Map Locations can be edited by the user if the Google Geocoding wasn't correct.<br>
<h2>Updates</h2>
Locations now shows Lat/Long/Geocoded status, Google results, and status icon<br>
Locations treeview now has icons to show geocoding status<br>
Ancestry.co* merge data for censuses now parsed on loading<br>
Dataerror check box status remembered between sessions<br>
Added option to search again for not found items<br>
Added web proxy detection if behind a proxy serverMap refreshes on pan and year change<br>
Map Individuals list now has double click to see facts for person<br>
Added filters to Geolocations form<br>
Added extra birth fact to individual if fact has age but no birth date recorded<br>
Census form (for census and Lost cousins) is now fully sortable<br>
<br>
<strong>Geocoding</strong><br>
If too many Google Queries then stop processing<br>
Now defaults to UK Google geocoding<br>
If a location isn't geocoded instead of ignoring it on timeline try at next level.<br>
Geocoding respects bounding boxes of countries to avoid a similarly named place in a different country getting picked up.<br>
<br>
<strong>Edit Locations</strong><br>
Added Save and reset map buttons<br>
Sort order preserved on refreshing edited locations<br>
Add tooltip to tell user how to edit location on GeoLocations form<br>
Change Edit Location to place pointer using right mouse click<br>
<br>
<strong>Timeline</strong><br>
Added playback option and associated play/stop buttons.<br>
Button selected highlight added to pointer button<br>
Added Option to lock zoom level<br>
Solo person shown with Google teardrop<br>
Clusterer added - this means as you zoom out data is clustered together as you zoom in you see the individual dots. The selection tool allows you to select an icon and view everyone where that is their best location in that year.<br>
Timeline map ignores those born more than 110 years at timeline date.<br>
Add option to display all locations<br>
Timeline step change is 5 if range is 150 years or less<br>
Implemented fact date limits in timeline<br>
Added map scalebar to timeline map<br>
<br>
<strong>Lost Cousins</strong><br>
Added 1940 US Census button to form<br>
Form now displays count by country and year as per Lost Cousins website<br>
Count also now shows number of people you have a census for but no Lost Cousins fact<br>
Lost Cousins buttons wasn't respecting country chosen<br>
Show Lost Cousins facts after death as a data error<br>
Lost Cousins report displays the census reference info if it is found in a source<br>
<br>
<strong>Bug Fixes</strong><br>
Colour census death now uses ends before<br>
IsMarried now uses starts before<br>
Fix bug with locations not opening for places tab<br>
Fix bug with locations for less detailed levels not showing if on more detailed tab<br>
Fixed Google Maps button not showing map<br>
Bing &amp; GoogleMaps now don't bother geo-coding if it already has the location geocoded<br>
Image in treeview doesn't change when selected<br>
Fix tooltip zoom icon tooltip text<br>
Fixed bugs with Census &amp; Lost Cousins forms headings<br>
Fixed Census &amp; Lost Cousins forms ignoring census records for people with no parents</div>
</div>
<p>&nbsp;</p>
<p><span style="color:#0000ff">New release &quot;Version 2.2.1.1&quot; for project FTAnalyzer</span><br>
<br>
Fix for 1Q style quarter days<br>
<br>
<span style="color:#0000ff">New release &quot;Version 2.2.1.0.&quot; for project FTAnalyzer</span><br>
<br>
Colour BMD, Colour Census, Facts Report &amp; Census all now :<br>
use custom data grids<br>
have export to excel<br>
have printing options including colours and icons<br>
have sortable columns<br>
save/load/reload column order, sort order and column widths<br>
Census facts on family are now included in all individuals in family alive at time of census<br>
Changing Use Residence facts as census facts now requires a reload to force update of data errors<br>
Status bar message on main form now shows what actions can happen on a double click<br>
First tentative search Ancestry.co.uk code on double click BMD.</p>
<p><span style="color:#0000ff; font-size:10pt">New release &quot;Version 2.2.0.1.&quot; for project FTAnalyzer</span></p>
<p>Residence facts location checked for census date warning.<br>
Do not prompt to reload file if no settings are changed.<br>
Fix for unknown death dates not being checked for loose deaths<br>
Added Reload option to main form<br>
Added missing BMD report filters<br>
Speedup loading of colour reports fix sort order<br>
Various extra countries recognised<br>
All standalone forms now implement custom column ordering<br>
Printing now allows icon printing and colours printing</p>
<p>&nbsp;</p>
<p><span style="color:#0000ff">New release &quot;Version 2.2.0.0.&quot; for project FTAnalyzer</span></p>
<h3>New Features in v2.2.0.0</h3>
<p>Coloured Census form now moved to separate tab &quot;Search Summaries&quot;<br>
Added BMD colour report to &quot;Search Summaries&quot; tab</p>
<ul>
<li>this means you can now see BMD &amp; Census at a glance to see what data you are missing
</li><li>Plus for marriage facts it shows colours for no partner but of marriage age, no partner &#43; children or partner but no marriage
</li></ul>
<p>Facts now checks for valid census dates<br>
Added option to tolerate Census dates where the year is right but the exact date is wrong these are accepted if the census year is valid<br>
Counts of errors and warnings for Census, Residence and Lost Cousins facts shown on load</p>
<h3>Small Changes to functionality</h3>
<p>Facts List now displays sources<br>
Added Age at Fact to Facts display<br>
Added Select All/Clear All to data errors list<br>
Data Errors are now sortable and have an error type column<br>
Added Census date range too wide error type<br>
Moved Treat Residence Facts as Census facts to a Global option<br>
Add support for 1921 Canada Census<br>
Added a report an issue link to help menu<br>
Treeview now allows map views buttons to work<br>
Fact Form now shows error facts for an individual too with error icons and tooltips for reason<br>
Various minor Bug Fixes</p>
</div><div class="ClearBoth"></div>
