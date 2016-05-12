var Sgc14;
(function (Sgc14) {
    (function (Charts) {
        "use strict";

        var Piece = (function () {
            function Piece(name, value) {
                this.name = name;
                this.value = value;
            }
            return Piece;
        })();

        var SentimentChartDirective = (function () {
            function SentimentChartDirective($parse) {
                return {
                    restrict: "A",
                    link: function ($scope, $element, $attrs) {
                        var canvas = $element[0];
                        var ctx = canvas.getContext("2d");
                        var chart = new Chart(ctx).Pie([
                            {
                                value: 0.0000367,
                                highlight: "#50C878",
                                color: "#00A693",
                                label: "Positive"
                            },
                            {
                                value: 0.0000367,
                                color: "#CF1020",
                                highlight: "#FF007F",
                                label: "Negative"
                            }
                        ], {
                            responsive: true
                        });

                        // watch for when data changes.
                        var exp = $parse($attrs.pieChart);
                        $scope.$watchCollection(exp, function (newValues) {
                            if (!newValues || newValues.length === 0) {
                                return;
                            }

                            var positives = newValues.filter(function (item) {
                                return item.score > 0.0;
                            }).length;

                            var negatives = newValues.filter(function (item) {
                                return item.score < 0.0;
                            }).length;

                            if (positives === 0.0) {
                                positives = 0.0000367;
                            }

                            if (negatives === 0.0) {
                                negatives = 0.0000367;
                            }

                            chart.segments[0].value = positives;
                            chart.segments[1].value = negatives;
                            chart.update();
                        });
                    }
                };
            }
            return SentimentChartDirective;
        })();
        Charts.SentimentChartDirective = SentimentChartDirective;

        angular.module("SGC14").directive("pieChart", [
            "$parse",
            function ($parse) {
                return new SentimentChartDirective($parse);
            }
        ]);
    })(Sgc14.Charts || (Sgc14.Charts = {}));
    var Charts = Sgc14.Charts;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=SentimentChart.directive.js.map
