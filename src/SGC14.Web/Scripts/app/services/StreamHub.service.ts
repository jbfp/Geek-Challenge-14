module Sgc14.Services {
    "use strict";

    export class StreamHubService {
        private _streamHub: HubProxy;
        private _query: string;
        private _isStreaming: boolean;

        public onStreamNext: (t: Models.ISGC14Item) => void;
        public onStreamError: (exception: any) => void;
        public onStreamCompleted: () => void;

        constructor() {
            this._streamHub = $.connection.hub.createHubProxy("streamHub");

            // add event handlers.
            this._streamHub.on("onStreamNext", (t: Models.ISGC14Item): void => {
                if (this.onStreamNext) {
                    this.onStreamNext(t);
                }
            });

            this._streamHub.on("onStreamError", (exception: any): void => {
                if (this.onStreamError) {
                    this.onStreamError(exception);
                }
            });

            this._streamHub.on("onStreamCompleted", (): void => {
                if (this.onStreamCompleted) {
                    this.onStreamCompleted();
                }
            });
        }

        public get query() {
            return this._query;
        }

        public set query(value: string) {
            this._query = value;
        }

        public startStream(): JQueryPromise<any> {
            return this._streamHub.invoke("startStream", this.query);
        }

        public stopStream(): JQueryPromise<any> {
            return this._streamHub.invoke("stopStream");
        }
    }

    angular.module("SGC14").service("StreamHub", StreamHubService);
} 