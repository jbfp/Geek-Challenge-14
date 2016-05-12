var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var SentimentController = (function () {
            function SentimentController($scope, $data) {
                this.$scope = $scope;
                this.$data = $data;
                this.$scope.vm = this;
            }
            SentimentController.prototype.isSentimentVisible = function () {
                return this.$data.items.filter(function (item) {
                    return item.type === "tweet";
                }).map(function (item) {
                    return item.sentiment;
                }).filter(function (sentiment) {
                    return sentiment.score !== 0.0;
                }).length > 0;
            };

            Object.defineProperty(SentimentController.prototype, "items", {
                get: function () {
                    var tweets = this.$data.items.filter(function (item) {
                        return item.type === "tweet";
                    });
                    return tweets.map(function (tweet) {
                        return tweet.sentiment;
                    });
                },
                enumerable: true,
                configurable: true
            });
            return SentimentController;
        })();
        Controllers.SentimentController = SentimentController;

        angular.module("SGC14").controller("SentimentController", [
            "$scope",
            "Data",
            function ($scope, $data) {
                return new SentimentController($scope, $data);
            }
        ]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Sentiment.controller.js.map
