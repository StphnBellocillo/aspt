app.factory('svcAttendance', ['$http', '$q', function svcAttendance($http, $q) {

    $this = {
        getFilter: function () {
            var filter = JSON.parse(sessionStorage.getItem('attendancesFilter'));
            if (filter) {
                return filter
            } else {
                return {
                    searchText: '',
                    TimeIn: moment().format('L'),
                    TimeOut: moment().format('L'),
                    CurrentPage: 1,
                    PageSize: 10,
                    orderField: "Id",
                    orderBy: "Asc"
                }
            }
        },

        search: function (filter) {
            //sessionStorage.setItem('attendancesFilter', JSON.stringify(filter));
            var deferred = $q.defer();
            return $this.GetPaged(filter.searchText != undefined || filter.searchText === null ? filter.searchText : '', filter.TimeIn != undefined ? filter.TimeIn : null,
                 filter.TimeOut != undefined ? filter.TimeOut : null, filter.CurrentPage,
                filter.PageSize, filter.orderField, filter.orderBy);
        },

        GetPaged: function (searchText, TimeIn, TimeOut, pageno, pagesize, orderField, orderBy) {
            var deferred = $q.defer();
            $http.get('/Attendances?SearchText=' + searchText + '&TimeIn=' + moment(TimeIn).format("YYYY-MM-DD") + '&TimeOut=' + moment(TimeOut).format("YYYY-MM-DD") + '&PageNo=' + pageno + '&PageSize=' + pagesize + '&orderBy=' + orderBy + '&orderField=' + orderField)
             .success(function (response, status) { deferred.resolve(response); deferred.resolve(status); })
                    .error(function (err, status) {
                        deferred.reject(err);
                    });
            return deferred.promise;
        },

        getAllAttendances: function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Attendances/All'
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
                url: '/attendances/' + Id
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        saveAttendance: function (attendance) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/Attendances/add',
                data: attendance

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
            url: '/Attendances/' + id,
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

