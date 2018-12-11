(function () {
    'use strict';
    angular.module('app.core').factory('interceptorService', ['$q', '$state', '$injector', function ($q, $state, $injector) {
        var interceptorServiceFactory = {};
        var _request = function (req) {
            return req;
        }

        var _response = function (res) {
            return res;
        }
        var _requestError = function (req) {

            return req;
        }
        var _responseError = function (res) {
            if (res.status === 401) {
                $state.go('login');
            } else if (res.status === 500) {

            } else if (res.status === 400) {

            } else {

            }
            return $q.reject(res);
        }
        interceptorServiceFactory.request = _request;
        interceptorServiceFactory.response = _response;
        interceptorServiceFactory.requestError = _requestError;
        interceptorServiceFactory.responseError = _responseError;

        return interceptorServiceFactory;
    }]);
})();
