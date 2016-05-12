declare var Chart: any;

module Sgc14.Charts {
    "use strict";

    interface ITemporalChartAttributes extends ng.IAttributes {
        temporalChart: string;
    }

    class Piece {
        constructor(public name: string, public value: number) { }
    }

    export class TemporalChartDirective {
        constructor($parse: Function) {
            return {
                restrict: "A",
                link: ($scope: ng.IScope, $element: ng.IAugmentedJQuery, $attrs: ITemporalChartAttributes): void => {
                    var weekdays: Array<string> = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                    var today: Date = new Date(Date.now());
                    var weekday = today.getDay();
                    var labels: Array<string> = [];

                    for (var i = 1; i <= 7; i++) {
                        labels.push(weekdays[(weekday + i) % weekdays.length]);
                    }

                    console.log(labels.length);

                    var canvas: HTMLCanvasElement = <HTMLCanvasElement> $element[0];
                    var ctx: CanvasRenderingContext2D = canvas.getContext("2d");
                    var chart: any = new Chart(ctx).Line({
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
                    var exp: any = $parse($attrs.temporalChart);
                    $scope.$watchCollection(exp, (newValues: Array<Models.ISGC14Item>): void => {
                        if (!newValues || newValues.length === 0) {
                            return;
                        }

                        // We only want items from at most one week ago.
                        var sevenDaysInMs: number = 604800000;
                        var todayInMs: number = today.getTime();
                        var oneWeekAgo: number = new Date(todayInMs - sevenDaysInMs).getTime();
                        var items: Array<Models.ISGC14Item> = newValues.filter((item: Models.ISGC14Item): boolean => {
                            var created = new Date(item.created);
                            return created.getTime() >= oneWeekAgo;
                        });

                        // group by week day.
                        var map: Map<number, number> = new Map<number, number>();
                        items.forEach((item: Models.ISGC14Item): void => {
                            var day = new Date(item.created).getDay();

                            // adjust day for label shift, e.g. Tuesday might not be == 2.
                            var actualWeekday = labels.indexOf(weekdays[day]);
                            var value = map.has(actualWeekday) ? map.get(actualWeekday) + 1 : 1;
                            map.set(actualWeekday, value);
                        });

                        // update chart.
                        map.forEach((value: number, key: number): void => {
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
    }

    angular.module("SGC14").directive("temporalChart", [
        "$parse",
        ($parse: Function): TemporalChartDirective => new TemporalChartDirective($parse)
    ]);
} 