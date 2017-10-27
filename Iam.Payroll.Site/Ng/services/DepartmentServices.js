/// <reference path="PeopleService.js" />
app.factory('svcDepartment', ['$http', '$q',
    function svcDepartment($http, $q) {

    $this = {

        getFilter: function () {
            var filter = JSON.parse(sessionStorage.getItem('departmentFilter'));
            if (filter) {
                return filter
            } else {
                return {
                    searchText: '',
                    CurrentPage: 1,
                    PageSize: 10,
                    orderField: "Id",
                    orderBy: "Asc"
                }
            }
        },
        search: function (filter) {
            sessionStorage.setItem('departmentFilter', JSON.stringify(filter));
            var deferred = $q.defer();
            return $this.GetPaged(filter.searchText != undefined ? filter.searchText : '',
                 filter.CurrentPage, filter.PageSize);
        },

        GetPaged: function (searchText, PageNo, PageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Department?SearchText=' + searchText + '&PageNo=' + PageNo + '&PageSize=' + PageSize,
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        getAllDepartments: function (SearchText) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Departments?SearchText=' + SearchText
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

         searchDepartment: function (SearchText, PageNo, PageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Department/Search/?SearchText=' + SearchText + '&PageNo=' + PageNo + '&PageSize=' + PageSize,
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (error, status) {
                deferred.reject(error);
            });

            return deferred.promise;
        },

        getById: function (Id) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Department/' + Id
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        saveDepartments: function (department) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/department/add',
                data: department

            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },
         Delete: function (id) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/department/' + id,
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (error, status) {
                deferred.reject(error);
            });

            return deferred.promise;
         },

    }
    return $this;
}])

