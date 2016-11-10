/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('Myshop.products', ['Myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "app/components/products/productListView.html",
            parent:'base',
            controller: "productListController"
        }).state('product_Add', {
            url: "/product_Add",
            templateUrl: "/app/components/products/productAddView.html",
            parent: 'base',
            controller: "productAddController"
        }).state('product_Edit', {
            url: "/product_Edit/:id",
            templateUrl: "/app/components/products/productEditView.html",
            parent: 'base',
            controller: "productEditController"
        });
    }

})();
