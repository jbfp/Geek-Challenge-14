declare var Isotope: any;

module Sgc14.Services {
    "use strict";

    export class IsotopeService {
        private _isotope: any;

        constructor() {
            var container: NodeSelector = document.querySelector(".isotope-container");

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

        public appended(selector: ng.IAugmentedJQuery): void {
            this._isotope.appended(selector);
        }

        public arrange(options: Object = {}): void {
            this._isotope.arrange(options);
        }

        public updateSortData(selector: any): void {
            this._isotope.updateSortData(selector);
        }
    }

    angular.module("SGC14").service("Isotope", IsotopeService);
}