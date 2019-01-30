var categoriesController = function ($scope, $rootScope, $http) {
    //sidebarSrv.toggleSidebar();    
    $scope.pageClass = 'page-categories';
    //hide the sidebar button
    $rootScope.hideit = true;
    $rootScope.callSidebarFunction = function () {
        sidebarSrv.toggleSidebar();
    };

    $http.get("/getCategories/masquerade-masks?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1&siteid=6").then(function (response) {
        $rootScope.Categories = response.data;
    });

    $scope.whatCat = function (categoryname, categoryurl) {
        $rootScope.thiscat = categoryname;
        $rootScope.thiscaturl = categoryurl;
        localStorage.setItem("currentCat", $rootScope.thiscat);
        localStorage.setItem("currentCatUrl", $rootScope.thiscaturl);
    };
};
categoriesController.$inject = ['$scope', '$rootScope', '$http'];