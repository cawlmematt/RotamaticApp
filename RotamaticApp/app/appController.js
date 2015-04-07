(function (app) {

    var thisController = function ($scope, $rootScope, $location, $cookieStore, $http, authService) {

        var init = function () {
            $rootScope.sessionData = $cookieStore.get('sessionData');

            $http.get('api/Google').
                success(function (data) {
                    console.log("Google Get Success");
                    $scope.theEvents = data;

                    _.each($scope.theEvents, function (anEvent) {
                        anEvent.start = moment(anEvent.start);
                        anEvent.end = moment(anEvent.end);
                    });
                }).
                error(function () {
                    console.log("Google Get Failure");
                });



            console.log("Try get Google");

            
            $scope.theEvents = [];

            
        }

        $scope.requestLeave = function () {
            console.log("leave requested");
        };

        $scope.isActive = function (route) {
            return route === $location.path();
        };

        $scope.signOut = function () {
            authService.signOut().then(function (data) {
                $scope.setSessionData(null);
                $cookieStore.remove('sessionData');
                $location.path("/home");
            });
        };
        
        $scope.setSessionData = function (sessionData) {
            $rootScope.sessionData = sessionData;

            if ($rootScope.sessionData) {
                // Convert roles into easier accessible booleans
                _.each($rootScope.sessionData.userRoles, function (userRole) {
                    $rootScope.sessionData[userRole] = true;
                });
            }
        }

        $scope.globalSettings = {};

        $rootScope.sessionData = null;

        init();
    };

    app.controller("appController", thisController);

}(angular.module("appModule")));


