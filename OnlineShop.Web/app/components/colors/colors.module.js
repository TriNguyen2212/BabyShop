/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('Myshop.colors', ['Myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('colors', {
            url: "/colors",
            templateUrl: "app/components/colors/colorListView.html",
            parent: 'base',
            controller: "colorListController"
        }).state('color_Add', {
            url: "/color_Add",
            templateUrl: "/app/components/colors/colorAddView.html",
            parent: 'base',
            controller: "colorAddController"
        }).state('color_Edit', {
            url: "/color_Edit/:id",
            templateUrl: "/app/components/colors/colorEditView.html",
            parent: 'base',
            controller: "colorEditController"
        });
    }
})();