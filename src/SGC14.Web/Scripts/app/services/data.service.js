var Sgc14;
(function (Sgc14) {
    (function (Services) {
        "use strict";

        var DataService = (function () {
            function DataService($rootScope, $searchHub, $streamHub) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$searchHub = $searchHub;
                this.$streamHub = $streamHub;
                this._state = 0 /* Disconnected */;
                this._items = [];
                this.$searchHub.onSearchNext = function (item) {
                    _this.onNext(item);
                };

                this.$searchHub.onSearchError = function (exception) {
                    _this.onComplete();
                    console.error(exception);
                    alert(exception);
                };

                this.$searchHub.onSearchCompleted = function () {
                    _this.onComplete();
                    console.log("Search done!");
                };

                this.$streamHub.onStreamNext = function (item) {
                    _this.onNext(item);
                };

                this.$streamHub.onStreamError = function (error) {
                    if (error.Message.indexOf("(420)") > -1) {
                        alert("Twitter asks that you lay of streaming for a little while. They must be busy... sorry!");
                    } else {
                        console.log(error);
                        alert(error);
                    }

                    _this.onComplete();
                };

                this.$streamHub.onStreamCompleted = function () {
                    _this.onComplete();
                    console.log("Stream done!");
                };

                $.connection.hub.start().done(function () {
                    $rootScope.$apply(function () {
                        _this._state = 1 /* Connected */;
                    });
                }).fail(function (error) {
                    console.error(error);
                });
            }
            Object.defineProperty(DataService.prototype, "state", {
                get: function () {
                    return this._state;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(DataService.prototype, "items", {
                get: function () {
                    return this._items;
                },
                enumerable: true,
                configurable: true
            });

            DataService.prototype.search = function (query, page, language) {
                var _this = this;
                if (!query || query.length === 0) {
                    throw new Error("query is null or empty.");
                }

                return this.$searchHub.search(query, page, language).done(function () {
                    _this.$streamHub.query = query;
                    _this.$rootScope.$apply(function () {
                        _this._state = 2 /* Searching */;
                    });
                });
            };

            DataService.prototype.toggleStream = function () {
                var _this = this;
                if (this.state === 3 /* Streaming */) {
                    return this.$streamHub.stopStream().done(function () {
                        _this.onComplete();
                    }).fail(function (error) {
                        window.alert("Could not stop stream: " + error);
                    });
                } else {
                    return this.$streamHub.startStream().done(function () {
                        _this.safeApply(function () {
                            _this._state = 3 /* Streaming */;
                        });
                    }).fail(function (error) {
                        window.alert("Could not start stream: " + error);
                    });
                }
            };

            DataService.prototype.safeApply = function (fn) {
                var phase = this.$rootScope.$$phase;
                if (phase == '$apply' || phase == '$digest') {
                    if (fn && (typeof (fn) === 'function')) {
                        fn(this.$rootScope.$parent);
                    }
                } else {
                    this.$rootScope.$apply(fn);
                }
            };

            DataService.prototype.onNext = function (item) {
                var _this = this;
                var exists = false;

                for (var i = 0; i < this.items.length; i++) {
                    var other = this.items[i];

                    if (other.id === item.id && other.type === item.type) {
                        exists = true;
                    }
                }

                if (exists) {
                    console.log("Item exists: " + JSON.stringify(item));
                } else {
                    this.$rootScope.$apply(function () {
                        _this.items.push(item);
                    });
                }
            };

            DataService.prototype.onComplete = function () {
                var _this = this;
                this.safeApply(function () {
                    _this._state = 4 /* Complete */;
                });
            };
            return DataService;
        })();
        Services.DataService = DataService;

        angular.module("SGC14").service("Data", [
            "$rootScope",
            "SearchHub",
            "StreamHub",
            function ($rootScope, $searchHub, $streamHub) {
                return new DataService($rootScope, $searchHub, $streamHub);
            }
        ]);
    })(Sgc14.Services || (Sgc14.Services = {}));
    var Services = Sgc14.Services;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Data.service.js.map
