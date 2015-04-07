(function (app) {

    var thisController = function ($scope, $window, $location, $q, authService, $modal, $cookieStore) {

        var init = function () {
            $scope.openLoginModal();
            $scope.setSessionData(null);
        }

        $scope.openLoginModal = function () {
            var modalInstance = $modal.open({
                templateUrl: 'app/auth/signInModal.html',
                controller: loginModalController,
                windowClass: 'x-dialog',
                scope: $scope
            });
        };

        var loginModalController = function ($scope, $modalInstance) {

            $scope.signIn = function (credentials) {
                authService.signIn(credentials)
                    .success(function (sessionData) {
                        if (sessionData.username) {
                            $scope.setSessionData(sessionData);

                            // Create a new cookie.
                            // - The cookie is used to persist the logged in user's user/role data
                            //   over the session and across page refreshes.
                            $cookieStore.put('sessionData', $rootScope.sessionData);

                            $modalInstance.close();
                            $location.path("/rooms");
                        } else {
                            $scope.isInvalidLoginCredentials = true;
                        }
                    })
                    .error(function (res) {
                        $scope.isInvalidLoginCredentials = true;
                    });
            };

            $scope.onKeyPress = function ($event) {
                if ($event.keyCode == 13) {
                    $scope.signIn();
                }
            };

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        };

        init();
    };

    app.controller("authController", thisController);

}(angular.module("appModule")));


