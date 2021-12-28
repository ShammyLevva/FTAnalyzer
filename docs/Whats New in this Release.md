### Updated Release Version 8.4.0.1 to be released
**Updates**  
Allow auto searching on BMDs even if exact date  

**Bugfixes**  
Altered grid resize to force to account for size of scrollbars on different resolutions  
Fixed FamilySearch searches given website changes  

#### Updated Release Version 8.4.0 26th December 2021
**Updates**  
Temp XML file is now unloaded after initial processing freeing up memory  
Initial Load now says how much memory was used during loading and procesing  
Updated packages to latest versions including BruTile & NewtonsoftJSON  
Changed all main form grids to use virtual grids this should make loading faster  
Duplicate checking radically overhauled thanks to assistance from Tamura Jones and Enno Borgsteede  
Added support for England and Wales 2021 census  
Removed 1931 England and Wales as census as it was destroyed  
Added feature to facts tab to filter on alternate/preferred facts  
Custom Facts tab now shows families who have a custom fact  
Added export of geocoded locations to Google MyMaps KML format  

**Bugfixes**  
Remapped sizes of all controls on main form to hopefully eliminate the scroll bar bug  
Duplicates exclude those with unknown names  
Icon sizes now scale if using a high resolution monitor  
Forced update of SQLite.Interop.dll files  
Loose deaths for fathers now limit to 1st of month rather than 1st January of year of oldest child  
Cope with missing characterset in GEDCOM header  
1940 US census is allowed to be a reference for a 1935 residence  
Dont overwrite a good census reference if more than one per fact  
Added routine to force trim grids to maximum window size  

#### Updated Release Version 8.3.1 16th March 2021  
**Updates**  
Added display of 1950 US census support in preparation for release on 1st April 2022   
Updated to NetTopologySuite 2.2.0  

**Bugfixes**  
Fixed duplicate display of facts in places and lifelines  
Fixed missing FULLNAME field in beta testers database  
Fixed issue where UK in location became England, England  
Added message where no one in file is at location  
Fixed issue of colour census reports not showing correctly when alternate facts off  
Fixed issue of created facts creating for dates when individual wasn't alive  
Hopefully fixed the installer uninstall first issue  

#### Updated Release Version 8.3.0 11th February 2021
**Bugfixes**  
Fixed bad bug causing skipping of preferred facts  

#### Updated Release Version 8.2.0 6th February 2021
**Updates**  you
Add option to include processing of alternate facts  
Display AKA name facts as ALIAS facts  
Allow loading of bad Ancestry GEDCOM bug with @@? tags on source references  

#### Updated Release Version 8.1.2 3rd February 2021
**Updates**  make 
Added support for US census references 1790-1890 using Mxxx reference format  

**Bugfixes**  
Reworded the check census reference option  
Fix on this day not loading world events  
1911 Child report shouldn't show families married after 1911  
Fix 1940 US census as default for Txxx references  

#### Updated Release Version 8.1.1 29th January 2021
**Bugfixes**  
Fixed missing icon for desktop shortcuts  

#### Updated Release Version 8.1.0 28th January 2021
**Updates**  
Added timer showing time to load file and time to analyse file  
Check footnotes in Ancestry source records for census references  
Add extra US Census reference checks from Ancestry  
Change Ancestry US Census links to use new databases  
Add support for 1921 UK Census to Research Suggestions in preparation for release soon  
Recognise alternate name facts as alias if no alias fact  
Recognise Windows 10 20H2  
  
**Bugfixes**  
Catch Database save to invalid location error  
Updated Lost Cousins tooltip to indicate not year or country  
If still living then max age at death should be age today  
Solo individual with no children fixed in colour BMD report  
Strip &nbsp; from line during XML parsing to check for spaces  

#### Updated Release Version 8.0.1 6th September 2020  
**Updates**  
GEDCOM processing errors now show last name seen in GEDCOM file to aid diagnosis  

**Bugfixes**
Census references as a entry on the CENS line weren't getting recognised  
1939 National Register census reference recognition fix  
1881 Scotland Lost Cousins entries wrongly treated as both Scotland, and England and Wales  
Alive date on census wasn't allowing you to enter nothing and leave field  
Census reports now don't show female married names if not old enough to be married  

#### Updated Release Version 8.0.0 22nd August 2020  
**Updates**  
Implemented Custom Fact display and ignoring of custom fact warnings  
BCE & CE dates (and their religious equivalents BC & AD) now understood  
BCE & BC dates treated as unknown as I have no means of handling negative years  
Update code analysis to v3.0.0  
Load Title & Suffix and nicknames from Name parts  
Show any futuristic facts - dates after today eg: born in the future  
Added Facts count to individuals report & export  
Added unique Sources count to individuals report & export  
Added Sources count to facts report & export  

**Bugfixes**  
Fixed display error with CAL dates showing as ABTL dates instead of ABT date  
Blessing fact type was looking for BLESS instead of BLES and so missing blessing facts  

#### Updated Release Version 7.8.5 21st July 2020  
**Updates**  
Added _ATTR custom attribute flag support from Family Historian  
Renamed Unknown fact types to Unknown/Custom fact types  
Reworked loading to speed up loading times signficantly on larger files  
Add progress bar to locations tree loading  
Update to BruTile 3.0  
Add in children born too close together checks  

**Bugfixes**  
Add Font set to null to try to fix dispose error  

#### Updated Release Version 7.8.3 4th June 2020  
**Bugfixes**  
Single families shouldn't show solo individuals    
Fixed issue with 29th Feb in double dates  
Fixed issue with printing initial loading page  

#### Updated Release Version 7.8.2 12th May 2020  
**Updates**  
Website update offers release version if cancel clicked.  
Added various additional Canadian Census reference checks  
Added web link to Library and Archives Canada site if item number included in reference  
Added web link to Scottish census references if identified  
Added single parent report  

#### Updated Release Version 7.8.1 8th May 2020  
**Updates**  
Added Birth before baptism error check  
(NB. This may cause error tick boxes to change as I added this one at start)  
Only shows error for quarter day births if birth 4 months after baptism/christening   
Added extra census reference checks for census references with /  

**Bugfixes**  
Lost cousins upload error when forename is empty fixed  

#### Updated Release Version 7.8.0 1st May 2020  
**Updates**  
Refactored code to take on board code analysis recommendations  
Added aged over 99 report  
Re-enabled web version check for releases  
Remove old clickonce dependancies  

**Bugfixes**  
Fixed font error crash when resizing fonts  
Fix issue of not showing census facts if selecting all dates  

#### Updated Release Version 7.7.2 26th Apr 2020  
**Updates**  
Geocoding now using region/country viewports to try to get more accurate results  
Geocoding now searches on country then region first to enable use of viewports  
Update Analytics to show x64 vs x86 OS  

**Bugfixes**  
Google API now works as intended 
  - timeouts after two lookups no longer crashes or erroneously says used up limit for today  
First fact type in facts tab wasn't allowing you to select it  
Added various using groups to catch similar possible issues   
Reverse geocoding now updates viewports  
Don't geocode unknown location  
Treat UNKNOWN location as already Geocoded  

 #### Updated Release Version 7.7.1 22nd Apr 2020  
**Updates**  
Added FamilySearchID recognition and clickable link in Individuals Report  
Added extra Canadian census reference check  
Added Windows Store check for analytics  

**Bugfixes**  
1911 FindMyPast UK Census links were broken by v7.6.5  

#### Updated Release Version 7.7.0.0 20th Apr 2020  
**Updates**  
Added new Census problem facts report  
Added new auto created census fact report  
Updated links to Parish boundary files that were locked on old Codeplex site  
Added error comment on Facts export to Excel  
Fix Google API calls when request denied    

**Bugfixes**  
Web special characters were preventing recognition of valid census references  
Don't export class name if icon in exports to Excel  
Fix Birthday Effect report crash  
Fix printing displaying blank page  

#### Updated Release Version 7.6.6.0 16th Apr 2020  
**Bugfixes**  
Fixed duplicate check name score crashing  
Fixed unable to save user config crashing  
Update icons to use extra sizes  
Code tidy ups as per analyzer suggestions  
Update font dispose methods to try to eliminate location tab crash  

#### Updated Release Version 7.6.5.0 15th Apr 2020  
**Updates**  
Changed FindMyPast UK Census search to use specialist form for that specific census  

**Bugfixes**  
Missed adding the new census records to the list of approved censuses  
Added force update of settings to avoid exception on locked user settings  

#### Updated Release Version 7.6.4.0 11th Apr 2020  
**Updates**  
Added in extra census dates so modern census records eg: US2020 don't throw errors  
Added score for duplicate parents names even if different individuals, 
this should catch entirely duplicated families  
Added potentially missing children report  

**Bugfixes**  
Resetting column layouts no longer resets form layouts  

#### Updated Release Version 7.6.3.0 6th Apr 2020  
**Updates**  
Update to Analytics v6  
Update included packages to latest versions  
Add new Scottish Census Reference checks  
Sources now parse note fields as well as text fields  

**Bugfixes**  
Changing region on Census & Research Suggestion remembers selection  
Failed Census references found in source fact pages are added to list of unrecognised  

#### Updated Release Version 7.6.2.0 27th Mar 2020  
**Updates**  
Add Name fact  
Add Sources to Name fact - Sources for an individual were getting ignored 
as they weren't tied to a fact adding a name fact addresses this  
Added support for CHANge tag  

#### Updated Release Version 7.6.1.1 29th Feb 2020  
**Bugfixes**  
Fix bug with leap day 29th Jan in On this day reports.  

#### Updated Release Version 7.6.1.0 29th Feb 2020  
**Updates**  
Added support for some non standard Ancestry tags and method to tag others  
This should then detect various facts that previously went undetected  

#### Updated Release Version 7.6.0.0 16th Feb 2020  
**Updates**  
Updated Mapping tools to use new version of BruTile/SharpMap  
Added support for "1881 GROS 233/ 2/ 7" style Scottish census references  
  
