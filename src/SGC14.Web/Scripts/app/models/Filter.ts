module Sgc14.Models {
    "use strict";

    export class Filter {
        private _selected: boolean = false;

        constructor(private _key: string, private _value: string, private _icon: string) { }

        public get key(): string {
            return this._key;
        }

        public get value(): string {
            return this._value;
        }

        public get icon(): string {
            return this._icon;
        }

        public get selected(): boolean {
            return this._selected;
        }

        public set selected(value: boolean) {
            if (this._selected === value) {
                return;
            }

            this._selected = value;
        }
    }
} 