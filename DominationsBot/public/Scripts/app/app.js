angular
    .module("app", [
        "ngSanitize",
        "ngMessages",
        "ngResource",
        "ui.bootstrap",
        "ui.select",
        "ui.router",
        "ks.ui.modalSpinner",
        "ks.ui.locker",
        "ks.validator",
        "mgcrea.ngStrap.timepicker",
        "mgcrea.ngStrap.datepicker",
        "toaster",
        "ipCookie",
        "ncy-angular-breadcrumb",
        "browserInformation",
        "kendo.directives",
        "gcr.interactivePlan",
        "star.entityEditor",
        "ks.alert",
        "tree",
        'admin.users',
        "qa",
        "ks.grid",
        "ks.editors",
        "ks.httpInterceptors",
        "gcr.planGrafik",
        'toir',
        "gcr.gpaCard",
        "gcr.remont",
        'ks.splitter',
        "app.login"
    ])
    .value("gpaRepairChartTemplates", {
        templateUrl: "/scripts/components/gpaRepairChart/chart/partial/chart.html",
        legendTemplateUrl: "/scripts/components/gpaRepairChart/chart/partial/legend.html"
    })

    .config([
        "uiSelectConfig",
        function (uiSelectConfig) {
            uiSelectConfig.theme = "bootstrap";
        }
    ])
    .config([
        "starFileEditorSettingsProvider",
        function (starFileEditorSettings) {
            starFileEditorSettings.basePath = "/scripts/components/entityeditor/directives/inputdirectives/fileeditor";
        }
    ])
    .config([
        "$httpProvider",
        function ($httpProvider) {
            $httpProvider.interceptors.push("unauthorizedRequestRedirectHandler");
            $httpProvider.interceptors.push("saveRequestHandler");
            $httpProvider.interceptors.push("lockScreenRequestHandler");
            //$httpProvider.interceptors.push("noTimeZonesRequestHandler");
            $httpProvider.interceptors.push("ajaxErrorHandler");
        }
    ])
    .factory("unauthorizedRequestRedirectHandler", [
        "$q", "$window","$injector",
        function ($q, $window, $injector) {
            return {
                responseError: function (rejection) {
                    if (rejection.status === 401) {
                        var $state = $injector.get('$state');
                        $state.go("login", { returnUrl: $window.encodeURIComponent($window.location.url()) });
                    }
                    return $q.reject(rejection);
                }
            };
        }
    ])
    .config([
        function () {
            window.handler = function (fn) {
                var result = {};
                result[_.uniqueId()] = fn;
                return result;
            };
        }
    ])
    .config([
        "$datepickerProvider", function ($datepickerProvider) {
            angular.extend($datepickerProvider.defaults, {
                dateFormat: "dd.MM.yyyy",
                //modelDateFormat: "yyyy-MM-ddTHH:mm:ss",
                //dateType: "string",
                startWeek: 1,
                autoclose: true
            });
        }
    ])
    .config([
        "$breadcrumbProvider", function ($breadcrumbProvider) {
            $breadcrumbProvider.setOptions({
                templateUrl: "/html/NcyBreadcrumb.html"
            });
        }
    ])
    .config([
        "$logProvider", function ($logProvider) {
            $logProvider.debugEnabled(false);
        }
    ])
    //.value('isDebug', true)
    .config([
        "$provide",
        function ($provide) {
            $provide.decorator("$exceptionHandler", [
                "$delegate", "$injector", "isDebug",
                function ($delegate, $injector, isDebug) {
                    return function (exception, cause) {
                        $delegate(exception, cause);
                        if (isDebug)
                            $injector.get("toaster").pop({
                                type: "error",
                                title: "Ошибка в JS",
                                body: "<pre contenteditable='true' style='height:150px'>" + exception.stack + "</pre>",
                                bodyOutputType: "trustedHtml",
                                showCloseButton: true,
                                //toastId: 'exception',
                                timeout: 0,
                                clickHandler: function (toast, isCloseButton) {
                                    return isCloseButton;
                                }
                            });
                    };
                }
            ]);
        }
    ])
    .run([
        "$uibModalStack", "$rootScope", 'treePath',
        function ($modalStack, $rootScope, treePath) {
            $rootScope.treePath = treePath;
            $rootScope.$on("$locationChangeSuccess", function (event, next, current) {
                if (next == current)
                    return;
                $modalStack.dismissAll("$locationChangeSuccess");
            });
        }
    ])
    .constant("dataFactoryObjectIDDefault", { objectID: "@objectID" })
    .directive("initJsonValue", function () {
        return {
            priority: 450,
            restrict: "E",
            compile: function () {
                return {
                    pre: function (scope, element, attrs) {
                        scope[attrs.name] = angular.fromJson(element.html());
                    }
                };
            }
        };
    })
    .run([
        "$rootScope",
        function ($rootScope) {
            $rootScope.$on("$stateChangeError", function (event, toState, toParams, fromState, fromParams, error) {
                event.preventDefault();
            });
        }
    ]);

