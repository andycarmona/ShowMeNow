'use strict';
app.factory('placesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var placesServiceFactory = {};

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

    placesServiceFactory.InitializeDataBase = _initializeDB;
    placesServiceFactory.AddInitialPeople = _addInitialPeople;
    placesServiceFactory.GetAllPlaces = _getAllPlaces;
    placesServiceFactory.GetAllPeople = _getAllPeople;
    return placesServiceFactory;

}]);