(function () {
    'use strict';
    angular.module('app.login')
        .controller('LoginController', ['$state', '$http', 'AuthService', function ($state, $http, AuthService) {
            var vm = this;
            vm.username = '';
            vm.password = '';
            vm.login = function () {
                var user = { username: vm.username, password: vm.password }
                AuthService.login(user).then(function (response) {
                    console.log(response);
                    if (AuthService.isAuthenticated()) {
                        $state.go('home');
                    } else {
                        console.log(AuthService.currentUser);
                    }
                }, function (error) {
                    alert(error);
                });
            }
        }]);
})();