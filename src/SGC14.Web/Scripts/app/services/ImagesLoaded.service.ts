module Sgc14.Services {
    "use strict";

    declare var imagesLoaded: Function;

    export class ImagesLoadedService {
        private _imagesLoaded: Function;

        constructor() {
            this._imagesLoaded = imagesLoaded;
        }

        public get imagesLoaded() {
            return this._imagesLoaded;
        }
    }

    angular.module("SGC14").service("ImagesLoaded", ImagesLoadedService);
} 