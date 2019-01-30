var footer = function () {
    return {
        restrict: 'A',
        templateUrl: 'pages/footer.html',
        scope: true,
        transclude: false,
        controller: 'FooterController'
    };
};
footer.$inject = [];