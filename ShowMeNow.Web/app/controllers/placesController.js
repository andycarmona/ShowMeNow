'use strict';
app.controller('placesController', ['$scope','placesService',  function ($scope,placesService) {

    $scope.places = [];
    //placesService.InitializeDataBase();
    $scope.iframeHeight = window.innerHeight;
    $scope.iframeWidth = window.innerWidth;
    $scope.styleContainer = function () {
        var style1 = "width: 400px;height:400px;";
     
            return style1;
       
    }

    $scope.monthPickerConfig = {
        start: "year",
        depth: "year",
        format: "MMMM yyyy"
    };

    //$scope.source = new kendo.data.DataSource({
    //    transport: {
    //        read: {
    //            url: "http://demos.telerik.com/kendo-ui/service/products",
    //            dataType: "jsonp"
    //        }
    //    },
    //    pageSize: 2
    //});

    placesService.GetAllPlaces().then(function (results) {

        $scope.places = results.data;


    }, function (error) {
        //alert(error.data.message);
    });

    placesService.GetAllPeople().then(function (results) {

        $scope.people = results.data;


    }, function (error) {
        //alert(error.data.message);
    });

    //$scope.products = new kendo.data.DataSource({
    //    data: [
    //       { id: 1, name: 'Tennis Balls', department: 'Sports', lastShipment: '10/01/2013' },
    //       { id: 2, name: 'Basket Balls', department: 'Sports', lastShipment: '10/02/2013' },
    //       { id: 3, name: 'Oil', department: 'Auto', lastShipment: '10/01/2013' },
    //       { id: 4, name: 'Filters', department: 'Auto', lastShipment: '10/01/2013' },
    //       { id: 5, name: 'Dresser', department: 'Home Furnishings', lastShipment: '10/01/2013' }
    //    ],
    //    pageSize: 2
    //});
}]);