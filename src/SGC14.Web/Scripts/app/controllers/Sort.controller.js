var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var SortController = (function () {
            function SortController($scope, $isotope, $data) {
                this.$scope = $scope;
                this.$isotope = $isotope;
                this.$data = $data;
                this._sortBy = null;
                this._options = [];
                this.sortBy = "original-order";
                this.$scope.vm = this;
            }
            SortController.prototype.isSortingEnabled = function () {
                return this.$data.state === 4 /* Complete */;
            };

            Object.defineProperty(SortController.prototype, "sortBy", {
                get: function () {
                    return this._sortBy;
                },
                set: function (value) {
                    if (this._sortBy === value) {
                        return;
                    }

                    this._sortBy = value;
                    this.$isotope.arrange({
                        sortBy: this.sortBy
                    });
                },
                enumerable: true,
                configurable: true
            });


            Object.defineProperty(SortController.prototype, "options", {
                get: function () {
                    if (this._options.length === 0) {
                        this._options = [
                            new Sgc14.Models.KeyValuePair("Default", "original-order"),
                            new Sgc14.Models.KeyValuePair("Random", "random"),
                            new Sgc14.Models.KeyValuePair("Color", "type"),
                            new Sgc14.Models.KeyValuePair("Date", "created")
                        ];
                    }

                    return this._options;
                },
                enumerable: true,
                configurable: true
            });
            return SortController;
        })();
        Controllers.SortController = SortController;

        angular.module("SGC14").controller("SortController", [
            "$scope",
            "Isotope",
            "Data",
            function ($scope, $isotope, $data) {
                return new SortController($scope, $isotope, $data);
            }
        ]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Sort.controller.js.map
