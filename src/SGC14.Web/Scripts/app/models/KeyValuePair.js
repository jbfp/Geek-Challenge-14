var Sgc14;
(function (Sgc14) {
    (function (Models) {
        "use strict";

        var KeyValuePair = (function () {
            function KeyValuePair(_key, _value) {
                this._key = _key;
                this._value = _value;
            }
            Object.defineProperty(KeyValuePair.prototype, "key", {
                get: function () {
                    return this._key;
                },
                enumerable: true,
                configurable: true
            });

            Object.defineProperty(KeyValuePair.prototype, "value", {
                get: function () {
                    return this._value;
                },
                enumerable: true,
                configurable: true
            });
            return KeyValuePair;
        })();
        Models.KeyValuePair = KeyValuePair;
    })(Sgc14.Models || (Sgc14.Models = {}));
    var Models = Sgc14.Models;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=KeyValuePair.js.map
