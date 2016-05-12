var Sgc14;
(function (Sgc14) {
    (function (Filters) {
        "use strict";

        var EncodeURICompoenentFilter = (function () {
            function EncodeURICompoenentFilter() {
            }
            EncodeURICompoenentFilter.encodeURIComponent = function () {
                return function (input) {
                    return encodeURIComponent(input);
                };
            };
            return EncodeURICompoenentFilter;
        })();

        angular.module("SGC14").filter("encodeURIComponent", EncodeURICompoenentFilter.encodeURIComponent);
    })(Sgc14.Filters || (Sgc14.Filters = {}));
    var Filters = Sgc14.Filters;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=EncodeURIComponentFilter.js.map
