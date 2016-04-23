/// <reference path="../components/gpaCard/templates/grpCard.html" />
/// <reference path="../components/entityEditor/directivesExamples.html" />
/// <reference path="../components/entityEditor/directivesExamples.html" />
/// <reference path="../components/entityEditor/directivesExamples.html" />
angular
    .module("app")
    .config([
        "$stateProvider", "$urlRouterProvider", "$locationProvider",
        function($stateProvider, $urlRouterProvider, $locationProvider) {

            $locationProvider.html5Mode(true);
            $urlRouterProvider.otherwise('/tree/path/maps');

            $stateProvider
                .state("login", {
                    url: "/login?returnUrl",
                    templateUrl: "login.html",
                    controller: 'loginController'
                })
                .state("layout", {
                    url: "",
                    template: '<div ng-show="currentUser" class="main-content container-fluid" ui-view></div>',
                    controller: 'getCurrentUser'
                })
                .state("layout.editRemont", {
                    resolve: {
                        entity: [
                            "$stateParams", "remontDataFactory",
                            function ($stateParams, dataFactory) {
                                return dataFactory.getByUID({ UID: $stateParams.UID }).$promise;
                            }]
                    },
                    url: "/tree/remont/:UID",
                    templateUrl: '/html/RemontEdit.html',
                    controller: 'remontEditController'
                })

                .state("layout.tree", {
                    url: "/tree",
                    abstract: true,
                    templateUrl: "/html/Profile/Tree.html",
                    ncyBreadcrumb: {
                        skip: true
                    },
                    controller: "treeCtrl"
                })
                .state("layout.tree.path", {
                    url: "/path?path",
                    ncyBreadcrumb: {
                        skip: true
                    },
                    template: "<ui-view/>"
                })
                
                .state("layout.tree.path.qa", {
                    url: "/qa",
                    templateUrl: "/html/qa.html",
                    ncyBreadcrumb: {
                        skip: true
                    },
                    controller: "qaCtrl"
                })

                .state("layout.tree.path.gpaCard", {
                    url: "/gpaCard",
                    templateUrl: "/Scripts/components/gpaCard/templates/gpaCard.html",
                    ncyBreadcrumb: {
                        skip: true
                    },
                    resolve: {
                        entity: ["treePath", "gpaDataFactory",
                            function (treePath, gpaDataFactory) {
                                return gpaDataFactory.get({ uid: "96A94C5F-DA01-4BDA-826A-18D3268D301E" }).$promise;
                            }
                        ]
                    },
                    controller: "gpaCardController"
                })

                .state("layout.tree.path.toir", {
                    url: "/toir",
                    templateUrl: "/html/toir/toir.html",
                    controller: "toirCtrl"
                })
                .state("layout.tree.path.toir.plan", {
                    url: "/plan",
                    templateUrl: "/html/toir/plan.html",
                    controller: "planCtrl"
                })
                .state("layout.tree.path.toir.current", {
                    url: "/current",
                    templateUrl: "/html/toir/current.html"
                })

                .state("layout.tree.path.plan-grafik", {
                    url: "/plan-grafik",
                    templateUrl: "/html/plan-grafik.html",
                    controller: "planGrafikCtrl"
                })
                 .state('layout.tree.path.maps', {
                     url: "/maps",
                     templateUrl: '/html/maps.html',
                     controller: 'mapsCtrl'
                 })
                .state('layout.admin', {
                    abstract: true,
                    ncyBreadcrumb: {
                        title: "Администрирование"
                    },
                    url: "/admin",
                    template: "<ui-view/>"
                })
                .state("layout.admin.users", {
                    ncyBreadcrumb: {
                        title: "Пользователи"
                    },
                    templateUrl: "/html/components/usersList.html",
                    url: "/users",
                    controller: "usersListController"
                })

                .state("test", {
                    abstract: true,
                url: "/test",
                    template: "<ui-view/>"
                })
                .state("test.inputs", {
                    url: "/inputs",
                    templateUrl: "/scripts/components/entityeditor/directivesexamples.html",
                    controller: "directiveExamplesController"
                })
            .state('layout.tree.path.import-plan-grafik', {
                url: "/import-plan-grafik",
                templateUrl: '/html/import-plan-grafik.html',
                controller: 'importPlanGrafikCtrl'
            });
        }
    ]);