(function (app) {
    app.controller('colorAddController', colorAddController);

    colorAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function colorAddController($scope, apiService, notificationService, $state) {
        $scope.Color = {
            Status: true
        }

        $scope.AddColor = AddColor;

        function AddColor() {

            apiService.post('api/color/create', $scope.Color,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('colors');
                }, function (error) {
                    notificationService.displayError(error.data);
                });
        }
    };
})(angular.module('Myshop.colors'));