#### Bugfix Release Version 7.5.0.1 23rd Jan 2020  
**Bugfixes**  
Lost Cousins passwords shorter than 15 characters were causing a crash  

#### Updated Release Version 7.5.0.0 21st Jan 2020  
**Updates**  
Added feature to filter colour BMD report on births, deaths and marriages  
Added warning about using potentially unstable out of date Windows 7  
Using new code signing certificate  
**Bugfixes**  
Trim Lost Cousins password to be max 15 characters  
Fix icon sizing on high definition monitors  
Update width of Lost Cousins report details tab  
Attempt to fix font issues with Locations tab  

#### Updated Release Version 7.4.2.0 20th Jun 2019  
**Updates**  
Added Children Count & Marriage Count to Individuals export  
Add statistics form titles  

#### Updated Release Version 7.4.1.0 15th Jun 2019  
**Updates**  
Updated referenced packages to latest versions  
Added new code analysers  
Applied various code tidyups  

**Bugfixes**  
Birthday Effect Menu wasn't disabled when no file was loaded  
Incomplete census refs in sources were getting ignored  
Incomplete census refs in Lost Cousins were looking up wrong census year  
Typo in Error/Fixes tab  
The internal column IsFamily shouldn't have been displayed on Data Errors report  
Lifelines no longer includes LostCousins facts with just country location  
Fix 1940 US Census not correctly searching on Ancestry  

#### Updated Release Version 7.4.0.0 26th May 2019  
**Updates**  
Core code now built using VS 2019 and .Net 4.8  
Added recognition of Windows 10 1903  
Added Birthday Effect Report  
Added support for automatic searching of Scotland's People Census records  
Added support for automatic searching of Scotland's People BMD records  
Added FamilyIDs as Parent & Child to export Individuals to Excel

**Bugfixes**  
Fixed residence as census facts where the date was after census date  
Descendants and Linked through marriage boxes now triggers updates on change  

#### Updated Release Version 7.3.7.7 14th February 2019  
**Updates**  
Initial work on verification tab  
Add Race as a fact type  

**Bugfixes**  
Lost Cousins Duplicate checking was using surname instead of surname at date  
1841 census pattern 3 book and folio wrong way round  
Lost Cousins other names should allow spaces  
Tweak Scottish Census Refs  
Fix crash on loading v7.3.7.5
Fix multiple dashes in name

#### Updated Release Version 7.3.7.6 14th February 2019  
**Bugfix**  
Remove Pupetteer nuget package   

#### Updated Release Version 7.3.7.5 14th February 2019  
**Updates**  
Initial work on verification tab  
Add Race as a fact type  

**Bugfixes**  
Lost Cousins Duplicate checking was using surname instead of surname at date  
1841 census pattern 3 book and folio wrong way round  
Lost Cousins other names should allow spaces  
Tweak Scottish Census Refs  

#### Updated Release Version 7.3.7.4 7th February 2019  
**Updates**  
Forms forced to move down if taskbar at top  

**Bugfixes**  
Add check to see if lost cousins database entry exists before adding it  
Strip invalid characters from Lost Cousins upload strings  
Only add shared facts if not already added  

#### Updated Release Version 7.3.7.3 4th February 2019  
**Bugfixes**  
Maximise was failing cast operation  

#### Updated Release Version 7.3.7.2 4th February 2019  
**Updates**  
Main Form now remembers its maximised state  
Issues when storing Lost Cousins facts shows error in feedback window  
Database checks for PC version number on startup different from Mac version number  
Added text to confirm login required to Lost Cousins before button becomes active  

**Bugfixes**  
Updating relationships on Lost Cousins tab updates LC reports
Strip invalid characters from Lost Cousins upload strings  
Only add shared facts if not already added  
11Th, 12Th, 13Th weren't fixed to lower case th  
Fixed bug where all ? were being converted to -  

#### Updated Release Version 7.3.7.1 2nd February 2019  
**Updates**  
Cater for forename and surname in data exports  
Recognise more census references with RGxx and /s and nothing else  

**Bugfixes**  
Lost Cousins Age was being trimmed to 1.  
Factdates with long em dashes were not getting recognised  
Tweak Handwriting font size  

#### Updated Release Version 7.3.7.0 1st February 2019  
**Updates**  
Reorganised Census Form to make it clearer what uses Filters and what doesn't  
Added Please verify the references text on upload to Lost Cousins  

**Bugfixes**  
Fixed typo on Welsh & IOM piece numbers in 1851 census refs  
Lost Cousins ages tweaks and cater for 5 year rounding in 1841 census  
Fix to Scottish reference pattern  
Census referecences that are recognised but incomplete weren't then checking rest of text  

#### Updated Release Version 7.3.6.0 30th January 2019  
**Updates**  
Added LooseInfo tab to combine Loose Births and Death info in one tab  
Added lots of new census reference checking thanks to a couple of people who pointed out missing references  
Added support for Julian dates  

#### Updated Release Version 7.3.5.1 28th January 2019  
**Bugfixes**  
Lost Cousins login cleared if user name or password changes  

#### Updated Release Version 7.3.5.0 27th January 2019  
**Updates**  
Added support for dashes in UK census refs  
Added extra 1841-51 UK census checks  
Tweak to Lost Cousins Age Calc for Abt years

#### Updated Release Version 7.3.4.0 26th January 2019  
**Updates**  
Export of unrecognised & Missing references changed to single report  
Change get text to look at siblings for CONC lines - should pickup more extended text fields beyond usual one line descriptions  
Added 1939 National Register recognitions for incomplete patterns (missing page references)  
Added extra UK census reference checks  
Extra support for unknown surnames  

**Bugfixes**  
Lost Cousins metaphones weren't being set for loaded data resulting in some duplicate checks being missed.  
If a valid census reference was found in notes it wasn't updating an invalid census ref on fact  

#### Updated Release Version 7.3.3.2 25th January 2019  
**Bugfixes**  
Fixed SQL loading bug creating missing LostCousins table  
Fixed analytics using local instead of UTC time  

#### Updated Release Version 7.3.3.1 24th January 2019  
**Bugfixes**  
Relationship types calc was using wrong logs  

#### Updated Release Version 7.3.3.0 24th January 2019  
**Updates**  
Added new 1939 census reference recognition  
Added option to see all Lost Cousins records with invalid references  
Added Lost Cousins Stats analytic  

**Bugfixes**  
Added check to see if LostCousins table exists in database and add it if missing  
Fixed issue with long notes not looking for secondary entries as other census references  
Fix issue with BigIntegers & relationtypes  
fix analytics issue showing zero uploads  
Fix census references to strip bold and italic markers  
Tweak Lost Cousins duplicate check to be 5 year age tolerant  

#### Updated Release Version 7.3.2.3 22nd January 2019  
**Bugfixes**  
Lost Cousins duplicate check uses forename not forenames  
Tweak to Analytics to include timestamp  

#### Updated Release Version 7.3.2.2 21st January 2019
**Bugfixes**  
Custom tag prefixes of CENSUS are treated as a census fact  
Use BigIntegers for Ahnantefal numbers  
Added clarification that custom types is a warning not an error  
Fix bug with individual ID of 0  
Fix Lost Cousins Action Analytic  

#### Updated Release Version 7.3.2.0 21st January 2019
**Updates**  
Added option preview records for upload to Lost Cousins  
Added Analytics for Lost Cousins preview

**Bugfixes**  
Added check for ABT dates missing a space  

#### Updated Release Version 7.3.1.0 19th January 2019
**Updates**  
Added option to convert any residence facts with a valid census reference to a census fact  
Added breakdown of husband and wifes forenames and surnames in family reports  

**Bugfixes**  
Added further fix for US 1940 census on records with very long notes  
Added Null pointer fix for loading Lost Cousins entries where no prior records exist  
LostCousin Facts that are auto created treated as LC_FTA facts to allow reporting separately from GEDCOM Lost Cousin facts  

#### Major Release Version 7.3.0.0 19th January 2019  
**Updates**  
Major new feature ability to automatically post your Lost Cousins entries to the Lost Cousins website  
Tracks uploaded individuals and creates an automated Lost Cousins Fact Type for you.  
 - This relies on the individual name and ID for a more permenent flagging you should still add a flag to your GEDCOM file  

Added Line Errors report to show you where the error is in the original file  
Added Linked relationship type - for people who are linked to root person but not related  
Added an option on Surnames tab to ignore case  
Added extra recognition of Scottish Parishes to fix missing cases  
Added extra recognition of some more US 1940 census reference formats  
Added piece number search to FMP census if Schedule missing - typically UK 1911 census  

**Bugfixes**  
Fix issue with 3 digit years in a between statement  
Census families were missing one parent families  
Fixed issue with filestream being held open once file had loaded  
Fixed issue with min/max dates arithmetic  
Replace old custom HttpUtility class with system one  
Fix issue on front page when maximising form  
Report if Census reference check is disabled  

#### Updated Release Version 7.2.4.4 3rd January 2019  
**Bugfixes**  
Fixed issue with adding months to edge case dates  
Fixed issue with line errors reporting twice  

#### Updated Release Version 7.2.4.0-3 3rd January 2019  
**Updates**  
Added User setting for Google API Key see [Google API Key Documentation](/GoogleAPI)  
Changed file loading options to detect code sets - note incompatible code sets will now give errors.

**Bugfixes**  
Fixed missing icon file  
Forgot to increase version number  
Fixed issue with stripping line ends option closing file  

#### Updated Release Version 7.2.3.0 2nd January 2019  
**Updates**  
Added region selector to searching to allow for different regional websites to be used  

#### Updated Release Version 7.2.2.1 30th December 2018  
**Updates**  
Added option to privatise or include details of living people on GEDCOM export  

