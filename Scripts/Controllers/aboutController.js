var aboutController = function ($scope, $location) {
    document.getElementById("main").style.marginLeft = "0";
    $scope.message = 'Look! I am an about page.';
    $scope.pageClass = 'page-about';
    document.body.style.marginLeft = "0";
};

aboutController.$inject = ['$scope', '$location'];
