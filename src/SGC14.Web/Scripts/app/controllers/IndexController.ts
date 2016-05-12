module Sgc14.Controllers {
    "use strict";

    export class IndexController implements IController {
        private _suggestions: Array<string> = [];

        constructor(public $scope: IControllerScope, private $http: ng.IHttpService) {
            this.$scope.vm = this;
            this.getSuggestions();
        }

        public get suggestions(): Array<string> {
            return this._suggestions;
        }

        public get showSuggestions(): boolean {
            return this.suggestions.length > 0;
        }

        private getSuggestions(): void {
            this.$http.get("/suggestions").success((suggestions: Array<string>): void => {
                this._suggestions = suggestions;
            });
        }
    }

    angular.module("SGC14").controller("IndexController", [
        "$scope",
        "$http",
        ($scope: IControllerScope, $http: ng.IHttpService): IndexController =>
            new IndexController($scope, $http)
    ]);
}