var app = angular.module('app', [
'ng',
'ngSanitize',
'ui.bootstrap',
'ui.router',
'angularFileUpload',
'ui'

]);
app.config(['$modalProvider', '$locationProvider', '$stateProvider', '$urlRouterProvider',
       function ($modalProvider, $locationProvider, $stateProvider, $urlRouterProvider) {
           $modalProvider.options = { dialogFade: true, backdrop: 'static', keyboard: false };
           $locationProvider.html5Mode(false);

           $urlRouterProvider.otherwise("/people/list");

           $stateProvider
               .state('app', {
                   abstract: true,
                   url: '/',
                   views: {
                       'main': { templateUrl: '/Ng/Views/people/index.html' }
                   }
               })


               //PEOPLE--->
               //GET ALL PEOPLE
               .state('app.people-list', {
                   url: 'people/list',
                   templateUrl: '/Ng/Views/people/people_list.html',
                   controller: 'GetAllPeopleController',

               })

               //GET EDIT PEOPLE
                .state('app.people-edit', {
                    url: 'people/edit/:Id',
                    templateUrl: '/Ng/Views/people/people_form.html',
                    controller: 'SavePeopleController',
                    controllerAs: 'person',
                    reloadOnSearch: false,
                })
               //GET DETAILS PEOPLE
                .state('app.people-details', {
                    url: 'people/details/:Id',
                    templateUrl: 'Views/people/people_form.html',
                    controller: 'GetPeopleController',
                    controllerAs: 'person',
                    reloadOnSearch: false,
                })
               //GET CREATE PEOPLE
               .state('app.people-create', {
                   url: 'people/create',
                   templateUrl: 'Views/people/people_form.html',
                   controller: 'SavePeopleController',
                   controllerAs: 'person',
                   reloadOnSearch: false,
               })



               //ATTENDANCES--->
               //List
               .state('app.attendances-list', {
                   url: 'attendances/list',
                   templateUrl: 'Views/attendances/list.html',
                   controller: 'AttendanceListController',
                   reloadOnSearch: false
               })

               //Save
                 .state('app.attendances-form', {
                     url: 'attendances/edit/:Id',
                     templateUrl: '/Ng/Views/attendances/form.html',
                     controller: 'AttendanceSaveController',
                     reloadOnSearch: false
                 })



               //DEPARTMENTS--->
               //GET ALL DEPARTMENTS
               .state('app.departments-list', {
                   url: 'departments/list',
                   templateUrl: '/Ng/Views/departments/list.html',
                   controller: 'GetAllDepartmentsController',
                   reloadOnSearch: false
               })

               //GET EDIT DEPARTMENT
               .state('app.departments-edit', {
                   url: 'departments/edit/:Id',
                   templateUrl: '/Ng/Views/departments/departments_edit.html',
                   controller: 'SaveDepartmentsController',
                   reloadOnSearch: false
               })

               //GET DETAILS DEPARTMENT
               .state('app.departments-details', {
                   url: 'departments/details/:Id',
                   templateUrl: '/Ng/Views/departments/departments_details.html',
                   controller: 'GetDepartmentsController',

                   reloadOnSearch: false
               })

               //GET CREATE DEPARTMENT
               .state('app.departments-create', {
                   url: 'departments/save',
                   templateUrl: '/Ng/Views/departments/departments_create.html',
                   controller: 'SaveDepartmentsController',
                   reloadOnSearch: false
               })

               //DELETE 
                  .state('app.departments-delete', {
                      controller: 'DeleteDepartmentsController',
                      reloadOnSearch: false
                  })


               //TYPES--->
               //TYPE LIST
                .state('app.types-list', {
                    url: 'types/list',
                    templateUrl: '/Ng/Views/types/list.html',
                    controller: 'GetAllTypesController',
                    reloadOnSearch: false
                })

               //TYPE FORM
                .state('app.types-form', {
                    url: 'type/edit/:Id',
                    templateUrl: '/Ng/Views/types/form.html',
                    controller: 'SaveTypesController',
                    reloadOnSearch: false
                })



               //HOLIDAYS--->
               //HOLIDAY LIST
                .state('app.holidays-list', {
                    url: 'holidays/list',
                    templateUrl: '/Ng/Views/holidays/list.html',
                    controller: 'HolidayListController',
                    reloadOnSearch: false
                })


               //HOLIDAY FORM
                .state('app.holidays-form', {
                    url: 'holidays/edit/:Id',
                    templateUrl: '/Ng/Views/holidays/form.html',
                    controller: 'HolidaySaveController',
                    reloadOnSearch: false
                })


               .state('app.flickr', {
                   url: 'flickr',
                   templateUrl: 'Views/flickr/flickr_index.html',
                   controller: 'FlickrController',
                   reloadOnSearch: false

               })

                  //Calendar
         .state('ContentCalendar',
         {
             url: '/company/:companyId/calendar/',
             templateUrl: "/ng/views/calendar/index.html",
             controller: ""
         })
         .state('ContentCalendar.view',
         {
             url: 'view',
             templateUrl: "/ng/views/calendar/view.html",
             controller: "AppCalendarViewController"
         })
           //End Calendar
       }]);

app.run(['$log', function ($log) {
    $log.log("Start.");
}]);

app.config(['uibDatepickerConfig', 'uibDatepickerPopupConfig', function (uibDatepickerConfig, uibDatepickerPopupConfig) {
    uibDatepickerConfig.startingDay = 1;
}]);