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

        var TemporalChartDirective = (function () {
            function TemporalChartDirective($parse) {
                return {
                    restrict: "A",
                    link: function ($scope, $element, $attrs) {
                        var weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                        var today = new Date(Date.now());
                        var weekday = today.getDay();
                        var labels = [];

                        for (var i = 1; i <= 7; i++) {
                            labels.push(weekdays[(weekday + i) % weekdays.length]);
                        }

                        console.log(labels.length);

                        var canvas = $element[0];
                        var ctx = canvas.getContext("2d");
                        var chart = new Chart(ctx).Line({
                            labels: labels,
                            datasets: [
                                {
                                    label: "Mentions over time",
                                    fillColor: "rgba(220,220,220,0.2)",
                                    strokeColor: "rgba(220,220,220,1)",
                                    pointColor: "rgba(220,220,220,1)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "#fff",
                                    pointHighlightStroke: "rgba(220,220,220,1)",
                                    data: [0, 0, 0, 0, 0, 0, 0]
                                }
                            ]
                        }, { responsive: true });

                        // watch for when data changes.
                        var exp = $parse($attrs.temporalChart);
                        $scope.$watchCollection(exp, function (newValues) {
                            if (!newValues || newValues.length === 0) {
                                return;
                            }

                            // We only want items from at most one week ago.
                            var sevenDaysInMs = 604800000;
                            var todayInMs = today.getTime();
                            var oneWeekAgo = new Date(todayInMs - sevenDaysInMs).getTime();
                            var items = newValues.filter(function (item) {
                                var created = new Date(item.created);
                                return created.getTime() >= oneWeekAgo;
                            });

                            // group by week day.
                            var map = new Map();
                            items.forEach(function (item) {
                                var day = new Date(item.created).getDay();

                                // adjust day for label shift, e.g. Tuesday might not be == 2.
                                var actualWeekday = labels.indexOf(weekdays[day]);
                                var value = map.has(actualWeekday) ? map.get(actualWeekday) + 1 : 1;
                                map.set(actualWeekday, value);
                            });

                            // update chart.
                            map.forEach(function (value, key) {
                                if (value <= 0) {
                                    return;
                                }

                                chart.datasets[0].points[key].value = value;
                            });
                            chart.update();
                        });
                    }
                };
            }
            return TemporalChartDirective;
        })();
        Charts.TemporalChartDirective = TemporalChartDirective;

        angular.module("SGC14").directive("temporalChart", [
            "$parse",
            function ($parse) {
                return new TemporalChartDirective($parse);
            }
        ]);
    })(Sgc14.Charts || (Sgc14.Charts = {}));
    var Charts = Sgc14.Charts;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=TemporalChart.directive.js.map
