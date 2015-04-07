(function (app) {

    var thisController = function ($scope, $location, $cookieStore, authService) {

        $scope.login = function (formData) {
            authService.signIn(formData)
                .success(function (sessionData) {
                    if (sessionData.username) {
                        $scope.setSessionData(sessionData);
                        $cookieStore.put('sessionData', sessionData);
                        $location.path("/home");
                    } else {
                        $scope.isInvalidLoginCredentials = true;
                    }
                })
                .error(function (error) {
                    $scope.isInvalidLoginCredentials = true;
                });
        }

    };

    app.controller("loginController", thisController);

}(angular.module("appModule")));


