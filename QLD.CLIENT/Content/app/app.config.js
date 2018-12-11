(function () {
    'use strict';
    angular.module('app')
        .config(['$locationProvider', '$localStorageProvider', '$httpProvider', function ($locationProvider, $localStorageProvider, $httpProvider) {
            $locationProvider.html5Mode(false);
            $localStorageProvider.setKeyPrefix('clinic-');
            $httpProvider.interceptors.push('interceptorService');
        }])
        .run(['$http', '$rootScope', '$localStorage', '$state', 'AuthService', function ($http, $rootScope, $localStorage, $state, AuthService) {
            if ($localStorage.currentUser) {
                $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.currentUser.access_token;
                AuthService.updateCurrentUser($localStorage.currentUser);
            }
            $rootScope.$on('$stateChangeStart',
                function (event, toState, toParams, fromState, fromParams, options) {
                    if (toState.name == 'login' || toState.name == 'home') {
                    } else {
                        if (!AuthService.isAuthenticated()) {
                            event.preventDefault();
                            $state.go('login');
                        }
                    }
                });
        }]);
})();