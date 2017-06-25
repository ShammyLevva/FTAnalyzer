# Maps
| Show Timeline | Show Lifelines | [Show Places](Maps---Show-Places) | [Run Geocoder to Find Locations](#RunGeocoder) | [Display Geocoded Locations](#ReviewGeocodedResults) |
## Background
The Maps feature of Family Tree Analyzer (FTA) was introduced in v3 and will slowly evolve over time. The quality of the information you see on the maps FTA produces will be directly related to the quality of the place information in your Family History program and the data that it exports to the GEDCOM file read by FTA. If you are new to FTA and have yet to review your Location data as shown on the [Locations tab](The-Locations-Tab), that would be a good place to start. 

As we know, our ancestors often named their town in the new country after their home town in their country of origin, for example Boston, MA, USA being named after Boston, Lincolnshire, England. You will find your experience of FTA's Maps feature less painful if your addresses include Country plus County/State/Province to reduces ambiguity (but that is not mandatory).

{anchor:RunGeocoder}
## Getting Started with Maps
So you think your Location data is in reasonable shape.  Before you can see those places on a map, FTA needs to find geographic co-ordinates for all of those places. It does this by taking each of your places in turn and making calls to the Google Maps service for you, in a process known as geocoding. See [Wikipedia - Geocoding](url_http___en.wikipedia.org_wiki_GeocodingWikipedia) for a full explanation. The result for each address is written an a small database which FTA stores on your computer for its future use.

To start the geocoding process, click the Maps menu item and pick **Run Geocoder to Find Locations**. Depending on the size of your GEDCOM file and the speed of your internet connection, this may take some time on the first occasion.

The geocoding run will complete when either (1) FTA has looked up all the locations in your file or (2) FTA detects it has reached the daily limit of look-ups which Google permits (which is currently around 2000 items).  If you hit the daily limit, you will need to invoke "Run Geocoder to Find Locations" on a subsequent day; FTA will continue geocoding your addresses from where it left off.

{anchor:ReviewGeocodedResults}
## Reviewing the Geocoded Results
As tempting as it may be to immediately view a map, you really need to look at the results you got from Geocoding your locations, so you better understand the map outputs. Google's Map Service is heavily orientated to the here and now, whilst your GEDCOM file will contain many historic addresses which no longer exist. The [Locations Geocoding Status Report](Locations-Geocoding-Status-Report) is explained [here](Locations-Geocoding-Status-Report).

