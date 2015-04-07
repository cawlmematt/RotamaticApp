(function (app) {

    var thisDirective = function () {
        return {
            templateUrl: "app/directives/mjcFullCalendar.html",
            restrict: "E",
            replace: true,
            scope: true,
            link: function (scope, element, attrs) {

                console.log("------------------------------------");
                console.log("link scope: " + scope);
                console.log(scope);
                console.log(scope.theEvents);
                console.log(scope.theEvents);
                console.log("------------------------------------");


                scope.$watch("theEvents", function (newValue, oldValue) {
                    if (newValue) {
                        $('#calendar').fullCalendar('destroy');

                        $('#calendar').fullCalendar({
                            events: scope.theEvents
                        });
                    }
                });




                
            },
            controller: function ($scope) {
                console.log("************************************");
                console.log("controller scope: " + $scope);
                console.log($scope);
                console.log($scope.theEvents);
                console.log("************************************");
            }
        };
    };

    app.directive("mjcFullCalendar", thisDirective);

}(angular.module("appModule")))