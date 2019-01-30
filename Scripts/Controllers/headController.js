var headController = function ($scope, $location) {
    $scope.$watch(function () {
        return $location.path();
    }, function (value) {
        console.log(value);
        $scope.location = value;
    });
};

headController.$inject = ['$scope', '$location'];
