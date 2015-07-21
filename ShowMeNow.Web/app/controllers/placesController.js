'use strict';
app.controller('placesController', ['$scope', 'placesService', function ($scope, placesService) {

    $scope.places = [];
    var markers = [];
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

    $scope.placeslist = new kendo.data.DataSource({
        transport: {
            read: {
                url: "http://demos.telerik.com/kendo-ui/service/products",
                dataType: "jsonp"
            }
        },
        pageSize: 2
    });

    $scope.triggernode = function (clickEl) {

        console.log("clicked on " + clickEl);
        placesService.GetExternalList(57.709421, 11.964304, "gothenburg", clickEl).then(function (d) {
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
                    var latlng = new google.maps.LatLng(lat, lng);
                    markers[i].setPosition(latlng);
                    markers[i].setMap($scope.map);
                }
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