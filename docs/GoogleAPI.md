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
   a) Click on APIs then click on details on the right  
   ![APIs](http://www.ftanalyzer.com/GoogleAPI-5a.png)
   b) then click on credentials  
   ![credentials](http://www.ftanalyzer.com/GoogleAPI-5b.png)  
   c) then click create credentials   
   ![create credentials](http://www.ftanalyzer.com/GoogleAPI-5c.png)
   d) and select API Key  
   ![select API key](http://www.ftanalyzer.com/GoogleAPI-5d.png)
   e) once your key is displayed click close  
   ![created API](http://www.ftanalyzer.com/GoogleAPI-5e.png)
6) Now copy your API key from this page by clicking on button next to key  
   ![Copy Key](http://www.ftanalyzer.com/GoogleAPI-6.png)
7) Open FTAnalyzer Tools Menu and select Options
    a) Select the Mapping tab  
   ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-8.png)
    b) Right click on the Google API box and paste in your key
    c) Click OK to save your key.
8) Now back on the website we need to setup billing 
    - NB. billing will be zero if you stay under the 40,000 lookups a month but Google no longer allows you to use the lookups without setting up billing.  
    ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-11.png)
    a) Click to link a billing account  
    ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-11a.png)
    b) If you don't have a billing account it will prompt you to create one  
    ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-11b.png)
9) Select your country and agree to terms and conditions
    ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-12a.png)
10) Entry you billing information and save
    ![Mapping Tab](http://www.ftanalyzer.com/GoogleAPI-12b.png)

Thats's it once you start your free trial you are all set to use your own personal Google API limits and 40,000 lookups a month should be plenty for you. 

Note you can always check your usage by going to the [Google API dashboard](https://console.cloud.google.com/google/maps-apis/overview) to make sure you never exceed the limit and thus always have totally free lookups.

NB. Remember to keep your key private and don't disclose it to anyone to ensure that no one else can use it.