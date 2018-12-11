(function () {
    'use strict';
    angular.module('app.home')
        .controller('HomeController', ['AuthService', '$state', function (AuthService, $state) {
            var vm = this;
            if (!AuthService.isAuthenticated()) {
                $state.go('login');
            } else {
                vm.currentUser = AuthService.currentUser;
            }

            vm.dmSupplier = function() {
                $state.go('dmSupplier');
            }
        }]);
})();