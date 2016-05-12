module Sgc14.Controllers {
    "use strict";

    export class Sgc14Controller implements IController {
        constructor(
            public $scope: IControllerScope,
            private $isotope: Services.IsotopeService,
            private $data: Services.DataService) {
            this.$scope.vm = this;

            $scope.$watch("vm.$data.state", (newValue: Models.State): void => {
                switch (newValue) {
                    case Models.State.Connected:
                        // start searching.
                        var query: string = this.getQueryStringValue("query");
                        var page: number = this.getQueryStringValue("page") || 1;
                        var language: string = this.getQueryStringValue("language") || "";
                        this.$data.search(query, page, language);
                        break;

                    case Models.State.Complete:
                        this.onComplete();
                        break;
                }
            }, false);
        }

        private getQueryStringValue(key: string): any {
            return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
        }

        public get items(): Array<Models.ISGC14Item> {
            return this.$data.items;
        }

        public toggleStream(): void {
            this.$data.toggleStream();
        }

        private onComplete(): void {
            this.$isotope.updateSortData($(".isotope-container .item"));
            this.$isotope.arrange();
        }

        // helper functions.
        public getStateMessage(): string {
            var message: string = "";

            switch (this.$data.state) {
                case Models.State.Disconnected:
                    message = "Connecting...";
                    break;
                case Models.State.Connected:
                    message = "Ready!";
                    break;
                case Models.State.Searching:
                    message = "Searching...";
                    break;
                case Models.State.Streaming:
                    message = "Streaming...";
                    break;
            }

            return message;
        }

        public getIsStreamingEnabled(): boolean {
            return this.$data.state === Models.State.Complete || this.$data.state === Models.State.Streaming;
        }

        public getIsStreaming(): boolean {
            return this.$data.state === Models.State.Streaming;
        }

        public getIsSearching(): boolean {
            return this.$data.state === Models.State.Searching;
        }

        public range(n: number): any[] {
            return new Array(n);
        }
    }

    angular.module("SGC14").controller("SGC14Controller", [
        "$scope",
        "Isotope",
        "Data",
        ($scope: IControllerScope, $isotope: Services.IsotopeService, $data: Services.DataService): Sgc14Controller =>
            new Sgc14Controller($scope, $isotope, $data)]);
}