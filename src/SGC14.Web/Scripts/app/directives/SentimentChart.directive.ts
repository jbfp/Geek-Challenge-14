declare var Chart: any;

module Sgc14.Charts {
    "use strict";

    interface ISentimentChartScope extends ng.IScope {
        render(data: Array<Models.ISentiment>): void;
    }

    interface ISentimentChartAttributes extends ng.IAttributes {
        pieChart: string;
    }

    class Piece {
        constructor(public name: string, public value: number) { }
    }

    export class SentimentChartDirective {
        constructor($parse: Function) {
            return {
                restrict: "A",
                link: ($scope: ISentimentChartScope, $element: ng.IAugmentedJQuery, $attrs: ISentimentChartAttributes): void => {
                    var canvas: HTMLCanvasElement = <HTMLCanvasElement> $element[0];
                    var ctx: CanvasRenderingContext2D = canvas.getContext("2d");
                    var chart: any = new Chart(ctx).Pie([
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
                    var exp: any = $parse($attrs.pieChart);
                    $scope.$watchCollection(exp, (newValues: Array<Models.ISentiment>): void => {
                        if (!newValues || newValues.length === 0) {
                            return;
                        }

                        var positives: number = newValues.filter(
                            (item: Models.ISentiment): boolean => item.score > 0.0).length;

                        var negatives: number = newValues.filter(
                            (item: Models.ISentiment): boolean => item.score < 0.0).length;

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
    }

    angular.module("SGC14").directive("pieChart", [
        "$parse",
        ($parse: Function): SentimentChartDirective => new SentimentChartDirective($parse)
    ]);
} 