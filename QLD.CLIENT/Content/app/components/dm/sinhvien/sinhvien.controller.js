(function () {
    'use strict';
    angular.module('app.dm')
        .controller('SinhVienController', ['$state', 'configService', '$uibModal', 'toastr', 'SinhVienService', '$ngConfirm', function ($state, configService, $uibModal, toastr, service, $ngConfirm) {
            var vm = this;
            function filterData() {
                service.getAll().then(function (response) {
                    if (response && response.status ===200) {
                        vm.target = response.data;
                    }
                })
                  
            }
            filterData();
        }]);
})();