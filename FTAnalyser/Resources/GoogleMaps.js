var map;

function initialize() {
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
        zoom: 12,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
}

function setCenter(lat, lng) {
    map.setCenter(new google.maps.LatLng(lat, lng));
}

function addMarker(lat, lng) {
    var point = new google.maps.LatLng(lat, lng);
    var options = { position: point, map: map, visible: true };
    var marker = new google.maps.Marker(options);
    return marker;
}

function frontAndCenter(lat, lng) {
    setCenter(lat, lng);
    addMarker(lat, lng);
}

function setViewport(neLat, neLng, swLat, swLng) {
    var ne = new google.maps.LatLng(neLat, neLng);
    var sw = new google.maps.LatLng(swLat, swLng);
    var viewport = new google.maps.LatLngBounds(sw, ne);
    map.fitBounds(viewport);
}
