(function () {
    'use strict';
    angular.module('app.header')
        .controller('HeaderController', ['$state', 'AuthService', function ($state, AuthService) {
            var vm = this;
            vm.currentUser = AuthService.currentUser;
            vm.logout = logout;
            function logout() {
                AuthService.logout();
                $state.go('home');
            }
        }]);
})();