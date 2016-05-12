var Sgc14;
(function (Sgc14) {
    (function (Models) {
        "use strict";

        (function (State) {
            State[State["Disconnected"] = 0] = "Disconnected";
            State[State["Connected"] = 1] = "Connected";
            State[State["Searching"] = 2] = "Searching";
            State[State["Streaming"] = 3] = "Streaming";
            State[State["Complete"] = 4] = "Complete";
        })(Models.State || (Models.State = {}));
        var State = Models.State;
    })(Sgc14.Models || (Sgc14.Models = {}));
    var Models = Sgc14.Models;
})(Sgc14 || (Sgc14 = {}));
//# sourceMappingURL=ConnectionState.js.map
