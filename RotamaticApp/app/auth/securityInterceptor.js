/* 
    A Response Interceptor registered in app.config. 
    - I use this to capture 401 Unauthorized responses and redirect to the login page.
    - This prevents users using pages they are not authorized to use.

    Sample Registration in app.Config:
    *    app.config(["$httpProvider", function ($httpProvider) {
    *        $httpProvider.responseInterceptors.push('securityInterceptor');
    *    }]);
*/

(function (app) {
    var securityInterceptor = function ($injector, $location) {
        return function (promise) {
            var $http = $injector.get('$http');
            return promise.then(null, function (response) {
                if (response.status === 401) {
                    $location.path("/sign-in");
                }
                return promise;
            });
        };
    };
    app.factory("securityInterceptor", securityInterceptor);
}(angular.module("appModule")))
