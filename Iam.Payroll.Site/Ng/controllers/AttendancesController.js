app.controller('AttendanceListController', ['$scope', 'svcAttendance', '$q', '$modal',
    function ($scope, svcAttendance, $q, $modal) {

        angular.element("title").text("Attendances List");
        $scope.filter = svcAttendance.getFilter()
    
        console.log($scope.filter);
        $scope.load = function () {
            
            svcAttendance.search($scope.filter).then(function (data) {
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
    }])

//app.controller('GetPeopleController', ['$scope', '$http', '$stateParams', '$q', '$rootScope', 'peopleService',
//    function ($scope, $http, $stateParams, $q, $rootScope, peopleService) {


//        $rootScope.getId = peopleService.getById($stateParams.Id);
//        $rootScope.getId.then(function (r) {
//            $scope.Id = r.Id;
//            $scope.FirstName = r.FirstName;
//       7     $scope.LastName = r.LastName;
//            $scope.Gender = r.Gender;
//            $scope.DepartmentName = r.DepartmentId;
//        });


//    }])


app.controller('AttendanceSaveController', ['$scope', '$stateParams', 'svcAttendance', 'svcPerson',
    function ($scope, $stateParams, svcAttendance, svcPerson) {

    $scope.Id = $stateParams.Id;
    
    $scope.People = [];

    $scope.get = function(){
        svcAttendance.getById($stateParams.Id);
    }

    $scope.getAttendanceId = function () {
        if ($scope.Id == 0) {
            $scope.formData = { Id: 0, PersonId: 0, TimeIn:'', TimeOut: ''};


        } else {
            svcAttendance.getById($scope.Id).then(function (r) {
                $scope.formData = r;
                console.log($scope.formData);
            }); 
        }

    }

    svcPerson.getAllPeople().then(function (r) {

        $scope.People = r;

        $scope.get();

    });

    $scope.getAttendanceId();

    $scope.save = function () {
        svcAttendance.saveAttendance($scope.formData);
    }

}])



app.controller('AttendanceModalController', ['$scope', '$http', '$state', '$modalInstance', 'DataId', '$stateParams', 'svcAttendance',
    function AttendanceModalController($scope, $http, $state, $modalInstance, DataId, $stateParams, svcAttendance) {
        $scope.DataId = DataId;
        $scope.delete = function () {
            svcAttendance.Delete($scope.DataId).then(function () {
                $modalInstance.close();
                toastr["error"]("Attendance Deleted Successfully")
            })
        }
        $scope.dismiss = function () {
            $modalInstance.close();
        }

    }]);