**Bugfixes**  
Fixed GEDCOM export missing out families of siblings  
Fixed GEDCOM export not displaying middle names  
Fixed date ranges for census searches where birth year is wide date range  

#### Updated Release Version 7.2.2.0 25th December 2018  
**Updates**  
FreeCEN Searches url changed to use old site until new one ready  
ADDR tags now recognised as additional to PLAC tags instead of just alternatives  

**Bugfixes**  
Fix crash on Cousin Count double click  
Fix crash on Opacity settings in map options where no background map selected  
Dont warn errors on same sex marriages post 2000  
Options on load reports all new options  
Fix bug with double spaces in between dates  

#### Updated Release Version 7.2.1.1 21st December 2018  
**Updates**  
Added option not to filter diacritics  
Added support for German date names  

**Bugfixes**  
Fixed issue with duplicates crashing on opening tab  
Fixed bug with crash when local date name wasn't recognised  

#### Updated Release Version 7.2.1.0 20th December 2018  
**Updates**  
Added option to export descendants to the GEDCOM file stripped down for DNA purposes  

**Bugfixes**  
Fix crash on double clicking on how many directs stats  
Attempted to catch font disposal bug  
Lat/Long reporting from GEDCOM fixed to exclude ones from database  
Fixed Duplicates report occassional crash with rounding error  
Fixed space bug in dates like BET 3 AND 4 JAN 2001  
Fixed bug with huge Ahnantafel numbers crashing  

#### Updated Release Version 7.2.0.0 18th December 2018  
**Updates**  
Added option to export a GEDCOM file stripped down for DNA purposes  
Added option to export Locations  
Added display of raw locations loaded from GEDCOM file  
Added option to skip tidying up locations so you can see the raw mess  
Repeated bad lines now gives option to stop processing and turn on bad line filter  

**Bugfixes**  
Fix crash on closing file  
Web version check skips now checking on beta versions  

#### Bugfix Release Version 7.1.0.1 16th December 2018  
**Bugfixes**  
Issue with custom tags for facts fixed was causing crash on load

#### Updated Release Version 7.1.0.0 16th December 2018  
**Updates**  
Update Google Maps display on Locations tab to work with updated Google Maps  
Updated OS Maps to display on historic OS Maps via interactive National Library of Scotland Maps  
Added extra Family Historian Fact types  
Added extra census custom fact type recognition  

**Bugfixes**  
Fix max extents on map zooming out too much and including 0,0  
Add marriage ID differentiator for possible duplicate facts  
Fix progress bar for Family Historian locations  

#### Updated Release Version 7.0.5.0 15th December 2018  
**Updates**  
Added a Data Errors export to Excel feature  
Added support for Family Historian _PLAC codes  

**Bugfixes**  
Add fix for hyphenated capitalised shires  
Add recognition of UK/GB countries with ,UK and ,GB at end  
Duplicate fact checks for unknown facts now look at original tag  
Duplicate fact checks no longer treats shared facts and individual facts as duplicates  
Shared surname errors now don't error if wife's natural father had that surname as that isn't an error  
No longer reports bad lines twice if stripping bad line breaks  
Dots in dates now correctly stripped  
Between dates with just day and month wasn't working correctly  
Dates that are formatted correctly but are wrong eg: 29 FEB 1735 now highlighted as error  

#### Updated Release Version 7.0.4.0 9th December 2018  
**Updates**  
Add colour note to tooltip to make it clearer what the status is.  
Add support for Census UK XXXX custom fact formats  
Change way UTF8 files handled  

**Bugfixes**  
Parental relationships were only using relationship from first child  
Update Loading to cope with crash on bad lines  

#### Updated Release Version 7.0.3.2, 23th November 2018  
**Bugfixes**  
Fix issue with facts form crashing due to leak of Mac code into windows code

#### Updated Release Version 7.0.3.1, 21th November 2018  
**Bugfixes**  
Fix issue with printing not wrapping lines

#### Updated Release Version 7.0.3.0, 19th November 2018  
**Updates**  
Better support for accented characters in different codesets  
Added support for Legacy 9 Hashtags  
Added option to skip checking notes and stories for census references  

**Bugfixes**  
Fix non GEDCOM dates with a dot not being recognised  
Fix default window size & grid sizing bug  

#### Updated Release Version 7.0.2.0, 11th November 2018  
**Updates**  
Trim quotes from dates and recognise dates with weekday names  
Cater for 2 QTR date formats and months with no space before year  
Support Surnames of LNU (Last Name Unknown) and 5 underscores meaning surname unknown  
Support Quaker date formats  

**Bugfixes**  
Add extra progress records for loading data errors during initial load  

#### Updated Release Version 7.0.1.2, 5th November 2018  
**Updates**  
Added option to show/hide display altering you to ancestors who are multi directs  
* Show Multi direct ancestors can cause issues for files going back many centuries as program runs out of memory trying to list what  the relationships are  

Tweaked database settings to try to use less resources on loading GEDCOM file should make loading a little faster on large files

**Bugfixes**  
Fixed issue with years prior to 1000 being double dates

#### Updated Release Version 7.0.1.1, 4th November 2018  
**Bugfixes**  
Fixed issue with text dates in family facts

#### Updated Release Version 7.0.1.0, 2nd November 2018  
**Updates**  
Added extra relation categeory for descendants  
Added report to show how many direct ancestors/descendants you've got  
Added link to Facebook support site in Help menu.
Added link to Facebook User Group site in Help menu 

**Bugfixes**  
Updated LAT/LONG coordinates in GEDCOM weren't updating internal database if there was a change  

#### Updated Release Version 7.0.0.3, 31st October 2018  
**Bugfixes**  
Minor tweak to version control  

#### Updated Release Version 7.0.0.2, 29th October 2018  
**Updates**  
Added back in support for high dpi screens  

**Bugfixes**  
Fix issue with birth, death dates not being given priority over baptism/burial facts  

#### Updated Release Version 7.0.0.1, 25th October 2018  
**Bugfixes**  
Code tidyups and fixed minor analytics issue 

#### Major New Release Version 7.0.0.0, 24th October 2018
**Updates**  
Updated the .Net Framework to the latest .Net 4.7.2 this means v6.7.3 is the last Vista version  
Updating to .Net 4.7.2 means new features in Windows can be properly supported  
Added a version check on startup so that zip installed versions will also warn if outdated  
Added an OS version check to see how many users of soon to be discontinued Windows 7 there still are  
Update database to v7.0.0.0 to fix issue with geocoded locations at 0,0  
Add privacy policy page and link in main menu  
Add support for Unmarried/Never married death date  
Added various analytics logging actions  
Add support for start and end dates for births and deaths in export routines  
Added region fix for US habit of adding superfluous word "county" to end of English Shire counties  
Children of families are now default sorted by age  

#### Updated Release Version 6.7.3.0, 18th October 2018  
**Updates**  
Added support include treetop individuals with one parent  
Temporarily reverted high res monitor manifest fix as it broke windows 7 install

#### Updated Release Version 6.7.2.0, 18th October 2018  
**Updates**  
Added support for high resolution monitors  
Added support custom facts for occupation and naturalisation  
Added child ids to family export to Excel  

#### Updated Release Version 6.7.1.0, 14th October 2018  
**Updates**  
Added support for DEAD, DECEASED, STILLBORN, INFANT, CHILD, YOUNG in death date.  
Added support for multiple default fact types from Family Tree Maker.

#### New Release Version 6.7.0.0, 11th October 2018  
**Updates**  
Ancestry 1939 UK National Register searching added  
Marriage comments now show names of both parties for easier reading in an individuals facts report

#### Updated Release Version 6.6.1.1, 29th September 2018  
**BugFixes**  
Fix to fact dates for loose births/deaths and some other reports

#### New Release Version 6.6.1.0, 28th September 2018  
**Updates**  
Added options to filter unknown births and unknown deaths from colour census reports.

#### Updated Release Version 6.6.0.2, 16th September 2018  
**BugFixes**  
Small tweak to bad lines check to better cope with the invalid screwed up nonsense that isn't valid GEDCOM that Ancestry produces with notes fields.

#### Updated Release Version 6.6.0.1, 14th September 2018  
**BugFixes**  
Locations with LAT/LONG in GEDCOM weren't updating as previously Geocoded locations  
Fix issue with unusual characters eg: © ® ¿ » « appearing in locations  

#### New Release Version 6.6.0.0, 9th September 2018  
**Updates**  
New Font setting feature - allows you to change default font used

#### Updated Release Version 6.5.1.2, 5th August 2018  
**BugFixes**  
Fix Bug with dashes causing failed test classes

#### Updated Release Version 6.5.1.1, 5th August 2018  
**BugFixes**  
Fix slow loading of individuals introduced in v6.5.0.0

#### Updated Release Version 6.5.1.0, 3rd August 2018  
**Updates**  
Added relationship check when someone is a direct ancestor more than once

**BugFixes**  
Fix bug with French spelling of May
Fix bug introduced in 6.5.0.0 with dates with dashes  

#### New Release Version 6.5.0.0, 17th July 2018  
**Updates**  
Significant addition to FactDate measurements now cater for non GEDCOM date formats such as dd/mm/yyyy  
See Tools/Options panel for settings. By default this is off.  
Note this should not be taken as an encouragement to use non GEDCOM dates.  

#### Updated Release Version 6.4.3.1, 15th June 2018  
**BugFixes**  
Fix issue with Diacritics in locations causing loading to get stuck forever.

#### New Release Version 6.4.3.0, 13th June 2018  
**Updates**  
Changed Google API Geocoding to use new Google pricing plan
**BugFixes**  
Fixed null pointer exception on generated facts

### Updated Release Version 6.4.2.1, 8th June 2018  
**BugFixes**  
Fixed issue with Children Status report not correctly filtering patterns where numbers followed words

#### New Release Version 6.4.2.0, 10th May 2018  
**Updates**  
Tweaked grid printing options to adjust margins and stop icons making grid huge  
Updated to latest NuGet Packages

