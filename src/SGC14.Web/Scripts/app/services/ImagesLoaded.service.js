var Sgc14;
(function (Sgc14) {
    (function (Services) {
        "use strict";

        var ImagesLoadedService = (function () {
            function ImagesLoadedService() {
                this._imagesLoaded = imagesLoaded;
            }
            Object.defineProperty(ImagesLoadedService.prototype, "imagesLoaded", {
                get: function () {
                    return this._imagesLoaded;
                },
                enumerable: true,
                configurable: true
            });
            return ImagesLoadedService;
        })();
        Services.ImagesLoadedService = ImagesLoadedService;

        angular.module("SGC14").service("ImagesLoaded", ImagesLoadedService);
    })(Sgc14.Services || (Sgc14.Services = {}));
    var Services = Sgc14.Services;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=ImagesLoaded.service.js.map
