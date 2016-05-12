module Sgc14.Services {
    "use strict";

    export class SearchHubService {
        private _searchHub: HubProxy;

        public onSearchNext: (t: Models.ISGC14Item) => void;
        public onSearchError: (exception: any) => void;
        public onSearchCompleted: () => void;

        constructor() {
            this._searchHub = $.connection.hub.createHubProxy("searchHub");

            // add event handlers.
            this._searchHub.on("onSearchNext", (t: Models.ISGC14Item): void => {
                if (this.onSearchNext) {
                    this.onSearchNext(t);
                }
            });

            this._searchHub.on("onSearchError", (exception: any): void => {
                if (this.onSearchError) {
                    this.onSearchError(exception);
                }
            });

            this._searchHub.on("onSearchCompleted", (): void => {
                if (this.onSearchCompleted) {
                    this.onSearchCompleted();
                }
            });
        }

        public search(query: string, page: number, language: string): JQueryPromise<any> {
            return this._searchHub.invoke("search", query, page, language);
        }
    }

    angular.module("SGC14").service("SearchHub", SearchHubService);
} 