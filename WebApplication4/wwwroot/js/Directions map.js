

    var CustomerLatitude = parseFloat('@ViewBag.CustomerLatitude');
    var CustomerLongitude = parseFloat('@ViewBag.CustomerLongitude');
    var ProviderLatitude = 32.55556;
    var ProviderLongitude = 35.85;



    function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: CustomerLatitude, lng: CustomerLongitude }, // Center the map on the customer location
    zoom: 10
            });

    // Add marker for customer location
    var customerMarker = new google.maps.Marker({
        position: {lat: CustomerLatitude, lng: CustomerLongitude },
    map: map,
    title: 'Customer Location'
            });

    // Add marker for provider location
    var providerMarker = new google.maps.Marker({
        position: {lat: ProviderLatitude, lng: ProviderLongitude },
    map: map,
    title: 'Provider Location'
            });

    // Calculate and display route between customer and provider
    var directionsService = new google.maps.DirectionsService();
    var directionsDisplay = new google.maps.DirectionsRenderer();
    directionsDisplay.setMap(map);

    var origin = new google.maps.LatLng(CustomerLatitude, CustomerLongitude);
    var destination = new google.maps.LatLng(ProviderLatitude, ProviderLongitude);

    var request = {
        origin: origin,
    destination: destination,
    travelMode: 'DRIVING'
            };
    directionsService.route(request, function (response, status) {
                if (status == 'OK') {
        directionsDisplay.setDirections(response);

    // Extract the distance and duration from the response
    var route = response.routes[0];
    var leg = route.legs[0];
    var distance = leg.distance.text;
    var duration = leg.duration.text;

    // Display the distance and duration
                    document.getElementById('distance').innerText = distance;
                    document.getElementById('duration').innerText = duration;
                } else {
            window.alert('Directions request failed due to ' + status);
                }
            });
        }


