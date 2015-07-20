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

    $scope.image = {
        url: 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png',
        size: [20, 32],
        origin: [0, 0],
        anchor: [0, 32]
    };
    $scope.shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };
    $scope.beaches = [
      ['Bondi Beach', 57.709421, 11.964304, 4],
      ['Coogee Beach', 57.709203, 11.963671, 5],
      ['Cronulla Beach', -34.028249, 151.157507, 3],
      ['Manly Beach', -33.80010128657071, 151.28747820854187, 2],
      ['Maroubra Beach', -33.950198, 151.259302, 1]
    ];

    $scope.monthPickerConfig = {
        start: "year",
        depth: "year",
        format: "MMMM yyyy"
    };
    var markers = [];
    for (var i = 0; i < 10 ; i++) {
        markers[i] = new google.maps.Marker({
            title: "Hi marker " + i
        });
    }

   
    //$scope.source = new kendo.data.DataSource({
    //    transport: {
    //        read: {
    //            url: "http://demos.telerik.com/kendo-ui/service/products",
    //            dataType: "jsonp"
    //        }
    //    },
    //    pageSize: 2
    //});
 
        //$scope.hotel = results.CompanyInfo.companyName;
    placesService.GetExternalList().then(function (d) {
      
        $scope.adverts = d.data.adverts;

        for (i = 0; i < 8; i++) {
            var lat = parseFloat(d.data.adverts[i].location.coordinates[0].latitude);
            var lng = parseFloat(d.data.adverts[i].location.coordinates[0].longitude);
            if ((lat != undefined) || (lng != undefined)) {
                var latlng = new google.maps.LatLng(lat, lng);
                markers[i].setPosition(latlng);
                markers[i].setMap($scope.map);
            }
        }
    });

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