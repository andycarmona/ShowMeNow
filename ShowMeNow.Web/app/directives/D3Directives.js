angular.module('D3Directives', [])
.directive('d3Test', function () {
    return function(scope, element, attrs) {
       
     


        var initializeSvg = function() {
         
            d3.select("graphic_1").append("circle").attr("cx", 25).attr("cy", 25).attr("r", 25).style("fill", "purple");
        }

        var AddGraph=function() {
 
        }

    
      initializeSvg();
      AddGraph();
    }
});