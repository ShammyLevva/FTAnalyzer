## Using A Custom Map in FT Analyzer
In order to use a custom map on the Edit Locations map you need to have the maps saved as "Georefererenced" TIFF files. This means a few conditions need to be satisfied:
# the file must be a TIFF format graphic file
# the file cannot be a pdf or any other type of file
# the file must contain information that tells the program where to display the map (georeference info)
# the files to display must all be in the same directory
It is unlikely that any scanned map you have has the extra info that tells Family Tree Analyzer how to display it on screen. Basically the file needs to know how a point on the scanned map file relates to a point in the real world. If you do have georeferenced files all you need to do is to tell FT Analyzer where the files are in the Tools | options menu under Mapping Settings. The program will then load then when you click on the show custom map overlay button.

If you have a scanned map file you can add georeference info yourself although it takes a bit of patience and needs a free program.

**The basic steps are:**
# load your map into a GIS program
# load a reference background map - eg: Google Maps
# mark 5 or 6 points on your scanned map then link them to the identical point on the reference map
# run the georeferencer
# save the file including the georeferenced info

**The Detailed Steps**
# Download QGIS from [http://www.qgis.org/en/site/forusers/download.html](http://www.qgis.org/en/site/forusers/download.html)
# Install OpenLayers Plugin
## Click Plugins Menu and select Manage and Install Plugins
## Select Get More and scroll down to select Open Layers Plugin
# Once Open Layers plugin is installed then load the background layer of choice
## Click Plugins Menu and select Open Layers plugin
## Select a background layer eg: select Add Google Physical Layer
# Now prepare your scanned map
## take the scanned map and using a graphics program trim the margins to remove borders, grid numbers, legends etc. Suitable graphics programs are:
### Paint.Net [http://download-paint.net/](http://download-paint.net/)
### ImageMagick [http://www.imagemagick.org/script/index.php](http://www.imagemagick.org/script/index.php)
### Irfanview [http://www.irfanview.com/](http://www.irfanview.com/)
### GIMP [http://www.gimp.org/](http://www.gimp.org/)
## Save the trimmed map as a TIFF file this is vital it cannot be a JPG file or other graphic format.
# Now load the trimmed map into QGIS
## Click Raster Menu and Georeferencer in the Georeferencer popout - The Georeferencer window should open
## Click File and Open Raster then browse to find your prepared scanned map and click to open it
## In the Coordinate Reference System dialog we need to choose the right map system
### In the filter box of the dialog type 3857 this will reduce the list in the lower pane to the item you want
### In the lower pane select WGS 84 / Pseudo Mercator and click OK to load the scanned map
# You should now have two windows open one with the background map and one with the scanned map
# Now we begin the process of mapping points on the scanned map to points on the reference map. 
**What is the Georeferencing Process**
The idea is that with several points, at least 5 or 6, mapped showing the exact point on one map that is the same as on the other map then the program can work out how to rotate and stretch the scanned map to fit the real map. For best results spread the points selected across the map eg: near the four corners and the centre, but not too regular - ie: try to avoid points with similar horizontal or vertical co-ordinates. You are trying to get a spread across various points so two similar horizontal points in a row isn't giving that variation. 

NB. Choose points on your historic scanned map that will not have moved and are very easy to work out where they are on a modern map.

You are trying to be pixel perfect in placing the points and selecting points on a modern map. The more accurate you are the more accurately the scanned map will line up with a modern map when the process is finished. So it pays to spend time doing more points and precisely placing them. 

NB. This is a one off process once you have Georeferenced the map once and saved the result you need never touch it again. It will always then load and overlay on top of a modern map.

**The Georeferencing Process** (aka placing the points)
For each point you place follow this procedure
# Zoom in on your map to as much detail as you can and find a recognisable point that won't have changed in the years between the maps
## Triangulation points at the top of hills are great for this
## buildings/roads aren't great as exactly where the line is may have changed, 
## rivers etc are really bad as the course of the river will have changed. 
## Rocky coastlines that haven't eroded can be good too if its really easy to pick a particular point. 
## Ideal points are gridlines on a map IF and ONLY IF the lines refer to exactly the same thing. You can then pick a crosshair and know that it exactly maps to the crosshair on the other map
# Click the add point button on Georeferencer window (the one with your scanned map) its the 7th button along a gray line with white crosshair and little yellow star beneath it. A window pops up.
# Now click on the from map canvass button to go back to your modern map layer in the other window
# Find exactly the same point on the modern map (Note you may have to retry this a few times before you get the two maps showing the same area)
# Once you click the exact spot on the modern map that was the same as the pixel on the old map then it will load the co-ordinates into the window. 
# Click OK to save that point a little red dot will appear on the old map and a line will appear in the grid below it

Repeat the above for at least 5 or 6 points. The more points you enter and the more accurate you are the better the results of the Georeferencing process.

Once you have entered enough points you are ready to create your file. 
# First we backup the points. Click File and Save GCP Points this is so that the work you did isn't lost if you don't like the result you can try more points or delete some bad ones and add better ones
# Now we start the Georeferencer by clicking File Menu and selecting Start Georeferencing. Note if this is the first time you have done this it will warn about a "transformation type" and bring up a transformations settings dialog. 
# All you need to enter here is the path and filename of the file to save click the button to the right of the output raster box and select a path and filename by default it will be called the same as your input file with -modified stuck on the end.
# Check that the "target SRS" setting is "EPSG:3857"
# You will want to see the result so make sure the "Load in QGIS when done" box is ticked
# Click ok to generate the georeferenced map

The program will create the map and show the result in the layers panel of the original form. To see how well its done you want to set the new map to be somewhat transparent. 

# Right Click on the new layer and select properties
# Select the transparency option and move the Global transparency slider to say 50% this will let you see a lot of the background map (NB. the new layer must be listed higher in the layers list than the background if not it will be hidden behind the background)

If all is well and you've been careful to pick the points correctly you should see your new map overlaid pretty much exactly over the top of the modern map.

All you now need is to put the modified tiff files in a directory and in FT Analyzer from the Tools Options menu select Mapping Settings and select the custom maps directory to be the directory the new modified tiff files are saved into. NB. Keep the raw non-referenced files in a different directory otherwise FT Analyzer will try and fail to load these non-referenced files.

Now you can see your map when in Edit Locations. If you click on the little map scroll icon in the toolbar it will load your map at the right location over the top of the modern map. NB. Transparency isn't currently supported in FT Analyzer.
