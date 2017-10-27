app.factory('svcHoliday', ['$http', '$q', function svcHoliday($http, $q) {

    $this = {
        getFilter: function () {
            var filter = JSON.parse(sessionStorage.getItem('holidayFilter'));
            if (filter) {
                return filter
            } else {
                return {
                    searchText: '',
                    calendar: moment().format('L'),
                    CurrentPage: 1,
                    PageSize: 10,
                    orderField: "Id",
                    orderBy: "Asc"
                }
            }
        },

        search: function (filter) {
            //sessionStorage.setItem('holidayFilter', JSON.stringify(filter));
            var deferred = $q.defer();
            return $this.GetPaged(filter.searchText != undefined || filter.searchText === null ? filter.searchText : '', filter.calendar != undefined ? filter.calendar : null, filter.CurrentPage,
                filter.PageSize, filter.orderField, filter.orderBy);
        },

        GetPaged: function (searchText, calendar, pageno, pagesize, orderField, orderBy) {
            var deferred = $q.defer();
            $http.get('/Holidays?SearchText=' + searchText + '&calendar=' + moment(calendar).format("YYYY-MM-DD") + '&PageNo=' + pageno + '&PageSize=' + pagesize + '&orderBy=' + orderBy + '&orderField=' + orderField)
             .success(function (response, status) { deferred.resolve(response); deferred.resolve(status); })
                    .error(function (err, status) {
                        deferred.reject(err);
                    });
            return deferred.promise;
        },

        getAllHolidays: function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Holidays/All/'
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
                url: '/Holiday/' + Id
            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        saveHoliday: function (holiday) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/Holiday/add',
                data: holiday

            }).success(function (data, status) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject(data);
            });
            return deferred.promise;
        }
    }
    return $this;
}])

