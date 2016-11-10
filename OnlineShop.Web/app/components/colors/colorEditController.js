(function (app) {
    app.controller('colorEditController', colorEditController);

    colorEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function colorEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.color = {
            Status: true
        }
        function loadcolorDetail() {
            apiService.get('/api/color/getbyid/' + $stateParams.id, null, function (result) {
                $scope.color = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.UpdateColor = UpdateColor;

        function UpdateColor() {
            apiService.put('/api/color/update', $scope.color,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('colors');
                }, function (error) {
                    notificationService.displayError(error.data);
                });
        }
        loadcolorDetail();
    };
})(angular.module('Myshop.colors'));