#### New Release Version 6.4.1.0, 13th April 2018  
**Updates**  
Added support for French month names and prefixes  

**BugFixes**  
Fixed issue with application crash checking duplicates on large files 

#### Updated Release Version 6.4.0.1, 19th March 2018  
**BugFixes**  
Fixed issue with links to FindMyPast
Checks unknown gender individuals appearing as a husband or wife

#### New Release Version 6.4.0.0, 19th March 2018  
**Updates**  
Added five new data error checks
* Duplicate Fact check - checks if two identical facts for a person exists  
* Possible Duplicate Fact check - checks if two facts have same type and date but different data  
* National Register 1939 birth date check - checks to see if someone is recorded on the 1939 register then they should have an exact birth date  
* Couples have same surname check - a common error where wife is recorded with married name instead of the correct birth name  
* Female husbands and male wives - checks if the right gender is assigned to the individuals tagged as husband and wife  

**Bugfixes**
Minor tweaks and code improvements

#### New Release Version 6.3.0.2, 13th March 2018
**Updates**  
Updated to new high res application icon  
Moved individuals, families, sources and occupations tabs into a Main Lists tab  
Moved Data Errors, Duplications, Loose Births and Loose Deaths into a Errors & Fixes tab 

**Bugfixes**  
1939 entries were eroneously limited to only people born before 1911.  
The moved tabs weren't reacting to all mouse clicks

#### Updated Release Version 6.2.1.0, 23rd Feb 2018  
**Updates**  
Fixed issue with On this day not showing any data

#### New Release Version 6.2.0.0, 19th Feb 2018  
**Updates**  
Major refactoring to move shared code into a new project to allow for developement of a Mac version
Duplicates processing should now be 8-10 times faster than before

**Bugfixes**  
Loose Births weren't showing now fixed

#### Updated Release Version 6.1.3.0, 9th Feb 2018  
**Updates**  
The files that standarise countries/regions wasn't getting published so countries weren't correctly recognised for census facts  
Special character processing and spurious line end fixes re-enabled and tweaked to be considerably faster
Several library updates and code splits to ready code for initial Mac development

#### Updated Release Version 6.1.2.0, 4th Feb 2018  

**Updates**  
Added an open ended date range category for colour BMD reports
Refactored Colour Census reports to use colour enumeration rather than integers

**Bugfixes**  
Fixed issue where transparency slider was still showing when using same OSM base map

#### Updated Release Version 6.1.1.0, 2nd Feb 2018

**Updates**  
Added a background Open Street Map even when using other maps so that where there is no coverage you can still see a map.

***Preparations***  
Started splitting out code for future MAC version into a shared code project. 
The aim is to have all the functionality in the shared code and just the user interface in the Windows/Mac specific versions.  

#### New Release Version 6.1.0.0, 29th Jan 2018

**Updates**  
Added a variety of old Maps (mostly UK/Scotland) so you can view places in your tree as they were at the time your ancestors lived there.  
Added recognition of the 1939 National Register (UK) references  
Added 1939 National Register references link to Find My Past  

**Bugfixes**  
Fixed issue with solo familes using same ID thus preventing facts result working  
Surname tab links to Guild of One Name Studies wasn't working  
Julian Calendar dates were incorrectly working out before and after if compared with a whole year  

#### Updated Release Version 6.0.5.0, 26th Jan 2018

**Bugfixes**  
Added font to distribution so the app name shows correctly  
Updated SharpMap & BruTile to latest versions and fixed background maps  
Fix for Timeline report year ranges being too broad

#### Updated Release Version 6.0.4.0, 23th Jan 2018

**Updates**  
Added new Logo and text on main page of app

**Bugfixes**  
Fixed issue of some options not sticking when changed  
Various minor code optimisations for c#7

#### Updated Release Version 6.0.3.0, 20th Jan 2018

**Updates**  
Imported the suggested patches from fire-eggs these add:
* Loading time improvements for processing of individuals
* Colour reporting exports filter columns & retain colours  

US census overhauled now uses date ranges to cope with times where enumerators ignored rule about the state of the house on census day

#### Updated Release Version 6.0.2.0, 19th Jan 2018

**Updates**  
Fixed most links to old codeplex documentation  
Amended range of dates for US census as enumerators didn't always adhere to guidelines that the census be recorded on a specific date.

#### Release Version 6.0.1.0, 8th Jun 2017

**Updates**  
Rebuilt for deployment on GitHub  
Upgraded to .Net 4.6 dropping XP support 

#### Updated Release Version 5.2.0.7 of FTAnalyzer, 31th Jan 2017

**New Features**  
Added filter to loading GEDCOM so that badly formatted CONC or CONT text that wraps onto a line leaving invalid GEDCOM syntax is treated as a continuation of the previous line of text. If the Census checker finds a reference but its partial don't crash

#### Updated Release Version 5.1.0.5 of FTAnalyzer, 11th Aug 2015

**New Features**  
**Updates**  
Birth and death searches now use loose birth/death to narrow range if unknown date  
Ancestry searches now filter on marriage date and death date  
Census Ref and date pattern matching is now faster  
**Bug Fixes**  
Now treats Unknown locations as geocoded  
When loading form locations added a check to make sure it is on one of the user's screens  
UK addresses geocode one level below rest of world due to Scotland, England, Wales etc being the country not UK.  
Status text on geocoding form wasn't correctly refreshing

#### Updated Release Version 5.1.0.4 of FTAnalyzer, 4th Aug 2015

**New Features**  
**Updates**  
**Bug Fixes**  
Geocoding of US locations have an extra level  
Fix for 40 year death ranges on FMP if unknown dates

#### Updated Release Version 5.1.0.3 of FTAnalyzer, 2nd August 2015

**New Features**  
**Updates**  
Birth and death searches now use loose birth/death to narrow range if unknown date  
Ancestry searches now filter on marriage date and death date  
Census Ref and date pattern matching is now faster  
**Bug Fixes**  
#### Updated Release Version 5.1.0.2 of FTAnalyzer, 1 August 2015

**New Features**  
**Updates**  
**Bug Fixes**  
Geocoding of US locations have an extra level

#### Updated Release Version 5.1.0.1 of FTAnalyzer, 25 July 2015

**New Features**  
**Updates**  
**Bug Fixes**  
Residence and census facts that had invalid years note weren't getting checked for census references in the notes  
Use married surnames for death searches  
Family Search only include birth location for birth searches  
Always make birth year dates use range of at least 1 year

#### Updated Release Version 5.1.0.0 of FTAnalyzer, 23 July 2015

**New Features**  
Added Report to show a count of relatives of each type  
Added BMD searching for Ancestry, FindMyPast and FamilySearch from BMD Colour Report - Double click a birth, marriage or death on the colour BMD form to auto search the web for that entry. Similar to how the census searching works.  
**Updates**  
Added Option to display females with unknown surname in brackets as unknown  
Set search dropdown on BMDs to FreeBMD rather than FreeCen  
**Bug Fixes**  
Missing fields no longer passed to FindMyPast census searches causing searches to fail  
Surnames box wasn't filling grid  
Valid Census records on Lost Cousins and colour census reports now use year comparisons rather than exact date.  
Census ref search wasn't picking up schedule for 1911  
Unknown page census refs were getting lost by unknown census refs

#### Updated Release Version 5.0.4.2 of FTAnalyzer, 11 May 2015

**Updates**  
Added Relation filter for Surname report  
**Bug Fixes**  
Loading with old database no longer fails if latm/longm columns already exist.

#### Updated Release Version 5.0.4.1 of FTAnalyzer, 9 May 2015

**Updates**  
Now recognises lat/long places from Legacy files  
**Bug Fixes**  
Someone who is married twice no longer appears twice on the census reports if on their own in a previous family  
Arrival and departure facts are location facts not comment facts  
Change IsAlive to StartsBefore instead of IsBefore  
Status bar text wasn't clearing on a reload

#### New Release Version 5.0.4.0 of FTAnalyzer, 5 May 2015

**Updates**  
If a census reference cut n paste from Lost Cousins is detected then a created Lost Cousins fact is added if one didn't exist.  
**Bug Fixes**  
ADDR & PLAC records for fact are no longer concatenated  
Census ref wasn't picking up from fact notes if it found text it didn't recognise in source

#### Updated Release Version 5.0.3.2 of FTAnalyzer, 5 May 2015

**Updates**  
Add support for 1881 census - District 146/B, Page 59, Family 273 - living at Rainham, Haldimand, Ontario, Canada. style Canadian census ref.  
**Bug Fixes**  

#### Updated Release Version 5.0.3.1 of FTAnalyzer, 5 May 2015

**Updates**  
Add support for Canadian Ancestry census references  
**Bug Fixes**  
Fix typo in census recognition for Lost Cousins census references

#### New Release Version 5.0.3.0 of FTAnalyzer, 3 May 2015

**Updates**  
Step value for "on this day" remembered between sessions  
Add function to export missing census references  
Various updates to US census reference checks  
Added Scottish Parish reference -> name lookup  
Census references for existing facts now tries to update Fact location details if fact had no location  
Added code to load TNG format location files  
Add support for 1881 Canadian LC Census pattern  
Add check to see if census fact year matches census reference year fact shows as error if there is a mismatch  
Added census ref year to facts display  
**Bug Fixes**  
Lost Cousins Colour status now also checks if likely to be out of country  
Fix for recognising Russia from Chapman Code

#### Updated Release Version 5.0.2.4 of FTAnalyzer

**Updates**  
**Bug Fixes**

Only copy census references if reference is good or partial  

#### Updated Release Version 5.0.2.3 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Notes continuation line was being read then ignored  
Set Census ref comment if length > 0  
When a census fact exists and a note exists with a census ref copy the census ref across if the census fact doesnt have one

