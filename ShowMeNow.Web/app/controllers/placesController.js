'use strict';
app.controller('placesController', ['$scope,$http',  function ($scope,$http,placesService) {

    $scope.places = [];
    placesService.InitializeDataBase();

}]);