/**
 * Provides a start point to run plugins and other scripts
 */
$(function () {
    $("#top-link-block-a").on("click", function () {
        $("html, body").animate({ scrollTop: 0 });
        return false;
    });

    kendo.culture("ru-RU");

    var filterMenu = kendo.ui.FilterMenu;
    if (filterMenu) {

        filterMenu.prototype.options.operators.string =
            $.extend(true, kendo.ui.FilterMenu.prototype.options.operators.string, {
                "nullorempty": "Не заданы" // CHANGED: added nullorempty operator
            });

        function removeFiltersForField(expression, field) {
            if (expression.filters) {
                expression.filters = $.grep(expression.filters, function (filter) {
                    removeFiltersForField(filter, field);
                    if (filter.filters) {
                        return filter.filters.length;
                    } else {
                        return filter.field != field;
                    }
                });
            }
        }

        filterMenu.prototype._merge = function (expression) {
            var that = this,
                logic = expression.logic || "and",
                filters = expression.filters,
                filter,
                result = that.dataSource.filter() || { filters: [], logic: "and" },
                idx,
                length;

            removeFiltersForField(result, that.field);

            filters = $.grep(filters, function (filter) {
                return filter.value !== "" && filter.value != null || filter.operator == "nullorempty";
            });

            for (idx = 0, length = filters.length; idx < length; idx++) {
                filter = filters[idx];
                filter.value = that._parse(filter.value);
            }

            if (filters.length) {
                if (result.filters.length) {
                    expression.filters = filters;

                    if (result.logic !== "and") {
                        result.filters = [{ logic: result.logic, filters: result.filters }];
                        result.logic = "and";
                    }

                    if (filters.length > 1) {
                        result.filters.push(expression);
                    } else {
                        result.filters.push(filters[0]);
                    }
                } else {
                    result.filters = filters;
                    result.logic = logic;
                }
            }

            return result;
        };
        filterMenu.prototype.filter = function (expression) {
            expression = this._merge(expression);

            if (expression.filters.length) {
                this.dataSource.filter(expression);
            } else {
                this.dataSource.filter(null);
            }
        };

        /* Filter menu operator messages */
        filterMenu.prototype.options.operators =
            $.extend(true, filterMenu.prototype.options.operators, {
                "date": {
                    "eq": "равны",
                    "gte": "с",
                    "lte": "по",
                    "gt": "от",
                    "lt": "до",
                    "neq": "не равны"
                },
                "number": {
                    "eq": "равны",
                    "gte": "больше или равны",
                    "gt": "больше",
                    "lte": "меньше или равны",
                    "lt": "меньше",
                    "neq": "не равны"
                },
                "string": {
                    "endswith": "оканчиваются на",
                    "eq": "равны",
                    "neq": "не равны",
                    "startswith": "начинаются на",
                    "contains": "содержат",
                    "doesnotcontain": "не содержат",
                    "nullorempty": "не заданы"
                },
                "enums": {
                    "eq": "равны",
                    "neq": "не равны"
                }
            });

        kendo.ui.FilterMenu.prototype.options.messages =
            $.extend(true, kendo.ui.FilterMenu.prototype.options.messages, {
                "info": "Строки, значения которых"
            });
    }

    var grid = kendo.ui.Grid;
    if (grid) {
        grid.prototype.options =
            $.extend(true, grid.prototype.options, {
                resizable: true,
                pageable: {
                    pageSize: 10
                },
                height: "auto"
            });
    }

    var pager = kendo.ui.Pager;
    if (pager) {
        var options = pager.prototype.options;
        options.pageSizes = [5, 10, 20, 50, 100, 500, 1000];
        options.refresh = true;
    }


    if (!store.enabled) {
        alert('Local storage is not supported by your browser. Please disable "Private Mode", or upgrade to a modern browser.');
        return;
    }

    // isoString UTC time
    (function () {
        var offsetMiliseconds = new Date().getTimezoneOffset() * 60000;
        var nativeToISOString = Date.prototype.toISOString;
        Date.prototype.toISOString = function () {
            return nativeToISOString.apply(new Date(+this - offsetMiliseconds));
        }
    })();
});