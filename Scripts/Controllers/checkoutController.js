var checkoutController = function ($scope, $rootScope, $state, $stateParams, ngCart, $http) {
    $scope.tot = ngCart.getTotalItems();
    $scope.cartitems = ngCart.getCart().items;
};
checkoutController.$inject = ['$scope', '$rootScope', '$state', '$stateParams', 'ngCart', '$http'];