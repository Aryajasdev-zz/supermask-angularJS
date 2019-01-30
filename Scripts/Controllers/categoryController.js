var categoryController = function ($scope, $stateParams, $rootScope, $state, ngCart, $http, $document, sidebarSrv, ngDialog) {
    ngCart.setPostageid(3);
    ngCart.setPostage(3.75);    
    $scope.message = 'one category';
    $scope.pageClass = 'page-category';
    //show the sidebar button
    $rootScope.categoryurl = $stateParams.url;
    //pagination
    $scope.currentPage = 1;
    $scope.pageSize = 12;
    $scope.q = '';

    $scope.changeCat = function (catname, caturl) {
        $scope.q = catname;
        $scope.catAnchor = caturl;
        ngDialog.close('ngdialog1');
    };
    //toggle sidebar
    $scope.callSidebarFunction = function () {
        sidebarSrv.toggleSidebar();
    };

    $scope.showcats = function () {
        ngDialog.openConfirm({
            template: 'subCatsDialogue',
            scope: $scope,
            closeByDocument: true,
            closeByEscape: true,
            showClose: true,
            closeByNavigation: true
        }).then(function (value) {
            var val = value;
            console.log(value);
            return val;
        });
    };
    $http.get("/getCategories/masquerade-masks?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1&siteid=6").then(function (response) {
        $rootScope.Categories = response.data;
    });

    $scope.changeMainCat = function (url, catname) {
        $rootScope.thiscat = catname;
        $rootScope.thiscaturl = url;
        localStorage.setItem("currentCat", $rootScope.thiscat);
        localStorage.setItem("currentCatUrl", $rootScope.thiscaturl);
        $state.go('category', { url: url });
        $('.navbar-collapse').removeClass('in');
        $scope.isNavCollapsed = !$scope.isNavCollapsed;

    };

    //scroll to top
    $scope.toTheTop = function () {
        $document.scrollTopAnimated(0, 500).then(function () {
            console && console.log('You just scrolled to the top!');
        });
    };

    $http.get("/getallproducts/" + $scope.categoryurl + "?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1&siteid=6")
    .then(function (response) {
        $scope.Products = response.data;        
        $scope.currentCat = localStorage.getItem("currentCat");
    });
    
    //get the sub categories
    $scope.currentCatUrl = localStorage.getItem("currentCatUrl");
    $http.get("/getCategories/" + $scope.currentCatUrl + "?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1&siteid=6")
    .then(function (response) {
        $scope.subCategories = response.data;
    });
};

categoryController.$inject = ['$scope', '$stateParams', '$rootScope', '$state', 'ngCart', '$http', '$document', 'sidebarSrv', 'ngDialog'];
