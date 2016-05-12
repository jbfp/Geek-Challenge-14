module Sgc14.Controllers {
    "use strict";

    export class SentimentController implements IController {
        constructor(public $scope: IControllerScope, private $data: Services.DataService) {
            this.$scope.vm = this;
        }

        public isSentimentVisible(): boolean {
            return this.$data.items.filter((item: Models.ISGC14Item): boolean => item.type === "tweet")
                .map((item: Models.ISentimentTweet): Models.ISentiment => item.sentiment)
                .filter((sentiment: Models.ISentiment): boolean => sentiment.score !== 0.0)
                .length > 0;
        }

        public get items() {
            var tweets: Array<Models.ISGC14Item> = this.$data.items.filter(
                (item: Models.ISGC14Item): boolean => item.type === "tweet");
            return tweets.map((tweet: Models.ISentimentTweet): Models.ISentiment => tweet.sentiment);
        }
    }

    angular.module("SGC14").controller("SentimentController", [
        "$scope",
        "Data",
        ($scope: IControllerScope, $data: Services.DataService): SentimentController =>
            new SentimentController($scope, $data)
    ]);
} 