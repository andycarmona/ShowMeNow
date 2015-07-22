angular.module('UtilsDirectives', []).directive('resize', function ($window) {
    return function (scope) {
        scope.width = $window.innerWidth;
        scope.height = $window.innerHeight;
        angular.element($window).bind('resize', function () {
            scope.$apply(function () {
                if ($window.innerHeight > 800) {
                    scope.width = $window.innerWidth;
                    scope.height = $window.innerHeight;
                } 
            });
        });
    };
});