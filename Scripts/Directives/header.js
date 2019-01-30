var header = function () {
    return {
        restrict: 'A',
        templateUrl: 'pages/header.html',
        scope: true,
        transclude: false,
        controller: 'HeaderController'
    };
};
header.$inject = [];