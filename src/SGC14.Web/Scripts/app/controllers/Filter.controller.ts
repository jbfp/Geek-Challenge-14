module Sgc14.Controllers {
    "use strict";

    export class FilterController implements IController {
        private _filters: Array<Models.Filter> = [];

        constructor(public $scope: IControllerScope, private $isotope: Services.IsotopeService, private $data: Services.DataService) {
            this.$scope.vm = this;
            this.filters.forEach((filter: Models.Filter): void => { filter.selected = true; });
            this.toggleFilter();
        }

        public isFiltersEnabled(): boolean {
            return this.$data.state === Models.State.Complete;
        }

        public get filters(): Array<Models.Filter> {
            if (this._filters.length === 0) {
                this._filters = [
                    new Models.Filter("Tweets", "tweet", "twtr"),
                    new Models.Filter("Articles", "article", "newspaper-o"),
                    new Models.Filter("Images", "image", "image"),
                    new Models.Filter("Movies", "movie", "video-camera"),
                    new Models.Filter("Wikipedia", "wiki", "wordpress"),
                    new Models.Filter("Books", "book", "book")
                ];
            }

            return this._filters;
        }

        public count(type: string): number {
            return this.$data.items.filter((item: Models.ISGC14Item): boolean => item.type === type).length;
        }

        public toggleFilter(): void {
            // generate selector that selects for data-type attributes.
            var reduced: string = this.filters
                .filter((item: Models.Filter): boolean => item.selected)
                .map((item: Models.Filter): string => "[data-type='" + item.value + "']")
                .join(", ");

            if (reduced.length === 0) {
                // when reduced === "", isotope will show everything.
                // we'd rather it didn't show aynthing, so...
                this.$isotope.arrange({
                    // ... we assign it a function that returns false for every element.
                    filter: (): boolean => false
                });
            } else {
                this.$isotope.arrange({
                    filter: reduced
                });
            }
        }
    }

    angular.module("SGC14").controller("FilterController", [
        "$scope",
        "Isotope",
        "Data",
        ($scope: IControllerScope, $isotope: Services.IsotopeService, $data: Services.DataService): FilterController =>
            new FilterController($scope, $isotope, $data)
    ]);
} 