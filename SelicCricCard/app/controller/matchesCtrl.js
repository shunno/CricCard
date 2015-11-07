angular.module("MyApp")
       .controller("matchesCtrl", ['$scope', 'MatchService', '$translate', function ($scope, matchService, $translate) {
           
           $scope.Matches = [];
           (function () {
               matchService.GetAll().then(function (data) {
                   if (data) {
                       $scope.Matches = data;
                   }
               });
           })();

           $scope.changeLang = function (langKey) {
               $translate.use(langKey);
           };
       }]);