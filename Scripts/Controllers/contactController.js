var contactController = function ($scope, $location) {
    document.getElementById("main").style.marginLeft = "0";
    $scope.message = 'Contact us! JK. This is just a demo.';
    $scope.pageClass = 'page-contact';
    document.body.style.marginLeft = "0";
};

contactController.$inject = ['$scope', '$location'];
