var deps = ['ui.router', 'RecursionHelper', 'SignalR', 'pascalprecht.translate'];
var app = angular.module('MyApp', deps);

app.config(['$urlRouterProvider', '$stateProvider', function ($urlRouterProvider, $stateProvider) {
    // default route
    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('home', {
            url: '/',
            templateUrl: 'app/view/home.html',
            controller: 'homeCtrl'
        })
         .state('matches', {
             url: '/matches',
             templateUrl: 'app/view/matches.html',
             controller: 'matchesCtrl'
         })
        .state('start', {
            url: '/start/{matchId:[0-9]{1,5}}',
            templateUrl: 'app/view/start.html',
            controller: 'startCtrl'
        })
        .state('exception', {
            url: '/exception',
            templateUrl: 'app/view/exception.html'
        })
        .state('review', {
            url: '/review/{matchId:[0-9]{1,5}}/{teamId:[0-9]{1,5}}/{overnumber:[0-9]{1,5}}/{ballnumber:[0-9]{1,5}}',
            templateUrl: 'app/view/review.html',
            controller: 'reviewCtrl'
        });

}]);


app.config(function ($translateProvider) {
    $translateProvider.translations('en', {
        TITLE: 'Selise CricCard',
        CREATEMATCH: 'CREATE MATCH',
        FIRSTTEAMNAME: 'Team 1 Name',
        SECONDTEAMNAME: 'Team 2 Name',
        PLAYNOW: 'PLAY NOW',
        OVER: 'Over',
        BALL:'Ball',
        NO_MATCH_AVAILABLE: 'NO MATCH AVAILABLE',
        BACK_GAME: 'Back To The Game',
        BUTTON_LANG_EN: 'English',
        BUTTON_LANG_DE: 'German',
        LOAD_MORE: 'Load More',
        MESSAGE: 'The Match or Ball you are looking for doesn’t exist. Take me'
        
    });
    $translateProvider.translations('de', {
        TITLE: 'Selise CricCard',
        CREATEMATCH: 'Create Match',
        FIRSTTEAMNAME: 'Team 1 Name',
        SECONDTEAMNAME: 'Team 2 Name',
        PLAYNOW: 'PLAY NOW',
        OVER: 'Zu Ende',
        BALL: 'Ball',
        NO_MATCH_AVAILABLE: 'NICHTS PASS VERFÜGBAR',
        BACK_GAME: 'Zurück zum Spiel',
        BUTTON_LANG_EN: 'Englisch',
        BUTTON_LANG_DE: 'Deutsch',
        LOAD_MORE: 'Mehr laden',
        MESSAGE: ' Das Spiel oder Kugel sie existiert nicht. Bringen Sie mich '
        
       
    });
    $translateProvider.preferredLanguage('en');
});
