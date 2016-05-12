module Sgc14.Directives {
    "use strict";

    export class IsotopeRepeatDirective {
        constructor($isotope: Services.IsotopeService, $imagesLoaded: Services.ImagesLoadedService) {
            return {
                restrict: "A",
                link: (scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes): void => {
                    var ngElement: ng.IAugmentedJQuery = angular.element(element);
                    $isotope.appended(ngElement);

                    if (attrs["data-type"] === "image" ||
                        attrs["data-type"] === "movie") {
                        $imagesLoaded.imagesLoaded(ngElement, (): void => { $isotope.arrange(); });
                    } else {
                        $isotope.arrange();
                    }
                }
            };
        }
    }

    angular.module("SGC14").directive("isotopeRepeat", [
        "Isotope",
        "ImagesLoaded",
        (isotope: Services.IsotopeService, imagesLoaded: Services.ImagesLoadedService): IsotopeRepeatDirective =>
            new IsotopeRepeatDirective(isotope, imagesLoaded)]);
}