angular.module("MyApp")
       .controller("reviewCtrl", ['$scope', '$state', '$stateParams', 'MatchService', 'OverService', '$translate', function ($scope, $state, $stateParams, matchService, overService, $translate) {

            var overDetailViewModel = {};
            overDetailViewModel.MatchID = $stateParams.matchId;
            overDetailViewModel.TeamID = $stateParams.teamId;
            overDetailViewModel.OverNumber = $stateParams.overnumber;
            overDetailViewModel.BallNumber = $stateParams.ballnumber;
           $scope.Match = {};
           $scope.Overs = [];
           $scope.OverDetails = [];

           var _loadOverDetails = function () {
               overService.LoadOverDetailsByBallNumber(overDetailViewModel).then(function (overDetails) {
                   $scope.OverDetails = overDetails;
                   
                   var overs = _.groupBy($scope.OverDetails, function (item) { return item.OverNumber });
                   var sortedKeys = Object.keys(overs).sort(function (a, b) {
                       console.log(b - a);
                       return b - a;
                   });

                   sortedKeys.forEach(function (key) {
                       var obj = {};
                       obj.OverNumber = key;
                       obj.Details = overs[key];
                       $scope.Overs.push(obj);
                       // $scope.Overs[key] = overs[key];
                   });
                   
               });
           };


           (function GetMatch() {
               matchService.GetForReview(overDetailViewModel).then(function (data) {
                   if (data) {
                       $scope.Match = data;
                       _loadOverDetails();
                   }
               });
           })();

           $scope.LoadMore = function () {
               overService.LoadOverDetailsByBallNumber($scope.OverDetails[$scope.OverDetails.length - 1]).then(function (overDetails) {
                   _.each(overDetails, function (item) {
                       $scope.OverDetails.push(item);
                   });
                   var overs = _.groupBy($scope.OverDetails, function (item) { return item.OverNumber });
                   var sortedKeys = Object.keys(overs).sort(function (a, b) {
                       console.log(b - a);
                       return b - a;
                   });

                   sortedKeys.forEach(function (key) {
                       var obj = {};
                       obj.OverNumber = key;
                       obj.Details = overs[key];
                       $scope.Overs.push(obj);
                       // $scope.Overs[key] = overs[key];
                   });
               });
           }

           $scope.changeLang = function (langKey) {
               $translate.use(langKey);
           };
       }]);