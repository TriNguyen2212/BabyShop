(function (app) {
    app.controller('colorListController', colorListController);

    colorListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function colorListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.colors = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getcolors = getcolors;
        $scope.keyword = '';
        $scope.search = search;

        $scope.deletecolor = deletecolor;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedcolors: JSON.stringify(listId)
                }
            }
            apiService.del('api/color/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.colors, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.colors, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("colors", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletecolor(id) {
            $ngBootbox.confirm('Bạn có muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/color/delete/', config,
                function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công.');
                });
            });
        }

        function search() {
            getcolors();
        }

        function getcolors(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            };
            apiService.get('/api/color/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bảng ghi nào được tìm thấy');
                }

                $scope.colors = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            },
            function () {
                console.log('load color failed');
            });
        }
        $scope.getcolors();
    }
})(angular.module('Myshop.colors'));