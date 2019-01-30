var rootConfig = function ($stateProvider, $locationProvider, $urlRouterProvider) {
    //$locationProvider.hashPrefix('!').html5Mode(true);
    $stateProvider
         // route for the categories page
       .state('home', {
           url: '/home',
           templateUrl: 'pages/categories.html',
           controller: 'categoriesController'
       })
       // route for each category page
       .state('category', {
           url: '/category/:url',
           templateUrl: 'pages/category.html',
           controller: 'categoryController'
       })
       // route for the product info page
       .state('product-info', {
           url: '/product-info/:url',
           templateUrl: 'pages/product-info.html',
           controller: 'productinfoController'
       })
       //search products 
       .state('search', {
           url: '/search/:q',
           templateUrl: 'pages/home.html',
           controller: 'mainController'
       })
       // route for the about page
       .state('about', {
           url: '/about',
           templateUrl: 'pages/about.html',
           controller: 'aboutController'
       })
       // route for the contact page
       .state('contact', {
           url: '/contact',
           templateUrl: 'pages/contact.html',
           controller: 'contactController'
       })
       // route for the checkout page
       .state('checkout', {
           url: '/checkout',
           templateUrl: 'pages/checkout.html',
           controller: 'checkoutController'
       })
       // for Realex Payment
       .state('realpay', {
           url: '/realpay/:orderid',
           templateUrl: 'pages/realpay.html',
           controller: 'realpayController'
       });
    $urlRouterProvider.otherwise('/home');
};
rootConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider'];