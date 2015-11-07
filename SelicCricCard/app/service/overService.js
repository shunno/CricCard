angular.module('MyApp')
       .factory('OverService', ['$q', '$http', function ($q, $http) {
           var baseUrl = 'http://localhost:49786/api/OverDetail/';

           var overService = {};

           // Search Customers
           overService.Save = function (matchId, teamId) {
               var deferred = $q.defer();
               $http.post(baseUrl + 'Post', { matchId: matchId, teamId: teamId })
                   .success(function (data) {
                       deferred.resolve(data);
                   }).error(function (error) {
                       deferred.reject(error);
                   });
               return deferred.promise;
           }

           overService.LoadOverDetails = function (matchId, teamId, lastBallNumber) {
               var deferred = $q.defer();
               $http.get(baseUrl + 'GetByMatchIdAndTeamId', { params: { matchId: matchId, teamId: teamId, lastBallNumber: lastBallNumber } })
                .success(function (data) {
                    deferred.resolve(data);
                }).error(function (error) {
                    deferred.reject(error);
                });
               return deferred.promise;
           }

           overService.LoadOverDetailsByBallNumber = function (overDetailViewModel) {
               var deferred = $q.defer();
               $http.get(baseUrl + 'GetByLastBall', { params: overDetailViewModel })
                .success(function (data) {
                    deferred.resolve(data);
                }).error(function (error) {
                    deferred.reject(error);
                });
               return deferred.promise;
           }

           return overService;
       }]);