(function () {
    'use strict';
    angular.module('app.core').factory('configService', function () {
        var result = {

        }
        result.buildUrl = function(module,controller,action) {
            return '/Content/app/components/'+module+"/"+controller+"/"+action+".html";
        };

        result.pageDefault = {
            totalItems: 0,
            itemsPerPage: 10,
            currentPage: 1,
            pageSize: 5,
            totalPage: 5
        };
        result.filterDefault = {
            summary: '',
            isAdvance: true,
            advanceData: {},
            orderBy: '',
            orderType: 'ASC',
            isStatus: false
        };
        return result;
    });
})();
