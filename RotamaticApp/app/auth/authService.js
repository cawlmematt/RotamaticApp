(function (app) {

    var authService = function ($http, $q, $window, $rootScope, $location) {

        this.signIn = function (credentials) {
            // Request Login
            return $http({
                url: "api/auth/sign-in",
                method: 'POST',
                data: JSON.stringify(credentials),
                headers: {
                    "Content-Type": "application/json"
                }
            })
            .success(function (data) {
                return data;
            })
            .error(function (err) {
                return err;
            });
        };

        this.signOut = function () {
            // Request Logout
            return $http({
                url: "api/auth/sign-out",
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                }
            })
            .success(function (res) {

            });
        };

        this.checkPermission = function (permittedRoles) {

            var hasRequiredRole = false;

            if (!!$rootScope.sessionData) {
                if (!!$rootScope.sessionData.userRoles) {
                    _.each($rootScope.sessionData.userRoles, function (userRole) {
                        _.each(permittedRoles, function (permittedRole) {
                            if (userRole.toUpperCase === permittedRole.toUpperCase)
                                hasRequiredRole = true;
                        });
                    });
                }
            }

            if (!hasRequiredRole) {
                $location.path('/sign-in');
            }
        };
    };

    app.service("authService", authService);

}(angular.module("appModule")))
