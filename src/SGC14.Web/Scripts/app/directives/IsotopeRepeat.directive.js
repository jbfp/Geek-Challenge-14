var Sgc14;
(function (Sgc14) {
    (function (Directives) {
        "use strict";

        var IsotopeRepeatDirective = (function () {
            function IsotopeRepeatDirective($isotope, $imagesLoaded) {
                return {
                    restrict: "A",
                    link: function (scope, element, attrs) {
                        var ngElement = angular.element(element);
                        $isotope.appended(ngElement);

                        if (attrs["data-type"] === "image" || attrs["data-type"] === "movie") {
                            $imagesLoaded.imagesLoaded(ngElement, function () {
                                $isotope.arrange();
                            });
                        } else {
                            $isotope.arrange();
                        }
                    }
                };
            }
            return IsotopeRepeatDirective;
        })();
        Directives.IsotopeRepeatDirective = IsotopeRepeatDirective;

        angular.module("SGC14").directive("isotopeRepeat", [
            "Isotope",
            "ImagesLoaded",
            function (isotope, imagesLoaded) {
                return new IsotopeRepeatDirective(isotope, imagesLoaded);
            }]);
    })(Sgc14.Directives || (Sgc14.Directives = {}));
    var Directives = Sgc14.Directives;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=IsotopeRepeat.directive.js.map
