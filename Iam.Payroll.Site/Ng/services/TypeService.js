app.factory('svcType', ['$http', '$q', 
 function svcType($http, $q) {

    $this = {

        getFilter: function () {
            var filter = JSON.parse(sessionStorage.getItem('TypeFilter'));
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
            sessionStorage.setItem('TypeFilter', JSON.stringify(filter));
            var deferred = $q.defer();
            return $this.GetPaged(filter.searchText != undefined ? filter.searchText : '',
                 filter.CurrentPage, filter.PageSize);
        },

        GetPaged: function (searchText, PageNo, PageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Types?SearchText=' + searchText + '&PageNo=' + PageNo + '&PageSize=' + PageSize,
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        getAllTypes: function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Types/All'
            })
            .success(function (data, status) {
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },


         searchType: function (SearchText, PageNo, PageSize) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Type/Search/?SearchText=' + SearchText + '&PageNo=' + PageNo + '&PageSize=' + PageSize,
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
                url: '/Type/' + Id
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        saveTypes: function (Type) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/Type/add',
                data: Type

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
                url: '/Type/' + id,
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

