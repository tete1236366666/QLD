(function () {
    'use strict';
    angular
        .module('app.dm')
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('sinhvien',
                {
                    url: "/dm/sinhvien",
                    templateUrl: "/Content/app/components/dm/sinhvien/index.html",
                    controller: "SinhVienController",
                    controllerAs: "vm"
                });
    }
})();