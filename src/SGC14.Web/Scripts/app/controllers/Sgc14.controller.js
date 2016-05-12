var Sgc14;
(function (Sgc14) {
    (function (Controllers) {
        "use strict";

        var Sgc14Controller = (function () {
            function Sgc14Controller($scope, $isotope, $data) {
                var _this = this;
                this.$scope = $scope;
                this.$isotope = $isotope;
                this.$data = $data;
                this.$scope.vm = this;

                $scope.$watch("vm.$data.state", function (newValue) {
                    switch (newValue) {
                        case 1 /* Connected */:
                            // start searching.
                            var query = _this.getQueryStringValue("query");
                            var page = _this.getQueryStringValue("page") || 1;
                            var language = _this.getQueryStringValue("language") || "";
                            _this.$data.search(query, page, language);
                            break;

                        case 4 /* Complete */:
                            _this.onComplete();
                            break;
                    }
                }, false);
            }
            Sgc14Controller.prototype.getQueryStringValue = function (key) {
                return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
            };

            Object.defineProperty(Sgc14Controller.prototype, "items", {
                get: function () {
                    return this.$data.items;
                },
                enumerable: true,
                configurable: true
            });

            Sgc14Controller.prototype.toggleStream = function () {
                this.$data.toggleStream();
            };

            Sgc14Controller.prototype.onComplete = function () {
                this.$isotope.updateSortData($(".isotope-container .item"));
                this.$isotope.arrange();
            };

            // helper functions.
            Sgc14Controller.prototype.getStateMessage = function () {
                var message = "";

                switch (this.$data.state) {
                    case 0 /* Disconnected */:
                        message = "Connecting...";
                        break;
                    case 1 /* Connected */:
                        message = "Ready!";
                        break;
                    case 2 /* Searching */:
                        message = "Searching...";
                        break;
                    case 3 /* Streaming */:
                        message = "Streaming...";
                        break;
                }

                return message;
            };

            Sgc14Controller.prototype.getIsStreamingEnabled = function () {
                return this.$data.state === 4 /* Complete */ || this.$data.state === 3 /* Streaming */;
            };

            Sgc14Controller.prototype.getIsStreaming = function () {
                return this.$data.state === 3 /* Streaming */;
            };

            Sgc14Controller.prototype.getIsSearching = function () {
                return this.$data.state === 2 /* Searching */;
            };

            Sgc14Controller.prototype.range = function (n) {
                return new Array(n);
            };
            return Sgc14Controller;
        })();
        Controllers.Sgc14Controller = Sgc14Controller;

        angular.module("SGC14").controller("SGC14Controller", [
            "$scope",
            "Isotope",
            "Data",
            function ($scope, $isotope, $data) {
                return new Sgc14Controller($scope, $isotope, $data);
            }]);
    })(Sgc14.Controllers || (Sgc14.Controllers = {}));
    var Controllers = Sgc14.Controllers;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Sgc14.controller.js.map
