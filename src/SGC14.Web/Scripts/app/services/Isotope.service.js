var Sgc14;
(function (Sgc14) {
    (function (Services) {
        "use strict";

        var IsotopeService = (function () {
            function IsotopeService() {
                var container = document.querySelector(".isotope-container");

                this._isotope = new Isotope(container, {
                    columnWidth: ".item",
                    itemSelector: ".item",
                    getSortData: {
                        type: "[data-type]",
                        created: "[data-created]"
                    },
                    layoutMode: "masonry",
                    masonry: {
                        columnWidth: ".item"
                    }
                });
            }
            IsotopeService.prototype.appended = function (selector) {
                this._isotope.appended(selector);
            };

            IsotopeService.prototype.arrange = function (options) {
                if (typeof options === "undefined") { options = {}; }
                this._isotope.arrange(options);
            };

            IsotopeService.prototype.updateSortData = function (selector) {
                this._isotope.updateSortData(selector);
            };
            return IsotopeService;
        })();
        Services.IsotopeService = IsotopeService;

        angular.module("SGC14").service("Isotope", IsotopeService);
    })(Sgc14.Services || (Sgc14.Services = {}));
    var Services = Sgc14.Services;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Isotope.service.js.map
