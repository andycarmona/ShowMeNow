angular.module('VivaGraphDirectives', [])
.directive('vivagraphMenu', function () {
    return function (scope, element, attrs) {

        var initializeSvg = function () {
           scope.graphics = Viva.Graph.View.svgGraphics();
            scope.defs = Viva.Graph.svg('defs');
            scope.graphics.getSvgRoot().append(scope.defs);


         

            scope.graphics.node(createNodeWithImage)
              .placeNode(placeNodeWithTransform);

            scope.graph = constructGraph();
            var renderer = Viva.Graph.View.renderer(scope.graph, {
                graphics: scope.graphics,
                interactive: false,
                container: document.getElementById('graph-container')
            });
            renderer.run();

        }

        var createNodeWithImage = function (node) {
            var radius = 20;
            // First, we create a fill pattern and add it to SVG's defs:
            var pattern = Viva.Graph.svg('pattern')
                .attr('id', "imageFor_" + node.id)
                .attr('patternUnits', "userSpaceOnUse")
                .attr('width', 350)
                .attr('height', 350);

            var image = Viva.Graph.svg('image')
              .attr('x', '0')
              .attr('y', '0')
              .attr('height', radius * 2)
              .attr('width', radius * 2)
              .link(node.data.url);

            var svgText = Viva.Graph.svg('text')
                .attr('y', '-4px')
                 .attr('height', radius * 2)
              .attr('width', radius * 2)
                .attr('color','#FFFFFF')
                .text(node.id);

            pattern.append(svgText);
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
                if (node.id != "info") {
                    scope.triggernode(node.id);
                } else {
                    scope.findCenterOfMap();
                }

            });
            $(circle).hover(function () { // mouse over
                highlightRelatedNodes(node.id, true);
            }, function () { // mouse out
                highlightRelatedNodes(node.id, false);
            });
            ui.append(circle);
            return ui;
        };
      

        var highlightRelatedNodes = function (nodeId, isOn) {
            // just enumerate all realted nodes and update link color:
            scope.graph.forEachLinkedNode(nodeId, function (node, link) {
                var linkUI = scope.graphics.getLinkUI(link.id);
                if (linkUI) {
                    // linkUI is a UI object created by graphics below
                    linkUI.attr('stroke', isOn ? 'red' : 'gray');
                    linkUI.textContent = "teste";
                }
            });
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
}).directive('vivagraphSocial', function () {
    return function (scope, element, attrs) {

        var initializeSvg = function () {
            scope.graphics = Viva.Graph.View.svgGraphics();
            scope.defs = Viva.Graph.svg('defs');
            scope.graphics.getSvgRoot().append(scope.defs);




            scope.graphics.node(createNodeWithImage)
              .placeNode(placeNodeWithTransform);

            scope.graph = constructGraph();
            var renderer = Viva.Graph.View.renderer(scope.graph, {
                graphics: scope.graphics,
                interactive: true,
                container: document.getElementById('social-container')
            });
            renderer.run();

        }

        var createNodeWithImage = function (node) {
            var radius = 20;
            // First, we create a fill pattern and add it to SVG's defs:
            var pattern = Viva.Graph.svg('pattern')
                .attr('id', "imageFor_" + node.id)
                .attr('patternUnits', "userSpaceOnUse")
                .attr('width', '100%')
                .attr('height', '100%');

            var image = Viva.Graph.svg('image')
              .attr('x', '0')
              .attr('y', '0')
              .attr('height', radius * 2)
              .attr('width', radius * 2)
              .link(node.data.url);

         

           // pattern.append(svgText);
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
            $(circle).hover(function () { // mouse over
                highlightRelatedNodes(node.id, true);
            }, function () { // mouse out
                highlightRelatedNodes(node.id, false);
            });
            ui.append(circle);
            return ui;
        };


        var highlightRelatedNodes = function (nodeId, isOn) {
            // just enumerate all realted nodes and update link color:
            scope.graph.forEachLinkedNode(nodeId, function (node, link) {
                var linkUI = scope.graphics.getLinkUI(link.id);
                var svgText = Viva.Graph.svg('text')
             .attr('y', '-4px')
              .attr('height', 40)
           .attr('width', 40)
             .attr('color', '#FFFFFF')
             .text(node.id);
                if (linkUI) {
                    // linkUI is a UI object created by graphics below
                    linkUI.attr('stroke', isOn ? 'red' : 'gray');
                    linkUI.append(svgText);
                }
            });
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