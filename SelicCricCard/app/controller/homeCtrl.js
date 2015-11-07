


angular.module("MyApp")
       .controller("homeCtrl", ['$scope', 'MatchService', '$state', '$translate', function ($scope, matchService, $state, $translate) {
           $scope.Match = {};
           $scope.Match.IsTeam2Bowl = true;

           $scope.SaveGame = function () {

               matchService.Save($scope.Match).then(function(data) {
                   debugger;
                   $state.go('start', { 'matchId': data });

               }, function(error) {

                   console.log(error);

               });
           };
           $scope.CheckTeam1 = function () {
               $scope.Match.IsTeam1Bowl = true;
               $scope.Match.IsTeam2Bowl = false;
           }

           $scope.CheckTeam2 = function () {
              
               $scope.Match.IsTeam2Bowl = true;
               $scope.Match.IsTeam1Bowl = false;
           }
           $scope.changeLang = function (langKey) {
               $translate.use(langKey);
           };
       }]);


