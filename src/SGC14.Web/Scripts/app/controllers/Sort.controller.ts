module Sgc14.Controllers {
    "use strict";

    export class SortController implements IController {
        private _sortBy: string = null;
        private _options: Array<Models.KeyValuePair<string, string>> = [];

        constructor(public $scope: IControllerScope, private $isotope: Services.IsotopeService, private $data: Services.DataService) {
            this.sortBy = "original-order";
            this.$scope.vm = this;
        }

        public isSortingEnabled(): boolean {
            return this.$data.state === Models.State.Complete;
        }

        public get sortBy(): string {
            return this._sortBy;
        }

        public set sortBy(value: string) {
            if (this._sortBy === value) {
                return;
            }

            this._sortBy = value;
            this.$isotope.arrange({
                sortBy: this.sortBy
            });
        }

        public get options(): Array<Models.KeyValuePair<string, string>> {
            if (this._options.length === 0) {
                this._options = [
                    new Models.KeyValuePair("Default", "original-order"),
                    new Models.KeyValuePair("Random", "random"),
                    new Models.KeyValuePair("Color", "type"),
                    new Models.KeyValuePair("Date", "created")
                ];
            }

            return this._options;
        }
    }

    angular.module("SGC14").controller("SortController", [
        "$scope",
        "Isotope",
        "Data",
        ($scope: IControllerScope, $isotope: Services.IsotopeService, $data: Services.DataService): SortController =>
            new SortController($scope, $isotope, $data)
    ]);
}