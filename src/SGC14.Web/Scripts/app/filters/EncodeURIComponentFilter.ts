module Sgc14.Filters {
    "use strict";

    class EncodeURICompoenentFilter {
        public static encodeURIComponent(): Function {
            return (input: string): string => {
                return encodeURIComponent(input);
            };
        }
    }

    angular.module("SGC14").filter("encodeURIComponent", EncodeURICompoenentFilter.encodeURIComponent);
}