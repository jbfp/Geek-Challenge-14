var Sgc14;
(function (Sgc14) {
    (function (Models) {
        "use strict";

        var Filter = (function () {
            function Filter(_key, _value, _icon) {
                this._key = _key;
                this._value = _value;
                this._icon = _icon;
                this._selected = false;
            }
            Object.defineProperty(Filter.prototype, "key", {
                get: function () {
                    return this._key;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(Filter.prototype, "value", {
                get: function () {
                    return this._value;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(Filter.prototype, "icon", {
                get: function () {
                    return this._icon;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(Filter.prototype, "selected", {
                get: function () {
                    return this._selected;
                },
                set: function (value) {
                    if (this._selected === value) {
                        return;
                    }

                    this._selected = value;
                },
                enumerable: true,
                configurable: true
            });

            return Filter;
        })();
        Models.Filter = Filter;
    })(Sgc14.Models || (Sgc14.Models = {}));
    var Models = Sgc14.Models;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=Filter.js.map
