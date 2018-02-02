var map;

function initialize() {
    var myOptions = { 
        credentials: "AuvyoYez01osvLqvR8ywVwWZvb4GAc9EcOk7eyUXv7ZDudVD26oX5XZhPjiWkNU6", 
        center: new Microsoft.Maps.Location(51.5, -.1), 
        zoom: 10, 
        mapTypeId: Microsoft.Maps.MapTypeId.ordnanceSurvey 
        }
    map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), myOptions);
}

function setCenter(lat, lng) {
    var latVal = parseInt(lat);
    var longVal = Microsoft.Maps.Location.normalizeLongitude(lng);
    map.setView({ center: new Microsoft.Maps.Location(latVal, longVal), mapTypeId: Microsoft.Maps.MapTypeId.ordnanceSurvey });
}

//function addMarker(lat, lng) {
//    var view = new Microsoft.Maps.Map.setView(lat, lng);
//    var options = { position: point, map: map, visible: true };
//    var marker = new Microsoft.Maps.Map.Marker(options);
//    return marker;
//}

function frontAndCenter(lat, lng) {
    setCenter(lat, lng);
//    addMarker(lat, lng);
}

function setBounds(neLat, neLng, swLat, swLng) {
//    var nwLatVal = parseInt(neLat);
//    var nwLongVal = Microsoft.Maps.Location.normalizeLongitude(swLng);
//    var seLatVal = parseInt(swLat);
//    var seLongVal = Microsoft.Maps.Location.normalizeLongitude(neLng);
    var nw = new Microsoft.Maps.Location(neLat, swLng);
    var se = new Microsoft.Maps.Location(swLat, neLng);

    var viewRect = Microsoft.Maps.LocationRect.fromCorners(nw, se);
    map.setView({ bounds: viewRect });
}
