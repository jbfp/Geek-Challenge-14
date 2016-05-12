var Sgc14;
(function (Sgc14) {
    (function (Filters) {
        "use strict";

        var EpochFilter = (function () {
            function EpochFilter() {
            }
            EpochFilter.getEpoch = function () {
                return function (input) {
                    return new Date(input).getTime() / 1000.0;
                };
            };
            return EpochFilter;
        })();
        Filters.EpochFilter = EpochFilter;

        angular.module("SGC14").filter("epoch", EpochFilter.getEpoch);
    })(Sgc14.Filters || (Sgc14.Filters = {}));
    var Filters = Sgc14.Filters;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Epoch.filter.js.map
