var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var FilterController = (function () {
            function FilterController($scope, $isotope, $data) {
                this.$scope = $scope;
                this.$isotope = $isotope;
                this.$data = $data;
                this._filters = [];
                this.$scope.vm = this;
                this.filters.forEach(function (filter) {
                    filter.selected = true;
                });
                this.toggleFilter();
            }
            FilterController.prototype.isFiltersEnabled = function () {
                return this.$data.state === 4 /* Complete */;
            };

            Object.defineProperty(FilterController.prototype, "filters", {
                get: function () {
                    if (this._filters.length === 0) {
                        this._filters = [
                            new Sgc14.Models.Filter("Tweets", "tweet", "twtr"),
                            new Sgc14.Models.Filter("Articles", "article", "newspaper-o"),
                            new Sgc14.Models.Filter("Images", "image", "image"),
                            new Sgc14.Models.Filter("Movies", "movie", "video-camera"),
                            new Sgc14.Models.Filter("Wikipedia", "wiki", "wordpress"),
                            new Sgc14.Models.Filter("Books", "book", "book")
                        ];
                    }

                    return this._filters;
                },
                enumerable: true,
                configurable: true
            });

            FilterController.prototype.count = function (type) {
                return this.$data.items.filter(function (item) {
                    return item.type === type;
                }).length;
            };

            FilterController.prototype.toggleFilter = function () {
                // generate selector that selects for data-type attributes.
                var reduced = this.filters.filter(function (item) {
                    return item.selected;
                }).map(function (item) {
                    return "[data-type='" + item.value + "']";
                }).join(", ");

                if (reduced.length === 0) {
                    // when reduced === "", isotope will show everything.
                    // we'd rather it didn't show aynthing, so...
                    this.$isotope.arrange({
                        // ... we assign it a function that returns false for every element.
                        filter: function () {
                            return false;
                        }
                    });
                } else {
                    this.$isotope.arrange({
                        filter: reduced
                    });
                }
            };
            return FilterController;
        })();
        Controllers.FilterController = FilterController;

        angular.module("SGC14").controller("FilterController", [
            "$scope",
            "Isotope",
            "Data",
            function ($scope, $isotope, $data) {
                return new FilterController($scope, $isotope, $data);
            }
        ]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Filter.controller.js.map
