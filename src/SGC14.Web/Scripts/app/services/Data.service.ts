module Sgc14.Services {
    "use strict";

    export class DataService {
        private _state: Models.State = Models.State.Disconnected;
        private _items: Array<Models.ISGC14Item> = [];

        constructor(private $rootScope: ng.IRootScopeService, private $searchHub: SearchHubService, private $streamHub: StreamHubService) {
            this.$searchHub.onSearchNext = (item: Models.ISGC14Item): void => {
                this.onNext(item);
            };

            this.$searchHub.onSearchError = (exception: any): void => {
                this.onComplete();
                console.error(exception);
                alert(exception);
            };

            this.$searchHub.onSearchCompleted = (): void => {
                this.onComplete();
                console.log("Search done!");
            };

            this.$streamHub.onStreamNext = (item: Models.ISGC14Item): void => {
                this.onNext(item);
            };

            this.$streamHub.onStreamError = (error: any): void => {
                if (error.Message.indexOf("(420)") > -1) {
                    alert("Twitter asks that you lay of streaming for a little while. They must be busy... sorry!")
                } else {
                    console.log(error);
                    alert(error);
                }

                this.onComplete();
            };

            this.$streamHub.onStreamCompleted = (): void => {
                this.onComplete();
                console.log("Stream done!");
            };

            $.connection.hub.start()
                .done((): void => { $rootScope.$apply((): void=> { this._state = Models.State.Connected; }); })
                .fail((error: any): void => { console.error(error); });
        }

        public get state() {
            return this._state;
        }

        public get items() {
            return this._items;
        }

        public search(query: string, page: number, language: string): JQueryPromise<any> {
            if (!query || query.length === 0) {
                throw new Error("query is null or empty.");
            }

            return this.$searchHub.search(query, page, language).done((): void => {
                this.$streamHub.query = query;
                this.$rootScope.$apply((): void => {
                    this._state = Models.State.Searching;
                });
            });
        }

        public toggleStream(): JQueryPromise<any> {
            if (this.state === Models.State.Streaming) {
                return this.$streamHub.stopStream()
                    .done((): void => { this.onComplete(); })
                    .fail((error: any): void => { window.alert("Could not stop stream: " + error); });
            } else {
                return this.$streamHub.startStream()
                    .done((): void => { this.safeApply((): void => { this._state = Models.State.Streaming; }); })
                    .fail((error: any): void => { window.alert("Could not start stream: " + error); });
            }
        }

        private safeApply(fn: (scope: ng.IScope) => any): void {
            var phase = this.$rootScope.$$phase;
            if (phase == '$apply' || phase == '$digest') {
                if (fn && (typeof (fn) === 'function')) {
                    fn(this.$rootScope.$parent);
                }
            } else {
                this.$rootScope.$apply(fn);
            }
        }

        private onNext(item: Models.ISGC14Item): void {
            var exists: boolean = false;

            for (var i: number = 0; i < this.items.length; i++) {
                var other: Models.ISGC14Item = this.items[i];

                if (other.id === item.id && other.type === item.type) {
                    exists = true;
                }
            }

            if (exists) {
                console.log("Item exists: " + JSON.stringify(item));
            } else {
                this.$rootScope.$apply((): void => {
                    this.items.push(item);
                });
            }
        }

        private onComplete(): void {
            this.safeApply((): void => {
                this._state = Models.State.Complete;
            });
        }
    }

    angular.module("SGC14").service("Data", [
        "$rootScope",
        "SearchHub",
        "StreamHub",
        ($rootScope: ng.IRootScopeService, $searchHub: SearchHubService, $streamHub: StreamHubService): DataService => new DataService($rootScope, $searchHub, $streamHub)
    ]);
}