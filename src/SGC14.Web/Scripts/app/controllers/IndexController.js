var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var IndexController = (function () {
            function IndexController($scope, $http) {
                this.$scope = $scope;
                this.$http = $http;
                this._suggestions = [];
                this.$scope.vm = this;
                this.getSuggestions();
            }
            Object.defineProperty(IndexController.prototype, "suggestions", {
                get: function () {
                    return this._suggestions;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(IndexController.prototype, "showSuggestions", {
                get: function () {
                    return this.suggestions.length > 0;
                },
                enumerable: true,
                configurable: true
            });

            IndexController.prototype.getSuggestions = function () {
                var _this = this;
                this.$http.get("/suggestions").success(function (suggestions) {
                    _this._suggestions = suggestions;
                });
            };
            return IndexController;
        })();
        Controllers.IndexController = IndexController;

        angular.module("SGC14").controller("IndexController", [
            "$scope",
            "$http",
            function ($scope, $http) {
                return new IndexController($scope, $http);
            }
        ]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=IndexController.js.map
