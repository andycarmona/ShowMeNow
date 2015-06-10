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

    placesServiceFactory.InitializeDataBase = _initializeDB;
    placesServiceFactory.AddInitialPeople = _addInitialPeople;

    return placesServiceFactory;

}]);