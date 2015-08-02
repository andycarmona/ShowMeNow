'use strict';
app.factory('placesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var placesServiceFactory = {};
    var maps = {};

    var _addMap = function (mapId) {
        maps[mapId] = {};
    }

    var _getMap = function (mapId) {
        if (!maps[mapId]) _addMap(mapId);
        return maps[mapId];
    }

    var _initializeDB = function () {

        return $http.get(serviceBase + 'api/Places/InitializeDatabase');
    };

    var _addInitialPeople = function () {

        return $http.get(serviceBase + 'api/Places/AddInitialPeople');
    };

    var _getAllPlaces = function () {

        return $http.get(serviceBase + 'api/Places/GetAllPlaces');
    };

    var _getAllPeople = function () {

        return $http.get(serviceBase + 'api/Places/GetAllPeople');
    };

    var _getExternalList = function (actualLatitude, actualLongitude, area, searchword,radius) {
        var params = JSON.stringify({
            latitude: actualLatitude,
            longitude: actualLongitude
        });
        var url = "http://api.eniro.com/cs/search/basic?country=se&version=1.1.3&geo_area=" + area + "&search_word=" + searchword + "&actualLatitude=" + actualLatitude + "&actualLongitude=" + actualLongitude + "&max_distance="+radius+"&key=330905261700999336&profile=andyw&callback=JSON_CALLBACK";
        var promise = $http.jsonp(url).then(function (response) {
            var filteredResult=checkPointInArea(actualLatitude, actualLongitude, response,radius);
            return filteredResult;
        });
        return promise;
    }

    function checkPointInArea(actualLatitude, actualLongitude, mapData,radius) {

        var filteredResult = [];
        var actualPosition = new google.maps.LatLng(actualLatitude, actualLongitude);
        for (var i = 0; i < mapData.data.adverts.length; i++) {
            var lat = parseFloat(mapData.data.adverts[i].location.coordinates[0].latitude);
            var lng = parseFloat(mapData.data.adverts[i].location.coordinates[0].longitude);
            var markerPosition = new google.maps.LatLng(lat, lng);
            var distance = google.maps.geometry.spherical.computeDistanceBetween(actualPosition, markerPosition);
            if (distance <= radius) {
                filteredResult.push(mapData.data.adverts[i]);
            }
        }
        return filteredResult;

    }

    var _getDirections = function (addressToSearch) {
        var url = "http://maps.google.com/maps/api/geocode/json?address=" + addressToSearch + ",gothenburg&sensor=false";
        var promise = $http.get(url).then(function (response) {
            return response;
        });
        return promise;
    }

    placesServiceFactory.InitializeDataBase = _initializeDB;
    placesServiceFactory.AddInitialPeople = _addInitialPeople;
    placesServiceFactory.GetAllPlaces = _getAllPlaces;
    placesServiceFactory.GetAllPeople = _getAllPeople;
    placesServiceFactory.GetExternalList = _getExternalList;
    placesServiceFactory.GetDirections = _getDirections;
    placesServiceFactory.AddMap = _addMap;
    placesServiceFactory.GetMap = _getMap;

    return placesServiceFactory;

}]);