'use strict';
app.controller('placesController', ['$scope', 'placesService', function ($scope, placesService) {

    $scope.places = [];
    var markers = [];
    var watchID = navigator.geolocation.watchPosition(showPosition, positionError);
    var directionsDisplay = new google.maps.DirectionsRenderer({ suppressMarkers: true });
    var directionsService = new google.maps.DirectionsService();
    var actualCoords;
    $scope.errorMessage = "No errors";
    $scope.iframeHeight = window.innerHeight;
    $scope.iframeWidth = window.innerWidth;
    $scope.actualRadius = 100;
    $scope.infoBox = new google.maps.InfoWindow();
    $scope.shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    $scope.$on('mapInitialized', function (event, map) {
        if ($scope.map) {
            placesService.AddMap(map);
        } else {
            placesService.GetMap(map);
        }
        google.maps.event.addListener($scope.map, 'tilesInitialized', function () {
            console.log("map loaded");
        });
    });

   



    function showPosition(position) {
        var coords = position.coords;
        $scope.actualLatitude = coords.latitude;
        $scope.actualLongitude = coords.longitude;
        actualCoords = new google.maps.LatLng($scope.actualLatitude, $scope.actualLongitude);
        $scope.map.setCenter(actualCoords);
        $scope.showInfoBox();
    }

    $scope.showInfoBox = function (city) {

        $scope.infoBox.setContent("You are here.<br /> Find a place" + $scope.actualRadius + " meters around you.");
        $scope.infoBox.setPosition(actualCoords);
        $scope.infoBox.open($scope.map);
      
    }

    $scope.findCenterOfMap = function () {

        $scope.map.setCenter(actualCoords);
    }

    $scope.logMsg = function (msg) {
        $scope.errorMessage = msg;
    }

    function positionError(e) {
        switch (e.code) {
            case 0:
                // UNKNOWN_ERROR
                $scope.logMsg("The application has encountered an unknown error while trying to determine your current location. Details: " + e.message);
                break;
            case 1:
                // PERMISSION_DENIED
                $scope.logMsg("You chose not to allow this application access to your location.");
                break;
            case 2:
                // POSITION_UNAVAILABLE
                $scope.logMsg("The application was unable to determine your location.");
                break;
            case 3:
                // TIMEOUT
                $scope.logMsg("The request to determine your location has timed out.");
                break;
        }
    }


    $scope.boundsChanged = function (event) {
        $scope.actualRadius = Math.round(this.radius);
        $scope.infoBox.setContent("You are here.<br /> Find a place in " + $scope.actualRadius + " meters.");
    };

    $scope.placeslist = new kendo.data.DataSource({
        transport: {
            read: {
                url: "http://demos.telerik.com/kendo-ui/service/products",
                dataType: "jsonp"
            }
        },
        pageSize: 2
    });

    $scope.showDirections = function (latEnd, longEnd, transport) {
        var chosentravelMode = google.maps.TravelMode.WALKING;
        if (transport === "car") {
            chosentravelMode = google.maps.TravelMode.DRIVING;
        } else if (transport === "bus") {
            chosentravelMode = google.maps.TravelMode.TRANSIT;
        } else if (transport === "bike") {
            chosentravelMode = google.maps.TravelMode.BICYCLING;
        }

        directionsDisplay.setMap(null);
        var start = $scope.actualLatitude + "," + $scope.actualLongitude;
        var end = latEnd + "," + longEnd;

        if ((latEnd != null) || (longEnd != null)) {
            directionsDisplay.setMap($scope.map);
            var request = {
                origin: start,
                destination: end,
                optimizeWaypoints: true,
                travelMode: chosentravelMode
            };

            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);

                }
            });
        } else {
            $scope.logMsg("Sorry, couldn't find coordinates.");
        }
    }

    $scope.triggernode = function (clickEl) {
        placesService.GetExternalList($scope.actualLatitude, $scope.actualLongitude, "gothenburg", clickEl).then(function (d) {
            $scope.image = {
                url: '../content/images/' + clickEl + '.png',
                size: new google.maps.Size(32, 37),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 37)
            };
            $scope.adverts = d.data.adverts;
            $scope.placesList = new kendo.data.DataSource({
                data: d.data.adverts,
                pageSize: 5
            });


            for (var i = 0; i < $scope.adverts.length; i++) {
                markers[i] = new google.maps.Marker({
                    title: $scope.adverts[i].companyInfo.companyName,
                    shape: $scope.shape,
                    icon: $scope.image.url,
                    zIndex: i + 1
                });

                var lat = parseFloat(d.data.adverts[i].location.coordinates[0].latitude);
                var lng = parseFloat(d.data.adverts[i].location.coordinates[0].longitude);
                if ((lat != undefined) || (lng != undefined)) {
                    var markerlatlng = new google.maps.LatLng(lat, lng);
                    var distance = google.maps.geometry.spherical.computeDistanceBetween(actualCoords, markerlatlng);
                    if (distance <= $scope.actualRadius) {
                        markers[i].setPosition(markerlatlng);
                        markers[i].setMap($scope.map);
                    }
                }
            }
        });
    }

    $scope.findDirection = function (address) {
        placesService.GetDirections(address).then(function (results) {
            $scope.placeByaddress = results.data;
            var missingMarker = new google.maps.Marker({
                title: "Testeo",
                shape: $scope.shape,
                icon: '../content/images/car.png',
                zIndex: 10
            });
            var lat = parseFloat(results.data.results[0].geometry.location.lat);
            var lng = parseFloat(results.data.results[0].geometry.location.lng);
            if ((lat != undefined) || (lng != undefined)) {
                var latlng = new google.maps.LatLng(lat, lng);
                missingMarker.setPosition(latlng);
                missingMarker.setMap($scope.map);
            }
        });
    }

    placesService.GetAllPlaces().then(function (results) {
        $scope.source = new kendo.data.DataSource({
            data: results.data,
            pageSize: 4
        });
    }, function (error) {
        //alert(error.data.message);
    });

    placesService.GetAllPeople().then(function (results) {
        $scope.people = results.data;
    }, function (error) {
        //alert(error.data.message);
    });

}]);