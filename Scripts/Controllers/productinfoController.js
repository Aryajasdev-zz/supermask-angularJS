var productinfoController = function ($scope, $rootScope, $state, $stateParams, $http, $sce, ngCart) {
    $scope.image = {};
    $scope.message = 'Product Info';
    $scope.pageClass = 'page-productinfo';
    document.body.style.marginLeft = "0";
    var produrl = $stateParams.url;
    $http.get("/getProductbyURL/" + produrl + "?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1", { cache: true }).then(function (response) {
        $rootScope.Product = response.data;
        $scope.image.img = $sce.trustAsResourceUrl($rootScope.Product.img);
    });
};

productinfoController.$inject = ['$scope', '$rootScope', '$state', '$stateParams', '$http', '$sce', 'ngCart'];
