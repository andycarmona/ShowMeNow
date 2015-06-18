angular.module('SnapSvgDirectives', [])
.directive('snapTest', function () {
    return function(scope, element, attrs) {
       
     


        var initializeSvg = function() {
            scope.s = Snap("#graphic_1");
          
        }

        var AddCircle=function(val) {
            var bigCircle = scope.s.circle(val, val, 50);
            bigCircle.attr({
                id:"myId",
                fill: "#bada55",
                stroke: "#000",
                strokeWidth: 5
            });

            var line = scope.s.paper.line(0, 0, 100, 100);
            var textValid = scope.s.text(val, val, "Testeo");
            textValid.attr({
                id: '1_textvalid',
                'font-size': '25px',
                fill: 'green',
                stroke: 'green',
                'stroke-width': 0.2
            });

            bigCircle.click(clickCallback);
            textValid.click(clickCallback);
            var group = scope.s.group(bigCircle, textValid);
            group.data("loco", 999);


        }

        //click callback
        var clickCallback = function (event) {
            // how do I get the id of the clicked element?
            // is this cross browser valid?
            //var id = event.target.attributes.id.nodeValue;
            this.attr({ fill: 'blue' });
            console.log("clicked on " + this.attr("id"));
        };

      var addTextBox = function() {
            var textValid = scope.s.text(0, 20, "Testeo");
            textValid.attr({
                id: '1_textvalid',
                'font-size': '25px',
                fill: 'green',
                stroke: 'green',
                'stroke-width': 0.2
            });

            textValid.click(clickCallback);
      }
      initializeSvg();
        var initial = 150;
      for (var i = 1; i < 5;i++)
        {
            AddCircle(initial*i);
        }
        // addTextBox();
    }
});