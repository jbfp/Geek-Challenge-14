module Sgc14.Models {
    "use strict";

    export class KeyValuePair<TKey, TValue> {
        constructor(private _key: TKey, private _value: TValue) { }

        public get key(): TKey {
            return this._key;
        }

        public get value(): TValue {
            return this._value;
        }
    }
} 