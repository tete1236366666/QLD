(function () {
    'use strict';
    angular.module('app.core').factory('AuthService', ['$q', '$http', '$localStorage', 'API', function ($q,$http, $localStorage, API) {
        var authService = {
            currentUser: {
                access_token: null,
                username: null,
                email: null,
                expires: null,
                issued: null
            },
            login: login,
            logout: logout,
            getCurrentUser:function() {
                this.currentUser = $localStorage.currentUser;
            },
            updateCurrentUser: function (user) {
                this.currentUser.access_token = user.access_token;
                this.currentUser.username = user.username;
                this.currentUser.email = user.email;
                this.currentUser.expires = user.expires;
                this.currentUser.issued = user.issued;
            },
            isAuthenticated: function () {
                this.getCurrentUser();
                if (!this.currentUser || !this.currentUser.access_token) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        function login(user) {
            var obj = {
                'username': user.username,
                'password': user.password,
                'grant_type': 'password'
            };
            Object.toparams = function ObjectsToParams(obj) {
                var p = [];
                for (var key in obj) {
                    p.push(key + '=' + encodeURIComponent(obj[key]));
                }
                return p.join('&');
            }

            var defer = $q.defer();
            $http({
                method: 'post',
                url: API.url + "/token",
                data: Object.toparams(obj),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                if (response.status && response.data.username) {
                    var res = response.data;
                    $http.defaults.headers.common.Authorization = 'Bearer ' + res.access_token;
                    var user = {
                        username: res.username,
                        access_token: res.access_token,
                        email: res.email,
                        expires: new Date(res[".expires"]),
                        issued: new Date(res[".issued"])
                    }
                    $localStorage.currentUser = user;
                    authService.currentUser = user;
                    defer.resolve(response);
                } else {
                    defer.reject(response);
                }
            },function(error) {
                defer.reject(error);
            });
            return defer.promise;
        }

        function logout() {
            $http.defaults.headers.common.Authorization = null;
            $localStorage.currentUser = null;
            authService.currentUser.token = null;
            authService.currentUser.username = null;
            authService.currentUser.email = null;
            authService.currentUser.expires = null;
            authService.currentUser.issued = null;
            return;
        }
        return authService;
    }]);
})();