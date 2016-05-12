module Sgc14.Filters {
    "use strict";

    export class EpochFilter {
        public static getEpoch(): Function {
            return (input: string): number => {
                return new Date(input).getTime() / 1000.0;
            };
        }
    }

    angular.module("SGC14").filter("epoch", EpochFilter.getEpoch);
} 