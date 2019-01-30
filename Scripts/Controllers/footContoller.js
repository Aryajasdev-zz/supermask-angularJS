var footController = function ($scope) {
    $scope.submitFeedback = function (action) {
        if (action) {
            //good
            //submit using $http or service
        }
        else {
            //bad
            //submit using $http or service
        }
    };
};
footController.$inject = ['$scope'];
