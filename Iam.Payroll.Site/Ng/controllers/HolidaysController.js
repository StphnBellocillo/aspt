app.controller('HolidayListController', ['$scope', 'svcHoliday', '$q', '$modal',
    function ($scope, svcHoliday, $q, $modal) {

        angular.element("title").text("Holidays List");
        $scope.filter = svcHoliday.getFilter()

        $scope.load = function () {

            svcHoliday.search($scope.filter).then(function (data) {
                $scope.data = data.Results
                $scope.TotalItems = data.Count;


            }, function (error) {

            });

        }

        $scope.Sort = function (field) {
            var sortData = $scope.sorter(field);
            $scope.filter.orderBy = sortData.sortBy;
            $scope.filter.orderField = sortData.field;
            $scope.load();
        }

        $scope.deleteModal = function (id) {
            var modal = $modal.open({
                animation: true,
                templateUrl: '/Ng/views/deletemodal/delete.html',
                controller: 'HolidayModalController',
                resolve: {
                    DataId: function () {
                        return id;
                    }
                }
            });

            modal.result.then(function () {
                $scope.load()
            }, function () {
            });
        }


        $scope.changePage = function (pageNo) {

            $scope.load();
        }

        //init function
        $q.all([]).then(function () {
            $scope.load();
        });

    }])


app.controller('HolidaySaveController', ['$scope', '$stateParams', 'svcHoliday', 'svcType',
    function ($scope, $stateParams, svcHoliday, svcType) {

        $scope.Id = $stateParams.Id;

        $scope.Types = [];

        $scope.get = function () {
            svcHoliday.getById($stateParams.Id);
        }

        $scope.getHolidayId = function () {
            if ($scope.Id == 0) {
                $scope.formData = { Id: 0, TypeId: 0, Name: '', Date: '' };


            } else {
                svcHoliday.getById($scope.Id).then(function (r) {
                    $scope.formData = r;
                    console.log($scope.formData);
                });
            }

        }

        svcType.getAllTypes().then(function (r) {

            $scope.Types = r;

            $scope.get();

        });

        $scope.getHolidayId();

        $scope.save = function () {
            svcHoliday.saveHoliday($scope.formData);
        }

    }])



app.controller('HolidayModalController', ['$scope', '$http', '$state', '$modalInstance', 'DataId', '$stateParams', 'svcHoliday',
    function HolidayModalController($scope, $http, $state, $modalInstance, DataId, $stateParams, svcHoliday) {
        $scope.DataId = DataId;
        $scope.delete = function () {
            svcHoliday.Delete($scope.DataId).then(function () {
                $modalInstance.close();
                toastr["error"]("Holiday Deleted Successfully")
            })
        }
        $scope.dismiss = function () {
            $modalInstance.close();
        }

    }]);
