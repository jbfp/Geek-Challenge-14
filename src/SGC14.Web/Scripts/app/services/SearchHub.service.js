var Sgc14;
(function (Sgc14) {
    (function (Services) {
        "use strict";

        var SearchHubService = (function () {
            function SearchHubService() {
                var _this = this;
                this._searchHub = $.connection.hub.createHubProxy("searchHub");

                // add event handlers.
                this._searchHub.on("onSearchNext", function (t) {
                    if (_this.onSearchNext) {
                        _this.onSearchNext(t);
                    }
                });

                this._searchHub.on("onSearchError", function (exception) {
                    if (_this.onSearchError) {
                        _this.onSearchError(exception);
                    }
                });

                this._searchHub.on("onSearchCompleted", function () {
                    if (_this.onSearchCompleted) {
                        _this.onSearchCompleted();
                    }
                });
            }
            SearchHubService.prototype.search = function (query, page, language) {
                return this._searchHub.invoke("search", query, page, language);
            };
            return SearchHubService;
        })();
        Services.SearchHubService = SearchHubService;

        angular.module("SGC14").service("SearchHub", SearchHubService);
    })(Sgc14.Services || (Sgc14.Services = {}));
    var Services = Sgc14.Services;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=SearchHub.service.js.map
