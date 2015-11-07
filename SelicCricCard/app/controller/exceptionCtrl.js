angular.module("MyApp")
       .controller("exceptionCtrl", ['$scope', 'MatchService', '$translate', function ($scope, matchService, $translate) {

           $scope.changeLang = function (langKey) {
               $translate.use(langKey);
           };
       }]);