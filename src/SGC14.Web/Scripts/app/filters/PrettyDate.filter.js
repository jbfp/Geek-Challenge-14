var Sgc14;
(function (Sgc14) {
    (function (Filters) {
        "use strict";

        /*
        * JavaScript Pretty Date
        * Copyright (c) 2011 John Resig (ejohn.org)
        * Licensed under the MIT and GPL licenses.
        */
        // takes an ISO time and returns a string representing how
        // long ago the date represents.
        var PrettyDateFilter = (function () {
            function PrettyDateFilter() {
            }
            PrettyDateFilter.getPrettyDate = function () {
                return function (input) {
                    var local = new Date(input);
                    var date = new Date(Date.UTC(local.getFullYear(), local.getMonth(), local.getDate(), local.getHours(), local.getMinutes()));
                    var diff = (((new Date()).getTime() - date.getTime()) / 1000);
                    var dayDiff = Math.floor(diff / 86400);

                    if (isNaN(dayDiff) || dayDiff < 0 || dayDiff >= 31) {
                        return "";
                    }

                    return dayDiff === 0 && (diff < 60 && "just now" || diff < 120 && "1 minute ago" || diff < 3600 && Math.floor(diff / 60) + " minutes ago" || diff < 7200 && "1 hour ago" || diff < 86400 && Math.floor(diff / 3600) + " hours ago") || dayDiff === 1 && "Yesterday" || dayDiff < 7 && dayDiff + " days ago" || dayDiff < 31 && Math.ceil(dayDiff / 7) + " weeks ago";
                };
            };
            return PrettyDateFilter;
        })();

        angular.module("SGC14").filter("prettyDate", PrettyDateFilter.getPrettyDate);
    })(Sgc14.Filters || (Sgc14.Filters = {}));
    var Filters = Sgc14.Filters;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=PrettyDate.filter.js.map
