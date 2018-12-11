(function () {
    'use strict';
    angular.module('app.core')
        .directive('ngEnter', function () {
            return function (scope, element, attrs) {
                element.bind("keydown keypress", function (event) {
                    if (event.which === 13) {
                        scope.$apply(function () {
                            scope.$eval(attrs.ngEnter);
                        });
                        event.preventDefault();
                    }
                });
            };
        })
        .directive('preventDefault', function () {
            return function (scope, element, attrs) {
                element.bind('click', function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                });
            }
        })
        .directive('myLoading', function () {
            var result = {
                restrict: 'E',
                template: '<p class="text-center"><i class="fa fa-refresh fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span></p>'
            }
            return result;
        });;
})();