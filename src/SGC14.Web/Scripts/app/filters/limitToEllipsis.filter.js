var Sgc14;
(function (Sgc14) {
    (function (Filters) {
        "use strict";

        var LimitToEllipsisFilter = (function () {
            function LimitToEllipsisFilter() {
            }
            LimitToEllipsisFilter.limitToEllipsis = function () {
                return function (input, limit) {
                    input = input || "";
                    limit = limit || input.length;

                    if (input.length > limit) {
                        return input.substring(0, limit) + "...";
                    } else {
                        return input;
                    }
                };
            };
            return LimitToEllipsisFilter;
        })();

        angular.module("SGC14").filter("limitToEllipsis", LimitToEllipsisFilter.limitToEllipsis);
    })(Sgc14.Filters || (Sgc14.Filters = {}));
    var Filters = Sgc14.Filters;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=LimitToEllipsis.filter.js.map
