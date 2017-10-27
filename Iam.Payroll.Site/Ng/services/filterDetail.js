app.factory('svcFilter', [function svcFilter() {
    var filterData = [];
    var addFilter = function (newObj) {
        filterData = newObj;
    }
    var getFilter = function () {
        return filterData;
    }
    return {
        addFilter: addFilter,
        getFilter: getFilter
    };
}]);