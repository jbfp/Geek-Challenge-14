module Sgc14.Filters {
    "use strict";

    class LimitToEllipsisFilter {
        public static limitToEllipsis(): Function {
            return (input: string, limit: number): string => {
                input = input || "";
                limit = limit || input.length;

                if (input.length > limit) {
                    return input.substring(0, limit) + "...";
                } else {
                    return input;
                }
            };
        }
    }

    angular.module("SGC14").filter("limitToEllipsis", LimitToEllipsisFilter.limitToEllipsis);
}