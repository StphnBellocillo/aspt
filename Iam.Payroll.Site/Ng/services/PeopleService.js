app.factory('svcPerson', ['$http', '$q', function svcPerson($http, $q) {

    $this = {

        getFilter: function () {
            var filter = JSON.parse(sessionStorage.getItem('peopleFilter'));
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
            sessionStorage.setItem('peopleFilter', JSON.stringify(filter));
            var deferred = $q.defer();
            return $this.GetPaged(filter.searchText != undefined ? filter.searchText : '',
                 filter.CurrentPage, filter.PageSize);
        },

        GetPaged: function (searchText, PageNo, PageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/People?SearchText=' + searchText + '&PageNo=' + PageNo + '&PageSize=' + PageSize,
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },
        getAllPeople: function (pageNo, pageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/People/All/'
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        getById: function (Id) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/People/' + Id
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        savePeople: function (people) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/People/add',
                data: people

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
        }
    }
    return $this;
}])

