var Sgc14;
(function (Sgc14) {
    (function (Services) {
        "use strict";

        var StreamHubService = (function () {
            function StreamHubService() {
                var _this = this;
                this._streamHub = $.connection.hub.createHubProxy("streamHub");

                // add event handlers.
                this._streamHub.on("onStreamNext", function (t) {
                    if (_this.onStreamNext) {
                        _this.onStreamNext(t);
                    }
                });

                this._streamHub.on("onStreamError", function (exception) {
                    if (_this.onStreamError) {
                        _this.onStreamError(exception);
                    }
                });

                this._streamHub.on("onStreamCompleted", function () {
                    if (_this.onStreamCompleted) {
                        _this.onStreamCompleted();
                    }
                });
            }
            Object.defineProperty(StreamHubService.prototype, "query", {
                get: function () {
                    return this._query;
                },
                set: function (value) {
                    this._query = value;
                },
                enumerable: true,
                configurable: true
            });


            StreamHubService.prototype.startStream = function () {
                return this._streamHub.invoke("startStream", this.query);
            };

            StreamHubService.prototype.stopStream = function () {
                return this._streamHub.invoke("stopStream");
            };
            return StreamHubService;
        })();
        Services.StreamHubService = StreamHubService;

        angular.module("SGC14").service("StreamHub", StreamHubService);
    })(Sgc14.Services || (Sgc14.Services = {}));
    var Services = Sgc14.Services;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=StreamHub.service.js.map
