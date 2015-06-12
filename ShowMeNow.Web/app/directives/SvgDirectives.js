angular.module('SvgDirectives', [])
.directive('snapTest', function () {
    return function(scope, element, attrs) {
       
     


        var initializeSvg = function() {
            scope.s = Snap(element[0]);
          
        }

        var AddCircle=function() {
            var bigCircle = scope.s.circle(150, 150, 100);
            bigCircle.attr({
                fill: "#bada55",
                stroke: "#000",
                strokeWidth: 5
            });
        }

      var addTextBox = function() {
            var textValid = scope.s.text(0, 20, "Testeo");
            textValid.attr({
                'id': '1_textvalid',
                'font-size': '25px',
                'fill': 'green',
                'stroke': 'green',
                'stroke-width': 0.2
            });
      }
      initializeSvg();
      AddCircle();
      addTextBox();
    }
});