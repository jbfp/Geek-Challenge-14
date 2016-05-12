var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var TemporalController = (function () {
            function TemporalController($scope, $data) {
                this.$scope = $scope;
                this.$data = $data;
                this.$scope.vm = this;
            }
            TemporalController.prototype.isTemporalVisible = function () {
                return this.$data.items.length > 0;
            };

            Object.defineProperty(TemporalController.prototype, "items", {
                get: function () {
                    return this.$data.items;
                },
                enumerable: true,
                configurable: true
            });
            return TemporalController;
        })();
        Controllers.TemporalController = TemporalController;

        angular.module("SGC14").controller("TemporalController", [
            "$scope",
            "Data",
            function ($scope, $data) {
                return new TemporalController($scope, $data);
            }
        ]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Temporal.controller.js.map
