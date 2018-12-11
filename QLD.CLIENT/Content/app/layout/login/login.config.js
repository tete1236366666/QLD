(function () {
    'use strict';
    angular.module('app.login').config(config);
    function config($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/home");
        $stateProvider.state('login', {
            url: "/login",
            templateUrl: "/Content/app/layout/login/login.html",
            controller: "LoginController",
            controllerAs: "vm"
        })
    }
})();