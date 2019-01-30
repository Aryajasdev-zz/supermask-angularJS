var MainappController = function ($scope, $rootScope, ngCart, ngDialog, $http, $stateParams, $state) {
    //for the humburger slide

    $scope.search = function () {       
        $state.go('search', { q: q });
    };
    $scope.tgState = false;
    $rootScope.jsonData = '{"foo": "bar"}';
    $rootScope.theme = 'ngdialog-theme-default animated bounce';

    $scope.openQuickView = function (url, prodid) {
        $rootScope.produrl = url;
        $rootScope.productid = prodid;
        console.log(url + '-' + prodid);
        angular.element(document.querySelector("#box-" + $rootScope.productid)).addClass("zoomOut");
        ngDialog.open({
            template: 'QuickviewController',
            controller: ['$scope', '$timeout', function ($scope, $timeout) {
                $scope.productid = prodid;
                //function to load quickview in modal with loader spinner
                $scope.loading = true;
                $http.get("/getProductbyURL/" + url + "?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1", { cache: true })
				.then(function (response) {
				    $rootScope.Product = response.data;
				    console.log($rootScope.Product);
				    $scope.loading = false;
				    angular.element(document.querySelector(".qviewdiv")).removeClass("hidden");
				});
            }],
            className: 'ngdialog-theme-plain'
        });
    };
    $rootScope.$on('ngDialog.opened', function (e, $dialog) {
        //  angular.element(document.querySelector("#qview")).css("max-width","100%");
        angular.element(document.querySelector(".ngdialog-content")).css("max-width", "100%");
        console.log('Quickview opened: ' + $dialog.attr('id'));
    });
    $rootScope.$on('ngDialog.closed', function (e, $dialog) {
        var thisdialog = $dialog.attr('id');
        console.log('Quickview closed: ' + $dialog.attr('id'));
        // angular.element(document.querySelector("#qview")).css("max-width","0");
        angular.element(document.querySelector(".ngdialog-content")).css("max-width", "0");
        angular.element(document.querySelector("#box-" + $rootScope.productid)).removeClass("zoomOut").addClass("zoomIn");
    });
};

MainappController.$inject = ['$scope', '$rootScope', 'ngCart', 'ngDialog', '$http', '$stateParams', '$state'];
