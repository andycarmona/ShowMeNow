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

    var _getExternalList = function (latitude, longitude, area, searchword) {
        var params = JSON.stringify({
            latitude: latitude,
            longitude: longitude
        });
        var url = "http://api.eniro.com/cs/search/basic?country=se&version=1.1.3&geo_area=" + area + "&search_word=" + searchword + "&max_distance=100&key=330905261700999336&profile=andyw&from_list=0&to_list=5&callback=JSON_CALLBACK&data=";
        var promise = $http.jsonp(url + params).then(function (response) {

            return response;
        });
        return promise;
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