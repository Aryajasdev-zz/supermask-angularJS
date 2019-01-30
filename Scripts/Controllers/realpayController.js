var realpayController = function ($scope, ngCart, $http, $stateParams) {
    $scope.orderid = $stateParams.orderid;
    //console.log($scope.orderid);
    $http.get("/getRealcode/real?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1&orderid=" + $scope.orderid).then(function (response) {
        $scope.payorder = response.data;
        //console.log($scope.payorder);
        document.forms.payment.target = "iframe";
        document.forms.payment.submit();
    });
};
realpayController.$inject = ['$scope', 'ngCart', '$http', '$stateParams'];