#### Updated Release Version 5.0.2.2 of FTAnalyzer

**Updates**  
Add support for Scottish Census patterns with years  
Add recognition of Lost Cousins 1940 US Census reference format  
Added recognition of 1940 US census references as cut n paste from FamilySearch, FindMyPast & Ancestry  
**Bug Fixes**  
Fix text lookup was aggressively looking for text even on names etc

#### Updated Release Version 5.0.2.1 of FTAnalyzer

**Updates**  
Added support for Legacy 8 default fact types recognition  
Dates with ~ now supported  
Added support for extra Scottish Valuation Rolls  
Add created census facts if partial recognition  
Report results window now shows Census Reference details found  
**Bug Fixes**  
Lots of tweaks to handling of notes to find more census references  
Gedcom parser now allows CONT & CONC lines to start with a @  
fix for WWI & WWII abbreviations being converted.

#### New Release Version 5.0.2.0 of FTAnalyzer

**Updates**  
Parent and Children facts now include birth location if known  
Revised handling of notes to better find census references  
Recognises cut n paste formatted references from FindMyPast  
Calculated Birth facts now show Age string used  
"1911 Census" and "Census 1911" patterns now recognised  
**Bug Fixes**  
Census Facts now check for notes on the fact as well as in general notes to find a census reference  
Fixed issue with double counting LC facts with bad/missing census facts  
"1841 Census:" reference types with Book: were getting skipped by mistake

#### Updated Release Version 5.0.1.5 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Facts before birth shouldn't show errors for calculated birth facts  
Only show missing children status where both parents are alive and at least parenting age  
Fix Age calc being 1 day out  
Fix loose births getting confused when partial dates used with no year

#### Updated Release Version 5.0.1.4 of FTAnalyzer

**Updates**  
Added File Handling option menu to allow loading files with special character handling  
Added support for dates of format  
1914 to 1918  
1914 until 1918  
(also any day month variants)  
Added support for C1xxx and C2xxx dates  
**Bug Fixes**  
Children status of ignore wasn't ignoring all records for missing Children status report.  
Warnings of file errors don't warn as frequently when loading very large files  
Fact Dates cope better with 3 digit years  
Loading families no longer fails if missing REF attribute  
Tidied up some reload required options not forcing a reload  
Added missing options current settings text to report window

#### Updated Release Version 5.0.1.3 of FTAnalyzer

**Updates**  
Add extra Scottish Valuation Rolls dates  
**Bug Fixes**  
Check Lost Cousins Fact Location before checking related census fact location  
Lost Cousins facts with missing census facts now shown separately from Census facts with missing Lost Cousins facts  
Fix issues with Date Calculations causing crash

#### Updated Release Version 5.0.1.2 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Census reference URL doesn't append F, R or O  
Fixed loading GEDCOMs with individuals with null IDs

#### Updated Release Version 5.0.1.1 of FTAnalyzer

**Updates**  
Census References now give better locations as taken from the census piece number descriptions on FindMyPast & Ancestry  
Calculated Birth Facts now have their own FactType  
Added option to exclude auto created locations from locations list  
Census References now displays on all fact reports  
Added tooltip to Census Ref column in Facts report if census reference URL available  
**Bug Fixes**  
Facts reduced minimum width of sources column  
Unicode diacritics no longer removed for displaying locations still removed for comparisons  
CensusReference checking now checks across continuation lines and keeps checking if a census record already exists  
Don't include occupation facts in On this Day's facts  
Allow duplicated created census facts if there are multiple census references for a single year to highlight to user the potential issue  
Duplicated created census facts now show in duplicated facts report  
Make all census reference searches lazy  
Tweaks to census references to fix folio numbers with alphabetic suffixes  
Census year check now fixed for Census XXXX or XXXX Census references

#### Updated Release Version 5.0.1.0 of FTAnalyzer

**Updates**  
Census References now give better locations as taken from the census piece number descriptions on FindMyPast & Ancestry  
Calculated Birth Facts now have their own FactType  
Added option to exclude auto created locations from locations list  
Census References now displays on all fact reports  
Added tooltip to Census Ref column in Facts report if census reference URL available

#### Updated Release Version 5.0.0.2 of FTAnalyzer

**Bug Fixes**  
Fixed issue with census references with no sources passing null dates around causing load GEDCOM to fail

#### New Release Version 5.0.0.0 of FTAnalyzer

**New Features**  
Added support for auto creating census records from identified census references in notes and source records. This will go a long way to helping users with older family tree applications that have no census facts.

Today Tab now shows events that happened in your tree and world events from that date or that month in the past.  
Both of these features come with options to turn them on or off.  
Census Reference reports now have clickable links to see the census page results on Find My Past website.

**Updates**  
Renewed Code Signing Certificate  
Census reference checks allow multiple spaces between parts  
Census References now don't need ; or , to match.  
Update count in load screen to show created census facts  
Allow < and > in dates ie: replace with BEF and AFT  
Added Lost Cousins My Ancestors page census reference formats  
Facts now display comments if available.  
Added new and tweaked various census reference formats  
Children status errors now have own error type  
Family facts errors now shown separately from individual fact errors.  
Add support for Legacy Shared Census Facts

**Bug Fixes**  
Set duplicates trackbar to only have 20 ticks  
Fixed issue with AFT dates messing up duplicates calcs  
Allow for Unicoded files  
Duplicate Fact report no longer reports duplicates of created facts  
Changed Double Date warning  
Colour Census double clicks now work for all bar not alive and known missing  
Children status patterns weren't correctly setting the alive & dead status  
Children Status form save button now displays  
Children Status grid colours changed to make it easier to see errors  
Children Status tooltips added to highlight why cell is coloured  
Loose Births and deaths grids can have columns ordered by user

#### Updated Release Version 4.1.1.4 of FTAnalyzer

**Updates**  
Added Enumeration District detection to US census ref  
Individuals export now exports relation to root column  
**Bug Fixes**  
29th or higher great grandparents now show correctly in relation to root calculations

#### Updated Release Version 4.1.1.3 of FTAnalyzer

**Updates**  
**Bug Fixes**  
US Census ref was being loaded but not displayed  
Fix issue with Channel Islands not counting as part of England & Wales for census

#### Updated Release Version 4.1.1.2 of FTAnalyzer

**Updates**  
Added Enumeration District detection to US census ref  
**Bug Fixes**  
Region shifts now only consider UK regions  
Files with bad data now clear data they partially loaded

#### Updated Release Version 4.1.1.1 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Fix for loose birth ranged birth year and no other facts  
Children status now accepts Living as alternate for Alive  
Red for mismatched entries was on wrong columns  
Report unrecognised census refs wasn't linked to button  
Multiple marriages was wrongly affecting loose births

#### New Release Version 4.1.1.0 of FTAnalyzer

**Updates**  
Added ignore option to mismatched census references. This allows you to hide results where the reason is two families on same census page but different addresses.  
Filter button on census ref facts report allows you to show/hide census reference mismatches  
Added At Sea as a special country that doesn't get Google lookups so it can be used for manual setting of locations where people were born/married/died etc at sea in various parts of the world.  
**Bug Fixes**  
Isle of Wight fact location fix

#### Updated Release Version 4.1.0.2 of FTAnalyzer

**New Features**  
**Updates**  
Loose births checks persons birth is before parents die  
**Bug fixes**  
Editing locations no longer loses it's place when reverse geocoding updates list

#### Updated Release Version 4.1.0.1 of FTAnalyzer

**New Features**  
**Updates**  
Add Children status family name column for easier sorting  
Add check for children status total = alive + dead  
Loose births makes sure the individual would be of marrying age  
**Bug fixes**  
typo on census tab button  
inconsistent facts report title wasn't being displayed  
Ignored facts weren't being ignored on census inconsistent report  
alive/dead totals were counting children born after census as not alive thus dead.  
AFT dates strings don't need to say 1 JAN if only a year

#### New Release Version 4.1.0.0 of FTAnalyzer

**New Features**  
Added an Inconsistent Census refs vs locations report  
Added 1911 census families Missing a Children Status report  
Added 1911 census families Mismatched Children Status report  
Add recognition of Ignore for Children Status  
Added support for Close GEDCOM file menu  
Added Missing Facts Filter  
Added Duplicate Facts Filter  
**Updates**  
Added random surname select to Colour Census  
OS fuzzy search now works on missing parish names  
OS fuzzy search checks to see if within suitable distance of previously found Google partial/level mismatch  
People form now allows shift double click to see family colour census report  
Children Status picked up from NOTE tag as well as EVEN tag  
Missing Facts report is now limited to a single report fact per person and double clicking an individual gives individuals facts  
**Bug fixes**  
Beef up error message on null pointer on load to show inner exception text  
Strings for spouse names were missing on mismatch report

#### Updated Release Version 4.0.0.1 of FTAnalyzer

**Updates**  
Census refs visible on source facts report  
OS Geocoding sets found location and type based on what was found  
OS Geocoding also now uses Fuzzy search and identifies matches with OS Fuzzy search. This allows matches where the names are not exact.  
**Bug Fixes**

#### New Release Version 4.0.0.0 of FTAnalyzer

