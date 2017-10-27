/// <reference path="../services/PeopleService.js" />
app.controller('GetAllPeopleController', ['$scope', 'svcPerson', '$q', '$modal',
    function ($scope, svcPerson, $q, $modal) {

        angular.element("title").text("People List");
        $scope.filter = svcPerson.getFilter()

        $scope.load = function () {
            svcPerson.search($scope.filter).then(function (r) {
                $scope.TotalItems = r.Count;
                $scope.data = r.Results;
            });
        }

        $scope.Sort = function (field) {
            var sortData = $scope.sorter(field);
            $scope.orderBy = sortData.sortBy;
            $scope.orderField = sortData.field;
            $scope.load();
        }

        $scope.deleteModal = function (id) {
            var modal = $modal.open({
                animation: true,
                templateUrl: '/Ng/views/deletemodal/delete.html',
                controller: 'PersonModalController',
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


app.controller('SavePeopleController', ['$scope', '$stateParams', 'svcPerson', 'svcDepartment',
    function ($scope, $stateParams, svcPerson, svcDepartment) {

        $scope.Id = $stateParams.Id;

        $scope.Departments = [];

        $scope.get = function () {
            svcPerson.getById($stateParams.Id);
        }

        $scope.getPersonId = function () {
            if ($scope.Id == 0) {
                $scope.formData = { Id: 0, FirstName: '', LastName: '', Gender: '', DepartmentId: 0 };


            } else {
                svcPerson.getById($scope.Id).then(function (r) {
                    $scope.formData = r;
                    console.log($scope.formData);
                });
            }

        }

        svcDepartment.getAllDepartments().then(function (r) {

            $scope.Departments = r;

            $scope.get();

        });

        $scope.getPersonId();
        $scope.save = function () {
            svcPerson.savePeople($scope.formData);
        }

    }])

app.controller('PersonModalController', ['$scope', '$http', '$state', '$modalInstance', 'DataId', '$stateParams', 'svcPerson',
    function PersonModalController($scope, $http, $state, $modalInstance, DataId, $stateParams, svcPerson) {
        $scope.DataId = DataId;
        $scope.delete = function () {
            svcPerson.Delete($scope.DataId).then(function () {
                $modalInstance.close();
                toastr["error"]("Person Deleted Successfully")
            })
        }
        $scope.dismiss = function () {
            $modalInstance.close();
        }

    }]);