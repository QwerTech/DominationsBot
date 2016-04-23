angular
    .module("app.login", [
        "ks.ui.modalSpinner",
        "ngResource"
    ])
    .factory("usersDataFactory", [
        "$resource",
        function($resource) {
            return $resource("/api/Users/:action/:objectID", {}, {
                login: { method: "POST", params: { action: "Login" }, lockScreen:true },
                getCurrentUser: { method: "GET", params: { action: "GetCurrentUser" } },
                logout: { method: "GET", params: { action: "Logout" } }
            });
        }
    ])
    .controller("userLogOutController", [
        "usersDataFactory", "$scope", "$window", "$state", "$location",
        function(dataFactory, $scope, $window, $state, $location) {
            $scope.logout = function() {
                dataFactory.logout().$promise.then(function() {
                    var loginUrl = $state.href("login", { returnUrl: $window.encodeURIComponent($location.url()) });
                    $window.location.href = loginUrl;
                });
            };
        }
    ])
    .controller("getCurrentUser", [
        "$rootScope", "usersDataFactory", "$state",
        function($rootScope, usersDataFactory, $state) {
            usersDataFactory.getCurrentUser().$promise.then(function(user) {
                user.isInRole = function(role) {
                    return _.any(user.Roles, function(userRole) {
                        return userRole.Role.toUpperCase() == role.toUpperCase();
                    });
                };
                $rootScope.currentUser = user;
            }, function() {
                $state.go("login");
            });
        }
    ])
    .controller("loginController", [
        "$scope", "$http", "$window", "usersDataFactory", "longOperationService", "$location",
        function($scope, $http, $window, usersDataFactory, longOperationService, $location) {
            longOperationService.endLoad();
            $scope.logIn = function() {
                $scope.wrongCredentials = $scope.accessDenied = "";
                if ($scope.formsLoginForm.$invalid) {
                    $scope.wrongCredentials = true;
                    $scope.Message = "";
                    return;
                }
                return usersDataFactory
                    .login($scope.logInfo, function(resp) {
                        angular.extend($scope, resp);
                        if ($scope.Success) {
                            $location.url(decodeURIComponent($location.search()["returnUrl"] || "/"));
                        } 
                    }, function() {
                        $scope.wrongCredentials = true;
                    }).$promise;
            };
        }
    ]);