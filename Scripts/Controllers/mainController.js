var mainController = function ($scope, $rootScope, ngCart, $http, $filter, $stateParams, myCache) {
    //homepage
    document.getElementById("main").style.marginLeft = "0";
    // create a message to display in our view
    $scope.message = 'this is the homepage later will hold all categories here';
    $scope.pageClass = 'page-home';
    $scope.currentPage = 1;
    $scope.pageSize = 12;    
    $scope.q = '';
    $rootScope.searchtxt = $stateParams.q;
    $scope.filled = false;   
    var cache = myCache.get('searchData');    
    if (cache) {
        //$scope.Products = $filter('filter')(cache,{name: $rootScope.searchtxt});
        $scope.Products = alasql("SELECT * FROM ? AS t where name like '%" + $rootScope.searchtxt + "%' or descr like '%" + $rootScope.searchtxt + "%' or keywords like '%" + $rootScope.searchtxt + "%'", [cache]);
        $scope.filled = true;
    } else {
        $http.get("/getallproducts/masquerade-masks?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1").then(function (response) {
            myCache.put('searchData', response.data);
            //$scope.Products = $filter('filter')(response.data,{name: $rootScope.searchtxt});
            $scope.Products = alasql("SELECT * FROM ? AS t where name like '%" + $rootScope.searchtxt + "%' or descr like '%" + $rootScope.searchtxt + "%' or keywords like '%" + $rootScope.searchtxt + "%'", [response.data]);
            $scope.filled = true;
        });
    }
};
mainController.$inject = ['$scope', '$rootScope', 'ngCart', '$http', '$filter', '$stateParams', 'myCache'];