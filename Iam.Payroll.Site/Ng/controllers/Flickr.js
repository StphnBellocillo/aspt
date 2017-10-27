app.controller('FlickrController', ['$scope', '$http', function ($scope, $http) {


    $scope.Photos = [];

    $scope.getAllPhotos= function () {
        $http.get('https://api.flickr.com/services/rest/?method=flickr.galleries.getPhotos&api_key=bf1d4d076da16203cbe36d1dbd50c940&gallery_id=72157662112606909&format=json&nojsoncallback=1&auth_token=72157686371780811-e0fa1bb389d415dd&api_sig=ec70748a307d978ea354bbbefb067e83').
            success(function (data) {
                $scope.Photos= data;
            });
    };

    $scope.getAllPhotos(); //call ajax method
}])




//app.controller('FlickrController', ['$scope', '$http', function ($scope, $http) {


//    $scope.Flickr = [];

//    $scope.getAllPhotos = function () {
//        $http.jsonp('https://api.flickr.com/services/rest/?method=flickr.urls.getUserPhotos&api_key=bf1d4d076da16203cbe36d1dbd50c940&user_id=155101709%40N08&format=json').
//            success(function (data) {
//                $scope.Flickr = data;
//            });
//    };

//    $scope.getAllPhotos(); //call ajax method
//}])

