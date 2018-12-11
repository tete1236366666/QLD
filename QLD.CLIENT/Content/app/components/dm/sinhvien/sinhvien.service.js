(function () {
    'use strict';
    angular.module('app.dm').factory('SinhVienService', ['$http', 'API', function ($http, api) {
        var baseApi = api.url + "/api/home";
        var result = {
            getAll: function (param) {
                return $http.get(baseApi + "/getall");
            }
        }
        return result;
    }]);
})();