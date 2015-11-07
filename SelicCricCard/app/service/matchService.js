angular.module('MyApp')
       .factory('MatchService', ['$q', '$http', function ($q, $http) {
           var baseUrl = 'http://localhost:49786/api/Match/';

           var matchService = {};

           // Search Customers
           matchService.Save = function (match) {
               var deferred = $q.defer();
                $http.post(baseUrl, match)
                   .success(function(data) {
                       deferred.resolve(data);
                   }).error(function(error) {
                       deferred.reject(error);
                   });
               return deferred.promise;
           }
           matchService.Get = function (id) {
               var deferred = $q.defer();
                $http.get(baseUrl + id)
                   .success(function (data) {

                       deferred.resolve(data);
                   }).error(function(error) {
                       deferred.reject(error);
                   });
               return deferred.promise;
           }
           matchService.GetAll = function () {
               var deferred = $q.defer();
               $http.get(baseUrl)
                  .success(function (data) {

                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
               return deferred.promise;
           }

           matchService.GetForReview = function (detailViewModel) {
               var deferred = $q.defer();
               $http.get(baseUrl + 'GetForReview', { params: detailViewModel })
                  .success(function (data) {

                      deferred.resolve(data);
                  }).error(function (error) {
                      deferred.reject(error);
                  });
               return deferred.promise;
           }

           return matchService;
       }]);