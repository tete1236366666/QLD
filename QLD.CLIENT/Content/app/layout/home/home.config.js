(function () {
    'use strict';
    angular
        .module('app.home')
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('home', {
                url: "/home",
                templateUrl: "/Content/app/layout/home/home.html",
                controller: "HomeController",
                controllerAs: "vm"
            })
    }
})();