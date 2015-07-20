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

    var _getExternalList = function() {
        var promise = $http.jsonp('http://api.eniro.com/cs/search/basic?country=se&geo_area=gothenburg&version=1.1.3&search_word=hoteller&latitude=57.709421&longitud=11.964304&max_distance=100&key=330905261700999336&profile=andyw&to_list=5&callback=JSON_CALLBACK').then(function (response) {

            return response;
        });
        return promise;
    }

    placesServiceFactory.InitializeDataBase = _initializeDB;
    placesServiceFactory.AddInitialPeople = _addInitialPeople;
    placesServiceFactory.GetAllPlaces = _getAllPlaces;
    placesServiceFactory.GetAllPeople = _getAllPeople;
    placesServiceFactory.GetExternalList = _getExternalList;

    return placesServiceFactory;

}]);