angular.module("MyApp")
       .controller("startCtrl", ['$scope', '$state', '$stateParams', 'MatchService', 'OverService', 'Hub', '$translate',
                        function ($scope, $state, $stateParams, matchService, overService, Hub, $translate) {

                            $scope.Match = {};
                            $scope.Overs = [];
                            $scope.OverDetails = [];

                            //private variables
                            var _matchId = $stateParams.matchId;
                            var _teamToBowlId;
                            var _MakeOvers = function () {
                                var overs = _.groupBy($scope.OverDetails, function (item) { return item.OverNumber });
                                var sortedKeys = Object.keys(overs).sort(function (a, b) {
                                    return b - a;
                                });
                                $scope.Overs = [];
                                sortedKeys.forEach(function (key) {
                                    var obj = {};
                                    obj.OverNumber = key;
                                    obj.Details = overs[key];
                                    $scope.Overs.push(obj);
                                });
                            };
                            var _loadOverdetails = function () {
                                overService.LoadOverDetails(_matchId, _teamToBowlId, null).then(function (overDetails) {
                                    $scope.OverDetails = overDetails;
                                    _MakeOvers();
                                });
                            };
                            (function () {
                                matchService.Get(_matchId).then(function (data) {
                                    $scope.Match = data;
                                    _teamToBowlId = $scope.Match.IsTeam1Bowl ? $scope.Match.Team1ID : $scope.Match.Team2ID;
                                    _loadOverdetails();
                                });
                            })();

                            var hub = new Hub('match', {
                                listeners: {
                                    'makeball': function (data) {//handler for client
                                        data = JSON.parse(data);
                                        $scope.Match.TotalRun = data.TotalRun;
                                        $scope.OverDetails.unshift(data);
                                        console.log($scope.OverDetails);
                                        $scope.$apply();
                                        _MakeOvers();
                                        $scope.$apply();
                                    }
                                },
                                methods: ['addtomatch'],//server Side Method to be called
                                errorHandler: function (error) {
                                    console.error(error);
                                },
                                rootPath: 'http://localhost:49786/signalr',
                                stateChanged: function (state) {
                                    switch (state.newState) {
                                        case $.signalR.connectionState.connected:

                                            break;
                                    }
                                }
                            });
                            hub.promise.done(function () {
                                hub.addtomatch(_matchId);
                            });
                            $scope.Bowl = function () {
                                overService.Save(_matchId, _teamToBowlId).then(function () {
                                    _loadOverdetails();
                                });
                            };

                            $scope.LoadMore = function () {
                                overService.LoadOverDetailsByBallNumber($scope.OverDetails[$scope.OverDetails.length - 1]).then(function (overDetails) {
                                    _.each(overDetails, function (item) {
                                        $scope.OverDetails.push(item);
                                    });
                                    _MakeOvers();
                                });
                            }

                            $scope.changeLang = function (langKey) {
                                $translate.use(langKey);
                            };
                        }]);