This major release introduces a significant new feature the ability to search the UK Ordnance Survey 50k Gazetteer for small placenames that often don't appear on a modern Google Map. This means significant numbers of locations that were previously not found by Google Geocoding will now be found by using OS Geocoding.  
In addition I've added support for loading the Scottish 1930 Parish boundary maps, whilst slightly different from the parish boundaries in use in the 1800s that are familiar to family historians most rural areas have almost the same if not identical boundaries in the 1800s as is in the 1930s Parish boundary set. The major variances is in the cities where several smaller parishes are amalgamated into one large parish ie: Glasgow has one large city centre parish rather than several smaller ones.  
Being able to see your Scottish Ancestor's Locations plotted on a map complete with parish boundaries makes it easier to see how close they were to other neighbouring parishes.  
NB. To use this feature you need the separate [https://ftanalyzer.codeplex.com/releases/view/115135](https://ftanalyzer.codeplex.com/releases/view/115135)Scottish Parish Boundary file[/url](http://ftanalyzer.codeplex.com/wikipage?title=/url). Simply download it and unzip it into the folder you selected from the Tools | Mapping Options form.  
**New Features**  
Scottish 1930 Parish Boundary maps are now supported  
OS Open Data 50k Gazetteer can now be used to lookup placenames  
Added support for various countries regions to be recognised - initially this is UK, US, Canada, Australia  
**Updates**  
Colour BMD report now has adjusted tooltips for over 90s  
Locations sorting is now house number agnostic  
Add version string to database export filename  
Added Regions recognition and map historic to modern counties for UK counties  
Added regions and alternate spellings recognitions for England, Scotland, Wales, Northern Ireland, UK Islands, USA, Canada, Australia  
Locations tab Treeview and other locations sub tabs now display in bold recognised regions. This should help users tidy up regions  
Recent Files menu now greyed out if no recent files  
Added Canadian Province Postal code support  
Added Support for Custom tag Military Service  
World Wars results can now be limited to just those men with known military facts  
Add Random Surname Census Search to Census Tab  
**Bug Fixes**  
Updated Fact Report to correct count of distinct individuals  
Add null protection to treeview image nodes  
Prevented opening a new file during loading

#### Updated Release Version 3.7.3.4 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Fix Fact Sources trying to display marriage facts caused a crash  
Updated source fact count to be 1 for each person in marriage instead of 1 for each shared fact  
Source IDs are now zero padded same as individual and family IDs which makes sorting work as expected  
Fix issue for brand new users not having an empty database setup correctly

#### Updated Release Version 3.7.3.3 of FTAnalyzer

**New Features**  
**Updates**  
Added several more countries to recognised countries list including extras like Gibraltar and Hong Kong  
**Bug Fixes**  
Fix Fact Sources trying to display marriage facts caused a crash  
Updated source fact count to be 1 for each person in marriage instead of 1 for each shared fact  
Source IDs are now zero padded same as individual and family IDs which makes sorting work as expected

#### Updated Release Version 3.7.3.2 of FTAnalyzer

**New Features**  
Added support for Family Historian Living flag  
Added data error type flagged as Living but has death date  
**Updates**  
Tweaks to colour BMD tooltips  
Move option "Include Locations with Partial Match Status" to Mapping settings  
**Bug Fixes**  
Fixes for Lost Cousins tab census report  
Changed LC facts no census to LC Facts bad/missing census  
Wrapped web calls in helper routine with try catch so failure to launch website will no longer crash program  
Census report now checks to see if census done for bold highlighting  
Recent File List now checks if file exists before adding to or displaying list

#### Updated Release Version 3.7.3.1 of FTAnalyzer

**New Features**  
**Updates**  
Added progress bar to Surnames tab display  
**Bug Fixes**  
Removed now redundant no country no LC fact as covered by bad/missing  
Footer text clear on tab switch now consistent across tabs  
Don’t use date filter for family groups on colour census report

#### New Release Version 3.7.3.0 of FTAnalyzer

**New Features**  
Added family drop down filter to colour reports so now you can view just a single family at a time  
Added support for Interpreted dates  
Added Help menu Link to Online Guides  
**Updates**  
Added support for Scottish Valuation Rolls as "census" reports  
Facts report now has birth date - assists identifying multiple individuals of same name  
**Bug Fixes**  
Added country check for LC census facts  
Fixed report of LC facts with no country

#### Updated Release Version 3.7.2.2 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Strip spaces from census references to make matching space insensitive

#### Updated Release Version 3.7.2.1 of FTAnalyzer

**Updates**  
**Bug Fixes**  
Family facts were getting hidden with v3.7.2.0

#### New Release Version 3.7.2.0 of FTAnalyzer

**Updates**  
Update URL for Lives of First World War to cope now the site has gone live  
Change FindMyPast search to work with new search  
Add extra tags to recognise custom events as normal facts  
Lost Cousins stats now shows census records with no countries and no Lost Cousins facts  
Added report to show Census facts missing country and Lost Cousins flag  
**Bug Fixes**  
Restoring a v3.1.2.0 database was failing  
Death Location width on census can now be resized  
Source Facts include error facts  
Add Datagridview double buffering to improve flicker on grid redraw  
Facts now log individuals

#### Updated Release Version 3.7.1.1 of FTAnalyzer

**New Features**  
**Updates**  
Shift click on census form now shows facts  
**Bug Fixes**  
Channel Islands now recognised as part of UK for census references

#### New Release Version 3.7.1.0 of FTAnalyzer

**New Features**  
Census Tab now has four new reports showing census records that have :

*   census references
*   are missing census references
*   partial census references
*   unrecognised census references

Added button to export unrecognised census facts to a text file for reporting issues with patterns that aren't being recognised  
**Updates**  
Before 1600 don't warn about marriages pre 13 years old  
Census facts now count for facts before birth  
Census facts now used for loose births  
Export to Excel referrals now uses custom interface  
Filter out Out of Country entries in census and colour census reports  
Reorganised Census tab to make it tidier  
Tidied up census tooltip text  
Added help button to link to census documentation  
Use upper case for fact types  
Added option to hide missing tagged people from census reports  
**Bug Fixes**  
Fixed out of UK but on UK census people not appearing on Lost Cousins reports  
Export from People form was crashing  
Counts for Lost Cousins facts was double counting people from UK eg: counting them as both Scotland & England  
Fix census refs only showing for LC years  
Out of UK Census refs weren’t being displayed  
Facts before birth & after death no longer appear twice  
Export Lost Cousins Referrals and Export Sources were crashing  
Fix Column sizing of census refs

#### Updated Release Version 3.7.0.2 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Colour census filters now show only distinct records  
Lost Cousins facts for overseas UK census entries now checks census fact country rather than LC country

#### Updated Release Version 3.7.0.1 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Lost Cousins facts won't now show orange if any UK fact is present  
If no preferred facts get first fact of that type - usually calculated fact

#### New Release Version 3.7.0.0 of FTAnalyzer

**New Features**  
Added US, Canadian and Irish Colour census reports  
Age facts that give a calculated birth add a calculated birth fact  
Add support for Alias fact type  
Added option to display alias in name displays  
**Updates**  
Add filter to remove someone from colour report who was never in country  
Show preferred fact status on fact report  
Armed Services and At Sea UK census now included on colour census report as part of relevant UK census - needs appropriate census ref to work  
Colour Census now filters out records where person is too old to be on census but death date is unknown  
**Bug Fixes**  
Fix to Age tag with BEF dates  
Census refs now tolerate lack of spaces after :  
Schedule numbers can now be 4 digits  
Only add preferred facts to preferred fact list

#### Updated Release Version 3.6.1.5 of FTAnalyzer

**New Features**  
**Updates**  
Added support for Missing tag so users can mark people as unable to be found on a census  
**Bug Fixes**  
Fix relation types if people married to blood/direct are also blood relations

#### Updated Release Version 3.6.1.4 of FTAnalyzer

**New Features**  
**Updates**  
Colour Census report now shows dark grey if person likely to be out of UK on census date  
**Bug Fixes**

#### Updated Release Version 3.6.1.3 of FTAnalyzer

**New Features**  
**Updates**  
Added features to exclude people with unknown births from census reports  
**Bug Fixes**

#### Updated Release Version 3.6.1.2 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Default FamilySearch UK census searches to England if unknown country  
GoogleFixes.xml allows empty to strings

#### Updated Release Version 3.6.1.1 of FTAnalyzer

**New Features**  
**Updates**  
FamilySearch set as default search provider for Lost Cousins Searches  
**Bug Fixes**  
Tweaks to Loose Births to ignore effects of long range dates  
FamilySearch now understands UK Searches

#### New Release Version 3.6.1.0 of FTAnalyzer

**New Features**  
Added Export to Excel for Treetops and World Wars Reports  
Added Option to show compact census references  
**Updates**  
Add Witnesses as custom fact type so that it functions the same as Witness custom fact type  
**Bug Fixes**  
Colour census "All green" now includes all with flag of green

#### Updated Release Version 3.6.0.2 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Colour BMD Filter two values were wrong way round  
Fix for Western Australia being mistaken for Washington, USA  
Census refs can optionally end in a ;  
**Library Updates**

  
#### Updated Release Version 3.6.0.1 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Allow census references to be less specific for matching  
Facts that have ADDR and PLAC tags now use both  
**Library Updates**

#### New Release Version 3.6.0.0 of FTAnalyzer

**New Features**  
Added Loose Birth & Death export to Excel menu items  
Added Lost Cousins Referrals Report  
Added Export Sources to Excel  
**Updates**  
Added notice of XP support removal  
Locations now understand ISO 3116-1 Alpha-3 country codes  
Duplicate check now uses standardised name file to compare forenames thus understanding that eg:Jacobus and James are variants of the same name, even though they don't sound alike.  
Referrals sorts using Lost Cousins Short Codes.  
Duplicate checks now give heavy penalty to known different parents  
Add filter function to show only matching referrals  
Referral report now has title and status bar count  
**Bug Fixes**  
Printng, previewing and exporting to excel no longer fails if no records listed  
Facts before birth and facts after death now flags as errors  
Locations show maps buttons weren't showing correct viewports  
**Library Updates**  
Updated SharpMap to v1.1  
Updated BruTile to v0.9.8  
Replaced TreeViewMS with MultiSelectTreeView

#### New Release Version 3.5.1.0 of FTAnalyzer

**Updates**  
Include UNKNOWN & Reference facts  
Sources form columns now resizable  
People form now has toolbar to allow printing and exporting to Excel  
**Bug Fixes**  
More robustly deal with facts & sources so that every fact is linked to its source and vice versa.  
Treetops and World Wars no longer multi select  
Hourglass change that broke hourglass now fixed again  
Apply common default sorting to people form  
**Library Updates**  
Updated Log4Net to v1.2.13  
Update DotNetZip to 1.9.2  
Update SQLLite to 1.0.92

#### New Release Version 3.5.0.0 of FTAnalyzer

**New Features**  
Added sources tab - double clicking lists all facts for that source  
Double clicking on a fact in the facts report lists all sources for that fact  
Individuals reports now lists notes in GEDCOM, right click on individuals reports to view notes where they exist in the GEDCOM  
Extra column in Individuals reports lists whether notes exist or not.  
Added Possible census facts report which lists all people who have a note with the word census in it. The idea is this should help people with census records as notes work out who has a census note.

#### New Release Version 3.4.1.0 of FTAnalyzer

**Updates**  
Added Child Born facts to facts reports so that it gives a better timeline of facts for an individual  
**Bug Fixes**  
Fixed crash if user clicks on duplicates grid or slider etc whilst duplicates data is being calculated.  
Updated Hourglass function  
Fixed flickering on duplicates tab

#### New Release Version 3.4.0.0 of FTAnalyzer

**New Features**  
Added a duplicates report - allows you to check if you have any likely duplicates in your file  
Added a filter to the facts report tab  
Added a sources report available by double clicking on a fact in the facts report  
**Updated Features**  
Added support for Family Historian Census references  
Facts report now shows surname at date field  
Multiple individual facts reports for different individuals can now be open at the same time  
Shift clicking on WWI searches now searches beta version of Lives of First World War site to try to find Life story page for that individual  
Added support for alternate name facts they appear as extra facts in the facts report  
Added parent fact type to facts report  
**Bug Fixes**  
Relation to root wasn't showing on census reports  
Lost cousins census report tooltips were inaccurate  
Lost cousins census report now has an option of a shift click to search census  
Fix for null locations causing duplicates crash  
Fixed facts reports reached by double clicking on individuals in duplicates page  
Fixed 1911 Family Historian census references  
Reports multiple fact forms for duplicate option on load  
Allowed 1841 Family historian census reference to be more tolerant of variants.  
Added select all/deselect all to fact tab  
Single click now works for selecting fact types to filter  
Unknown fact types no longer have blank descriptions

#### New Release Version 3.3.3.0 of FTAnalyzer

**New Features**  
Added Relation to Root description column to various reports  
Add support for 1901 & 1911 Ancestry Irish Census searches  
Double date errors now have reason message.  
Added relationship progress bar  
**Bug Fixes**  
Lost Cousins and Census Facts were erroring for non UK census dates  
Printing forms was erroneously using same title for all reports  
Colour reports right columns now sizable

#### Updated Release Version 3.3.2.8 of FTAnalyzer

**New Features**  
Added support for 1921 Canadian Census

**Updates**  
**Bug Fixes**

#### Updated Release Version 3.3.2.7 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**

1911 Irish Lost Cousins census wasn't showing people

#### Updated Release Version 3.3.2.6 of FTAnalyzer

**New Features**  
**Updates**  
**Bug Fixes**  
Colour BMD & Census column widths now save/reload properly  
1881 LostCousins years wasn't showing correct colours

#### Updated Release Version 3.3.2.5 of FTAnalyzer

**New Features**  
**Updates**

Double dates now uses the upper year not lower year as the upper bound on a date  
Force database update to clean up bad viewport items  
Add logging for re-trying with original location text  
Title bar now shows which GEDCOM file you are working on

**Bug Fixes**

Updating location geocode status now correctly updates treeviews

#### Updated Release Version 3.3.2.4 of FTAnalyzer

**New Features**  
**Updates**  
Added restore tooltip  
Added remember location feature to most forms  
Added logging for Google fixes  
Google fixes now load when GEDCOM loads and reports to the stats window  
**Bug Fixes**

#### Updated Release Version 3.3.2.3 of FTAnalyzer

**New Features**  
Added option for users to use their own GoogleFixes.xml file  
**Bug Fixes**  
Fix for edit location not restoring pre-search value if clicked no to save on exit  
Fix for edit location search not moving to correct location on search  
Google geocoding viewport was getting wrongly set

#### Updated Release Version 3.3.2.2 of FTAnalyzer

**New Features**  
**Bug Fixes**  
Places selection now actually shows a result  
Fix for max living date within 9 months of MINDATE.

#### Updated Release Version 3.3.2.1 of FTAnalyzer

**New Features**  
**Bug Fixes**  
Fix mitre limit to avoid mitred joins on line ends  
Fix for saving size and position of mainform  
Add end cap triangles to line.  
Places selection now actually shows a resultFix for max living date within 9 months of MINDATE.

#### New Release Version 3.3.2.0 of FTAnalyzer

**New Features**  
Surname tab - reports counts of individuals, families & marriages in a family.  
Also links to Guild of One Name Studies site where surname is a GOONS study  
**Bug Fixes**  
Fix dates with huge numbers of spaces  
Count of not searched locations was wrong

#### New Release Version 3.3.1.0 of FTAnalyzer

**Updates**  
Lifelines Report now highlights birth facts (red teardrop), death facts (black teardrop) and currently selected fact (green teardrop).  
Changing the selected fact highlights the point on the map.  
Also works the other way using the query tool you can select a fact and see it highlighted on the map  
Tooltip also now shows the facts beneath the mouse.  
**Bug Fixes**  
All times birth facts and death facts are looked at the rule to use christening/burial facts is implemented

#### Updated Release Version 3.3.0.1 of FTAnalyzer

**New Features**  
Added Places form - You can now select a place or places and see who lived there on a map. Discover different branches of your family living in the same area perhaps uncover new leads.  
Clicking on a cluster or teardrop icon on new places map shows you who was at that location  
Database Upgrade to use projected LatM/LongM instead of lat/long this will make map drawing quicker. **It does mean that the first time the program loads it will need to update your database. PLEASE LET THIS FINISH.**  
**Updates**  
Facts table now displays Relation  
Facts table now shows grey/white bands per individual  
Added TileCaches to maps  
Added Birth Reg, Marriage Reg and Death Reg custom facts as alternates for Birth, Marriage, Death facts  
Geocode locations filter menu now has icons  
MainForm now remembers position as well as size  
Hide Scale bar is now a global option on all maps  
Added support for Google Country/Region/SubRegion & MultiLevel text substitution in FactLocationFixes.xml file this will translate old 19th Century locations to modern ones that Google can find without needing to touch the GEDCOM file.  
Added support for 10 APR-15 JUL 1918, 9-17 JUL 1824 and 10 APR 1914-15 APR 1918 style dates  
Places & Lifeline remembers splitter positions  
ADDR tags in GEDCOM are now recognised as locations if the PLACe tag is empty  
**Bug Fixes**  
Report Options reports all options now  
Fact Source text that was on a continuation line in the GEDCOM was missing text on subsequent lines  
Tweaks to Parent Age Profile report  
Facts form row selector was too narrow  
Lifeline expands to include the viewport as well as the coordinate eg: This should mean Scotland is no longer zoomed in to a point on Loch Tay  
Database restore now forces update if restoring an old database  
Added titles to all messageboxes  
Map Individuals Icons refreshed on edit location  
Clicking to open locations window always ensures that location isn't filtered  
If a file encounters too many errors and user says to quit it will now fail gracefully

#### New Release Version 3.2.0.0 of FTAnalyzer

**Major Features**  
Added Lifelines map - shows where ancestors lived with lines for how they moved over time. Allows selection of one or more individuals  
Added ability to display custom maps - [see documentation for details](https://ftanalyzer.codeplex.com/wikipage?title=Adding%20Custom%20Maps)  
Added ability to display English & Welsh parish boundaries on a map - [see documentation for details](http://ftanalyzer.codeplex.com/wikipage?title=Displaying%20England%20and%20Wales%20Parish%20Boundaries)  
Added Facts Report this shows all facts recorded in your database for all individuals  
**Lifelines Map**  
Shows lists of people and where they move in time  
Right click a person to select all family their members, ancestors, descendants or all relatives  
Displays Google & geocoding data in lifeline facts  
Double click any lifeline fact to enable location editing for that fact location  
Added a save & exit button to edit locations  
Added option to hide labels  
**Updates**  
Relations check only now sets direct ancestor for natural parent ie: step/adopted etc parents  
Added remaining recognised countries  
When geocoding if no Google Match with country, county, region string try again with GEDCOM Location  
Add option to reset partials  
Add event to location editing to refresh lifeline facts  
**Bug Fixes**  
Fix bug with valid double dates with double date year ending in 00  
Fixed double dates isbefore, startsbefore, isafter, endsafter  
Remove "fix" for Durham as it was causing towns in Durham to appear in Durham, Durham.  
Retried partial Google Matches now works  
Fixed locations tab bold text for known countries  
Lost Cousins census check in coloured reports now checks census country before flagging yellow.  
Exiting edit location checks for saved point if yes to save rather than search moving point.  
Turn off auto size mode on source column in facts  
Fix surname search for facts  
If GEDCOM CHAR set is UTF-8 then use the UTF-8 encoding

#### New Release Version 3.1.2.0 of FTAnalyzer

**New Features**  
Added database restore option  
Allow for natural/step/adopted etc relationships (uses \_MREL & \_FREL tags)  
Added support for 3 digit years (year only)  
Added count of unknown facttypes on load  
Data Errors now show unknown fact types  
Save main form's size on close  
Reverse Geocoding now uses zoom level to pick place  
Logger added creates a FTAnalyzer.log file for reverse geocoding info and future debugging support  
**Bug fixes**  
Fact dates tolerate spaces between dashes  
Force Treeview font to bold or regular to hopefully fix truncation issues

#### New Release Version 3.1.1.0 of FTAnalyzer

### Edit Location Enhancements

Double clicking now zooms in (left) or out (right)  
New Search box jumps to locations entered if Google can identify it  
New Edit button allows you to pick up marker even if its not on current zoom level  
Reset button will reset the location back to close to how it was on load  
Added option to copy and paste locations - useful when you have several locations in same street/place and you don't want to manually edit them all  
Marking a location as verified or editing a location now updates the Google location by reverse geocoding  
Added check for Ancestry 1911 census references  
Tweaks to Google Georeferencing levels  
Fixed map issue when behind a proxy  
Understands GEDCOM DEATH Y tags used when death date/location unknown but it is known the individual is dead.  
Added support for MON YYYY-MON YYYY format dates

#### New Release Version 3.1.0.0 of FTAnalyzer

#### New Features

The Mapping feature can now view your locations using **Historic OS Maps** and modern OS Maps.

#### Updates

Map location list now shows Google location  
Backup of Geocodes database remembers last backup location  
Map selector remembers the last map used

#### Bugfixes

Don't add pre-marriage individuals if already in a family

#### New Release Version 3.0.3.0 of FTAnalyzer

Count Isle of Man as England & Wales for Census  
Lost Cousins text was preventing drag and drop of files  
Lost Cousins no Countries form won’t open more than one copy  
Changed sort order of Lost Cousins buttons to match report which matches Lost Cousins website  
UK Census date verification is only applied to UK facts  
Counts duplicate Lost Cousins facts  
Added report for Lost Cousins facts but no census fact  
Added Census Duplicate and Census Missing Location reports  
Lost Cousins fact lists don't show people with no country  
Flag census families where checking treetops person before they married as "Pre-Marriage"  
Census facts were getting confused when user had BEF or AFT dates  
People who born or die on a census date are included in census families  
Cell style changes applied to Inherited style as a performance gain  
Census grid refreshes on change of provider thus updating tooltips  
First fact of a fact type for an individual is recorded as the preferred fact  
Individuals report now has a double click for facts  
People form shows proper split grid and scrollbars and now has double click for facts

#### New Release Version 3.0.2.4 of FTAnalyzer

Added Lost Cousins missing country report  
Tweaked Loose Births to take account of childrens ages  
Fixed bug with export to excel from census report

#### New Release Version 3.0.2.3 of FTAnalyzer

Fixed bug with census dates always being 1881.  
Lost Cousins tab now has a proper Relation Types filter  
Lost Cousins tab buttons have lost weight  
Add Individual ID to facts list  
Initial work on reverse geocoding - doesn't do anything with the results it finds yet

#### New Release Version 3.0.2.2 of FTAnalyzer

Fixed Select All/Clear All  
On finished Geocoding display results on stats tab  
Added terms of use links to maps  
Unknown fact types now display fact type description from GEDCOM  
Custom facts were erroneously checking for Birth/Marriage/Death text  
Reset any User entered/verified locations to set the Google location to empty

#### New Release Version 3.0.2.1 of FTAnalyzer

Added right click menus to census, Lost Cousins, and both colour reports to view facts for individuals.  
Fixed issue with 1851 & 1901 census dates for colour report not working due to matching Canadian census dates  
Tolerated census dates now set the adjusted year  
Family facts weren't getting checked for data errors when added to facts list

#### New Release Version 3.0.2.0 of FTAnalyzer

Using Google Maps Geocoder v3 - **NB. your old Google Matched results will be reset**  
Added Use Burial/Cremation dates option if no death date  
Fix US Census that were inaccurate dates  
Add option to set minimum parental age for loose births  
Added Select All/Clear All to Geocode status  
Colour BMDs won't now show red for people still likely to be alive  
Added extra 4 year double date option eg: 11 Mar 1747/1748 is now recognised  
Grey/white banding on census report now groups by sort column  
Added recent files list feature  
Added extra map background tiles to Timeline  
Fixes to loose births to use death fact when calculating earliest living date  
Loose Births & Deaths sorted by surname, forename

#### New Release Version 3.0.1.0 of FTAnalyzer

**New Features**  
Added a Loose Births report  
Added ability to backup Geocoding database  
Added option to only apply family census facts to parents  
People on a census outside UK now show as such in census report (includes "equivalent" US census year eg: 1880 for 1881 UK).  
**Bug Fixes**  
Fix check for not_searched and partial_match not re-checking Google.

#### New Release Version 3.0.1.0 of FTAnalyzer

**New Features**  
Added a Loose Births report  
Added ability to backup Geocoding database  
Added option to only apply family census facts to parents  
People on a census outside UK now show as such in census report (includes "equivalent" US census year eg: 1880 for 1881 UK).

#### New Release Version 3.0.0.2 of FTAnalyzer

New Mapping Feature
===================

Version 3 introduces a new mapping feature that allows you to see your ancestors plotted on a modern Google Map.  
  
This works by searching Google Maps for the locations in your GEDCOM and storing the results. This process is known as Geocoding. The program can then plot those results on various types of map.  
  
In v3.0.0.0 there is one initial Timeline map showing where everyone in your file was in a particular year.  

New Features since v2.2.1.1
---------------------------

Added Geocoding of locations in file from Google Maps. This new form is also the report showing the list of locations and how they were geocoded.  
Added display of individuals on Google Map as a Timeline view  
What's New menu links to What's New page on website.  
Map Locations can be edited by the user if the Google Geocoding wasn't correct.  

Updates
-------

Locations now shows Lat/Long/Geocoded status, Google results, and status icon  
Locations treeview now has icons to show geocoding status  
Ancestry.co* merge data for censuses now parsed on loading  
Dataerror check box status remembered between sessions  
Added option to search again for not found items  
Added web proxy detection if behind a proxy serverMap refreshes on pan and year change  
Map Individuals list now has double click to see facts for person  
Added filters to Geolocations form  
Added extra birth fact to individual if fact has age but no birth date recorded  
Census form (for census and Lost cousins) is now fully sortable  
  
**Geocoding**  
If too many Google Queries then stop processing  
Now defaults to UK Google geocoding  
If a location isn't geocoded instead of ignoring it on timeline try at next level.  
Geocoding respects bounding boxes of countries to avoid a similarly named place in a different country getting picked up.  
  
**Edit Locations**  
Added Save and reset map buttons  
Sort order preserved on refreshing edited locations  
Add tooltip to tell user how to edit location on GeoLocations form  
Change Edit Location to place pointer using right mouse click  
  
**Timeline**  
Added playback option and associated play/stop buttons.  
Button selected highlight added to pointer button  
Added Option to lock zoom level  
Solo person shown with Google teardrop  
Clusterer added - this means as you zoom out data is clustered together as you zoom in you see the individual dots. The selection tool allows you to select an icon and view everyone where that is their best location in that year.  
Timeline map ignores those born more than 110 years at timeline date.  
Add option to display all locations  
Timeline step change is 5 if range is 150 years or less  
Implemented fact date limits in timeline  
Added map scalebar to timeline map  
  
**Lost Cousins**  
Added 1940 US Census button to form  
Form now displays count by country and year as per Lost Cousins website  
Count also now shows number of people you have a census for but no Lost Cousins fact  
Lost Cousins buttons wasn't respecting country chosen  
Show Lost Cousins facts after death as a data error  
Lost Cousins report displays the census reference info if it is found in a source  
  
**Bug Fixes**  
Colour census death now uses ends before  
IsMarried now uses starts before  
Fix bug with locations not opening for places tab  
Fix bug with locations for less detailed levels not showing if on more detailed tab  
Fixed Google Maps button not showing map  
Bing & GoogleMaps now don't bother geo-coding if it already has the location geocoded  
Image in treeview doesn't change when selected  
Fix tooltip zoom icon tooltip text  
Fixed bugs with Census & Lost Cousins forms headings  
Fixed Census & Lost Cousins forms ignoring census records for people with no parents

#### New Release Version 2.2.1.1 of FTAnalyzer  
  
Fix for 1Q style quarter days  
  
#### New Release Version 2.2.1.0. of FTAnalyzer  
  
Colour BMD, Colour Census, Facts Report & Census all now :  
use custom data grids  
have export to excel  
have printing options including colours and icons  
have sortable columns  
save/load/reload column order, sort order and column widths  
Census facts on family are now included in all individuals in family alive at time of census  
Changing Use Residence facts as census facts now requires a reload to force update of data errors  
Status bar message on main form now shows what actions can happen on a double click  
First tentative search Ancestry.co.uk code on double click BMD.

#### New Release Version 2.2.0.1. of FTAnalyzer

Residence facts location checked for census date warning.  
Do not prompt to reload file if no settings are changed.  
Fix for unknown death dates not being checked for loose deaths  
Added Reload option to main form  
Added missing BMD report filters  
Speedup loading of colour reports fix sort order  
Various extra countries recognised  
All standalone forms now implement custom column ordering  
Printing now allows icon printing and colours printing

#### New Release Version 2.2.0.0. of FTAnalyzer

### New Features in v2.2.0.0

Coloured Census form now moved to separate tab "Search Summaries"  
Added BMD colour report to "Search Summaries" tab

*   this means you can now see BMD & Census at a glance to see what data you are missing
*   Plus for marriage facts it shows colours for no partner but of marriage age, no partner + children or partner but no marriage

Facts now checks for valid census dates  
Added option to tolerate Census dates where the year is right but the exact date is wrong these are accepted if the census year is valid  
Counts of errors and warnings for Census, Residence and Lost Cousins facts shown on load

### Small Changes to functionality

Facts List now displays sources  
Added Age at Fact to Facts display  
Added Select All/Clear All to data errors list  
Data Errors are now sortable and have an error type column  
Added Census date range too wide error type  
Moved Treat Residence Facts as Census facts to a Global option  
Add support for 1921 Canada Census  
Added a report an issue link to help menu  
Treeview now allows map views buttons to work  
Fact Form now shows error facts for an individual too with error icons and tooltips for reason  
Various minor Bug Fixes
