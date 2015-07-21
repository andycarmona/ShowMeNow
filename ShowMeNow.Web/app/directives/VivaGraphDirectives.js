angular.module('VivaGraphDirectives', [])
.directive('vivagraphTest', function () {
    return function (scope, element, attrs) {

        var initializeSvg = function () {
            var graphics = Viva.Graph.View.svgGraphics();
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

        var createNodeWithImage = function (node) {
            var radius = 12;
            // First, we create a fill pattern and add it to SVG's defs:
            var pattern = Viva.Graph.svg('pattern')
                .attr('id', "imageFor_" + node.id)
                .attr('patternUnits', "userSpaceOnUse")
                .attr('width', 650)
                .attr('height', 650);

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
                scope.triggernode(node.id);

            });
            ui.append(circle);
            return ui;
        };

        var placeNodeWithTransform = function (nodeUI, pos) {
            // Shift image to let links go to the center:
            nodeUI.attr('transform', 'translate(' + (pos.x - 12) + ',' + (pos.y - 12) + ')');
        }

        var constructGraph = function () {
            var graph = Viva.Graph.graph();

            graph.addNode('info', {
                url: '../content/images/info.png'
            });
            graph.addNode('restaurant', {
                url: '../content/images/restaurant.png'
            });
            graph.addNode('hotel', {
                url: '../content/images/hotel.png'
            });
            graph.addNode('coffee', {
                url: '../content/images/coffee.png'
            });
            graph.addNode('dance', {
                url: '../content/images/dance.png'
            });
            graph.addNode('fastfood', {
                url: '../content/images/fastfood.png'
            });
            graph.addNode('festival', {
                url: '../content/images/festival.png'
            });

            graph.addLink('info', 'coffee');
            graph.addLink('info', 'restaurant');
            graph.addLink('info', 'dance');
            graph.addLink('info', 'hotel');
            graph.addLink('info', 'festival');
            graph.addLink('info', 'fastfood');

            return graph;
        }



        initializeSvg();
    }
});