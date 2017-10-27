app.controller('GetAllTypesController', ['$scope', 'svcType', '$q', '$modal',
    function ($scope, svcType, $q, $modal) {
       angular.element("title").text("Type List");
        $scope.filter = svcType.getFilter()

        $scope.load = function () {
            svcType.search($scope.filter).then(function (r) {
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
                controller: 'TypeModalController',
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

app.controller('GetTypesController', ['$scope', '$http', '$stateParams', '$q', '$rootScope', 'svcType',
    function ($scope, $http, $stateParams, $q, $rootScope, svcType) {

     
        $rootScope.getId = svcType.getById($stateParams.Id);
        $rootScope.getId.then(function (r) {
            $scope.Id = r.Id;
            $scope.Name = r.Name;
            $scope.Differentials = r.Differentials;
        });
        
    }])

app.controller('DeleteTypesController', ['$scope', '$http', '$stateParams', '$q', '$rootScope', 'svcType',
     function ($scope, $http, $stateParams, $q, $rootScope, svcType) {

         var confirm = confirm('Are you sure you want to save this thing into the database?')
         if (confirm == true) {
             svcType.delete();
         } else {
             // Do nothing!
         }

     }])


app.controller('SaveTypesController', ['$scope', '$state', '$stateParams', 'svcType', function ($scope, $state, $stateParams, svcType) {

    $scope.Id = $stateParams.Id;
    $scope.get = svcType.getById($stateParams.Id);
    $scope.getTypeId = function () {
        if ($scope.Id == 0) {
            $scope.formData = { Id: 0, Name: '', Differential: ''};

        } else {
            svcType.getById($scope.Id).then(function (r) {
                $scope.formData = r;
                console.log($scope.formData);
            });
        }

    }
    
    $scope.getTypeId();
    $scope.save = function () {
        svcType.saveTypes($scope.formData);
    }

}])

app.controller('TypeModalController', ['$scope', '$http', '$state', '$modalInstance', 'DataId', '$stateParams', 'svcType',
    function TypeModalController($scope, $http, $state, $modalInstance, DataId, $stateParams, svcType) {
        $scope.DataId = DataId;
        $scope.delete = function () {
            svcType.Delete($scope.DataId).then(function () {
                $modalInstance.close();
                toastr["error"]("Type Deleted Successfully")
            })
        }
        $scope.dismiss = function () {
            $modalInstance.close();
        }

    }]);