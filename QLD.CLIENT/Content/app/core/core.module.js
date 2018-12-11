(function () {
    'use strict';
    angular.module('app.core', ['ngAnimate', 'ngSanitize', 'ngResource', 'ui.router', 'ngStorage', 'toastr', 'ui.bootstrap', 'cp.ngConfirm'])
        .constant('API', {
            'url': 'http://localhost:62228'
        });
})();