
//Refrences for Intellesences
/// <reference path="C:\My Websites\masksapp\angular/angular.js" />
/// <reference path="Directives/header.js" />
/// <reference path="Directives/footer.js" />
/// <reference path="Directives/zoom.js" />
/// <reference path="Directives/animate.js" />

//Controller
/// <reference path="Controllers/MainappController.js" />
/// <reference path="Controllers/headController.js" />
/// <reference path="Controllers/footContoller.js" />
/// <reference path="Controllers/mainController.js" />
/// <reference path="Controllers/categoriesController.js" />
/// <reference path="Controllers/categoryController.js" />
/// <reference path="Controllers/productinfoController.js" />
/// <reference path="Controllers/aboutController.js" />
/// <reference path="Controllers/contactController.js" />

/// <reference path="Controllers/todaysdateController.js" />
/// <reference path="Controllers/datController.js" />
/// <reference path="Controllers/qtyController.js" />
/// <reference path="Controllers/checkoutController.js" />
/// <reference path="Controllers/realpayController.js" />
/// <reference path="Configuration/rootConfig.js" />


//Services
/// <reference path="Factories/cacheService.js" />
/// <reference path="Factories/sidebarService.js" />
/// <reference path="common.js" />
/// <reference path="ngCart.js" />


var myngapp = angular.module('myngApp', ['ui.router', 'ngAnimate', 'ngCart', 'duScroll',
    'angularUtils.directives.dirPagination', 'countdownTimer', 'percentCircle-directive', 'revolunet.stepper',
    'ngDialog', 'angularSpinners', 'angular-velocity']);

//Configuration
myngapp.config(rootConfig);

//Controllers
myngapp.controller('MainCtrl', MainappController);
myngapp.controller('HeaderController', headController);
myngapp.controller('FooterController', footController);

myngapp.controller('mainController', mainController);
myngapp.controller('categoriesController', categoriesController);
myngapp.controller('categoryController', categoryController);
myngapp.controller('productinfoController', productinfoController);

myngapp.controller('aboutController', aboutController);
myngapp.controller('contactController', contactController);
myngapp.controller('todaysdate', todaysdateController);
myngapp.controller('datCtrl', datController);
myngapp.controller('QtyStepsCtrl', qtyController);

myngapp.controller('checkoutController', checkoutController);
myngapp.controller('realpayController', realpayController);

//Services
myngapp.factory("myCache", myCacheService);
myngapp.factory("sidebarSrv", sidebarService);

//Directives
myngapp.directive('header', header);
myngapp.directive('footer', footer);
myngapp.directive('zoom', zoom);
myngapp.directive('animateOnLoad', animateDirective);