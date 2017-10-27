app.controller('GetAllDepartmentsController', ['$scope', 'svcDepartment', '$q', '$modal',
    function ($scope, svcDepartment, $q, $modal) {
        angular.element("title").text("Department List");
        $scope.filter = svcDepartment.getFilter()

        $scope.load = function () {
            svcDepartment.search($scope.filter).then(function (r) {
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
                controller: 'DepartmentModalController',
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




        //$scope.SearchText = '';
        //$scope.PageNo = 1;
        //$scope.PageSize = 3;
        //$scope.orderBy = "Id Asc";
        //$scope.Load = function () {
        //    svcDepartment.GetPaged('', $scope.PageSize, $scope.PageNo).then(function (r) {
        //        $scope.departmentList = r.Results;
        //        $scope.TotalItems = r.Count;
        //        console.log($scope.TotalItems)
        //    });

        //}

        //$scope.changePage = function (pageNo) {

        //    $scope.Load();
        //}
        //$scope.Load();
  

         //svcDepartment.getAllDepartments().then(function (r) {
         //   $scope.departmentList = r;
         //});

         //$scope.searchDepartments = function (department) {
         //    svcDepartment.searchDepartments(department, 1, 15, false, "Id", "Asc").then(function (resp) {
         //        $scope.departments = resp.Results;
         //    });
         //}

}])

app.controller('GetDepartmentsController', ['$scope', '$http', '$stateParams', '$q', '$rootScope', 'svcDepartment',
    function ($scope, $http, $stateParams, $q, $rootScope, svcDepartment) {

     
        $rootScope.getId = svcDepartment.getById($stateParams.Id);
        $rootScope.getId.then(function (r) {
            $scope.Name = r.Name;
            $scope.Id = r.Id;
        });
        

}])


app.controller('SaveDepartmentsController', ['$scope', '$state', '$stateParams', 'svcDepartment', function ($scope, $state, $stateParams, svcDepartment) {

    $scope.Id = $stateParams.Id;
    $scope.get = svcDepartment.getById($stateParams.Id);
    $scope.getDepartmentId = function () {
        if ($scope.Id == 0) {
            $scope.formData = { Id: 0, Name: '' };

        } else {
            svcDepartment.getById($scope.Id).then(function (r) {
                $scope.formData = r;
                console.log($scope.formData);
            });
        }

    }
    
    $scope.getDepartmentId();
    $scope.save = function () {
        svcDepartment.saveDepartments($scope.formData);
    }

}])

app.controller('DepartmentModalController', ['$scope', '$http', '$state', '$modalInstance', 'DataId', '$stateParams', 'svcDepartment',
    function DepartmentModalController($scope, $http, $state, $modalInstance, DataId, $stateParams, svcDepartment) {
        $scope.DataId = DataId;
        $scope.delete = function () {
            svcDepartment.Delete($scope.DataId).then(function () {
                $modalInstance.close();
                toastr["error"]("Department Deleted Successfully")
            })
        }
        $scope.dismiss = function () {
            $modalInstance.close();
        }

    }]);