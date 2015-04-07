/// <reference path="1-home/home.html" />
(function () {

    // Create the module with a name and with no dependencies
    var app = angular.module("appModule", ["ngRoute", 'ui.bootstrap', "ngAnimate", 'ngCookies']);

    app.config(["$httpProvider", function ($httpProvider) {
        // We have to add the security interceptor by name rather than the object itself
        // because it depends on services that are not available within the config block.
        //$httpProvider.responseInterceptors.push('securityInterceptor');

    }]);

    app.run(function ($rootScope, $log, $cookieStore) {
        $rootScope.sessionData = { };

        $rootScope.$watchCollection('sessionData', function () {
            console.log('sessionData has changed. Updating cookie...');
            $cookieStore.put('sessionData', $rootScope.sessionData);
            console.log('Cookie updated.');
        });
    });

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/home", {
                templateUrl: "app/home/home.html",
                controller: "homeController"
            })
            .when("/sign-in", {
                templateUrl: "app/auth/login.html",
                controller: "loginController"
            })
            .when("/calendar", {
                templateUrl: "app/calendar/calendar.html",
                controller: "calendarController",
                resolve: {
                    permission: function (authService) {
                        authService.checkPermission(['Admin', 'User']);
                    }
                }
            })
            .otherwise({ redirectTo: "/home" });
    });

}());