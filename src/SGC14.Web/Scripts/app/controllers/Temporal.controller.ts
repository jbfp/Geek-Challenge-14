module Sgc14.Controllers {
    "use strict";

    export class TemporalController implements IController {
        constructor(public $scope: IControllerScope, private $data: Services.DataService) {
            this.$scope.vm = this;
        }

        public isTemporalVisible(): boolean {
            return this.$data.items.length > 0;
        }

        public get items() {
            return this.$data.items;
        }
    }

    angular.module("SGC14").controller("TemporalController", [
        "$scope",
        "Data",
        ($scope: IControllerScope, $data: Services.DataService): TemporalController
            => new TemporalController($scope, $data)
    ]);
} 