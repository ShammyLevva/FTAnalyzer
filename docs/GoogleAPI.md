## Google API Keys ##
### Background Information ###
In order to geocode your locations and turn them into Latitude and Longitudes for display on a map FTAnalyzer needs to lookup the Google Maps website and use what is called an API to get the coordinates.

In July 2018 Google changed their pricing mechanism from a largely free setup to one that costs $5/1000 lookups. With the first 40,000 lookups a month free. 
For FTAnalyzer this means that if the sum total of every user in the world's lookups exceeds 40,000 in a month then I am charged $5/1000 lookups over 40,000.

In December 2018 the total usage reached 60,127 lookups and so I was charged for the 20,127 lookups over the 40,000 which cost me $100.64 naturally for a free product I cannot sustain this sort of cost or higher per month. So I have been forced to setup more restrictive daily caps to ensure I don't exceed the monthly limit of 40,000 lookups and thus have to pay.

This makes FTAnalyzer less usable as all the users are sharing that one lower limit per day. So I decided to add a feature that allows users to add their own Google API Key.

A Google API Key is entirely free to create and private to you. You will never pay for it unless you were to do more than 40,000 lookups in any one month and their website tracks your usage so you can always see how close to the limit you are.

### Creating your own Google API Key ###
1) Visit the [Google API Key website](https://cloud.google.com/console/google/maps-apis/overview) and login with your existing Google account if you have one, or create a new one.  
   ![Example Screen when logged in](http://www.ftanalyzer.com/GoogleAPI-1.png) 
2) Click to create a new Project  
   ![Creating a Project](http://www.ftanalyzer.com/GoogleAPI-2.png) 
3) Search for Geocoding API and click to view it
   ![Searching for Geocoding API](http://www.ftanalyzer.com/GoogleAPI-3.png) 
4) Enable Geocoding API  
   ![Enable Google API](http://www.ftanalyzer.com/GoogleAPI-4.png)
5) Get your API Key for FTAnalyzer
   a) Click on APIs  
   ![APIs](http://www.ftanalyzer.com/GoogleAPI-5a.png)
   b) then click on credentials  
   ![credentials](http://www.ftanalyzer.com/GoogleAPI-5b.png)  
   c) then click create credentials   
   ![create credentials](http://www.ftanalyzer.com/GoogleAPI-5c.png)
   d) and select API Key  
   ![select API key](http://www.ftanalyzer.com/GoogleAPI-5d.png)
   e) once your key is displayed click close  
   ![created API](http://www.ftanalyzer.com/GoogleAPI-5e.png)
6) Now copy your API key to paste it into FTAnalyzer  
   ![Copy Key](http://www.ftanalyzer.com/GoogleAPI-6.png)