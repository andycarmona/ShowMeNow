﻿angular.module('SigmaDirectives', [])
.directive('sigmaTest', function () {
    return function(scope, element, attrs) {
       
     


        var initializeSvg = function() {
            scope.s = new sigma(element[0]);
          
        }

        var AddGraph=function() {
            // Then, let's add some data to display:
            scope.s.graph.addNode({
                // Main attributes:
                id: 'n0',
                label: 'Hello',
                // Display attributes:
                x: 0,
                y: 0,
                size: 1,
                color: '#f00'
            }).addNode({
                // Main attributes:
                id: 'n1',
                label: 'World !',
                // Display attributes:
                x: 1,
                y: 1,
                size: 1,
                color: '#00f'
            }).addEdge({
                id: 'e0',
                // Reference extremities:
                source: 'n0',
                target: 'n1'
            });

            // Finally, let's ask our sigma instance to refresh:
            scope.s.refresh();
        }

    
      initializeSvg();
      AddGraph();
    }
});