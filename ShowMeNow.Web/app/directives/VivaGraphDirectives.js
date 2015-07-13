angular.module('VivaGraphDirectives', [])
.directive('vivagraphTest', function () {
    return function(scope, element, attrs) {
       
     


        var initializeSvg = function() {
            var graphics = Viva.Graph.View.svgGraphics();

            // we will use SVG patterns to fill circle with image brush:
            // http://stackoverflow.com/questions/11496734/add-a-background-image-png-to-a-svg-circle-shape
            scope.defs = Viva.Graph.svg('defs');
            graphics.getSvgRoot().append(scope.defs);
           

            $(scope.defs).click(function () {
                console.log("clicked");
            });
            
            graphics.node(createNodeWithImage)
              .placeNode(placeNodeWithTransform);

            scope.graph = constructGraph();
            var renderer = Viva.Graph.View.renderer(scope.graph, {
                graphics: graphics,
                interactive: false,
                container: document.getElementById('graph-container')
            });

            renderer.run();
      
        }

        

        var createNodeWithImage =function(node) {
            var radius = 12;
            // First, we create a fill pattern and add it to SVG's defs:
            var pattern = Viva.Graph.svg('pattern')
                .attr('id', "imageFor_" + node.id)
                .attr('patternUnits', "userSpaceOnUse")
                .attr('width', 500)
                .attr('height', 500);

            var image = Viva.Graph.svg('image')
              .attr('x', '0')
              .attr('y', '0')
              .attr('height', radius * 2)
              .attr('width', radius * 2)
              .link(node.data.url);
           
            pattern.append(image); 
            scope.defs.append(pattern);

            // now create actual node and reference created fill pattern:
            var ui = Viva.Graph.svg('g');
            var circle = Viva.Graph.svg('circle')
              .attr('cx', radius)
              .attr('cy', radius)
              .attr('fill', 'url(#imageFor_' + node.id + ')')
              .attr('r', radius);
            $(circle).click(function () {
                console.log("clicked on "+ node.id);
            });
            ui.append(circle);
            return ui;
        };

        var placeNodeWithTransform=function(nodeUI, pos) {
            // Shift image to let links go to the center:
            nodeUI.attr('transform', 'translate(' + (pos.x - 12) + ',' + (pos.y - 12) + ')');
        }

        var constructGraph=function() {
            var graph = Viva.Graph.graph();

            graph.addNode('anvaka', {
                url: 'https://secure.gravatar.com/avatar/91bad8ceeec43ae303790f8fe238164b'
            });
            graph.addNode('manunt', {
                url: 'https://secure.gravatar.com/avatar/c81bfc2cf23958504617dd4fada3afa8'
            });
            graph.addNode('thlorenz', {
                url: 'https://secure.gravatar.com/avatar/1c9054d6242bffd5fd25ec652a2b79cc'
            });
            graph.addNode('bling', {
                url: 'https://secure.gravatar.com/avatar/24a5b6e62e9a486743a71e0a0a4f71af'
            });
            graph.addNode('diyan', {
                url: 'https://secure.gravatar.com/avatar/01bce7702975191fdc402565bd1045a8?'
            });
            graph.addNode('pocheptsov', {
                url: 'https://secure.gravatar.com/avatar/13da974fc9716b42f5d62e3c8056c718'
            });
            graph.addNode('dimapasko', {
                url: 'https://secure.gravatar.com/avatar/8e587a4232502a9f1ca14e2810e3c3dd'
            });

            graph.addLink('anvaka', 'manunt');
            graph.addLink('anvaka', 'thlorenz');
            graph.addLink('anvaka', 'bling');
            graph.addLink('anvaka', 'diyan');
            graph.addLink('anvaka', 'pocheptsov');
            graph.addLink('anvaka', 'dimapasko');

            return graph;
        }
       

    
      initializeSvg();
    }
});